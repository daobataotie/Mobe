using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Accounting.Report
{
    public partial class XRProfitLoss : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtProfitLossManager detailManager = new Book.BL.AtProfitLossManager();
        IList<Model.AtProfitLoss> oList = new List<Model.AtProfitLoss>();
        public XRProfitLoss(ConditionProfitLoss condition)
        {
            InitializeComponent();
            double? a = 0;
            double? b = 0;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "�p���";
            IList<Model.AtProfitLoss> list = detailManager.Select(condition.StartDate, condition.EndDate);
            this.xrLabel1.Text = "���څ^�g��" + DateTime.Now.ToShortDateString();
            this.xrLabel2.Text = "�б����ڣ�" + condition.StartDate.ToShortDateString() + "��" + condition.EndDate.ToShortDateString();

            if (list != null)
            {
                foreach (Model.AtProfitLoss at in list)
                {
                    at.CategoriesName = at.CategoriesName + "/" + at.SubjectName;
                    a+=at.ThisMoney;
                    b+=at.IsMoney;
                    oList.Add(at);
                }
            }
            this.DataSource = oList;
            this.xrLabel4.Text = a.ToString();
            this.xrLabel5.Text = b.ToString();

            this.xrTableCell4.DataBindings.Add("Text", this.DataSource, Model.AtProfitLoss.PRO_CategoriesName);
            this.xrTableCell5.DataBindings.Add("Text", this.DataSource, Model.AtProfitLoss.PRO_ThisMoney, "{0:0}");
            this.xrTableCell6.DataBindings.Add("Text", this.DataSource, Model.AtProfitLoss.PRO_IsMoney, "{0:0}");
        }
    }
}
