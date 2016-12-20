//------------------------------------------------------------------------------
//
// file name：InvoiceHR.cs
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
    public partial class InvoiceHR : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceHRDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceHRDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private System.Collections.Generic.IList<Model.InvoiceJCDetail> jcdetails;

        public System.Collections.Generic.IList<Model.InvoiceJCDetail> Jcdetails
        {
            get { return jcdetails; }
            set { jcdetails = value; }
        }
	}
}
