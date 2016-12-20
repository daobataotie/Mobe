using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Linq;

namespace Book.UI.AccountPayable.AcOtherShouldPayment
{
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        Model.AcOtherShouldPayment AcOtherShouldPayment = new Book.Model.AcOtherShouldPayment();
        BL.AcOtherShouldPaymentManager AcOtherShouldPaymentManager = new Book.BL.AcOtherShouldPaymentManager();
        BL.AcOtherShouldPaymentDetailManager AcOtherShouldPaymentDetailManager = new Book.BL.AcOtherShouldPaymentDetailManager();
        IList<Model.AcItem> acitemlist = null;

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.AcOtherShouldPayment.PRO_AcOtherShouldPaymentId, new AA(Properties.Resources.RequireDataForId, this.textEditAcOtherShouldPaymentId));
            this.invalidValueExceptions.Add(Model.AcOtherShouldPayment.PRO_InvoiceHeji, new AA("違反規則:<稅額+合計=總額>,請檢查數據", this.calcHeJiMoney));
            //  this.newChooseContorlAtCurrencyCategoryId.Choose = new Accounting.CurrencyCategory.ChooseAtCurrencyCategory();
            this.newChooseContorlSupplierId.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlEmployeeId.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployee1Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployee0Id.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.nccCompany.Choose = new Settings.BasicData.Company.ChooseCompany();

            this.action = "view";
        }

        public EditForm(Model.AcOtherShouldPayment AcOtherShouldPayment)
            : this()
        {
            this.AcOtherShouldPayment = AcOtherShouldPayment;
            this.AcOtherShouldPayment.Details = this.AcOtherShouldPaymentDetailManager.Select(AcOtherShouldPayment);
            this.action = "update";
        }

        public EditForm(Model.AcOtherShouldPayment AcOtherShouldPayment, string action)
            : this()
        {
            this.AcOtherShouldPayment = AcOtherShouldPayment;
            this.AcOtherShouldPayment.Details = this.AcOtherShouldPaymentDetailManager.Select(AcOtherShouldPayment);
            this.action = action;
        }

        protected override void Save()
        {
            this.AcOtherShouldPayment.AcOtherShouldPaymentId = this.textEditAcOtherShouldPaymentId.Text;
            this.AcOtherShouldPayment.InvoiceId = this.textEditFPId.Text;
            this.AcOtherShouldPayment.BillMoney = this.calcFPMoney.Value;
            this.AcOtherShouldPayment.ObjectName = this.textEditObjectName.Text;
            //    this.AcOtherShouldPayment.AtCurrencyCategory = this.newChooseContorlAtCurrencyCategoryId.EditValue as Model.AtCurrencyCategory;
            if (this.AcOtherShouldPayment.AtCurrencyCategory != null)
            {
                this.AcOtherShouldPayment.AtCurrencyCategoryId = this.AcOtherShouldPayment.AtCurrencyCategory.AtCurrencyCategoryId;
            }
            this.AcOtherShouldPayment.Supplier = this.newChooseContorlSupplierId.EditValue as Model.Supplier;
            if (this.AcOtherShouldPayment.Supplier != null)
            {
                this.AcOtherShouldPayment.SupplierId = this.AcOtherShouldPayment.Supplier.SupplierId;
            }
            this.AcOtherShouldPayment.Employee = this.newChooseContorlEmployeeId.EditValue as Model.Employee;
            if (this.AcOtherShouldPayment.Employee != null)
            {
                this.AcOtherShouldPayment.EmployeeId = this.AcOtherShouldPayment.Employee.EmployeeId;
            }
            this.AcOtherShouldPayment.Employee0 = this.newChooseContorlEmployee0Id.EditValue as Model.Employee;
            if (this.AcOtherShouldPayment.Employee0 != null)
            {
                this.AcOtherShouldPayment.Employee0Id = this.AcOtherShouldPayment.Employee0.EmployeeId;
            }
            this.AcOtherShouldPayment.Employee1 = this.newChooseContorlEmployee1Id.EditValue as Model.Employee;
            if (this.AcOtherShouldPayment.Employee1 != null)
            {
                this.AcOtherShouldPayment.Employee1Id = this.AcOtherShouldPayment.Employee1.EmployeeId;
            }
            this.AcOtherShouldPayment.Company = this.nccCompany.EditValue as Model.Company;
            if (this.AcOtherShouldPayment.Company != null)
            {
                this.AcOtherShouldPayment.CompanyId = this.AcOtherShouldPayment.Company.CompanyId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditAcOtherShouldPaymentDate.DateTime, new DateTime()))
            {
                this.AcOtherShouldPayment.AcOtherShouldPaymentDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.AcOtherShouldPayment.AcOtherShouldPaymentDate = this.dateEditAcOtherShouldPaymentDate.DateTime;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditAdvancePayableDate.DateTime, new DateTime()))
            {
                this.AcOtherShouldPayment.AdvancePayableDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.AcOtherShouldPayment.AdvancePayableDate = this.dateEditAdvancePayableDate.DateTime;
            }

            //  this.AcOtherShouldPayment.ExchangeRate = this.spinEditExchangeRate.EditValue == null ? 0 :double.Parse(this.spinEditExchangeRate.EditValue.ToString());
            this.AcOtherShouldPayment.HeJi = this.calcZongEMoney.EditValue == null ? 0 : decimal.Parse(this.calcZongEMoney.EditValue.ToString());
            this.AcOtherShouldPayment.AcDesc = this.memoEditDesc.Text;

            this.AcOtherShouldPayment.InvoiceHeji = this.calcHeJiMoney.EditValue == null ? 0 : decimal.Parse(this.calcHeJiMoney.EditValue.ToString());
            this.AcOtherShouldPayment.InvoiceTax = this.calcTaxRateMoney.EditValue == null ? 0 : decimal.Parse(this.calcTaxRateMoney.EditValue.ToString());
            this.AcOtherShouldPayment.InvoiceTaxrate = this.calcTaxRate.EditValue == null ? 0 : double.Parse(this.calcTaxRate.EditValue.ToString());
            this.AcOtherShouldPayment.TaxCaluType = this.TaxType.SelectedIndex;
            this.AcOtherShouldPayment.IsPaymentOver = this.checkEdit1.Checked;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            switch (this.action)
            {
                case "insert":
                    this.AcOtherShouldPaymentManager.Insert(this.AcOtherShouldPayment);
                    break;

                case "update":
                    this.AcOtherShouldPaymentManager.Update(this.AcOtherShouldPayment);
                    break;
            }
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.AcOtherShouldPaymentManager.Delete(this.AcOtherShouldPayment.AcOtherShouldPaymentId);
                this.AcOtherShouldPayment = this.AcOtherShouldPaymentManager.GetNext(this.AcOtherShouldPayment);
                if (this.AcOtherShouldPayment == null)
                {
                    this.AcOtherShouldPayment = this.AcOtherShouldPaymentManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void AddNew()
        {
            this.AcOtherShouldPayment = new Model.AcOtherShouldPayment();
            this.AcOtherShouldPayment.AcOtherShouldPaymentId = this.AcOtherShouldPaymentManager.GetId();
            this.AcOtherShouldPayment.Details = new List<Model.AcOtherShouldPaymentDetail>();
            this.AcOtherShouldPayment.TaxCaluType = 0;
            this.AcOtherShouldPayment.AcOtherShouldPaymentDate = DateTime.Now;
            this.AcOtherShouldPayment.Employee = BL.V.ActiveOperator.Employee;
            this.AcOtherShouldPayment.InvoiceTaxrate = 5;

            //添加详细
            this.AddDataRows();
        }

        public override void Refresh()
        {
            if (this.AcOtherShouldPayment == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this.AcOtherShouldPayment = this.AcOtherShouldPaymentManager.GetDetails(AcOtherShouldPayment.AcOtherShouldPaymentId);
                    if (this.AcOtherShouldPayment == null)
                    {
                        this.AddNew();
                        this.action = "insert";
                    }
                }
            }
            this.textEditFPId.EditValue = this.AcOtherShouldPayment.InvoiceId;

            this.textEditAcOtherShouldPaymentId.Text = this.AcOtherShouldPayment.AcOtherShouldPaymentId;
            //  this.textEditInvoiceId.Text = this.AcOtherShouldPayment.InvoiceId;
            this.textEditObjectName.Text = this.AcOtherShouldPayment.ObjectName;
            this.memoEditDesc.Text = this.AcOtherShouldPayment.AcDesc;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AcOtherShouldPayment.AcOtherShouldPaymentDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditAcOtherShouldPaymentDate.EditValue = null;
            }
            else
            {
                this.dateEditAcOtherShouldPaymentDate.EditValue = this.AcOtherShouldPayment.AcOtherShouldPaymentDate;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.AcOtherShouldPayment.AdvancePayableDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditAdvancePayableDate.EditValue = null;
            }
            else
            {
                this.dateEditAdvancePayableDate.EditValue = this.AcOtherShouldPayment.AdvancePayableDate;
            }
            // this.spinEditExchangeRate.EditValue = this.AcOtherShouldPayment.ExchangeRate;
            //  this.newChooseContorlAtCurrencyCategoryId.EditValue = this.AcOtherShouldPayment.AtCurrencyCategory;
            this.newChooseContorlSupplierId.EditValue = this.AcOtherShouldPayment.Supplier;
            this.newChooseContorlEmployee0Id.EditValue = this.AcOtherShouldPayment.AuditEmp;
            this.newChooseContorlEmployee1Id.EditValue = this.AcOtherShouldPayment.Employee1;
            this.newChooseContorlEmployeeId.EditValue = this.AcOtherShouldPayment.Employee;
            this.nccCompany.EditValue = this.AcOtherShouldPayment.Company;
            this.calcTaxRate.EditValue = this.AcOtherShouldPayment.InvoiceTaxrate;          //税率
            this.TaxType.SelectedIndex = this.AcOtherShouldPayment.TaxCaluType.Value;       //税别
            this.calcTaxRateMoney.EditValue = this.AcOtherShouldPayment.InvoiceTax;         //税额
            this.calcHeJiMoney.EditValue = this.AcOtherShouldPayment.InvoiceHeji;           //合计
            this.calcZongEMoney.EditValue = this.AcOtherShouldPayment.HeJi;                 //总额
            this.calcFPMoney.EditValue = this.AcOtherShouldPayment.BillMoney;               //发票金额
            this.txt_AuditState.EditValue = this.GetAuditName(this.AcOtherShouldPayment.AuditingState);
            this.checkEdit1.Checked = this.AcOtherShouldPayment.IsPaymentOver.HasValue ? this.AcOtherShouldPayment.IsPaymentOver.Value : false;
            this.bindingSource1.DataSource = this.AcOtherShouldPayment.Details;


            if (this.AcOtherShouldPayment.Details != null && this.AcOtherShouldPayment.Details.Count > 0)
            {
                foreach (Model.AcOtherShouldPaymentDetail d in this.AcOtherShouldPayment.Details)
                {
                    if (d.AcQuantity.HasValue && d.AcQuantity.Value > 0)
                    {
                        d.TaxCalualateDictionary = new global::Helper.TaxCalculateHelper((Helper.TaxType)d.AcOtherShouldPayment.TaxCaluType.Value, d.AcOtherShouldPayment.InvoiceTaxrate.Value, d.AcOtherShouldPayment.InvoiceTaxrate.Value, d.AcMoney.Value, d.AcQuantity.Value).TaxCalculateDictionary;
                    }
                }
            }

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
            Model.AcOtherShouldPayment AcOtherShouldPayment = this.AcOtherShouldPaymentManager.GetNext(this.AcOtherShouldPayment);
            if (AcOtherShouldPayment == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.AcOtherShouldPayment = this.AcOtherShouldPaymentManager.Get(AcOtherShouldPayment.AcOtherShouldPaymentId);
        }

        protected override void MovePrev()
        {
            Model.AcOtherShouldPayment AcOtherShouldPayment = this.AcOtherShouldPaymentManager.GetPrev(this.AcOtherShouldPayment);
            if (AcOtherShouldPayment == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.AcOtherShouldPayment = this.AcOtherShouldPaymentManager.Get(AcOtherShouldPayment.AcOtherShouldPaymentId);
        }

        protected override void MoveFirst()
        {
            this.AcOtherShouldPayment = this.AcOtherShouldPaymentManager.Get(this.AcOtherShouldPaymentManager.GetFirst() == null ? "" : this.AcOtherShouldPaymentManager.GetFirst().AcOtherShouldPaymentId);
        }

        protected override void MoveLast()
        {
            this.AcOtherShouldPayment = this.AcOtherShouldPaymentManager.Get(this.AcOtherShouldPaymentManager.GetLast() == null ? "" : this.AcOtherShouldPaymentManager.GetLast().AcOtherShouldPaymentId);
        }

        protected override bool HasRows()
        {
            return this.AcOtherShouldPaymentManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.AcOtherShouldPaymentManager.HasRowsAfter(this.AcOtherShouldPayment);
        }

        protected override bool HasRowsPrev()
        {
            return this.AcOtherShouldPaymentManager.HasRowsBefore(this.AcOtherShouldPayment);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new AccountPayable.AcOtherShouldPayment.XO(this.AcOtherShouldPayment.AcOtherShouldPaymentId);
        }

        private void simpleButtonA_Click(object sender, EventArgs e)
        {
            APParameterSet.ChooseAcItem f = new Book.UI.AccountPayable.APParameterSet.ChooseAcItem();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.AcOtherShouldPaymentDetail detail = new Book.Model.AcOtherShouldPaymentDetail();
                Model.AcItem acitem = f.SelectedItem as Model.AcItem;
                detail.AcOtherShouldPaymentDetailId = Guid.NewGuid().ToString();
                detail.AcOtherShouldPaymentId = this.AcOtherShouldPayment.AcOtherShouldPaymentId;
                detail.LoanName = acitem.ItemName;
                detail.AcQuantity = 0;
                detail.AcMoney = 0;
                detail.AcItemPrice = acitem.ItemPrice;
                detail.AcItemId = acitem.AcItemId;
                this.AcOtherShouldPayment.Details.Add(detail);
                this.gridControl1.RefreshDataSource();
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
            //Model.AcOtherShouldPaymentDetail detail = new Model.AcOtherShouldPaymentDetail();
            //detail.AcOtherShouldPaymentId = this.AcOtherShouldPayment.AcOtherShouldPaymentId;
            //this.AcOtherShouldPayment.Details.Add(detail);
            //this.gridControl1.RefreshDataSource();
            //this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
        }

        private void simpleButtonJ_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.AcOtherShouldPayment.Details.Remove(this.bindingSource1.Current as Book.Model.AcOtherShouldPaymentDetail);

                if (this.AcOtherShouldPayment.Details.Count == 0)
                {
                    this.AddDataRows();
                    this.gridControl1.RefreshDataSource();
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

            this.acitemlist = new BL.AcItemManager().Select();

            foreach (Model.AcItem item in this.acitemlist)
            {
                this.repositoryItemComboBox1.Items.Add(string.IsNullOrEmpty(item.ItemName) ? "" : item.ItemName);
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colAcItemPrice || e.Column == this.colAcQuantity)
            {
                Model.AcOtherShouldPaymentDetail CurrentDetail = this.AcOtherShouldPayment.Details[e.RowHandle];
                double? mInvoiceTaxrate = (calcTaxRate.EditValue == null || string.IsNullOrEmpty(this.calcTaxRate.EditValue.ToString())) ? 0 : double.Parse(this.calcTaxRate.EditValue.ToString());
                global::Helper.TaxType t = (Helper.TaxType)this.TaxType.SelectedIndex;

                CurrentDetail.AcItemPrice = this.GetDecimal((CurrentDetail.AcItemPrice.HasValue ? CurrentDetail.AcItemPrice.Value : 0), BL.V.SetDataFormat.CGDJXiao.Value);
                //CurrentDetail.AcQuantity = this.GetDecimal(CurrentDetail.AcQuantity.HasValue ? CurrentDetail.AcQuantity.Value : 0, BL.V.SetDataFormat.CGSLXiao.Value);
                CurrentDetail.AcMoney = this.GetDecimal(CurrentDetail.AcItemPrice.Value * CurrentDetail.AcQuantity.Value, BL.V.SetDataFormat.CGJEXiao.Value);

                if (CurrentDetail.AcQuantity.HasValue && CurrentDetail.AcQuantity.Value > 0)
                {
                    CurrentDetail.TaxCalualateDictionary = new global::Helper.TaxCalculateHelper(t, mInvoiceTaxrate.Value, mInvoiceTaxrate.Value, CurrentDetail.AcMoney.Value, CurrentDetail.AcQuantity.Value).TaxCalculateDictionary;
                    this.CalculateMoney();
                }

                this.gridControl1.RefreshDataSource();
                //this.CalcHeJI();
            }

            if (e.Column == this.colLoanName)
            {
                Model.AcOtherShouldPaymentDetail CurrentDetail = this.AcOtherShouldPayment.Details[e.RowHandle];
                IList<Model.AcItem> acilist = (from Model.AcItem ac in acitemlist
                                               where ac.ItemName == CurrentDetail.LoanName
                                               select ac).ToList<Model.AcItem>();

                if (acilist != null && acilist.Count != 0)
                {
                    Model.AcItem aci = acilist.First<Model.AcItem>();

                    CurrentDetail.AcItemId = aci.AcItemId;
                    CurrentDetail.AcItem = aci;
                    CurrentDetail.LoanName = aci.ItemName;

                    double? mInvoiceTaxrate = (calcTaxRate.EditValue == null || string.IsNullOrEmpty(this.calcTaxRate.EditValue.ToString())) ? 0 : double.Parse(this.calcTaxRate.EditValue.ToString());
                    global::Helper.TaxType t = (Helper.TaxType)this.TaxType.SelectedIndex;

                    CurrentDetail.AcItemPrice = this.GetDecimal(aci.ItemPrice.HasValue ? aci.ItemPrice.Value : 0, BL.V.SetDataFormat.CGDJXiao.Value);
                    //CurrentDetail.AcQuantity = this.GetDecimal(CurrentDetail.AcQuantity.HasValue ? CurrentDetail.AcQuantity.Value : 0, BL.V.SetDataFormat.CGSLXiao.Value);
                    CurrentDetail.AcMoney = this.GetDecimal(CurrentDetail.AcItemPrice.Value * CurrentDetail.AcQuantity.Value, BL.V.SetDataFormat.CGJEXiao.Value);

                    if (CurrentDetail.AcQuantity.HasValue && CurrentDetail.AcQuantity.Value > 0)
                    {
                        CurrentDetail.TaxCalualateDictionary = new global::Helper.TaxCalculateHelper(t, mInvoiceTaxrate.Value, mInvoiceTaxrate.Value, CurrentDetail.AcMoney.Value, CurrentDetail.AcQuantity.Value).TaxCalculateDictionary;
                        this.CalculateMoney();

                    }

                    this.gridControl1.RefreshDataSource();
                }
                else
                {
                    CurrentDetail = new Book.Model.AcOtherShouldPaymentDetail();
                    CurrentDetail.AcItemPrice = 0;
                    CurrentDetail.AcMoney = 0;
                    CurrentDetail.AcQuantity = 0;
                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        //更改税率
        private void calcTaxRate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action != "view")
            {
                double? mInvoiceTaxrate = (calcTaxRate.EditValue == null || string.IsNullOrEmpty(this.calcTaxRate.EditValue.ToString())) ? 0 : double.Parse(this.calcTaxRate.EditValue.ToString());
                IList<Model.AcOtherShouldPaymentDetail> list = this.bindingSource1.DataSource as IList<Model.AcOtherShouldPaymentDetail>;
                global::Helper.TaxType t = (Helper.TaxType)this.TaxType.SelectedIndex;
                if (list != null && list.Count > 0)
                {
                    foreach (Model.AcOtherShouldPaymentDetail d in list)
                    {
                        if (d.AcQuantity.HasValue && d.AcQuantity.Value > 0)
                        {
                            d.TaxCalualateDictionary = new global::Helper.TaxCalculateHelper(t, mInvoiceTaxrate.Value, this.AcOtherShouldPayment.InvoiceTaxrate.Value, d.AcMoney.Value, d.AcQuantity.Value).TaxCalculateDictionary;
                        }
                    }
                }

                this.CalculateMoney();
                this.gridControl1.RefreshDataSource();
                this.AcOtherShouldPayment.InvoiceTaxrate = mInvoiceTaxrate;
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

        //计算金额
        private void CalculateMoney()
        {
            if (this.bindingSource1.DataSource != null)
            {
                IList<Model.AcOtherShouldPaymentDetail> list = this.bindingSource1.DataSource as IList<Model.AcOtherShouldPaymentDetail>;
                global::Helper.TaxType t = (Helper.TaxType)this.TaxType.SelectedIndex;
                if (list != null && list.Count > 0)
                {
                    decimal mInvoiceTax = 0;        //税额

                    foreach (Model.AcOtherShouldPaymentDetail d in list)
                    {
                        if (d.TaxCalualateDictionary != null)
                        {
                            d.AcItemPrice = d.TaxCalualateDictionary[t].UnitPrice;
                            d.AcMoney = d.TaxCalualateDictionary[t].UnitMoney;
                            mInvoiceTax += d.TaxCalualateDictionary[t].UnitTaxMoney;        //单笔税额统计
                        }
                    }

                    //GivenControlsValue
                    //税额
                    this.calcTaxRateMoney.EditValue = this.AcOtherShouldPayment.InvoiceTax = mInvoiceTax;
                    //合计
                    this.calcHeJiMoney.EditValue = this.AcOtherShouldPayment.InvoiceHeji = this.AcOtherShouldPayment.Details.Sum(d => d.AcMoney);
                    //总额
                    this.calcZongEMoney.EditValue = this.AcOtherShouldPayment.InvoiceTax + this.AcOtherShouldPayment.InvoiceHeji;
                    //发票金额
                    this.calcFPMoney.EditValue = this.calcZongEMoney.EditValue;
                }
            }
        }

        private void AddDataRows()
        {
            Model.AcOtherShouldPaymentDetail detail = new Book.Model.AcOtherShouldPaymentDetail();
            detail.AcOtherShouldPaymentDetailId = Guid.NewGuid().ToString();
            detail.AcOtherShouldPaymentId = this.AcOtherShouldPayment.AcOtherShouldPaymentId;
            detail.AcOtherShouldPayment = this.AcOtherShouldPayment;
            detail.AcItemPrice = 0;
            detail.AcMoney = 0;
            detail.AcQuantity = 0;
            this.AcOtherShouldPayment.Details.Add(detail);

            this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action != "view")
            {
                switch (e.KeyValue)
                {
                    case 13:
                        this.AddDataRows();
                        break;
                    case 46:
                        if (this.bindingSource1.Current != null)
                        {
                            this.AcOtherShouldPayment.Details.Remove((this.bindingSource1.Current as Model.AcOtherShouldPaymentDetail));
                            if (this.AcOtherShouldPayment.Details.Count == 0)
                            {
                                this.AddDataRows();
                                this.gridControl1.RefreshDataSource();
                            }
                            else
                            {
                                this.gridControl1.RefreshDataSource();
                                this.CalculateMoney();
                            }
                            this.gridControl1.RefreshDataSource();
                        }
                        break;
                }

            }
        }

        //搜寻页面LIST
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UI.AccountPayable.AcOtherShouldPayment.ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.AcOtherShouldPayment = f.SelectItem;
                this.action = "view";
                this.Refresh();
            }
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            //decimal? abc = 0;
            //foreach (Model.AcOtherShouldPaymentDetail detail in this.AcOtherShouldPayment.Details)
            //{
            //    if (detail.AcMoney != null && detail.AcMoney != 0)
            //    {
            //        abc += detail.AcMoney;
            //    }
            //}
            //this.calcZongEMoney.EditValue = this.GetDecimal(abc.Value, BL.V.SetDataFormat.CGJEXiao.Value);
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcOtherShouldPayment.PRO_AcOtherShouldPaymentId;
        }

        protected override int AuditState()
        {
            return this.AcOtherShouldPayment.AuditState.HasValue ? this.AcOtherShouldPayment.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcOtherShouldPayment" + "," + this.AcOtherShouldPayment.AcOtherShouldPaymentId;
        }

        #endregion
    }
}