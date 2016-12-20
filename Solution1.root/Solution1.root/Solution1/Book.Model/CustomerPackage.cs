//------------------------------------------------------------------------------
//
// file name：CustomerPackage.cs
// author: peidun
// create date：2010-2-4 11:15:14
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 客户包装
	/// </summary>
	[Serializable]
	public partial class CustomerPackage
	{
        private System.Collections.Generic.IList<Model.CustomerPackageDetail> _detail = new System.Collections.Generic.List<Model.CustomerPackageDetail>();
        public System.Collections.Generic.IList<Model.CustomerPackageDetail> detail
        {
            get { return _detail; }
            set { _detail = value; }

        }
        public override string ToString()
        {
            return Id;
        }
     

	}
}
