using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCIncomingCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.PCIncomingCheck model)
            : this()
        {
            Model.PCIncomingCheck PCIncomingCheck = new BL.PCIncomingCheckManager().GetDetail(model.PCIncomingCheckId);
            this.DataSource = PCIncomingCheck.Detail;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = "入料检验单";
            //this.lbl_PrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.lbl_PCIncomingCheckId.Text = model.PCIncomingCheckId;
            this.lbl_PurchaseDate.Text = model.PurchaseDate.HasValue ? model.PurchaseDate.Value.ToString("yyyy-MM-dd") : "";
            this.lbl_IncomingDate.Text = model.IncomingDate.HasValue ? model.IncomingDate.Value.ToString("yyyy-MM-dd") : "";
            this.lbl_CheckDate.Text = model.CheckDate.HasValue ? model.CheckDate.Value.ToString("yyyy-MM-dd") : "";
            this.lbl_MaterialCategory.Text = model.MaterialCategory;
            this.lbl_Pihao.Text = model.Note;

            this.lbl_Employee.Text = PCIncomingCheck.Employee == null ? "" : PCIncomingCheck.Employee.EmployeeName;

            //TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            TCProduct.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_ProductName);
            TCSehao.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_Sehao);
            TCPihao.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_Pihao);
            TCGuangxue.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_Guangxue);
            TCToushilv.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_Toushilv);
            TCWaiguan.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_Waiguan);
            TCChongji.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_Chongji);
            TCUVValue.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_UVValue);
            TCNairanceshi.DataBindings.Add("Text", this.DataSource, Model.PCIncomingCheckDetail.PRO_Nairanceshi);
        }
    }
}
