using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCEarPressCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.PCEarPressCheck PCEarPressCheck)
        {
            InitializeComponent();
            if (PCEarPressCheck == null)
                return;


            this.DataSource = PCEarPressCheck.Details.OrderBy(d => d.CheckDate).ToList();
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = "耳压制程测试报告";
            //this.lblPrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            lblPCEarPressCheckId.Text = PCEarPressCheck.PCEarPressCheckId;
            lblCheckCount.Text = PCEarPressCheck.PCEarPressCheckCount.HasValue ? (PCEarPressCheck.PCEarPressCheckCount.ToString() + (PCEarPressCheck.ProductUnit == null ? "" : PCEarPressCheck.ProductUnit.ToString()) + "/每天") : "";
            lblCheckDate.Text = PCEarPressCheck.PCEarPressCheckDate.HasValue ? PCEarPressCheck.PCEarPressCheckDate.Value.ToShortDateString() : "";
            lblCheckStandard.Text = PCEarPressCheck.PCEarPressCheckStandard;
            lblInvoiceXOQuantity.Text = PCEarPressCheck.InvoiceXOQuantity.HasValue ? PCEarPressCheck.InvoiceXOQuantity.ToString() : "";
            lblInvpiceCusXOId.Text = PCEarPressCheck.InvoiceCusXOId;
            lblProduct.Text = PCEarPressCheck.Product == null ? "" : PCEarPressCheck.Product.ToString();
            lblPronoterHeaderId.Text = PCEarPressCheck.PronoteHeaderId;
            //lblUnit.Text = PCEarPressCheck.ProductUnit == null ? null : PCEarPressCheck.ProductUnit.ToString();
            lblAuditEmp.Text = PCEarPressCheck.AuditEmp == null ? null : PCEarPressCheck.AuditEmp.ToString();
            lblEmployee.Text = PCEarPressCheck.Employee == null ? "" : PCEarPressCheck.Employee.ToString();
            RTNote.Rtf = PCEarPressCheck.Note;
            //this.lbl_CustomerProductName.Text = PCEarPressCheck.Product == null ? "" : PCEarPressCheck.Product.CustomerProductName;
            if (PCEarPressCheck.Product != null)
            {
                if (string.IsNullOrEmpty(PCEarPressCheck.Product.CustomerProductName))
                    this.lbl_CustomerProductName.Text = new Help().GetCustomerProductNameByPronoteHeaderId(PCEarPressCheck.PronoteHeaderId, PCEarPressCheck.ProductId);
                else
                    this.lbl_CustomerProductName.Text = PCEarPressCheck.Product.CustomerProductName;
            }

            TCDate.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckDate, "{0:yyyy-MM-dd}");
            TCHeadBound.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_HeadBand);
            TCTimeFir.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckTime, "{0:hh:mm}");
            TCTimeSec.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckTimeSec, "{0:hh:mm}");
            TCPressFir.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckPress);
            TCPressSec.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckPressSec);
            TCBoundFir.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckBound);
            TCBoundSec.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckBoundSec);
            TCEmployee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            TCNote.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_NoteIsPass);
            CTimeFirStandard.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckStandard);
            CTimeFSecStandard.DataBindings.Add("Text", this.DataSource, Model.PCEarPressCheckDetail.PRO_CheckStandardW);
            this.xrLBusinessHours.DataBindings.Add("Text", this.DataSource, "BusinessHours." + Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);


        }

    }
}
