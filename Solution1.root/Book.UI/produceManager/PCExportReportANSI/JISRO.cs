using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class JISRO : DevExpress.XtraReports.UI.XtraReport
    {
        public JISRO()
        {
            InitializeComponent();
        }

        //当受测数大于0时，显示受测数，判定数显示：PASS
        //当受测数不大于0时，受测数、判定数表示为:—
        public JISRO(Model.PCExportReportANSI _PCExportReportANSI, int tag)
            : this()
        {
            if (tag == 1)
            {
                this.xrLabel8.Text = "ALAN    SAFETY    COMPANY.";
            }
            if (tag == 2)
            {
                this.xrLabel8.Text = "PPE   SAFETY   INC.";
            }
            else if (tag == 3)
            {
                this.xrLabel8.Text = "鏡片品質標準檢測表";
            }

            this.LbCustomer.Text = _PCExportReportANSI.Customer == null ? null : _PCExportReportANSI.Customer.CustomerName;
            this.LbOrderId.Text = _PCExportReportANSI.InvoiceCusXOId == null ? null : _PCExportReportANSI.InvoiceCusXOId.ToString();
            this.LbProduct.Text = _PCExportReportANSI.Product.CustomerProductName == null ? null : _PCExportReportANSI.Product.CustomerProductName.ToString();
            this.LbOrderAmount.Text = (_PCExportReportANSI.Amount.HasValue ? _PCExportReportANSI.Amount.ToString() : "0") + "PCS";
            this.LbTesrPerson.Text = _PCExportReportANSI.Employee == null ? null : _PCExportReportANSI.Employee.ToString();
            this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToShortDateString();

            string[] test = new string[10];
            double[] number = new double[10] { _PCExportReportANSI.ShouCeShu1.Value, _PCExportReportANSI.ShouCeShu2.Value, _PCExportReportANSI.ShouCeShu3.Value, _PCExportReportANSI.ShouCeShu4.Value, _PCExportReportANSI.ShouCeShu5.Value, _PCExportReportANSI.ShouCeShu6.Value, _PCExportReportANSI.ShouCeShu7.Value, _PCExportReportANSI.ShouCeShu8.Value, _PCExportReportANSI.ShouCeShu9.Value, _PCExportReportANSI.ShouCeShu10.Value };
            int b = -1;
            foreach (double quality in number)
            {
                for (int i = 0; i < test.Length; i++)
                {
                    b++;
                    if (quality > 0)
                    {
                        test[i + b] = quality.ToString() + "PCS/" + quality.ToString() + "PCS";
                        break;
                    }
                    else
                    {
                        test[i + b] = "—";
                        break;
                    }
                }
            }

            this.lblensTestApp.Text = test[0].ToString();
            this.lblensTestPri.Text = test[1].ToString();
            this.lblensTestRef.Text = test[2].ToString();
            this.lblensTestAti.Text = test[3].ToString();
            this.lblensTestTran.Text = test[4].ToString();
            this.lblensTestShock.Text = test[5].ToString();
            this.lblensTestSfr.Text = test[6].ToString();
            this.lblensTestSAET.Text = test[7].ToString();
            this.lblensTestRTC.Text = test[8].ToString();
            this.lblensTestIgn.Text = test[9].ToString();
            //this.lbFinTestApp.Text = test[10].ToString();
            //this.lbFinTestShock.Text = test[11].ToString();
            //this.lbFinTestHCOSTE.Text = test[12].ToString();
            //this.lbFinTestHCOS.Text = test[13].ToString();
            //this.lbFinTestSOHAS.Text = test[14].ToString();
            //this.lbFinTestSFD.Text = test[15].ToString();
            //this.lbFinTestMfpro.Text = test[16].ToString();
            //this.FinTestMfpac.Text = test[17].ToString();


            string[] pan = new string[10];
            double[] amount = new double[10] { _PCExportReportANSI.PanDing1.Value, _PCExportReportANSI.PanDing2.Value, _PCExportReportANSI.PanDing3.Value, _PCExportReportANSI.PanDing4.Value, _PCExportReportANSI.PanDing5.Value, _PCExportReportANSI.PanDing6.Value, _PCExportReportANSI.PanDing7.Value, _PCExportReportANSI.PanDing8.Value, _PCExportReportANSI.PanDing9.Value, _PCExportReportANSI.PanDing10.Value };
            int a = -1;
            foreach (double quantity in amount)
            {
                a++;
                for (int j = 0; j < pan.Length; j++)
                {
                    if (quantity > 0)
                    {
                        pan[j + a] = "PASS";
                        break;
                    }
                    else
                    {
                        pan[j + a] = "—";
                        break;
                    }
                }
            }

            this.lblensJudgeApp.Text = pan[0].ToString();
            this.lblensJudgePri.Text = pan[1].ToString();
            this.lblensJudgeRef.Text = pan[2].ToString();
            this.lblensJudgeAti.Text = pan[3].ToString();
            this.lblensJudgeTran.Text = pan[4].ToString();
            this.lblensJudgeShock.Text = pan[5].ToString();
            this.lblensJudgeSfr.Text = pan[6].ToString();
            this.lblensJudgeSAET.Text = pan[7].ToString();
            this.lblensJudgeRTC.Text = pan[8].ToString();
            this.lblensJudgeIgn.Text = pan[9].ToString();
            //this.lbFinJudgeApp.Text = pan[10].ToString();
            //this.lbFinJudgeShock.Text = pan[11].ToString();
            //this.lbFinJudgeHCOSTE.Text = pan[12].ToString();
            //this.lbFinJudgeHCOS.Text = pan[13].ToString();
            //this.lbFinJudgeSOHAS.Text = pan[14].ToString();
            //this.lbFinJudgeSFD.Text = pan[15].ToString();
            //this.lbFinJudgeCon.Text = pan[16].ToString();
            //this.lbFinJudgeMat.Text = pan[17].ToString();
            //this.lbFinJudgeMfpro.Text = pan[18].ToString();
            //this.lbFinJudgeMfpac.Text = pan[19].ToString();
            //this.lbFinJudgeIM.Text = pan[20].ToString();
        }
    }
}
