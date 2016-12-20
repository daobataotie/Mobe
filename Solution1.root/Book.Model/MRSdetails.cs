//------------------------------------------------------------------------------
//
// file name：MRSdetails.cs
// author: peidun
// create date：2009-12-18 11:23:48
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 物料需求计 划明细
    /// </summary>
    [Serializable]
    public partial class MRSdetails
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

        public bool _isChecked;

        public readonly static string PROPERTY_ISCHECKED = "IsChecked";
        public bool IsChecked
        {
            get
            {
                return this._isChecked;
            }
            set
            {
                this._isChecked = value;
            }
        }

        public string ShowSupplier
        {
            get { return this._product == null ? null : (this._product.Supplier == null ? null : this._product.Supplier.ToString()); }
        }

        public string ShowStockQuerty
        {
            get
            {
                return this._product == null ? "0" : (this._product.StocksQuantity.HasValue ? this._product.StocksQuantity.ToString() : "0");
            }
        }

        private bool? checkSign;

        public bool? CheckSign
        {
            get { return checkSign; }
            set { checkSign = value; }
        }

        private double? checkNums;

        /// <summary>
        /// 委外數量
        /// </summary>
        public double? CheckNums
        {
            get { return checkNums; }
            set { checkNums = value; }
        }

        //public string InvoiceXOId
        //{
        //    get { new  m.Get(this.MPSdetailsId); }
        //}
        private string _customerInvoiceXOId;
        /// <summary>
        /// 客户订单编号
        /// </summary>
        public string CustomerInvoiceXOId
        {
            get { return _customerInvoiceXOId; }
            set { _customerInvoiceXOId = value; }
        }
        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        /// <summary>
        /// 操作人
        /// </summary>
        private string _employee0Name;
        public string Employee0Name
        {
            get { return _employee0Name; }
            set { _employee0Name = value; }
        }
        /// <summary>
        /// 制成品
        /// </summary>
        //private string _madeProductName;
        public string MadeProductName
        {
            get;
            set;
        }

        /// <summary>
        /// 商品编号
        /// </summary>
        private string _id;
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
        /// <summary>
        /// 客户型号
        /// </summary>
        private string _customerProductName;
        public string CustomerProductName
        {
            get { return _customerProductName; }
            set { _customerProductName = value; }

        }
        /// <summary>
        /// 客户型号
        /// </summary>
        private string _sourceTypeName;
        public string SourceTypeName
        {
            get { return _sourceTypeName; }
            set { _sourceTypeName = value; }


        }
        private string _productCategoryName;
        public string ProductCategoryName
        {
            get { return _productCategoryName; }
            set { _productCategoryName = value; }


        }
        private DateTime? _mRSstartdate;
        public DateTime? MRSstartdate
        {
            get { return _mRSstartdate; }
            set { _mRSstartdate = value; }


        }
        private double? _stocksQuantity;
        /// <summary>
        /// 临时库存數量 用于明细查询
        /// </summary>
        public double? StocksQuantity
        {
            get { return _stocksQuantity; }
            set { _stocksQuantity = value; }
        }
        /// <summary>
        /// 已分配
        /// </summary>
        private double? _produceDistributioned;
        /// <summary>
        ///临时已分配
        /// </summary>
        public double? ProduceDistributioned
        {
            get { return _produceDistributioned; }
            set { _produceDistributioned = value; }
        }

        /// <summary>
        /// 已定未入
        /// </summary>
        private double? _orderOnWayQuantity;
        /// <summary>
        ///临时 已定未入
        /// </summary>
        public double? OrderOnWayQuantity
        {
            get { return _orderOnWayQuantity; }
            set { _orderOnWayQuantity = value; }
        }

        private string _SupplierShortName;

        public string SupplierShortName
        {
            get { return _SupplierShortName; }
            set { _SupplierShortName = value; }
        }

        /// <summary>
        ///库存数量
        /// </summary>
        public double? SpotStock
        {
            get;
            set;
        }

        public string Id2 { get { return this.Product == null ? null : this.Product.Id; } }

        /// <summary>
        /// 分配数量总计
        /// </summary>
        public double ProductFPSLsum { get; set; }
        public readonly static string PRO_ProductFPSLsum = "ProductFPSLsum";

        public readonly static string PRO_SupplierShortName = "SupplierShortName";

        /// <summary>
        /// 库存数量
        /// </summary>
        public readonly static string PRO_SpotStock = "SpotStock";
    }
}
