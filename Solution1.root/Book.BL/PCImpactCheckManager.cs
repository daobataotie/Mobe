//------------------------------------------------------------------------------
//
// file name：PCImpactCheckManager.cs
// author: mayanjun
// create date：2011-11-15 13:56:51
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCImpactCheck.
    /// </summary>
    public partial class PCImpactCheckManager : BaseManager
    {
        private static readonly DA.IPCImpactCheckDetailAccessor PCImpactCheckDetailAccessor = (DA.IPCImpactCheckDetailAccessor)Accessors.Get("PCImpactCheckDetailAccessor");
        private static readonly PCImpactCheckDetailManager PCICDManager = new PCImpactCheckDetailManager();

        /// <summary>
        /// Delete PCImpactCheck by primary key.
        /// </summary>
        public void Delete(string pCImpactCheckId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCImpactCheckId);
        }

        public void Delete(Model.PCImpactCheck pcic)
        {
            try
            {
                BL.V.BeginTransaction();

                PCImpactCheckDetailAccessor.DeleteByPCImpactCheckId(pcic.PCImpactCheckId);

                this.Delete(pcic.PCImpactCheckId);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a PCImpactCheck.
        /// </summary>
        public void Insert(Model.PCImpactCheck pCImpactCheck)
        {
            Validate(pCImpactCheck);

            try
            {
                BL.V.BeginTransaction();
                pCImpactCheck.InsertTime = DateTime.Now;
                pCImpactCheck.UpdateTime = DateTime.Now;
                TiGuiExists(pCImpactCheck);

                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCImpactCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCImpactCheck.InsertTime.Value.Year, pCImpactCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCImpactCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCImpactCheck);

                foreach (Model.PCImpactCheckDetail pcicDetail in pCImpactCheck.Details)
                {
                    pcicDetail.PCImpactCheckId = pCImpactCheck.PCImpactCheckId;
                    PCICDManager.Insert(pcicDetail);
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
        /// Update a PCImpactCheck.
        /// </summary>
        public void Update(Model.PCImpactCheck pCImpactCheck)
        {
            Validate(pCImpactCheck);
            if (pCImpactCheck != null)
            {
                //修改头
                pCImpactCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCImpactCheck);
                //删除详细
                PCImpactCheckDetailAccessor.DeleteByPCImpactCheckId(pCImpactCheck.PCImpactCheckId);
                //添加详细
                foreach (Model.PCImpactCheckDetail details in pCImpactCheck.Details)
                {
                    details.PCImpactCheckId = pCImpactCheck.PCImpactCheckId;
                    PCImpactCheckDetailAccessor.Insert(details);
                }
            }
        }

        public Model.PCImpactCheck GetDetail(string pCImpactCheckId)
        {
            Model.PCImpactCheck pcic = accessor.Get(pCImpactCheckId);
            pcic.Details = PCImpactCheckDetailAccessor.Select(pCImpactCheckId);
            return pcic;
        }

        protected override string GetSettingId()
        {
            return "pcicRule";
        }

        protected override string GetInvoiceKind()
        {
            return "pcic";
        }

        //数据验证
        private void Validate(Model.PCImpactCheck pcic)
        {
            if (string.IsNullOrEmpty(pcic.PCImpactCheckId))
                throw new Helper.RequireValueException(Model.PCImpactCheck.PRO_PCImpactCheckId);
            if (string.IsNullOrEmpty(pcic.PCImpactCheckDate.Value.ToString()))
                throw new Helper.RequireValueException(Model.PCImpactCheck.PRO_PCImpactCheckDate);
            if (string.IsNullOrEmpty(pcic.ProductId))
                throw new Helper.RequireValueException(Model.PCImpactCheck.PRO_ProductId);
            if (string.IsNullOrEmpty(pcic.EmployeeId))
                throw new Helper.RequireValueException(Model.PCImpactCheck.PRO_EmployeeId);
        }

        public IList<Model.PCImpactCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, Model.Product product, Model.Customer customer, string CusXOId)
        {
            return accessor.SelectByDateRage(StartDate, EndDate, product, customer, CusXOId);
        }

        private void TiGuiExists(Model.PCImpactCheck model)
        {
            if (this.ExistsPrimary(model.PCImpactCheckId))
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
                model.PCImpactCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }
    }
}

