//------------------------------------------------------------------------------
//
// file name:InvoiceCFDetail.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 货品拆分单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceCFDetail
	{
        public override bool Equals(object obj)
        {
            if (obj is InvoiceCFDetail)
            {
                if ((obj as InvoiceCFDetail).InvoiceCFDetailId == _invoiceCFDetailId)
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
