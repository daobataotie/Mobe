//------------------------------------------------------------------------------
//
// file name：Pronotedetails.cs
// author: peidun
// create date：2009-12-29 11:58:41
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 生产通知明细
	/// </summary>
	[Serializable]
	public partial class Pronotedetails
	{
        private string _productSpecification;
        private double? _mPSQuantity;
        private double? _quantityTemp;
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
        public  double? MPSQuantity
        {

            get { return this._mPSQuantity; }
            set
            {
                this._mPSQuantity = value;
            }
        }

        public double? QuantityTemp
        {

            get { return this._quantityTemp; }
            set
            {
                this._quantityTemp = value;
            }
        }
	}
}
