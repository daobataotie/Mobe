//------------------------------------------------------------------------------
//
// file name:InvoiceQO.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 其他支出单
	/// </summary>
	[Serializable]
	public partial class InvoiceQO : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceQODetail> details;

        public System.Collections.Generic.IList<Model.InvoiceQODetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
