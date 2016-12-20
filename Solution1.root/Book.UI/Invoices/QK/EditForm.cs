using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.QK
{
    public partial class EditForm : BaseEditForm
    {

        private BL.InvoiceQKManager invoiceManager = new BL.InvoiceQKManager();
        private Model.InvoiceQK invoice;

        #region Constructors

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));
            this.requireValueExceptions.Add("Total", new AA(Properties.Resources.RequireDataForMoney, this.calcEditMoney1));

            this.invalidValueExceptions.Add(Model.InvoiceQK.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            
            this.action = "view";

            this.buttonEditCompany.Choose = new ChooseCompany(global::Helper.CompanyKind.Customer);
            this.buttonEditEmployee.Choose = new ChooseEmployee();
        }

        public EditForm(Model.InvoiceQK invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            this.action = "update";
        }

        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);

            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";           
            
        }

        //public EditForm(Model.Company company)
        //    : this()
        //{
        //    if (company == null)
        //        throw new ArgumentNullException("company");

        //    this.invoice = new Book.Model.InvoiceQK();
        //    ////this.invoice.InvoiceId = this.invoiceManager.GetNewId();
        //    this.invoice.InvoiceDate = DateTime.Today;
        //    this.invoice.Company = company;
        //    this.invoice.InvoiceTotal0 = company.CompanyR1;
        //    this.invoice.InvoiceTotal1 = company.CompanyR1;
        //}

        #endregion

        #region FormLoad

        private void EditForm_Load(object sender, EventArgs e)
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceQK();
                ////this.invoice.InvoiceId = this.invoiceManager.GetNewId();
                this.invoice.InvoiceDate = DateTime.Today;
            }

            this.textEditInvoiceId.Text = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.EditValue = this.invoice.InvoiceDate;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            //this.textEditAbstract.Text = this.invoice.InvoiceAbstract;
            this.textEditNote.Text = this.invoice.InvoiceNote;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.calcEditMoney0.EditValue = this.invoice.InvoiceTotal0;
            this.calcEditMoney1.EditValue = this.invoice.InvoiceTotal1;
            
        }

        #endregion

        #region Choose Object

        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseCustoms f = new ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
                //this.calcEditMoney0.EditValue = (f.SelectedItem as Model.Company).CompanyR1;
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
            //this.invoice.Company = this.buttonEditCompany.EditValue as Model.Company;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            //this.invoice.Employee2 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            this.invoice.InvoiceTotal0 = this.calcEditMoney0.Value;
            this.invoice.InvoiceTotal1 = this.calcEditMoney1.Value;

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
                if (value is Model.InvoiceQK)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceQK).InvoiceId);
                }
            }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceQK();

            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
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

        protected override void MoveNext()
        {
            Model.InvoiceQK invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceQK invoice = this.invoiceManager.GetPrev(this.invoice);
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
                this.invoice = new Book.Model.InvoiceQK();
                this.action = "insert";
            }
            this.textEditInvoiceId.Text = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.EditValue = this.invoice.InvoiceDate;

            //this.textEditAbstract.Text = this.invoice.InvoiceAbstract;
            this.textEditNote.Text = this.invoice.InvoiceNote;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditCompany.EditValue = this.invoice.Customer;

            this.calcEditMoney0.EditValue = this.invoice.InvoiceTotal0;
            this.calcEditMoney1.EditValue = this.invoice.InvoiceTotal1;
            
            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;
                    this.calcEditMoney1.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.calcEditMoney1.Properties.ReadOnly = false;                    
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.calcEditMoney1.Properties.ReadOnly = true;
                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditCompany.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;

                    this.buttonEditCompany.ButtonReadOnly = true;
                    this.buttonEditEmployee.ButtonReadOnly = true;
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
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.calcEditMoney1, this.textEditInvoiceId, this });
        }
    }
}