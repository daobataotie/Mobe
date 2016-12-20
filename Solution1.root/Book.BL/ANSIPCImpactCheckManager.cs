//------------------------------------------------------------------------------
//
// file name：ANSIPCImpactCheckManager.cs
// author: mayanjun
// create date：2011-11-23 09:49:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ANSIPCImpactCheck.
    /// </summary>
    public partial class ANSIPCImpactCheckManager : BaseManager
    {
        private static readonly DA.IANSIPCImpactCheckDetailAccessor ANSIPCImpactCheckDetailAccessor = (DA.IANSIPCImpactCheckDetailAccessor)Accessors.Get("ANSIPCImpactCheckDetailAccessor");
        private static readonly ANSIPCImpactCheckDetailManager ANSIpcicDetailManager = new ANSIPCImpactCheckDetailManager();
        /// <summary>
        /// Delete ANSIPCImpactCheck by primary key.
        /// </summary>
        public void Delete(string aNSIPCImpactCheckID)
        {
            accessor.Delete(aNSIPCImpactCheckID);
        }

        public void Delete(Model.ANSIPCImpactCheck ansipcic)
        {
            try
            {
                BL.V.BeginTransaction();

                ANSIPCImpactCheckDetailAccessor.DeleteByANSIPCICId(ansipcic.ANSIPCImpactCheckID);       //首先删除详细

                this.Delete(ansipcic.ANSIPCImpactCheckID);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a ANSIPCImpactCheck.
        /// </summary>
        public void Insert(Model.ANSIPCImpactCheck aNSIPCImpactCheck)
        {
            Validate(aNSIPCImpactCheck);
            try
            {
                BL.V.BeginTransaction();
                aNSIPCImpactCheck.InsertTime = DateTime.Now;
                aNSIPCImpactCheck.UpdateTime = DateTime.Now;
                TiGuiExists(aNSIPCImpactCheck);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, aNSIPCImpactCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, aNSIPCImpactCheck.InsertTime.Value.Year, aNSIPCImpactCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, aNSIPCImpactCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(aNSIPCImpactCheck);

                foreach (Model.ANSIPCImpactCheckDetail detail in aNSIPCImpactCheck.Details)
                {
                    ANSIpcicDetailManager.Insert(detail);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        /// <summary>
        /// Update a ANSIPCImpactCheck.
        /// </summary>
        public void Update(Model.ANSIPCImpactCheck aNSIPCImpactCheck)
        {
            if (aNSIPCImpactCheck != null)
            {
                Validate(aNSIPCImpactCheck);
                //修改头
                aNSIPCImpactCheck.UpdateTime = DateTime.Now;
                accessor.Update(aNSIPCImpactCheck);
                //删除详细
                ANSIPCImpactCheckDetailAccessor.DeleteByANSIPCICId(aNSIPCImpactCheck.ANSIPCImpactCheckID);
                //添加详细
                foreach (Model.ANSIPCImpactCheckDetail detail in aNSIPCImpactCheck.Details)
                {
                    detail.ANSIPCImpactCheckID = aNSIPCImpactCheck.ANSIPCImpactCheckID;
                    ANSIPCImpactCheckDetailAccessor.Insert(detail);
                }
            }
        }

        public Model.ANSIPCImpactCheck GetDetail(Model.ANSIPCImpactCheck ANSIPCIC)
        {
            ANSIPCIC.Details = ANSIPCImpactCheckDetailAccessor.Select(ANSIPCIC.ANSIPCImpactCheckID);
            return ANSIPCIC;
        }

        protected override string GetSettingId()
        {
            return "ANSIpcicRule";
        }

        protected override string GetInvoiceKind()
        {
            return "ANSIpcic";
        }

        //数据验证
        private void Validate(Model.ANSIPCImpactCheck ANSIpcic)
        {
            if (string.IsNullOrEmpty(ANSIpcic.ANSIPCImpactCheckID))
                throw new Helper.RequireValueException(Model.ANSIPCImpactCheck.PRO_ANSIPCImpactCheckID);
            if (string.IsNullOrEmpty(ANSIpcic.ANSIPCImpactCheckDate.ToString()))
                throw new Helper.RequireValueException(Model.ANSIPCImpactCheck.PRO_ANSIPCImpactCheckDate);
            if (string.IsNullOrEmpty(ANSIpcic.ProductId))
                throw new Helper.RequireValueException(Model.ANSIPCImpactCheck.PRO_ProductId);
            if (string.IsNullOrEmpty(ANSIpcic.EmployeeId))
                throw new Helper.RequireValueException(Model.ANSIPCImpactCheck.PRO_EmployeeId);
        }

        public IList<Model.ANSIPCImpactCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string CusXOId, string ForANSIOrJIS)
        {
            return accessor.SelectByDateRage(StartDate, EndDate, product, customer, CusXOId,ForANSIOrJIS);
        }

        private void TiGuiExists(Model.ANSIPCImpactCheck model)
        {
            if (this.ExistsPrimary(model.ANSIPCImpactCheckID))
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
                model.ANSIPCImpactCheckID = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.ANSIPCImpactCheck GetLastByForANSIOrJIS(string ForANSIOrJIS)
        {
            return accessor.GetLastByForANSIOrJIS(ForANSIOrJIS);
        }

        public Model.ANSIPCImpactCheck GetFirstByForANSIOrJIS(string ForANSIOrJIS)
        {
            return accessor.GetFirstByForANSIOrJIS(ForANSIOrJIS);
        }

        public bool HasRowsByForANSIOrJIS(string ForANSIOrJIS)
        {
            return accessor.HasRowsByForANSIOrJIS(ForANSIOrJIS);
        }

        public bool HasRowsBeforeByForANSIOrJIS(Model.ANSIPCImpactCheck e)
        {
            return accessor.HasRowsBeforeByForANSIOrJIS(e);
        }

        public bool HasRowsAfterByForANSIOrJIS(Model.ANSIPCImpactCheck e)
        {
            return accessor.HasRowsAfterByForANSIOrJIS(e);
        }

        public Model.ANSIPCImpactCheck GetNextByForANSIOrJIS(Model.ANSIPCImpactCheck e)
        {
            return accessor.GetNextByForANSIOrJIS(e);
        }

        public Model.ANSIPCImpactCheck GetPrevByForANSIOrJIS(Model.ANSIPCImpactCheck e)
        {
            return accessor.GetPrevByForANSIOrJIS(e);
        }
    }
}

