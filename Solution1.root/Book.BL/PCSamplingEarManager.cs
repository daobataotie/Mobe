//------------------------------------------------------------------------------
//
// file name：PCSamplingEarEarManager.cs
// author: mayanjun
// create date：2015/10/31 16:25:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCSamplingEarEar.
    /// </summary>
    public partial class PCSamplingEarManager:BaseManager
    {

        private static readonly DA.IPCSamplingEarDetailAccessor accessorDetail = (DA.IPCSamplingEarDetailAccessor)Accessors.Get("PCSamplingEarDetailAccessor");
        /// <summary>
        /// Delete PCSamplingEar by primary key.
        /// </summary>
        public void Delete(string PCSamplingEarId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessorDetail.DeleteByPCMaterialCheckId(PCSamplingEarId);
                accessor.Delete(PCSamplingEarId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.PCSamplingEar model)
        {
            if (model != null)
                this.Delete(model.PCSamplingEarId);
        }

        /// <summary>
        /// Insert a PCSamplingEar.
        /// </summary>
        public void Insert(Model.PCSamplingEar PCSamplingEar)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(PCSamplingEar);
                PCSamplingEar.InsertTime = DateTime.Now;
                PCSamplingEar.UpdateTime = DateTime.Now;
                this.TiGuiExists(PCSamplingEar);

                accessor.Insert(PCSamplingEar);
                foreach (var item in PCSamplingEar.Details)
                {
                    item.PCSamplingEarId = PCSamplingEar.PCSamplingEarId;
                    accessorDetail.Insert(item);
                }
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, PCSamplingEar.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, PCSamplingEar.InsertTime.Value.Year, PCSamplingEar.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, PCSamplingEar.InsertTime.Value.ToString("yyyy-MM-dd"));
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
        /// Update a PCSamplingEar.
        /// </summary>
        public void Update(Model.PCSamplingEar PCSamplingEar)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                Validate(PCSamplingEar);
                PCSamplingEar.UpdateTime = DateTime.Now;
                accessor.Update(PCSamplingEar);

                accessorDetail.DeleteByPCMaterialCheckId(PCSamplingEar.PCSamplingEarId);
                foreach (var item in PCSamplingEar.Details)
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
            return "PCSE";
        }

        protected override string GetSettingId()
        {
            return "PCSamplingEar";
        }

        public void Validate(Model.PCSamplingEar model)
        {
            if (model.PCSamplingEarDate == null)
                throw new Helper.InvalidValueException(Model.PCSamplingEar.PRO_PCSamplingEarDate);
            if (string.IsNullOrEmpty(model.PronoteHeaderId))
                throw new Helper.InvalidValueException(Model.PCSamplingEar.PRO_PronoteHeaderId);

        }

        private void TiGuiExists(Model.PCSamplingEar model)
        {
            if (this.ExistsPrimary(model.PCSamplingEarId))
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
                model.PCSamplingEarId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCSamplingEar GetDetail(string id)
        {
            Model.PCSamplingEar model = this.Get(id);
            if (model != null)
                model.Details = accessorDetail.SelectByPCMaterialCheckId(id);
            return model;
        }
    }
}
