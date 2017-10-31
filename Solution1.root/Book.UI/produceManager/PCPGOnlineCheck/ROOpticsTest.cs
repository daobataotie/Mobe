using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class ROOpticsTest : DevExpress.XtraReports.UI.XtraReport
    {
        public ROOpticsTest()
        {
            InitializeComponent();
        }
        public ROOpticsTest(Model.OpticsTest opticsTest)
            : this()
        {
            if (opticsTest == null)
                return;
            lblCompanyName.Text = BL.Settings.CompanyChineseName;
            lblROPCOpticsCheck.Text = Book.UI.Properties.Resources.ROPCOpticsCheck;
            lblTestId.Text = opticsTest.OpticsTestId;
            lblManualTestId.Text = opticsTest.ManualId;
            lblTestTime.Text = opticsTest.OptiscTestDate.Value.ToString("yyyy-MM-dd");
            lblCheckModel.Text = opticsTest.MachineName;
            lblCheckCondition.Text = opticsTest.Condition;
            lblTestEmployee.Text = opticsTest.Employee.ToString();
            lblPrintTime.Text += DateTime.Now.ToString("yyyy-MM-dd");
            //lblRemarks.Text=opticsTest.
            lblLeftA.Text = opticsTest.LattrA.Value.ToString("F2");
            lblLeftC.Text = opticsTest.LattrC.Value.ToString("F2");
            lblLeftDOWN.Text = opticsTest.LeftVerticalJudge;
            lblLeftIN.Text = opticsTest.LinPSM.Value.ToString("F2");
            lblLeftOUT.Text = opticsTest.LeftLevelJudge;
            lblLeftS.Text = opticsTest.LattrS.Value.ToString("F2");
            lblLeftUP.Text = opticsTest.LupPSM.Value.ToString("F2");

            lblRightS.Text = opticsTest.RattrS.Value.ToString("F2");
            lblRightA.Text = opticsTest.RattrA.Value.ToString("F2");
            lblRightC.Text = opticsTest.RattrC.Value.ToString("F2");
            lblRightDOWN.Text = opticsTest.RightVerticalJudge;
            lblRightIN.Text = opticsTest.RinPSM.Value.ToString("F2");
            lblRightOUT.Text = opticsTest.RightLevelJudge;
            lblRightUP.Text = opticsTest.RupPSM.Value.ToString("F2");
        }
    }
}
