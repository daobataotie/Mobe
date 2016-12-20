//------------------------------------------------------------------------------
//
// file name：InvoiceSKManager.cs
// author: peidun
// create date：2008/6/6 14:48:22
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceSK.
    /// </summary>
    public partial class InvoiceSKManager : InvoiceManager
    {
        private static readonly DA.IInvoice01Accessor invoice01Accessor = (DA.IInvoice01Accessor)Accessors.Get("Invoice01Accessor");
        private static readonly DA.IXR1Accessor xr1Accessor = (DA.IXR1Accessor)Accessors.Get("XR1Accessor");
        private static readonly DA.IXR2Accessor xr2Accessor = (DA.IXR2Accessor)Accessors.Get("XR2Accessor");
        //private static readonly DA.ICompanyAccessor companyAccessor = (DA.ICompanyAccessor)Accessors.Get("CompanyAccessor");
        private static readonly DA.IInvoiceCTAccessor invoiceCTAccessor = (DA.IInvoiceCTAccessor)Accessors.Get("InvoiceCTAccessor");
        private static readonly DA.IInvoiceXSAccessor invoiceXSAccessor = (DA.IInvoiceXSAccessor)Accessors.Get("InvoiceXSAccessor");
        private static readonly DA.IAccountAccessor accountAccessor = (DA.IAccountAccessor)Accessors.Get("AccountAccessor");

        #region Select

        public IList<Model.InvoiceSK> Select(DateTime start, DateTime end)
        {
            return accessor.Select(start, end);
        }

        public Model.InvoiceSK Get(string invoiceid)
        {
            return accessor.Get(invoiceid);
        }
        public IList<Model.InvoiceSK> Select(Helper.InvoiceStatus status)
        {
            return accessor.Select(status);
        }

        #endregion

        #region Override

        #region Operations

        protected override void _Insert(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceSK))
                throw new ArgumentException();

            invoice.InsertTime = DateTime.Now;
            invoice.UpdateTime = DateTime.Now;

            _Insert((Model.InvoiceSK)invoice);
        }

        protected override void _Update(Book.Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceSK))
                throw new ArgumentException();

            _Update((Model.InvoiceSK)invoice);
        }

        protected override void _TurnNormal(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceSK))
                throw new ArgumentException();

            _TurnNormal((Model.InvoiceSK)invoice);
        }

        protected override void _TurnNull(Model.Invoice invoice)
        {
            if (!(invoice is Model.InvoiceSK))
                throw new ArgumentException();

            _TurnNull((Model.InvoiceSK)invoice);
        }

        #endregion

        private void _TurnNull(Model.InvoiceSK invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Null;
            _Update(invoice);
        }

        private void _TurnNormal(Model.InvoiceSK invoice)
        {
            invoice.InvoiceStatus = (int)Helper.InvoiceStatus.Normal;
            _Update(invoice);
        }

        #region Other

        protected override string GetInvoiceKind()
        {
            return "SK";
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

            Validate((Model.InvoiceSK)invoice);
        }

        #endregion

        #endregion

        private void _Insert(Model.InvoiceSK invoice)
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
            this.SaveXRs(invoice);

            if ((Helper.InvoiceStatus)invoice.InvoiceStatus.Value == Helper.InvoiceStatus.Normal)
            {
                // 单据欠款额
                foreach (Model.Invoice01 detail in invoice.Details)
                {
                    switch (detail.Kind.ToLower())
                    {
                        case "ct":
                            invoiceCTAccessor.OwedDecrement(detail.InvoiceId, detail.PayReceived);
                            break;

                        case "xs":
                            invoiceXSAccessor.OwedDecrement(detail.InvoiceId, detail.PayReceived);
                            break;
                    }
                }
                //Model.Company company = invoice.Company;

                // 应收款额
                //companyAccessor.DecrementR(invoice.Company, invoice.InvoiceTotal);

                // 账户余额
                accountAccessor.Increment(invoice.Account, invoice.InvoiceTotal);
            }
        }

        private void _Update(Model.InvoiceSK invoice)
        {
            _ValidateForUpdate(invoice);

            Model.InvoiceSK invoiceOriginal = accessor.Get(invoice.InvoiceId);

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

                            this.SaveXRs(invoice);
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


                            // 单据应付款额
                            foreach (Model.Invoice01 detail in invoice.Details)
                            {
                                if (detail.PayReceived.HasValue && detail.PayReceived == 0)
                                    continue;

                                switch (detail.Kind.ToLower())
                                {
                                    case "xs":
                                        invoiceXSAccessor.OwedDecrement(detail.InvoiceId, detail.PayReceived);
                                        break;

                                    case "ct":
                                        invoiceCTAccessor.OwedDecrement(detail.InvoiceId, detail.PayReceived);
                                        break;
                                }
                            }

                            // 应收应付
                            //companyAccessor.DecrementR(invoice.Company, invoice.InvoiceTotal);

                            // 账户余额
                            accountAccessor.Increment(invoice.Account, invoice.InvoiceTotal);

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
                            IList<Model.XR1> xr1s1 = xr1Accessor.Select(invoiceOriginal);
                            foreach (Model.XR1 xr1 in xr1s1)
                            {
                                invoiceXSAccessor.OwedIncrement(xr1.InvoiceXS, xr1.XR1Money);
                            }
                            // 单据应收款额
                            // 采购退货单
                            IList<Model.XR2> xr2s1 = xr2Accessor.Select(invoiceOriginal);
                            foreach (Model.XR2 xr2 in xr2s1)
                            {
                                invoiceCTAccessor.OwedIncrement(xr2.InvoiceCT, xr2.XR2Money);
                            }

                            // 应收应付
                            //companyAccessor.IncrementR(invoice.Company, invoice.InvoiceTotal);

                            //修改账户余额
                            accountAccessor.Increment(invoice.Account, invoice.InvoiceTotal);

                            accessor.Delete(invoiceOriginal.InvoiceId);

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

                            // 单据应收款额
                            // 销售出货单
                            IList<Model.XR1> xr1s = xr1Accessor.Select(invoice);
                            foreach (Model.XR1 xr1 in xr1s)
                            {
                                invoiceXSAccessor.OwedIncrement(invoiceXSAccessor.Get(xr1.InvoiceXSId), xr1.XR1Money);
                            }
                            // 单据应收款额
                            // 采购退货单
                            IList<Model.XR2> xr2s = xr2Accessor.Select(invoice);
                            foreach (Model.XR2 xr2 in xr2s)
                            {
                                invoiceCTAccessor.OwedIncrement(invoiceCTAccessor.Get(xr2.InvoiceCTId), xr2.XR2Money);
                            }

                            // 应收应付
                            //companyAccessor.IncrementR(companyAccessor.Get(invoice.CompanyId), invoice.InvoiceTotal);

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

        private void SaveXRs(Model.InvoiceSK invoice)
        {
            xr1Accessor.Delete(invoice);
            xr2Accessor.Delete(invoice);

            foreach (Model.Invoice01 detail in invoice.Details)
            {
                switch (detail.Kind.ToLower())
                {
                    case "xs":
                        //冲销应付款情况1
                        Model.XR1 xr1 = new Book.Model.XR1();
                        xr1.InvoiceXSId = detail.InvoiceId;
                        xr1.InvoiceSKId = invoice.InvoiceId;
                        xr1.XR1Id = Guid.NewGuid().ToString();
                        xr1.XR1Money = detail.PayReceived == null ? decimal.Zero : detail.PayReceived.Value;
                        xr1Accessor.Insert(xr1);
                        break;

                    case "ct":
                        //冲销应付款情况2
                        Model.XR2 xr2 = new Book.Model.XR2();
                        xr2.InvoiceCTId = detail.InvoiceId;
                        xr2.InvoiceSKId = invoice.InvoiceId;
                        xr2.XR2Id = Guid.NewGuid().ToString();
                        xr2.XR2Money = detail.PayReceived == null ? decimal.Zero : detail.PayReceived.Value;
                        xr2Accessor.Insert(xr2);
                        break;

                }
            }
        }

        #endregion

        private void Validate(Model.InvoiceSK invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceId))
                throw new Helper.RequireValueException("Id");

            if (invoice.Employee0 == null)
                throw new Helper.RequireValueException("Employee0");

            if (invoice.Customer == null)
                throw new Helper.RequireValueException("Company");

            if (invoice.PayMethod == null) 
                throw new Helper.RequireValueException("PayMethod");

            if (invoice.Account == null)
                throw new Helper.RequireValueException("Account");

            if (invoice.InvoiceTotal.Value <= 0)
                throw new Helper.RequireValueException("Total");
        }

    }
}
