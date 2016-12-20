//------------------------------------------------------------------------------
//
// file name：InvoicePIDetail.cs
// author: peidun
// create date：2008-11-29 11:07:05
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class InvoicePIDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoicePIDetail)
            {
                if ((obj as InvoicePIDetail)._invoicePIDetailId == _invoicePIDetailId)
                    return true;
            }
            return false;
        }
	}
}
