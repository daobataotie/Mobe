//------------------------------------------------------------------------------
//
// file name：InvoiceQKManager.cs
// author: peidun
// create date：2008/7/28 11:05:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceQK.
    /// </summary>
    public partial class InvoiceQKManager : InvoiceManager
    {


        #region Select

        public IList<Model.InvoiceQK> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public Model.InvoiceQK Get(string invoiceId)
        {
            return accessor.Get(invoiceId) ;
        }

        public IList<Model.InvoiceQK> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        #endregion


        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQK))
                throw new ArgumentException();

            _Insert((Model.InvoiceQK)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQK))
                throw new ArgumentException();

            _Update((Model.InvoiceQK)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQK))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceQK)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQK))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceQK)invoice);
        }
        private void _TurnNull(Model.InvoiceQK invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceQK invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }
        #endregion

        #region Other

        protected override string GetInvoiceKind()
        {
            return "QK";
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

            Validate((Model.InvoiceQK)invoice);
        }

        #endregion

        #endregion


        #region Helpers

        private void _Update(Model.InvoiceQK invoice)
        {
            _ValidateForUpdate(invoice);

            Model.InvoiceQK invoiceOriginal = this.Get(invoice.InvoiceId);

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            invoice.UpdateTime = DateTime.Now;
                            invoice.CustomerId = invoice.Customer.CustomerId;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            accessor.Update(invoice);
                            break;

                        case Helper.InvoiceStatus.Normal:
                            invoice.UpdateTime = DateTime.Now;
                            invoice.CustomerId = invoice.Customer.CustomerId;
                            invoice.Employee0Id = invoice.Employee0.EmployeeId;
                            invoice.Employee1Id = invoice.Employee1.EmployeeId;
                            invoice.Employee2Id = invoice.Employee2.EmployeeId;
                            invoice.InvoiceGZTime = DateTime.Now;

                            accessor.Update(invoice);
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
                            invoice.UpdateTime = DateTime.Now;
                            accessor.Update(invoice);
                            break;

                        case Helper.InvoiceStatus.Null:
                            invoice.UpdateTime = DateTime.Now;
                            invoice.Employee3Id = invoice.Employee3.EmployeeId;
                            invoice.InvoiceZFTime = DateTime.Now;
                            invoice.InvoiceZFCause = "";
                            accessor.Update(invoice);
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }

        private void _Insert(Model.InvoiceQK invoice)
        {
            _ValidateForInsert(invoice);

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
            invoice.CustomerId = invoice.Customer.CustomerId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.Employee1Id = invoice.Employee1.EmployeeId;

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                //过账人
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                //过账时间
                invoice.InvoiceGZTime = DateTime.Now;
            }
            accessor.Insert(invoice);
        }

        private void Validate(Model.InvoiceQK invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");
        }

        #endregion
    }
}

