//------------------------------------------------------------------------------
//
// file name：PCBoxFootCheckManager.cs
// author: mayanjun
// create date：2013-1-28 15:42:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCBoxFootCheck.
    /// </summary>
    public partial class PCBoxFootCheckManager : BaseManager
    {
        private static readonly DA.IPCBoxFootCheckDetailAccessor DetailAccessor = (DA.IPCBoxFootCheckDetailAccessor)Accessors.Get("PCBoxFootCheckDetailAccessor");
        /// <summary>
        /// Delete PCBoxFootCheck by primary key.
        /// </summary>
        public void Delete(string pCBoxFootCheckId)
        {
            //
            // todo:add other logic here
            //
            DetailAccessor.DeleteByPCBoxFootCheckId(pCBoxFootCheckId);
            accessor.Delete(pCBoxFootCheckId);
        }

        public void Delete(Model.PCBoxFootCheck PCBoxFootCheck)
        {
            if (PCBoxFootCheck != null)
                this.Delete(PCBoxFootCheck.PCBoxFootCheckId);
        }

        /// <summary>
        /// Insert a PCBoxFootCheck.
        /// </summary>
        public void Insert(Model.PCBoxFootCheck pCBoxFootCheck)
        {
            //
            // todo:add other logic here
            //
            pCBoxFootCheck.InsertTime = DateTime.Now;
            pCBoxFootCheck.UpdateTime = DateTime.Now;
            accessor.Insert(pCBoxFootCheck);

            foreach (var item in pCBoxFootCheck.Details)
            {
                item.PCBoxFootCheckId = pCBoxFootCheck.PCBoxFootCheckId;
                DetailAccessor.Insert(item);
            }
        }

        /// <summary>
        /// Update a PCBoxFootCheck.
        /// </summary>  
        public void Update(Model.PCBoxFootCheck pCBoxFootCheck)
        {
            //
            // todo: add other logic here.
            //
            pCBoxFootCheck.UpdateTime = DateTime.Now;
            accessor.Update(pCBoxFootCheck);

            DetailAccessor.DeleteByPCBoxFootCheckId(pCBoxFootCheck.PCBoxFootCheckId);
            foreach (var item in pCBoxFootCheck.Details)
            {
                item.PCBoxFootCheckId = pCBoxFootCheck.PCBoxFootCheckId;
                DetailAccessor.Insert(item);
            }
        }

        protected override string GetInvoiceKind()
        {
            return "BFC";
        }

        protected override string GetSettingId()
        {
            return "PCBoxFootCheckRule";
        }

        public void TiGuiExists(Model.PCBoxFootCheck model)
        {
            if (this.ExistsPrimary(model.PCBoxFootCheckId))
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
                model.PCBoxFootCheckId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }
        }

        public IList<Model.PCBoxFootCheck> SelectByRage(DateTime StartDate, DateTime EndDate, string InvoiceXOId, string PronoteHeaderId, Model.Product product)
        {
            return accessor.SelectByRage(StartDate, EndDate, InvoiceXOId, PronoteHeaderId, product);
        }
    }
}

