//------------------------------------------------------------------------------
//
// file name：InvoiceHC.cs
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
    public partial class InvoiceHC : Invoice
    {
        private System.Collections.Generic.IList<Model.InvoiceHCDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceHCDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private System.Collections.Generic.IList<Model.InvoiceJRDetail> jrdetails;

        public System.Collections.Generic.IList<Model.InvoiceJRDetail> Jrdetails
        {
            get { return jrdetails; }
            set { jrdetails = value; }
        }
    }
}
