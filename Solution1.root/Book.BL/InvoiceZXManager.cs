//------------------------------------------------------------------------------
//
// file name：InvoiceZXManager.cs
// author: mayanjun
// create date：2012-10-29 14:32:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.InvoiceZX.
    /// </summary>
    public partial class InvoiceZXManager
    {
        //private InvoiceZXDetailManager dm = new InvoiceZXDetailManager();

        public void Delete(string invoiceZXId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(invoiceZXId);
        }

        public void Insert(Model.InvoiceZX invoiceZX)
        {
            //
            // todo:add other logic here
            //
            //
            invoiceZX.InsertTime = DateTime.Now;
            invoiceZX.UpdateTime = DateTime.Now;
            accessor.Insert(invoiceZX);
        }

        public void Update(Model.InvoiceZX invoiceZX)
        {
            //
            // todo: add other logic here.
            //
            invoiceZX.UpdateTime = DateTime.Now;
            accessor.Update(invoiceZX);
        }

        public void InsertList(IList<Model.InvoiceZX> invoicezxlist)
        {
            ValidateList(invoicezxlist);
            foreach (Model.InvoiceZX invoicezx in invoicezxlist)
            {
                Insert(invoicezx);
            }
        }

        //private void Validate(Model.InvoiceZX invoice)
        //{
        //    if (invoice.PackingId == "" || invoice.PackingId == null)
        //        throw new Helper.RequireValueException(Model.InvoiceZX.PRO_PackingId);
        //    IList<Model.InvoiceZX> zx = accessor.selectByPackingId(invoice.PackingId);
        //    if (zx != null && zx.Count > 0 && zx[0].PackingId == invoice.PackingId && zx[0].InvoiceZXId != invoice.InvoiceZXId)
        //        throw new Helper.InvalidValueException(Model.InvoiceZX.PRO_PackingId);
        //    if (invoice.BLong <= 0)
        //        throw new Helper.InvalidValueException(Model.InvoiceZX.PRO_BLong);
        //    if (invoice.BWide <= 0)
        //        throw new Helper.InvalidValueException(Model.InvoiceZX.PRO_BWide);
        //    if (invoice.BHigh <= 0)
        //        throw new Helper.InvalidValueException(Model.InvoiceZX.PRO_BHigh);
        //    if (invoice.BWeight <= 0)
        //        throw new Helper.InvalidValueException(Model.InvoiceZX.PRO_BWeight);
        //    if (invoice.AllWeight <= 0)
        //        throw new Helper.InvalidValueException(Model.InvoiceZX.PRO_AllWeight);
        //    if (invoice.AllWeight <= invoice.BWeight)
        //        throw new Helper.InvalidValueException(Model.InvoiceZX.PRO_AllWeight + Model.InvoiceZX.PRO_BWeight);
        //}

        public IList<Model.InvoiceZX> SelectPackingRecord(DateTime dateStart, DateTime dateEnd, Model.Customer customer, Model.Customer XOcustomer)
        {
            return accessor.SelectPackingRecord(dateStart, dateEnd, customer, XOcustomer);
        }

        private void ValidateList(IList<Model.InvoiceZX> invoicezxlist)
        {
            foreach (var item in invoicezxlist)
            {
                if (item.PackingId == "" || item.PackingId == null)
                    throw new Helper.RequireValueException(Model.InvoiceZX.PRO_PackingId);
                if (item.InvoiceDate == null)
                    throw new Helper.RequireValueException(Model.InvoiceZX.PRO_InvoiceDate);
                if (item.EmployeeId == null)
                    throw new Helper.RequireValueException(Model.InvoiceZX.PRO_EmployeeId);

                IList<Model.InvoiceZX> zx = accessor.selectByPackingId(item.PackingId);
                if (zx != null && zx.Count > 0 && zx[0].PackingId == item.PackingId && zx[0].InvoiceZXId != item.InvoiceZXId)
                    throw new Helper.InvalidValueException(Model.InvoiceZX.PRO_PackingId + "," + item.PackingId);
            }
        }

        public int SelectHasPackingNum(string productId)
        {
            return accessor.SelectHasPackingNum(productId);
        }
    }
}

