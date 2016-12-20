//------------------------------------------------------------------------------
//
// file name:InvoiceXJ.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 销售报价单
	/// </summary>
	[Serializable]
	public partial class InvoiceXJ : Invoice
	{
        private System.Collections.Generic.IList<Model.InvoiceXJDetail> details;

        public System.Collections.Generic.IList<Model.InvoiceXJDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
        private System.Collections.Generic.IList<Model.InvoiceXJProcess> _detailsprocess=new System.Collections.Generic.List<Model.InvoiceXJProcess>();

        public System.Collections.Generic.IList<Model.InvoiceXJProcess> DetailsProcess
        {
            get { return _detailsprocess; }
            set { _detailsprocess = value; }
        }

	}
}
