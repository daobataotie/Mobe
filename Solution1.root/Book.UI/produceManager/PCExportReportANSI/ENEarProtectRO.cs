using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class ENEarProtectRO : DevExpress.XtraReports.UI.XtraReport
    {
        public ENEarProtectRO()
        {
            InitializeComponent();
        }

        public ENEarProtectRO(Model.PCExportReportANSI _PCExpANSI, int tag, string size, string valueA, string valueB, string earmuffsAbove)
            : this()
        {
            if (tag == 1)
            {
                this.lblCompanyName.Text = "ALAN    SAFETY    COMPANY.";
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ALANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ALANSignature")[0].SettingCurrentValue;
            }
            else if (tag == 2)
            {
                this.lblCompanyName.Text = "PPE   SAFETY   INC.";
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("PPESignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("PPESignature")[0].SettingCurrentValue;
            }
            else
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ASWANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ASWANSignature")[0].SettingCurrentValue;

            xrTableCell6.Text = xrTableCell6.Text.Replace("*M", size).Replace("*A1", valueA).Replace("*B1", valueB);
            xrTableCell7.Text = xrTableCell7.Text.Replace("*B", valueB);
            xrTableCell9.Text = xrTableCell9.Text.Replace("**", earmuffsAbove);

            this.lblCeShiBaoGaoRiQi.Text = _PCExpANSI.ReportDate.Value.ToShortDateString();
            this.lblCeShiShuLiang.Text = (_PCExpANSI.AmountTest.HasValue ? _PCExpANSI.AmountTest.ToString() : "0") + "pcs";
            this.lblChanpingpingming.Text = _PCExpANSI.Product.CustomerProductName == null ? "" : _PCExpANSI.Product.CustomerProductName.ToString();
            this.lblDingDanBianHao.Text = _PCExpANSI.InvoiceCusXOId;
            this.lblDingDanShuLiang.Text = (_PCExpANSI.Amount.HasValue ? _PCExpANSI.Amount.ToString() : "0") + "pcs";
            this.lblKehu.Text = _PCExpANSI.Customer == null ? "" : _PCExpANSI.Customer.CustomerName;
            this.lblZhiXingCeShiYuan.Text = _PCExpANSI.Employee == null ? "" : _PCExpANSI.Employee.ToString();

            this.lblShouCe1.Text = _PCExpANSI.ShouCeShu1.HasValue ? _PCExpANSI.ShouCeShu1.Value.ToString() : "0";
            this.lblShouCe2.Text = _PCExpANSI.ShouCeShu2.HasValue ? _PCExpANSI.ShouCeShu2.Value.ToString() : "0";
            this.lblShouCe3.Text = _PCExpANSI.ShouCeShu3.HasValue ? _PCExpANSI.ShouCeShu3.Value.ToString() : "0";
            this.lblShouCe4.Text = _PCExpANSI.ShouCeShu4.HasValue ? _PCExpANSI.ShouCeShu4.Value.ToString() : "0";
            this.lblShouCe5.Text = _PCExpANSI.ShouCeShu5.HasValue ? _PCExpANSI.ShouCeShu5.Value.ToString() : "0";
            this.lblShouCe6.Text = _PCExpANSI.ShouCeShu6.HasValue ? _PCExpANSI.ShouCeShu6.Value.ToString() : "0";

            this.lblPanDing1.Text = _PCExpANSI.PanDing1.HasValue ? _PCExpANSI.PanDing1.Value.ToString() : "0";
            this.lblPanDing2.Text = _PCExpANSI.PanDing2.HasValue ? _PCExpANSI.PanDing2.Value.ToString() : "0";
            this.lblPanDing3.Text = _PCExpANSI.PanDing3.HasValue ? _PCExpANSI.PanDing3.Value.ToString() : "0";
            this.lblPanDing4.Text = _PCExpANSI.PanDing4.HasValue ? _PCExpANSI.PanDing4.Value.ToString() : "0";
            this.lblPanDing5.Text = _PCExpANSI.PanDing5.HasValue ? _PCExpANSI.PanDing5.Value.ToString() : "0";
            this.lblPanDing6.Text = _PCExpANSI.PanDing6.HasValue ? _PCExpANSI.PanDing6.Value.ToString() : "0";
        }
    }
}
