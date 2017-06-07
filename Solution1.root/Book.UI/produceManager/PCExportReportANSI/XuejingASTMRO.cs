using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class XuejingASTMRO : DevExpress.XtraReports.UI.XtraReport
    {
        public XuejingASTMRO()
        {
            InitializeComponent();
            this.xrPictureBox1.Visible = false;
        }

        public XuejingASTMRO(Model.PCExportReportANSI _PCExportReportANSI, int tag)
            : this()
        {
            if (tag == 1)
            {
                this.xrLabel1.Text = "ALAN    SAFETY    COMPANY.";
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ALANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ALANSignature")[0].SettingCurrentValue;
                this.xrPictureBox1.Visible = true;
            }
            else if (tag == 2)
            {
                this.xrLabel1.Text = "PPE   SAFETY   INC.";
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("PPESignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("PPESignature")[0].SettingCurrentValue;
            }
            else
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ASWANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ASWANSignature")[0].SettingCurrentValue;
            //this.LbCustomer.Text = _PCExportReportANSI.Customer == null ? null : _PCExportReportANSI.Customer.CustomerName;
            this.LbOrderId.Text = _PCExportReportANSI.InvoiceCusXOId == null ? null : _PCExportReportANSI.InvoiceCusXOId.ToString();
            this.LbProduct.Text = _PCExportReportANSI.Product.CustomerProductName == null ? null : _PCExportReportANSI.Product.CustomerProductName.ToString();
            this.LbOrderAmount.Text = (_PCExportReportANSI.Amount.HasValue ? _PCExportReportANSI.Amount.ToString() : "0") + "PCS";
            this.LbTestAmount.Text = (_PCExportReportANSI.AmountTest.HasValue ? _PCExportReportANSI.AmountTest.ToString() : "0") + "PCS";
            this.LbTesrPerson.Text = _PCExportReportANSI.Employee == null ? null : _PCExportReportANSI.Employee.ToString();
            this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToString("dd/MM/yyyy");


            this.TC_Test1.Text = (_PCExportReportANSI.ShouCeShu1.HasValue ? _PCExportReportANSI.ShouCeShu1.ToString() : "0") + "PCS";
            this.TC_Test2.Text = (_PCExportReportANSI.ShouCeShu2.HasValue ? _PCExportReportANSI.ShouCeShu2.ToString() : "0") + "PCS";
            this.TC_Test3.Text = (_PCExportReportANSI.ShouCeShu3.HasValue ? _PCExportReportANSI.ShouCeShu3.ToString() : "0") + "PCS";
            this.TC_Test4.Text = (_PCExportReportANSI.ShouCeShu4.HasValue ? _PCExportReportANSI.ShouCeShu4.ToString() : "0") + "PCS";
            this.TC_Test5.Text = (_PCExportReportANSI.ShouCeShu5.HasValue ? _PCExportReportANSI.ShouCeShu5.ToString() : "0") + "PCS";
            this.TC_Test6.Text = (_PCExportReportANSI.ShouCeShu6.HasValue ? _PCExportReportANSI.ShouCeShu6.ToString() : "0") + "PCS";
            this.TC_Test7.Text = (_PCExportReportANSI.ShouCeShu7.HasValue ? _PCExportReportANSI.ShouCeShu7.ToString() : "0") + "PCS";
            this.TC_Test8.Text = (_PCExportReportANSI.ShouCeShu8.HasValue ? _PCExportReportANSI.ShouCeShu8.ToString() : "0") + "PCS";
            this.TC_Test9.Text = (_PCExportReportANSI.ShouCeShu9.HasValue ? _PCExportReportANSI.ShouCeShu9.ToString() : "0") + "PCS";
            this.TC_Test10.Text = (_PCExportReportANSI.ShouCeShu10.HasValue ? _PCExportReportANSI.ShouCeShu10.ToString() : "0") + "PCS";
            this.TC_Test11.Text = (_PCExportReportANSI.ShouCeShu11.HasValue ? _PCExportReportANSI.ShouCeShu11.ToString() : "0") + "PCS";


            this.TC_Judge1.Text = (_PCExportReportANSI.PanDing1.HasValue ? _PCExportReportANSI.PanDing1.ToString() : "0") + "PASS";
            this.TC_Judge2.Text = (_PCExportReportANSI.PanDing2.HasValue ? _PCExportReportANSI.PanDing2.ToString() : "0") + "PASS";
            this.TC_Judge3.Text = (_PCExportReportANSI.PanDing3.HasValue ? _PCExportReportANSI.PanDing3.ToString() : "0") + "PASS";
            this.TC_Judge4.Text = (_PCExportReportANSI.PanDing4.HasValue ? _PCExportReportANSI.PanDing4.ToString() : "0") + "PASS";
            this.TC_Judge5.Text = (_PCExportReportANSI.PanDing5.HasValue ? _PCExportReportANSI.PanDing5.ToString() : "0") + "PASS";
            this.TC_Judge6.Text = (_PCExportReportANSI.PanDing6.HasValue ? _PCExportReportANSI.PanDing6.ToString() : "0") + "PASS";
            this.TC_Judge7.Text = (_PCExportReportANSI.PanDing7.HasValue ? _PCExportReportANSI.PanDing7.ToString() : "0") + "PASS";
            this.TC_Judge8.Text = (_PCExportReportANSI.PanDing8.HasValue ? _PCExportReportANSI.PanDing8.ToString() : "0") + "PASS";
            this.TC_Judge9.Text = (_PCExportReportANSI.PanDing9.HasValue ? _PCExportReportANSI.PanDing9.ToString() : "0") + "PASS";
            this.TC_Judge10.Text = (_PCExportReportANSI.PanDing10.HasValue ? _PCExportReportANSI.PanDing10.ToString() : "0") + "PASS";
            this.TC_Judge11.Text = (_PCExportReportANSI.PanDing11.HasValue ? _PCExportReportANSI.PanDing11.ToString() : "0") + "PASS";
        }

    }
}
