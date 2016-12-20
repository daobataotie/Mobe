using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;

namespace Book.UI.Invoices.PO
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoicePODetailManager invoiceJRDetailManager = new Book.BL.InvoicePODetailManager();
        protected BL.InvoicePOManager invoiceManager = new Book.BL.InvoicePOManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();

        private Model.InvoicePO invoice = null;
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepartment));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));

            this.invalidValueExceptions.Add(Model.InvoicePO.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";
            this.buttonEditDepartment.Choose = new ChooseDepartment();
            this.bindingSource4.DataSource = new BL.DepotManager().Select();
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

        public EditForm(Model.InvoicePO invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoice;
            this.action = "update";
        }

        protected override string tableCode()
        {
            return "InvoicePO";
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
                if (this.invoice.Details.Count > 0 && string.IsNullOrEmpty(this.invoice.Details[0].ProductId))
                    this.invoice.Details.RemoveAt(0);
                Model.InvoicePODetail detail = null;

                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.InvoicePODetail();
                        detail.Invoice = this.invoice;
                        detail.InvoicePODetailId = Guid.NewGuid().ToString();
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.InvoicePODetailJCQuantity = 0;
                        detail.InvoicePODetailWHQuantity = 0;
                        detail.InvoicePODetailYHQuantity = 0;
                        detail.InvoicePODetailNote = "";
                        if (detail.Product.DepotUnit != null)
                            detail.InvoiceProductUnit = detail.Product.DepotUnit.ToString();
                        this.invoice.Details.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.InvoicePODetail();
                    detail.InvoicePODetailId = Guid.NewGuid().ToString();
                    detail.Product = f.SelectedItem as Model.Product; ;
                    detail.ProductId = detail.Product.ProductId;
                    detail.InvoicePODetailJCQuantity = 0;
                    detail.InvoicePODetailWHQuantity = 0;
                    detail.InvoicePODetailYHQuantity = 0;
                    detail.InvoicePODetailNote = "";
                    if (detail.Product.DepotUnit != null)
                        detail.InvoiceProductUnit = detail.Product.DepotUnit.ToString();
                    this.invoice.Details.Add(detail);
                }
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
            f.Dispose();
            GC.Collect();
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

        private void buttonEditDepot_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseDepartmentForm f = new ChooseDepartmentForm();
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
            this.invoice = new Model.InvoicePO();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoicePODetail>();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();

            Model.InvoicePODetail detail = new Model.InvoicePODetail();
            detail.InvoicePODetailId = Guid.NewGuid().ToString();
            detail.InvoicePIDetailNote = "";
            detail.InvoicePIDetailQuantity = 0;
            detail.InvoicePODetailNote = "";
            detail.InvoicePODetailJCQuantity = 0;
            detail.InvoiceProductUnit = "";
            detail.InvoicePODetailWHQuantity = 0;
            detail.InvoicePODetailYHQuantity = 0;
            detail.Product = new Book.Model.Product();
            this.invoice.Details.Add(detail);
            this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
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
                if (value is Model.InvoicePO)
                {
                    invoice = invoiceManager.Get((value as Model.InvoicePO).InvoiceId);
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
            Model.InvoicePO invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoicePO invoice = this.invoiceManager.GetPrev(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);

        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoicePO();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            //this.buttonEditDepot.EditValue = this.invoice.Depot;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditDepartment.EditValue = this.invoice.Department;


            this.bindingSource1.DataSource = this.invoice.Details;

            this.EmpAudit.EditValue = this.invoice.AuditEmp;

            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.gridControl1.RefreshDataSource();

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditDepot.Properties.ReadOnly = false;
                    this.buttonEditDepartment.ButtonReadOnly = false;

                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepartment.ShowButton = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditDepot.Properties.ReadOnly = false;
                    this.buttonEditDepartment.ButtonReadOnly = false;

                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepartment.ShowButton = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;

                    this.gridView1.OptionsBehavior.Editable = true;

                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditEmployee.ButtonReadOnly = true;
                    this.buttonEditDepot.Properties.ReadOnly = true;
                    this.buttonEditDepartment.ButtonReadOnly = true;

                    this.buttonEditEmployee.ShowButton = false;
                    this.buttonEditDepartment.ShowButton = false;

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
            if (this.buttonEditDepot.EditValue != null)
                this.invoice.PoDepotId = this.buttonEditDepot.EditValue.ToString();
            this.invoice.Department = this.buttonEditDepartment.EditValue as Model.Department;
            if (this.invoice.Department != null)
            {
                this.invoice.DepartmentId = this.invoice.Department.DepartmentId;
            }
            this.invoice.InvoiceNote = this.textEditNote.Text;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;


            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            double? quantity = 0;

            foreach (Model.InvoicePODetail detail in this.invoice.Details)
            {
                quantity += detail.InvoicePODetailJCQuantity;
            }

            this.invoice.InvoicePOQuantity = quantity;

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
            if (this.bindingSource1.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoicePODetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoicePODetail detail = new Model.InvoicePODetail();
                    detail.InvoicePODetailId = Guid.NewGuid().ToString();
                    detail.InvoicePIDetailNote = "";
                    detail.InvoicePIDetailQuantity = 0;
                    detail.InvoicePODetailNote = "";
                    detail.InvoicePODetailJCQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    detail.InvoicePODetailWHQuantity = 0;
                    detail.InvoicePODetailYHQuantity = 0;
                    detail.Product = new Book.Model.Product();
                    this.invoice.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
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
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoicePODetail).Product;

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

            IList<Model.InvoicePODetail> invoiceCgDetails = this.bindingSource1.DataSource as IList<Model.InvoicePODetail>;

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

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSource2.DataSource = this.productManager.Select();
        }

        private bool CanAdd(IList<Model.InvoicePODetail> list)
        {

            foreach (Model.InvoicePODetail detail in list)
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
                        Model.InvoicePODetail detail = new Model.InvoicePODetail();
                        detail.InvoicePODetailId = Guid.NewGuid().ToString();
                        detail.InvoicePIDetailNote = "";
                        detail.InvoicePIDetailQuantity = 0;
                        detail.InvoicePODetailNote = "";
                        detail.InvoicePODetailJCQuantity = 0;
                        detail.InvoiceProductUnit = "";
                        detail.InvoicePODetailWHQuantity = 0;
                        detail.InvoicePODetailYHQuantity = 0;
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

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.InvoicePODetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoicePODetail;
            if (e.Column == this.colProductId)
            {
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.InvoicePODetailId = Guid.NewGuid().ToString();
                    detail.InvoicePIDetailNote = "";
                    detail.InvoicePIDetailQuantity = 0;
                    detail.InvoicePODetailNote = "";
                    detail.InvoicePODetailJCQuantity = 0;
                    detail.InvoicePODetailWHQuantity = 0;
                    detail.InvoicePODetailYHQuantity = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.InvoiceProductUnit = detail.Product.ProduceUnit.CnName; ;
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            if (e.Column == this.gridColumn1)
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

        private void buttonEditDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.buttonEditDepot.EditValue != null)
            {
                string depot = this.buttonEditDepot.EditValue.ToString();
                this.bindingSource3.DataSource = new BL.DepotPositionManager().Select(depot);
            }
        }
    }
}