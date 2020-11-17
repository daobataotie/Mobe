using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class subReportHD : DevExpress.XtraReports.UI.XtraReport
    {
        private BL.ThicknessTestManager _ThicknessTestManager = new BL.ThicknessTestManager();

        /// <summary>
        /// 空：光学/厚度表；2：首件上线检查表(新)
        /// </summary>
        string sign = string.Empty;

        public subReportHD()
        {
            InitializeComponent();

            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(subReportHD_BeforePrint);
            this.TCcsdh.DataBindings.Add("Text", this.DataSource, Model.ThicknessTest.PRO_ThicknessTestId);
            this.TCtsl.DataBindings.Add("Text", this.DataSource, Model.ThicknessTest.PRO_Perspectiverate);
            this.TCcsrq.DataBindings.Add("Text", this.DataSource, Model.ThicknessTest.PRO_ThicknessTestDate, "{0:yyyy-MM-dd}");
            this.TCcsyg.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);
            this.TCbeizhu.DataBindings.Add("Text", this.DataSource, Model.ThicknessTest.PRO_ThicknessDescript);

            this.subThicknessTestDetails.ReportSource = new subReportHDDetail();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">空：光学/厚度表；2：首件上线检查表(新)</param>
        public subReportHD(string s)
            : this()
        {
            this.sign = s;
        }

        public string _PCPGOnlineCheckDetailId { get; set; }

        private void subReportHD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.sign == string.Empty)
                this.DataSource = this._ThicknessTestManager.mSelect(this._PCPGOnlineCheckDetailId);
            else if (this.sign == "2")
                this.DataSource = this._ThicknessTestManager.PFCSelect(this._PCPGOnlineCheckDetailId);

            subReportHDDetail subHDDetails = this.subThicknessTestDetails.ReportSource as subReportHDDetail;
            subHDDetails._ThicknessTestId = (this.GetCurrentRow() as Model.ThicknessTest) == null ? "" : (this.GetCurrentRow() as Model.ThicknessTest).ThicknessTestId;
        }
    }
}
