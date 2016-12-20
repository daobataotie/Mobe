using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Accounting.AtAccountSubject;
using Book.UI.Accounting.CurrencyCategory;
using Book.UI.Settings.BasicData.Customs;

namespace Book.UI.AccountPayable.AcCollection
{
    internal partial class EditForm : Settings.BasicData.BaseEditForm
    {

        private BL.PayMethodManager _payMethodManager = new BL.PayMethodManager();
        private BL.AcCollectionManager _acCollectionManager = new BL.AcCollectionManager();
        Model.AcCollection _acCollection = null;

        int LastFlag = 0; //页面载入时是否执行 last方法
        public EditForm()
        {
            InitializeComponent();
            //this.requireValueExceptions.Add(Model.AcCollection.PRO_PayMethodId, new AA(Properties.Resources.RequireDataForPayMethod, this.newChoosePayMethodId));
            this.requireValueExceptions.Add(Model.Customer.PRO_CustomerId, new AA(Properties.Resources.Customer, this.newChooseCustomerId));
            this.requireValueExceptions.Add(Model.AcCollection.PRO_Employee1Id, new AA(Properties.Resources.NullShouKuanRen, this.newChooseEmployee1Id));
            this.invalidValueExceptions.Add(Model.AcCollection.PRO_SubscriptionAdvanceCollection, new AA("取預付款不得大於預付款餘額", this.calcSubscriptionAdvanceCollection));
            this.invalidValueExceptions.Add(Model.AcCollectionDetail.PRO_DomesticThisChargeMoney, new AA("沖銷金額不得大於未收金額", this.gridControl1));
            this.bindingSourcePayMethod.DataSource = this._payMethodManager.Select();
            this.newChoosePayMethodId.Choose = new UI.Invoices.ChoosePayMethod();
            this.newChooseEmployee0Id.Choose = new ChooseEmployee();
            this.newChooseEmployee1Id.Choose = new ChooseEmployee();
            this.newChooseEmployeeId.Choose = new ChooseEmployee();
            this.newChooseSubjectId.Choose = new ChooseAccountSubject();
            this.newChooseCustomerId.Choose = new ChooseCustoms();
            this.newChooseAtCurrencyCategoryId.Choose = new ChooseAtCurrencyCategory();
            this.action = "view";
        }

        public EditForm(Model.AcCollection acc)
            : this()
        {
            this._acCollection = acc;
            this.action = "view";
            LastFlag = 1;
        }

        public EditForm(Model.AcCollection acc, string action)
            : this()
        {
            this._acCollection = acc;
            this.action = "view";
            LastFlag = 1;
        }

        protected override void MoveFirst()
        {
            this._acCollection = this._acCollectionManager.Get(this._acCollectionManager.GetFirst() == null ? "" : this._acCollectionManager.GetFirst().AcCollectionId);
        }

        protected override void MoveLast()
        {
            if (this.LastFlag == 1)
            {
                this.LastFlag = 0;
                return;
            }
            this._acCollection = this._acCollectionManager.Get(this._acCollectionManager.GetLast() == null ? "" : this._acCollectionManager.GetLast().AcCollectionId);

        }

        protected override void MoveNext()
        {
            Model.AcCollection temp = this._acCollectionManager.GetNext(this._acCollection);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acCollection = this._acCollectionManager.Get(temp.AcCollectionId);
        }

        protected override void MovePrev()
        {
            Model.AcCollection temp = this._acCollectionManager.GetPrev(this._acCollection);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acCollection = this._acCollectionManager.Get(temp.AcCollectionId);

        }

        protected override bool HasRows()
        {
            return this._acCollectionManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._acCollectionManager.HasRowsAfter(this._acCollection);
        }

        protected override bool HasRowsPrev()
        {
            return this._acCollectionManager.HasRowsBefore(this._acCollection);
        }

