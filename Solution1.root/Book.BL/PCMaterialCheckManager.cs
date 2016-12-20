//------------------------------------------------------------------------------
//
// file name：PCMaterialCheckManager.cs
// author: mayanjun
// create date：2015/10/24 17:47:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCMaterialCheck.
    /// </summary>
    public partial class PCMaterialCheckManager
    {
        private static readonly DA.IPCMaterialCheckDetailAccessor accessorDetail = (DA.IPCMaterialCheckDetailAccessor)Accessors.Get("PCMaterialCheckDetailAccessor");

        /// <summary>
        /// Delete PCMaterialCheck by primary key.
        /// </summary>
        public void Delete(string pCMaterialCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessorDetail.DeleteByPCMaterialCheckId(pCMaterialCheckId);
                accessor.Delete(pCMaterialCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.PCMaterialCheck model)
        {
            this.Delete(model.PCMaterialCheckId);
        }

        /// <summary>
        /// Insert a PCMaterialCheck.
        /// </summary>
        public void Insert(Model.PCMaterialCheck pCMaterialCheck)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                pCMaterialCheck.InsertTime = DateTime.Now;
                pCMaterialCheck.UpdateTime = DateTime.Now;
                Validate(pCMaterialCheck);
                TiGuiExists(pCMaterialCheck);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCMaterialCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCMaterialCheck.InsertTime.Value.Year, pCMaterialCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCMaterialCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                accessor.Insert(pCMaterialCheck);

                foreach (var item in pCMaterialCheck.Details)
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

        /// <summary>
        /// Update a PCMaterialCheck.
        /// </summary>
        public void Update(Model.PCMaterialCheck pCMaterialCheck)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                Validate(pCMaterialCheck);
                pCMaterialCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCMaterialCheck);

                //先删除详细再添加
                accessorDetail.DeleteByPCMaterialCheckId(pCMaterialCheck.PCMaterialCheckId);
                foreach (var item in pCMaterialCheck.Details)
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
            return "PMC";
        }

        protected override string GetSettingId()
        {
            return "PCMaterialCheck";
        }

        public void Validate(Model.PCMaterialCheck model)
        {
            if (model.PCMaterialCheckDate == null)
                throw new Helper.InvalidValueException(Model.PCMaterialCheck.PRO_PCMaterialCheckDate);

            if (string.IsNullOrEmpty(model.InvoiceCOId))
                throw new Helper.InvalidValueException(Model.PCMaterialCheck.PRO_InvoiceCOId);
        }

        private void TiGuiExists(Model.PCMaterialCheck model)
        {
            if (this.ExistsPrimary(model.PCMaterialCheckId))
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
                model.PCMaterialCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCMaterialCheck GetDetail(string id)
        {
            Model.PCMaterialCheck model = this.Get(id);
            if (model != null)
                model.Details = accessorDetail.SelectByPCMaterialCheckId(id);
            return model;
        }
    }
}
