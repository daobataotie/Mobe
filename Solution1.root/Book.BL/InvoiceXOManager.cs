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
        private static readonly BL.BGHandbookDetail1Manager bGHandbookDetail1Manager = new BGHandbookDetail1Manager();
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
            if (invoice != null)
                invoice.Details = invoiceXODetailAccessor.Select(invoice, false);
            return invoice;
        }

        public Model.InvoiceXO GetById(string invoiceId)
        {
            return accessor.Get(invoiceId);
        }

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

        protected override string GetInvoiceKind()
        {
            return "XO";
        }

        protected override Book.DA.IInvoiceAccessor GetAccessor()
        {
            return accessor;
        }

        protected override void _ValidateForUpdate(Book.Model.Invoice invoice)
        {
            base._ValidateForUpdate(invoice);
            Validate((Model.InvoiceXO)invoice);
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
            foreach (Model.InvoiceXODetail detail in invoice.Details)
            {
                if (detail.IsConfirmed != true)
                    throw new Helper.RequireValueException("Customer");
                if (detail.InvoiceXODetailQuantity == 0)
                    throw new Helper.RequireValueException("quantity");
            }
        }

        private void _Insert(Model.InvoiceXO invoice)
        {

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            if (invoice.Employee0 != null)
                invoice.Employee0Id = invoice.Employee0.EmployeeId;
            if (invoice.Customer != null)
                invoice.CustomerId = invoice.Customer.CustomerId;
            if (invoice.xocustomer != null)
                invoice.xocustomerId = invoice.xocustomer.CustomerId;
            invoice.Employee1Id = null; //invoice.Employee1.EmployeeId;
            invoice.InvoiceFlag = Convert.ToInt32(Helper.InvoiceFlag.WaitingForProcess);

            //过账人
            invoice.Employee2Id = null;// invoice.Employee2.EmployeeId;
            //过账时间
            invoice.InvoiceGZTime = DateTime.Now;
            invoice.IsClose = false;
            invoice.InvoiceMPSState = 0;
            accessor.Insert(invoice);
            foreach (Model.InvoiceXODetail detail in invoice.Details)
            {
                //if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId))
                if (string.IsNullOrEmpty(detail.ProductId))
                    continue;
                detail.InvoiceId = invoice.InvoiceId;
                detail.DetailsFlag = Convert.ToInt32(Helper.DetailsFlag.OnTheWay);
                detail.InvoiceXODetailQuantity0 = detail.InvoiceXODetailQuantity;
                detail.InvoiceXTDetailQuantity = 0;
                detail.InvoiceXODetailBeenQuantity = 0;
                detail.DetailMPSState = 0;
                invoiceXODetailAccessor.Insert(detail);

                //修改手册已定未出量
                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                {
                    bGHandbookDetail1Manager.UpdateYDWC(detail, detail.InvoiceXODetailQuantity0.Value);
                }
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
            //客户基本资料 最后交易日期
            string sql = "update Customer set LastTransactionDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where CustomerId='" + invoice.CustomerId + "'";
            accessor.UpdateSql(sql);
        }

        private void addUpdateDtail(Model.InvoiceXO invoice)
        {
            foreach (Model.InvoiceXODetail detail in invoice.Details)
            {
                //if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId))
                if (string.IsNullOrEmpty(detail.ProductId))
                    continue;
                detail.InvoiceId = invoice.InvoiceId;
                detail.InvoiceXTDetailQuantity = detail.InvoiceXTDetailQuantity == null ? 0 : detail.InvoiceXTDetailQuantity;
                //修改已出货记录等
                detail.InvoiceXODetailBeenQuantity = detail.InvoiceXODetailBeenQuantity == null ? 0 : detail.InvoiceXODetailBeenQuantity;
                detail.InvoiceXODetailQuantity0 = detail.InvoiceXODetailQuantity - detail.InvoiceXODetailBeenQuantity + detail.InvoiceXTDetailQuantity;
                if (detail.InvoiceXODetailQuantity0 < 0) detail.InvoiceXODetailQuantity0 = 0;
                if (detail.InvoiceXODetailQuantity0 == 0)
                    detail.DetailsFlag = Convert.ToInt32(Helper.DetailsFlag.AllArrived);
                else
                    detail.DetailsFlag = Convert.ToInt32(Helper.DetailsFlag.OnTheWay);

                //修改已排单记录
                detail.InvoiceMPSQuantity = detail.InvoiceMPSQuantity == null ? 0 : detail.InvoiceMPSQuantity;
                if (detail.InvoiceXODetailQuantity <= detail.InvoiceMPSQuantity)
                    detail.DetailMPSState = Convert.ToInt32(Helper.DetailsFlag.AllArrived);
                else
                    detail.DetailMPSState = Convert.ToInt32(Helper.DetailsFlag.OnTheWay);
                invoiceXODetailAccessor.Insert(detail);
                //修改手册已定出量
                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                {
                    bGHandbookDetail1Manager.UpdateYDWC(detail, detail.InvoiceXODetailQuantity0.Value);
                }
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
            //_ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;
            if (invoice.Employee0 != null)
                invoice.Employee0Id = invoice.Employee0.EmployeeId;
            if (invoice.Customer != null)
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
                            //foreach (Model.InvoiceXODetail detail in invoice.Details)
                            //{
                            //    if (detail.IsConfirmed != true)
                            //        throw new Helper.RequireValueException("Customer");
                            //}

                            invoiceOriginal.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
                            _TurnNull(invoiceOriginal);
                            invoiceXODetailAccessor.Delete(invoice);
                            this.addUpdateDtail(invoice);
                            EvaInvoiceFlag(invoice); //修改头状态
                            EvaInvoiceMPSFlag(invoice);//修改头计划状态
                            accessor.Update(invoice);
                            //invoice.InsertTime = invoiceOriginal.InsertTime;
                            //invoice.UpdateTime = DateTime.Now;
                            //_Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            //foreach (Model.InvoiceXODetail detail in invoice.Details)
                            //{
                            //    //暂时隐藏 修改产品表中 产品的数量
                            //    //Model.CustomerProducts cusproduct = detail.PrimaryKey;
                            //    //if (cusproduct.OrderQuantity == null)
                            //    //    cusproduct.OrderQuantity = 0;
                            //    //cusproduct.OrderQuantity -= detail.InvoiceXODetailQuantity;
                            //    //customerProductsAccessor.Update(cusproduct);
                            //}
                            //修改手册已定未出量
                            foreach (Model.InvoiceXODetail detail in invoice.Details)
                            {
                                if (!string.IsNullOrEmpty(detail.HandbookProductId) && !string.IsNullOrEmpty(detail.HandbookId))
                                {
                                    bGHandbookDetail1Manager.UpdateYDWC(detail, 0 - detail.InvoiceXODetailQuantity0.Value);
                                }
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

        public IList<Book.Model.InvoiceXO> SelectByYJRQCustomEmpCusXOId(Model.Customer customer1, Model.Customer customer2, DateTime startDate, DateTime endDate, DateTime yjrq1, DateTime yjrq2, Model.Employee employee1, Model.Employee employee2, string xoid1, string xoid2, string cusxoidkey, Model.Product product, Model.Product product2, bool isclose, bool mpsIsClose, bool isForeigntrade)
        {
            return accessor.SelectByYJRQCustomEmpCusXOId(customer1, customer2, startDate, endDate, yjrq1, yjrq2, employee1, employee2, xoid1, xoid2, cusxoidkey, product, product2, isclose, mpsIsClose, isForeigntrade);
        }

        public IList<Book.Model.InvoiceXO> SelectByCustomers(Model.Customer customer)
        {
            return accessor.SelectByCustomers(customer);
        }

        public void UpdateAccess(Book.Model.InvoiceXO invoiceXO)
        {
            accessor.Update(invoiceXO);
        }

        public void EvaInvoiceFlag(Model.InvoiceXO invoiceXO)
        {
            int flag = 0;
            foreach (Model.InvoiceXODetail detail in invoiceXO.Details)
            {
                flag += detail.DetailsFlag.HasValue ? detail.DetailsFlag.Value : 0;
            }
            if (flag == 0)
                invoiceXO.InvoiceFlag = 0;
            else if (flag < invoiceXO.Details.Count * 2)
                invoiceXO.InvoiceFlag = 1;
            else if (flag == invoiceXO.Details.Count * 2)
            {
                invoiceXO.InvoiceFlag = 2;
                invoiceXO.IsClose = true;



            }
        }

        public void UpdateInvoiceFlag(Model.InvoiceXO invoiceXO)
        {

            int flag = 0;
            IList<Model.InvoiceXODetail> list = invoiceXODetailAccessor.Select(invoiceXO, false);
            foreach (Model.InvoiceXODetail detail in list)
            {
                flag += detail.DetailsFlag.Value;
            }
            if (flag == 0)
            {
                invoiceXO.InvoiceFlag = 0;

                invoiceXO.IsClose = false;
                invoiceXO.JieAnDate = null;
            }
            else if (flag < list.Count * 2)
            {
                invoiceXO.InvoiceFlag = 1;

                invoiceXO.IsClose = false;
                invoiceXO.JieAnDate = null;
            }
            else if (flag == list.Count * 2)
            {
                invoiceXO.InvoiceFlag = 2;
                invoiceXO.IsClose = true;
                invoiceXO.JieAnDate = DateTime.Now;
                new BL.PronoteHeaderManager().UpdateHeaderIsClseByXOId(invoiceXO.InvoiceId, true);
            }
            accessor.Update(invoiceXO);
        }

        public void EvaInvoiceMPSFlag(Model.InvoiceXO invoiceXO)
        {
            int flag = 0;
            foreach (Model.InvoiceXODetail detail in invoiceXO.Details)
            {
                flag += detail.DetailMPSState == null ? 0 : detail.DetailMPSState.Value;
            }
            if (flag == 0)
                invoiceXO.InvoiceMPSState = 0;
            else if (flag < invoiceXO.Details.Count * 2)
                invoiceXO.InvoiceMPSState = 1;
            else if (flag == invoiceXO.Details.Count * 2)
            {
                invoiceXO.InvoiceMPSState = 2;
            }

        }

        public void UpdateInvoiceMPSFlag(Model.InvoiceXO invoiceXO)
        {
            int flag = 0;
            IList<Model.InvoiceXODetail> list = invoiceXODetailAccessor.Select(invoiceXO, false);
            foreach (Model.InvoiceXODetail detail in list)
            {
                flag += detail.DetailMPSState == null ? 0 : detail.DetailMPSState.Value;
            }
            if (flag == 0)
                invoiceXO.InvoiceMPSState = 0;
            else if (flag < list.Count * 2)
                invoiceXO.InvoiceMPSState = 1;
            else if (flag == list.Count * 2)
            {
                invoiceXO.InvoiceMPSState = 2;
            }
            accessor.Update(invoiceXO);

        }

        public IList<Model.InvoiceXO> SelectFlagNot2()
        {
            return accessor.SelectFlagNot2();
        }

        public IList<Model.InvoiceXO> SelectDateRangCusXOCustomer(DateTime startdate, DateTime enddate, string cusxoid, Model.Customer customer)
        {
            return accessor.SelectDateRangCusXOCustomer(startdate, enddate, cusxoid, customer);
        }

        public Model.InvoiceXO SelectMpsIsClose(string mpsheader)
        {
            return accessor.SelectMpsIsClose(mpsheader);
        }

        public string SelectCusXOIdByPronoteHeaderId(string id)
        {
            return accessor.SelectCusXOIdByPronoteHeaderId(id);
        }
    }
}

