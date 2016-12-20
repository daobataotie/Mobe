//------------------------------------------------------------------------------
//
// file name:InvoiceXS.cs
// author: peidun
// create date:2008/6/6 10:00:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 出货单
    /// </summary>
    [Serializable]
    public partial class InvoiceXS : Invoice
    {
        private System.Collections.Generic.IList<Model.InvoiceXSDetail> details = new System.Collections.Generic.List<Model.InvoiceXSDetail>();

        public System.Collections.Generic.IList<Model.InvoiceXSDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private System.Collections.Generic.IList<Model.InvoiceXSDetail> setdetails = new System.Collections.Generic.List<Model.InvoiceXSDetail>();

        public System.Collections.Generic.IList<Model.InvoiceXSDetail> Setdetails
        {
            get { return setdetails; }
            set { setdetails = value; }
        }


        private string kind;

        public string Kind
        {
            get
            {
                switch (this.kind)
                {
                    case "xs":
                        return "銷售";
                    //return Properties.Resource.XS;
                    case "xt":
                        return "銷退";
                    //return Properties.Resource.XT;
                }
                return "";
            }
            set
            {
                this.kind = value;
            }
        }
        /// <summary>
        /// 货别
        /// </summary>
        public readonly static string PROPERTY_KIND = "Kind";

        /// <summary>
        /// 已收
        /// </summary>
        public decimal? YiShou
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// 已收
        /// </summary>
        public readonly static string PROPERTY_YISHOU = "YiShou";

        public override string ToString()
        {
            return this.InvoiceId;
        }

        public bool IsCheck { get; set; }

    }
}
