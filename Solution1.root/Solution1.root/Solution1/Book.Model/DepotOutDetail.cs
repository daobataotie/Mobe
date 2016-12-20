//------------------------------------------------------------------------------
//
// file name：DepotOutDetail.cs
// author: mayanjun
// create date：2010-1 0-15 15:41:13
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 出库详细
	/// </summary>
	[Serializable]


	public partial class DepotOutDetail
	{
        private double? _depotStock;
        public double? DepotStock
        {        
            get{return this._depotStock;}
            set{this._depotStock=value;} 
        }

        public string ProductDescription
        {

            get
            {
                return this._product == null ? "" : this._product.ProductDescription;
            }
        }

        private string depotPositionDesc;

        public string DepotPositionDesc
        {
            get { return depotPositionDesc; }
            set { depotPositionDesc = value; }
        }

        private string stockDesc;

        public string StockDesc
        {
            get { return stockDesc; }
            set { stockDesc = value; }
        }

        public double? SafeStockQuantity
        {
            get
            {
                return _product == null ? 0 : _product.SafeStock;
            }
        }


        public static readonly string Pro_SafeStockQuantity =  "SafeStockQuantity";
	}
}
