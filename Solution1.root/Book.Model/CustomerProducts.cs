//------------------------------------------------------------------------------
//
// file name：CustomerProducts.cs
// author: peidun
// create date：2009-09-14 下午 05:25:52
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 客户产品
	/// </summary> 
	[Serializable]
	public partial class CustomerProducts
	{
        private System.Collections.Generic.IList<Model.CustomerProductProcess> customerProductProcessList = new System.Collections.Generic.List<Model.CustomerProductProcess>();

        public System.Collections.Generic.IList<Model.CustomerProductProcess> CustomerProductProcessList
        {
            get { return customerProductProcessList; }
            set { customerProductProcessList = value; }
        }


        //private System.Collections.Generic.IList<Model.CustomerProductsBom> customerProductsBomInfos = new System.Collections.Generic.List<Model.CustomerProductsBom>();

        //public System.Collections.Generic.IList<Model.CustomerProductsBom> CustomerProductsBomInfos
        //{
        //    get { return customerProductsBomInfos; }
        //    set { customerProductsBomInfos = value; }
        //}
       //private System.Collections.Generic.IList<Model.PackageCustomerDetails> packageCustomerDetails;
         private System.Collections.Generic.IList<Model.PackageCustomerDetails> packageCustomerDetails = new System.Collections.Generic.List<Model.PackageCustomerDetails>();

        public System.Collections.Generic.IList<Model.PackageCustomerDetails> PackageCustomerDetails
        {
            get { return packageCustomerDetails; }
            set { packageCustomerDetails = value; }
        }

        public override string ToString()
        {
            return _customerProductName;
        }
        private string _xoprice;
        /// <summary>
        /// 销售单价
        /// </summary>
        public string XOPrice
        {
            get { return this._xoprice; }
            set { this._xoprice = value; }
        }


       

	}
}
