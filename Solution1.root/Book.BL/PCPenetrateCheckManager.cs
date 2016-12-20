//------------------------------------------------------------------------------
//
// file name：PCPenetrateCheckManager.cs
// author: mayanjun
// create date：2012-3-21 11:02:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCPenetrateCheck.
    /// </summary>
    public partial class PCPenetrateCheckManager : BaseManager
    {
        public void Delete(string pCPenetrateCheckId)
        {
            try
            {
                BL.V.BeginTransaction();

                accessor.Delete(pCPenetrateCheckId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }

        }

        public void Insert(Model.PCPenetrateCheck pCPenetrateCheck)
        {
            Validate(pCPenetrateCheck);
            try
            {
                BL.V.BeginTransaction();

                pCPenetrateCheck.InsertTime = DateTime.Now;
                pCPenetrateCheck.UpdateTime = DateTime.Now;
                TiGuiExists(pCPenetrateCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCPenetrateCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCPenetrateCheck.InsertTime.Value.Year, pCPenetrateCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCPenetrateCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCPenetrateCheck);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Update(Model.PCPenetrateCheck pCPenetrateCheck)
        {
            if (pCPenetrateCheck != null)
            {
                Validate(pCPenetrateCheck);
                pCPenetrateCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCPenetrateCheck);
            }
        }

        protected override string GetSettingId()
        {
            return "pcPenetrateCheckRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcPenetrateCheck";
        }

        private void TiGuiExists(Model.PCPenetrateCheck model)
        {
            if (this.ExistsPrimary(model.PCPenetrateCheckId))
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
                model.PCPenetrateCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.PCPenetrateCheck mPCPenetraeCheck)
        {
            if (string.IsNullOrEmpty(mPCPenetraeCheck.PCPenetrateCheckId))
                throw new Helper.RequireValueException(Model.PCPenetrateCheck.PRO_PCPenetrateCheckId);
            if (string.IsNullOrEmpty(mPCPenetraeCheck.ProductId))
                throw new Helper.RequireValueException(Model.PCPenetrateCheck.PRO_ProductId);
            if (string.IsNullOrEmpty(mPCPenetraeCheck.PCPenetrateCheckDate.ToString()))
                throw new Helper.RequireValueException(Model.PCPenetrateCheck.PRO_PCPenetrateCheckDate);
            if (string.IsNullOrEmpty(mPCPenetraeCheck.EmployeeId))
                throw new Helper.RequireValueException(Model.PCPenetrateCheck.PRO_EmployeeId);
        }

        public IList<Model.PCPenetrateCheck> SelectByDateRage(DateTime startdate, DateTime enddate, Model.Product product, Model.Customer customer, string CusXOId)
        {
            return accessor.SelectByDateRage(startdate, enddate, product, customer, CusXOId);
        }
    }
}

