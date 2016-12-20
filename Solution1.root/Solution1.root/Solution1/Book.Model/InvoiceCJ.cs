//------------------------------------------------------------------------------
//
// file name:InvoiceCJ.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 采购报价单
	/// </summary>
	[Serializable]
	public partial class InvoiceCJ : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceCJDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceCJDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
