//------------------------------------------------------------------------------
//
// file name:InvoiceZZ.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 货品组装单
	/// </summary>
	[Serializable]
	public partial class InvoiceZZ : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceZZDetail> detailsIn;

        public System.Collections.Generic.IList<Model.InvoiceZZDetail> DetailsIn
        {
            get { return detailsIn; }
            set { detailsIn = value; }
        }
        private System.Collections.Generic.IList<Model.InvoiceZZDetail> detailsOut;

        public System.Collections.Generic.IList<Model.InvoiceZZDetail> DetailsOut
        {
            get { return detailsOut; }
            set { detailsOut = value; }
        }
	}
}
