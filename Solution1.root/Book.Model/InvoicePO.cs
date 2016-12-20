//------------------------------------------------------------------------------
//
// file name：InvoicePO.cs
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
    public partial class InvoicePO : Invoice
    {
        private System.Collections.Generic.IList<Model.InvoicePODetail> details = new System.Collections.Generic.List<Model.InvoicePODetail>();

        public System.Collections.Generic.IList<Model.InvoicePODetail> Details
        {
            get { return details; }
            set { details = value; }
        }

	}
}
