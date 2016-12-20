//------------------------------------------------------------------------------
//
// file name：PackageCustomerDetails.cs
// author: peidun
// create date：2009-11-11 9:52:39
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class PackageCustomerDetails
	{
        private Model.Product _product;
        public Model.Product Product
        {
            get { return _product; }
            set { _product = value; }
        }
	}
}
