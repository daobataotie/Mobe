//------------------------------------------------------------------------------
//
// file name：AcPayment.cs
// author: mayanjun
// create date：2011-6-23 09:29:23
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 付款结算
	/// </summary>
	[Serializable]
	public partial class AcPayment
	{
        private System.Collections.Generic.IList<Model.AcPaymentDetail> detail;

        public System.Collections.Generic.IList<Model.AcPaymentDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
	}
}
