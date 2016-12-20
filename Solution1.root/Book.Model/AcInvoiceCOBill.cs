//------------------------------------------------------------------------------
//
// file name：AcInvoiceCOBill.cs
// author: mayanjun
// create date：2011-06-27 15:07:22
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 采购发票
    /// </summary>
    [Serializable]
    public partial class AcInvoiceCOBill
    {
        private System.Collections.Generic.IList<Model.AcInvoiceCOBillDetail> details;

        public System.Collections.Generic.IList<Model.AcInvoiceCOBillDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private bool _Checked;

        public bool Checked
        {
            get
            {
                return this._Checked;
            }
            set
            {
                this._Checked = value;
            }
        }
    }
}
