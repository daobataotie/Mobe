//------------------------------------------------------------------------------
//
// file name:InvoicePTDetail.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 调拨单货品
    /// </summary>
    [Serializable]
    public partial class InvoicePTDetail
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoicePTDetail)
            {
                if ((obj as InvoicePTDetail)._invoicePTDetailId == _invoicePTDetailId)
                    return true;
            }
            return false;
        }

        private string _ProDesc;

        public string ProDesc
        {
            get
            {
                if (this.Product == null) return "";
                else return this.Product.ProductDescription;
            }
        }


    }
}
