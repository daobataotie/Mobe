using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Accounting.AtAccountSubject;
using Book.UI.Settings.BasicData.Supplier;
using Book.UI.Accounting.CurrencyCategory;


namespace Book.UI.AccountPayable.AcPayment
{
    public partial class EditForm : BaseEditForm
    {
        private BL.AcPaymentManager _acPayManager = new BL.AcPaymentManager();
        private Model.AcPayment _acPayment = null;
        private BL.PayMethodManager _payMethodManager = new BL.PayMethodManager();

        int LastFlag = 0; //页面载入时是否执行 last方法

        public EditForm()
        {
            InitializeComponent();
            //this.requireValueExceptions.Add(Model.AcPayment.PRO_BillNo, new AA(Properties.Resources.AcPaymentBillNo, this.textEditBillNo));
            this.requireValueExceptions.Add(Model.AcPayment.PRO_SupplierId, new AA(Properties.Resources.ChooseSupplier, this.newChooseContorlSupplierId));
            this.requireValueExceptions.Add(Model.AcPayment.PRO_PayMethodId, new AA(Properties.Resources.RequireDataForPayMethod, this.newChoosePayMethodId));
            this.invalidValueExceptions.Add(Model.AcPayment.PRO_SubscriptionAdvancePayment, new AA("取預付款不得大於預付款餘額", this.calcSubscriptionAdvancePayment));
            //this.invalidValueExceptions.Add(Model.AcPayment.PRO_DomesticEealityPayment, new AA("本幣實際付款與本幣已沖金額不等", this.calcDomesticEealityPayment));
            this.invalidValueExceptions.Add(Model.AcPayment.PRO_DomesticEealityPayment, new AA("本幣實際付款有誤,違反條件:(已沖總額-折扣總額=本幣實際付款+取預付款額)", this.calcDomesticEealityPayment));
            this.invalidValueExceptions.Add(Model.AcPaymentDetail.PRO_DomesticThisChargeMoney, new AA("沖銷金額不得大於未收金額", this.gridControl1));
            this.bindingSourcePayMethod.DataSource = this._payMethodManager.Select();
            this.newChooseContorlEmployee0Id.Choose = new ChooseEmployee();
            this.newChooseContorlEmployee1Id.Choose = new ChooseEmployee();
            this.newChooseContorlEmployeeId.Choose = new ChooseEmployee();
            this.newChooseContorlSubjectId.Choose = new ChooseAccountSubject();
            this.newChooseContorlSupplierId.Choose = new ChooseSupplier();
            //  this.newChooseContorlAtCurrencyCategoryId.Choose = new ChooseAtCurrencyCategory();
            this.newChoosePayMethodId.Choose = new UI.Invoices.ChoosePayMethod();
            this.action = "view";
        }

        public EditForm(Model.AcPayment acp)
            : this()
        {
            this._acPayment = acp;
            this.action = "view";
            LastFlag = 1;
        }

        public EditForm(Model.AcPayment acp, string action)
            : this()
        {
            this._acPayment = acp;
            this.action = "view";
            LastFlag = 1;
        }

        protected override void MoveFirst()
        {
            this._acPayment = this._acPayManager.Get(this._acPayManager.GetFirst() == null ? "" : this._acPayManager.GetFirst().AcPaymentId);
        }

        protected override void MoveLast()
        {
            if (LastFlag == 1)
            {
                LastFlag = 0;
                return;
            }
            this._acPayment = this._acPayManager.Get(this._acPayManager.GetLast() == null ? "" : this._acPayManager.GetLast().AcPaymentId);
        }

        protected override void MoveNext()
        {
            Model.AcPayment temp = this._acPayManager.GetNext(this._acPayment);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acPayment = this._acPayManager.Get(temp.AcPaymentId);
        }

