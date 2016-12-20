//------------------------------------------------------------------------------
//
// file name：InvoiceZX.cs
// author: mayanjun
// create date：2012-10-29 14:32:20
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 出货装箱
    /// </summary>
    [Serializable]
    public partial class InvoiceZX
    {
        private System.Collections.Generic.IList<Model.InvoiceZXDetail> details = new System.Collections.Generic.List<Model.InvoiceZXDetail>();

        public System.Collections.Generic.IList<Model.InvoiceZXDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private double productNum;

        public double ProductNum
        {
            get { return productNum; }
            set { productNum = value; }
        }

        private double boxNum;

        public double BoxNum
        {
            get { return boxNum; }
            set { boxNum = value; }
        }

        /// <summary>
        /// 序列
        /// </summary>
        public string Sequence { get; set; }

        public string ParentInvoceZXId { get; set; }

        public bool IsChecked { get; set; }
    }
}
