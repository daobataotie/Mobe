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
        private static readonly DA.IInvoiceXJDetailAccessor invoiceXJDetailAccessor = (DA.IInvoiceXJDetailAccessor)Accessors.Get("InvoiceXJDetailAccessor");
        private static readonly DA.IInvoiceXJProcessAccessor invoiceXJProcessAccessor = (DA.IInvoiceXJProcessAccessor)Accessors.Get("InvoiceXJProcessAccessor");



        #region Select

        public IList<Model.InvoiceXJ> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceXJ> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
        public Model.InvoiceXJ Get(string invoiceId)
        {
            Model.InvoiceXJ invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceXJDetailAccessor.Select(invoice);
            invoice.DetailsProcess = invoiceXJProcessAccessor.Select(invoice);
            return invoice;
        }
        #endregion


        #region Override

        #region Operations

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

        #endregion

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

        #region Other

        protected override string GetInvoiceKind()
        {
            return "XJ";
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

            Validate((Model.InvoiceXJ)invoice);

        }

        #endregion

        #endregion

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

            foreach (Model.InvoiceXJDetail detail in invoice.Details)
            {
                //if (invoice.ProductType == 1)
                //{
                    //if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId))
                    //   throw new Exception("貨品不為空");
             //   }
               // if (invoice.ProductType == 0)
              //  { 
                        if(detail.Product==null||string.IsNullOrEmpty(detail.Product.ProductId))
                        throw new Exception("貨品不為空");
               // }
                detail.InvoiceId = invoice.InvoiceId;
                detail.Customer = invoice.Customer;
                if (detail.Customer != null)
                    detail.CustomerId = detail.Customer.CustomerId;
                detail.IsCustomerProduct = detail.Product.IsCustomerProduct;
                invoiceXJDetailAccessor.Insert(detail);
            }
            //foreach (Model.InvoiceXJProcess detail in invoice.DetailsProcess)
            //{
            //    //if (invoice.ProductType == 1)
            //    //{
            //    //if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId))
            //    //   throw new Exception("貨品不為空");
            //    //   }
            //    // if (invoice.ProductType == 0)
            //    //  { 
            //    if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
            //        continue;
            //    // }
            //    detail.InvoiceXJProcessId = Guid.NewGuid().ToString();
            //    detail.InvoiceXJId = invoice.InvoiceId;
            //    //detail.ProcessCategoryId = detail.ProcessCategory == null ? null : detail.ProcessCategory.ProcessCategoryId;                
            
            //    detail.ProductId= detail.Product==null?null :detail.Product.ProductId;
            //    invoiceXJProcessAccessor.Insert(detail);
            //}
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

                            invoiceXJDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceXJDetail detail in invoice.Details)
                            {
                                //   if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId)) continue; 
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))                                      continue;
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                                if (detail.Customer != null)
                                    detail.CustomerId = detail.Customer.CustomerId;
                                detail.IsCustomerProduct = detail.Product.IsCustomerProduct;
                                invoiceXJDetailAccessor.Insert(detail);
                            }

                            break;
                        case Helper.InvoiceStatus.Normal:

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

            if (invoice.Details.Count == 0)
            {
                throw new Helper.RequireValueException("Details");
            }

        }

    }
}

