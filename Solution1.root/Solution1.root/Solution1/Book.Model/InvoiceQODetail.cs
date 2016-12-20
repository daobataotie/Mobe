//------------------------------------------------------------------------------
//
// file name:InvoiceQODetail.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 其他支出单支出项目
	/// </summary>
	[Serializable]
	public partial class InvoiceQODetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceQODetail)
            {
                if ((obj as InvoiceQODetail)._invoiceQODetailId == _invoiceQODetailId)
                    return true;
            }
            return false;
        }
	}
}
