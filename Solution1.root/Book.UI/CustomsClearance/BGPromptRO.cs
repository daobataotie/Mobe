using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace Book.UI.CustomsClearance
{
    public partial class BGPromptRO : DevExpress.XtraReports.UI.XtraReport
    {
        public BGPromptRO()
        {
            InitializeComponent();
        }
        public BGPromptRO(DataTable dt)
            : this()
        {
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lblReportDate.Text += DateTime.Now.Date;

            this.DataSource = dt;
            TCBGHandbookId.DataBindings.Add("Text", this.DataSource, "BGHandbookId");
            TCId.DataBindings.Add("Text", this.DataSource, "Id");
            TCId2.DataBindings.Add("Text", this.DataSource, "Id2");
            TCProname.DataBindings.Add("Text", this.DataSource, "ProName");
            TCQuantity.DataBindings.Add("Text", this.DataSource, "Quantity");
            TCBeeQuantity.DataBindings.Add("Text", this.DataSource, "BeeQuantity");
            TCYdwcQuantity.DataBindings.Add("Text", this.DataSource, "YdwcQuantity");
            TCBeyond.DataBindings.Add("Text", this.DataSource, "Beyond");
        }
    }
}
