using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCSampling
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.PCSampling model)
            : this()
        {
            Model.PCSampling PCSampling = new BL.PCSamplingManager().GetDetail(model.PCSamplingId);
            this.DataSource = PCSampling.Details;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = "组装检验日报表";
            //this.lbl_PrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lbl_PCSamplingId.Text = PCSampling.PCSamplingId;
            this.lbl_PCSamplingCheckDate.Text = PCSampling.PCSamplingDate.Value.ToString("yyyy-MM-dd");
            this.lbl_PronoteHeaderId.Text = PCSampling.PronoteHeaderId;
            this.lbl_InvoiceCusId.Text = PCSampling.InvoiceCusId;
            this.lbl_Customer.Text = PCSampling.Customer == null ? "" : PCSampling.Customer.CustomerFullName;
            this.lbl_model.Text = PCSampling.Model;
            this.lbl_Note.Text = PCSampling.Note;
            this.lbl_Employee.Text = PCSampling.Employee == null ? "" : PCSampling.Employee.EmployeeName;
            this.lblEmployee1.Text = PCSampling.Employee1 == null ? "" : PCSampling.Employee1.EmployeeName;

            TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            TCCheckDate.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_PCSamplingDetailDate, "{0:yyyy-MM-dd HH:mm}");
            TCCheckNum.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_CheckNum, "{0:0}");
            TCGrade.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Grade);
            TCWaiguan.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Waiguan);
            TCJiao.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Jiao);
            TCShensuojiao.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Shensuojiao);
            TCSuoluosi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Suoluosi);
            TCCashang.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Cashang);
            TCKuang.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Kuang);
            TCBidian.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Bidian);
            TCYinzi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Yinzi);
            TCBaozhuangdai.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Baozhuangdai);
            TCTiaomabiao.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Tiaomabiao);
            TCZhengcemai.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Zhengcemai);
            TCChongji.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Chongji);
            TCPassNum.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_PassNum, "{0:0}");
            TCToudai.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Toudai);
            TCTangzi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingDetail.PRO_Tangzi);
        }
    }
}
