//------------------------------------------------------------------------------
//
// file name：InvoicePackingManager.cs
// author: mayanjun
// create date：2013-1-14 10:58:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoicePacking.
    /// </summary>
    public partial class InvoicePackingManager : BaseManager
    {

        /// <summary>
        /// Delete InvoicePacking by primary key.
        /// </summary>
        public void Delete(string invoicePackingId)
        {
            //
            // todo:add other logic here
            //
            (new BL.InvoicePackingDetailManager()).DeleteByInvoicePackingId(invoicePackingId);
            (new BL.CustomerMarksManager()).DeleteByInvoicePackingId(invoicePackingId);
            accessor.Delete(invoicePackingId);
        }

        /// <summary>
        /// Insert a InvoicePacking.
        /// </summary>
        public void Insert(Model.InvoicePacking invoicePacking)
        {
            //
            // todo:add other logic here
            //
            this.Validate(invoicePacking);
            invoicePacking.InsertTime = DateTime.Now;
            invoicePacking.UpdateTime = DateTime.Now;
            accessor.Insert(invoicePacking);
            foreach (Model.InvoicePackingDetail detail in invoicePacking.Details)
            {
                (new BL.InvoicePackingDetailManager()).Insert(detail);
            }
            foreach (Model.CustomerMarks mark in invoicePacking.Marks)
            {
                (new BL.CustomerMarksManager()).Insert(mark);
            }
        }

        /// <summary>
        /// Update a InvoicePacking.
        /// </summary>
        public void Update(Model.InvoicePacking invoicePacking)
        {
            //
            // todo: add other logic here.
            //
            this.Validate(invoicePacking);
            invoicePacking.UpdateTime = DateTime.Now;
            accessor.Update(invoicePacking);
            (new BL.InvoicePackingDetailManager()).DeleteByInvoicePackingId(invoicePacking.InvoicePackingId);
            foreach (Model.InvoicePackingDetail detail in invoicePacking.Details)
            {
                (new BL.InvoicePackingDetailManager()).Insert(detail);
            }
            (new BL.CustomerMarksManager()).DeleteByInvoicePackingId(invoicePacking.InvoicePackingId);
            foreach (Model.CustomerMarks mark in invoicePacking.Marks)
            {
                (new BL.CustomerMarksManager()).Insert(mark);
            }
        }

        protected override string GetSettingId()
        {
            return "InvoiceNumberRuleOfZX";
        }

        protected override string GetInvoiceKind()
        {
            return "ZX";
        }

        public void TiGuiExists(Model.InvoicePacking model)
        {
            if (this.ExistsPrimary(model.InvoicePackingId))
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
                model.InvoicePackingId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        private void Validate(Model.InvoicePacking model)
        {
            if (string.IsNullOrEmpty(model.InvoiceNO))
                throw new Helper.RequireValueException(Model.InvoicePacking.PRO_InvoiceNO);
            if (model.InvoicePackingDate == null)
                throw new Helper.RequireValueException(Model.InvoicePacking.PRO_InvoicePackingDate);
            foreach (Model.InvoicePackingDetail detail in model.Details)
            {
                if (string.IsNullOrEmpty(detail.HandPackingId))
                    throw new Helper.RequireValueException(Model.InvoicePackingDetail.PRO_HandPackingId);
            }
        }

        public IList<Model.InvoicePacking> SelectByCondition(DateTime startDate, DateTime endDate, string No, string InvoiceOf, string ShippedBy, string Consignee)
        {
            return accessor.SelectByCondition(startDate, endDate, No, InvoiceOf, ShippedBy, Consignee);
        }

        public string SelectCustomerInvoiceId(string id)
        {
            return accessor.SelectCustomerInvoiceId(id);
        }
    }
}

