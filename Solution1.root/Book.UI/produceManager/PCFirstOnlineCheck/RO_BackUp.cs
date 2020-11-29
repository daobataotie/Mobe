using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Book.UI.produceManager.PCPGOnlineCheck;

namespace Book.UI.produceManager.PCFirstOnlineCheck
{
    public partial class RO_BackUp : DevExpress.XtraReports.UI.XtraReport
    {
        public RO_BackUp()
        {
            InitializeComponent();
        }

        public RO_BackUp(Model.PCFirstOnlineCheck model)
            : this()
        {
            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            //this.lbl_ReportName.Text = "首件上线检查表";

            this.lbl_Id.Text = model.PCFirstOnlineCheckId;
            this.lbl_OnlineDate.Text = model.OnlineDate.Value.ToString("yyyy-MM-dd");

            this.lbl_PronoteHeaderId.Text = model.PronoteHeaderId;

            this.lblCheckNum.Text = model.CheckNum == null ? "" : model.CheckNum.Value.ToString("0.##");
            this.lblPassNum.Text = model.PassNum == null ? "" : model.PassNum.Value.ToString("0.##");
            this.lblProductUnit.Text = model.ProductUnit;

            this.lbl_CustomerProduct.Text = model.PronoteHeader == null ? "" : model.PronoteHeader.Product.CustomerProductName;
            this.lbl_CheckedStandard.Text = model.CheckedStandard;

            this.DataSource = model.Detail.OrderBy(d => d.CheckDate).ToList();

            this.TC_InvoiceCusId.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_InvoiceXOCusId);
            this.TC_Product.DataBindings.Add("Text", DataSource, "Product.ProductName");
            this.TC_CheckDate.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_CheckDate, "{0:yyyy-MM-dd HH:mm}");
            this.TC_Biduixiandu.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_DuibiXiandu);
            this.xrTBusinessHours.DataBindings.Add("Text", DataSource, "BusinessHours." + Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
            this.TC_Jihao.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Jihao);
            this.TC_Waiguan.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Waiguan);
            this.TC_Guangxue.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Guangxue);
            this.TC_Houdu.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Houdu);
            this.TC_Chongji.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Chongji);
            this.TC_Note.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Remark);
            this.TC_Employee.DataBindings.Add("Text", DataSource, "Employee.EmployeeName");

            this.subReportGX.ReportSource = new subReportGX("2");
            this.subReportHD.ReportSource = new subReportHD("2");
            this.subreportChongji.ReportSource = new ROImpactCheck();
        }

        private void subReportGX_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportGX subGX = this.subReportGX.ReportSource as subReportGX;
            Model.PCFirstOnlineCheckDetail detail = this.GetCurrentRow() as Model.PCFirstOnlineCheckDetail;
            if (detail != null && detail.Guangxue != "△")
                subGX._PCPGOnlineCheckDetailId = detail.PCFirstOnlineCheckDetailId;
        }

        private void subReportHD_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            subReportHD subHD = this.subReportHD.ReportSource as subReportHD;
            Model.PCFirstOnlineCheckDetail detail = this.GetCurrentRow() as Model.PCFirstOnlineCheckDetail;
            if (detail != null && detail.Houdu != "△")
                subHD._PCPGOnlineCheckDetailId = detail.PCFirstOnlineCheckDetailId;
        }

        private void subreportChongji_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ROImpactCheck subChongji = this.subreportChongji.ReportSource as ROImpactCheck;
            Model.PCFirstOnlineCheckDetail detail = this.GetCurrentRow() as Model.PCFirstOnlineCheckDetail;
            if (detail != null && detail.Chongji != "△")
                subChongji.PCFirstOnlineCheckDetailId = detail.PCFirstOnlineCheckDetailId;
        }

    }

}
