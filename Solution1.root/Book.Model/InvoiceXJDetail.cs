//------------------------------------------------------------------------------
//
// file name:InvoiceXJDetail.cs
// author: peidun
// create date:2008/6/20 15:33:25
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.Model
{
    /// <summary>
    /// 销售报价单货品
    /// </summary>
    [Serializable]
    public partial class InvoiceXJDetail
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is InvoiceXJDetail)
            {
                if ((obj as InvoiceXJDetail)._invoiceXJDetailId == _invoiceXJDetailId)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 方便从BOM关系导出到报价详细
        /// </summary>
        public string ShamParentID { get; set; }

        public bool IsChecked { get; set; }
    }
}
