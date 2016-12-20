//------------------------------------------------------------------------------
//
// file name:InvoiceFK.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 付款单
	/// </summary>
	[Serializable]
	public partial class InvoiceFK : Invoice
	{
        private System.Collections.Generic.IList<Model.Invoice01> details;

        public System.Collections.Generic.IList<Model.Invoice01> Details
        {
            get { return details; }
            set { details = value; }
        }

	}
}
