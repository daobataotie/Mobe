using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCFinishCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        string _PCFinishCheckId;
        public RO(Model.PCFinishCheck _pcfc)
        {
            InitializeComponent();
            if (_pcfc == null)
                return;
            //CompanyInfo
            this._PCFinishCheckId = _pcfc.PCFinishCheckID;
            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            //this.lblDataName.Text = Properties.Resources.PCFinishCheck;
            //this.lblPrintDate.Text += DateTime.Now.ToShortDateString();

            //Details Controls
            this.lblPCFinishCheckID.Text = _pcfc.PCFinishCheckID;
            this.lblPCFinishCheckDate.Text = _pcfc.PCFinishCheckDate.HasValue ? _pcfc.PCFinishCheckDate.Value.ToShortDateString() : "";
            this.lblProductName.Text = _pcfc.Product == null ? "" : _pcfc.Product.ToString();
            this.lblBuMen.Text = _pcfc.WorkHouse == null ? "" : _pcfc.WorkHouse.ToString();
            this.lblInvoiceCusXOId.Text = _pcfc.InvoiceCusXOId;
            this.lblPCFinishCheckCount.Text = _pcfc.PCFinishCheckCount.ToString();
            this.lblPCFinishCheckInCoiunt.Text = _pcfc.PCFinishCheckInCoiunt.HasValue ? _pcfc.PCFinishCheckInCoiunt.Value.ToString() : "";
            this.lblPCFinishCheckDesc.Text = _pcfc.PCFinishCheckDesc;
            this.lblEmployee0.Text = _pcfc.Employee0 == null ? "" : _pcfc.Employee0.ToString();
            this.lblEmployee1.Text = _pcfc.Employee1 == null ? "" : _pcfc.Employee1.ToString();
            this.lblCustomerProductName.Text = _pcfc.Product.CustomerProductName;
            this.lblPronoteHeardId.Text = _pcfc.PronoteHeaderID;
            this.lblCustomerProductName.Text = _pcfc.CustomerProductName;
            this.lbl_proudctunit.Text = _pcfc.ProductUnit == null ? "" : _pcfc.ProductUnit.ToString();

            //this.lblId.Text = _pcfc.PCFinishCheckID;
            //this.lblDate.Text = _pcfc.PCFinishCheckDate.HasValue ? _pcfc.PCFinishCheckDate.Value.ToString("yyyy-MM-dd") : "";
            //this.lblJiaGongId.Text = _pcfc.PronoteHeaderID;

            this.lblEmployee01.Text = _pcfc.EmployeeCheck1 == null ? "" : _pcfc.EmployeeCheck1.ToString();
            this.lblEmployee02.Text = _pcfc.EmployeeCheck2 == null ? "" : _pcfc.EmployeeCheck2.ToString();
            this.lblEmployee03.Text = _pcfc.EmployeeCheck3 == null ? "" : _pcfc.EmployeeCheck3.ToString();
            this.lblEmployee04.Text = _pcfc.EmployeeCheck4 == null ? "" : _pcfc.EmployeeCheck4.ToString();
            //this.lblEmployee05.Text = _pcfc.EmployeeCheck5 == null ? "" : _pcfc.EmployeeCheck5.ToString();
            this.lbl_AuditEmployee.Text = _pcfc.AuditEmp == null ? "" : _pcfc.AuditEmp.EmployeeName;

            //details
            this.lblChanpinyanse.Text = Trans(_pcfc.AttrChanpinyanse);
            this.lblChanpinjihao.Text = Trans(_pcfc.AttrChanpinjihao);
            this.lblQitapeijianjihao.Text = Trans(_pcfc.AttrQitapeijianjihao);
            this.lblErhuzuzhuang.Text = Trans(_pcfc.AttrErhuzuzhuang);
            this.lblDZDWQDW.Text = Trans(_pcfc.AttrDZDWQDW);
            this.lblJWYHWRL.Text = Trans(_pcfc.AttrJWYHWRL);
            this.lblGZBKYRL.Text = Trans(_pcfc.AttrGZBKYRL);
            this.lblZZWBXGJ.Text = Trans(_pcfc.AttrZZWBXGJ);
            this.lblJingpianbukeguashang.Text = Trans(_pcfc.AttrJPBKGS);
            this.lblJingjiaoshifoutaisong.Text = Trans(_pcfc.AttrJJSFTSYH);

            this.lblSujiaodaikuanxing.Text = Trans(_pcfc.AttrSujiaodaikuanxing);
            this.lblsujiaodaiMF.Text = Trans(_pcfc.AttrSLDSFMF);
            this.lblSujiaodaitiebiao.Text = Trans(_pcfc.AttrSujiaodaitiebiao);
            this.lblNeihekuanxing.Text = Trans(_pcfc.AttrNeihekuanxing);
            this.lblNHTB.Text = Trans(_pcfc.AttrNHTB);
            this.lblWaixiangtiebiao.Text = Trans(_pcfc.AttrWXTB);
            this.lblZhengcemai.Text = Trans(_pcfc.AttrZMCM);
            this.lblJingsheng.Text = Trans(_pcfc.AttrJSSFZQ);
            this.lblJingdai.Text = Trans(_pcfc.AttrJDZRFS);
            //this.lblChuhuochongji.Text = Trans(_pcfc.AttrChuhuochongji);
            this.lblChuhuochongji.Text = _pcfc.AttrCheckStandard;
            this.lblESSSFZH.Text = Trans(_pcfc.AttrESSSFZH);
            this.lblESSFYGZTZ.Text = Trans(_pcfc.AttrESSFYGZTZ);

        }

        private string Trans(int? a)
        {
            string str = string.Empty;
            switch (a.HasValue ? a.Value : -1)
            {
                case -1:
                    str = "";
                    break;
                case 0:
                    str = "¡Ì";
                    break;
                case 1:
                    str = "X";
                    break;
                case 2:
                    str = "¡÷";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }

        private void subReportGX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //PCPGOnlineCheck.subReportGX subGX = this.xrSubreportGX.ReportSource as PCPGOnlineCheck.subReportGX;
            //subGX._PCPGOnlineCheckDetailId = this._PCFinishCheckId;
        }
    }
}
