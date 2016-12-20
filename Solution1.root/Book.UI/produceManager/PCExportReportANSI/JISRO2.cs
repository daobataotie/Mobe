using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCExportReportANSI
{
    public partial class JISRO2 : DevExpress.XtraReports.UI.XtraReport
    {
        public JISRO2(Model.PCExportReportANSI _PCExportReportANSI, int tag)
        {
            InitializeComponent();

            if (tag == 1)
            {
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ALANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ALANSignature")[0].SettingCurrentValue;
            }
            else if (tag == 2)
            {
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("PPESignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("PPESignature")[0].SettingCurrentValue;
            }
            else if (tag == 0)
                this.lbl_Signature.Text = new BL.SettingManager().SelectByName("ASWANSignature").Count < 1 ? "" : new BL.SettingManager().SelectByName("ASWANSignature")[0].SettingCurrentValue;

            string[] test = new string[8];
            double[] number = new double[8] { _PCExportReportANSI.ShouCeShu11.Value, _PCExportReportANSI.ShouCeShu12.Value, _PCExportReportANSI.ShouCeShu13.Value, _PCExportReportANSI.ShouCeShu14.Value, _PCExportReportANSI.ShouCeShu15.Value, _PCExportReportANSI.ShouCeShu16.Value, _PCExportReportANSI.ShouCeShu17.Value, _PCExportReportANSI.ShouCeShu18.Value };
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
                        test[i + b] = "¡ª";
                        break;
                    }
                }
            }
            this.lbFinTestApp.Text = test[0].ToString();
            this.lbFinTestShock.Text = test[1].ToString();
            this.lbFinTestHCOSTE.Text = test[2].ToString();
            this.lbFinTestHCOS.Text = test[3].ToString();
            this.lbFinTestSOHAS.Text = test[4].ToString();
            this.lbFinTestSFD.Text = test[5].ToString();
            this.lbFinTestMfpro.Text = test[6].ToString();
            this.FinTestMfpac.Text = test[7].ToString();


            string[] pan = new string[11];
            double[] amount = new double[11] { _PCExportReportANSI.PanDing11.Value, _PCExportReportANSI.PanDingShu12.Value, _PCExportReportANSI.PanDingShu13.Value, _PCExportReportANSI.PanDingShu14.Value, _PCExportReportANSI.PanDingShu15.Value, _PCExportReportANSI.PanDingShu16.Value, _PCExportReportANSI.PanDingShu17.Value, _PCExportReportANSI.PanDingShu18.Value, _PCExportReportANSI.PanDingShu19.Value, _PCExportReportANSI.PanDingShu20.Value, _PCExportReportANSI.PanDingShu21.Value };
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
                        pan[j + a] = "¡ª";
                        break;
                    }
                }
            }

            this.lbFinJudgeApp.Text = pan[0].ToString();
            this.lbFinJudgeShock.Text = pan[1].ToString();
            this.lbFinJudgeHCOSTE.Text = pan[2].ToString();
            this.lbFinJudgeHCOS.Text = pan[3].ToString();
            this.lbFinJudgeSOHAS.Text = pan[4].ToString();
            this.lbFinJudgeSFD.Text = pan[5].ToString();
            this.lbFinJudgeCon.Text = pan[6].ToString();
            this.lbFinJudgeMat.Text = pan[7].ToString();
            this.lbFinJudgeMfpro.Text = pan[8].ToString();
            this.lbFinJudgeMfpac.Text = pan[9].ToString();
            this.lbFinJudgeIM.Text = pan[10].ToString();
        }

    }
}
