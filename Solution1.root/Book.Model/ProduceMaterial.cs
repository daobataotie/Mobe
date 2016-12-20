//------------------------------------------------------------------------------
//
// file name：ProduceMaterial.cs
// author: peidun
// create date：2009-12-30 16:33:32
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 加工领料单头
    /// </summary>
    [Serializable]
    public partial class ProduceMaterial
    {
        private System.Collections.Generic.IList<ProduceMaterialdetails> details = new System.Collections.Generic.List<Model.ProduceMaterialdetails>();

        public System.Collections.Generic.IList<ProduceMaterialdetails> Details
        {
            get { return details; }
            set { details = value; }
        }
        private string _cusxoid;
        public string CusXOId
        {
            get { return _cusxoid; }
            set { _cusxoid = value; }
        }
        private string _parenProductName;
        public string ParenProductName
        {
            get { return _parenProductName; }
            set { _parenProductName = value; }
        }
        private Model.InvoiceXO _invoicexo;
        public Model.InvoiceXO InvoiceXO
        {
            get { return this._invoicexo; }

            set { this._invoicexo = value; }

        }
        private Model.PronoteHeader _pronoteHeader;
        public Model.PronoteHeader PronoteHeader
        {
            get { return this._pronoteHeader; }

            set { this._pronoteHeader = value; }

        }
        public string SourceTypeName
        {
            get
            {
                if (this.SourceType == 1)
                    return "需求計劃";
                else return "生產加工";
            }

        }

        private string _WorkhouseName;

        public string WorkhouseName
        {
            get { return _WorkhouseName; }
            set { _WorkhouseName = value; }
        }

        private string _Employee0Name;

        public string Employee0Name
        {
            get { return _Employee0Name; }
            set { _Employee0Name = value; }
        }

        private string _Employee1Name;

        public string Employee1Name
        {
            get { return _Employee1Name; }
            set { _Employee1Name = value; }
        }

        private string _Employee2Name;

        public string Employee2Name
        {
            get { return _Employee2Name; }
            set { _Employee2Name = value; }
        }
        public string CustomerInvoiceXOId { get; set; }

        public static readonly string PRO_ParenProductName = "ParenProductName";

        public static readonly string PRO_WorkhouseName = "WorkhouseName";

    }
}
