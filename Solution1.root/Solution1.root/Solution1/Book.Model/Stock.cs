//------------------------------------------------------------------------------
//
// file name:Stock.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 库存
    /// </summary>
    [Serializable]
    public partial class Stock
    {

        private double? _stockDiffQuantity;
      
        public double? StockDiffQuantity
        {
            get
            {
                return  _stockDiffQuantity == null ? 0 : _stockDiffQuantity;
            }
            set { _stockDiffQuantity = value; }
        }
        public readonly static string PROPERTY_STOCKDIFFQUANTITY = "StockDiffQuantity";
        private string _depotPositionName;
        /// <summary>
        /// 货位名称
        /// </summary>
        public string DepotPositionName
        {
            get { return _depotPositionName; }
            set { _depotPositionName = value; }
        }
        public readonly static string PROPERTY_DEPOTPOSITIONNAME = "DepotPositionName";

        private string _depotStockCount;
        public string DepotStockCount
        {
            get { return _depotStockCount; }
            set { _depotStockCount = value; }
        }
        private string _customerProductName;
        public string CustomerProductName
        {
            get { return _customerProductName; }
            set { _customerProductName = value; }
        }



        public readonly static string PROPERTY_DEPOTSTOCKCOUNT = "DepotStockCount";

        public readonly static string PROPERTY_GETDESCRIPTION = "GetDescription";

        public string GetDescription
        {
            get { return _product.ProductDescription; }
            set { _product.ProductDescription = value; }
        }

        public string ToPositionAndNums()
        {
            return this.DepotPosition.ToString() + "庫存量(" + this._stockQuantity1+")";
        }

        public double? StockQuantityDiff
        {
            get
            {
                return this._stockQuantityOld == null ? 0 : this._stockQuantityOld - this._stockQuantity1 == null ? 0 : this._stockQuantity1;
            }

        }
        private Model.ProductCategory _ProductCategory;
        public Model.ProductCategory ProductCategory
        {
            get { return _ProductCategory; }
            set { _ProductCategory = value; }
        }

        public readonly static string PROPERTY_stockQuantityDiff = "StockQuantityDiff";



        private double? _oldstock;
        public double? OldStock
        {
            get
            {
                return this._oldstock;
            }
            set { this._oldstock = value; }
        }
        private double? _depotStockQuantity;
        /// <summary>
        /// 庫房库存
        /// </summary>
        public double? DepotStockQuantity
        {
            get { return _depotStockQuantity; }
            set { _depotStockQuantity = value; }
        }

    }
    public class StockSeach
    {
        private string _invoiceType;
        /// <summary>
        /// 订单类别
        /// </summary>
        public string InvoiceType
        {
            get { return _invoiceType; }
            set { _invoiceType = value; }
        }

        private string _invoiceNO;
        /// <summary>
        /// 订单编号
        /// </summary>
        public string InvoiceNO
        {
            get { return _invoiceNO; }
            set { _invoiceNO = value; }
        }
        private DateTime? _invoiceDate;
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? InvoiceDate
        {
            get { return _invoiceDate; }
            set { _invoiceDate = value; }
        }


        private double? _invoiceQuantity;
        /// <summary>
        /// 数量
        /// </summary>
        public double? InvoiceQuantity
        {
            get { return _invoiceQuantity; }
            set { _invoiceQuantity = value; }
        }


        private string _invoiceUnit;
        /// <summary>
        /// 单位
        /// </summary>
        public string InvoiceUnit
        {
            get { return _invoiceUnit; }
            set { _invoiceUnit = value; }
        }
        private string _depotName;
        /// <summary>
        /// 库房
        /// </summary>
        public string DepotName
        {
            get { return _depotName; }
            set { _depotName = value; }
        }
        private string _positionName;
        /// <summary>
        /// 货位名称
        /// </summary>
        public string PositionName
        {
            get { return _positionName; }
            set { _positionName = value; }
        }
        private double? _stock1;
        /// <summary>
        /// 库存
        /// </summary>
        public double? Stock1
        {
            get { return _stock1; }
            set { _stock1 = value; }
        }
        private string _description;
        /// <summary>
        /// 货位名称
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

     
    }
}
