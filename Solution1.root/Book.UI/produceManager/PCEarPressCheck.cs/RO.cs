using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCEarPressCheck.cs
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.PCEarPressCheck PCEarPressCheck)
        {
            InitializeComponent();
            if (PCEarPressCheck== null)
                return;


            this.DataSource = PCEarPressCheck.Details;
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportName.Text = Properties.Resources.ClarityReport;
            this.lblPrintDate.Text += DateTime.Now.ToLongDateString();

            lblPCEarPressCheckId.Text = PCEarPressCheck.PCEarPressCheckId;
            lblCheckCount.Text = PCEarPressCheck.PCEarPressCheckCount.HasValue?PCEarPressCheck.PCEarPressCheckCount.ToString():"";
            lblCheckDate.Text = PCEarPressCheck.PCEarPressCheckDate.HasValue ? PCEarPressCheck.PCEarPressCheckDate.Value.ToShortDateString() : "";
            lblCheckStandard.Text = PCEarPressCheck.PCEarPressCheckStandard;
            lblInvoiceXOQuantity.Text = PCEarPressCheck.InvoiceXOQuantity.HasValue?PCEarPressCheck.InvoiceXOQuantity.ToString():"";
            lblInvpiceCusXOId.Text = PCEarPressCheck.InvoiceCusXOId;
            lblProduct.Text = PCEarPressCheck.Product==null ? "" : PCEarPressCheck.Product.ToString();
            lblPronoterHeaderId.Text = PCEarPressCheck.PronoteHeaderId;
            lblUnit.Text = PCEarPressCheck.ProductUnit==null? "":PCEarPressCheck.ProductUnit.ToString();
            lblAuditEmp.Text = PCEarPressCheck.AuditEmp == null ? "" : PCEarPressCheck.AuditEmp.ToString();
            lblEmployee.Text = PCEarPressCheck.Employee == null ? "" : PCEarPressCheck.Employee.ToString();
            RTNote.Text = PCEarPressCheck.Note;


            TCDate.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_CheckDate,"{0:yyyy-mm-dd hh:mm:ss}");
            TCHeadBound.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_HeadBand);
            TCTimeFir.DataBindings.Add("Text",this.DataSource,Model .PCEarPressCheckDetail.PRO_CheckTime);
            TCTimeSec.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_CheckTimeSec);
            CTimeFirStandard.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_CheckStandard);
            CTimeSecStangard.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_CheckStandardW);
            TCPressFir.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_CheckPress);
            TCPressSec.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_CheckPressSec);
            TCBoundFir.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_CheckBound);
            TCBoundSec.DataBindings.Add("Text",this.DataSource,Model.PCEarPressCheckDetail.PRO_CheckBoundSec);
            TCEmployee.DataBindings.Add("Text",this.DataSource,"Employee"+Model.Employee.PROPERTY_EMPLOYEENAME);
            this.TCNote.DataBindings.Add("Text",this.DataSource ,Model.PCEarPressCheckDetail.PRO_NoteIsPass);



            
        }

        

    }
}