        protected override void AddNew()
        {
            this._acCollection = new Model.AcCollection();
            this._acCollection.AcCollectionId = this._acCollectionManager.GetId();
            this._acCollection.Employee = BL.V.ActiveOperator.Employee;
            this._acCollection.AcPaymentDate = DateTime.Now;
            this._acCollection.Detail = new List<Model.AcCollectionDetail>();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this._acCollectionManager.Delete(this._acCollection);
                this._acCollection = this._acCollectionManager.GetNext(this._acCollection);
                if (this._acCollection == null)
                {
                    this._acCollection = this._acCollectionManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void Save()
        {
            if (this.calcSubscriptionAdvanceCollection.Value > this.calcAdvanceCollectionBalance.Value)
                throw new Helper.InvalidValueException(Model.AcCollection.PRO_SubscriptionAdvanceCollection);
            this._acCollection.AcCollectionId = this.textAcCollectionId.Text;
            this._acCollection.AcDesc = this.memoAcDesc.Text;
            //this._acCollection.AuditingState = this.textAuditingState.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateAcPaymentDate.DateTime, new DateTime()))
                this._acCollection.AcPaymentDate = global::Helper.DateTimeParse.NullDate;
            else
                this._acCollection.AcPaymentDate = this.dateAcPaymentDate.DateTime;
            this._acCollection.AdvanceCollectionBalance = this.calcAdvanceCollectionBalance.Value;
            this._acCollection.AtCurrencyCategory = this.newChooseAtCurrencyCategoryId.EditValue as Model.AtCurrencyCategory;
            if (this._acCollection.AtCurrencyCategory != null)
                this._acCollection.AtCurrencyCategoryId = this._acCollection.AtCurrencyCategory.AtCurrencyCategoryId;
            this._acCollection.BankAccount = this.textBankAccount.Text;
            this._acCollection.BillNo = this.textBillNo.Text;
            this._acCollection.Customer = this.newChooseCustomerId.EditValue as Model.Customer;
            if (this._acCollection.Customer != null)
                this._acCollection.CustomerId = this._acCollection.Customer.CustomerId;
            this._acCollection.DomesticCashAgio = this.calcDomesticCashAgio.Value;
            this._acCollection.DomesticEealityCollection = this.calcDomesticEealityCollection.Value;
            this._acCollection.DomesticMayChargeMoney = this.calcDomesticMayChargeMoney.Value;
            this._acCollection.Employee = this.newChooseEmployeeId.EditValue as Model.Employee;
            if (this._acCollection.Employee != null)
                this._acCollection.EmployeeId = this._acCollection.Employee.EmployeeId;
            this._acCollection.Employee0 = this.newChooseEmployee0Id.EditValue as Model.Employee;
            if (this._acCollection.Employee0 != null)
                this._acCollection.Employee0Id = this._acCollection.Employee0.EmployeeId;
            this._acCollection.Employee1 = this.newChooseEmployee1Id.EditValue as Model.Employee;
            if (this._acCollection.Employee1 != null)
                this._acCollection.Employee1Id = this._acCollection.Employee1.EmployeeId;
            this._acCollection.PayMethod = this.newChoosePayMethodId.EditValue as Model.PayMethod;
            if (this._acCollection.PayMethod != null)
                this._acCollection.PayMethodId = this._acCollection.PayMethod.PayMethodId;
            this._acCollection.Subject = this.newChooseSubjectId.EditValue as Model.AtAccountSubject;
            if (this._acCollection.Subject != null)
                this._acCollection.SubjectId = this._acCollection.Subject.SubjectId;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._acCollectionManager.Insert(this._acCollection);
                    break;

                case "update":
                    this._acCollectionManager.Update(this._acCollection);
                    break;
            }
            this._acCollectionManager.UpdateAcInvoiceXOHeXiao(this._acCollection.Detail);
        }

        public override void Refresh()
        {
            if (this._acCollection == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._acCollection = this._acCollectionManager.GetDetails(this._acCollection);
                    if (this._acCollection == null)
                    {
                        this._acCollection = new Book.Model.AcCollection();
                    }
                }
            }

            this.textAcCollectionId.Text = this._acCollection.AcCollectionId;
            this.textAuditingState.Text = this.GetAuditName(this._acCollection.AuditState);
            this.textBankAccount.Text = this._acCollection.BankAccount;
            this.textBillNo.Text = this._acCollection.BillNo;
            this.calcAdvanceCollectionBalance.Value = this._acCollection.AdvanceCollectionBalance == null ? 0 : this._acCollection.AdvanceCollectionBalance.Value;
            this.calcDomesticCashAgio.Value = this._acCollection.DomesticCashAgio == null ? 0 : this._acCollection.DomesticCashAgio.Value;
            this.calcDomesticEealityCollection.Value = this._acCollection.DomesticEealityCollection == null ? 0 : this._acCollection.DomesticEealityCollection.Value;
            this.calcDomesticMayChargeMoney.Value = this._acCollection.DomesticMayChargeMoney == null ? 0 : this._acCollection.DomesticMayChargeMoney.Value;
            this.calcJoinAdvanceCollection.Value = this._acCollection.JoinAdvanceCollection == null ? 0 : this._acCollection.JoinAdvanceCollection.Value;
            this.calcSubscriptionAdvanceCollection.Value = this._acCollection.SubscriptionAdvanceCollection == null ? 0 : this._acCollection.SubscriptionAdvanceCollection.Value;
            this.newChooseAtCurrencyCategoryId.EditValue = this._acCollection.AtCurrencyCategory;
            this.newChooseCustomerId.EditValue = this._acCollection.Customer;
            this.newChooseEmployee0Id.EditValue = this._acCollection.AuditEmp;
            this.newChooseEmployee1Id.EditValue = this._acCollection.Employee1;
            this.newChooseEmployeeId.EditValue = this._acCollection.Employee;
            this.newChoosePayMethodId.EditValue = this._acCollection.PayMethod;
            this.newChooseSubjectId.EditValue = this._acCollection.Subject;
            this.dateAcPaymentDate.Text = this._acCollection.AcPaymentDate == null ? "" : this._acCollection.AcPaymentDate.Value.ToString();
            this.memoAcDesc.Text = this._acCollection.AcDesc;

            this.bindingSourceDetails.DataSource = this._acCollection.Detail;
            this.gridView1.GroupPanelText = "共: " + (this._acCollection.Detail == null ? "0" : this._acCollection.Detail.Count.ToString()) + " 項";
            base.Refresh();

            if (this.action == "view")
            {
                this.barBtnInvoiceXO.Enabled = false;
            }
            else
            {
                this.barBtnInvoiceXO.Enabled = true;
            }
            this.textAcCollectionId.Enabled = false;
            this.newChooseEmployeeId.Enabled = false;
            this.calcAdvanceCollectionBalance.Enabled = false;
            this.calcDomesticCashAgio.Enabled = false;
            this.calcDomesticMayChargeMoney.Enabled = false;

        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new AccountPayable.AcCollection.XR(this._acCollection.AcCollectionId);
        }

