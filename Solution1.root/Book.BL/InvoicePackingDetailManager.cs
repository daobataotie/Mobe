//------------------------------------------------------------------------------
//
// file name：InvoicePackingDetailManager.cs
// author: mayanjun
// create date：2013-1-14 10:58:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoicePackingDetail.
    /// </summary>
    public partial class InvoicePackingDetailManager : BaseManager
    {

        /// <summary>
        /// Delete InvoicePackingDetail by primary key.
        /// </summary>
        public void Delete(string invoicePackingDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(invoicePackingDetailId);
        }

        /// <summary>
        /// Insert a InvoicePackingDetail.
        /// </summary>
        public void Insert(Model.InvoicePackingDetail invoicePackingDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(invoicePackingDetail);
        }

        /// <summary>
        /// Update a InvoicePackingDetail.
        /// </summary>
        public void Update(Model.InvoicePackingDetail invoicePackingDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(invoicePackingDetail);
        }

        protected override string GetSettingId()
        {
            return "InvoiceNumberRuleOfBOX";
        }

        protected override string GetInvoiceKind()
        {
            return "BOX";
        }

        public void TiGuiExists(Model.InvoicePackingDetail model)
        {
            if (this.ExistsPrimary(model.InvoicePackingDetailId))
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
                model.InvoicePackingDetailId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        private void Validate(Model.InvoicePackingDetail detail)
        {
        }

        public IList<Model.InvoicePackingDetail> SelectByInvoicePackingId(string InvoicePackingId)
        {
            return accessor.SelectByInvoicePackingId(InvoicePackingId);
        }

        public void DeleteByInvoicePackingId(string InvoicePackingId)
        {
            accessor.DeleteByInvoicePackingId(InvoicePackingId);
        }
    }
}

