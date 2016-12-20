//------------------------------------------------------------------------------
//
// file name:InvoiceBYDetail.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 报溢单货品
	/// </summary>
	[Serializable]
	public partial class InvoiceBYDetail
	{
        public override bool Equals(object obj)
        {
            if (obj is InvoiceBYDetail) 
            {
                if ((obj as InvoiceBYDetail)._invoiceBYDetailId == _invoiceBYDetailId) 
                {
                    return true;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
	}
}
