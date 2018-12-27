using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Book.UI.produceManager.PCFlameRetardant
{
    public partial class RO : DevExpress.XtraReports.UI.XtraReport
    {
        public RO(Model.PCFlameRetardant pCFlameRetardant)
        {
            InitializeComponent();

            this.DataSource = pCFlameRetardant.Details;

            this.lbl_CompanyName.Text = BL.Settings.CompanyChineseName;
            this.lbl_ReportName.Text = "阻燃性测试表";
            this.lbl_ReportDate.Text += DateTime.Now.ToString("yyyy-MM-dd");

            this.lbl_ID.Text = pCFlameRetardant.PCFlameRetardantId;
            this.lbl_InvoiceDate.Text = pCFlameRetardant.InvoiceDate.Value.ToString("yyyy-MM-dd");
            this.lbl_Employee.Text = pCFlameRetardant.Employee.ToString();
            this.lbl_Note.Text = pCFlameRetardant.Note;

            this.TC_Product.DataBindings.Add("Text", this.DataSource, "Product.ProductName");
            this.TCPihao.DataBindings.Add("Text", this.DataSource, Model.PCFlameRetardantDetail.PRO_Pihao);
            this.TCYanse.DataBindings.Add("Text", this.DataSource, Model.PCFlameRetardantDetail.PRO_Yanse);
            this.TCQianghua.DataBindings.Add("Text", this.DataSource, Model.PCFlameRetardantDetail.PRO_Qianghua);
            this.TCFangwu.DataBindings.Add("Text", this.DataSource, Model.PCFlameRetardantDetail.PRO_Fangwu);
            this.TCWuQianghua.DataBindings.Add("Text", this.DataSource, Model.PCFlameRetardantDetail.PRO_WuQianghua);
            this.TCRanshao.DataBindings.Add("Text", this.DataSource, Model.PCFlameRetardantDetail.PRO_Ranshao);
            this.TCEmployee.DataBindings.Add("Text", this.DataSource, "Employee.EmployeeName");
            this.TCTestQty.DataBindings.Add("Text", this.DataSource, Model.PCFlameRetardantDetail.PRO_TestQty);
            this.TCJudge.DataBindings.Add("Text", this.DataSource, Model.PCFlameRetardantDetail.PRO_Judge);
        }

    }
}
