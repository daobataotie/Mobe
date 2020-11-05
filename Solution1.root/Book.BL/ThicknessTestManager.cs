//------------------------------------------------------------------------------
//
// file name：ThicknessTestManager.cs
// author: mayanjun
// create date：2012-4-24 10:33:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ThicknessTest.
    /// </summary>
    public partial class ThicknessTestManager : BaseManager
    {
        BL.ThicknessTestDetailsManager detailManager = new ThicknessTestDetailsManager();

        public void DeleteContainTrans(string thicknessTestId)
        {
            try
            {
                BL.V.BeginTransaction();

                this.Delete(thicknessTestId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(string thicknessTestId)
        {
            detailManager.DeleteByheaderId(thicknessTestId);

            accessor.Delete(thicknessTestId);
        }

        public void Insert(Model.ThicknessTest thicknessTest)
        {
            Validate(thicknessTest);
            try
            {
                BL.V.BeginTransaction();

                thicknessTest.InsertTime = DateTime.Now;
                thicknessTest.UpdateTime = DateTime.Now;
                TiGuiExists(thicknessTest);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, thicknessTest.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, thicknessTest.InsertTime.Value.Year, thicknessTest.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, thicknessTest.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(thicknessTest);

                foreach (Model.ThicknessTestDetails d in thicknessTest.Details)
                {
                    detailManager.Insert(d);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.ThicknessTest thicknessTest)
        {
            Validate(thicknessTest);
            try
            {
                BL.V.BeginTransaction();

                detailManager.DeleteByheaderId(thicknessTest.ThicknessTestId);

                foreach (Model.ThicknessTestDetails d in thicknessTest.Details)
                {
                    d.ThicknessTestId = thicknessTest.ThicknessTestId;
                    detailManager.Insert(d);
                }

                thicknessTest.UpdateTime = DateTime.Now;
                accessor.Update(thicknessTest);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public Model.ThicknessTest GetDetail(string thicknessTestId)
        {
            Model.ThicknessTest thicknessTest = accessor.Get(thicknessTestId);
            thicknessTest.Details = detailManager.SelectByHeaderId(thicknessTestId);
            return thicknessTest;
        }

        private void TiGuiExists(Model.ThicknessTest model)
        {
            if (this.ExistsPrimary(model.ThicknessTestId))
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
                model.ThicknessTestId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.ThicknessTest thicknessTest)
        {
            if (string.IsNullOrEmpty(thicknessTest.ThicknessTestId))
                throw new Helper.RequireValueException(Model.ThicknessTest.PRO_ThicknessTestId);
            if (string.IsNullOrEmpty(thicknessTest.ThicknessTestDate.Value.ToString()))
                throw new Helper.RequireValueException(Model.ThicknessTest.PRO_ThicknessTestDate);
            if (string.IsNullOrEmpty(thicknessTest.EmployeeId))
                throw new Helper.RequireValueException(Model.ThicknessTest.PRO_EmployeeId);
            //if (string.IsNullOrEmpty(thicknessTest.manualId))
            //    throw new Helper.RequireValueException(Model.ThicknessTest.PRO_manualId);
            if (accessor.ExistsManualId(thicknessTest.ThicknessTestId, thicknessTest.manualId))
                throw new Helper.InvalidValueException(Model.ThicknessTest.PRO_manualId);
        }

        protected override string GetSettingId()
        {
            return "ThicknessTestRule";
        }

        protected override string GetInvoiceKind()
        {
            return "ThicknessTest";
        }

        public IList<Model.ThicknessTest> SelectByDateRage(DateTime startdate, DateTime enddate, string PCPGOnlineCheckDetailId)
        {
            return accessor.SelectByDateRage(startdate, enddate, PCPGOnlineCheckDetailId);
        }

        public void DeleteByPCPGOnlineCheckDetailId(string PCPGOnlineCheckDetailId)
        {
            IList<Model.ThicknessTest> Dellist = this.SelectByDateRage(global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, PCPGOnlineCheckDetailId);
            foreach (Model.ThicknessTest d in Dellist)
            {
                this.Delete(d.ThicknessTestId);
            }
        }

        #region 分类构建
        public Model.ThicknessTest mGetFirst(string PCPGOnlineCheckDetailId)
        {
            return accessor.mGetFirst(PCPGOnlineCheckDetailId);
        }

        public Model.ThicknessTest mGetLast(string PCPGOnlineCheckDetailId)
        {
            return accessor.mGetLast(PCPGOnlineCheckDetailId);
        }

        public Model.ThicknessTest mGetPrev(DateTime InsertDate, string PCPGOnlineCheckDetailId)
        {
            return accessor.mGetPrev(InsertDate, PCPGOnlineCheckDetailId);
        }

        public Model.ThicknessTest mGetNext(DateTime InsertDate, string PCPGOnlineCheckDetailId)
        {
            return accessor.mGetNext(InsertDate, PCPGOnlineCheckDetailId);
        }

        public bool mHasRows(string PCPGOnlineCheckDetailId)
        {
            return accessor.mHasRows(PCPGOnlineCheckDetailId);
        }

        public bool mHasRowsBefore(Model.ThicknessTest e, string PCPGOnlineCheckDetailId)
        {
            return accessor.mHasRowsBefore(e, PCPGOnlineCheckDetailId);
        }

        public bool mHasRowsAfter(Model.ThicknessTest e, string PCPGOnlineCheckDetailId)
        {
            return accessor.mHasRowsAfter(e, PCPGOnlineCheckDetailId);
        }

        public IList<Model.ThicknessTest> mSelect(string PCPGOnlineCheckDetailId)
        {
            return accessor.mSelect(PCPGOnlineCheckDetailId);
        }
        #endregion



        #region 适用于首件上线检查表
        public Model.ThicknessTest PFCGetFirst(string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCGetFirst(PCFirstOnlineCheckDetailId);
        }

        public Model.ThicknessTest PFCGetLast(string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCGetLast(PCFirstOnlineCheckDetailId);
        }

        public Model.ThicknessTest PFCGetPrev(DateTime InsertDate, string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCGetPrev(InsertDate, PCFirstOnlineCheckDetailId);
        }

        public Model.ThicknessTest PFCGetNext(DateTime InsertDate, string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCGetNext(InsertDate, PCFirstOnlineCheckDetailId);
        }

        public bool PFCHasRows(string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCHasRows(PCFirstOnlineCheckDetailId);
        }

        public bool PFCHasRowsBefore(Model.ThicknessTest e, string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCHasRowsBefore(e, PCFirstOnlineCheckDetailId);
        }

        public bool PFCHasRowsAfter(Model.ThicknessTest e, string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCHasRowsAfter(e, PCFirstOnlineCheckDetailId);
        }

        public IList<Model.ThicknessTest> PFCSelect(string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCSelect(PCFirstOnlineCheckDetailId);
        }

        public IList<Model.ThicknessTest> PFCSelectByDateRage(DateTime startdate, DateTime enddate, string PCFirstOnlineCheckDetailId)
        {
            return accessor.PFCSelectByDateRage(startdate, enddate, PCFirstOnlineCheckDetailId);
        }
        #endregion
    }
}

