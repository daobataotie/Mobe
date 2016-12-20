//------------------------------------------------------------------------------
//
// file name:InvoiceQIDetail.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 其他收入单收入项目
	/// </summary>
	[Serializable]
	public partial class InvoiceQIDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceQIDetail)
            {
                if ((obj as InvoiceQIDetail)._invoiceQIDetailId == _invoiceQIDetailId)
                    return true;
            }
            return false;
        }
	}
}
