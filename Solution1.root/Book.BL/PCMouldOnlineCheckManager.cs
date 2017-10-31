//------------------------------------------------------------------------------
//
// file name：PCMouldOnlineCheckManager.cs
// author: mayanjun
// create date：2015/4/13 上午 10:11:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCMouldOnlineCheck.
    /// </summary>
    public partial class PCMouldOnlineCheckManager : BaseManager
    {
        private static readonly DA.IPCMouldOnlineCheckDetailAccessor Detailaccessor = (DA.IPCMouldOnlineCheckDetailAccessor)Accessors.Get("PCMouldOnlineCheckDetailAccessor");

        /// <summary>
        /// Delete PCMouldOnlineCheck by primary key.
        /// </summary>
        public void Delete(string pCMouldOnlineCheckId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCMouldOnlineCheckId);
        }

        public void Delete(Model.PCMouldOnlineCheck pCMouldOnlineCheck)
        {
            try
            {
                BL.V.BeginTransaction();
                //删除详细
                Detailaccessor.DeleteByHeaderId(pCMouldOnlineCheck.PCMouldOnlineCheckId);
                accessor.Delete(pCMouldOnlineCheck.PCMouldOnlineCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a PCMouldOnlineCheck.
        /// </summary>
        public void Insert(Model.PCMouldOnlineCheck pCMouldOnlineCheck)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(pCMouldOnlineCheck);
                pCMouldOnlineCheck.InsertTime = DateTime.Now;
                pCMouldOnlineCheck.UpdateTime = DateTime.Now;
                this.TiGuiExists(pCMouldOnlineCheck);
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, pCMouldOnlineCheck.InsertTime.Value.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, pCMouldOnlineCheck.InsertTime.Value.Year, pCMouldOnlineCheck.InsertTime.Value.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, pCMouldOnlineCheck.InsertTime.Value.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);

                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);

                accessor.Insert(pCMouldOnlineCheck);

                foreach (var item in pCMouldOnlineCheck.Detail)
                {
                    item.PCMouldOnlineCheckId = pCMouldOnlineCheck.PCMouldOnlineCheckId;
                    Detailaccessor.Insert(item);
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
        /// Update a PCMouldOnlineCheck.
        /// </summary>
        public void Update(Model.PCMouldOnlineCheck pCMouldOnlineCheck)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(pCMouldOnlineCheck);
                pCMouldOnlineCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCMouldOnlineCheck);

                //删除详细
                Detailaccessor.DeleteByHeaderId(pCMouldOnlineCheck.PCMouldOnlineCheckId);
                //增加详细
                foreach (var item in pCMouldOnlineCheck.Detail)
                {
                    item.PCMouldOnlineCheckId = pCMouldOnlineCheck.PCMouldOnlineCheckId;
                    Detailaccessor.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        protected override string GetSettingId()
        {
            return "PCMouldOnlineCheck";
        }

        protected override string GetInvoiceKind()
        {
            return "PMC";
        }

        public void Validate(Model.PCMouldOnlineCheck model)
        {
            if (model.PCMouldOnlineCheckDate == null)
                throw new Helper.InvalidValueException(Model.PCMouldOnlineCheck.PRO_PCMouldOnlineCheckDate);

            foreach (var item in model.Detail)
            {
                if (item.OnlineDate == null)
                    throw new Helper.InvalidValueException(Model.PCMouldOnlineCheckDetail.PRO_OnlineDate);
                if (item.CheckDate == null)
                    throw new Helper.InvalidValueException(Model.PCMouldOnlineCheckDetail.PRO_CheckDate);
            }
        }

        private void TiGuiExists(Model.PCMouldOnlineCheck model)
        {
            if (this.ExistsPrimary(model.PCMouldOnlineCheckId))
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
                model.PCMouldOnlineCheckId = this.GetId(model.InsertTime.Value);
                TiGuiExists(model);
            }
        }

        public Book.Model.PCMouldOnlineCheck GetDetail(string p)
        {
            Model.PCMouldOnlineCheck model = this.Get(p);
            if (model != null)
                model.Detail = Detailaccessor.SelectByHeaderId(p);
            return model;
        }
    }
}
