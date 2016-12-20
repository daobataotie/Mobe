//------------------------------------------------------------------------------
//
// file name：InvoiceHRDetail.cs
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
	public partial class InvoiceHRDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceHRDetail)
            {
                if ((obj as InvoiceHRDetail)._invoiceHRDetailId == _invoiceHRDetailId)
                    return true;
            }
            return false;
        }
	}
}
