using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCPenetrateCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.PCPenetrateCheck _pcpc)
        {
            InitializeComponent();
            if (_pcpc == null)
                return;
            //CompanyInfo
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.ChuanTouCheck;
            //this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Details Controls
            this.lblPCPenetrateCheckId.Text = _pcpc.PCPenetrateCheckId;
            this.lblPCPenetrateCheckDate.Text = _pcpc.PCPenetrateCheckDate.HasValue ? _pcpc.PCPenetrateCheckDate.Value.ToString("yyyy-MM-dd") : "";
            this.lblPronoteHeaderId.Text = _pcpc.PronoteHeaderId;
            this.lblProductName.Text = _pcpc.Product == null ? "" : _pcpc.Product.ToString();
            this.lblInvoiceCusXOId.Text = _pcpc.InvoiceCusXOId;
            this.lblInvoiceXOQuantity.Text = _pcpc.InvoiceXOQuantity.HasValue ? _pcpc.InvoiceXOQuantity.ToString() : "0";
            this.lblPCPenetrateCheckQuantity.Text = _pcpc.PCPenetrateCheckQuantity.HasValue ? _pcpc.PCPenetrateCheckQuantity.ToString() : "";
            this.lblPCPenetrateCheckLeftCount.Text = _pcpc.PCPenetrateCheckLeftCount.HasValue ? _pcpc.PCPenetrateCheckLeftCount.ToString() : "0";
            this.lblPCPenetrateCheckCenterCount.Text = _pcpc.PCPenetrateCheckCenterCount.HasValue ? _pcpc.PCPenetrateCheckCenterCount.ToString() : "0";
            this.lblPCPenetrateCheckRightCount.Text = _pcpc.PCPenetrateCheckRightCount.HasValue ? _pcpc.PCPenetrateCheckRightCount.ToString() : "0";
            this.chkIsPassing.Checked = _pcpc.IsPassing.HasValue ? _pcpc.IsPassing.Value : false;
            this.lblPCOpticsCheckDesc.Text = _pcpc.PCPenetrateCheckDesc;
            this.lblEmployee.Text = _pcpc.Employee.ToString();
            this.lbl_ProductUnit.Text = _pcpc.ProductUnit == null ? "" : _pcpc.ProductUnit.ToString();
        }
    }
}
