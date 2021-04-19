using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;

namespace Book.UI.produceManager.PCEarProtectCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.PCEarProtectCheck PCEarProtectCheck)
        {
            InitializeComponent();

            if (PCEarProtectCheck == null)
                return;

            //foreach (var detail in PCClarityCheck.Details)
            //{
            //    foreach (var item in detail.GetType().GetProperties())
            //    {
            //        if (item.Name.Contains("ClarityL") || item.Name.Contains("ClarityR") || item.Name.Contains("NoteIsPass"))
            //        {
            //            switch (item.GetValue(detail, null).ToString())
            //            {
            //                case "0":
            //                    item.SetValue(detail, "√", null);
            //                    break;
            //                case "1":
            //                    item.SetValue(detail, "X", null);
            //                    break;
            //                case "2":
            //                    item.SetValue(detail, "△", null);
            //                    break;
            //            }
            //        }
            //    }
            //}

            this.DataSource = PCEarProtectCheck.Details.OrderBy(d => d.CheckDate).ToList();

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblDataName.Text = "耳护制程坠落测试报告";
            //this.lblPriteDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.lblPCClarityCheckID.Text = PCEarProtectCheck.PCEarProtectCheckId;
            this.lblCheckDate.Text = PCEarProtectCheck.CheckDate.HasValue ? PCEarProtectCheck.CheckDate.Value.ToShortDateString() : "";
            this.lblInvoiceCusXOId.Text = PCEarProtectCheck.InvoiceCusXOId;
            this.lblPronoteHeaderId.Text = PCEarProtectCheck.PronoteHeaderId;
            this.lblCheckStandard.Text = PCEarProtectCheck.CheckStadard;
            this.lblProduct.Text = PCEarProtectCheck.Product == null ? "" : PCEarProtectCheck.Product.ToString();
            this.lblEmployee.Text = PCEarProtectCheck.Employee == null ? "" : PCEarProtectCheck.Employee.ToString();
            this.lblPCCheckCount.Text = PCEarProtectCheck.CheckCount.HasValue ? (PCEarProtectCheck.CheckCount.ToString() + (PCEarProtectCheck.ProductUnit == null ? "" : PCEarProtectCheck.ProductUnit.ToString()) + "/每天") : "";
            this.lblInvoiceXOQuantity.Text = PCEarProtectCheck.InvoiceXOQuantity.HasValue ? PCEarProtectCheck.InvoiceXOQuantity.ToString() : "";
            //this.lblUnit.Text = PCEarProtectCheck.ProductUnit == null ? null : PCEarProtectCheck.ProductUnit.ToString();
            this.lblAuditEmp.Text = PCEarProtectCheck.AuditEmp == null ? null : PCEarProtectCheck.AuditEmp.ToString();
            this.RTNote.Rtf = PCEarProtectCheck.Note;
            //this.lbl_CustomerProductName.Text = PCEarProtectCheck.Product == null ? "" : PCEarProtectCheck.Product.CustomerProductName;
            if (PCEarProtectCheck.Product != null)
            {
                if (string.IsNullOrEmpty(PCEarProtectCheck.Product.CustomerProductName))
                    this.lbl_CustomerProductName.Text =  CommonHelp.GetCustomerProductNameByPronoteHeaderId(PCEarProtectCheck.PronoteHeaderId, PCEarProtectCheck.ProductId);
                else
                    this.lbl_CustomerProductName.Text = PCEarProtectCheck.Product.CustomerProductName;
            }

            this.TCDate.DataBindings.Add("Text", this.DataSource, Model.PCEarProtectCheckDetail.PRO_CheckDate, "{0:yyyy-MM-dd}");
            //this.TCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            this.TCFitted.DataBindings.Add("Text", this.DataSource, Model.PCEarProtectCheckDetail.PRO_Fitted);
            this.TCIsBreak.DataBindings.Add("Text", this.DataSource, Model.PCEarProtectCheckDetail.PRO_IsBreak);
            this.TCIsDrop.DataBindings.Add("Text", this.DataSource, Model.PCEarProtectCheckDetail.PRO_IsDrop);
            this.TCEmployee.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.TCNoteIsPass.DataBindings.Add("Text", this.DataSource, Model.PCClarityCheckDetail.PRO_NoteIsPass);
            //this.TCBusinessHours.DataBindings.Add("Text",this.DataSource,"BusinessHours."+Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
        }
    }
}
