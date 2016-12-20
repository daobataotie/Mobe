using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.ZS
{
    public partial class EditForm : BaseEditForm
    {
        BL.InvoiceZSManager invoiceManager = new Book.BL.InvoiceZSManager();
        BL.InvoiceZSDetailManager invoiceZSDetailManager = new Book.BL.InvoiceZSDetailManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private BL.CustomerProductsManager customerProductManager = new Book.BL.CustomerProductsManager();
        protected BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();

        private Model.InvoiceZS invoice;

        #region Constructors

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));

            this.invalidValueExceptions.Add(Model.InvoiceZS.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.action = "view";
            // this.buttonEditCompany.Choose = new ChooseCompany(global::Helper.CompanyKind.Customer);
            this.buttonEditCompany.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.buttonEditDepot.Choose = new ChooseDepot();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.EmpAudit.Choose = new ChooseEmployee();

        }

        public EditForm(Model.InvoiceZS invoicezs)
            : this()
        {
            if (invoicezs == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoicezs;
            this.action = "update";
        }

        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = invoiceManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";
        }

        #endregion

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

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSource2.DataSource = this.customerProductManager.Select();
        }

        protected override string tableCode()
        {
            return "InvoiceZS";
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

        #region Save

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;

            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            //this.invoice.Depot = this.buttonEditDepot.EditValue as Model.Depot;
            this.invoice.Customer = this.buttonEditCompany.EditValue as Model.Customer;
            this.invoice.InvoiceTotal = this.calcEditTotal.Value;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;
            if (this.buttonEditDepot.EditValue != null)
            {
                this.invoice.Depot = this.buttonEditDepot.EditValue as Model.Depot;
                this.invoice.DepotId = (this.buttonEditDepot.EditValue as Model.Depot).DepotId;
            }

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

        #region Choose Object

        private void buttonEditEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditDepot_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseDepotForm f = new ChooseDepotForm();
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

        #endregion

        #region simpleButton_Click

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            Model.Customer customer = this.buttonEditCompany.EditValue as Model.Customer;
            if (customer == null)
            {
                MessageBox.Show("Õˆßx„t¿Í‘ô£¡", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.bindingSource2.DataSource = this.customerProductManager.Select(customer);

            Book.UI.Settings.BasicData.Customs.ChooseCustomerProductForm f = new Book.UI.Settings.BasicData.Customs.ChooseCustomerProductForm(customer);



            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceZSDetail detail = new Book.Model.InvoiceZSDetail();
                detail.InvoiceZSDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                // detail.Product = f.SelectedItem as Model.CustomerProducts;

                Model.CustomerProducts product = f.SelectedItem as Model.CustomerProducts;
                detail.PrimaryKey = product;
                detail.PrimaryKeyId = product.PrimaryKeyId;
                //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                detail.InvoiceZSDetailQuantity = 0;
                detail.InvoiceZSDetailPrice = decimal.Zero;
                detail.InvoiceZSDetailMoney = decimal.Zero;
                detail.InvoiceZSDetailNote = "";
                this.invoice.Details.Add(detail);
                this.gridControl1.RefreshDataSource();

                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);


            }
        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoiceZSDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceZSDetail detail = new Model.InvoiceZSDetail();
                    detail.InvoiceZSDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceZSDetailMoney = 0;
                    detail.InvoiceZSDetailNote = "";
                    detail.InvoiceZSDetailPrice = 0;
                    detail.InvoiceZSDetailQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    detail.PrimaryKey = new Book.Model.CustomerProducts();
                    this.invoice.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.Total();
            }
        }

        #endregion

        #region gridView1_CellValueChanged

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column == this.colInvoiceZSDetailPrice || e.Column == this.colInvoiceZSDetailQuantity)
            {
                decimal price = decimal.Zero;
                decimal quantity = decimal.Zero;

                if (e.Column == this.colInvoiceZSDetailPrice)
                {
                    decimal.TryParse(e.Value.ToString(), out price);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceZSDetailQuantity).ToString(), out quantity);
                }
                if (e.Column == this.colInvoiceZSDetailQuantity)
                {
                    decimal.TryParse(e.Value.ToString(), out quantity);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceZSDetailPrice).ToString(), out price);
                }

                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceZSDetailMoney, price * quantity);
                this.Total();
            }
        }

        private void Total()
        {
            decimal total = decimal.Zero;
            foreach (Model.InvoiceZSDetail detail in this.invoice.Details)
            {
                total += detail.InvoiceZSDetailMoney.Value;
            }
            this.calcEditTotal.EditValue = total;
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
                if (value is Model.InvoiceZS)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceZS).InvoiceId);
                }
            }
        }

        protected override void AddNew()
        {
            this.bindingSource3.DataSource = null;
            this.invoice = new Model.InvoiceZS();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceZSDetail>();


            if (this.action == "insert")
            {
                Model.InvoiceZSDetail detail = new Model.InvoiceZSDetail();
                detail.InvoiceZSDetailId = Guid.NewGuid().ToString();
                detail.InvoiceZSDetailMoney = 0;
                detail.InvoiceZSDetailNote = "";
                detail.InvoiceZSDetailPrice = 0;
                detail.InvoiceZSDetailQuantity = 0;
                detail.InvoiceProductUnit = "";
                detail.PrimaryKey = new Book.Model.CustomerProducts();
                this.invoice.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        protected override void MoveNext()
        {
            Model.InvoiceZS invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceZS invoice = this.invoiceManager.GetPrev(this.invoice);
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
                this.invoice = new Book.Model.InvoiceZS();
                this.action = "insert";
            }

            if (this.action == "view")
            {
                this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }

            if (invoice.Depot != null)
            {
                this.bindingSource3.DataSource = this.depotPositionManager.Select(invoice.Depot);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.buttonEditDepot.EditValue = this.invoice.Depot;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditDepot.EditValue = this.invoice.Depot;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;
            this.bindingSource1.DataSource = this.invoice.Details;
            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSource2.DataSource = this.customerProductManager.Select(this.buttonEditCompany.EditValue as Model.Customer);

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditDepot.ButtonReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot.ShowButton = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditDepot.ButtonReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot.ShowButton = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;

                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.calcEditTotal.Properties.ReadOnly = true;
                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditCompany.ButtonReadOnly = true;
                    this.buttonEditEmployee.ButtonReadOnly = true;
                    this.buttonEditDepot.ButtonReadOnly = true;

                    this.buttonEditCompany.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;
                    this.buttonEditDepot.ShowButton = false;

                    this.simpleButtonAppend.Enabled = false;
                    this.simpleButtonRemove.Enabled = false;
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
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
        }
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumn1")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.CustomerProducts p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceZSDetail).PrimaryKey;

                    this.repositoryItemComboBox1.Items.Clear();
                    // if (!string.IsNullOrEmpty(p.UnitGroup))
                    {
                        BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                        IList<Model.ProductUnit> unitList = manager.Select(p.UnitGroupId);
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

        protected void dateEditInvoiceDate_Fouce()
        {
            this.dateEditInvoiceDate.Focus();
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.textEditInvoiceId, this });
        }

        #region gridView1_CellValueChanging

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.InvoiceZSDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceZSDetail;
            if (e.Column == this.gridproductId)
            {

                if (detail != null)
                {
                    Model.CustomerProducts p = customerProductManager.Get(e.Value.ToString());
                    detail.InvoiceZSDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceZSDetailMoney = 0;
                    detail.InvoiceZSDetailNote = "";
                    detail.InvoiceZSDetailPrice = 0;
                    detail.InvoiceZSDetailQuantity = 0;

                    detail.PrimaryKey = p;
                    detail.PrimaryKeyId = p.PrimaryKeyId;
                    //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            if (e.Column == this.gridColumnDepotPisition)
            {
                if (detail != null)
                {
                    Model.DepotPosition position = new BL.DepotPositionManager().Get(e.Value.ToString());
                    detail.DepotPosition = position;
                    detail.DepotPositionId = position.DepotPositionId;
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        #endregion

        private bool CanAdd(IList<Model.InvoiceZSDetail> list)
        {
            foreach (Model.InvoiceZSDetail detail in list)
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
                        Model.InvoiceZSDetail detail = new Model.InvoiceZSDetail();
                        detail.InvoiceZSDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceZSDetailMoney = 0;
                        detail.InvoiceZSDetailNote = "";
                        detail.InvoiceZSDetailPrice = 0;
                        detail.InvoiceZSDetailQuantity = 0;
                        detail.InvoiceProductUnit = "";
                        detail.Product = new Book.Model.Product();
                        this.invoice.Details.Add(detail);
                        this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
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
                Model.Depot depot = this.buttonEditDepot.EditValue as Model.Depot;
                this.bindingSource3.DataSource = new BL.DepotPositionManager().Select(depot);
            }

        }

        private void buttonEditCompany_EditValueChanged(object sender, EventArgs e)
        {
            if (this.buttonEditCompany.EditValue != null)
                this.bindingSource2.DataSource = this.customerProductManager.Select(this.buttonEditCompany.EditValue as Model.Customer);
        }
    }
}