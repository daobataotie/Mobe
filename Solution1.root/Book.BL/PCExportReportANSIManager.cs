//------------------------------------------------------------------------------
//
// file name：PCExportReportANSIManager.cs
// author: mayanjun
// create date：2012-3-9 17:01:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PCExportReportANSIManager : BaseManager
    {
        public void Delete(string exportReportId)
        {
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(exportReportId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Insert(Model.PCExportReportANSI pCExportReportANSI)
        {
            Validate(pCExportReportANSI);
            try
            {
                BL.V.BeginTransaction();

                TiGuiExists(pCExportReportANSI);

                pCExportReportANSI.InsertTime = DateTime.Now;
                pCExportReportANSI.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCExportReportANSI.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCExportReportANSI.InsertTime.Value.Year, pCExportReportANSI.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCExportReportANSI.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCExportReportANSI);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.PCExportReportANSI pCExportReportANSI)
        {
            if (pCExportReportANSI != null)
            {
                Validate(pCExportReportANSI);
                pCExportReportANSI.UpdateTime = DateTime.Now;
                accessor.Update(pCExportReportANSI);
            }
        }

        protected override string GetSettingId()
        {
            return "pcExpANSIRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcExpANSI";
        }

        private void TiGuiExists(Model.PCExportReportANSI model)
        {
            if (this.ExistsPrimary(model.ExportReportId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.ReportDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.ReportDate.Value.Year, model.ReportDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.ReportDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.ExportReportId = this.GetId(model.ReportDate.Value);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.PCExportReportANSI mPCExpANSI)
        {
            if (string.IsNullOrEmpty(mPCExpANSI.ExportReportId))
                throw new Helper.RequireValueException(Model.PCExportReportANSI.PRO_ExportReportId);
            if (string.IsNullOrEmpty(mPCExpANSI.ProductId))
                throw new Helper.RequireValueException(Model.PCExportReportANSI.PRO_ProductId);
            if (string.IsNullOrEmpty(mPCExpANSI.InvoiceCusXOId))
                throw new Helper.RequireValueException(Model.PCExportReportANSI.PRO_InvoiceCusXOId);
            if (string.IsNullOrEmpty(mPCExpANSI.CustomerId.ToString()))
                throw new Helper.RequireValueException(Model.PCExportReportANSI.PRO_CustomerId);
            if (string.IsNullOrEmpty(mPCExpANSI.Amount.ToString()))
                throw new Helper.RequireValueException(Model.PCExportReportANSI.PRO_Amount);
            if (string.IsNullOrEmpty(mPCExpANSI.AmountTest.ToString()))
                throw new Helper.RequireValueException(Model.PCExportReportANSI.PRO_AmountTest);
            if (string.IsNullOrEmpty(mPCExpANSI.EmployeeId))
                throw new Helper.RequireValueException(Model.PCExportReportANSI.PRO_EmployeeId);
            if (string.IsNullOrEmpty(mPCExpANSI.ReportDate.ToString()))
                throw new Helper.RequireValueException(Model.PCExportReportANSI.PRO_ReportDate);

            //测试数量未达标
            //受测数量默认为订单数量的1/500,无条件进位.最大12
            //int mInvoiceXoDetailQuantity = int.Parse(mPCExpANSI.Amount.HasValue ? mPCExpANSI.Amount.ToString() : "0");

            //double mMustCheck = 0;

            //if (mInvoiceXoDetailQuantity < 500)
            //    mMustCheck = 1;
            //else
            //    mMustCheck = mInvoiceXoDetailQuantity % 500 == 0 ? mInvoiceXoDetailQuantity / 500 : mInvoiceXoDetailQuantity / 500 + 1;

            //mMustCheck = mMustCheck > 12 ? 12 : mMustCheck;

            //if (mPCExpANSI.AmountTest.Value != mMustCheck)
            //    throw new Helper.InvalidValueException(Model.PCExportReportANSI.PRO_AmountTest + "_ForInvoiceXoQuantity");

            //double RightCount = mPCExpANSI.ShouCeShu1.Value + mPCExpANSI.ShouCeShu2.Value + mPCExpANSI.ShouCeShu3.Value + mPCExpANSI.ShouCeShu4.Value + mPCExpANSI.ShouCeShu5.Value + mPCExpANSI.ShouCeShu6.Value + mPCExpANSI.ShouCeShu7.Value + mPCExpANSI.ShouCeShu8.Value + mPCExpANSI.ShouCeShu9.Value + mPCExpANSI.ShouCeShu10.Value + mPCExpANSI.ShouCeShu11.Value;
            //if (mPCExpANSI.AmountTest != RightCount)
            //    throw new Helper.InvalidValueException(Model.PCExportReportANSI.PRO_AmountTest + "_ForDetailsCount");
        }

        public Model.PCExportReportANSI SelectForExpANSI(string InvoiceCusXoid, string productid)
        {
            return accessor.SelectForExpANSI(InvoiceCusXoid, productid);
        }

        public IList<Model.PCExportReportANSI> SelectByDateRage(string ExpType, DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId)
        {
            return accessor.SelectByDateRage(ExpType, startdate, enddate, product, customer, CusXOId);
        }

        public IList<Model.PCExportReportANSI> SelectForChooseExport(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId, string ExpType)
        {
            return accessor.SelectForChooseExport(startdate, enddate, product, customer, CusXOId, ExpType);
        }

        public Model.PCExportReportANSI mget_last(string ExpType)
        {
            return accessor.mget_last(ExpType);
        }

        public Model.PCExportReportANSI mget_first(string ExpType)
        {
            return accessor.mget_first(ExpType);
        }

        public Model.PCExportReportANSI mget_prev(string ExpType, DateTime InsertTime)
        {
            return accessor.mget_prev(ExpType, InsertTime);
        }

        public Model.PCExportReportANSI mget_next(string ExpType, DateTime InsertTime)
        {
            return accessor.mget_next(ExpType, InsertTime);
        }

        public bool mhas_rows(string ExpType)
        {
            return accessor.mhas_rows(ExpType);
        }

        public bool mhas_rows_before(string ExpType, DateTime InsertTime)
        {
            return accessor.mhas_rows_before(ExpType, InsertTime);
        }

        public bool mhas_rows_after(string ExpType, DateTime InsertTime)
        {
            return accessor.mhas_rows_after(ExpType, InsertTime);
        }

        public IList<Model.PCExportReportANSI> SelectByInvoiceCusId(string invoiceCusId, string type)
        {
            return accessor.SelectByInvoiceCusId(invoiceCusId, type);
        }
    }
}

