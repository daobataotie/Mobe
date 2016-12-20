//------------------------------------------------------------------------------
//
// file name：InvoiceXOManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXO.
    /// </summary>
    public partial class InvoiceXOManager : InvoiceManager
    {
        private static readonly DA.IInvoiceXODetailAccessor invoiceXODetailAccessor = (DA.IInvoiceXODetailAccessor)Accessors.Get("InvoiceXODetailAccessor");
        private static readonly DA.ICustomerProductsAccessor customerProductsAccessor = (DA.ICustomerProductsAccessor)Accessors.Get("CustomerProductsAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");

        #region Select

        public IList<Model.InvoiceXO> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceXO> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceXO Get(string invoiceId)
        {
            Model.InvoiceXO invoice = accessor.Get(invoiceId);
            if (invoice!=null)
            invoice.Details = invoiceXODetailAccessor.Select(invoice);
            return invoice;
        }
        public Model.InvoiceXO GetById(string invoiceId)
        {
            return accessor.Get(invoiceId);
        }
        #endregion


        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXO))
                throw new ArgumentException();

            _Insert((Model.InvoiceXO)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXO))
                throw new ArgumentException();

            _Update((Model.InvoiceXO)invoice);
        }
        public  void Updates(Book.Model.InvoiceXO invoiceXO)
        {
            if (!(invoiceXO is Model.InvoiceXO))
                throw new ArgumentException();

               accessor.Updates(invoiceXO);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXO))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceXO)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXO))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceXO)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceXO invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceXO invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "XO";
        }

        protected override Book.DA.IInvoiceAccessor GetAccessor()
        {
            return accessor;
        }

        #endregion

        #region Validation

        protected override void _ValidateForUpdate(Book.Model.Invoice invoice)
        {
            base._ValidateForUpdate(invoice);
            //
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);

            Validate((Model.InvoiceXO)invoice);
        }

        private void Validate(Book.Model.InvoiceXO invoice)
        {
            if (invoice.Customer == null) 
            {
                throw new Helper.RequireValueException("Company");
            }           
            if (invoice.Details.Count == 0)
            {
                throw new Helper.RequireValueException("Details");
            }
        }

        #endregion

        #endregion


        private void _Insert(Model.InvoiceXO invoice)
        {
            _ValidateForInsert(invoice);

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.CustomerId = invoice.Customer.CustomerId;
            if (invoice.xocustomer!=null)
            invoice.xocustomerId = invoice.xocustomer.CustomerId;
            invoice.Employee1Id = null; //invoice.Employee1.EmployeeId;
            invoice.InvoiceFlag=Convert.ToInt32(Helper.InvoiceFlag.WaitingForProcess);

            //过账人
            invoice.Employee2Id = null;// invoice.Employee2.EmployeeId;
            //过账时间
            invoice.InvoiceGZTime = DateTime.Now;

            accessor.Insert(invoice);

            foreach (Model.InvoiceXODetail detail in invoice.Details)
            {
                //if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId))
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    continue;                
                detail.InvoiceId = invoice.InvoiceId;
                detail.DetailsFlag =Convert.ToInt32(Helper.DetailsFlag.OnTheWay);
                detail.InvoiceXODetailQuantity0 = detail.InvoiceXODetailQuantity;
                detail.InvoiceXODetailBeenQuantity = 0; 
                invoiceXODetailAccessor.Insert(detail);

              //  Model.CustomerProducts cusproduct = detail.PrimaryKey;
                //if (cusproduct.OrderQuantity == null)
                //    cusproduct.OrderQuantity = 0;
                //cusproduct.OrderQuantity += detail.InvoiceXODetailQuantity;
                //customerProductsAccessor.Update(cusproduct);

                //产品表已定未出量
                //Model.Product product = productAccessor.Get(detail.ProductId);

                //product.OrderOnWayQuantity -= detail.OrderQuantity;
                //productAccessor.Update(product);
              
            }
        }

        private void _Update(Model.InvoiceXO invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.CustomerId = invoice.Customer.CustomerId;

            Model.InvoiceXO invoiceOriginal = this.Get(invoice.InvoiceId);
           
            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            break;
                        case Helper.InvoiceStatus.Normal:

                            accessor.Update(invoice);

                            invoiceXODetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceXODetail detail in invoice.Details)
                            {
                              // if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId)) continue;
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceXODetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceXODetailAccessor.Insert(detail);
                            }

                            invoice.InvoiceGZTime = DateTime.Now;
                            invoice.Employee2Id = invoice.Employee2.EmployeeId;
                            break;
                        case Helper.InvoiceStatus.Null:
                            throw new InvalidOperationException();
                        default:
                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Normal:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            throw new InvalidOperationException();
                        case Helper.InvoiceStatus.Normal:
                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            foreach (Model.InvoiceXODetail detail in invoice.Details)
                            {
                                //暂时隐藏 修改产品表中 产品的数量
                                //Model.CustomerProducts cusproduct = detail.PrimaryKey;
                                //if (cusproduct.OrderQuantity == null)
                                //    cusproduct.OrderQuantity = 0;
                                //cusproduct.OrderQuantity -= detail.InvoiceXODetailQuantity;
                                //customerProductsAccessor.Update(cusproduct);
                            }
                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }
        public IList<Book.Model.InvoiceXO> SelectNoDeal()
        {
            return accessor.SelectNoDeal();
        }
        public IList<Book.Model.InvoiceXO> SelectByYJRQCustomEmp(Model.Customer customer, string startDate, string endDate,  Model.Employee employee)
        {
            return accessor.SelectByYJRQCustomEmp(customer, startDate, endDate, employee);
        }
        public IList<Book.Model.InvoiceXO> SelectByCustomers(Model.Customer customer)
        {
            return accessor.SelectByCustomers(customer);
        }
       
        public void UpdateXoState(Book.Model.InvoiceXO invoiceXO)
        {
            accessor.Updates(invoiceXO);
        }
        public void UpdateInvoiceFlag(Model.InvoiceXO invoiceXO)
        {
            int flag = 0;
            IList<Model.InvoiceXODetail> list = invoiceXODetailAccessor.Select(invoiceXO);
            foreach (Model.InvoiceXODetail detail in list)
            {
                flag += detail.DetailsFlag.Value;
            }
            if (flag == 0)
                invoiceXO.InvoiceFlag = 0;
            else if (flag < list.Count * 2)
                invoiceXO.InvoiceFlag = 1;
            else if (flag == list.Count * 2)
                invoiceXO.InvoiceFlag = 2;
            accessor.Update(invoiceXO);

        }
        public IList<Model.InvoiceXO> SelectFlagNot2()
        {
            return accessor.SelectFlagNot2();
        }
    }
}

