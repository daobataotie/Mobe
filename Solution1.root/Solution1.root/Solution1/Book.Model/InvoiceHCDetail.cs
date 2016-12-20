//------------------------------------------------------------------------------
//
// file name：InvoiceHCDetail.cs
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
	public partial class InvoiceHCDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceHCDetail)
            {
                if ((obj as InvoiceHCDetail)._invoiceHCDetailId == _invoiceHCDetailId)
                    return true;
            }
            return false;
        }
	}
}
