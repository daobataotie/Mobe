using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.PCExportReportANSI _PCExpANSI, int tag)
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
            //this.lblCeShiBaoGaoRiQi.Text = _PCExpANSI.ReportDate.Value.ToShortDateString();
            this.lblCeShiBaoGaoRiQi.Text = _PCExpANSI.ReportDate.Value.ToString("dd/MM/yyyy");
            this.lblCeShiShuLiang.Text = (_PCExpANSI.AmountTest.HasValue ? _PCExpANSI.AmountTest.ToString() : "0") + "PCS";
            this.lblChanpingpingming.Text = _PCExpANSI.Product.CustomerProductName == null ? "" : _PCExpANSI.Product.CustomerProductName.ToString();
            this.lblDingDanBianHao.Text = _PCExpANSI.InvoiceCusXOId;
            this.lblDingDanShuLiang.Text = (_PCExpANSI.Amount.HasValue ? _PCExpANSI.Amount.ToString() : "0") + "PCS";
            this.lblKehu.Text = _PCExpANSI.Customer == null ? "" : _PCExpANSI.Customer.ToString();
            this.lblZhiXingCeShiYuan.Text = _PCExpANSI.Employee == null ? "" : _PCExpANSI.Employee.ToString();

            this.lblCriteria3.Text = _PCExpANSI.Criteria3;

            this.lblPanDing1.Text = (_PCExpANSI.PanDing1.HasValue ? _PCExpANSI.PanDing1.ToString() : "0") + "PASS";
            this.lblPanDing2.Text = (_PCExpANSI.PanDing2.HasValue ? _PCExpANSI.PanDing2.ToString() : "0") + "PASS";
            this.lblPanDing3.Text = (_PCExpANSI.PanDing3.HasValue ? _PCExpANSI.PanDing3.ToString() : "0") + "PASS";
            this.lblPanDing4.Text = (_PCExpANSI.PanDing4.HasValue ? _PCExpANSI.PanDing4.ToString() : "0") + "PASS";
            this.lblPanDing5.Text = (_PCExpANSI.PanDing5.HasValue ? _PCExpANSI.PanDing5.ToString() : "0") + "PASS";
            this.lblPanDing6.Text = (_PCExpANSI.PanDing6.HasValue ? _PCExpANSI.PanDing6.ToString() : "0") + "PASS";
            this.lblPanDing7.Text = (_PCExpANSI.PanDing7.HasValue ? _PCExpANSI.PanDing7.ToString() : "0") + "PASS";
            this.lblPanDing8.Text = (_PCExpANSI.PanDing8.HasValue ? _PCExpANSI.PanDing8.ToString() : "0") + "PASS";
            this.lblPanDing9.Text = (_PCExpANSI.PanDing9.HasValue ? _PCExpANSI.PanDing9.ToString() : "0") + "PASS";
            this.lblPanDing10.Text = (_PCExpANSI.PanDing10.HasValue ? _PCExpANSI.PanDing10.ToString() : "0") + "PASS";
            this.lblPanDing11.Text = (_PCExpANSI.PanDing11.HasValue ? _PCExpANSI.PanDing11.ToString() : "0") + "PASS";
            this.lblPanDing12.Text = (_PCExpANSI.PanDingShu12.HasValue ? _PCExpANSI.PanDingShu12.ToString() : "0") + "PASS";

            this.lblQuYangShu1.Text = (_PCExpANSI.QuYangShu1.HasValue ? _PCExpANSI.QuYangShu1.ToString() : "0") + "PCS";
            this.lblQuYangShu2.Text = (_PCExpANSI.QuYangShu2.HasValue ? _PCExpANSI.QuYangShu2.ToString() : "0") + "PCS";
            this.lblQuYangShu3.Text = (_PCExpANSI.QuYangShu3.HasValue ? _PCExpANSI.QuYangShu3.ToString() : "0") + "PCS";
            this.lblQuYangShu4.Text = (_PCExpANSI.QuYangShu4.HasValue ? _PCExpANSI.QuYangShu4.ToString() : "0") + "PCS";
            this.lblQuYangShu5.Text = (_PCExpANSI.QuYangShu5.HasValue ? _PCExpANSI.QuYangShu5.ToString() : "0") + "PCS";
            this.lblQuYangShu6.Text = (_PCExpANSI.QuYangShu6.HasValue ? _PCExpANSI.QuYangShu6.ToString() : "0") + "PCS";
            this.lblQuYangShu7.Text = (_PCExpANSI.QuYangShu7.HasValue ? _PCExpANSI.QuYangShu7.ToString() : "0") + "PCS";
            this.lblQuYangShu8.Text = (_PCExpANSI.QuYangShu8.HasValue ? _PCExpANSI.QuYangShu8.ToString() : "0") + "PCS";
            this.lblQuYangShu9.Text = (_PCExpANSI.QuYangShu9.HasValue ? _PCExpANSI.QuYangShu9.ToString() : "0") + "PCS";
            this.lblQuYangShu10.Text = (_PCExpANSI.QuYangShu10.HasValue ? _PCExpANSI.QuYangShu10.ToString() : "0") + "PCS";
            this.lblQuYangShu11.Text = (_PCExpANSI.QuYangShu11.HasValue ? _PCExpANSI.QuYangShu11.ToString() : "0") + "PCS";
            this.lblQuYangShu12.Text = (_PCExpANSI.QuYangShu12.HasValue ? _PCExpANSI.QuYangShu12.ToString() : "0") + "PCS";

            this.lblShouCeShu1.Text = (_PCExpANSI.ShouCeShu1.HasValue ? _PCExpANSI.ShouCeShu1.ToString() : "0") + "PCS";
            this.lblShouCeShu2.Text = (_PCExpANSI.ShouCeShu2.HasValue ? _PCExpANSI.ShouCeShu2.ToString() : "0") + "PCS";
            this.lblShouCeShu3.Text = (_PCExpANSI.ShouCeShu3.HasValue ? _PCExpANSI.ShouCeShu3.ToString() : "0") + "PCS";
            this.lblShouCeShu4.Text = (_PCExpANSI.ShouCeShu4.HasValue ? _PCExpANSI.ShouCeShu4.ToString() : "0") + "PCS";
            this.lblShouCeShu5.Text = (_PCExpANSI.ShouCeShu5.HasValue ? _PCExpANSI.ShouCeShu5.ToString() : "0") + "PCS";
            this.lblShouCeShu6.Text = (_PCExpANSI.ShouCeShu6.HasValue ? _PCExpANSI.ShouCeShu6.ToString() : "0") + "PCS";
            this.lblShouCeShu7.Text = (_PCExpANSI.ShouCeShu7.HasValue ? _PCExpANSI.ShouCeShu7.ToString() : "0") + "PCS";
            this.lblShouCeShu8.Text = (_PCExpANSI.ShouCeShu8.HasValue ? _PCExpANSI.ShouCeShu8.ToString() : "0") + "PCS";
            this.lblShouCeShu9.Text = (_PCExpANSI.ShouCeShu9.HasValue ? _PCExpANSI.ShouCeShu9.ToString() : "0") + "PCS";
            this.lblShouCeShu10.Text = (_PCExpANSI.ShouCeShu10.HasValue ? _PCExpANSI.ShouCeShu10.ToString() : "0") + "PCS";
            this.lblShouCeShu11.Text = (_PCExpANSI.ShouCeShu11.HasValue ? _PCExpANSI.ShouCeShu11.ToString() : "0") + "PCS";
            this.lblShouCeShu12.Text = (_PCExpANSI.ShouCeShu12.HasValue ? _PCExpANSI.ShouCeShu12.ToString() : "0") + "PCS";
        }
    }
}