        protected override void MovePrev()
        {
            Model.AcPayment temp = this._acPayManager.GetPrev(this._acPayment);
            if (temp == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._acPayment = this._acPayManager.Get(temp.AcPaymentId);
        }

        protected override bool HasRows()
        {
            return this._acPayManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._acPayManager.HasRowsAfter(this._acPayment);
        }

        protected override bool HasRowsPrev()
        {
            return this._acPayManager.HasRowsBefore(this._acPayment);
        }

        protected override void AddNew()
        {
            this._acPayment = new Model.AcPayment();
            this._acPayment.AcPaymentId = this._acPayManager.GetId();
            this._acPayment.Employee = BL.V.ActiveOperator.Employee;
            this._acPayment.AcPaymentDate = DateTime.Now;
            this._acPayment.SubscriptionAdvancePayment = 0;
            this._acPayment.EealityPayment = 0;
            this._acPayment.DomesticMayChargeMoney = 0;
            this._acPayment.Detail = new List<Model.AcPaymentDetail>();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this._acPayManager.Delete(this._acPayment.AcPaymentId);
                this._acPayment = this._acPayManager.GetNext(this._acPayment);
                if (this._acPayment == null)
                {
                    this._acPayment = this._acPayManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override void Save()
        {
            if (this.calcSubscriptionAdvancePayment.Value > this.calcAdvancePaymentBalance.Value)
                throw new Helper.InvalidValueException(Model.AcPayment.PRO_SubscriptionAdvancePayment);
            #region 判断是否计算正确 (已沖總額-折扣總額=本幣實際付款+取預付款額)

            decimal money_1 = (this.calcDomesticMayChargeMoney.EditValue == null ? 0 : this.calcDomesticMayChargeMoney.Value) - (this.calcDomesticCashAgio.EditValue == null ? 0 : this.calcDomesticCashAgio.Value);
            decimal money_2 = (this.calcDomesticEealityPayment.EditValue == null ? 0 : this.calcDomesticEealityPayment.Value) + (this.calcSubscriptionAdvancePayment.EditValue == null ? 0 : this.calcSubscriptionAdvancePayment.Value);
            if (money_1 != money_2)
                throw new Helper.InvalidValueException(Model.AcPayment.PRO_DomesticEealityPayment);

            #endregion
            this._acPayment.AcPaymentId = this.textEditAcPaymentId.Text;
            this._acPayment.AdvancePaymentBalance = this.calcAdvancePaymentBalance.Value;
            //this._acPayment.AuditingState = this.textAuditingState.Text;
            // this._acPayment.AlreadyChargeMoney = this.calcAlreadyChargeMoney.Value;
            // this._acPayment.AtCurrencyCategory = this.newChooseContorlAtCurrencyCategoryId.EditValue as Model.AtCurrencyCategory;
            if (this._acPayment.AtCurrencyCategory != null)
                this._acPayment.AtCurrencyCategoryId = this._acPayment.AtCurrencyCategory.AtCurrencyCategoryId;
            this._acPayment.BankAccount = this.textEditBankAccount.Text;
            this._acPayment.AcDesc = this.memoAcDesc.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditAcPaymentDate.DateTime, new DateTime()))
                this._acPayment.AcPaymentDate = global::Helper.DateTimeParse.NullDate;
            else
                this._acPayment.AcPaymentDate = this.dateEditAcPaymentDate.DateTime;
            this._acPayment.BillNo = this.textEditBillNo.Text;
            // this._acPayment.CashAgio = this.calcCashAgio.Value;
            // this._acPayment.DomesticCashAgio = this.calcEditDomesticCashAgio.Value;
            this._acPayment.DomesticEealityPayment = this.calcDomesticEealityPayment.Value;
            this._acPayment.DomesticMayChargeMoney = this.calcDomesticMayChargeMoney.Value;
            // this._acPayment.EealityPayment = this.calcEealityPayment.Value;
            this._acPayment.Employee = this.newChooseContorlEmployeeId.EditValue as Model.Employee;
            if (this._acPayment.Employee != null)
                this._acPayment.EmployeeId = this._acPayment.Employee.EmployeeId;
            this._acPayment.Employee0 = this.newChooseContorlEmployee0Id.EditValue as Model.Employee;
            if (this._acPayment.Employee0 != null)
                this._acPayment.Employee0Id = this._acPayment.Employee0.EmployeeId;
            this._acPayment.Employee1 = this.newChooseContorlEmployee1Id.EditValue as Model.Employee;
            if (this._acPayment.Employee1 != null)
                this._acPayment.Employee1Id = this._acPayment.Employee1.EmployeeId;
            //this._acPayment.JoinAdvancePayment = this.calcJoinAdvancePayment.Value;
            this._acPayment.PayMethod = this.newChoosePayMethodId.EditValue as Model.PayMethod;
            if (this._acPayment.PayMethod != null)
                this._acPayment.PayMethodId = this._acPayment.PayMethod.PayMethodId;
            this._acPayment.Subject = this.newChooseContorlSubjectId.EditValue as Model.AtAccountSubject;
            if (this._acPayment.Subject != null)
                this._acPayment.SubjectId = this._acPayment.Subject.SubjectId;
            this._acPayment.SubscriptionAdvancePayment = this.calcSubscriptionAdvancePayment.Value;
            this._acPayment.Supplier = this.newChooseContorlSupplierId.EditValue as Model.Supplier;
            if (this._acPayment.Supplier != null)
                this._acPayment.SupplierId = this._acPayment.Supplier.SupplierId;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._acPayManager.Insert(this._acPayment);
                    break;

                case "update":
                    this._acPayManager.Update(this._acPayment);
                    break;
            }
            this._acPayManager.UpdateAcInvoiceXOHeXiao(this._acPayment.Detail);

        }

