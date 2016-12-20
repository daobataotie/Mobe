//------------------------------------------------------------------------------
//
// file name：InvoiceXJManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceXJ.
    /// </summary>
    public partial class InvoiceXJManager : InvoiceManager
    {
        //private static readonly DA.IInvoiceXJDetailAccessor _invoiceXJDetailManager = (DA.IInvoiceXJDetailAccessor)Accessors.Get("InvoiceXJDetailAccessor");
        //private static readonly DA.IInvoiceXJProcessAccessor _invoiceXJProcessManager = (DA.IInvoiceXJProcessAccessor)Accessors.Get("InvoiceXJProcessAccessor");

        private static readonly InvoiceXJDetailManager _invoiceXJDetailManager = new InvoiceXJDetailManager();
        private static readonly InvoiceXJProcessManager _invoiceXJProcessManager = new InvoiceXJProcessManager();
        private static readonly InvoiceXJPackageDetailsManager _invoiceXJPackageDetailsManager = new InvoiceXJPackageDetailsManager();

        public IList<Model.InvoiceXJ> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceXJ> Select(DateTime startDate, DateTime endDate,string customerid,string productid,string invoicexjid,string companyid)
        {
            return accessor.Select(startDate, endDate, customerid, productid, invoicexjid, companyid);
        }

        public IList<Model.InvoiceXJ> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        public Model.InvoiceXJ Get(string invoiceId)
        {
            Model.InvoiceXJ invoice = accessor.Get(invoiceId);
            //invoice.Details = invoiceXJDetailAccessor.Select(invoice);
            //invoiceXJDetailAccessor.getRecursiveInvoiceXJDetails(invoiceId);
            invoice.DetailsProcess = _invoiceXJProcessManager.Select(invoice);
            invoice.DetailPackage = _invoiceXJPackageDetailsManager.SelectByHeaderId(invoiceId);
            return invoice;
        }

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXJ))
                throw new ArgumentException();

            _Insert((Model.InvoiceXJ)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXJ))
                throw new ArgumentException();

            _Update((Model.InvoiceXJ)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXJ))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceXJ)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceXJ))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceXJ)invoice);
        }

        private void _TurnNull(Model.InvoiceXJ invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceXJ invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        protected override string GetInvoiceKind()
        {
            return "XJ";
        }

        protected override Book.DA.IInvoiceAccessor GetAccessor()
        {
            return accessor;
        }

        protected override void _ValidateForUpdate(Book.Model.Invoice invoice)
        {
            base._ValidateForUpdate(invoice);
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);

            Validate((Model.InvoiceXJ)invoice);

        }

        private void _Insert(Model.InvoiceXJ invoice)
        {
            _ValidateForInsert(invoice);
            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.CustomerId = invoice.Customer.CustomerId;
            invoice.Employee1Id = null;
            //过账人
            invoice.Employee2Id = null;
            //过账时间
            invoice.InvoiceGZTime = DateTime.Now;

            accessor.Insert(invoice);

            //报价详细
            foreach (Model.InvoiceXJDetail detail in invoice.Details)
            {
                detail.InvoiceId = invoice.InvoiceId;
                detail.Customer = invoice.Customer;
                if (detail.Customer != null)
                    detail.CustomerId = detail.Customer.CustomerId;
                //detail.IsCustomerProduct = detail.Product.IsCustomerProduct;
                _invoiceXJDetailManager.Insert(detail);
            }

            //加工
            foreach (Model.InvoiceXJProcess detail in invoice.DetailsProcess)
            {
                detail.InvoiceXJProcessId = Guid.NewGuid().ToString();
                detail.InvoiceXJId = invoice.InvoiceId;
                detail.ProcessCategoryId = detail.ProcessCategory == null ? null : detail.ProcessCategory.ProcessCategoryId;
                detail.ProductId = detail.Product == null ? null : detail.Product.ProductId;
                //detail.SupplierId = detail.Supplier == null ? null : detail.Supplier.SupplierId;
                _invoiceXJProcessManager.Insert(detail);
            }

            //包材
            foreach (Model.InvoiceXJPackageDetails d in invoice.DetailPackage)
            {
                d.InvoiceXJPackageDetailsId = Guid.NewGuid().ToString();
                d.InvoiceId = invoice.InvoiceId;
                _invoiceXJPackageDetailsManager.Insert(d);
            }
        }

        private void _Update(Model.InvoiceXJ invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.CustomerId = invoice.Customer.CustomerId;

            Model.InvoiceXJ invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            //_invoiceXJDetailManager.Delete(invoice);

                            foreach (Model.InvoiceXJDetail detail in invoice.Details)
                            {
                                //   if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId)) continue; 
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                                if (detail.Customer != null)
                                    detail.CustomerId = detail.Customer.CustomerId;
                                detail.IsCustomerProduct = detail.Product.IsCustomerProduct;
                                _invoiceXJDetailManager.Insert(detail);
                            }

                            break;
                        case Helper.InvoiceStatus.Normal:
                            //_invoiceXJDetailManager.Delete(invoiceOriginal); //删除详细
                            _invoiceXJProcessManager.Delete(invoiceOriginal); //删除加工
                            _invoiceXJPackageDetailsManager.DeleteByHeader(invoiceOriginal.InvoiceId);      //删除包材
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            throw new ArithmeticException();
                    }
                    break;
                case Helper.InvoiceStatus.Normal:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            throw new InvalidOperationException();
                        case Helper.InvoiceStatus.Normal:
                            //_invoiceXJDetailManager.Delete(invoiceOriginal); //删除详细
                            _invoiceXJProcessManager.Delete(invoiceOriginal); //删除加工
                            _invoiceXJPackageDetailsManager.DeleteByHeader(invoiceOriginal.InvoiceId);      //删除包材
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            break;
                        case Helper.InvoiceStatus.Null:
                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Null:
                    throw new ArithmeticException();
            }
        }

        private void Validate(Model.InvoiceXJ invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoice.Customer == null)
                throw new Helper.RequireValueException("Company");

            if (invoice.Details == null)
            {
                throw new Helper.RequireValueException("Details");
            }

        }

        public override void Delete(string invoiceId)
        {
            try
            {
                BL.V.BeginTransaction();

                _invoiceXJDetailManager.DeleteByHeaderId(invoiceId);
                _invoiceXJProcessManager.DeleteByHeaderId(invoiceId);
                _invoiceXJPackageDetailsManager.DeleteByHeader(invoiceId);

                accessor.Delete(invoiceId);
                BL.V.CommitTransaction();
            }
            catch
            {

                BL.V.RollbackTransaction();
            }
        }
    }
}

