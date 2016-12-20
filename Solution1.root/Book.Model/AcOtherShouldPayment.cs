//------------------------------------------------------------------------------
//
// file name：AcOtherShouldPayment.cs
// author: mayanjun
// create date：2011-6-10 10:11:51
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 其他应付款
    /// </summary>
    [Serializable]
    public partial class AcOtherShouldPayment
    {
        private System.Collections.Generic.IList<AcOtherShouldPaymentDetail> details;

        public System.Collections.Generic.IList<AcOtherShouldPaymentDetail> Details
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
