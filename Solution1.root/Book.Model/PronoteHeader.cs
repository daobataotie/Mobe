//------------------------------------------------------------------------------
//
// file name：PronoteHeader.cs
// author: peidun
// create date：2009-12-29 11:58:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 生产通知头
    /// </summary>
    [Serializable]
    public partial class PronoteHeader
    {
        public override string ToString()
        {
            return this._pronoteHeaderID;
        }

        //  private System.Collections.Generic.IList<Pronotedetails> details;
        private System.Collections.Generic.IList<Model.PronotedetailsMaterial> _detailsMaterial = new System.Collections.Generic.List<Model.PronotedetailsMaterial>();
        private System.Collections.Generic.IList<Model.PronoteProceduresDetail> _pronoteProceduresDetail = new System.Collections.Generic.List<Model.PronoteProceduresDetail>();
        private IList<Model.ProceduresMachine> proceduresMachineDetail = new List<Model.ProceduresMachine>();

        //public System.Collections.Generic.IList<Pronotedetails> Details
        //{
        //    get { return details; }
        //    set { details = value; }
        //}
        public System.Collections.Generic.IList<Model.PronotedetailsMaterial> DetailsMaterial
        {
            get { return _detailsMaterial; }
            set { _detailsMaterial = value; }
        }
        public System.Collections.Generic.IList<Model.PronoteProceduresDetail> DetailProcedures
        {
            get { return _pronoteProceduresDetail; }
            set { _pronoteProceduresDetail = value; }
        }


        public IList<Model.ProceduresMachine> ProceduresMachineDetail
        {
            get { return proceduresMachineDetail; }
            set { proceduresMachineDetail = value; }
        }

        private string customerShortName;

        public string CustomerShortName
        {
            get { return customerShortName; }
            set { customerShortName = value; }
        }
        private string customerInvoiceXOId;

        public string CustomerInvoiceXOId
        {
            get { return customerInvoiceXOId; }
            set { customerInvoiceXOId = value; }
        }
        private string _customerProductName;
        public string CustomerProductName
        {
            get { return _customerProductName; }
            set { this._customerProductName = value; }
        }
        private string _id;
        public string Id
        {
            get { return _id; }
            set { this._id = value; }
        }
        private string _productName;
        public string ProductName
        {
            get { return this._productName; }
            set { this._productName = value; }
        }

        private string _InvoiceId;

        public string InvoiceId
        {
            get { return _InvoiceId; }
            set { _InvoiceId = value; }
        }

        private string _CustomerCheckStandard;

        public string CustomerCheckStandard
        {
            get { return _CustomerCheckStandard; }
            set { _CustomerCheckStandard = value; }
        }
        private string _productDesc;

        public string ProductDesc
        {
            get { return _productDesc; }
            set { _productDesc = value; }
        }
        private Model.InvoiceXO _invoicexo;

        public Model.InvoiceXO InvoiceXO
        {
            get { return this._invoicexo; }
            set
            {
                this._invoicexo = value;
            }
        }
        private string _workhousename;

        public string Workhousename
        {
            get { return _workhousename; }
            set { _workhousename = value; }
        }
        private Model.MRSdetails _mrsdetail;

        public Model.MRSdetails MRSDetails
        {
            get { return this._mrsdetail; }
            set
            {
                this._mrsdetail = value;
            }
        }
        private string _employee0Name;

        public string Employee0Name
        {
            get { return _employee0Name; }
            set { _employee0Name = value; }
        }

        private double? _produceTransferQuantity;
        public double? ProduceTransferQuantity
        {
            get { return this._produceTransferQuantity; }
            set { this._produceTransferQuantity = value; }
        }

        private string _workHousenextName;
        public string WorkHouseNextName
        {
            get { return this._workHousenextName; }
            set { this._workHousenextName = value; }
        }
        private DateTime? _pronoteProceduresDate;
        public DateTime? PronoteProceduresDate
        {
            get { return this._pronoteProceduresDate; }
            set { this._pronoteProceduresDate = value; }
        }
        private double? _heJiCheckOutSum;
        public double? HeJiCheckOutSum
        {
            get { return this._heJiCheckOutSum; }
            set { this._heJiCheckOutSum = value; }
        }

        /// <summary>
        /// 当前部门合计生产数量
        /// </summary>
        public double? HeJiProceduresSum { get; set; }

        //计划数量
        public double? Plannum { get; set; }

        public string MaterialId { get; set; }

        public string MaterialName { get; set; }

        public double? MaterialQty { get; set; }

        public readonly static string PRO_CustomerCheckStandard = "CustomerCheckStandard";
        public readonly static string PRO_InvoiceId = "InvoiceId";
        public readonly static string PRO_CustomerInvoiceXOId = "CustomerInvoiceXOId";
        public readonly static string PRO_CustomerShortName = "CustomerShortName";
        public readonly static string PRO_Plannum = "Plannum";
    }
}