        //选择销售发票
        private void barBtnInvoiceXO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AcInvoiceXOBill.ChooseAcInvoiceXOBill f = new Book.UI.AccountPayable.AcInvoiceXOBill.ChooseAcInvoiceXOBill();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Model.AcInvoiceXOBill item in f.listAcinvoiceXOBill)
                {
                    Model.AcCollectionDetail detail = new Book.Model.AcCollectionDetail();
                    detail.AcCollectionDetailId = Guid.NewGuid().ToString();
                    detail.AcCollectionId = this._acCollection.AcCollectionId;
                    detail.AcInvoiceId = item.AcInvoiceXOBillId;
                    detail.AcInvoiceType = Properties.Resources.AcInvoiceXOBill;
                    detail.BillId = item.Id;
                    detail.DomesticShouldCollectionMoney = item.ZongMoney == null ? 0 : item.ZongMoney.Value;
                    detail.DomesticNoPaymentMoney = item.NoHeXiaoTotal == null ? 0 : item.NoHeXiaoTotal.Value;
                    this._acCollection.Detail.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void newChooseCustomerId_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseCustomerId.EditValue != null)
            {
                Model.Customer cus = this.newChooseCustomerId.EditValue as Model.Customer;
                this.calcAdvanceCollectionBalance.Text = cus.AdvanceCollectionBalance == null ? "0" : cus.AdvanceCollectionBalance.Value.ToString();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal count = 0;
            switch (e.Column.Name)
            {
                case "colDomesticThisChargeMoney":
                    foreach (Model.AcCollectionDetail item in this._acCollection.Detail)
                    {
                        count += this.GetDecimal((item.DomesticThisChargeMoney == null ? 0 : item.DomesticThisChargeMoney.Value), BL.V.SetDataFormat.XSJEXiao.Value);
                    }
                    this.calcDomesticMayChargeMoney.Value = count;
                    break;
                case "colDomesticDetailCashAgio":
                    foreach (Model.AcCollectionDetail item in this._acCollection.Detail)
                    {
                        count += this.GetDecimal((item.DomesticDetailCashAgio == null ? 0 : item.DomesticDetailCashAgio.Value), BL.V.SetDataFormat.XSJEXiao.Value);
                    }
                    this.calcDomesticCashAgio.Value = count;
                    break;
            }

            this.calcDomesticEealityCollection.Value = this.calcDomesticMayChargeMoney.Value - this.calcDomesticCashAgio.Value;
        }

        //其它应收款
        private void barBtnAcOtherShouldCollection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AcOtherShouldCollection.ChooseAcOtherShouldCollection f = new Book.UI.AccountPayable.AcOtherShouldCollection.ChooseAcOtherShouldCollection();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Model.AcOtherShouldCollection item in f.listAcOtherShouldCollection)
                {
                    Model.AcCollectionDetail detail = new Book.Model.AcCollectionDetail();
                    detail.AcCollectionDetailId = Guid.NewGuid().ToString();
                    detail.AcCollectionId = this._acCollection.AcCollectionId;
                    detail.AcInvoiceId = item.AcOtherShouldCollectionId;
                    detail.AcInvoiceType = Properties.Resources.AcOtherShouldCollection;
                    detail.DomesticShouldCollectionMoney = item.HeJi == null ? 0 : item.HeJi.Value;
                    detail.DomesticNoPaymentMoney = item.NoHeXiaoTotal == null ? 0 : item.NoHeXiaoTotal.Value;
                    this._acCollection.Detail.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton_Remove_Click(object sender, EventArgs e)
        {
            Model.AcCollectionDetail currentRow = this.bindingSourceDetails.Current as Model.AcCollectionDetail;
            if (currentRow != null)
                this._acCollection.Detail.Remove(currentRow);
            this.gridControl1.RefreshDataSource();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcCollection.PRO_AcCollectionId;
        }

        protected override int AuditState()
        {
            return this._acCollection.AuditState.HasValue ? this._acCollection.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcCollection" + "," + this._acCollection.AcCollectionId;
        }

        #endregion
    }
}