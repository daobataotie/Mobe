using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.HZ
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceHZDetailManager invoiceHZDetailManager = new Book.BL.InvoiceHZDetailManager();
        protected BL.InvoiceHZManager invoiceManager = new Book.BL.InvoiceHZManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        protected BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private Model.InvoiceHZ invoice;

        #region Constructors

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Supplier", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));

            this.invalidValueExceptions.Add(Model.InvoiceHZ.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));


            this.action = "view";

            //this.buttonEditCompany.Choose = new ChooseCompany(global::Helper.CompanyKind.Supplier);
            this.buttonEditCompany.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.buttonEditDepot.Choose = new ChooseDepot();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
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

        public EditForm(Model.InvoiceHZ initInvoiceHZ)
            : this()
        {
            if (initInvoiceHZ == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = initInvoiceHZ;
            this.action = "update";
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceProduct.DataSource = this.productManager.GetProduct();
        }

        protected override string tableCode()
        {
            return "InvoiceHZ";
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

        private void update()
        {
            this.invoice.Details = this.invoiceHZDetailManager.Select(this.invoice);

            global::Helper.InvoiceStatus status = (Helper.InvoiceStatus)this.invoice.InvoiceStatus.Value;

            this.textEditInvoiceId.Properties.ReadOnly = true;


            //this.textEditAbstract.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);
            this.textEditNote.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);

            this.buttonEditEmployee.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.buttonEditDepot.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.buttonEditCompany.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.calcEditTotal.Enabled = (status == global::Helper.InvoiceStatus.Draft);


            this.barButtonItemSave.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.simpleButtonAppend.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.simpleButtonRemove.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.colInvoiceHZDetailPrice.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceHZDetailQuantity.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceHZDetailNote.OptionsColumn.AllowEdit = (status != global::Helper.InvoiceStatus.Null);
        }

        #endregion

        #region gridView1_CellValueChanged


        private void Total()
        {
            decimal total = decimal.Zero;
            foreach (Model.InvoiceHZDetail detail in this.invoice.Details)
            {
                total += detail.InvoiceHZDetailMoney.Value;
            }
            this.calcEditTotal.EditValue = total;
        }

        #endregion

        #region gridView1_CellValueChanging

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column == this.colInvoiceHZDetailPrice || e.Column == this.colInvoiceHZDetailQuantity)
            {
                decimal price = decimal.Zero;
                decimal quantity = decimal.Zero;

                if (e.Column == this.colInvoiceHZDetailPrice)
                {
                    decimal.TryParse(e.Value.ToString(), out price);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceHZDetailQuantity).ToString(), out quantity);
                }
                if (e.Column == this.colInvoiceHZDetailQuantity)
                {
                    decimal.TryParse(e.Value.ToString(), out quantity);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceHZDetailPrice).ToString(), out price);
                }

                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceHZDetailMoney, price * quantity);
                this.Total();
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            Model.InvoiceHZDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceHZDetail;
            if (e.Column == this.colProductId)
            {

                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.InvoiceHZDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceHZDetailMoney = 0;
                    detail.InvoiceHZDetailNote = "";
                    detail.InvoiceHZDetailPrice = 0;
                    detail.InvoiceHZDetailQuantity = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    //detail.InvoiceCGDetailPrice = detail.Product.ProductCurrentCGPrice == null ? 0 : detail.Product.ProductCurrentCGPrice.Value;
                    //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                    //detail.InvoiceCGDetailMoney0 = Convert.ToDecimal(detail.InvoiceCGDetailQuantity.Value) * detail.InvoiceCGDetailPrice;
                    //detail.InvoiceCGDetailMoney1 = detail.InvoiceCGDetailMoney0;
                    this.bindingSourceInvoiceHZDetail.Position = this.bindingSourceInvoiceHZDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            if (e.Column == this.DepotPositionId)
            {
                if (detail != null)
                {
                    Model.DepotPosition position = new BL.DepotPositionManager().Get(e.Value.ToString());
                    detail.DepotPosition = position;

                    if (position != null)
                        detail.DepotPositionId = position.DepotPositionId;

                }
                this.gridControl1.RefreshDataSource();
            }
        }

        #endregion

        #region simpleButton_Click

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceInvoiceHZDetail.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSourceInvoiceHZDetail.Current as Book.Model.InvoiceHZDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceHZDetail detail = new Model.InvoiceHZDetail();
                    detail.InvoiceHZDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceHZDetailMoney = 0;
                    detail.InvoiceHZDetailNote = "";
                    detail.InvoiceHZDetailPrice = 0;
                    detail.InvoiceHZDetailQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.invoice.Details.Add(detail);
                    this.bindingSourceInvoiceHZDetail.Position = this.bindingSourceInvoiceHZDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.Total();
            }
        }

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceHZDetail detail = new Book.Model.InvoiceHZDetail();
                detail.InvoiceHZDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                detail.InvoiceHZDetailQuantity = 0;
                detail.InvoiceHZDetailPrice = decimal.Zero;
                detail.InvoiceHZDetailMoney = decimal.Zero;
                detail.InvoiceHZDetailNote = "";
                this.invoice.Details.Add(detail);
                this.gridControl1.RefreshDataSource();

                this.bindingSourceInvoiceHZDetail.Position = this.bindingSourceInvoiceHZDetail.IndexOf(detail);

                this.bindingSourceProduct.DataSource = this.productManager.Select();
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
            ChooseSupplier f = new ChooseSupplier();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        #endregion

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            //this.invoice.Company = this.buttonEditCompany.EditValue as Model.Company;
            //this.invoice.Depot = this.buttonEditDepot.EditValue as Model.Depot;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            this.invoice.InvoiceTotal = this.calcEditTotal.Value;
            this.invoice.Supplier = this.buttonEditCompany.EditValue as Model.Supplier;
            if (this.invoice.Supplier != null)
                this.invoice.SupplierId = (this.buttonEditCompany.EditValue as Model.Supplier).SupplierId;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            this.invoice.Depot = this.buttonEditDepot.EditValue as Model.Depot;
            if (this.invoice.Depot != null)
                this.invoice.DepotId = (this.buttonEditDepot.EditValue as Model.Depot).DepotId;
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
                if (value is Model.InvoiceHZ)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceHZ).InvoiceId);
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

        protected override void AddNew()
        {
            this.bindingSourceDepotPosition.DataSource = null;
            this.invoice = new Model.InvoiceHZ();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceHZDetail>();

            if (this.action == "insert")
            {
                Model.InvoiceHZDetail detail = new Model.InvoiceHZDetail();
                detail.InvoiceHZDetailId = Guid.NewGuid().ToString();
                detail.InvoiceHZDetailMoney = 0;
                detail.InvoiceHZDetailNote = "";
                detail.InvoiceHZDetailPrice = 0;
                detail.InvoiceHZDetailQuantity = 0;
                detail.InvoiceProductUnit = "";
                detail.Product = new Book.Model.Product();
                this.invoice.Details.Add(detail);
                this.bindingSourceInvoiceHZDetail.Position = this.bindingSourceInvoiceHZDetail.IndexOf(detail);
            }
        }

        protected override void MoveNext()
        {
            Model.InvoiceHZ invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceHZ invoice = this.invoiceManager.GetPrev(this.invoice);
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
                this.invoice = new Book.Model.InvoiceHZ();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }

            if (invoice.Depot != null)
            {
                this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select(invoice.Depot);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.buttonEditCompany.EditValue = this.invoice.Supplier;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            //this.buttonEditDepot.EditValue = this.invoice.Depot;
            this.buttonEditDepot.EditValue = this.invoice.Depot;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSourceInvoiceHZDetail.DataSource = this.invoice.Details;

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot.ShowButton = true;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditDepot.ButtonReadOnly = false;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot.ShowButton = true;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditDepot.ButtonReadOnly = false;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;

                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditCompany.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;
                    this.buttonEditDepot.ShowButton = false;

                    this.buttonEditCompany.ButtonReadOnly = true;
                    this.buttonEditEmployee.ButtonReadOnly = true;
                    this.buttonEditDepot.ButtonReadOnly = true;

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
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceHZDetail).Product;

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

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.textEditInvoiceId, this });
        }

        private bool CanAdd(IList<Model.InvoiceHZDetail> list)
        {
            foreach (Model.InvoiceHZDetail detail in list)
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
                        Model.InvoiceHZDetail detail = new Model.InvoiceHZDetail();
                        detail.InvoiceHZDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceHZDetailMoney = 0;
                        detail.InvoiceHZDetailNote = "";
                        detail.InvoiceHZDetailPrice = 0;
                        detail.InvoiceHZDetailQuantity = 0;
                        detail.InvoiceProductUnit = "";
                        detail.Product = new Book.Model.Product();
                        this.invoice.Details.Add(detail);
                        this.bindingSourceInvoiceHZDetail.Position = this.bindingSourceInvoiceHZDetail.IndexOf(detail);
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
                this.bindingSourceDepotPosition.DataSource = new BL.DepotPositionManager().Select(depot);
            }
        }
    }
}