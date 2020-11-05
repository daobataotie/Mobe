//------------------------------------------------------------------------------
//
// file name：PCFirstOnlineCheckManager.cs
// author: mayanjun
// create date：2020/10/30 22:05:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCFirstOnlineCheck.
    /// </summary>
    public partial class PCFirstOnlineCheckManager : BaseManager
    {

        /// <summary>
        /// Delete PCFirstOnlineCheck by primary key.
        /// </summary>
        public void Delete(string pCFirstOnlineCheckId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                detailAccessor.DelectByHeaderId(pCFirstOnlineCheckId);
                accessor.Delete(pCFirstOnlineCheckId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a PCFirstOnlineCheck.
        /// </summary>
        public void Insert(Model.PCFirstOnlineCheck pCFirstOnlineCheck)
        {
            //
            // todo:add other logic here
            //

            Validate(pCFirstOnlineCheck);
            try
            {
                BL.V.BeginTransaction();
                pCFirstOnlineCheck.InsertTime = DateTime.Now;
                pCFirstOnlineCheck.UpdateTime = DateTime.Now;
                accessor.Insert(pCFirstOnlineCheck);

                foreach (Model.PCFirstOnlineCheckDetail model in pCFirstOnlineCheck.Detail)
                {
                    detailAccessor.Insert(model);
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
        /// Update a PCFirstOnlineCheck.
        /// </summary>
        public void Update(Model.PCFirstOnlineCheck pCFirstOnlineCheck)
        {
            //
            // todo: add other logic here.
            //
            Validate(pCFirstOnlineCheck);
            try
            {
                BL.V.BeginTransaction();
                pCFirstOnlineCheck.UpdateTime = DateTime.Now;
                accessor.Update(pCFirstOnlineCheck);

                detailAccessor.DelectByHeaderId(pCFirstOnlineCheck.PCFirstOnlineCheckId);
                foreach (Model.PCFirstOnlineCheckDetail model in pCFirstOnlineCheck.Detail)
                {
                    detailAccessor.Insert(model);
                }

                //IList<Model.PCFirstOnlineCheckDetail> details = detailAccessor.SelectByHeaderId(pCFirstOnlineCheck.PCFirstOnlineCheckId);
                //foreach (var item in pCFirstOnlineCheck.Detail)
                //{
                //    if (details.Any(d => d.PCFirstOnlineCheckDetailId == item.PCFirstOnlineCheckDetailId))
                //        detailAccessor.Update(item);
                //    else
                //        detailAccessor.Insert(item);
                //}


                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public Model.PCFirstOnlineCheck GetDetail(string pCFirstOnlineCheckId)
        {
            Model.PCFirstOnlineCheck model = this.Get(pCFirstOnlineCheckId);

            if (model != null)
            {
                model.Detail = detailAccessor.SelectByHeaderId(pCFirstOnlineCheckId);
            }

            return model;
        }

        protected override string GetSettingId()
        {
            return "PCFirstOnlineCheckRule";
        }

        protected override string GetInvoiceKind()
        {
            return "PFC";
        }

        public void TiGuiExists(Model.PCFirstOnlineCheck model)
        {
            if (this.ExistsPrimary(model.PCFirstOnlineCheckId))
            {
                //设置KEY值
                string invoiceKind = this.GetInvoiceKind().ToLower();
                string sequencekey_y = string.Format("{0}-y-{1}", invoiceKind, DateTime.Now.Year);
                string sequencekey_m = string.Format("{0}-m-{1}-{2}", invoiceKind, DateTime.Now.Year, DateTime.Now.Month);
                string sequencekey_d = string.Format("{0}-d-{1}", invoiceKind, DateTime.Now.ToString("yyyy-MM-dd"));
                string sequencekey = string.Format(invoiceKind);
                SequenceManager.Increment(sequencekey_y);
                SequenceManager.Increment(sequencekey_m);
                SequenceManager.Increment(sequencekey_d);
                SequenceManager.Increment(sequencekey);
                model.PCFirstOnlineCheckId = this.GetId(DateTime.Now);
                TiGuiExists(model);
            }
        }

        private void Validate(Model.PCFirstOnlineCheck model)
        {
            if (model.OnlineDate == null)
                throw new Helper.RequireValueException(Model.PCFirstOnlineCheck.PRO_OnlineDate);
        }
    }
}
