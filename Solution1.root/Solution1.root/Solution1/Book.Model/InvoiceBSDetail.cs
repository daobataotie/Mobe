//------------------------------------------------------------------------------
//
// file name:InvoiceBSDetail.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 报损单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceBSDetail
	{
        public override bool Equals(object obj)
        {
            if (obj is InvoiceBSDetail) 
            {
                if ((obj as InvoiceBSDetail).InvoiceBSDetailId == _invoiceBSDetailId)
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
