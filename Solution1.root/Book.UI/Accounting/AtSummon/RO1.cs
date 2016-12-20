using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Accounting.AtSummon
{
    public partial class RO1 : DevExpress.XtraReports.UI.XtraReport
    {
        public RO1()
        {
            InitializeComponent();
        }
        public RO1(System.Collections.Generic.IList<Model.AtSummon> list)
            : this()
        {
            this.DataSource = list;
            //this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lblDate.DataBindings.Add("Text", this.DataSource, Model.AtSummon.PRO_SummonDate, "{0:yyyy-MM-dd}");
            this.lblId.DataBindings.Add("Text", this.DataSource, Model.AtSummon.PRO_Id);
            this.lblCategory.DataBindings.Add("Text", this.DataSource, Model.AtSummon.PRO_SummonCategory);
            this.lblJieTotal.DataBindings.Add("Text", this.DataSource, Model.AtSummon.PRO_TotalDebits, "{0:F2}");
            this.lblDaiTotal.DataBindings.Add("Text", this.DataSource, Model.AtSummon.PRO_CreditTotal, "{0:F2}");
            this.xrSubreport1.ReportSource = new RO2();
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            RO2 r = this.xrSubreport1.ReportSource as RO2;
            r.AtSummon = this.GetCurrentRow() as Model.AtSummon;
        }

    }
}
