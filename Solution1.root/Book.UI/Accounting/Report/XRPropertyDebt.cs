using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Accounting.Report
{
    public partial class XRPropertyDebt : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtPropertyDebtManager detailManager = new Book.BL.AtPropertyDebtManager();
        IList<Model.AtPropertyDebt> oList = new List<Model.AtPropertyDebt>();
        public XRPropertyDebt(ConditionPropertyDebt condition)
        {
            InitializeComponent();
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "資產負債表";
            IList<Model.AtPropertyDebt> list = detailManager.Select(condition.StartDate, condition.EndDate);
            this.xrLabel1.Text = "日期區間：" + DateTime.Now.ToShortDateString();
            this.xrLabel2.Text = "列表日期：" + condition.StartDate.ToShortDateString() + "至" + condition.EndDate.ToShortDateString();

            
            this.DataSource = list;

            this.xrTableCell6.DataBindings.Add("Text", this.DataSource, Model.AtPropertyDebt.PRO_CategoriesName);
            this.xrTableCell7.DataBindings.Add("Text", this.DataSource, "Subject.AccountingCategory." + Model.AtAccountingCategory.PRO_AccountingCategoryName);
            this.xrTableCell8.DataBindings.Add("Text", this.DataSource, "Subject." + Model.AtAccountSubject.PRO_SubjectName);
            this.xrTableCell9.DataBindings.Add("Text", this.DataSource, Model.AtPropertyDebt.PRO_IsMoney, "{0:0}");
            this.xrTableCell10.DataBindings.Add("Text", this.DataSource, Model.AtPropertyDebt.PRO_AddMoney, "{0:0}");
        }

    }
}
