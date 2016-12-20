//------------------------------------------------------------------------------
//
// file name：PronotedetailsMaterial.cs
// author: mayanjun
// create date：2010-9-15 10:11:11
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 生产通知明细配料
	/// </summary>
	[Serializable]
	public partial class PronotedetailsMaterial
	{
        private double? _mpsquantity;
   
        public double? MPSQuantity
        {
            get { return this._mpsquantity; }
            set
            {
                this._mpsquantity = value;
            }
        }

        private double? _quantityTemp;

        public double? QuantityTemp
        {
            get { return this._quantityTemp; }
            set
            {
                this._quantityTemp = value;
            }
        }
        //private string _productDesc;

        public string  ProductDesc
        {
            get { return this._product.ProductDescription; }
           
        }
        private bool _checkeds;

        public bool Checkeds
        {
            get { return this._checkeds; }
            set
            {
                this._checkeds = value;
            }
           
        }
        /// <summary>
        /// 商品描述
        /// </summary>
        public readonly static string PRO_ProductDesc = "ProductDesc";

	}
}
