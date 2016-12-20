//------------------------------------------------------------------------------
//
// file name：PCIncomingCheckManager.cs
// author: mayanjun
// create date：2015/11/8 20:10:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCIncomingCheck.
    /// </summary>
    public partial class PCIncomingCheckManager : BaseManager
    {
        private static readonly DA.IPCIncomingCheckDetailAccessor accessorDetail = (DA.IPCIncomingCheckDetailAccessor)Accessors.Get("PCIncomingCheckDetailAccessor");

        /// <summary>
        /// Delete PCIncomingCheck by primary key.
        /// </summary>
        public void Delete(string pCIncomingCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessorDetail.DeleteByPrimaryId(pCIncomingCheckId);
                accessor.Delete(pCIncomingCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.PCIncomingCheck model)
        {
            if (model != null)
                this.Delete(model.PCIncomingCheckId);
        }

        /// <summary>
        /// Insert a PCIncomingCheck.
        /// </summary>
        public void Insert(Model.PCIncomingCheck pCIncomingCheck)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(pCIncomingCheck);
                this.TiGuiExists(pCIncomingCheck);

                pCIncomingCheck.InsertTime = DateTime.Now;
                pCIncomingCheck.UpdateTime = DateTime.Now;
                accessor.Insert(pCIncomingCheck);

                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCIncomingCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCIncomingCheck.InsertTime.Value.Year, pCIncomingCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCIncomingCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                foreach (var item in pCIncomingCheck.Detail)
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
        /// Update a PCIncomingCheck.
        /// </summary>
        public void Update(Model.PCIncomingCheck pCIncomingCheck)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(pCIncomingCheck);
                pCIncomingCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCIncomingCheck);

                accessorDetail.DeleteByPrimaryId(pCIncomingCheck.PCIncomingCheckId);
                foreach (var item in pCIncomingCheck.Detail)
                {
                    accessorDetail.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                return;
            }
        }

        protected override string GetInvoiceKind()
        {
            return "PIC";
        }

        protected override string GetSettingId()
        {
            return "PCIncomingCheck";
        }

        public void Validate(Model.PCIncomingCheck model)
        {
            //if (model.PCInputCheckDate == null)
            //    throw new Helper.InvalidValueException(Model.PCInputCheck.PRO_PCInputCheckDate);

            //if (string.IsNullOrEmpty(model.Note))
            //    throw new Helper.MessageValueException("批號不能為空");
        }

        private void TiGuiExists(Model.PCIncomingCheck model)
        {
            if (this.ExistsPrimary(model.PCIncomingCheckId))
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
                model.PCIncomingCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Model.PCIncomingCheck GetDetail(string PCIncomingCheckId)
        {
            Model.PCIncomingCheck model = this.Get(PCIncomingCheckId);
            if (model != null)
                model.Detail = accessorDetail.SelectByPrimaryId(PCIncomingCheckId);
            return model;
        }
    }
}
