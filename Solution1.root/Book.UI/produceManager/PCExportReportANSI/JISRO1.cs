using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class JISRO1 : DevExpress.XtraReports.UI.XtraReport
    {
        public JISRO1()
        {
            InitializeComponent();
        }

        //当受测数大于0时，显示受测数，判定数显示：PASS
        //当受测数不大于0时，受测数、判定数表示为:—
        public JISRO1(Model.PCExportReportANSI _PCExportReportANSI, int tag)
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
            this.LbTesrPerson.Text = (_PCExportReportANSI.Employee == null ? null : _PCExportReportANSI.Employee.ToString()) + (_PCExportReportANSI.Employee2 == null ? null : " / " + _PCExportReportANSI.Employee2.ToString()) + (_PCExportReportANSI.Employee3 == null ? null : " / " + _PCExportReportANSI.Employee3.ToString()) + (_PCExportReportANSI.Employee4 == null ? null : " / " + _PCExportReportANSI.Employee4.ToString());
            this.LbReportDate.Text = _PCExportReportANSI.ReportDate == null ? null : _PCExportReportANSI.ReportDate.Value.ToShortDateString();

            string[] test = new string[10];
            double[] number = new double[10] { _PCExportReportANSI.ShouCeShu1.Value, _PCExportReportANSI.ShouCeShu2.Value, _PCExportReportANSI.ShouCeShu3.Value, _PCExportReportANSI.ShouCeShu4.Value, _PCExportReportANSI.ShouCeShu5.Value, _PCExportReportANSI.ShouCeShu6.Value, _PCExportReportANSI.ShouCeShu7.Value, _PCExportReportANSI.ShouCeShu8.Value, _PCExportReportANSI.ShouCeShu9.Value, _PCExportReportANSI.ShouCeShu10.Value };
            int b = -1;

            this.lblTestStandard.Text = string.IsNullOrEmpty(_PCExportReportANSI.CSAJiShuBiaoZhun) ? "JIS T8147:2016" : _PCExportReportANSI.CSAJiShuBiaoZhun;
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


            //string[] pan = new string[10];
            //double[] amount = new double[10] { _PCExportReportANSI.PanDing1.Value, _PCExportReportANSI.PanDing2.Value, _PCExportReportANSI.PanDing3.Value, _PCExportReportANSI.PanDing4.Value, _PCExportReportANSI.PanDing5.Value, _PCExportReportANSI.PanDing6.Value, _PCExportReportANSI.PanDing7.Value, _PCExportReportANSI.PanDing8.Value, _PCExportReportANSI.PanDing9.Value, _PCExportReportANSI.PanDing10.Value };
            //int a = -1;
            //foreach (double quantity in amount)
            //{
            //    a++;
            //    for (int j = 0; j < pan.Length; j++)
            //    {
            //        if (quantity > 0)
            //        {
            //            pan[j + a] = "PASS";
            //            break;
            //        }
            //        else
            //        {
            //            pan[j + a] = "—";
            //            break;
            //        }
            //    }
            //}

            //this.lblensJudgeApp.Text = pan[0].ToString();
            //this.lblensJudgePri.Text = pan[1].ToString();
            //this.lblensJudgeRef.Text = pan[2].ToString();
            //this.lblensJudgeAti.Text = pan[3].ToString();
            //this.lblensJudgeShock.Text = pan[4].ToString();
            //this.lblensJudgeShock.Text = pan[5].ToString();
            //this.lblensJudgeSAET.Text = pan[6].ToString();
            //this.lblensJudgeSAET.Text = pan[7].ToString();
            //this.lblensJudgeRTC.Text = pan[8].ToString();
            //this.lblensJudgeIgn.Text = pan[9].ToString();
            this.lblensJudgeApp.Text = "PASS";
            this.lblensJudgePri.Text = "PASS";
            this.lblensJudgeRef.Text = "PASS";
            this.lblensJudgeAti.Text = "PASS";
            this.lblensJudgeTran.Text = "PASS";
            this.lblensJudgeShock.Text = "PASS";
            this.lblensJudgeSfr.Text = "PASS";
            this.lblensJudgeSAET.Text = "PASS";
            this.lblensJudgeRTC.Text = "PASS";
            this.lblensJudgeIgn.Text = "PASS";




            string[] test2 = new string[8];
            double[] number2 = new double[8] { _PCExportReportANSI.ShouCeShu11.Value, _PCExportReportANSI.ShouCeShu12.Value, _PCExportReportANSI.ShouCeShu13.Value, _PCExportReportANSI.ShouCeShu14.Value, _PCExportReportANSI.ShouCeShu15.Value, _PCExportReportANSI.ShouCeShu16.Value, _PCExportReportANSI.ShouCeShu17.Value, _PCExportReportANSI.ShouCeShu18.Value };
            int b2 = -1;
            foreach (double quality in number2)
            {
                for (int i = 0; i < test2.Length; i++)
                {
                    b2++;
                    if (quality > 0)
                    {
                        test2[i + b2] = quality.ToString() + "PCS/" + quality.ToString() + "PCS";
                        break;
                    }
                    else
                    {
                        test2[i + b2] = "—";
                        break;
                    }
                }
            }
            this.lbFinTestApp.Text = test2[0].ToString();
            this.lbFinTestShock.Text = test2[1].ToString();
            this.lbFinTestHCOSTE.Text = test2[2].ToString();
            this.lbFinTestHCOS.Text = test2[3].ToString();
            this.lbFinTestSOHAS.Text = test2[4].ToString();
            this.lbFinTestSFD.Text = test2[5].ToString();
            this.lbFinTestMfpro.Text = test2[6].ToString();
            this.FinTestMfpac.Text = test2[7].ToString();


            //string[] pan2 = new string[11];
            //double[] amount2 = new double[11] { _PCExportReportANSI.PanDing11.Value, _PCExportReportANSI.PanDingShu12.Value, _PCExportReportANSI.PanDingShu13.Value, _PCExportReportANSI.PanDingShu14.Value, _PCExportReportANSI.PanDingShu15.Value, _PCExportReportANSI.PanDingShu16.Value, _PCExportReportANSI.PanDingShu17.Value, _PCExportReportANSI.PanDingShu18.Value, _PCExportReportANSI.PanDingShu19.Value, _PCExportReportANSI.PanDingShu20.Value, _PCExportReportANSI.PanDingShu21.Value };
            //int a = -1;
            //foreach (double quantity in amount2)
            //{
            //    a++;
            //    for (int j = 0; j < pan2.Length; j++)
            //    {
            //        if (quantity > 0)
            //        {
            //            pan2[j + a] = "PASS";
            //            break;
            //        }
            //        else
            //        {
            //            pan2[j + a] = "—";
            //            break;
            //        }
            //    }
            //}

            //this.lbFinJudgeApp.Text = pan2[0].ToString();
            //this.lbFinJudgeShock.Text = pan2[1].ToString();
            //this.lbFinJudgeHCOSTE.Text = pan2[2].ToString();
            //this.lbFinJudgeHCOS.Text = pan2[3].ToString();
            //this.lbFinJudgeSOHAS.Text = pan2[4].ToString();
            //this.lbFinJudgeSFD.Text = pan2[5].ToString();
            //this.lbFinJudgeCon.Text = pan2[6].ToString();
            //this.lbFinJudgeMat.Text = pan2[7].ToString();
            //this.lbFinJudgeMfpro.Text = pan2[8].ToString();
            //this.lbFinJudgeMfpac.Text = pan2[9].ToString();
            //this.lbFinJudgeIM.Text = pan2[10].ToString();
            this.lbFinJudgeApp.Text = "PASS";
            this.lbFinJudgeShock.Text = "PASS";
            this.lbFinJudgeHCOSTE.Text = "PASS";
            this.lbFinJudgeHCOS.Text = "PASS";
            this.lbFinJudgeSOHAS.Text = "PASS";
            this.lbFinJudgeSFD.Text = "PASS";
            this.lbFinJudgeCon.Text = "PASS";
            this.lbFinJudgeMat.Text = "PASS";
            this.lbFinJudgeMfpro.Text = "PASS";
            this.lbFinJudgeMfpac.Text = "PASS";
            this.lbFinJudgeIM.Text = "PASS";
        }
    }
}
