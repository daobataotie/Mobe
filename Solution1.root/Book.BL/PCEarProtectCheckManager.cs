//------------------------------------------------------------------------------
//
// file name：PCEarProtectCheckManager.cs
// author: mayanjun
// create date：2013-09-03 15:16:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarProtectCheck.
    /// </summary>
    public partial class PCEarProtectCheckManager : BaseManager
    {
        private static readonly DA.IPCEarProtectCheckDetailAccessor Detailaccessor = (DA.IPCEarProtectCheckDetailAccessor)Accessors.Get("PCEarProtectCheckDetailAccessor");
        /// <summary>
        /// Delete PCEarProtectCheck by primary key.
        /// </summary>
        public void Delete(string pCEarProtectCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                Detailaccessor.DeleteByPCEarProtectCheckId(pCEarProtectCheckId);
                accessor.Delete(pCEarProtectCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.PCEarProtectCheck PCEarProtectCheck)
        {
            if (PCEarProtectCheck != null)
                this.Delete(PCEarProtectCheck.PCEarProtectCheckId);
        }

        /// <summary>
        /// Insert a PCEarProtectCheck.
        /// </summary>
        public void Insert(Model.PCEarProtectCheck pCEarProtectCheck)
        {
            //
            // todo:add other logic here
            //
            Validate(pCEarProtectCheck);
            try
            {
                BL.V.BeginTransaction();
                pCEarProtectCheck.InsertTime = DateTime.Now;
                pCEarProtectCheck.UpdateTime = DateTime.Now;

                TiGuiExists(pCEarProtectCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCEarProtectCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCEarProtectCheck.InsertTime.Value.Year, pCEarProtectCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCEarProtectCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCEarProtectCheck);
                foreach (var item in pCEarProtectCheck.Details)
                {
                    Detailaccessor.Insert(item);
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
        /// Update a PCEarProtectCheck.
        /// </summary>
        public void Update(Model.PCEarProtectCheck pCEarProtectCheck)
        {
            //
            // todo: add other logic here.
            //
            Validate(pCEarProtectCheck);
            try
            {
                BL.V.BeginTransaction();
                pCEarProtectCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCEarProtectCheck);
                Detailaccessor.DeleteByPCEarProtectCheckId(pCEarProtectCheck.PCEarProtectCheckId);
                foreach (var item in pCEarProtectCheck.Details)
                {
                    Detailaccessor.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        protected override string GetInvoiceKind()
        {
            return "PCE";
        }

        protected override string GetSettingId()
        {
            return "PCEarProtectCheckRule";
        }

        public void TiGuiExists(Model.PCEarProtectCheck model)
        {
            if (this.ExistsPrimary(model.PCEarProtectCheckId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.PCEarProtectCheckId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }
        }

        private void Validate(Model.PCEarProtectCheck PCEarProtectCheck)
        {
            if (PCEarProtectCheck.CheckDate == null)
                throw new Helper.RequireValueException(Model.PCClarityCheck.PRO_CheckDate);
            if (PCEarProtectCheck.ProductId == null)
                throw new Helper.RequireValueException(Model.PCEarProtectCheck.PRO_ProductId);
            if (PCEarProtectCheck.EmployeeId == null)
                throw new Helper.RequireValueException(Model.PCEarProtectCheck.PRO_EmployeeId);
        }

        /// <summary>
        /// 查询该单据及其详细
        /// </summary>
        /// <param name="pCClarityCheck"></param>
        /// <returns></returns>
        public Book.Model.PCEarProtectCheck Get(Book.Model.PCEarProtectCheck PCEarProtectCheck)
        {
            Model.PCEarProtectCheck model = this.Get(PCEarProtectCheck.PCEarProtectCheckId);
            if (model != null)
                model.Details = new BL.PCEarProtectCheckDetailManager().SelectByPCEarProtectCheckId(PCEarProtectCheck.PCEarProtectCheckId);
            return model;
        }

        public IList<Model.PCEarProtectCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, bool IsReport)
        {
            return accessor.SelectByDateRage(StartDate, EndDate, IsReport);
        }

        public bool mHasRows(bool IsReport)
        {
            return accessor.mHasRows(IsReport);
        }

        public bool mHasRowsBefore(Model.PCEarProtectCheck e)
        {
            return accessor.mHasRowsBefore(e);
        }

        public bool mHasRowsAfter(Model.PCEarProtectCheck e)
        {
            return accessor.mHasRowsAfter(e);
        }

        public Model.PCEarProtectCheck mGetFirst(bool IsReport)
        {
            return accessor.mGetFirst(IsReport);
        }

        public Model.PCEarProtectCheck mGetLast(bool IsReport)
        {
            return accessor.mGetLast(IsReport);
        }

        public Model.PCEarProtectCheck mGetNext(Model.PCEarProtectCheck e)
        {
            return accessor.mGetNext(e);
        }

        public Model.PCEarProtectCheck mGetPrev(Model.PCEarProtectCheck e)
        {
            return accessor.mGetPrev(e);
        }
    }
}

