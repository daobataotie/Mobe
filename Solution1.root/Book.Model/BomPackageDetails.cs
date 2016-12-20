//------------------------------------------------------------------------------
//
// file name：BomPackageDetails.cs
// author: peidun
// create date：2009-11-12 11:03:21
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class BomPackageDetails
	{
        public string ProductDesc
        {
            get
            {
                return this.Product == null ? "" : this.Product.ProductDescription;
            }
        }
        
	}
}
