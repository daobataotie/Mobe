//------------------------------------------------------------------------------
//
// file name：InvoiceJC.cs
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
    public partial class InvoiceJC : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceJCDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceJCDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
