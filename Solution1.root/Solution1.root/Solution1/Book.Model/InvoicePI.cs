//------------------------------------------------------------------------------
//
// file name：InvoicePI.cs
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
    public partial class InvoicePI : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoicePIDetail> details;

        public System.Collections.Generic.IList<Model.InvoicePIDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private System.Collections.Generic.IList<Model.InvoicePODetail> podetails;

        public System.Collections.Generic.IList<Model.InvoicePODetail> Podetails
        {
            get { return podetails; }
            set { podetails = value; }
        }
	}
}
