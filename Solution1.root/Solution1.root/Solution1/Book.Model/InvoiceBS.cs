//------------------------------------------------------------------------------
//
// file name:InvoiceBS.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 报损单
	/// </summary>
	[Serializable]
	public partial class InvoiceBS : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceBSDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceBSDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
