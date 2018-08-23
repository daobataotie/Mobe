using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCClarityCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.PCClarityCheck PCClarityCheck)
        {
            InitializeComponent();

            if (PCClarityCheck == null)
                return;

            foreach (var detail in PCClarityCheck.Details)
            {
                foreach (var item in detail.GetType().GetProperties())
                {
                    if (item.Name.Contains("NoteIsPass"))
                    {
                        switch (item.GetValue(detail, null) == null ? null : item.GetValue(detail, null).ToString())
                        {
                            case "0":
                                item.SetValue(detail, "¡Ì", null);
                                break;
                            case "1":
                                item.SetValue(detail, "X", null);
                                break;
                            case "2":
                                item.SetValue(detail, "¡÷", null);
                                break;
                        }
                    }
                }
            }

            this.DataSource = PCClarityCheck.Details.OrderBy(d => d.CheckDate).ToList();

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = Properties.Resources.ClarityReport;
            //this.lblPriteDate.Text += DateTime.Now.ToShortDateString();

            this.lblPCClarityCheckID.Text = PCClarityCheck.PCClarityCheckId;
            this.lblCheckDate.Text = PCClarityCheck.CheckDate.HasValue ? PCClarityCheck.CheckDate.Value.ToShortDateString() : "";
            this.lblInvoiceCusXOId.Text = PCClarityCheck.InvoiceCusXOId;
            this.lblPronoteHeaderId.Text = PCClarityCheck.PronoteHeaderId;
            this.lblCheckStandard.Text = PCClarityCheck.CheckStadard;
            this.lblProduct.Text = PCClarityCheck.Product == null ? "" : PCClarityCheck.Product.ToString();
            this.lblEmployee.Text = PCClarityCheck.Employee == null ? "" : PCClarityCheck.Employee.ToString();
            this.lblPCCheckCount.Text = PCClarityCheck.CheckCount.HasValue ? PCClarityCheck.CheckCount.ToString() : "";
            this.lblInvoiceXOQuantity.Text = PCClarityCheck.InvoiceXOQuantity.HasValue ? PCClarityCheck.InvoiceXOQuantity.ToString() : "";
            this.lblUnit.Text = PCClarityCheck.ProductUnit == null ? null : PCClarityCheck.ProductUnit.ToString();
            this.lblAuditEmp.Text = PCClarityCheck.AuditEmp == null ? null : PCClarityCheck.AuditEmp.ToString();
            this.RTNote.Rtf = PCClarityCheck.Note;
            //this.lbl_CustomerProductName.Text = PCClarityCheck.Product == null ? "" : PCClarityCheck.Product.CustomerProductName;
            if (PCClarityCheck.Product != null)
            {
                if (string.IsNullOrEmpty(PCClarityCheck.Product.CustomerProductName))
                    this.lbl_CustomerProductName.Text = new Help().GetCustomerProductNameByPronoteHeaderId(PCClarityCheck.PronoteHeaderId, PCClarityCheck.ProductId);
                else
                    this.lbl_CustomerProductName.Text = PCClarityCheck.Product.CustomerProductName;
            }

            this.TCDate.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_CheckDate, "{0:yyyy-MM-dd HH:mm:ss}");
            this.TCLeftD1.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_Left1);
            this.TCLeftD2.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_Left2);
            this.TCRightD1.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_Right1);
            this.TCRightD2.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_Right2);
            this.TCAstigmatismL.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_AstigmatismL);
            this.TCAstigmatismR.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_AstigmatismR);
            this.TCSphericaSurfaceL.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_SphericaSurfaceL);
            this.TCSphericaSurfaceR.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_SphericaSurfaceR);
            this.TCClarityL.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_ClarityL);
            this.TCClarityR.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_ClarityR);
            this.TCEmployee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.lblNote.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_NoteIsPass);

            this.xrLBusinessHours.DataBindings.Add("Text", this.DataSource, "BusinessHours." + Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
        }
    }
}
