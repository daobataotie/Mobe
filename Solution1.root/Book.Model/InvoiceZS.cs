//------------------------------------------------------------------------------
//
// file name:InvoiceZS.cs
// author: peidun
// create date:2008/6/20 15:33:25
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 赠送单
	/// </summary>
	[Serializable]
	public partial class InvoiceZS : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceZSDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceZSDetail> Details
        {
            get { return details; }
            set { details = value; }
        }       


	}
}
