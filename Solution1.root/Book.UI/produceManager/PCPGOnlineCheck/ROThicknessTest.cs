using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class ROThicknessTest : DevExpress.XtraReports.UI.XtraReport
    {
        public ROThicknessTest()
        {
            InitializeComponent();
        }
        public ROThicknessTest(Model.ThicknessTest thicknessTest)
            : this()
        {
            if (thicknessTest == null)
                return;
            lblCompanyName.Text = BL.Settings.CompanyChineseName;
            lblROThicknessTest.Text = Book.UI.Properties.Resources.ROThicknessTest;
            lblTestId.Text = thicknessTest.ThicknessTestId;
            lblPerspective.Text = thicknessTest.Perspectiverate.ToString();
            lblTestDate.Text = thicknessTest.ThicknessTestDate.Value.ToString("yyyy-MM-dd");
            lblRemarks.Text = thicknessTest.ThicknessDescript;
            lblEmployee.Text = thicknessTest.Employee.EmployeeName;
            lblPrintTime.Text += DateTime.Now.ToShortDateString();
            lblCondition.Text = thicknessTest.Condition;

            this.DataSource = thicknessTest.Details;
            TCThickness.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_HouduBiao);
            TCGuanKou.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrGuanKou);
            TCGuanKouDuiMian.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrGuanKouFengMian);
            TCL1.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL1);
            TCL2.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL2);
            TCL3.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL3);
            TCL4.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL4);
            TCL5.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrL5);
            TCR1.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR1);
            TCR2.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR2);
            TCR3.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR3);
            TCR4.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR4);
            TCR5.DataBindings.Add("Text", this.DataSource, Model.ThicknessTestDetails.PRO_attrR5);
        }
    }
}
