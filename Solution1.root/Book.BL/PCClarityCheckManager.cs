//------------------------------------------------------------------------------
//
// file name：PCClarityCheckManager.cs
// author: mayanjun
// create date：2013-08-19 15:44:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCClarityCheck.
    /// </summary>
    public partial class PCClarityCheckManager : BaseManager
    {
        BL.PCClarityCheckDetailManager detailManager = new PCClarityCheckDetailManager();
        /// <summary>
        /// Delete PCClarityCheck by primary key.
        /// </summary>
        public void Delete(string pCClarityCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.detailManager.DeleteByPCClarityCheckID(pCClarityCheckId);
                accessor.Delete(pCClarityCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.PCClarityCheck PCClarityCheck)
        {
            if (PCClarityCheck != null)
                this.Delete(PCClarityCheck.PCClarityCheckId);
        }

        /// <summary>
        /// Insert a PCClarityCheck.
        /// </summary>
        public void Insert(Model.PCClarityCheck pCClarityCheck)
        {
            //
            // todo:add other logic here
            //
            Validate(pCClarityCheck);
            try
            {
                BL.V.BeginTransaction();
                pCClarityCheck.InsertTime = DateTime.Now;
                pCClarityCheck.UpdateTime = DateTime.Now;
                accessor.Insert(pCClarityCheck);

                foreach (Model.PCClarityCheckDetail model in pCClarityCheck.Details)
                {
                    detailManager.Insert(model);
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
        /// Update a PCClarityCheck.
        /// </summary>
        public void Update(Model.PCClarityCheck pCClarityCheck)
        {
            //
            // todo: add other logic here.
            //
            Validate(pCClarityCheck);
            try
            {
                BL.V.BeginTransaction();
                pCClarityCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCClarityCheck);

                this.detailManager.DeleteByPCClarityCheckID(pCClarityCheck.PCClarityCheckId);
                foreach (Model.PCClarityCheckDetail model in pCClarityCheck.Details)
                {
                    detailManager.Insert(model);
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
            return "PCC";
        }

        protected override string GetSettingId()
        {
            return "PCClarityCheckRule";
        }

        public void TiGuiExists(Model.PCClarityCheck model)
        {
            if (this.ExistsPrimary(model.PCClarityCheckId))
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
                model.PCClarityCheckId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }
        }

        private void Validate(Model.PCClarityCheck pCClarityCheck)
        {
            if (pCClarityCheck.ProductId == null)
                throw new Helper.RequireValueException(Model.PCClarityCheck.PRO_ProductId);
            if (pCClarityCheck.EmployeeId == null)
                throw new Helper.RequireValueException(Model.PCClarityCheck.PRO_EmployeeId);
        }

        /// <summary>
        /// 查询该单据及其详细
        /// </summary>
        /// <param name="pCClarityCheck"></param>
        /// <returns></returns>
        public Book.Model.PCClarityCheck Get(Book.Model.PCClarityCheck pCClarityCheck)
        {
            Model.PCClarityCheck model = this.Get(pCClarityCheck.PCClarityCheckId);
            if (model != null)
                model.Details = new BL.PCClarityCheckDetailManager().SelectByPCClarityCheckId(pCClarityCheck.PCClarityCheckId);
            return model;
        }

        public IList<Model.PCClarityCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate)
        {
            return accessor.SelectByDateRage(StartDate, EndDate);
        }
    }
}

