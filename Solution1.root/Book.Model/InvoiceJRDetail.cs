//------------------------------------------------------------------------------
//
// file name：InvoiceJRDetail.cs
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
	public partial class InvoiceJRDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceJRDetail)
            {
                if ((obj as InvoiceJRDetail)._invoiceJRDetailId == _invoiceJRDetailId)
                    return true;
            }
            return false;
        }

        private double? _invoiceHCDetailQuantity = 0;

        public double? InvoiceHCDetailQuantity
        {
            get { return _invoiceHCDetailQuantity; }
            set { _invoiceHCDetailQuantity = value; }
        }

        private string _invoiceHCDetailNote;

        public string InvoiceHCDetailNote
        {
            get { return _invoiceHCDetailNote; }
            set { _invoiceHCDetailNote = value; }
        }
	}
}
