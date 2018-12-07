//------------------------------------------------------------------------------
//
// file name:InvoiceXODetail.cs
// author: peidun
// create date:2008/6/20 15:33:25
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 销售订单货品
    /// </summary>
    [Serializable]
    public partial class InvoiceXODetail
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceXODetail)
            {
                if ((obj as InvoiceXODetail)._invoiceXODetailId == _invoiceXODetailId)
                    return true;
            }
            return false;
        }

        private string _Id;
        public string Id
        {
            get
            {

                return this.PrimaryKey == null ? null : this.PrimaryKey.CustomerProductId;
            }
            set { _Id = value; }
        }

        public DateTime OrderDate
        {
            get
            {
                return this.Invoice.InvoiceDate.Value;
            }
        }

        public double? Stock
        {
            get { return this._product.StocksQuantity; }
        }

        public readonly static string PRO_OId = "OId";
        private int oId;
        public int OId
        {
            get { return oId; }
            set { oId = value; }
        }

        private bool _checked;
        public bool Checked
        {
            get { return this._checked; }
            set { this._checked = value; }
        }

        public string CusXOId { get { return this.Invoice == null ? "" : this.Invoice.CustomerInvoiceXOId; } }

        /// <summary>
        /// 手册商品
        /// </summary>
        public BGHandbookDetail1 BGProduct
        {
            get;
            set;
        }

        public DateTime InvoiceDate { get; set; }

        public DateTime InvoiceYjrq { get; set; }

        public string CustomerInvoiceXOId { get; set; }

        public string CustomerName { get; set; }

        public string XOCustomerName { get; set; }

        public string ProductName { get; set; }

        public string CustomerProductName { get; set; }

        public string DepotName { get; set; }

        /// <summary>
        /// 出货日期
        /// </summary>
        public DateTime? InvoiceXSDate { get; set; }

        public readonly static string PRO_InvoiceDate = "InvoiceDate";

        public readonly static string PRO_InvoiceYjrq = "InvoiceYjrq";

        public readonly static string PRO_CustomerInvoiceXOId = "CustomerInvoiceXOId";

        public readonly static string PRO_CustomerName = "CustomerName";

        public readonly static string PRO_XOCustomerName = "XOCustomerName";

        public readonly static string PRO_ProductName = "ProductName";

        public readonly static string PRO_CustomerProductName = "CustomerProductName";

        public readonly static string PRO_InvoiceXSDate = "InvoiceXSDate";
    }
}
