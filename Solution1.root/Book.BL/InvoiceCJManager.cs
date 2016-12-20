//------------------------------------------------------------------------------
//
// file name：InvoiceCJManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceCJ.
    /// </summary>
    public partial class InvoiceCJManager : InvoiceManager
    {
        private static readonly DA.IInvoiceCJDetailAccessor invoiceCJDetailAccessor = (DA.IInvoiceCJDetailAccessor)Accessors.Get("InvoiceCJDetailAccessor");

        #region Select

        public IList<Model.InvoiceCJ> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceCJ> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }


        /// <summary>
        /// Select by primary key.
        /// </summary>
        public Model.InvoiceCJ Get(string invoiceId)
        {
            Model.InvoiceCJ invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceCJDetailAccessor.Select(invoice);
            return invoice;
        }

        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCJ))
                throw new ArgumentException();

            _Insert((Model.InvoiceCJ)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCJ))
                throw new ArgumentException();

            _Update((Model.InvoiceCJ)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCJ))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceCJ)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceCJ))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceCJ)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceCJ invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceCJ invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "CJ";
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

        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);

            Validate((Model.InvoiceCJ)invoice);

        }

        #endregion

        #endregion

        #region Helpers

        private void _Update(Model.InvoiceCJ invoice)
        {
            _ValidateForUpdate(invoice);

            Model.InvoiceCJ invoiceOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        #region Draft 
                        case Helper.InvoiceStatus.Draft:

                            invoice.UpdateTime = DateTime.Now;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            invoice.SupplierId = invoice.Supplier.SupplierId;
                            accessor.Update(invoice);

                            invoiceCJDetailAccessor.Delete(invoice);
                            foreach (Model.InvoiceCJDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoiceCJDetailId = Guid.NewGuid().ToString();
                                invoiceCJDetailAccessor.Insert(detail);
                            }
                            break;
                        #endregion
                        #region Normal
                        case Helper.InvoiceStatus.Normal:

                            accessor.Delete(invoice.InvoiceId);

                            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;

                            _Insert(invoice);
                            break;
                        #endregion
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

                            invoice.UpdateTime = DateTime.Now;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            invoice.SupplierId = invoice.Supplier.SupplierId;

                            accessor.Update(invoice);

                            invoiceCJDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceCJDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoiceCJDetailId = Guid.NewGuid().ToString();
                                invoiceCJDetailAccessor.Insert(detail);
                            }

                            break;

                        case Helper.InvoiceStatus.Null:

                            invoice.UpdateTime = DateTime.Now;
                            invoice.Employee3Id = null;
                            invoice.InvoiceZFTime = DateTime.Now;
                            accessor.Update(invoice);

                            foreach (Model.InvoiceCJDetail detail in invoice.Details)
                            {
                                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                                invoiceCJDetailAccessor.Update(detail);
                            }
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();

            }
        }

        private void _Insert(Model.InvoiceCJ invoice)
        {
            _ValidateForInsert(invoice);

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.SupplierId = invoice.Supplier.SupplierId;
            invoice.Employee1Id = invoice.Employee0Id;
            invoice.Employee2Id = null;
            invoice.InvoiceGZTime = DateTime.Now;
            accessor.Insert(invoice);

            foreach (Model.InvoiceCJDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                detail.InvoiceId = invoice.InvoiceId;
                invoiceCJDetailAccessor.Insert(detail);
            }
        }

        private void Validate(Model.InvoiceCJ invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoice.Supplier == null)
                throw new Helper.RequireValueException("Company");

            if (invoice.Details.Count == 0)
            {
                throw new Helper.RequireValueException("Details");
            }
            else
            {

                if (string.IsNullOrEmpty(invoice.Details[0].ProductId))
                {
                    if (invoice.Details.Count <= 1)
                    {
                        throw new Helper.RequireValueException("Details");
                    }
                }
            }

            foreach (Model.InvoiceCJDetail detail in invoice.Details)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId)) continue;
                if (detail.InvoiceCJDetailMoney == 0)
                    throw new Helper.RequireValueException("Price");
            }
        }

        #endregion
    }
}

