//------------------------------------------------------------------------------
//
// file name：SupplierProcesscategory.cs
// author: mayanjun
// create date：2012-8-30 17:02:25
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 厂商加工对照表
    /// </summary>
    [Serializable]
    public partial class SupplierProcesscategory
    {
        public bool? IsNewTemp { get; set; }

        public string ProductDesc { get { return this.Product == null ? "" : this.Product.ProductDescription; } }

        public string ProductIDNo { get { return this.Product == null ? "" : this.Product.Id; } }

        public string CustomerProductName
        {
            get
            {
                return this.Product == null ? "" : this.Product.CustomerProductName;
            }
        }
    }
}