        public override void Refresh()
        {
            if (this._acPayment == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._acPayment = this._acPayManager.GetDetails(this._acPayment);
                    if (this._acPayment == null)
                    {
                        this._acPayment = new Book.Model.AcPayment();
                    }
                }
            }

            this.textEditAcPaymentId.Text = this._acPayment.AcPaymentId;
            this.calcAdvancePaymentBalance.Text = this._acPayment.AdvancePaymentBalance == null ? "" : this._acPayment.AdvancePaymentBalance.Value.ToString();
            // this.calcAlreadyChargeMoney.Text = this._acPayment.AlreadyChargeMoney == null ? "" : this._acPayment.AlreadyChargeMoney.Value.ToString();
            // this.newChooseContorlAtCurrencyCategoryId.EditValue = this._acPayment.AtCurrencyCategory;
            this.textEditBankAccount.Text = this._acPayment.BankAccount;
            this.memoAcDesc.Text = this._acPayment.AcDesc;
            this.dateEditAcPaymentDate.DateTime = this._acPayment.AcPaymentDate == null ? global::Helper.DateTimeParse.NullDate : this._acPayment.AcPaymentDate.Value;
            this.textEditBillNo.Text = this._acPayment.BillNo;
            // this.calcCashAgio.Text = this._acPayment.CashAgio == null ? "" : this._acPayment.CashAgio.Value.ToString();
            // this.calcEditDomesticCashAgio.Text = this._acPayment.DomesticCashAgio == null ? "" : this._acPayment.DomesticCashAgio.Value.ToString();
            this.calcDomesticEealityPayment.Text = this._acPayment.DomesticEealityPayment == null ? "" : this._acPayment.DomesticEealityPayment.Value.ToString();
            this.calcDomesticMayChargeMoney.Text = this._acPayment.DomesticMayChargeMoney == null ? "" : this._acPayment.DomesticMayChargeMoney.Value.ToString();
            // this.calcEealityPayment.Text = this._acPayment.EealityPayment == null ? "" : this._acPayment.EealityPayment.Value.ToString();
            this.newChooseContorlEmployeeId.EditValue = this._acPayment.Employee;
            this.newChooseContorlEmployee0Id.EditValue = this._acPayment.AuditEmp;
            this.newChooseContorlEmployee1Id.EditValue = this._acPayment.Employee1;
            // this.calcJoinAdvancePayment.Text = this._acPayment.JoinAdvancePayment == null ? "" : this._acPayment.JoinAdvancePayment.Value.ToString();
            this.newChoosePayMethodId.EditValue = this._acPayment.PayMethod;
            this.newChooseContorlSubjectId.EditValue = this._acPayment.Subject;
            this.calcSubscriptionAdvancePayment.Text = this._acPayment.SubscriptionAdvancePayment == null ? "" : this._acPayment.SubscriptionAdvancePayment.Value.ToString();
            this.newChooseContorlSupplierId.EditValue = this._acPayment.Supplier;
            this.textAuditingState.EditValue = this.GetAuditName(this._acPayment.AuditState);
            this.bindingSourceDetails.DataSource = this._acPayment.Detail;
            this.gridView1.GroupPanelText = "共: " + (this._acPayment.Detail == null ? "0" : this._acPayment.Detail.Count + " 項");

            base.Refresh();

