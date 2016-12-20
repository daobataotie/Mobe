//------------------------------------------------------------------------------
//
// file name：StockEditorDetal.cs
// author: mayanjun
// create date：2010-11-4 11:02:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 库存盘点录入详细
    /// </summary>
    [Serializable]
    public partial class StockEditorDetal
    {
        public double? GetChazhi
        {
            get
            {
                if (this.StockEditorQuantity == null)
                    this.StockEditorQuantity = 0;
                if (this.StockQuantity == null)
                    this.StockQuantity = 0;
                return this.StockQuantity - this.StockEditorQuantity;
            }

        }
        public readonly static string PROPERTY_GETCHAZHI = "GetChazhi";


        private string _productDesc;
        public string ProductDesc
        {
            get
            {
                return this._productDesc;
            }
            set
            {
                this._productDesc = value;
            }

        }
        private string _productVersion;
        public string ProductVersion
        {
            get
            {
                return this._productVersion;
            }
            set
            {
                this._productVersion = value;
            }

        }

        public string Id { get; set; }
        public string ProductName { get; set; }
        public string CustomerProductName { get; set; }

        public readonly static string PROPERTY_ProductDescription = "ProductDescription";
    }
}
