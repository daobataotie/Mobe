//------------------------------------------------------------------------------
//
// file name:InvoiceQI.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 其他收入单
	/// </summary>
	[Serializable]
	public partial class InvoiceQI : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceQIDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceQIDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
