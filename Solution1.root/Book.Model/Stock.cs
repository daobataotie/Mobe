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
                return _stockDiffQuantity == null ? 0 : _stockDiffQuantity;
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
            return this.DepotPosition.ToString() + "庫存量(" + this._stockQuantity1 + ")";
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
        /// <summary>
        /// 0 出 1人 2调拨 3盘点
        /// </summary>
        private int _invoiceTypeIndex;
        /// <summary>
        /// 订单类别
        /// </summary>
        public int InvoiceTypeIndex
        {
            get { return _invoiceTypeIndex; }
            set { _invoiceTypeIndex = value; }
        }

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
        private DateTime? _insertTime;
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? InsertTime
        {
            get { return _insertTime; }
            set { _insertTime = value; }
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

        private string _WorkHouseName;

        /// <summary>
        /// 生产站
        /// </summary>
        public string WorkHouseName
        {
            get { return _WorkHouseName; }
            set { _WorkHouseName = value; }
        }

        private string _InvioiceId;
        /// <summary>
        /// 领料单单号--生产领料&委外领料
        /// </summary>
        public string InvioiceId
        {
            get { return _InvioiceId; }
            set { _InvioiceId = value; }
        }

        /// <summary>
        /// 货位名称
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _outdepotName;
        /// <summary>
        /// 库房
        /// </summary>
        public string OutDepotName
        {
            get { return _outdepotName; }
            set { _outdepotName = value; }
        }
        private string _outpositionName;
        /// <summary>
        /// 货位名称
        /// </summary>
        public string OutPositionName
        {
            get { return _outpositionName; }
            set { _outpositionName = value; }
        }
        private double? _stockCheckBookQuantity;
        /// <summary>
        /// 账面数量
        /// </summary>
        public double? StockCheckBookQuantity
        {
            get { return _stockCheckBookQuantity; }
            set { _stockCheckBookQuantity = value; }
        }
        /// <summary>
        /// 客户订单编号
        /// </summary>
        public string CusXOId { get; set; }

        public double? ProduceTransferQuantity { get; set; }

        public string PId { get; set; }

        public string ProductName { get; set; }

        public string ProductCategoryName1 { get; set; }

        public string ProductCategoryName2 { get; set; }

        public string ProductCategoryName3 { get; set; }

        public string PronoteHeaderID { get; set; }

    }
}
