using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class ANSI2015RO : DevExpress.XtraReports.UI.XtraReport
    {
        public ANSI2015RO()
        {
            InitializeComponent();
        }

        public ANSI2015RO(Model.PCExportReportANSI _PCExportReportANSI, int tag)
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
            this.LbCustomer.Text = _PCExportReportANSI.Customer == null ? null : _PCExportReportANSI.Customer.ToString();
            this.LbOrderId.Text = _PCExportReportANSI.InvoiceCusXOId == null ? null : _PCExportReportANSI.InvoiceCusXOId.ToString();
            this.LbProduct.Text = _PCExportReportANSI.Product.CustomerProductName == null ? null : _PCExportReportANSI.Product.CustomerProductName.ToString();
            this.LbOrderAmount.Text = (_PCExportReportANSI.Amount.HasValue ? _PCExportReportANSI.Amount.ToString() : "0") + "PCS";
            this.LbTestAmount.Text = (_PCExportReportANSI.AmountTest.HasValue ? _PCExportReportANSI.AmountTest.ToString() : "0") + "PCS";
            this.LbTesrPerson.Text = _PCExportReportANSI.Employee == null ? null : _PCExportReportANSI.Employee.ToString();
            //this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToShortDateString();
            this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToString("dd/MM/yyyy");

            //this.lbl_ShouCe1.Text = (_PCExportReportANSI.QuYangShu1.HasValue ? _PCExportReportANSI.QuYangShu1.ToString() : "0") + " PCS";
            this.lbl_ShouCe2.Text = (_PCExportReportANSI.QuYangShu2.HasValue ? _PCExportReportANSI.QuYangShu2.ToString() : "0") + " PCS";
            this.lbl_ShouCe3.Text = (_PCExportReportANSI.QuYangShu3.HasValue ? _PCExportReportANSI.QuYangShu3.ToString() : "0") + " PCS";
            this.lbl_ShouCe4.Text = (_PCExportReportANSI.QuYangShu4.HasValue ? _PCExportReportANSI.QuYangShu4.ToString() : "0") + " PCS";
            this.lbl_ShouCe5.Text = (_PCExportReportANSI.QuYangShu5.HasValue ? _PCExportReportANSI.QuYangShu5.ToString() : "0") + " PCS";
            this.lbl_ShouCe6.Text = (_PCExportReportANSI.QuYangShu6.HasValue ? _PCExportReportANSI.QuYangShu6.ToString() : "0") + " PCS";
            this.lbl_ShouCe7.Text = (_PCExportReportANSI.QuYangShu7.HasValue ? _PCExportReportANSI.QuYangShu7.ToString() : "0") + " PCS";
            this.lbl_ShouCe8.Text = (_PCExportReportANSI.QuYangShu8.HasValue ? _PCExportReportANSI.QuYangShu8.ToString() : "0") + " PCS";
            this.lbl_ShouCe9.Text = (_PCExportReportANSI.QuYangShu9.HasValue ? _PCExportReportANSI.QuYangShu9.ToString() : "0") + " PCS";
            this.lbl_ShouCe10.Text = (_PCExportReportANSI.QuYangShu10.HasValue ? _PCExportReportANSI.QuYangShu10.ToString() : "0") + " PCS";
            this.lbl_ShouCe11.Text = (_PCExportReportANSI.QuYangShu11.HasValue ? _PCExportReportANSI.QuYangShu11.ToString() : "0") + " PCS";
            this.lbl_ShouCe12.Text = (_PCExportReportANSI.QuYangShu12.HasValue ? _PCExportReportANSI.QuYangShu12.ToString() : "0") + " PCS";
            this.lbl_ShouCe13.Text = (_PCExportReportANSI.QuYangShu13.HasValue ? _PCExportReportANSI.QuYangShu13.ToString() : "0") + " PCS";
            //this.lbl_ShouCe14.Text = (_PCExportReportANSI.QuYangShu14.HasValue ? _PCExportReportANSI.QuYangShu14.ToString() : "0") + " PCS";
            this.lbl_ShouCe15.Text = (_PCExportReportANSI.QuYangShu15.HasValue ? _PCExportReportANSI.QuYangShu15.ToString() : "0") + " PCS";
            this.lbl_ShouCe16.Text = (_PCExportReportANSI.QuYangShu16.HasValue ? _PCExportReportANSI.QuYangShu16.ToString() : "0") + " PCS";
            this.lbl_ShouCe17.Text = (_PCExportReportANSI.QuYangShu17.HasValue ? _PCExportReportANSI.QuYangShu17.ToString() : "0") + " PCS";
            this.lbl_ShouCe18.Text = (_PCExportReportANSI.QuYangShu18.HasValue ? _PCExportReportANSI.QuYangShu18.ToString() : "0") + " PCS";


            //this.lbl_PanDing1.Text = (_PCExportReportANSI.PanDing1.HasValue ? _PCExportReportANSI.PanDing1.ToString() : "0") + " PASS";
            this.lbl_PanDing2.Text = (_PCExportReportANSI.PanDing2.HasValue ? _PCExportReportANSI.PanDing2.ToString() : "0") + " PASS";
            this.lbl_PanDing3.Text = (_PCExportReportANSI.PanDing3.HasValue ? _PCExportReportANSI.PanDing3.ToString() : "0") + " PASS";
            this.lbl_PanDing4.Text = (_PCExportReportANSI.PanDing4.HasValue ? _PCExportReportANSI.PanDing4.ToString() : "0") + " PASS";
            this.lbl_PanDing5.Text = (_PCExportReportANSI.PanDing5.HasValue ? _PCExportReportANSI.PanDing5.ToString() : "0") + " PASS";
            this.lbl_PanDing6.Text = (_PCExportReportANSI.PanDing6.HasValue ? _PCExportReportANSI.PanDing6.ToString() : "0") + " PASS";
            this.lbl_PanDing7.Text = (_PCExportReportANSI.PanDing7.HasValue ? _PCExportReportANSI.PanDing7.ToString() : "0") + " PASS";
            this.lbl_PanDing8.Text = (_PCExportReportANSI.PanDing8.HasValue ? _PCExportReportANSI.PanDing8.ToString() : "0") + " PASS";
            this.lbl_PanDing9.Text = (_PCExportReportANSI.PanDing9.HasValue ? _PCExportReportANSI.PanDing9.ToString() : "0") + " PASS";
            this.lbl_PanDing10.Text = (_PCExportReportANSI.PanDing10.HasValue ? _PCExportReportANSI.PanDing10.ToString() : "0") + " PASS";
            this.lbl_PanDing11.Text = (_PCExportReportANSI.PanDing11.HasValue ? _PCExportReportANSI.PanDing11.ToString() : "0") + " PASS";
            this.lbl_PanDing12.Text = (_PCExportReportANSI.PanDingShu12.HasValue ? _PCExportReportANSI.PanDingShu12.ToString() : "0") + " PASS";
            this.lbl_PanDing13.Text = (_PCExportReportANSI.PanDingShu13.HasValue ? _PCExportReportANSI.PanDingShu13.ToString() : "0") + " PASS";
            //this.lbl_PanDing14.Text = (_PCExportReportANSI.PanDingShu14.HasValue ? _PCExportReportANSI.PanDingShu14.ToString() : "0") + " PASS";
            this.lbl_PanDing15.Text = (_PCExportReportANSI.PanDingShu15.HasValue ? _PCExportReportANSI.PanDingShu15.ToString() : "0") + " PASS";
            this.lbl_PanDing16.Text = (_PCExportReportANSI.PanDingShu16.HasValue ? _PCExportReportANSI.PanDingShu16.ToString() : "0") + " PASS";
            this.lbl_PanDing17.Text = (_PCExportReportANSI.PanDingShu17.HasValue ? _PCExportReportANSI.PanDingShu17.ToString() : "0") + " PASS";
            this.lbl_PanDing18.Text = (_PCExportReportANSI.PanDingShu18.HasValue ? _PCExportReportANSI.PanDingShu18.ToString() : "0") + " PASS";
        }

    }
}
