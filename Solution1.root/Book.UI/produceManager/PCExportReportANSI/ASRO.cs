using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class ASRO : DevExpress.XtraReports.UI.XtraReport
    {
        public ASRO()
        {
            InitializeComponent();
        }

        public ASRO(Model.PCExportReportANSI _PCExportReportANSI, int tag)
            : this()
        {
            if (tag == 1)
            {
                this.xrLabel1.Text = "ALAN    SAFETY    COMPANY.";

                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ALANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ALANSignature")[0].SettingCurrentValue;
            }
            else if (tag == 2)
            {
                this.xrLabel1.Text = "PPE   SAFETY   INC.";
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("PPESignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("PPESignature")[0].SettingCurrentValue;
            }
            else
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ASWANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ASWANSignature")[0].SettingCurrentValue;
            this.xrLabel2.Text = "ASSEMBLED EYE PROTECTORS-QUALITY CONTROL TESE TEPORT";
            this.LbModelNo.Text = _PCExportReportANSI.Product == null ? null : _PCExportReportANSI.Product.CustomerProductName;
            //this.LbTestDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToShortDateString();
            this.LbTestDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToString("dd-MMM-yy", new System.Globalization.CultureInfo("en-us"));
            this.LbQtyTest.Text = (_PCExportReportANSI.QuYangShu2.HasValue ? _PCExportReportANSI.QuYangShu2.Value.ToString() : "") + "PCS";
            this.LbBuyer.Text += "   " + (_PCExportReportANSI.Customer == null ? null : _PCExportReportANSI.Customer.CustomerName);
            this.LbOrderNo.Text += "   " + (_PCExportReportANSI.InvoiceCusXOId == null ? null : _PCExportReportANSI.InvoiceCusXOId.ToString());
            this.lblOrderQty.Text += "   " + (_PCExportReportANSI.Amount.HasValue ? _PCExportReportANSI.Amount.Value.ToString("0.##")  : "")+ "PCS";
            this.LbBatchNo.Text = _PCExportReportANSI.ProductBatchNo == null ? null : _PCExportReportANSI.ProductBatchNo.ToString();
            this.CheckVisual.Checked = _PCExportReportANSI.VisualTest.HasValue ? _PCExportReportANSI.VisualTest.Value : false;
            this.CheckThermal.Checked = _PCExportReportANSI.ThermalStability.HasValue ? _PCExportReportANSI.ThermalStability.Value : false;
            this.CheckPriHIn.Checked = _PCExportReportANSI.PrismaticPowerHIn.HasValue ? _PCExportReportANSI.PrismaticPowerHIn.Value : false;
            this.CheckPriHOut.Checked = _PCExportReportANSI.PrismaticPowerHOut.HasValue ? _PCExportReportANSI.PrismaticPowerHOut.Value : false;
            this.CheckPriVUp.Checked = _PCExportReportANSI.PrismaticPowerVUp.HasValue ? _PCExportReportANSI.PrismaticPowerVUp.Value : false;
            this.CheckPriVDwn.Checked = _PCExportReportANSI.PrismaticPowerVDwn.HasValue ? _PCExportReportANSI.PrismaticPowerVDwn.Value : false;
            this.LbRefractive.Text = _PCExportReportANSI.RefractivePower.HasValue ? _PCExportReportANSI.RefractivePower.Value.ToString("0.00") : "";
            this.CheckScatter.Checked = _PCExportReportANSI.ScatterLight.HasValue ? _PCExportReportANSI.ScatterLight.Value : false;
            this.CheckMdeium.Checked = _PCExportReportANSI.MediumImpact.HasValue ? _PCExportReportANSI.MediumImpact.Value : false;
            this.CheckHigh.Checked = _PCExportReportANSI.HighImpact.HasValue ? _PCExportReportANSI.HighImpact.Value : false;
            this.CheckExtraHigh.Checked = _PCExportReportANSI.ExtraHighImpact.HasValue ? _PCExportReportANSI.ExtraHighImpact.Value : false;
            this.CheckPemer.Checked = _PCExportReportANSI.PermertrationTest.HasValue ? _PCExportReportANSI.PermertrationTest.Value : false;
            this.CheckIgnition.Checked = _PCExportReportANSI.IgnitionTest.HasValue ? _PCExportReportANSI.IgnitionTest.Value : false;
            this.CheckCorrosion.Checked = _PCExportReportANSI.Corrsion.HasValue ? _PCExportReportANSI.Corrsion.Value : false;
            this.CheckMarkings.Checked = _PCExportReportANSI.Markings.HasValue ? _PCExportReportANSI.Markings.Value : false;
            this.LbTester.Text = _PCExportReportANSI.Employee == null ? null : _PCExportReportANSI.Employee.ToString();
            this.lblPZDate.Text = _PCExportReportANSI.ApprovalDate;
        }

    }
}
