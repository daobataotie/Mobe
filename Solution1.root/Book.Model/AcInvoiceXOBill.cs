//------------------------------------------------------------------------------
//
// file name：AcInvoiceXOBill.cs
// author: mayanjun
// create date：2011-09-28 08:45:16
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 销售发票
    /// </summary>
    [Serializable]
    public partial class AcInvoiceXOBill
    {
        private System.Collections.Generic.IList<Model.AcInvoiceXOBillDetail> details;

        public System.Collections.Generic.IList<Model.AcInvoiceXOBillDetail> Details
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
