//------------------------------------------------------------------------------
//
// file name：InvoicePacking.cs
// author: mayanjun
// create date：2013-1-14 10:58:48
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class InvoicePacking : Invoice
    {
        /// <summary>
        /// 装箱详细
        /// </summary>
        private System.Collections.Generic.IList<Model.InvoicePackingDetail> details = new System.Collections.Generic.List<Model.InvoicePackingDetail>();

        /// <summary>
        /// 装箱详细
        /// </summary>
        public System.Collections.Generic.IList<Model.InvoicePackingDetail> Details
        {
            get { return details; }
            set { details = value; }
        }


        /// <summary>
        /// 唛头
        /// </summary>
        private System.Collections.Generic.IList<Model.CustomerMarks> marks = new System.Collections.Generic.List<Model.CustomerMarks>();

        /// <summary>
        /// 唛头
        /// </summary>
        public System.Collections.Generic.IList<Model.CustomerMarks> Marks
        {
            get { return marks; }
            set { marks = value; }
        }

        private string _customerFullName;

        public string CustomerFullName
        {
            get { return _customerFullName; }
            set { _customerFullName = value; }
        }

    }
}
