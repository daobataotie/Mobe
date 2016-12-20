//------------------------------------------------------------------------------
//
// file name:InvoiceHZDetail.cs
// author: peidun
// create date:2008/6/20 15:33:25
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 获赠单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceHZDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceHZDetail)
            {
                if ((obj as InvoiceHZDetail)._invoiceHZDetailId == _invoiceHZDetailId)
                    return true;
            }
            return false;
        }
	}
}
