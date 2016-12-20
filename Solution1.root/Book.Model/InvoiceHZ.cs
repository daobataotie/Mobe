//------------------------------------------------------------------------------
//
// file name:InvoiceHZ.cs
// author: peidun
// create date:2008/6/20 15:33:25
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 获赠单
	/// </summary>
	[Serializable]
	public partial class InvoiceHZ : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceHZDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceHZDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
