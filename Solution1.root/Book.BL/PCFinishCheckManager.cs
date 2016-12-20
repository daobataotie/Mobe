//------------------------------------------------------------------------------
//
// file name：PCFinishCheckManager.cs
// author: mayanjun
// create date：2011-11-12 15:10:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCFinishCheck.
    /// </summary>
    public partial class PCFinishCheckManager : BaseManager
    {

        /// <summary>
        /// Delete PCFinishCheck by primary key.
        /// </summary>
        public void Delete(string pCFinishCheckID)
        {
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(pCFinishCheckID);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a PCFinishCheck.
        /// </summary>
        public void Insert(Model.PCFinishCheck pCFinishCheck)
        {
            Validate(pCFinishCheck);
            try
            {
                BL.V.BeginTransaction();

                TiGuiExists(pCFinishCheck);

                pCFinishCheck.InsertTime = DateTime.Now;
                pCFinishCheck.UpdateTime = DateTime.Now;

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCFinishCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCFinishCheck.InsertTime.Value.Year, pCFinishCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCFinishCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCFinishCheck);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        /// <summary>
        /// Update a PCFinishCheck.
        /// </summary>
        public void Update(Model.PCFinishCheck pCFinishCheck)
        {
            Validate(pCFinishCheck);
            if (pCFinishCheck != null)
            {
                pCFinishCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCFinishCheck);
                //this.Delete(pCFinishCheck.PCFinishCheckID);
                //pCFinishCheck.UpdateTime = DateTime.Now;
                //this.Insert(pCFinishCheck);
            }
        }

        public IList<Model.PCFinishCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId)
        {
            return accessor.SelectByDateRage(startdate, enddate, product, customer, CusXOId);
        }

        protected override string GetSettingId()
        {
            return "pcfcRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcfc";
        }
        private void Validate(Model.PCFinishCheck _pcoc)
        {
            if (string.IsNullOrEmpty(_pcoc.PCFinishCheckID))
                throw new Helper.RequireValueException(Model.PCFinishCheck.PRO_PCFinishCheckID);
            if (string.IsNullOrEmpty(_pcoc.PCFinishCheckDate.ToString()))
                throw new Helper.RequireValueException(Model.PCFinishCheck.PRO_PCFinishCheckDate);
            if (string.IsNullOrEmpty(_pcoc.ProductId))
                throw new Helper.RequireValueException(Model.PCFinishCheck.PRO_ProductId);
            if (string.IsNullOrEmpty(_pcoc.Employee0Id))
                throw new Helper.RequireValueException(Model.PCFinishCheck.PRO_Employee0Id);
        }

        private void TiGuiExists(Model.PCFinishCheck model)
        {
            if (this.ExistsPrimary(model.PCFinishCheckID))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, model.PCFinishCheckDate.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, model.PCFinishCheckDate.Value.Year, model.PCFinishCheckDate.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, model.PCFinishCheckDate.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.PCFinishCheckID = this.GetId(model.PCFinishCheckDate.Value);
                TiGuiExists(model);             
            }
        }
    }
}

