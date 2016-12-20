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
            get {             

                    return this.PrimaryKey==null?null:this.PrimaryKey.CustomerProductId;              
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
	}
}
