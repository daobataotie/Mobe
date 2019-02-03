using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class CSARO : DevExpress.XtraReports.UI.XtraReport
    {
        public CSARO()
        {
            InitializeComponent();
        }

        public CSARO(Model.PCExportReportANSI _PCExportReportANSI, int tag)
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
            this.LbCustomer.Text = _PCExportReportANSI.Customer == null ? null : _PCExportReportANSI.Customer.CustomerName;
            this.LbOrderId.Text = _PCExportReportANSI.InvoiceCusXOId == null ? null : _PCExportReportANSI.InvoiceCusXOId.ToString();
            this.LbProduct.Text = _PCExportReportANSI.Product.CustomerProductName == null ? null : _PCExportReportANSI.Product.CustomerProductName.ToString();
            this.LbOrderAmount.Text = (_PCExportReportANSI.Amount.HasValue ? _PCExportReportANSI.Amount.ToString() : "0") + "PCS";
            this.LbTestAmount.Text = (_PCExportReportANSI.AmountTest.HasValue ? _PCExportReportANSI.AmountTest.ToString() : "0") + "PCS";
            this.LbTesrPerson.Text = _PCExportReportANSI.Employee == null ? null : _PCExportReportANSI.Employee.ToString();
            //this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToShortDateString();
            this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToString("dd/MM/yyyy");
            this.LbMirrorlens.Text = _PCExportReportANSI.Mirrorlens == null ? null : _PCExportReportANSI.Mirrorlens.ToString();

            this.xrTableCell20.Text = _PCExportReportANSI.CeShiSuLi + this.xrTableCell20.Text;

            this.LbSpecifiction.Text = string.IsNullOrEmpty(_PCExportReportANSI.CSAJiShuBiaoZhun) ? "CSA Z94.3-2015" : _PCExportReportANSI.CSAJiShuBiaoZhun;
            //if (_PCExportReportANSI.CSAJiShuBiaoZhun != null && _PCExportReportANSI.CSAJiShuBiaoZhun.Contains("2015"))
            //    this.xrLabel121.Text="R7-08-03-R-2";
            this.LbOpticsTestAmount.Text = (_PCExportReportANSI.ShouCeShu1.HasValue ? _PCExportReportANSI.ShouCeShu1.ToString() : "0") + "PCS";
            this.LbOpticsTestAmount.Text += "/" + this.LbOpticsTestAmount.Text;
            this.LbClearTestAmount.Text = (_PCExportReportANSI.ShouCeShu2.HasValue ? _PCExportReportANSI.ShouCeShu2.ToString() : "0") + "PCS";
            this.LbClearTestAmount.Text += "/" + this.LbClearTestAmount.Text;
            this.LbPolarizedTestAmount.Text = (_PCExportReportANSI.ShouCeShu3.HasValue ? _PCExportReportANSI.ShouCeShu3.ToString() : "0") + "PCS";
            this.LbPolarizedTestAmount.Text += "/" + this.LbPolarizedTestAmount.Text;
            this.LbFogTestAmount.Text = (_PCExportReportANSI.ShouCeShu4.HasValue ? _PCExportReportANSI.ShouCeShu4.ToString() : "0") + "PCS";
            this.LbFogTestAmount.Text += "/" + this.LbFogTestAmount.Text;
            this.LbLightTestAmount.Text = (_PCExportReportANSI.ShouCeShu5.HasValue ? _PCExportReportANSI.ShouCeShu5.ToString() : "0") + "PCS";
            this.LbLightTestAmount.Text += "/" + this.LbLightTestAmount.Text;
            this.LbImpaceTestAmount.Text = (_PCExportReportANSI.ShouCeShu6.HasValue ? _PCExportReportANSI.ShouCeShu6.ToString() : "0") + "PCS";
            this.LbImpaceTestAmount.Text += "/" + this.LbImpaceTestAmount.Text;

            this.LbOpticsJudge.Text = (_PCExportReportANSI.PanDing0.HasValue ? _PCExportReportANSI.PanDing0.ToString() : "0") + "PASS";
            this.LbClearJudge.Text = (_PCExportReportANSI.PanDing1.HasValue ? _PCExportReportANSI.PanDing1.ToString() : "0") + "PASS";
            this.LbPolarizedJudge.Text = (_PCExportReportANSI.PanDing2.HasValue ? _PCExportReportANSI.PanDing2.ToString() : "0") + "PASS";
            this.LbFogJudge.Text = (_PCExportReportANSI.PanDing3.HasValue ? _PCExportReportANSI.PanDing3.ToString() : "0") + "PASS";
            this.LbLightJudge.Text = (_PCExportReportANSI.PanDing4.HasValue ? _PCExportReportANSI.PanDing4.ToString() : "0") + "PASS";
            this.LbImpactJudge.Text = (_PCExportReportANSI.PanDing5.HasValue ? _PCExportReportANSI.PanDing5.ToString() : "0") + "PASS";
        }
    }
}
