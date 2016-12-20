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

        public string ProductDesc
        {
            get { return this._product.ProductDescription; }
        }

        //private string stringInumber;

        //public string StringInumber
        //{
        //    get { return this._inumber.HasValue ? this._inumber.ToString() : stringInumber; }
        //    set { stringInumber = value; }
        //}

        //private string stringPronoteQuantity;

        //public string StringPronoteQuantity
        //{
        //    get { return this._pronoteQuantity.HasValue ? this._pronoteQuantity.ToString() : stringPronoteQuantity; }
        //    set { stringPronoteQuantity = value; }
        //}

        private string stringStocksQuantity;

        public string StringStocksQuantity
        {
            get
            {
                if (this._product == null)
                    return stringStocksQuantity;
                else
                    return this._product.StocksQuantity.HasValue ? this._product.StocksQuantity.ToString() : stringStocksQuantity;
            }
            set { stringStocksQuantity = value; }
        }

        //private string stringJiaoQi;

        //public string StringJiaoQi
        //{
        //    get { return this._jiaoqi.HasValue ? this._jiaoqi.Value.ToString() : stringJiaoQi; }
        //    set { stringJiaoQi = value; }
        //}

        //private string supplierName;

        //public string SupplierName
        //{
        //    get { return supplierName; }
        //    set { supplierName = value; }
        //}

        private bool _checkeds;

        public bool Checkeds
        {
            get { return this._checkeds; }
            set
            {
                this._checkeds = value;
            }
        }

        //private string productName;

        //public string ProductName
        //{
        //    get
        //    {
        //        if (this._product == null)
        //            return productName;
        //        else
        //            return string.IsNullOrEmpty(this._product.ProductName) ? productName : this._product.ProductName;
        //    }
        //    set { productName = value; }
        //}

        private DateTime? _jiaoqi;

        //对应的物料需求的交期
        public DateTime? JiaoQi
        {
            get { return this._jiaoqi; }
            set
            {
                this._jiaoqi = value;
            }
        }
      
        public double? PronoteQuantity1
        {
            get
            {
                double? a = this.PronoteQuantity;
                if (a.HasValue && a.ToString().IndexOf('.') > 0 && (a.ToString().Length - a.ToString().IndexOf('.') > 2) && Convert.ToInt16(a.ToString().Substring(a.ToString().IndexOf('.') + 1, 2)) == 99)
                    a = Math.Ceiling(a.Value);
                return a;
            }
        }

        //public readonly static string PRO_SupplierName = "SupplierName";
        //public readonly static string PRO_ProductName = "ProductName";
        //public readonly static string PRO_StringJiaoQi = "StringJiaoQi";

        public readonly static string PRO_StringStocksQuantity = "StringStocksQuantity";
        //public readonly static string PRO_StringPronoteQuantity = "StringPronoteQuantity";

        /// <summary>
        /// 商品描述
        /// </summary>
        public readonly static string PRO_ProductDesc = "ProductDesc";
        /// <summary>
        /// 交期
        /// </summary>
        public readonly static string PRO_JiaoQi = "JiaoQi";

        //public readonly static string PRO_StringInumber = "StringInumber";

        public double mMaterialprocesedsum { get; set; }

        public readonly static string PRO_mMaterialprocesedsum = "mMaterialprocesedsum";

        public readonly static string PRO_PronoteQuantity1 = "PronoteQuantity1";
    }
}
