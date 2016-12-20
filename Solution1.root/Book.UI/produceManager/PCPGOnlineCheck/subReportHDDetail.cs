using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class subReportHDDetail : DevExpress.XtraReports.UI.XtraReport
    {

        public subReportHDDetail()
        {
            InitializeComponent();
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(subReportHDDetail_BeforePrint);

            this.TChdb.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_HouduBiao);
            this.TCguankou.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrGuanKou);
            this.TCgkfm.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrGuanKouFengMian);
            this.TCleft1.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL1);
            this.TCleft2.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL2);
            this.TCleft3.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL3);
            this.TCleft4.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL4);
            this.TCleft5.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL5);
            this.TCRight1.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR1);
            this.TCRight2.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR2);
            this.TCRight3.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR3);
            this.TCRight4.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR4);
            this.TCRight5.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR5);

        }

        public string _ThicknessTestId { get; set; }

        private void subReportHDDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = new BL.ThicknessTestDetailsManager().SelectByHeaderId(this._ThicknessTestId);
        }

    }
}
