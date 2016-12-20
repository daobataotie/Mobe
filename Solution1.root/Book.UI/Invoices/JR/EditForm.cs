using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.JR
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceJRDetailManager invoiceJRDetailManager = new Book.BL.InvoiceJRDetailManager();
        protected BL.InvoiceJRManager invoiceManager = new Book.BL.InvoiceJRManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private Model.InvoiceJR invoice = null;
        protected BL.ProductUnitManager productunitManager = new Book.BL.ProductUnitManager();
        public EditForm()
        {
            InitializeComponent();


            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.NewChooserControlSupper));
            this.invalidValueExceptions.Add(Model.InvoiceJRDetail.PRO_InvoiceJRDetailQuantity, new AA("借入數量不能為0", this.gridControl1));
            this.invalidValueExceptions.Add(Model.InvoiceJR.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";

            // this.buttonEditCompany.Choose = new ChooseCompany(global::Helper.CompanyKind.Supplier);
            this.NewChooserControlSupper.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.bindingSource4.DataSource = new BL.DepotManager().Select();
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

        public EditForm(Model.InvoiceJR invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoice;
            this.action = "update";
        }


        protected override string tableCode()
        {
            return "InvoiceJR";
        }

        protected override int AuditState()
        {
            return this.invoice.AuditState.HasValue ? this.invoice.AuditState.Value : 0;
        }

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this.invoice.Details.Count > 0 && string.IsNullOrEmpty(this.invoice.Details[0].ProductId))
                    this.invoice.Details.RemoveAt(0);
                Model.InvoiceJRDetail detail = null;

                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.InvoiceJRDetail();
                        detail.Invoice = this.invoice;

                        detail.InvoiceJRDetailId = Guid.NewGuid().ToString();
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.InvoiceJRDetailQuantity = 0;
                        detail.InvoiceWeiHuaiChuQuantity = 0;
                        detail.InvoiceYiHuaiChuQuantity = 0;
                        detail.InvoiceJRDetailNote = "";
                        if (detail.Product.DepotUnit != null)
                            detail.InvoiceProductUnit = detail.Product.DepotUnit.ToString();
                        this.invoice.Details.Add(detail);
                    }
                }

                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.InvoiceJRDetail();
                    detail.InvoiceJRDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = this.invoice;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                    detail.InvoiceJRDetailQuantity = 0;
                    detail.InvoiceWeiHuaiChuQuantity = 0;
                    detail.InvoiceYiHuaiChuQuantity = 0;
                    detail.InvoiceJRDetailNote = "";
                    this.invoice.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);


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
            ChooseSupplier f = new ChooseSupplier();
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
            this.invoice = new Model.InvoiceJR();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceJRDetail>();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();

            Model.InvoiceJRDetail detail = new Model.InvoiceJRDetail();
            detail.InvoiceJRDetailId = Guid.NewGuid().ToString();
            detail.InvoiceHCDetailNote = "";
            detail.InvoiceHCDetailQuantity = 0;
            detail.InvoiceJRDetailNote = "";
            detail.InvoiceJRDetailQuantity = 0;
            detail.InvoiceProductUnit = "";
            detail.InvoiceWeiHuaiChuQuantity = 0;
            detail.InvoiceYiHuaiChuQuantity = 0;
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
                if (value is Model.InvoiceJR)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceJR).InvoiceId);
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
            Model.InvoiceJR invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceJR invoice = this.invoiceManager.GetPrev(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);

        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceJR();
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

            this.NewChooserControlSupper.EditValue = this.invoice.Supplier;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditDepot.EditValue = this.invoice.DepotId;
            this.NewChooserControlSupper.EditValue = this.invoice.Supplier;
            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSource1.DataSource = this.invoice.Details;

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;

                    //this.textEditAbstract.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.NewChooserControlSupper.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot.Properties.ReadOnly = false;

                    this.NewChooserControlSupper.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.NewChooserControlSupper.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot.Properties.ReadOnly = false;

                    this.NewChooserControlSupper.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;

                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditNote.Properties.ReadOnly = true;

                    this.NewChooserControlSupper.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;
                    this.buttonEditDepot.Properties.ReadOnly = true;

                    this.NewChooserControlSupper.ButtonReadOnly = true;
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
            //this.invoice.Company = this.buttonEditCompany.EditValue as Model.Company;
            //this.invoice.Depot = this.buttonEditDepot.EditValue as Model.Depot;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;            
            this.invoice.DepotId = this.buttonEditDepot.EditValue.ToString();
            if (this.NewChooserControlSupper.EditValue != null)
            {
                this.invoice.Supplier = this.NewChooserControlSupper.EditValue as Model.Supplier;
                this.invoice.SupplierId = this.invoice.Supplier.SupplierId;
            }

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            double? quantity = 0;

            foreach (Model.InvoiceJRDetail detail in this.invoice.Details)
            {
                quantity += detail.InvoiceJRDetailQuantity;
            }

            this.invoice.InvoiceJRQuantity = quantity;

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
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoiceJRDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceJRDetail detail = new Model.InvoiceJRDetail();
                    detail.InvoiceJRDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceHCDetailNote = "";
                    detail.InvoiceHCDetailQuantity = 0;
                    detail.InvoiceJRDetailNote = "";
                    detail.InvoiceJRDetailQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    detail.InvoiceWeiHuaiChuQuantity = 0;
                    detail.InvoiceYiHuaiChuQuantity = 0;
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
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceJRDetail).Product;

                    this.repositoryItemComboBox1.Items.Clear();

                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                        IList<Model.ProductUnit> unitList = this.productunitManager.Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit item in unitList)
                        {
                            this.repositoryItemComboBox1.Items.Add(item.CnName);
                        }
                    }
                }
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;

            IList<Model.InvoiceJRDetail> invoiceCgDetails = this.bindingSource1.DataSource as IList<Model.InvoiceJRDetail>;

            if (invoiceCgDetails == null || invoiceCgDetails.Count <= 0)
                return;

            switch (e.Column.Name)
            {
                //case "gridColumn3":
                //    e.DisplayText = invoiceCgDetails[e.ListSourceRowIndex].Product.ProduceUnit == null ? "" : invoiceCgDetails[e.ListSourceRowIndex].Product.ProduceUnit.CnName;
                //    break;
                //case "gridColumn4":
                //    e.DisplayText = invoiceCgDetails[e.ListSourceRowIndex].Product.ProductSpecification;
                //    break;
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
            string sql = "SELECT productid,id,productname FROM product WHERE IsCustomerProduct IS NULL OR IsCustomerProduct =0";
            this.bindingSource2.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);

        }

        private bool CanAdd(IList<Model.InvoiceJRDetail> list)
        {

            foreach (Model.InvoiceJRDetail detail in list)
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
                        Model.InvoiceJRDetail detail = new Model.InvoiceJRDetail();
                        detail.InvoiceJRDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceHCDetailNote = "";
                        detail.InvoiceHCDetailQuantity = 0;
                        detail.InvoiceJRDetailNote = "";
                        detail.InvoiceJRDetailQuantity = 0;
                        detail.InvoiceProductUnit = "";
                        detail.InvoiceWeiHuaiChuQuantity = 0;
                        detail.InvoiceYiHuaiChuQuantity = 0;
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
            if (e.Column == this.colProductId)
            {
                Model.InvoiceJRDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceJRDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.InvoiceJRDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceHCDetailNote = "";
                    detail.InvoiceHCDetailQuantity = 0;
                    detail.InvoiceJRDetailNote = "";
                    detail.InvoiceJRDetailQuantity = 0;
                    detail.InvoiceWeiHuaiChuQuantity = 0;
                    detail.InvoiceYiHuaiChuQuantity = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.InvoiceProductUnit = p.ProduceUnit.CnName;
                    //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
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