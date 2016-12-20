//------------------------------------------------------------------------------
//
// file name:InvoiceXO.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 销售订单
	/// </summary>
	[Serializable]
	public partial class InvoiceXO : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceXODetail> details;

        public System.Collections.Generic.IList<Model.InvoiceXODetail> Details
        {
            get { return details; }
            set { details = value; }
        }
     
	}
}
