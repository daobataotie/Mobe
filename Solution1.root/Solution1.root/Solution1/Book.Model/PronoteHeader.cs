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
	public partial class 
        PronoteHeader
	{
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
            get { return  _pronoteProceduresDetail; }
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
        public readonly static string PRO_CustomerInvoiceXOId = "CustomerInvoiceXOId";
        public readonly static string PRO_CustomerShortName = "CustomerShortName";
	}
}
