//------------------------------------------------------------------------------
//
// file name:InvoiceXT.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 出货单
	/// </summary>
	[Serializable]
	public partial class InvoiceXT : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceXTDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceXTDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

	}
}
