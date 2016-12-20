//------------------------------------------------------------------------------
//
// file name:InvoiceXTDetail.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 出货单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceXTDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceXTDetail)
            {
                if ((obj as InvoiceXTDetail)._invoiceXTDetailId == _invoiceXTDetailId)
                    return true;
            }
            return false;
        }
	}
}
