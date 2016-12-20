//------------------------------------------------------------------------------
//
// file name：PCFogCheckManager.cs
// author: mayanjun
// create date：2012-3-16 17:42:23
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCFogCheck.
    /// </summary>
    public partial class PCFogCheckManager : BaseManager
    {
        public void Delete(string pCFogCheckId)
        {
            accessor.Delete(pCFogCheckId);
        }

        public void Delete(Model.PCFogCheck pcfc)
        {
            try
            {
                BL.V.BeginTransaction();

                new BL.PCFogCheckDetailManager().DeleteByHeaderId(pcfc.PCFogCheckId);

                this.Delete(pcfc.PCFogCheckId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Insert(Model.PCFogCheck pCFogCheck)
        {
            Validate(pCFogCheck);
            try
            {
                BL.V.BeginTransaction();

                pCFogCheck.InsertTime = DateTime.Now;
                pCFogCheck.UpdateTime = DateTime.Now;
                TiGuiExists(pCFogCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCFogCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCFogCheck.InsertTime.Value.Year, pCFogCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCFogCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCFogCheck);

                foreach (Model.PCFogCheckDetail d in pCFogCheck.Details)
                {
                    d.PCFogCheckId = pCFogCheck.PCFogCheckId;
                    new PCFogCheckDetailManager().Insert(d);
                }

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.PCFogCheck pCFogCheck)
        {
            if (pCFogCheck != null)
            {
                Validate(pCFogCheck);

                pCFogCheck.UpdateTime = DateTime.Now;

                accessor.Update(pCFogCheck);

                new PCFogCheckDetailManager().DeleteByHeaderId(pCFogCheck.PCFogCheckId);

                foreach (Model.PCFogCheckDetail d in pCFogCheck.Details)
                {
                    new PCFogCheckDetailManager().Insert(d);
                }
            }
        }

        public Model.PCFogCheck GetDetail(string PCFogCheckId)
        {
            Model.PCFogCheck p = accessor.Get(PCFogCheckId);
            p.Details = new BL.PCFogCheckDetailManager().Select(PCFogCheckId);
            return p;
        }

        public IList<Model.PCFogCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId)
        {
            return accessor.SelectByDateRage(startdate, enddate, product, customer, CusXOId);
        }

        protected override string GetSettingId()
        {
            return "pcFogRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcFog";
        }

        private void TiGuiExists(Model.PCFogCheck model)
        {
            if (this.ExistsPrimary(model.PCFogCheckId))
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
                model.PCFogCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.PCFogCheck mPCFog)
        {
            if (string.IsNullOrEmpty(mPCFog.PCFogCheckId))
                throw new Helper.RequireValueException(Model.PCFogCheck.PRO_PCFogCheckId);
            if (string.IsNullOrEmpty(mPCFog.ProductId))
                throw new Helper.RequireValueException(Model.PCFogCheck.PRO_ProductId);
            if (string.IsNullOrEmpty(mPCFog.PCFogCheckDate.ToString()))
                throw new Helper.RequireValueException(Model.PCFogCheck.PRO_PCFogCheckDate);
            if (string.IsNullOrEmpty(mPCFog.EmployeeId))
                throw new Helper.RequireValueException(Model.PCFogCheck.PRO_EmployeeId);
        }

        public IList<Model.PCFogCheck> SelectByDate(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDate(startdate, enddate);
        }
    }
}

