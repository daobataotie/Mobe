using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.Accounting.AtSummon
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }
        public RO(Model.AtSummon atSummon)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lblDate.Text = atSummon.SummonDate == null ? "" : atSummon.SummonDate.Value.ToString("yyyy-MM-dd");
            this.lblId.Text = atSummon.Id;
            this.lblCategory.Text = atSummon.SummonCategory;
            this.lblJieTotal.Text = atSummon.TotalDebits == null ? "0" : atSummon.TotalDebits.Value.ToString("F2");
            this.lblDaiTotal.Text = atSummon.CreditTotal == null ? "0" : atSummon.CreditTotal.Value.ToString("F2");

            this.DataSource = atSummon.Details;
            this.TCJieDai.DataBindings.Add("Text", this.DataSource, Model.AtSummonDetail.PRO_Lending);
            this.TCKemuId.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_Id);
            this.TCKemuName.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_SubjectName);
            this.TCNote.DataBindings.Add("Text", this.DataSource, Model.AtSummonDetail.PRO_Summary);
            this.TCMoney.DataBindings.Add("Text", this.DataSource, Model.AtSummonDetail.PRO_AMoney,"{0:F2}");
        }
    }
}
