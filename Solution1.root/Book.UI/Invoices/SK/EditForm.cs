using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;

namespace Book.UI.Invoices.SK
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.Invoice01Manager invoice01Manager = new Book.BL.Invoice01Manager();
        protected BL.InvoiceSKManager invoiceManager = new Book.BL.InvoiceSKManager();

        /// <summary>
        /// 被修改的单据
        /// </summary>        
        private Model.InvoiceSK invoice;

        #region Constructors

        public EditForm()
        {
            InitializeComponent();
            
            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));
            this.requireValueExceptions.Add("PayMethod", new AA(Properties.Resources.RequireDataForPayMethod, this.buttonEditPayMethod));
            this.requireValueExceptions.Add("Total", new AA(Properties.Resources.RequireDataForMoney, this.calcEditTotal));
            this.requireValueExceptions.Add("Total2", new AA(Properties.Resources.Total2, this.calcEditTotal));
            this.requireValueExceptions.Add("Account", new AA(Properties.Resources.RequireDataForAccount, this.buttonEditAccount));

            this.invalidValueExceptions.Add(Model.InvoiceSK.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.invalidValueExceptions.Add("Total.ValueMustGreaterThanZero", new AA(Properties.Resources.ValueMustGreaterThanZero, this.calcEditTotal));
            
            this.action = "view";
            this.buttonEditAccount.Choose = new ChooseAccount();
            this.buttonEditCompany.Choose = new ChooseCompany(global::Helper.CompanyKind.Customer);
            this.buttonEditEmployee.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.buttonEditPayMethod.Choose = new ChoosePayMethod();
            this.EmpAudit.Choose = new ChooseEmployee();
        }

        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (this.invoice == null)            
                throw new ArithmeticException("invoiceid");            
            this.action = "update";
        }

        public EditForm(Model.InvoiceSK invoice)
            : this()
        {
            if (invoice == null)            
                throw new ArithmeticException("invoiceid");            
            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            this.action = "update";
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            
        }

        protected override string tableCode()
        {
            return "InvoiceSK";
        }

        protected override int AuditState()
        {
            return this.invoice.AuditState.HasValue ? this.invoice.AuditState.Value : 0;
        }

        protected override string getName()
        {
            string formName = this.GetType().FullName;
            formName = formName.Substring(formName.IndexOf('.') + 1).Substring(formName.Substring(formName.IndexOf('.') + 1).IndexOf('.') + 1);
            return formName;
        }

        #endregion

        #region  Choose Object

        private void buttonEditAccount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseAccountForm f = new ChooseAccountForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseCustoms f = new ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
                //this.invoice.Details = this.invoice01Manager.Select("Sk", (f.SelectedItem as Model.Company).CompanyId);
                //this.textEditPay.EditValue = (f.SelectedItem as Model.Company).CompanyR1;
                this.bindingSource1.DataSource = this.invoice.Details;
            }
        }

        private void buttonEditEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditPayMethod_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChoosePayMethodForm f = new ChoosePayMethodForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        #endregion

        #region Save

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            this.invoice.Account = this.buttonEditAccount.EditValue as Model.Account;
            //this.invoice.Company = this.buttonEditCompany.EditValue as Model.Company;
            this.invoice.PayMethod = this.buttonEditPayMethod.EditValue as Model.PayMethod;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            this.invoice.InvoiceTotal = this.calcEditTotal.Value;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;
            this.invoice.AuditState = this.saveAuditState;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.invoiceManager.Insert(this.invoice);
                    break;

                case "update":
                    this.invoiceManager.Update(this.invoice);
                    break;
            }
        }

        #endregion

        #region Help

        /// <summary>
        /// 分配金额
        /// </summary>
        private void allot()
        {
            decimal money = this.calcEditTotal.Value;
            if (money <= 0)
            {
                this.calcEditTotal.SelectAll();
                this.calcEditTotal.Focus();
                return;
            }
            foreach (Model.Invoice01 invoice01 in this.invoice.Details)
            {
                if (money <= 0)
                {
                    invoice01.PayReceived = decimal.Zero;
                }
                else
                {
                    if (money - invoice01.InvoiceOwed.Value >= 0)
                    {
                        invoice01.PayReceived = invoice01.InvoiceOwed;
                    }
                    else
                    {
                        invoice01.PayReceived = money;
                    }
                    money = money - invoice01.InvoiceOwed.Value;
                }
            }
            this.gridControl1.RefreshDataSource();
        }
        #endregion

        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoice.InvoiceId);
        }

        public override BaseListForm GetListForm()
        {
            return new ListForm();
        }

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return invoice;
            }
            set
            {
                if (value is Model.InvoiceSK)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceSK).InvoiceId);
                }
            }
        }

        protected override void TurnNull()
        {
            if (this.invoice == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.invoiceManager.TurnNull(this.invoice.InvoiceId);
            this.invoice = this.invoiceManager.GetNext(this.invoice);
            if (this.invoice == null)
            {
                this.invoice = this.invoiceManager.GetLast();
            }
        }
        /// <summary>
        /// linkLabel1_LinkClicked
        /// </summary>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.allot();
        }

        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            decimal? origal = (decimal?)view.GetRowCellValue(e.RowHandle, this.colPayReceived);

            if (e.Column == this.colPayReceived)
            {
                decimal pay = Convert.ToDecimal(e.Value);
                decimal invoiceOwed = (decimal)view.GetRowCellValue(e.RowHandle, this.colInvoiceOwed);

                if (pay > invoiceOwed)
                {
                    this.gridView1.SetRowCellValue(e.RowHandle, this.colPayReceived, origal);
                    this.gridControl1.RefreshDataSource();
                    MessageBox.Show(Properties.Resources.FkMoneyCantGtWeiFuMoney, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceSK();

            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            //this.invoice.Company = new Book.Model.Company();
            this.invoice.Details = new List<Model.Invoice01>();
        }

        protected override void MoveNext()
        {
            Model.InvoiceSK invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceSK invoice = this.invoiceManager.GetPrev(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MoveFirst()
        {
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetFirst() == null ? "" : this.invoiceManager.GetFirst().InvoiceId);
        }

        protected override void MoveLast()
        {
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetLast() == null ? "" : this.invoiceManager.GetLast().InvoiceId);   
        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceSK();
                this.action = "insert";
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditAccount.EditValue = this.invoice.Account;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.buttonEditPayMethod.EditValue = this.invoice.PayMethod;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;
            this.textEditPay.EditValue = this.invoice.Customer == null ? decimal.Zero : this.invoice.Customer.CustomerReceivable;
            this.invoice.Details = this.invoice01Manager.Select("SK_UPDATE",invoice.Customer==null?null:invoice.Customer.CustomerId, this.invoice.InvoiceId);


            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSource1.DataSource = this.invoice.Details;

            
            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;

                    this.calcEditTotal.Properties.ReadOnly = false;

                    //this.textEditAbstract.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditAccount.ShowButton = true;
                    this.buttonEditPayMethod.ShowButton = true;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditAccount.ButtonReadOnly = false;
                    this.buttonEditPayMethod.ButtonReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;

                    this.calcEditTotal.Properties.ReadOnly = false;

                    //this.textEditAbstract.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditAccount.ShowButton = true;
                    this.buttonEditPayMethod.ShowButton = true;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditAccount.ButtonReadOnly = false;
                    this.buttonEditPayMethod.ButtonReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;

                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.calcEditTotal.Properties.ReadOnly = true;

                    //this.textEditAbstract.Properties.ReadOnly = true;
                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditCompany.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;
                    this.buttonEditAccount.ShowButton = false;
                    this.buttonEditPayMethod.ShowButton = false;

                    this.buttonEditCompany.ButtonReadOnly = true;
                    this.buttonEditEmployee.ButtonReadOnly = true;
                    this.buttonEditAccount.ButtonReadOnly = true;
                    this.buttonEditPayMethod.ButtonReadOnly = true;

                    this.gridView1.OptionsBehavior.Editable = false;

                    break;

                default:
                    break;
            }

            base.Refresh();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R01(this.invoice.InvoiceId);
        }

        protected override bool HasRows()
        {
            return this.invoiceManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.invoiceManager.HasRowsAfter(this.invoice);
        }

        protected override bool HasRowsPrev()
        {
            return this.invoiceManager.HasRowsBefore(this.invoice);
        }

        private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        {
            if (this.action == "insert"){this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime);}
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.textEditInvoiceId, this.calcEditTotal, this });
        }
    }
}