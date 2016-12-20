//------------------------------------------------------------------------------
//
// file name:InvoiceBY.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 报溢单
	/// </summary>
	[Serializable]
	public partial class InvoiceBY : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceBYDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceBYDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
