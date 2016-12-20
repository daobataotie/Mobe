//------------------------------------------------------------------------------
//
// file name：InvoiceQOManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceQO.
    /// </summary>
    public partial class InvoiceQOManager : InvoiceManager
    {
        private static readonly DA.IAccountAccessor accountAccessor = (DA.IAccountAccessor)Accessors.Get("AccountAccessor");
        private static readonly DA.IInvoiceQODetailAccessor invoiceQODetailAccessor = (DA.IInvoiceQODetailAccessor)Accessors.Get("InvoiceQODetailAccessor");

        #region Select

        public IList<Model.InvoiceQO> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceQO> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }
        public Model.InvoiceQO Get(string invoiceId)
        {
            Model.InvoiceQO invoice = accessor.Get(invoiceId);
            invoice.Details = invoiceQODetailAccessor.Select(invoice);
            return invoice;
        }
        #endregion


        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQO))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceQO)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQO))
                throw new ArgumentException();

            _Update((Model.InvoiceQO)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQO))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceQO)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceQO))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceQO)invoice);
        }
        private void _TurnNull(Model.InvoiceQO invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceQO invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #endregion

        #region Other

        protected override string GetInvoiceKind()
        {
            return "QO";
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

            Validate((Model.InvoiceQO)invoice);
        }

        #endregion

        #endregion

        private void _Insert(Model.InvoiceQO invoice)
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

            foreach (Model.InvoiceQODetail detail in invoice.Details)
            {
                detail.InvoiceId = invoice.InvoiceId;
                invoiceQODetailAccessor.Insert(detail);
            }

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                Model.Account account = invoice.Account;
                account.AccountBalance1 -= invoice.InvoiceTotal;
                accountAccessor.Update(account);
            }
        }
		

        private void _Update(Model.InvoiceQO invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.AccountId = invoice.Account.AccountId;

            Model.InvoiceQO invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:

                            accessor.Update(invoice);

                            invoiceQODetailAccessor.Delete(invoice);

                            foreach (Model.InvoiceQODetail detail in invoice.Details)
                            {
                                detail.InvoiceId = invoice.InvoiceId;
                                detail.InvoiceQODetailId = Guid.NewGuid().ToString();
                                invoiceQODetailAccessor.Insert(detail);
                            }
                            break;
                        case Helper.InvoiceStatus.Normal:
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            //accessor.Update(invoice);

                            //invoiceQODetailAccessor.Delete(invoice);

                            //foreach (Model.InvoiceQODetail detail in invoice.Details)
                            //{
                            //    detail.InvoiceId = invoice.InvoiceId;
                            //    detail.InvoiceQODetailId = Guid.NewGuid().ToString();
                            //    invoiceQODetailAccessor.Insert(detail);
                            //}

                            //Model.Account account = invoice.Account;
                            //account.AccountBalance1 -= invoice.InvoiceTotal;
                            //accountAccessor.Update(account);

                            //invoice.Employee2Id = invoice.Employee2.EmployeeId;
                            //invoice.InvoiceGZTime = DateTime.Now;
                            break;
                        case Helper.InvoiceStatus.Null:
                            throw new InvalidOperationException();
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
                            //Model.Account accountOriginal = invoiceOriginal.Account;
                            //accountOriginal.AccountBalance1 += invoiceOriginal.InvoiceTotal;
                            //accountAccessor.Update(accountOriginal);

                            //accessor.Delete(invoiceOriginal.InvoiceId);

                            //invoice.InsertTime = invoiceOriginal.InsertTime;
                            //invoice.UpdateTime = DateTime.Now;
                            //invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;

                            //_Insert(invoice);

                            break;
                        case Helper.InvoiceStatus.Null:

                            //invoice.Employee3Id = V.ActiveEmployee.EmployeeId;
                            //invoice.InvoiceZFTime = DateTime.Now;
                            //invoice.InvoiceZFCause = "";

                            //accessor.Update(invoice);

                            Model.Account account = invoice.Account;
                            account.AccountBalance1 += invoice.InvoiceTotal;
                            accountAccessor.Update(account);
                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }

        private void Validate(Model.InvoiceQO invoice)
        {
            if (invoice.Account == null)
                throw new Helper.RequireValueException("Account");

            if (invoice.Details.Count == 0)
            {
                throw new Helper.RequireValueException("Details");
            }
        }

    }
}

