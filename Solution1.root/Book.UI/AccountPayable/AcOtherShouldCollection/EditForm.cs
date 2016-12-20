using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.AccountPayable.AcOtherShouldCollection
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.AcOtherShouldCollection AcOtherShouldCollection = new Book.Model.AcOtherShouldCollection();

        BL.AcOtherShouldCollectionManager AcOtherShouldCollectionManager = new Book.BL.AcOtherShouldCollectionManager();

        BL.AcOtherShouldCollectionDetailManager AcOtherShouldCollectionDetailManager = new Book.BL.AcOtherShouldCollectionDetailManager();

        public EditForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.requireValueExceptions.Add(Model.AcOtherShouldCollection.PRO_AcOtherShouldCollectionId, new AA(Properties.Resources.RequireDataForId, this.textEditAcOtherShouldCollectionId));
            this.invalidValueExceptions.Add(Model.AcOtherShouldCollection.PRO_InvoiceHeji, new AA("違反規則:<稅額+合計=總額>,請檢查數據", this.calcHeJiMoney));
            //this.newChooseContorlAtCurrencyCategoryId.Choose = new Accounting.CurrencyCategory.ChooseAtCurrencyCategory();
            this.newChooseContorlCustomerId.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseContorlEmployeeId.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployee1Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployee0Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccCompany.Choose = new Settings.BasicData.Company.ChooseCompany();
            this.action = "view";
        }

        public EditForm(Model.AcOtherShouldCollection AcOtherShouldCollection)
            : this()
        {
            this.AcOtherShouldCollection = AcOtherShouldCollection;
            this.AcOtherShouldCollection.Details = this.AcOtherShouldCollectionDetailManager.Select(AcOtherShouldCollection);
            this.action = "update";
        }

        public EditForm(Model.AcOtherShouldCollection AcOtherShouldCollection, string action)
            : this()
        {
            this.AcOtherShouldCollection = AcOtherShouldCollection;
            this.AcOtherShouldCollection.Details = this.AcOtherShouldCollectionDetailManager.Select(AcOtherShouldCollection);
            this.action = action;
        }

        protected override void Save()
        {
            this.AcOtherShouldCollection.AcOtherShouldCollectionId = this.textEditAcOtherShouldCollectionId.Text;
            //  this.AcOtherShouldCollection.InvoiceId = this.textEditInvoiceId.Text;
            this.AcOtherShouldCollection.InvoiceId = this.textEditFPId.Text;
            this.AcOtherShouldCollection.BillMoney = this.calcFPMoney.Value;
            this.AcOtherShouldCollection.ObjectName = this.textEditObjectName.Text;
            //  this.AcOtherShouldCollection.AtCurrencyCategory = this.newChooseContorlAtCurrencyCategoryId.EditValue as Model.AtCurrencyCategory;
            if (this.AcOtherShouldCollection.AtCurrencyCategory != null)
            {
                this.AcOtherShouldCollection.AtCurrencyCategoryId = this.AcOtherShouldCollection.AtCurrencyCategory.AtCurrencyCategoryId;
            }
            this.AcOtherShouldCollection.Customer = this.newChooseContorlCustomerId.EditValue as Model.Customer;
            if (this.AcOtherShouldCollection.Customer != null)
            {
                this.AcOtherShouldCollection.CustomerId = this.AcOtherShouldCollection.Customer.CustomerId;
            }
            this.AcOtherShouldCollection.Employee = this.newChooseContorlEmployeeId.EditValue as Model.Employee;
            if (this.AcOtherShouldCollection.Employee != null)
            {
                this.AcOtherShouldCollection.EmployeeId = this.AcOtherShouldCollection.Employee.EmployeeId;
            }
            this.AcOtherShouldCollection.Employee = this.newChooseContorlEmployee0Id.EditValue as Model.Employee;
            if (this.AcOtherShouldCollection.Employee != null)
            {
                this.AcOtherShouldCollection.Employee0Id = this.AcOtherShouldCollection.Employee.EmployeeId;
            }
            this.AcOtherShouldCollection.Employee = this.newChooseContorlEmployee1Id.EditValue as Model.Employee;
            if (this.AcOtherShouldCollection.Employee != null)
            {
                this.AcOtherShouldCollection.Employee1Id = this.AcOtherShouldCollection.Employee.EmployeeId;
            }
            this.AcOtherShouldCollection.Company = this.nccCompany.EditValue as Model.Company;
            if (this.AcOtherShouldCollection.Company != null)
            {
                this.AcOtherShouldCollection.CompanyId = this.AcOtherShouldCollection.Company.CompanyId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditAcOtherShouldCollectionDate.DateTime, new DateTime()))
            {
                this.AcOtherShouldCollection.AcOtherShouldCollectionDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.AcOtherShouldCollection.AcOtherShouldCollectionDate = this.dateEditAcOtherShouldCollectionDate.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditAdvancePayableDate.DateTime, new DateTime()))
            {
                this.AcOtherShouldCollection.AdvancePayableDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.AcOtherShouldCollection.AdvancePayableDate = this.dateEditAdvancePayableDate.DateTime;
            }
            //  this.AcOtherShouldCollection.ExchangeRate = this.spinEditExchangeRate.EditValue == null ? 0 :double.Parse(this.spinEditExchangeRate.EditValue.ToString());
            this.AcOtherShouldCollection.HeJi = this.calcZongEMoney.EditValue == null ? 0 : decimal.Parse(this.calcZongEMoney.EditValue.ToString());
            this.AcOtherShouldCollection.AcDesc = this.memoEditDesc.Text;

            this.AcOtherShouldCollection.InvoiceHeji = this.calcHeJiMoney.EditValue == null ? 0 : decimal.Parse(this.calcHeJiMoney.EditValue.ToString());
            this.AcOtherShouldCollection.InvoiceTax = this.calcTaxRateMoney.EditValue == null ? 0 : decimal.Parse(this.calcTaxRateMoney.EditValue.ToString());
            this.AcOtherShouldCollection.InvoiceTaxrate = this.calcTaxRate.EditValue == null ? 0 : double.Parse(this.calcTaxRate.EditValue.ToString());
            this.AcOtherShouldCollection.TaxCaluType = this.TaxType.SelectedIndex;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this.AcOtherShouldCollectionManager.Insert(this.AcOtherShouldCollection);
                    break;

                case "update":
                    this.AcOtherShouldCollectionManager.Update(this.AcOtherShouldCollection);
                    break;
            }

        }

        protected override void Delete()
        {
            this.AcOtherShouldCollectionManager.Delete(this.AcOtherShouldCollection.AcOtherShouldCollectionId);
        }

        public override void Refresh()
        {
            if (this.AcOtherShouldCollection == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {

                    this.AcOtherShouldCollection = this.AcOtherShouldCollectionManager.GetDetails(AcOtherShouldCollection.AcOtherShouldCollectionId);

                }

            }
            this.textEditFPId.EditValue = this.AcOtherShouldCollection.InvoiceId;
            this.calcFPMoney.EditValue = this.AcOtherShouldCollection.BillMoney;
            this.textEditAcOtherShouldCollectionId.Text = this.AcOtherShouldCollection.AcOtherShouldCollectionId;
            // this.textEditInvoiceId.Text = this.AcOtherShouldCollection.InvoiceId;
            this.textEditObjectName.Text = this.AcOtherShouldCollection.ObjectName;
            this.memoEditDesc.Text = this.AcOtherShouldCollection.AcDesc;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AcOtherShouldCollection.AcOtherShouldCollectionDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditAcOtherShouldCollectionDate.EditValue = null;
            }
            else
            {
                this.dateEditAcOtherShouldCollectionDate.EditValue = this.AcOtherShouldCollection.AcOtherShouldCollectionDate;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AcOtherShouldCollection.AdvancePayableDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditAdvancePayableDate.EditValue = null;
            }
            else
            {
                this.dateEditAdvancePayableDate.EditValue = this.AcOtherShouldCollection.AdvancePayableDate;
            }
            // this.spinEditExchangeRate.EditValue = this.AcOtherShouldCollection.ExchangeRate;
            this.calcZongEMoney.EditValue = this.AcOtherShouldCollection.HeJi;
            // this.newChooseContorlAtCurrencyCategoryId.EditValue = this.AcOtherShouldCollection.AtCurrencyCategory;
            this.newChooseContorlCustomerId.EditValue = this.AcOtherShouldCollection.Customer;
            this.newChooseContorlEmployee0Id.EditValue = this.AcOtherShouldCollection.AuditEmp;
            this.newChooseContorlEmployee1Id.EditValue = this.AcOtherShouldCollection.Employee1;
            this.newChooseContorlEmployeeId.EditValue = this.AcOtherShouldCollection.Employee;
            this.nccCompany.EditValue = this.AcOtherShouldCollection.Company;
            this.bindingSource1.DataSource = this.AcOtherShouldCollection.Details;

            this.calcTaxRate.EditValue = this.AcOtherShouldCollection.InvoiceTaxrate;
            this.calcTaxRateMoney.EditValue = this.AcOtherShouldCollection.InvoiceTax;
            this.TaxType.SelectedIndex = this.AcOtherShouldCollection.TaxCaluType.Value;
            this.calcHeJiMoney.EditValue = this.AcOtherShouldCollection.InvoiceHeji;

            if (this.AcOtherShouldCollection.Details != null && this.AcOtherShouldCollection.Details.Count > 0)
            {
                foreach (Model.AcOtherShouldCollectionDetail d in this.AcOtherShouldCollection.Details)
                {
                    d.TaxCalualateDictionary = new global::Helper.TaxCalculateHelper((Helper.TaxType)d.AcOtherShouldCollection.TaxCaluType.Value, d.AcOtherShouldCollection.InvoiceTaxrate.Value, d.AcOtherShouldCollection.InvoiceTaxrate.Value, d.AcMoney.Value, 1).TaxCalculateDictionary;
                }
            }
            this.txt_AuditState.EditValue = this.GetAuditName(this.AcOtherShouldCollection.AuditState);
            base.Refresh();
            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
                default:
                    break;
            }

            this.newChooseContorlEmployeeId.Enabled = false;
            this.calcZongEMoney.Enabled = false;
            this.calcHeJiMoney.Enabled = false;
            this.calcTaxRateMoney.Enabled = false;
        }

        protected override void MoveNext()
        {
            Model.AcOtherShouldCollection AcOtherShouldCollection = this.AcOtherShouldCollectionManager.GetNext(this.AcOtherShouldCollection);
            if (AcOtherShouldCollection == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.AcOtherShouldCollection = this.AcOtherShouldCollectionManager.Get(AcOtherShouldCollection.AcOtherShouldCollectionId);
        }

        protected override void MovePrev()
        {
            Model.AcOtherShouldCollection AcOtherShouldCollection = this.AcOtherShouldCollectionManager.GetPrev(this.AcOtherShouldCollection);
            if (AcOtherShouldCollection == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.AcOtherShouldCollection = this.AcOtherShouldCollectionManager.Get(AcOtherShouldCollection.AcOtherShouldCollectionId);
        }

        protected override void MoveFirst()
        {
            this.AcOtherShouldCollection = this.AcOtherShouldCollectionManager.Get(this.AcOtherShouldCollectionManager.GetFirst() == null ? "" : this.AcOtherShouldCollectionManager.GetFirst().AcOtherShouldCollectionId);
        }

        protected override void MoveLast()
        {
            //if (AcOtherShouldCollection == null)
            {
                this.AcOtherShouldCollection = this.AcOtherShouldCollectionManager.Get(this.AcOtherShouldCollectionManager.GetLast() == null ? "" : this.AcOtherShouldCollectionManager.GetLast().AcOtherShouldCollectionId);
            }
        }

        protected override bool HasRows()
        {
            return this.AcOtherShouldCollectionManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.AcOtherShouldCollectionManager.HasRowsAfter(this.AcOtherShouldCollection);
        }

        protected override bool HasRowsPrev()
        {
            return this.AcOtherShouldCollectionManager.HasRowsBefore(this.AcOtherShouldCollection);
        }

        protected override void AddNew()
        {
            this.AcOtherShouldCollection = new Model.AcOtherShouldCollection();
            this.AcOtherShouldCollection.AcOtherShouldCollectionId = this.AcOtherShouldCollectionManager.GetId();
            this.AcOtherShouldCollection.AcOtherShouldCollectionDate = DateTime.Now;
            this.AcOtherShouldCollection.TaxCaluType = 0;
            this.AcOtherShouldCollection.Details = new List<Model.AcOtherShouldCollectionDetail>();
            this.AcOtherShouldCollection.Employee = BL.V.ActiveOperator.Employee;
            if (this.action == "insert")
            {
                Model.AcOtherShouldCollectionDetail detail = new Model.AcOtherShouldCollectionDetail();
                detail.AcOtherShouldCollectionId = this.AcOtherShouldCollection.AcOtherShouldCollectionId;
                this.AcOtherShouldCollection.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new AccountPayable.AcOtherShouldCollection.XO(this.AcOtherShouldCollection.AcOtherShouldCollectionId);
        }

        private void simpleButtonA_Click(object sender, EventArgs e)
        {
            // Model.AcOtherShouldCollection  acotherShouledColl= new Model.AcOtherShouldCollection();
            //this.AcOtherShouldCollection.Details = new List<Model.AcOtherShouldCollectionDetail>();
            //if (this.action == "insert")
            //{
            Model.AcOtherShouldCollectionDetail detail = new Model.AcOtherShouldCollectionDetail();
            detail.AcOtherShouldCollectionId = this.AcOtherShouldCollection.AcOtherShouldCollectionId;
            this.AcOtherShouldCollection.Details.Add(detail);
            this.gridControl1.RefreshDataSource();
            this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            //}
            // this.Refresh();
        }

        private void simpleButtonJ_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.AcOtherShouldCollection.Details.Remove(this.bindingSource1.Current as Book.Model.AcOtherShouldCollectionDetail);

                if (this.AcOtherShouldCollection.Details.Count == 0)
                {
                    Model.AcOtherShouldCollectionDetail detail = new Model.AcOtherShouldCollectionDetail();
                    this.AcOtherShouldCollection.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                    this.calcZongEMoney.EditValue = 0;
                }
                else
                {
                    this.gridControl1.RefreshDataSource();
                    this.CalculateMoney();
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSource2.DataSource = new BL.AtAccountSubjectManager().Select();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colAcMoney)
            {
                Model.AcOtherShouldCollectionDetail CurrentDetail = this.AcOtherShouldCollection.Details[e.RowHandle];
                double? mInvoiceTaxrate = (calcTaxRate.EditValue == null || string.IsNullOrEmpty(this.calcTaxRate.EditValue.ToString())) ? 0 : double.Parse(this.calcTaxRate.EditValue.ToString());
                global::Helper.TaxType t = (Helper.TaxType)this.TaxType.SelectedIndex;

                CurrentDetail.AcMoney = this.GetDecimal(CurrentDetail.AcMoney.HasValue ? CurrentDetail.AcMoney.Value : 0, BL.V.SetDataFormat.CGJEXiao.Value);

                CurrentDetail.TaxCalualateDictionary = new global::Helper.TaxCalculateHelper(t, mInvoiceTaxrate.Value, mInvoiceTaxrate.Value, CurrentDetail.AcMoney.Value, 1).TaxCalculateDictionary;
                this.CalculateMoney();

                this.gridControl1.RefreshDataSource();
            }
        }

        //计算金额
        private void CalculateMoney()
        {
            if (this.bindingSource1.DataSource != null)
            {
                IList<Model.AcOtherShouldCollectionDetail> list = this.bindingSource1.DataSource as IList<Model.AcOtherShouldCollectionDetail>;
                global::Helper.TaxType t = (Helper.TaxType)this.TaxType.SelectedIndex;
                if (list != null && list.Count > 0)
                {
                    decimal mInvoiceTax = 0;    //税额
                    foreach (Model.AcOtherShouldCollectionDetail d in list)
                    {
                        if (d.TaxCalualateDictionary != null)
                        {
                            d.AcMoney = d.TaxCalualateDictionary[t].UnitMoney;
                            mInvoiceTax += d.TaxCalualateDictionary[t].UnitTaxMoney;        //单笔税额统计
                        }
                    }

                    //GivenControlsValue
                    //税额
                    this.calcTaxRateMoney.EditValue = this.AcOtherShouldCollection.InvoiceTax = mInvoiceTax;
                    //合计
                    this.calcHeJiMoney.EditValue = this.AcOtherShouldCollection.InvoiceHeji = this.AcOtherShouldCollection.Details.Sum(d => d.AcMoney);
                    //总额
                    this.calcZongEMoney.EditValue = this.AcOtherShouldCollection.InvoiceTax + this.AcOtherShouldCollection.InvoiceHeji;
                    //发票金额
                    this.calcFPMoney.EditValue = this.calcZongEMoney.EditValue;
                }
            }
        }

        //更改税率
        private void calcTaxRate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action != "view")
            {
                double? mInvoiceTaxrate = (calcTaxRate.EditValue == null || string.IsNullOrEmpty(this.calcTaxRate.EditValue.ToString())) ? 0 : double.Parse(this.calcTaxRate.EditValue.ToString());
                IList<Model.AcOtherShouldCollectionDetail> list = this.bindingSource1.DataSource as IList<Model.AcOtherShouldCollectionDetail>;
                global::Helper.TaxType t = (Helper.TaxType)this.TaxType.SelectedIndex;
                if (list != null && list.Count > 0)
                {
                    foreach (Model.AcOtherShouldCollectionDetail d in list)
                    {
                        d.TaxCalualateDictionary = new global::Helper.TaxCalculateHelper(t, mInvoiceTaxrate.Value, this.AcOtherShouldCollection.InvoiceTaxrate.Value, d.AcMoney.Value, 1).TaxCalculateDictionary;
                    }
                }

                this.CalculateMoney();
                this.gridControl1.RefreshDataSource();
                this.AcOtherShouldCollection.InvoiceTaxrate = mInvoiceTaxrate;
            }
        }

        //更改税别
        private void TaxType_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action != "view")
            {
                this.CalculateMoney();
                this.gridControl1.RefreshDataSource();
            }
        }

        //List搜索
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UI.AccountPayable.AcOtherShouldCollection.ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.AcOtherShouldCollection = f.SelectItem;
                this.action = "view";
                this.Refresh();
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcOtherShouldCollection.PRO_AcOtherShouldCollectionId;
        }

        protected override int AuditState()
        {
            return this.AcOtherShouldCollection.AuditState.HasValue ? this.AcOtherShouldCollection.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcOtherShouldCollection" + "," + this.AcOtherShouldCollection.AcOtherShouldCollectionId;
        }

        #endregion
    }
}

//计算合计 赋值控件
//private void CalcHeJI()
//{
//    decimal? heji = 0;
//    decimal? zonge = 0;
//    decimal? shuie = 0;

//    foreach (Model.AcOtherShouldCollectionDetail detail in this.AcOtherShouldCollection.Details)
//    {
//        if (detail.AcMoney != null && detail.AcMoney != 0)
//        {
//            heji += detail.AcMoney;
//        }
//    }

//    if (this.calcTaxRate.EditValue == null)
//        shuie = 0;
//    else
//        shuie = (string.IsNullOrEmpty(this.calcTaxRate.EditValue.ToString()) ? 0 : Decimal.Parse(this.calcTaxRate.EditValue.ToString())) / 100 * heji;

//    heji = this.GetDecimal(heji.Value, BL.V.SetDataFormat.XSZJXiao.Value);
//    shuie = this.GetDecimal(zonge.Value, BL.V.SetDataFormat.XSZJXiao.Value);
//    zonge = shuie + heji;

//    //合计
//    this.calcHeJiMoney.EditValue = heji;
//    //总额
//    this.calcZongEMoney.EditValue = zonge;
//    //税额
//    this.calcTaxRateMoney.EditValue = shuie;
//}