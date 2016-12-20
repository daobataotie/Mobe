using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCMaterialCheck
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO()
        {
            InitializeComponent();
        }

        public RO(Model.PCMaterialCheck model)
            : this()
        {
            Model.PCMaterialCheck PCMaterialCheck = new BL.PCMaterialCheckManager().GetDetail(model.PCMaterialCheckId);
            foreach (var detail in PCMaterialCheck.Details)
            {
                foreach (var item in detail.GetType().GetProperties())
                {
                    if (item.Name != "PCMaterialCheckDetailId" && item.Name != "PCMaterialCheckId" && item.Name != "CheckDate" && item.Name != "ProductId")
                    {
                        if (item.GetValue(detail, null) != null)
                        {
                            switch (item.GetValue(detail, null).ToString())
                            {
                                case "0":
                                    item.SetValue(detail, "√", null);
                                    break;
                                case "1":
                                    item.SetValue(detail, "×", null);
                                    break;
                                case "2":
                                    item.SetValue(detail, "△", null);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            this.DataSource = PCMaterialCheck.Details;

            this.lblCompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = "物料检验单";
            //this.lbl_PrintDate.Text += DateTime.Now.ToString("yyyy-MM-dd");
            this.lbl_PCMaterialCheckId.Text = PCMaterialCheck.PCMaterialCheckId;
            this.lbl_PCMaterialCheckDate.Text = PCMaterialCheck.PCMaterialCheckDate.Value.ToString("yyyy-MM-dd");
            this.lbl_InvoiceCOId.Text = PCMaterialCheck.InvoiceCOId;
            this.lbl_InvoiceCusId.Text = PCMaterialCheck.InvoiceCusId;
            this.lbl_Note.Text = PCMaterialCheck.Note;
            this.lbl_Employee.Text = PCMaterialCheck.Employee == null ? "" : PCMaterialCheck.Employee.ToString();

            TCCheckDate.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_CheckDate, "{0:yyyy-MM-dd}");
            TCProductId.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_Id);
            TCProductName.DataBindings.Add("Text", this.DataSource, "Product." + Model.Product.PRO_ProductName);
            TCWaiguan.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Waiguan);
            TCZhengcemai.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Zhengcemai);
            TCGuige.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Guige);
            TCChouyangshu.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Chouyangshu);
            TCZhandu.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Zhandu);
            TCDuise.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Duise);
            TCTiaoma.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Tiaoma);
            TCZhiliang.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Zhiliang);
            TCShiyongxingneng.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Shiyongxingneng);
            TCToushilv.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Toushilv);
            TCWaiguanfuzhuodu.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_WaiguanFuzhuodu);
            TCNeirong.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Neirong);
            TCMushijianyan.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Mushijianyan);
            TCWaiguanduise.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_DuiseWaiguan);
            TCRuliaojianyan.DataBindings.Add("Text", this.DataSource, Model.PCMaterialCheckDetail.PRO_Ruliaojianyan);
        }
    }
}
