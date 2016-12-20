//------------------------------------------------------------------------------
//
// file name:InvoiceCG.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 进货单
    /// </summary>
    [Serializable]
    public partial class InvoiceCG : Invoice
    {
        public InvoiceCG()
        {
        }

        private System.Collections.Generic.IList<InvoiceCGDetail> details = new System.Collections.Generic.List<Model.InvoiceCGDetail>();

        public System.Collections.Generic.IList<InvoiceCGDetail> Details
        {
            get { return details; }
            set { details = value; }
        }

        private System.Collections.Generic.IList<InvoiceCGDetail> positionAndNumsSet = new System.Collections.Generic.List<Model.InvoiceCGDetail>();

        public System.Collections.Generic.IList<InvoiceCGDetail> PositionAndNumsSet
        {
            get { return positionAndNumsSet; }
            set { positionAndNumsSet = value; }
        }

        private string kind;

        public string Kind
        {
            get
            {
                switch (this.kind)
                {
                    case "cg":
                        return "採購";
                    //return Properties.Resource.CG;
                    case "ct":
                        return "採退";
                }
                return "";
            }
            set
            {
                this.kind = value;
            }
        }
        private string taxCaluType;

        public string TaxCaluTypeName
        {
            get
            {
                if (this._taxCaluType.HasValue)
                {
                    if (this._taxCaluType.Value == 1)
                        return "外加";
                    else if (this._taxCaluType.Value == 2)
                        return "內含";
                    else
                        return "免稅";
                }
                else
                    return "免稅";
            }

        }
        /// <summary>
        /// 课税名称
        /// </summary>
        public readonly static string PROPERTY_TaxCaluTypeName = "TaxCaluTypeName";
        /// <summary>
        /// 货别
        /// </summary>
        public readonly static string PROPERTY_KIND = "Kind";

        /// <summary>
        /// 已付
        /// </summary>
        public decimal? YiFu
        {
            get
            {
                return 0;
            }
        }
        /// <summary>
        /// 已付
        /// </summary>
        public readonly static string PROPERTY_YIFU = "YiFu";



        //public decimal? hejiQ16
        //{
        //    get
        //    {
        //        return this._invoiceCO.InvoiceHeji;
        //    }
        //}

        public string DetailCOId
        {
            get;
            set;
        }
    }
}
