using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
namespace Book.UI.Accounting.Report
{
    public partial class XRNCashYF : DevExpress.XtraReports.UI.XtraReport
    {
        BL.AtBillsIncomeManager detailManager = new Book.BL.AtBillsIncomeManager();
        IList<Model.AtBillsIncome> oList = new List<Model.AtBillsIncome>();
        public XRNCashYF(ConditionNCashYF condition)
        {
            InitializeComponent();
            decimal? a = 0;
            this.xrLabelCompanyInfoName.Text = BL.Settings.CompanyChineseName;
            this.xrLabelDataName.Text = "未兌現應付票據";
            IList<Model.AtBillsIncome> list = detailManager.SelectOferAndDate(condition.StartDate, condition.EndDate, "尚未兌現");
            this.xrLabel2.Text = DateTime.Now.ToShortDateString();
            this.xrLabel1.Text = "日期区间：" + condition.StartDate.ToShortDateString() + "至" + condition.EndDate.ToShortDateString();
            if (list != null)
            {
                foreach (Model.AtBillsIncome at in list)
                {
                    if (at.IncomeCategory == "付票")
                    {
                        a += at.NotesMoney;
                        at.PassingObject = new BL.SupplierManager().Get(at.PassingObject) == null ? null : new BL.SupplierManager().Get(at.PassingObject).SupplierFullName;
                        at.CollectionAccount = new BL.AtBankAccountManager().Get(at.CollectionAccount) == null ? null : new BL.AtBankAccountManager().Get(at.CollectionAccount).BankAccountName;
                        oList.Add(at);
                    }
                }
            }
            this.DataSource = oList;
            this.xrLabel3.Text = "合計金額：" + a.ToString();
            this.xrTableCellTheOpenDate.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_TheJpy, "{0:yyyy-MM-dd}");
            this.xrTableCellBillsId.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_PassingObject);
            this.xrTableCellTheJpy.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_BillsOften);
            this.xrTableCell12.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_CollectionAccount);
            this.xrTableCell13.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_NotesAccount);
            this.xrTableCell14.DataBindings.Add("Text", this.DataSource, Model.AtBillsIncome.PRO_NotesMoney);
        }

    }
}
