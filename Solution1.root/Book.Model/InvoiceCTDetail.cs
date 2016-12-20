//------------------------------------------------------------------------------
//
// file name:InvoiceCTDetail.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 进货退货单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceCTDetail
	{
        public override bool Equals(object obj)
        {
            if (obj is InvoiceCTDetail)
            {
                if ((obj as InvoiceCTDetail).InvoiceCTDetailId == _invoiceCTDetailId)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private Model.InvoiceCODetail _invoiceCODetail;

        public Model.InvoiceCODetail InvoiceCODetail
        {
            get
            {
                return this._invoiceCODetail;
            }
            set
            {
                _invoiceCODetail = value;
            }
        }
	}
}
