using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.JC
{

    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceJCDetailManager invoiceJRDetailManager = new Book.BL.InvoiceJCDetailManager();
        protected BL.InvoiceJCManager invoiceManager = new Book.BL.InvoiceJCManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();

        private Model.InvoiceJC invoice = null;

        public EditForm()
        {
            InitializeComponent();


            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.NewChooseControlCustomer));

            this.invalidValueExceptions.Add(Model.InvoiceJC.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";
            //this.buttonEditCompany.Choose = new ChooseCompany(global::Helper.CompanyKind.Customer);
            this.NewChooseControlCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.bindingSourceDepot.DataSource = new BL.DepotManager().Select();
            this.bindingSourceproduct.DataSource = this.productManager.GetProduct();
            this.EmpAudit.Choose = new ChooseEmployee();
        }

        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";
        }

        public EditForm(Model.InvoiceJC invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoice;
            this.action = "update";
        }

        protected override string tableCode()
        {
            return "InvoiceJC";
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

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceJCDetail detail = new Book.Model.InvoiceJCDetail();
                detail.InvoiceJCDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                detail.InvoiceJCDetailQuantity = 1;
                detail.InvoiceWeiHuaiRuQuantity = 1;
                detail.InvoiceYiHuaiRuQuantity = 0;
                detail.InvoiceJCDetailNote = "";
                this.invoice.Details.Add(detail);
                this.gridControl1.RefreshDataSource();

                this.bindingSourceInvoiceJCDetail.Position = this.bindingSourceInvoiceJCDetail.IndexOf(detail);


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

        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseCustoms f = new ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        {
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceJC();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceJCDetail>();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();

            if (this.action == "insert")
            {
                Model.InvoiceJCDetail detail = new Model.InvoiceJCDetail();
                detail.InvoiceJCDetailId = Guid.NewGuid().ToString();
                detail.InvoiceHRDetailNote = "";
                detail.InvoiceHRDetailQuantity = 0;
                detail.InvoiceJCDetailNote = "";
                detail.InvoiceJCDetailQuantity = 0;
                detail.InvoiceProductUnit = "";
                detail.InvoiceWeiHuaiRuQuantity = 0;
                detail.InvoiceYiHuaiRuQuantity = 0;
                detail.Product = new Book.Model.Product();
                this.invoice.Details.Add(detail);
                this.bindingSourceInvoiceJCDetail.Position = this.bindingSourceInvoiceJCDetail.IndexOf(detail);
            }
        }

        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoice.InvoiceId);
        }

        public override BaseListForm GetListForm()
        {
            return new ListForm();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return base.GetReport();
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

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return invoice;
            }
            set
            {
                if (value is Model.InvoiceJC)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceJC).InvoiceId);
                }
            }
        }

        protected override void MoveFirst()
        {
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetFirst() == null ? "" : this.invoiceManager.GetFirst().InvoiceId);
        }

        protected override void MoveLast()
        {
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetLast() == null ? "" : this.invoiceManager.GetLast().InvoiceId);
        }

        protected override void MoveNext()
        {
            Model.InvoiceJC invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceJC invoice = this.invoiceManager.GetPrev(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);

        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceJC();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.NewChooseControlCustomer.EditValue = this.invoice.Customer;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditDepot.EditValue = this.invoice.DepotId;
            this.textEditInvoiceSendAddress.EditValue = this.invoice.InvoiceSendAddress;

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSourceInvoiceJCDetail.DataSource = this.invoice.Details;

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.NewChooseControlCustomer.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot.Properties.ReadOnly = false;

                    this.NewChooseControlCustomer.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.textEditInvoiceSendAddress.Properties.ReadOnly = false;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditInvoiceSendAddress.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.NewChooseControlCustomer.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot.Properties.ReadOnly = false;

                    this.NewChooseControlCustomer.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.textEditInvoiceSendAddress.Properties.ReadOnly = false;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditInvoiceSendAddress.Properties.ReadOnly = true;
                    this.textEditNote.Properties.ReadOnly = true;
                    this.textEditInvoiceSendAddress.Properties.ReadOnly = true;

                    this.NewChooseControlCustomer.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;
                    this.buttonEditDepot.Properties.ReadOnly = true;

                    this.NewChooseControlCustomer.ButtonReadOnly = true;
                    this.buttonEditEmployee.ButtonReadOnly = true;

                    this.simpleButtonAppend.Enabled = false;
                    this.simpleButtonRemove.Enabled = false;
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;

                default:
                    break;
            }

            base.Refresh();
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            this.invoice.DepotId = this.buttonEditDepot.EditValue.ToString();
            this.invoice.InvoiceNote = this.textEditNote.Text;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;
            if (this.NewChooseControlCustomer.EditValue != null)
            {
                this.invoice.Customer = this.NewChooseControlCustomer.EditValue as Model.Customer;
                this.invoice.CustomerId = this.invoice.Customer.CustomerId;
            }

            this.invoice.InvoiceSendAddress = this.textEditInvoiceSendAddress.Text;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            double? quantity = 0;

            foreach (Model.InvoiceJCDetail detail in this.invoice.Details)
            {
                quantity += detail.InvoiceJCDetailQuantity;
            }

            this.invoice.InvoiceJCQuantity = quantity;

            this.invoice.AuditState = this.saveAuditState;

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

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceInvoiceJCDetail.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSourceInvoiceJCDetail.Current as Book.Model.InvoiceJCDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceJCDetail detail = new Model.InvoiceJCDetail();
                    detail.InvoiceJCDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceHRDetailNote = "";
                    detail.InvoiceHRDetailQuantity = 0;
                    detail.InvoiceJCDetailNote = "";
                    detail.InvoiceJCDetailQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    detail.InvoiceWeiHuaiRuQuantity = 0;
                    detail.InvoiceYiHuaiRuQuantity = 0;
                    detail.Product = new Book.Model.Product();
                    this.invoice.Details.Add(detail);
                    this.bindingSourceInvoiceJCDetail.Position = this.bindingSourceInvoiceJCDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumn3")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceJCDetail).Product;

                    this.repositoryItemComboBox1.Items.Clear();
                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                        IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit item in unitList)
                        {
                            this.repositoryItemComboBox1.Items.Add(item.CnName);
                        }

                    }

                    //if (!string.IsNullOrEmpty(p.ProductBaseUnit))
                    //{
                    //    this.repositoryItemComboBox1.Items.Add(p.ProductBaseUnit);
                    //}
                    //if (!string.IsNullOrEmpty(p.ProductInnerPackagingUnit))
                    //{
                    //    this.repositoryItemComboBox1.Items.Add(p.ProductInnerPackagingUnit);
                    //}
                    //if (!string.IsNullOrEmpty(p.ProductOuterPackagingUnit))
                    //{
                    //    this.repositoryItemComboBox1.Items.Add(p.ProductOuterPackagingUnit);
                    //}
                }
            }

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;

            IList<Model.InvoiceJCDetail> invoiceCgDetails = this.bindingSourceInvoiceJCDetail.DataSource as IList<Model.InvoiceJCDetail>;

            if (invoiceCgDetails == null || invoiceCgDetails.Count <= 0)
                return;

            switch (e.Column.Name)
            {
                case "gridColumn4":
                    e.DisplayText = invoiceCgDetails[e.ListSourceRowIndex].Product.ProductSpecification;
                    break;
                case "gridColumn5":
                    e.DisplayText = invoiceCgDetails[e.ListSourceRowIndex].Product.ProductSpecification;
                    break;
            }
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.textEditInvoiceId, this });
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId)
            {
                Model.InvoiceJCDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceJCDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.InvoiceJCDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceHRDetailNote = "";
                    detail.InvoiceHRDetailQuantity = 0;
                    detail.InvoiceJCDetailNote = "";
                    detail.InvoiceJCDetailQuantity = 0;
                    detail.InvoiceWeiHuaiRuQuantity = 0;
                    detail.InvoiceYiHuaiRuQuantity = 0;
                    detail.InvoiceProductUnit = p.ProduceUnit.CnName;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                    this.bindingSourceInvoiceJCDetail.Position = this.bindingSourceInvoiceJCDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }
        private bool CanAdd(IList<Model.InvoiceJCDetail> list)
        {

            foreach (Model.InvoiceJCDetail detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.CanAdd(this.invoice.Details))
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        Model.InvoiceJCDetail detail = new Model.InvoiceJCDetail();
                        detail.InvoiceJCDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceHRDetailNote = "";
                        detail.InvoiceHRDetailQuantity = 0;
                        detail.InvoiceJCDetailNote = "";
                        detail.InvoiceJCDetailQuantity = 0;
                        detail.InvoiceProductUnit = "";
                        detail.InvoiceWeiHuaiRuQuantity = 0;
                        detail.InvoiceYiHuaiRuQuantity = 0;
                        detail.Product = new Book.Model.Product();
                        this.invoice.Details.Add(detail);
                        this.bindingSourceInvoiceJCDetail.Position = this.bindingSourceInvoiceJCDetail.IndexOf(detail);
                    }
                }
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButtonRemove.PerformClick();
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void buttonEditDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.buttonEditDepot.EditValue != null)
            {
                string depot = this.buttonEditDepot.EditValue.ToString();
                this.bindingSourceDepotPosition.DataSource = new BL.DepotPositionManager().Select(depot);
            }
        }
    }
}