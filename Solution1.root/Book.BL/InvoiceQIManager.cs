//------------------------------------------------------------------------------
//
// file name：InvoiceQIManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceQI.
    /// </summary>
    public partial class InvoiceQIManager : InvoiceManager
    {
        private static readonly DA.IAccountAccessor accountAccessor = (DA.IAccountAccessor)Accessors.Get("AccountAccessor");
        private static readonly DA.IInvoiceQIDetailAccessor invoiceQIDetailAccessor = (DA.IInvoiceQIDetailAccessor)Accessors.Get("InvoiceQIDetailAccessor");


        #region Select

        public IList<Model.InvoiceQI> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceQI> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
        public Model.InvoiceQI Get(string invoiceId)
        {
            Model.InvoiceQI invoice =accessor.Get(invoiceId);
            invoice.Details = invoiceQIDetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQI))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceQI)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQI))
                throw new ArgumentException();

            _Update((Model.InvoiceQI)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQI))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceQI)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQI))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceQI)invoice);
        }

        private void _TurnNull(Model.InvoiceQI invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceQI invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #endregion

        #region Other

        protected override string GetInvoiceKind()
        {
            return "QI";
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

            Validate((Model.InvoiceQI)invoice);
        }

        private void Validate(Model.InvoiceQI invoice)
        {
            if (invoice.Account == null)
                throw new Helper.RequireValueException("Account");

            if (invoice.Details.Count == 0)
            {
                throw new Helper.RequireValueException("Details");
            }
        }
        #endregion

        #endregion

        #region Helpers

        private void _Insert(Model.InvoiceQI invoice)
        {
            _ValidateForInsert(invoice);
            
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.AccountId = invoice.Account.AccountId;

            invoice.Employee1Id = invoice.Employee1.EmployeeId;


            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                //过账人
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                //过账时间
                invoice.InvoiceGZTime = DateTime.Now;
            }
            accessor.Insert(invoice);

            foreach (Model.InvoiceQIDetail detail in invoice.Details)
            {
                detail.InvoiceId = invoice.InvoiceId;
                invoiceQIDetailAccessor.Insert(detail);
            }

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                Model.Account account = invoice.Account;
                account.AccountBalance1 += invoice.InvoiceTotal;
                accountAccessor.Update(account);
            }
        }

        private void _Update(Model.InvoiceQI invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.AccountId = invoice.Account.AccountId;

            Model.InvoiceQI invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            invoiceQIDetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceQIDetail detail in invoice.Details)
                            {
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoiceQIDetailId = Guid.NewGuid().ToString();
                                invoiceQIDetailAccessor.Insert(detail);
                            }
                            break;
                        case Helper.InvoiceStatus.Normal:
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            //accessor.Update(invoice);

                            //invoiceQIDetailAccessor.Delete(invoice);

                            //foreach (Model.InvoiceQIDetail detail in invoice.Details)
                            //{
                            //    detail.InvoiceId = invoice.InvoiceId;
                            //    detail.InvoiceQIDetailId = Guid.NewGuid().ToString();
                            //    invoiceQIDetailAccessor.Insert(detail);
                            //}

                            //Model.Account account = invoice.Account;
                            //account.AccountBalance1 += invoice.InvoiceTotal;
                            //accountAccessor.Update(account);

                            //invoice.Employee2Id = invoice.Employee2.EmployeeId;
                            //invoice.InvoiceGZTime = DateTime.Now;
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
                            //Model.Account account1 = invoiceOriginal.Account;
                            //account1.AccountBalance1 -= invoiceOriginal.InvoiceTotal;
                            //accountAccessor.Update(account1);

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

                            Model.Account account = invoice.Account;
                            account.AccountBalance1 -= invoice.InvoiceTotal;
                            accountAccessor.Update(account);
                            break;
                    }
                    break;

                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }
        #endregion

    }
}

