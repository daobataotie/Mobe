//------------------------------------------------------------------------------
//
// file name：InvoiceFTManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceFT.
    /// </summary>
    public partial class InvoiceFTManager : InvoiceManager
    {
        private static readonly DA.IAccountAccessor accountAccessor = (DA.IAccountAccessor)Accessors.Get("AccountAccessor");


        #region Select

        public IList<Model.InvoiceFT> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public IList<Model.InvoiceFT> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceFT))
                throw new ArgumentException();
            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;
        
            _Insert((Model.InvoiceFT)invoice);
       }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceFT))
                throw new ArgumentException();

            _Update((Model.InvoiceFT)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceFT))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceFT)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceFT))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceFT)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceFT invoice) 
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceFT invoice) 
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "FT";
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

            Validate((Model.InvoiceFT)invoice);
        }

        #endregion

        #endregion


        private void _Insert(Model.InvoiceFT invoice)
        {
            _ValidateForInsert(invoice);            
            invoice.Account1Id = invoice.Account1.AccountId;
            invoice.Account2Id = invoice.Account2.AccountId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            invoice.Employee1Id = invoice.Employee1.EmployeeId;

            if (invoice.InvoiceStatus == (int)Helper.InvoiceStatus.Normal)
            {
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                invoice.InvoiceGZTime = DateTime.Now;
            }

            accessor.Insert(invoice);

            if (invoice.InvoiceStatus == (int)Helper.InvoiceStatus.Normal)
            {
                Model.Account account1 = invoice.Account1;
                Model.Account account2 = invoice.Account2;
                account1.AccountBalance1 -= invoice.InvoiceTotal;
                account2.AccountBalance1 += invoice.InvoiceTotal;

                accountAccessor.Update(account1);
                accountAccessor.Update(account2);
            }
        }

        private void _Update(Model.InvoiceFT invoice)
        {
            _ValidateForUpdate(invoice);

            invoice.UpdateTime = DateTime.Now;
            invoice.Account1Id = invoice.Account1.AccountId;
            invoice.Account2Id = invoice.Account2.AccountId;
            invoice.Employee0Id = invoice.Employee0.EmployeeId;

            Model.InvoiceFT invoiceOriginal = this.Get(invoice.InvoiceId);

            Helper.InvoiceStatus invoiceStatus = (Helper.InvoiceStatus)invoice.InvoiceStatus.Value;

            switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
            {
                case Helper.InvoiceStatus.Draft:
                    switch (invoiceStatus)
                    {
                        case Helper.InvoiceStatus.Draft:
                            break;
                        case Helper.InvoiceStatus.Normal:
                            accessor.Delete(invoiceOriginal.InvoiceId);
                            invoice.InsertTime = invoiceOriginal.InsertTime;
                            invoice.UpdateTime = DateTime.Now;
                            _Insert(invoice);
                            //invoice.Employee2Id = invoice.Employee2.EmployeeId;
                            //invoice.InvoiceGZTime = DateTime.Now;
                            //accessor.Update(invoice);

                            //Model.Account account1 = invoice.Account1;
                            //Model.Account account2 = invoice.Account2;
                            //account1.AccountBalance1 -= invoice.InvoiceTotal;
                            //account2.AccountBalance1 += invoice.InvoiceTotal;

                            //accountAccessor.Update(account1);
                            //accountAccessor.Update(account2);


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
                            //invoice.Employee2Id = invoice.Employee2.EmployeeId;                           
                            //accessor.Update(invoice);

                            //Model.Account account1 = invoice.Account1;
                            //Model.Account account2 = invoice.Account2;

                            //account1.AccountBalance1 += invoiceOriginal.InvoiceTotal;
                            //account1.AccountBalance1 -= invoice.InvoiceTotal;

                            //account2.AccountBalance1 -= invoiceOriginal.InvoiceTotal;
                            //account2.AccountBalance1 += invoice.InvoiceTotal;

                            //accountAccessor.Update(account1);
                            //accountAccessor.Update(account2);


                            break;
                        case Helper.InvoiceStatus.Null:
                            //invoice.Employee3Id = V.ActiveEmployee.EmployeeId;
                            //invoice.InvoiceZFTime = DateTime.Now;
                            //invoice.InvoiceZFCause = "";

                            //accessor.Update(invoice);

                            //消除影响

                            Model.Account acc1 = invoice.Account1;
                            Model.Account acc2 = invoice.Account2;
                            acc1.AccountBalance1 += invoice.InvoiceTotal;
                            acc2.AccountBalance1 -= invoice.InvoiceTotal;

                            accountAccessor.Update(acc1);
                            accountAccessor.Update(acc2);
                            break;
                    }
                    break;
                case Helper.InvoiceStatus.Null:
                    throw new InvalidOperationException();
            }
        }

        public void Validate(Model.InvoiceFT invoice) 
        {
            if (invoice.Account1 == null)
                throw new Helper.RequireValueException("Account1");

            if (invoice.Account2 == null) 
                throw new Helper.RequireValueException("Account2");

            if (invoice.Employee0 == null) 
                throw new Helper.RequireValueException("Employee0");

            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.InvoiceTotal == null) 
                throw new Helper.RequireValueException("Total");
        }

        public Model.InvoiceFT Get(string invoiceId)
        {
            return accessor.Get(invoiceId);
        }
    }
}

