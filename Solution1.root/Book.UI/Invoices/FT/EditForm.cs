using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.FT
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceFTManager invoiceManager = new Book.BL.InvoiceFTManager();

        private Model.InvoiceFT invoice;

        #region Constructors

        public EditForm()
        {
            InitializeComponent();
            
            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Total", new AA(Properties.Resources.RequireDataForMoney, this.calcEditTotal));
            this.requireValueExceptions.Add("Account1", new AA(Properties.Resources.RequireDataForAccount, this.buttonEditAccount1));
            this.requireValueExceptions.Add("Account2", new AA(Properties.Resources.RequireDataForAccount, this.buttonEditAccount2));

            this.invalidValueExceptions.Add(Model.InvoiceFT.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.action = "view";

            this.buttonEditAccount1.Choose = new ChooseAccount();
            this.buttonEditAccount2.Choose = new ChooseAccount();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
        }
        int LastFlag = 0;
        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.InvoiceFT invoiceFT):this()
        {
            if (invoiceFT == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoiceFT;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        #endregion

        #region Choose Objcet

        private void buttonEditEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditAccount1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseAccountForm f = new ChooseAccountForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditAccount2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseAccountForm f = new ChooseAccountForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Save

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            this.invoice.Account1 = this.buttonEditAccount1.EditValue as Model.Account;
            this.invoice.Account2 = this.buttonEditAccount2.EditValue as Model.Account;
            this.invoice.InvoiceTotal = this.calcEditTotal.Value;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;

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

        public override BaseListForm GetListForm()
        {
            return new ListForm();
        }

        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoice.InvoiceId);
        }

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return invoice;
            }
            set
            {
                if (value is Model.InvoiceFT)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceFT).InvoiceId);
                }
            }
        }
        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceFT();

            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
        }

        protected override void MoveNext()
        {
            Model.InvoiceFT invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceFT invoice = this.invoiceManager.GetPrev(this.invoice);
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
            if (LastFlag == 1) { LastFlag = 0; return; }
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetLast() == null ? "" : this.invoiceManager.GetLast().InvoiceId);   
        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceFT();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditAccount1.EditValue = this.invoice.Account1;
            this.buttonEditAccount2.EditValue = this.invoice.Account2;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;

            
            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;

                    this.calcEditTotal.Properties.ReadOnly = false;

                    //this.textEditAbstract.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditAccount1.ShowButton = true;
                    this.buttonEditAccount2.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;

                    this.buttonEditAccount1.ButtonReadOnly = false;
                    this.buttonEditAccount2.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.calcEditTotal.Properties.ReadOnly = false;

                    //this.textEditAbstract.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditAccount1.ShowButton = true;
                    this.buttonEditAccount2.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;

                    this.buttonEditAccount1.ButtonReadOnly = false;
                    this.buttonEditAccount2.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.calcEditTotal.Properties.ReadOnly = true;

                    //this.textEditAbstract.Properties.ReadOnly = true;
                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditAccount1.ShowButton = false;
                    this.buttonEditAccount2.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;

                    this.buttonEditAccount1.ButtonReadOnly = true;
                    this.buttonEditAccount2.ButtonReadOnly = true;
                    this.buttonEditEmployee.ButtonReadOnly = true;
                    break;

                default:
                    break;
            }
            base.Refresh();
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
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.calcEditTotal,this.textEditInvoiceId, this });
        }
    }
}