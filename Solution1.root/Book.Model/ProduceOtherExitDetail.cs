//------------------------------------------------------------------------------
//
// file name：ProduceOtherExitDetail.cs
// author: peidun
// create date：2010-1-6 10:20:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 外包退料详细
    /// </summary>
    [Serializable]
    public partial class ProduceOtherExitDetail
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

        private bool isChecked = true;

        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        private double noRepayQuantity;

        public double NoRepayQuantity
        {
            get
            {
                return (this._produceQuantity == null ? 0 : this._produceQuantity.Value) - (this._produceRepayQuantity == null ? 0 : this._produceRepayQuantity.Value);
            }
        }
    }
}
