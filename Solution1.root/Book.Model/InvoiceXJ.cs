//------------------------------------------------------------------------------
//
// file name:InvoiceXJ.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 销售报价单
    /// </summary>
    [Serializable]
    public partial class InvoiceXJ : Invoice
    {
        private IList<Model.InvoiceXJDetail> details;

        /// <summary>
        /// 报价详细
        /// </summary>
        public IList<Model.InvoiceXJDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private IList<Model.InvoiceXJProcess> _detailsprocess = new System.Collections.Generic.List<Model.InvoiceXJProcess>();

        /// <summary>
        /// 报价加工
        /// </summary>
        public IList<Model.InvoiceXJProcess> DetailsProcess
        {
            get { return _detailsprocess; }
            set { _detailsprocess = value; }
        }

        private IList<Model.InvoiceXJPackageDetails> detailPackage;

        /// <summary>
        /// 报价包装
        /// </summary>
        public IList<Model.InvoiceXJPackageDetails> DetailPackage
        {
            get { return detailPackage; }
            set { detailPackage = value; }
        }
    }
}
