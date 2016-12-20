//------------------------------------------------------------------------------
//
// file name：InvoicePODetail.cs
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
	public partial class InvoicePODetail
	{
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoicePODetail)
            {
                if ((obj as InvoicePODetail)._invoicePODetailId == _invoicePODetailId)
                    return true;
            }
            return false;
        }

        private double? _invoicePIDetailQuantity = 0;

        public double? InvoicePIDetailQuantity
        {
            get
            {
                return this._invoicePIDetailQuantity;
            }
            set
            {
                this._invoicePIDetailQuantity = value;
            }
        }


        private string _invoicePIDetailNote = "";

        public string InvoicePIDetailNote
        {
            get
            {
                return this._invoicePIDetailNote;
            }
            set
            {
                this._invoicePIDetailNote = value;
            }
        }

	}
}
