//------------------------------------------------------------------------------
//
// file name：PackageType.cs
// author: peidun
// create date：2009-08-12 9:51:49
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 包装类型
	/// </summary>
	[Serializable]
	public partial class PackageType
	{
        public override string ToString()
        {
            return this._packagePypeName;
        }

        private System.Collections.Generic.IList<Model.PackageDetails> _details = new System.Collections.Generic.List<Model.PackageDetails>();

        public System.Collections.Generic.IList<Model.PackageDetails> PruoductsDetails
        {
            get { return _details; }
            set { _details = value; }
        }

        private System.Collections.Generic.IList<Model.PackageTypeCustomer> _customerDetails = new System.Collections.Generic.List<Model.PackageTypeCustomer>();

        public System.Collections.Generic.IList<Model.PackageTypeCustomer> CustomerDetails
        {
            get { return  _customerDetails; }
            set { _customerDetails = value; }
        }
	}
}
