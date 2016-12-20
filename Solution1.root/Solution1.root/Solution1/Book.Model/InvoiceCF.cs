//------------------------------------------------------------------------------
//
// file name:InvoiceCF.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 货品拆分单
	/// </summary>
	[Serializable]
	public partial class InvoiceCF : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceCFDetail> detailsIn;

        public System.Collections.Generic.IList<Model.InvoiceCFDetail> DetailsIn
        {
            get { return detailsIn; }
            set { detailsIn = value; }
        }
        private System.Collections.Generic.IList<Model.InvoiceCFDetail> detailsOut;

        public System.Collections.Generic.IList<Model.InvoiceCFDetail> DetailsOut
        {
            get { return detailsOut; }
            set { detailsOut = value; }
        }
	}
}
