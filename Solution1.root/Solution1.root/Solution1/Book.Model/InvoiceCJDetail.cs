//------------------------------------------------------------------------------
//
// file name:InvoiceCJDetail.cs
// author: peidun
// create date:2008/6/20 15:33:25
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 采购询价单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceCJDetail
	{
        public override bool Equals(object obj)
        {
            if (obj is InvoiceCJDetail)
            {
                if ((obj as InvoiceCJDetail).InvoiceCJDetailId == _invoiceCJDetailId)
                    return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
	}
}
