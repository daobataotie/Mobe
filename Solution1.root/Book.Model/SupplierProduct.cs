//------------------------------------------------------------------------------
//
// file name：SupplierProduct.cs
// author: mayanjun
// create date：2012-8-30 17:02:26
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 厂商商品对照
    /// </summary>
    [Serializable]
    public partial class SupplierProduct
    {
        public string ProductDesc { get { return this.Product == null ? "" : this.Product.ProductDescription; } }

        public string ProductIDNo { get { return this.Product == null ? "" : this.Product.Id; } }

        public string CustomerProductName { get { return this.Product == null ? "" : this.Product.CustomerProductName; } }

        /// <summary>
        /// 对照商品类别{外購,耗用,委外,自製,半成品加工,}
        /// </summary>
        public string ProductType
        {
            get
            {
                if (this.Product.OutSourcing.HasValue && this.Product.OutSourcing.Value)
                    return "外購";
                if (this.Product.Consume.HasValue && this.Product.Consume.Value)
                    return "耗用";
                if (this.Product.TrustOut.HasValue && this.Product.TrustOut.Value)
                    return "委外";
                if (this.Product.HomeMade.HasValue && this.Product.HomeMade.Value)
                    return "自製";
                if (this.Product.IsProcee.HasValue && this.Product.IsProcee.Value)
                    return "半成品加工";

                return "";
            }
        }

        public bool? IsNewTemp { get; set; }
    }
}
