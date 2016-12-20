//------------------------------------------------------------------------------
//
// file name：InvoiceJCDetail.cs
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
	public partial class InvoiceJCDetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceJCDetail)
            {
                if ((obj as InvoiceJCDetail)._invoiceJCDetailId == _invoiceJCDetailId)
                    return true;
            }
            return false;
        }
        private double? _invoiceHRDetailQuantity = 0;

        public double? InvoiceHRDetailQuantity
        {
            get { return _invoiceHRDetailQuantity; }
            set { _invoiceHRDetailQuantity = value; }
        }     

        private string _invoiceHRDetailNote;

        public string InvoiceHRDetailNote
        {
            get { return _invoiceHRDetailNote; }
            set { _invoiceHRDetailNote = value; }
        }

	}
}
