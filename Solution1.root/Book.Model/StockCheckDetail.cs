//------------------------------------------------------------------------------
//
// file name：StockCheckDetail.cs
// author: mayanjun
// create date：2010-7-30 11:43:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 库存盘点详细
    /// </summary>
    [Serializable]
    public partial class StockCheckDetail
    {
        public double? MinusStockcheck
        {
            get
            {
                double? facenum = _stockCheckQuantity == null ? 0 : _stockCheckQuantity;
                double? stocnum = _stockCheckQuantityOld == null ? 0 : _stockCheckQuantityOld;
                return stocnum - facenum;
            }
        }

        public readonly static string PROPERTY_MINUSSTOCKCHECK = "MinusStockcheck";

        /// <summary>
        /// 差异
        /// </summary>
        public double? StockCheckQuantityDiff
        {
            get { return this._stockCheckBookQuantity - this._stockCheckQuantity; }

        }
        public readonly static string PROPERTY_StockCheckQuantityDiff = "StockCheckQuantityDiff";

        public string ProductVersion
        {
            get { return this.Product == null ? "" : this.Product.ProductVersion; }
        }

        public readonly static string PROPERTY_ProductVersion = "ProductVersion";
    }
}
