//------------------------------------------------------------------------------
//
// file name:InvoiceSK.cs
// author: peidun
// create date:2008/6/6 14:48:21
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 收款单
	/// </summary>
	[Serializable]
	public partial class InvoiceSK : Invoice
	{
        private System.Collections.Generic.IList<Model.Invoice01> details;

        public System.Collections.Generic.IList<Model.Invoice01> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
