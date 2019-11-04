using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class EarRO : DevExpress.XtraReports.UI.XtraReport
    {
        public EarRO()
        {
            InitializeComponent();
        }

        public EarRO(Model.PCExportReportANSI _PCExpANSI, int tag, string size, string valueA, string valueB)
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

            xrTableCell8.Text = string.Format("3.2.4  Clamping force test: {0} size                           Test height, a={1}+/ - 2mm                                        Test width , b={2}+/ - 2mm                                         For 120+/ - 5 second", size, valueA, valueB);

            this.lblCeShiBaoGaoRiQi.Text = _PCExpANSI.ReportDate.Value.ToShortDateString();
            this.lblCeShiShuLiang.Text = (_PCExpANSI.AmountTest.HasValue ? _PCExpANSI.AmountTest.ToString() : "0") + "pcs";
            this.lblChanpingpingming.Text = _PCExpANSI.Product.CustomerProductName == null ? "" : _PCExpANSI.Product.CustomerProductName.ToString();
            this.lblDingDanBianHao.Text = _PCExpANSI.InvoiceCusXOId;
            this.lblDingDanShuLiang.Text = (_PCExpANSI.Amount.HasValue ? _PCExpANSI.Amount.ToString() : "0") + "pcs";
            this.lblKehu.Text = _PCExpANSI.Customer == null ? "" : _PCExpANSI.Customer.CustomerName;
            this.lblZhiXingCeShiYuan.Text = _PCExpANSI.Employee == null ? "" : _PCExpANSI.Employee.ToString();

            this.lblShouCe1.Text = _PCExpANSI.ShouCeShu1.HasValue ? _PCExpANSI.ShouCeShu1.Value.ToString() : "0";
            this.lblShouCe2.Text = _PCExpANSI.ShouCeShu2.HasValue ? _PCExpANSI.ShouCeShu2.Value.ToString() : "0";
            this.lblShouCe4.Text = _PCExpANSI.ShouCeShu4.HasValue ? _PCExpANSI.ShouCeShu4.Value.ToString() : "0";
            this.lblShouCe5.Text = _PCExpANSI.ShouCeShu5.HasValue ? _PCExpANSI.ShouCeShu5.Value.ToString() : "0";

            this.lblPanDing1.Text = _PCExpANSI.PanDing1.HasValue ? _PCExpANSI.PanDing1.Value.ToString() : "0";
            this.lblPanDing2.Text = _PCExpANSI.PanDing1.HasValue ? _PCExpANSI.PanDing2.Value.ToString() : "0";
            this.lblPanDing3.Text = _PCExpANSI.PanDing1.HasValue ? _PCExpANSI.PanDing3.Value.ToString() : "0";
            this.lblPanDing4.Text = _PCExpANSI.PanDing1.HasValue ? _PCExpANSI.PanDing4.Value.ToString() : "0";
            this.lblPanDing5.Text = _PCExpANSI.PanDing1.HasValue ? _PCExpANSI.PanDing5.Value.ToString() : "0";
        }
    }
}
