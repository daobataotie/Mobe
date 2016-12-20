//------------------------------------------------------------------------------
//
// file name：CustomerProductPrice.cs
// author: mayanjun
// create date：2013-3-8 16:09:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 客户产品价格
    /// </summary>
    [Serializable]
    public partial class CustomerProductPrice
    {
        //public string ProductDesc { get { return this.Product == null ? "" : this.Product.ProductDescription; } }

        //public string ProductIDNo { get { return this.Product == null ? "" : this.Product.Id; } }

        //public string ProductVersion { get { return this.CustomerProducts == null ? "" : this.CustomerProducts.Version; } }

        //public string CustomerProductId { get { return this.CustomerProducts == null ? "" : this.CustomerProducts.CustomerProductId; } }
        public string ProductDesc { get; set; }

        public string ProductIDNo { get; set; }

        public string ProductVersion { get; set; }

        public string CustomerProductId { get; set; }

        public string ProductName { get; set; }

    }
}
