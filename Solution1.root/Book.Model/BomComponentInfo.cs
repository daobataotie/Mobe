//------------------------------------------------------------------------------
//
// file name：BomComponentInfo.cs
// author: peidun
// create date：2009-08-25 17:49:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// BOM子件信息
    /// </summary>
    [Serializable]
    public partial class BomComponentInfo
    {
        private int jibie = 1;
        public int Jibie
        {
            get { return jibie; }
            set { jibie = value; }
        }
        private Model.MPSheader _MPSheader;
        public Model.MPSheader MPSheader
        {
            get { return _MPSheader; }
            set { _MPSheader = value; }
        }

        private string _MPSheaderId;
        public string MPSheaderId
        {
            get { return _MPSheaderId; }
            set { _MPSheaderId = value; }
        }
        private Model.Customer _Customer;
        public Model.Customer Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }
        private string _InvoiceXOId;
        public string InvoiceXOId
        {
            get { return _InvoiceXOId; }
            set { _InvoiceXOId = value; }
        }
        private double? _mrpQuantity;
        public double? MrpQuantity
        {
            get { return _mrpQuantity; }
            set { _mrpQuantity = value; }
        }
        private double? _mpsQuantity;
        public double? MpsQuantity
        {
            get { return _mpsQuantity; }
            set { _mpsQuantity = value; }
        }
        private string _mPSdetailsId;
        public string MPSdetailsId
        {
            get { return _mPSdetailsId; }
            set { _mPSdetailsId = value; }
        }
        private string _bomComponentInfoDesc;
        public string BomComponentInfoDesc
        {
            get { return this._bomComponentInfoDesc; }
            set { this._bomComponentInfoDesc = value; }

        }
        public string ProductDesc
        {
            get
            {
                return this.Product == null ? "" : this.Product.ProductDescription;
            }

        }
        private string _madeProductId;
        public string MadeProductId
        {
            get { return this._madeProductId; }
            set { this._madeProductId = value; }
        }
        private string _beforepPackageProductId;
        public string BeforepPackageProductId
        {
            get { return this._beforepPackageProductId; }
            set { this._beforepPackageProductId = value; }
        }
        private DateTime? _jiaoqi;
        public DateTime? JiaoQi
        {
            get { return _jiaoqi; }
            set { this._jiaoqi = value; }
        }
        //private int? _isChengPinLiao;
        ///// <summary>
        ///// 是否成品的原料 包括包装
        ///// </summary>
        //public DateTime? IsChengPinLiao
        //{
        //    get { return _isChengPinLiao; }
        //    set { this._isChengPinLiao = value; }
        //}

        /// <summary>
        /// 手册号
        /// </summary>
        private string _handbookId;

        /// <summary>
        /// 手册项号
        /// </summary>
        private string _handbookProductId;

        /// <summary>
        /// 手册号
        /// </summary>
        public string HandbookId
        {
            get { return _handbookId; }
            set { _handbookId = value; }
        }

        /// <summary>
        /// 手册项号
        /// </summary>
        public string HandbookProductId
        {
            get { return _handbookProductId; }
            set { _handbookProductId = value; }
        }

        public static readonly string PRO_JiaoQi = "JiaoQi";

        public IList<Model.BomComponentInfo> Details { get; set; }

        /// <summary>
        /// 是否为包材
        /// </summary>
        public bool isBaoCai { get; set; }

        public string ProductName { get; set; }
    }
}
