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
            lblLeftA.Text = opticsTest.LattrA.ToString();
            lblLeftC.Text = opticsTest.LattrC.ToString();
            lblLeftDOWN.Text = opticsTest.LdownPSM.ToString();
            lblLeftIN.Text = opticsTest.LinPSM.ToString();
            lblLeftOUT.Text = opticsTest.LoutPSM.ToString();
            lblLeftS.Text = opticsTest.LattrS.ToString();
            lblLeftUP.Text = opticsTest.LupPSM.ToString();

            lblRightA.Text = opticsTest.RattrA.ToString();
            lblRightC.Text = opticsTest.RattrC.ToString();
            lblRightDOWN.Text = opticsTest.RdowmPSM.ToString();
            lblRightIN.Text = opticsTest.RinPSM.ToString();
            lblRightOUT.Text = opticsTest.RoutPSM.ToString();
            lblRightS.Text = opticsTest.RattrS.ToString();
            lblRightUP.Text = opticsTest.RupPSM.ToString();

            lblOpticsTestH.Text = opticsTest.OpticsTestH.ToString();
            lblOpticsTestV.Text = opticsTest.OpticsTestV.ToString();
        }
    }
}
