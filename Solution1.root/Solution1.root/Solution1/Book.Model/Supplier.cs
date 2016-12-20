//------------------------------------------------------------------------------
//
// file name：Supplier.cs
// author: peidun
// create date：2009-08-03 9:37:30
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 供应商
	/// </summary>
	[Serializable]
	public partial class Supplier
	{
        private System.Collections.Generic.IList<Model.SupplierContact> contacts = new System.Collections.Generic.List<Model.SupplierContact>();

        public System.Collections.Generic.IList<Model.SupplierContact> Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        public override string ToString()
        {
            return this._supplierShortName;
        }
        private bool _customerCheck;
        public bool customerCheck
        {
            get { return _customerCheck; }
            set
            {
                _customerCheck = value;
            }

        }

	}
}
