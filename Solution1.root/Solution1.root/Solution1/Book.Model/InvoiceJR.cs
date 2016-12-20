//------------------------------------------------------------------------------
//
// file name：InvoiceJR.cs
// author: peidun
// create date：2008-11-29 11:07:05
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
    public partial class InvoiceJR : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceJRDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceJRDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
