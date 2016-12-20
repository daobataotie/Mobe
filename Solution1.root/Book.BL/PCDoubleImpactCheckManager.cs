//------------------------------------------------------------------------------
//
// file name：PCDoubleImpactCheckManager.cs
// author: mayanjun
// create date：2011-11-24 17:38:15
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCDoubleImpactCheck.
    /// </summary>
    public partial class PCDoubleImpactCheckManager : BaseManager
    {
        private static readonly DA.IPCDoubleImpactCheckDetailAccessor PCDICDAccessor = (DA.IPCDoubleImpactCheckDetailAccessor)Accessors.Get("PCDoubleImpactCheckDetailAccessor");

        /// <summary>
        /// Delete PCDoubleImpactCheck by primary key.
        /// </summary>
        public void Delete(string pCDoubleImpactCheckID)
        {
            accessor.Delete(pCDoubleImpactCheckID);
        }

        public void Delete(Model.PCDoubleImpactCheck pcdic)
        {

            try
            {
                BL.V.BeginTransaction();

                PCDICDAccessor.DeleteByPCDoubleImpactCheckId(pcdic.PCDoubleImpactCheckID);      //产出详细

                this.Delete(pcdic.PCDoubleImpactCheckID);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a PCDoubleImpactCheck.
        /// </summary>
        public void Insert(Model.PCDoubleImpactCheck pCDoubleImpactCheck)
        {
            Validate(pCDoubleImpactCheck);
            try
            {
                BL.V.BeginTransaction();
                pCDoubleImpactCheck.InsertTime = DateTime.Now;
                pCDoubleImpactCheck.UpdateTime = DateTime.Now;
                TiGuiExists(pCDoubleImpactCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCDoubleImpactCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCDoubleImpactCheck.InsertTime.Value.Year, pCDoubleImpactCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCDoubleImpactCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCDoubleImpactCheck);

                foreach (Model.PCDoubleImpactCheckDetail detail in pCDoubleImpactCheck.Detail)
                {
                    detail.PCDoubleImpactCheckID = pCDoubleImpactCheck.PCDoubleImpactCheckID;
                    PCDICDAccessor.Insert(detail);
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
        /// Update a PCDoubleImpactCheck.
        /// </summary>
        public void Update(Model.PCDoubleImpactCheck pCDoubleImpactCheck)
        {
            if (pCDoubleImpactCheck != null)
            {
                Validate(pCDoubleImpactCheck);
                //修改头信息
                pCDoubleImpactCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCDoubleImpactCheck);
                //删除详细
                PCDICDAccessor.DeleteByPCDoubleImpactCheckId(pCDoubleImpactCheck.PCDoubleImpactCheckID);
                //添加详细
                foreach (Model.PCDoubleImpactCheckDetail detail in pCDoubleImpactCheck.Detail)
                {
                    detail.PCDoubleImpactCheckID = pCDoubleImpactCheck.PCDoubleImpactCheckID;
                    PCDICDAccessor.Insert(detail);
                }
            }
        }

        public Model.PCDoubleImpactCheck GetDetail(Model.PCDoubleImpactCheck pcdic)
        {
            pcdic.Detail = PCDICDAccessor.Select(pcdic.PCDoubleImpactCheckID);
            return pcdic;
        }

        protected override string GetSettingId()
        {
            return "pcdicRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcdic";
        }

        public IList<Model.PCDoubleImpactCheck> SelectByDateRage(DateTime startdate, DateTime enddate, int pcFlag, Model.Product product, Model.Customer customer, string CusXOId)
        {
            return accessor.SelectByDateRage(startdate, enddate, pcFlag, product, customer, CusXOId);
        }

        public void Validate(Model.PCDoubleImpactCheck pcdic)
        {
            if (string.IsNullOrEmpty(pcdic.PCDoubleImpactCheckID))
                throw new Helper.RequireValueException(Model.PCDoubleImpactCheck.PRO_PCDoubleImpactCheckID);
            if (string.IsNullOrEmpty(pcdic.PCDoubleImpactCheckDate.ToString()))
                throw new Helper.RequireValueException(Model.PCDoubleImpactCheck.PRO_PCDoubleImpactCheckDate);
            if (string.IsNullOrEmpty(pcdic.ProductId))
                throw new Helper.RequireValueException(Model.PCDoubleImpactCheck.PRO_ProductId);
            if (string.IsNullOrEmpty(pcdic.EmployeeId))
                throw new Helper.RequireValueException(Model.PCDoubleImpactCheck.PRO_EmployeeId);
        }

        private void TiGuiExists(Model.PCDoubleImpactCheck model)
        {
            if (this.ExistsPrimary(model.PCDoubleImpactCheckID))
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
                model.PCDoubleImpactCheckID = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

    }
}