            if (this.action == "view")
            {
                this.barBtnInvoiceCG.Enabled = false;
            }
            else
            {
                this.barBtnInvoiceCG.Enabled = true;
            }
            this.calcAdvancePaymentBalance.Enabled = false;
            this.calcDomesticMayChargeMoney.Enabled = false;
            this.calcDomesticCashAgio.Enabled = false;
            this.newChooseContorlEmployeeId.Enabled = false;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new AccountPayable.AcPayment.XR(this._acPayment.AcPaymentId);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Model.AcPaymentDetail currentRow = this.bindingSourceDetails.Current as Model.AcPaymentDetail;
            if (currentRow != null)
                this._acPayment.Detail.Remove(currentRow);
            this.gridControl1.RefreshDataSource();
        }

        private void IbtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm lf = new ListForm();
            if (lf.ShowDialog(this) == DialogResult.OK)
            {
                this._acPayment = (Model.AcPayment)lf.SelectItem;
                this.action = "view";
                this.Refresh();
            }
            lf.Dispose();
            GC.Collect();
        }

        //更换供应商
        private void newChooseContorlSupplierId_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseContorlSupplierId.EditValue != null)
            {
                Model.Supplier sup = this.newChooseContorlSupplierId.EditValue as Model.Supplier;
                this.calcAdvancePaymentBalance.Text = sup.PayableOwe == null ? "0" : sup.PayableOwe.Value.ToString();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal count1 = 0;
            decimal count2 = 0;
            foreach (Model.AcPaymentDetail item in this._acPayment.Detail)
            {
                count1 += this.GetDecimal((item.DomesticThisChargeMoney == null ? 0 : item.DomesticThisChargeMoney.Value), BL.V.SetDataFormat.CGJEXiao.Value);
                count2 += this.GetDecimal((item.DomesticDetailCashAgio == null ? 0 : item.DomesticDetailCashAgio.Value), BL.V.SetDataFormat.CGJEXiao.Value);
            }
            this.calcDomesticMayChargeMoney.Value = count1;
            this.calcDomesticCashAgio.Value = count2;
            this.calcDomesticEealityPayment.Value = this.GetDecimal(this.calcDomesticMayChargeMoney.Value - this.calcDomesticCashAgio.Value, BL.V.SetDataFormat.CGZJXiao.Value);
        }

        //选择采购发票
        private void barBtnInvoiceCG_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AcInvoiceCOBill.ChooseAcInvoiceCOBill f = new AcInvoiceCOBill.ChooseAcInvoiceCOBill();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Model.AcInvoiceCOBill item in f.listAcinvoiceCOBill)
                {
                    Model.AcPaymentDetail detail = new Book.Model.AcPaymentDetail();
                    detail.AcPaymentDetailId = Guid.NewGuid().ToString();
                    detail.AcPaymentId = this._acPayment.AcPaymentId;
                    detail.AcInvoiceId = item.AcInvoiceCOBillId;
                    detail.AcInvoiceType = Properties.Resources.AcInvoiceCOBill;
                    detail.BillId = item.Id;
                    detail.DomesticShouldPaymentMoney = item.ZongMoney == null ? 0 : item.ZongMoney.Value;
                    detail.DomesticNoPaymentMoney = item.NoHeXiaoTotal == null ? 0 : item.NoHeXiaoTotal.Value;
                    this._acPayment.Detail.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        //其它应付款
        private void barBtnAcOtherShouldPayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AcOtherShouldPayment.ChooseAcOtherShouldPayment f = new Book.UI.AccountPayable.AcOtherShouldPayment.ChooseAcOtherShouldPayment();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Model.AcOtherShouldPayment item in f.listAcOtherShouldPayment)
                {
                    Model.AcPaymentDetail detail = new Book.Model.AcPaymentDetail();
                    detail.AcPaymentDetailId = Guid.NewGuid().ToString();
                    detail.AcPaymentId = this._acPayment.AcPaymentId;
                    detail.AcInvoiceId = item.AcOtherShouldPaymentId;
                    detail.AcInvoiceType = Properties.Resources.AcOtherShouldPayment;
                    detail.DomesticShouldPaymentMoney = item.HeJi == null ? 0 : item.HeJi.Value;
                    detail.DomesticNoPaymentMoney = item.NoHeXiaoTotal == null ? 0 : item.NoHeXiaoTotal.Value;
                    this._acPayment.Detail.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.AcPayment.PRO_AcPaymentId;
        }

        protected override int AuditState()
        {
            return this._acPayment.AuditState.HasValue ? this._acPayment.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "AcPayment" + "," + this._acPayment.AcPaymentId;
        }

        #endregion
    }
}