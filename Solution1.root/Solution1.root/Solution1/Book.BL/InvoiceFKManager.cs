//------------------------------------------------------------------------------
//
// file name：InvoiceFKManager.cs
// author: peidun
// create date：2008/6/6 10:00:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceFK.
    /// </summary>
    public partial class InvoiceFKManager : InvoiceManager
    {
        //1入2退
        private static readonly DA.IInvoice01Accessor invoice01Accessor = (DA.IInvoice01Accessor)Accessors.Get("Invoice01Accessor");
        private static readonly DA.IXP1Accessor xp1Accessor = (DA.IXP1Accessor)Accessors.Get("XP1Accessor");
        private static readonly DA.IXP2Accessor xp2Accessor = (DA.IXP2Accessor)Accessors.Get("XP2Accessor");
        //private static readonly DA.ICompanyAccessor companyAccessor = (DA.ICompanyAccessor)Accessors.Get("CompanyAccessor");
        private static readonly DA.IInvoiceCGAccessor invoiceCGAccessor = (DA.IInvoiceCGAccessor)Accessors.Get("InvoiceCGAccessor");
        private static readonly DA.IInvoiceXTAccessor invoiceXTAccessor = (DA.IInvoiceXTAccessor)Accessors.Get("InvoiceXTAccessor");
        private static readonly DA.IAccountAccessor accountAccessor = (DA.IAccountAccessor)Accessors.Get("AccountAccessor");
        //private static readonly DA.ITransactionController transactionController = (DA.ITransactionController)Accessors.Get("TransactionController");


        #region Select

        public IList<Model.InvoiceFK> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }
        public Model.InvoiceFK Get(string invoiceid) 
        {
            return accessor.Get(invoiceid);
        }
        public IList<Model.InvoiceFK> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceFK))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceFK)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceFK))
                throw new ArgumentException();

            _Update((Model.InvoiceFK)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceFK))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceFK)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceFK))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceFK)invoice);
        }

        #endregion

        #endregion

        private void _TurnNull(Model.InvoiceFK invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceFK invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "FK";
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

            //decimal? payTotal = 0;

            //foreach (Model.Invoice01 detail in invoice.Details)
            //{
            //    payTotal += detail.PayReceived;
            //}
            //if (payTotal > invoice.InvoiceTotal)
            //{
            //    throw new Helper.RequireValueException("Total2");
            //}
        }

        protected override void _ValidateForInsert(Book.Model.Invoice invoice)
        {
            base._ValidateForInsert(invoice);

            Validate((Model.InvoiceFK)invoice);

        }

        private void Validate(Model.InvoiceFK invoice) 
        {
            if (invoice.Customer == null)
                throw new Helper.RequireValueException("Compamy");
            if (invoice.Account == null)
                throw new Helper.RequireValueException("Account");
            if (invoice.PayMethod == null)
                throw new Helper.RequireValueException("PayMethod");
        }

        #endregion

        private void _Insert(Model.InvoiceFK invoice)
        {
            _ValidateForInsert(invoice);

            invoice.Employee0Id = invoice.Employee0.EmployeeId;
            invoice.PayMethodId = invoice.PayMethod.PayMethodId;
            invoice.CustomerId = invoice.Customer.CustomerId;
            invoice.AccountId = invoice.Account.AccountId;

            invoice.Employee1Id = invoice.Employee1.EmployeeId;

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                invoice.InvoiceGZTime = DateTime.Now;
            }

            accessor.Insert(invoice);

            // 冲销记录
            SaveXPs(invoice);

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                // 单据欠款额
                foreach (Model.Invoice01 detail in invoice.Details)
                {
                    switch (detail.Kind.ToLower())
                    {
                        case "cg":
                            invoiceCGAccessor.OwedDecrement(detail.InvoiceId, detail.PayReceived == null ? 0 : detail.PayReceived);
                            break;

                        case "xt":
                            invoiceXTAccessor.OwedDecrement(detail.InvoiceId, detail.PayReceived == null ? 0 : detail.PayReceived);
                            break;
                    }
                }

                // 应付款额
                //companyAccessor.DecrementP(invoice.Company, invoice.InvoiceTotal);

                // 账户余额
                accountAccessor.Decrement(invoice.Account, invoice.InvoiceTotal);
            }

        }

        private void _Update(Model.InvoiceFK invoice)
        {
            _ValidateForUpdate(invoice);

            Model.InvoiceFK invoiceOriginal = accessor.Get(invoice.InvoiceId);

                switch ((Helper.InvoiceStatus)invoiceOriginal.InvoiceStatus)
                {
                    case Helper.InvoiceStatus.Draft:
                        switch ((Helper.InvoiceStatus)invoice.InvoiceStatus)
                        {
                            case Helper.InvoiceStatus.Draft:

                                invoice.UpdateTime = DateTime.Now;
                                invoice.PayMethodId = invoice.PayMethod.PayMethodId;
                                invoice.CustomerId = invoice.Customer.CustomerId;
                                invoice.AccountId = invoice.Account.AccountId;
                                invoice.Employee0Id = invoice.Employee0.EmployeeId;
                                accessor.Update(invoice);

                                // 冲销记录
                                this.SaveXPs(invoice);
                                break;

                            case Helper.InvoiceStatus.Normal:

                                invoice.UpdateTime = DateTime.Now;
                                invoice.PayMethodId = invoice.PayMethod.PayMethodId;
                                invoice.CustomerId = invoice.Customer.CustomerId;
                                invoice.AccountId = invoice.Account.AccountId;
                                invoice.Employee0Id = invoice.Employee0.EmployeeId;
                                invoice.Employee2Id = invoice.Employee2.EmployeeId;
                                invoice.InvoiceGZTime = DateTime.Now;
                                accessor.Update(invoice);

                                this.SaveXPs(invoice);

                                // 单据应付款额
                                foreach (Model.Invoice01 detail in invoice.Details)
                                {
                                    if (detail.PayReceived.HasValue && detail.PayReceived == 0)
                                        continue;

                                    switch (detail.Kind.ToLower())
                                    {
                                        case "cg":
                                            invoiceCGAccessor.OwedDecrement(detail.InvoiceId, detail.PayReceived);
                                            break;

                                        case "xt":
                                            invoiceXTAccessor.OwedDecrement(detail.InvoiceId, detail.PayReceived);
                                            break;
                                    }
                                }

                                // 应收应付
                                //companyAccessor.DecrementP(invoice.Company, invoice.InvoiceTotal);

                                // 账户余额
                                accountAccessor.Decrement(invoice.Account, invoice.InvoiceTotal);

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
                                IList<Model.XP1> xp1s = xp1Accessor.Select(invoiceOriginal);
                                foreach (Model.XP1 xp1 in xp1s)
                                {
                                    invoiceCGAccessor.OwedIncrement(xp1.InvoiceCG, xp1.XP1Money);
                                }
                                // 单据应付款额
                                // 销售退货单
                                IList<Model.XP2> xp2s = xp2Accessor.Select(invoiceOriginal);
                                foreach (Model.XP2 xp2 in xp2s)
                                {
                                    invoiceXTAccessor.OwedIncrement(xp2.InvoiceXT, xp2.XP2Money);
                                }

                                // 应收应付
                                //companyAccessor.IncrementP(invoiceOriginal.Company, invoiceOriginal.InvoiceTotal);

                                //修改账户余额
                                accountAccessor.Increment(invoiceOriginal.Account, invoiceOriginal.InvoiceTotal);

                                accessor.Delete(invoice.InvoiceId);

                                invoice.InsertTime = invoiceOriginal.InsertTime;
                                invoice.UpdateTime = DateTime.Now;
                                invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;

                                _Insert(invoice);
                                break;

                            case Helper.InvoiceStatus.Null:
                                invoice.UpdateTime = DateTime.Now;
                                invoice.Employee3Id = invoice.Employee3.EmployeeId;
                                invoice.InvoiceZFTime = DateTime.Now;
                                invoice.InvoiceZFCause = "";
                                accessor.Update(invoice);

                                // 单据应付款额
                                // 采购进货单
                                IList<Model.XP1> xp1s1 = xp1Accessor.Select(invoice);
                                foreach (Model.XP1 xp1 in xp1s1)
                                {
                                    invoiceCGAccessor.OwedIncrement(invoiceCGAccessor.Get(xp1.InvoiceCGId), xp1.XP1Money);
                                }
                                // 单据应付款额
                                // 销售退货单
                                IList<Model.XP2> xp2s1 = xp2Accessor.Select(invoice);
                                foreach (Model.XP2 xp2 in xp2s1)
                                {
                                    invoiceXTAccessor.OwedIncrement(invoiceXTAccessor.Get(xp2.InvoiceXTId), xp2.XP2Money);
                                }

                                // 应收应付
                                //companyAccessor.IncrementP(companyAccessor.Get(invoice.CompanyId), invoice.InvoiceTotal);

                                //修改账户余额
                                accountAccessor.Increment(accountAccessor.Get(invoice.AccountId), invoice.InvoiceTotal);
                                break;
                        }
                        break;

                    case Helper.InvoiceStatus.Null:
                        throw new InvalidOperationException();
                }
        }

        #region Helpers

        private void SaveXPs(Model.InvoiceFK invoice)
        {
            xp1Accessor.Delete(invoice);
            xp2Accessor.Delete(invoice);

            foreach (Model.Invoice01 detail in invoice.Details)
            {
                switch (detail.Kind.ToLower())
                {
                    case "cg":
                        //冲销应付款情况1
                        Model.XP1 xp1 = new Book.Model.XP1();
                        xp1.InvoiceCGId = detail.InvoiceId;
                        xp1.InvoiceFKId = invoice.InvoiceId;
                        xp1.XP1Id = Guid.NewGuid().ToString();
                        xp1.XP1Money = detail.PayReceived == null ? decimal.Zero : detail.PayReceived.Value;
                        xp1Accessor.Insert(xp1);
                        break;

                    case "xt":
                        //冲销应付款情况2
                        Model.XP2 xp2 = new Book.Model.XP2();
                        xp2.InvoiceXTId = detail.InvoiceId;
                        xp2.InvoiceFKId = invoice.InvoiceId;
                        xp2.XP2Id = Guid.NewGuid().ToString();
                        xp2.XP2Money = detail.PayReceived == null ? decimal.Zero : detail.PayReceived.Value;
                        xp2Accessor.Insert(xp2);
                        break;

                }
            }
        }

        #endregion

    }
}

