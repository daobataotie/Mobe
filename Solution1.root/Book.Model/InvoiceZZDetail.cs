//------------------------------------------------------------------------------
//
// file name:InvoiceZZDetail.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 货品组装单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceZZDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceZZDetail)
            {
                if ((obj as InvoiceZZDetail)._invoiceZZDetailId == _invoiceZZDetailId)
                    return true;
            }
            return false;
        }
	}
}
