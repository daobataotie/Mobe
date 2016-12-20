//------------------------------------------------------------------------------
//
// file name：PCOpticsCheckManager.cs
// author: mayanjun
// create date：2012-3-16 17:41:45
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCOpticsCheck.
    /// </summary>
    public partial class PCOpticsCheckManager : BaseManager
    {
        public void Delete(string pCFogCheckId)
        {
            try
            {
                BL.V.BeginTransaction();

                accessor.Delete(pCFogCheckId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Insert(Model.PCOpticsCheck pCOpticsCheck)
        {
            Validate(pCOpticsCheck);
            try
            {
                BL.V.BeginTransaction();

                pCOpticsCheck.InsertTime = DateTime.Now;
                pCOpticsCheck.UpdateTime = DateTime.Now;
                TiGuiExists(pCOpticsCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCOpticsCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCOpticsCheck.InsertTime.Value.Year, pCOpticsCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCOpticsCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCOpticsCheck);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.PCOpticsCheck pCOpticsCheck)
        {
            if (pCOpticsCheck != null)
            {
                Validate(pCOpticsCheck);
                pCOpticsCheck.UpdateTime = DateTime.Now;

                accessor.Update(pCOpticsCheck);
            }
        }

        protected override string GetSettingId()
        {
            return "pcOpticsRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcOptics";
        }

        private void TiGuiExists(Model.PCOpticsCheck model)
        {
            if (this.ExistsPrimary(model.PCOpticsCheckId))
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
                model.PCOpticsCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.PCOpticsCheck mPCOptics)
        {
            if (string.IsNullOrEmpty(mPCOptics.PCOpticsCheckId))
                throw new Helper.RequireValueException(Model.PCOpticsCheck.PRO_PCOpticsCheckId);
            if (string.IsNullOrEmpty(mPCOptics.ProductId))
                throw new Helper.RequireValueException(Model.PCOpticsCheck.PRO_ProductId);
            if (string.IsNullOrEmpty(mPCOptics.PCOpticsCheckDate.ToString()))
                throw new Helper.RequireValueException(Model.PCOpticsCheck.PRO_PCOpticsCheckDate);
            if (string.IsNullOrEmpty(mPCOptics.EmployeeId))
                throw new Helper.RequireValueException(Model.PCOpticsCheck.PRO_EmployeeId);
        }

        public IList<Model.PCOpticsCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId)
        {
            return accessor.SelectByDateRage(startdate, enddate, product, customer, CusXOId);
        }
    }
}

