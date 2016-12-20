//------------------------------------------------------------------------------
//
// file name：InvoiceXJPackageDetails.cs
// author: mayanjun
// create date：2012-8-14 17:05:02
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 报价单包材
    /// </summary>
    [Serializable]
    public partial class InvoiceXJPackageDetails
    {
        public string ProId
        {
            get { return this.Product == null ? "" : this.Product.Id; }
        }

        public string PriceAndRange { get; set; }

        public bool IsChecked { get; set; }
    }
}
