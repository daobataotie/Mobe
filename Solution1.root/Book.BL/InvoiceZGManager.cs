//------------------------------------------------------------------------------
//
// file name：InvoiceZGManager.cs
// author: mayanjun
// create date：2012-11-19 14:13:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceZG.
    /// </summary>
    public partial class InvoiceZGManager : BaseManager
    {

        /// <summary>
        /// Delete InvoiceZG by primary key.
        /// </summary>
        public void Delete(string invoiceZGId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(invoiceZGId);
        }

        /// <summary>
        /// Insert a InvoiceZG.
        /// </summary>
        public void Insert(Model.InvoiceZG invoiceZG)
        {
            //
            // todo:add other logic here
            //
            //TiGuiExists(invoiceZG);
            Validate(invoiceZG);
            invoiceZG.InsertTime = DateTime.Now;
            invoiceZG.UpdateTime = DateTime.Now;
            accessor.Insert(invoiceZG);
            foreach (Model.InvoiceZGDetail detai in invoiceZG.Details)
            {
                (new InvoiceZGDetailManager()).Insert(detai);
            }
        }

        /// <summary>
        /// Update a InvoiceZG.
        /// </summary>
        public void Update(Model.InvoiceZG invoiceZG)
        {
            //
            // todo: add other logic here.
            //
            Validate(invoiceZG);
            invoiceZG.UpdateTime = DateTime.Now;
            accessor.Update(invoiceZG);
            (new InvoiceZGDetailManager()).DeleteByInvoiceZGId(invoiceZG.InvoiceZGId);
            foreach (Model.InvoiceZGDetail detai in invoiceZG.Details)
            {
                (new InvoiceZGDetailManager()).Insert(detai);
            }
        }

        protected override string GetSettingId()
        {
            return "InvoiceNumberRuleOfZG";
        }

        protected override string GetInvoiceKind()
        {
            return "ZG";
        }

        public void TiGuiExists(Model.InvoiceZG model)
        {
            if (this.ExistsPrimary(model.InvoiceZGId))
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
                model.InvoiceZGId = this.GetId(DateTime.Now);
                TiGuiExists(model);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }

        private void Validate(Model.InvoiceZG invoicezg)
        {
            if (invoicezg.InvoiceZGDate == null)
            {
                throw new Helper.RequireValueException(Model.InvoiceZG.PRO_InvoiceZGDate);
            }
        }

        public IList<Model.InvoiceZG> SelectInvoiceZG(DateTime StartDate, DateTime EndDate, Model.Customer XOcustomer, string InvoiceId, string ShippedId)
        {
           return accessor.SelectInvoiceZG(StartDate, EndDate, XOcustomer, InvoiceId, ShippedId);
        }
    }
}

