using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Book.UI.produceManager.PCPGOnlineCheck;

namespace Book.UI.produceManager.PCFirstOnlineCheck
{
    public partial class RO2 : DevExpress.XtraReports.UI.XtraReport
    {
        public RO2()
        {
            InitializeComponent();
        }

        public RO2(Model.PCFirstOnlineCheck model)
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

            this.DataSource = model.Detail.OrderBy(d => d.CheckDate).ToList();

            this.TC_InvoiceCusId.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_InvoiceXOCusId);
            this.TC_Product.DataBindings.Add("Text", DataSource, "Product.ProductName");
            this.TC_CheckDate.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_CheckDate, "{0:yyyy-MM-dd HH:mm}");
            this.TC_Biduixiandu.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_DuibiXiandu);
            this.xrTBusinessHours.DataBindings.Add("Text", DataSource, "BusinessHours." + Model.BusinessHours.PROPERTY_BUSINESSHOURSNAME);
            this.TC_Jihao.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Jihao);
            this.TC_Waiguan.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Waiguan);
            this.TC_Note.DataBindings.Add("Text", DataSource, Model.PCFirstOnlineCheckDetail.PRO_Remark);
            this.TC_Employee.DataBindings.Add("Text", DataSource, "Employee.EmployeeName");
        }

    }

}
