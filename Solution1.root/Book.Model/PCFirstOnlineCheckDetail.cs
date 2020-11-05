//------------------------------------------------------------------------------
//
// file name：PCFirstOnlineCheckDetail.cs
// author: mayanjun
// create date：2020/10/30 22:05:32
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 首件上线检查表明细(新)
    /// </summary>
    [Serializable]
    public partial class PCFirstOnlineCheckDetail
    {
        public string InvoiceXOCusId { get; set; }

        private string _productName;

        public string ProductName
        {
            get
            {
                if (this.Product != null)
                    return this.Product.ProductName;
                else
                    return _productName;
            }
            set { _productName = value; }
        }

        //public string ProductName { get; set; }

        public readonly static string PRO_InvoiceXOCusId = "InvoiceXOCusId";
    }
}