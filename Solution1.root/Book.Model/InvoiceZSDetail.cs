//------------------------------------------------------------------------------
//
// file name:InvoiceZSDetail.cs
// author: peidun
// create date:2008/6/20 15:33:25
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 赠送单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceZSDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceZSDetail)
            {
                if ((obj as InvoiceZSDetail)._invoiceZSDetailId == _invoiceZSDetailId)
                    return true;
            }
            return false;
        }
	}
}
