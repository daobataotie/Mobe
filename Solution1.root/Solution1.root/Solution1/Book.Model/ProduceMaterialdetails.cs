//------------------------------------------------------------------------------
//
// file name：ProduceMaterialdetails.cs
// author: peidun
// create date：2009-12-30 16:33:32
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 加工领料单明细
	/// </summary>
	[Serializable]
	public partial class ProduceMaterialdetails
	{
        private string _productSpecification;
        /// <summary>
        /// 规格型号
        /// </summary>
        public string ProductSpecification
        {
            get
            {
                return this._productSpecification;
            }
            set
            {
                this._productSpecification = value;
            }
        }
        private string productDescription;

        public string ProductDescription
        {
            get 
            {
                return this._product== null ? null : this._product.ProductDescription; 
            }
        }
	}
}
