//------------------------------------------------------------------------------
//
// file name：PCSamplingManager.cs
// author: mayanjun
// create date：2015/10/30 17:07:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCSampling.
    /// </summary>
    public partial class PCSamplingManager : BaseManager
    {
        private static readonly DA.IPCSamplingDetailAccessor accessorDetail = (DA.IPCSamplingDetailAccessor)Accessors.Get("PCSamplingDetailAccessor");
        /// <summary>
        /// Delete PCSampling by primary key.
        /// </summary>
        public void Delete(string pCSamplingId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessorDetail.DeleteByPCMaterialCheckId(pCSamplingId);
                accessor.Delete(pCSamplingId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.PCSampling model)
        {
            if (model != null)
                this.Delete(model.PCSamplingId);
        }

        /// <summary>
        /// Insert a PCSampling.
        /// </summary>
        public void Insert(Model.PCSampling pCSampling)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(pCSampling);
                pCSampling.InsertTime = DateTime.Now;
                pCSampling.UpdateTime = DateTime.Now;
                this.TiGuiExists(pCSampling);

                accessor.Insert(pCSampling);
                foreach (var item in pCSampling.Details)
                {
                    item.PCSamplingId = pCSampling.PCSamplingId;
                    accessorDetail.Insert(item);
                }
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCSampling.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCSampling.InsertTime.Value.Year, pCSampling.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCSampling.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a PCSampling.
        /// </summary>
        public void Update(Model.PCSampling pCSampling)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                Validate(pCSampling);
                pCSampling.UpdateTime = DateTime.Now;
                accessor.Update(pCSampling);

                accessorDetail.DeleteByPCMaterialCheckId(pCSampling.PCSamplingId);
                foreach (var item in pCSampling.Details)
                {
                    accessorDetail.Insert(item);
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
            return "PCS";
        }

        protected override string GetSettingId()
        {
            return "PCSampling";
        }

        public void Validate(Model.PCSampling model)
        {
            if (model.PCSamplingDate == null)
                throw new Helper.InvalidValueException(Model.PCSampling.PRO_PCSamplingDate);
            if (string.IsNullOrEmpty(model.PronoteHeaderId))
                throw new Helper.InvalidValueException(Model.PCSampling.PRO_PronoteHeaderId);

        }

        private void TiGuiExists(Model.PCSampling model)
        {
            if (this.ExistsPrimary(model.PCSamplingId))
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
                model.PCSamplingId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCSampling GetDetail(string id)
        {
            Model.PCSampling model = this.Get(id);
            if (model != null)
                model.Details = accessorDetail.SelectByPCMaterialCheckId(id);
            return model;
        }
    }
}
