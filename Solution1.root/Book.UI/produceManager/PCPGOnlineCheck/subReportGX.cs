using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace Book.UI.produceManager.PCPGOnlineCheck
{
    public partial class subReportGX : DevExpress.XtraReports.UI.XtraReport
    {
        /// <summary>
        /// �գ���ѧ/��ȱ�1����Ʒ���鵥��2���׼����߼���(��)
        /// </summary>
        string sign = string.Empty;
        private BL.OpticsTestManager _OpticsTestManager = new Book.BL.OpticsTestManager();

        public subReportGX()
        {
            InitializeComponent();
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(subReportGX_BeforePrint);


            this.TCcsbh.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_OpticsTestId);
            this.TCsdcsbh.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_ManualId);
            this.TCcsrq.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_OptiscTestDate, "{0:yyyy-MM-dd}");
            this.TCxzjx.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_MachineName);
            this.TCxztj.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_Condition);
            this.TCcsyg.DataBindings.Add("Text", this.DataSource, "Employee." + Model.Employee.PROPERTY_EMPLOYEENAME);

            this.TCls.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_LattrS, "{0:F2}");
            this.TCla.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_LattrA, "{0:F2}");
            this.TClc.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_LattrC, "{0:F2}");
            this.TClin.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_LinPSM, "{0:F2}");
            this.TClout.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_LoutPSM);
            this.TClup.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_LupPSM, "{0:F2}");
            this.TCldown.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_LdownPSM);

            this.TCrs.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_RattrS, "{0:F2}");
            this.TCra.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_RattrA, "{0:F2}");
            this.TCrc.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_RattrC, "{0:F2}");
            this.TCrin.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_RinPSM, "{0:F2}");
            this.TCrout.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_RoutPSM);
            this.TCrup.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_RupPSM, "{0:F2}");
            this.TCrdown.DataBindings.Add("Text", this.DataSource, Model.OpticsTest.PRO_RdowmPSM);
        }

        /// <summary>
        /// ��ӡ��ѧ��
        /// </summary>
        /// <param name="s">�գ���ѧ/��ȱ�1����Ʒ���鵥��2���׼����߼���(��)</param>
        public subReportGX(string s)
            : this()
        {
            this.sign = s;
        }

        public string _PCPGOnlineCheckDetailId { get; set; }

        private void subReportGX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.sign == string.Empty)
                this.DataSource = this._OpticsTestManager.mSelect(this._PCPGOnlineCheckDetailId);
            else if (this.sign == "1")
                this.DataSource = this._OpticsTestManager.FSelect(this._PCPGOnlineCheckDetailId);
            else
                this.DataSource = this._OpticsTestManager.PFCSelect(this._PCPGOnlineCheckDetailId);

            IList<Model.OpticsTest> list = this.DataSource as List<Model.OpticsTest>;
            if (list != null)
                foreach (var item in list)
                {
                    if (item.LeftLevelJudge == "OUT")
                    {
                        item.LoutPSM = item.LinPSM;
                        item.LinPSM = null;
                    }
                    if (item.LeftVerticalJudge == "DOWN")
                    {
                        item.LdownPSM = item.LupPSM;
                        item.LupPSM = null;
                    }
                    if (item.RightLevelJudge == "OUT")
                    {
                        item.RoutPSM = item.RinPSM;
                        item.RinPSM = null;
                    }
                    if (item.RightVerticalJudge == "DOWN")
                    {
                        item.RdowmPSM = item.RupPSM;
                        item.RupPSM = null;
                    }
                }
        }
    }
}