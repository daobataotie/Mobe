//------------------------------------------------------------------------------
//
// file name：OpticsTestManager.cs
// author: mayanjun
// create date：2012-4-21 09:55:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.OpticsTest.
    /// </summary>
    public partial class OpticsTestManager : BaseManager
    {
        public void Delete(string opticsTestId)
        {
            accessor.Delete(opticsTestId);
        }

        public void DeleteByPCPGOnlineCheckDetailId(string PCPGOnlineCheckDetailId)
        {
            accessor.DeleteByPCPGOnlineCheckDetailId(PCPGOnlineCheckDetailId);
        }

        public void Insert(Model.OpticsTest opticsTest)
        {
            Validate(opticsTest);
            try
            {
                BL.V.BeginTransaction();

                opticsTest.InsertTime = DateTime.Now;
                opticsTest.UpdateTime = DateTime.Now;

                TiGuiExists(opticsTest);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, opticsTest.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, opticsTest.InsertTime.Value.Year, opticsTest.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, opticsTest.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(opticsTest);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw new Helper.InvalidValueException(Model.OpticsTest.PRO_PCPGOnlineCheckDetailId);
            }
        }

        public void Update(Model.OpticsTest opticsTest)
        {
            Validate(opticsTest);
            opticsTest.UpdateTime = DateTime.Now;
            accessor.Update(opticsTest);
        }

        private void TiGuiExists(Model.OpticsTest model)
        {
            if (this.ExistsPrimary(model.OpticsTestId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.InsertTime.Value.Year, model.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.OpticsTestId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.OpticsTest opticsTest)
        {
            if (string.IsNullOrEmpty(opticsTest.OpticsTestId))
                throw new Helper.RequireValueException(Model.OpticsTest.PRO_OpticsTestId);
            if (string.IsNullOrEmpty(opticsTest.OptiscTestDate.Value.ToString()))
                throw new Helper.RequireValueException(Model.OpticsTest.PRO_OptiscTestDate);
            if (string.IsNullOrEmpty(opticsTest.EmployeeId))
                throw new Helper.RequireValueException(Model.OpticsTest.PRO_EmployeeId);
            if (string.IsNullOrEmpty(opticsTest.MachineName))
                throw new Helper.RequireValueException(Model.OpticsTest.PRO_MachineName);
            //if (string.IsNullOrEmpty(opticsTest.ManualId))
            //    throw new Helper.RequireValueException(Model.OpticsTest.PRO_ManualId);
            //if (accessor.ExistsManualId(opticsTest.OpticsTestId, opticsTest.ManualId))
            //    throw new Helper.InvalidValueException(Model.OpticsTest.PRO_ManualId);
        }

        protected override string GetSettingId()
        {
            return "OpticsTestRule";
        }

        protected override string GetInvoiceKind()
        {
            return "OpticsTest";
        }

        public IList<Model.OpticsTest> SelectByDateRage(DateTime startdate, DateTime enddate, string PCPGOnlineCheckDetailId)
        {
            return accessor.SelectByDateRage(startdate, enddate, PCPGOnlineCheckDetailId);
        }

        #region 分类构建

        public Model.OpticsTest mGetFirst(string PCPGOnlineCheckDetailId)
        {
            return accessor.mGetFirst(PCPGOnlineCheckDetailId);
        }

        public Model.OpticsTest mGetLast(string PCPGOnlineCheckDetailId)
        {
            return accessor.mGetLast(PCPGOnlineCheckDetailId);
        }

        public Model.OpticsTest mGetPrev(DateTime InsertDate, string PCPGOnlineCheckDetailId)
        {
            return accessor.mGetPrev(InsertDate, PCPGOnlineCheckDetailId);
        }

        public Model.OpticsTest mGetNext(DateTime InsertDate, string PCPGOnlineCheckDetailId)
        {
            return accessor.mGetNext(InsertDate, PCPGOnlineCheckDetailId);
        }

        public bool mHasRows(string PCPGOnlineCheckDetailId)
        {
            return accessor.mHasRows(PCPGOnlineCheckDetailId);
        }

        public bool mHasRowsBefore(Model.OpticsTest e, string PCPGOnlineCheckDetailId)
        {
            return accessor.mHasRowsBefore(e, PCPGOnlineCheckDetailId);
        }

        public bool mHasRowsAfter(Model.OpticsTest e, string PCPGOnlineCheckDetailId)
        {
            return accessor.mHasRowsAfter(e, PCPGOnlineCheckDetailId);
        }

        public IList<Model.OpticsTest> mSelect(string PCPGOnlineCheckDetailId)
        {
            return accessor.mSelect(PCPGOnlineCheckDetailId);
        }

        #endregion



        public Model.OpticsTest FGetFirst(string PCFinishCheckId)
        {
            return accessor.FGetFirst(PCFinishCheckId);
        }

        public Model.OpticsTest FGetLast(string PCFinishCheckId)
        {
            return accessor.FGetLast(PCFinishCheckId);
        }

        public Model.OpticsTest FGetPrev(DateTime InsertDate, string PCFinishCheckId)
        {
            return accessor.FGetPrev(InsertDate, PCFinishCheckId);
        }

        public Model.OpticsTest FGetNext(DateTime InsertDate, string PCFinishCheckId)
        {
            return accessor.FGetNext(InsertDate, PCFinishCheckId);
        }

        public bool FHasRows(string PCFinishCheckId)
        {
            return accessor.FHasRows(PCFinishCheckId);
        }

        public bool FHasRowsBefore(Model.OpticsTest e, string PCFinishCheckId)
        {
            return accessor.FHasRowsBefore(e, PCFinishCheckId);
        }

        public bool FHasRowsAfter(Model.OpticsTest e, string PCFinishCheckId)
        {
            return accessor.FHasRowsAfter(e, PCFinishCheckId);
        }

        public IList<Model.OpticsTest> FSelect(string PCFinishCheckId)
        {
            return accessor.FSelect(PCFinishCheckId);
        }

        public IList<Book.Model.OpticsTest> FSelectByDateRage(DateTime startdate, DateTime enddate, string PCFinishCheckId)
        {
            return accessor.FSelectByDateRage(startdate, enddate, PCFinishCheckId);
        }
    }
}

