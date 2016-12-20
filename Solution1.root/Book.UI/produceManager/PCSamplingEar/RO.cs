using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCSamplingEar
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.PCSamplingEar model)
            : this()
        {
            Model.PCSamplingEar PCSamplingEar = new BL.PCSamplingEarManager().GetDetail(model.PCSamplingEarId);
            this.DataSource = PCSamplingEar.Details;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = "品管抽检日报表(耳护类)";
            //this.lbl_PrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lbl_PCSamplingEarId.Text = PCSamplingEar.PCSamplingEarId;
            this.lbl_PCSamplingEarDate.Text = PCSamplingEar.PCSamplingEarDate.Value.ToString("yyyy-MM-dd");
            this.lbl_PronoteHeaderId.Text = PCSamplingEar.PronoteHeaderId;
            this.lbl_InvoiceCusId.Text = PCSamplingEar.InvoiceCusId;
            this.lbl_Customer.Text = PCSamplingEar.Customer == null ? "" : PCSamplingEar.Customer.CustomerFullName;
            this.lbl_model.Text = PCSamplingEar.Model;
            this.lbl_Note.Text = PCSamplingEar.Note;
            this.lbl_Employee.Text = PCSamplingEar.Employee == null ? "" : PCSamplingEar.Employee.EmployeeName;
            this.lblEmployee1.Text = PCSamplingEar.Employee1 == null ? "" : PCSamplingEar.Employee1.EmployeeName;

            TCProduct.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            TCCheckDate.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_PCSamplingEarDetailDate, "{0:yyyy-MM-dd HH:mm}");
            TCCheckNum.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_CheckNum, "{0:0}");
            TCGrade.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Grade);
            TCYinziToudaitao.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_YinziToudaitao);
            TCChaoyinbo.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Chaoyinbo);
            TCMihedu.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Mihedu);
            TCCLIPNaihua.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_CLIPNaihua);
            TCNailaoceshi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Nailaoceshi);
            TCFurongpiJiaoshui.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_FurongpiJiaoshui);
            TCBaozhuangdai.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Baozhuangdai);
            TCTiaomabiao.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Tiaomabiao);
            TCZhengcemai.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Zhengcemai);
            TCEryaceshi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Eryaceshi);
            TCLoufengceshi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Loufengceshi);
            TCZhuiluoceshi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Zhuiluoceshi);
            TCZhuiguaceshi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Zhuiguaceshi);
            TCNaizheceshi.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Naizheceshi);
            TCPassNum.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_PassNum, "{0:0}");
            TCNote.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Note);
            TC_Shensuojianniuju.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Shensuojianniuju);
            TC_Shensuojianshouli.DataBindings.Add("Text", this.DataSource, Model.PCSamplingEarDetail.PRO_Shensuojianshouli);
        }
    }
}
