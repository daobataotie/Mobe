//------------------------------------------------------------------------------
//
// file name：InvoiceZSManager.cs
// author: peidun
// create date：2008/6/20 15:33:49
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceZS.
    /// </summary>
    public partial class InvoiceZSManager : InvoiceManager
    {
        private readonly static DA.IStockAccessor stockAccessor = (DA.IStockAccessor)Accessors.Get("StockAccessor");
        private static readonly DA.ICustomerProductsAccessor customerProductsAccessor = (DA.ICustomerProductsAccessor)Accessors.Get("CustomerProductsAccessor");
        private static readonly DA.IInvoiceZSDetailAccessor invoiceZSDetailAccessor = (DA.IInvoiceZSDetailAccessor)Accessors.Get("InvoiceZSDetailAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");

        #region Select

        public IList<Model.InvoiceZS> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceZS> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceZS Get(string invoiceId)
        {
            Model.InvoiceZS invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceZSDetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceZS))
                throw new ArgumentException();
            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;           
            _Insert((Model.InvoiceZS)invoice);

        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceZS))
                throw new ArgumentException();
            invoice.UpdateTime = DateTime.Now;                       
            _Update((Model.InvoiceZS)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceZS))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceZS)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceZS))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceZS)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceZS invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceZS invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "ZS";
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

            Validate((Model.InvoiceZS)invoice);

        }

        #endregion

        #endregion

        private void _Insert(Model.InvoiceZS invoice)
        {
            _ValidateForInsert(invoice);
           //客户
            if(invoice.Customer!=null)
            invoice.CustomerId = invoice.Customer.CustomerId;
            // 库房
            //invoice.DepotId = invoice.Depot.DepotId;
            //经手人
            if (invoice.Employee0 != null)
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            //录单人
            if(invoice.Employee1!=null)
            invoice.Employee1Id = invoice.Employee1.EmployeeId;

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                //过账人
                if(invoice.Employee2!=null)
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                //过账时间
                invoice.InvoiceGZTime = DateTime.Now;
            }
            //插入表单
            accessor.Insert(invoice);

            //插入明细
            foreach (Model.InvoiceZSDetail detail in invoice.Details)
            {
                if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId))
                {
                    throw new Exception("商品不為空");
                }
                if (detail.DepotPosition == null || detail.DepotPositionId == null)
                {
                    throw new Exception("貨位不為空");
                }
            
                detail.InvoiceId = invoice.InvoiceId;
                invoiceZSDetailAccessor.Insert(detail);

                Model.CustomerProducts p = detail.PrimaryKey;
                p.PrimaryKeyId = detail.PrimaryKey.PrimaryKeyId;
                if (p.DepotQuantity == null)
                    p.DepotQuantity = 0;
                if (p.OrderQuantity == null)
                    p.OrderQuantity = 0;
                p.DepotQuantity -= detail.InvoiceZSDetailQuantity;               
                //更新产品表库存
                customerProductsAccessor.Update(p);                      
            }
        }

        private void _Update(Model.InvoiceZS invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;
            //invoice.DepotId = invoice.Depot.DepotId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.CustomerId = invoice.Customer.CustomerId;

            Model.InvoiceZS invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            invoiceZSDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceZSDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceZSDetailId = Guid.NewGuid().ToString();
                                detail.InvoiceId = invoice.InvoiceId;
                                invoiceZSDetailAccessor.Insert(detail);
                            }
                            break;
                        case Helper.InvoiceStatus.Normal:
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                             break;
                        case Helper.InvoiceStatus.Null:
                            throw new InvalidOperationException();
                    }
                    break;
                case Helper.InvoiceStatus.Normal:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            throw new InvalidOperationException();
                        case Helper.InvoiceStatus.Normal:
                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invoiceOriginal);
                            invoiceZSDetailAccessor.Delete(invoiceOriginal);
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                         
                            _Insert(invoice);
                            ////消除影响
                            //foreach (Model.InvoiceZSDetail detail in invoiceOriginal.Details)
                            //{
                            //    stockAccessor.Increment(invoiceOriginal.Depot, detail.Product, detail.InvoiceZSDetailQuantity.Value);
                            //}
                            //accessor.Delete(invoiceOriginal.InvoiceId);

                            //invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
                            //invoice.InsertTime = invoiceOriginal.InsertTime;
                            //invoice.UpdateTime = DateTime.Now;

                            //_Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:

                            //invoice.Employee3Id = V.ActiveEmployee.EmployeeId;
                            //invoice.InvoiceZFTime = DateTime.Now;
                            //invoice.InvoiceZFCause = "";

                            //accessor.Update(invoice);

                            //invoice.Details = invoiceZSDetailAccessor.Select(invoice);

                            //消除影响
                            foreach (Model.InvoiceZSDetail detail in invoice.Details)
                            {
                                //if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;

                                Model.CustomerProducts p = detail.PrimaryKey;
                                p.PrimaryKeyId = detail.PrimaryKey.PrimaryKeyId;
                                if (p.DepotQuantity == null)
                                    p.DepotQuantity = 0;
                                if (p.OrderQuantity == null)
                                    p.OrderQuantity = 0;
                                p.DepotQuantity += detail.InvoiceZSDetailQuantity;
                                customerProductsAccessor.Update(p);
                            }
                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }

        private void Validate(Model.InvoiceZS invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            //if (invoice.Employee1 == null)
              //  throw new Helper.RequireValueException("Employee1");

            if (invoice.Customer == null)
                throw new Helper.RequireValueException("Company");

            //if (invoice.Depot == null)
            //    throw new Helper.RequireValueException("Depot");

            if (invoice.Details.Count == 0)
                throw new Helper.RequireValueException("Details");

            foreach (Model.InvoiceZSDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.InvoiceZSDetailQuantity == 0)
                    throw new Helper.RequireValueException("Details");
            }
        }

    }
}