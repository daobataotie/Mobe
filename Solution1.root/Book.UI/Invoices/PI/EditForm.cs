using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.PI
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoicePIDetailManager invoicePIDetailManager = new Book.BL.InvoicePIDetailManager();
        protected BL.InvoicePIManager invoiceManager = new Book.BL.InvoicePIManager();
        protected BL.InvoicePODetailManager invoicePODetailManager = new Book.BL.InvoicePODetailManager();
        private Model.InvoicePI invoice = null;
        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotpositionManager = new Book.BL.DepotPositionManager();
        private BL.ProductManager productManager = new Book.BL.ProductManager();
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.lookUpEditDepartment));

            this.invalidValueExceptions.Add(Model.InvoicePI.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";

            //this.buttonEditDepartment.Choose = new ChooseDepartment();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.EmpAudit.Choose = new ChooseEmployee();
            this.bindingSourceDepartment.DataSource = new BL.DepartmentManager().Select();
            this.bindingSourceDepot.DataSource = depotManager.Select();
            this.bindingSourceProduct.DataSource = productManager.GetProduct();

        }

        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";
        }

        public EditForm(Model.InvoicePI invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoice;
            this.action = "update";
        }

        protected override string tableCode()
        {
            return "InvoicePI";
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

        private void buttonEditEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        private void buttonEditDepartment_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseDepartmentForm f = new ChooseDepartmentForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
                IList<Model.InvoicePODetail> podetails = this.invoicePODetailManager.Select(f.SelectedItem as Model.Department);
                this.invoice.Podetails = podetails;
                this.bindingSourcePODetail.DataSource = podetails;
            }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoicePI();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoicePIDetail>();
            this.invoice.Podetails = new List<Model.InvoicePODetail>();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
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
                if (value is Model.InvoicePI)
                {
                    invoice = invoiceManager.Get((value as Model.InvoicePI).InvoiceId);
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
            Model.InvoicePI invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoicePI invoice = this.invoiceManager.GetPrev(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);

        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoicePI();
                this.invoice.Details = new List<Model.InvoicePIDetail>();
                this.action = "insert";
            }
            else
            {
                if (this.action != "insert")
                {
                    this.invoice = this.invoiceManager.Get(this.invoice.InvoiceId);
                    if (this.invoice == null)
                    {
                        this.invoice = new Book.Model.InvoicePI();
                        this.invoice.Details = new List<Model.InvoicePIDetail>();
                    }
                }
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.lookUpEditDepartment.EditValue = this.invoice.DepartmentId;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.lookUpEditDepot.EditValue = this.invoice.PoDepotId;
            this.textEditInvoicePoId.Text = this.invoice.poInvoiceId;
            if (this.invoice.PoDepotId != null)
                this.bindingSourcePosition.DataSource = this.depotpositionManager.Select(this.depotManager.Get(this.invoice.PoDepotId));

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSourcePODetail.DataSource = this.invoice.Details;

            this.gridControl1.RefreshDataSource();

            switch (this.action)
            {
                case "insert":
                case "update":
                    this.barButtonItem1.Enabled = true;
                    break;
                case "view":
                    this.barButtonItem1.Enabled = false;
                    break;

                default:
                    break;
            }
            base.Refresh();
            this.buttonEditEmployee.Enabled = false;
            this.lookUpEditDepartment.Properties.ReadOnly = true;
            this.lookUpEditDepot.Properties.ReadOnly = true;
            this.textEditInvoicePoId.Properties.ReadOnly = true;
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            if (this.lookUpEditDepartment.EditValue != null)
            {
                if (invoice != null)
                {
                    this.invoice.Department = new BL.DepartmentManager().Get(this.lookUpEditDepartment.EditValue.ToString());
                }
                this.invoice.DepartmentId = this.lookUpEditDepartment.EditValue.ToString();
            }
            this.invoice.InvoiceNote = this.textEditNote.Text;
            this.invoice.InvoiceLRTime = DateTime.Now;
            this.invoice.poInvoiceId = this.textEditInvoicePoId.Text;
            this.invoice.PoDepotId = this.lookUpEditDepot.EditValue == null ? "" : this.lookUpEditDepot.EditValue.ToString();

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            double? quantity = 0;

            foreach (Model.InvoicePIDetail detail in this.invoice.Details)
            {
                quantity += detail.InvoicePIDetailQuantity;
            }

            this.invoice.InvoicePIQuantity = quantity;

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

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            Model.Product p = null;

            if (this.action != "view")
            {
                IList<Model.InvoicePODetail> invoicePODetails = this.bindingSourcePODetail.DataSource as IList<Model.InvoicePODetail>;

                if (invoicePODetails == null || invoicePODetails.Count <= 0)
                    return;
                p = invoicePODetails[e.ListSourceRowIndex].Product;
            }
            else
            {
                IList<Model.InvoicePIDetail> invoicePIDetails = this.bindingSourcePODetail.DataSource as IList<Model.InvoicePIDetail>;
                if (invoicePIDetails == null || invoicePIDetails.Count <= 0)
                    return;
                p = invoicePIDetails[e.ListSourceRowIndex].Product;
            }

            switch (e.Column.Name)
            {
                case "gridColumn5":
                    e.DisplayText = p.ProductSpecification;
                    break;
                case "gridColumn10":
                    e.DisplayText = p.CustomerProductName;
                    break;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumn6")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoicePODetail).Product;

                    this.repositoryItemComboBox1.Items.Clear();

                    //if (!string.IsNullOrEmpty(p.ProductBaseUnit))
                    //{
                    //    this.repositoryItemComboBox1.Items Add(p.ProductBaseUnit);
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

        private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        {
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.textEditInvoiceId, this });
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.lookUpEdit1.EditValue != null)
            //{
            //IList<Model.InvoicePODetail> podetails = this.invoicePODetailManager.Select(new BL.DepartmentManager().Get(this.lookUpEdit1.EditValue.ToString()));
            //this.invoice.Podetails = podetails;
            //this.bindingSourcePODetail.DataSource = podetails;
            //}
        }

        public static Model.InvoicePO invoicepo = new Book.Model.InvoicePO();

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseInvoicePOForm form = new ChooseInvoicePOForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                if (invoicepo == null) return;
                this.lookUpEditDepot.EditValue = invoicepo.PoDepotId;
                this.textEditInvoicePoId.Text = invoicepo.InvoiceId;
                this.buttonEditEmployee.EditValue = invoicepo.Employee0;
                this.lookUpEditDepartment.EditValue = invoicepo.DepartmentId;
                if (invoicepo.PoDepotId != null)
                    this.bindingSourcePosition.DataSource = depotpositionManager.Select(invoicepo.PoDepot);
                this.invoice.Details = new List<Model.InvoicePIDetail>();

                foreach (Model.InvoicePODetail item in invoicepo.Details)
                {
                    Model.InvoicePIDetail temp = new Book.Model.InvoicePIDetail();
                    temp.InvoicePIDetailId = Guid.NewGuid().ToString();
                    temp.InvoiceId = item.InvoiceId;
                    temp.InvoicePODetailId = item.InvoicePODetailId;
                    temp.InvoicePODetailId = item.InvoicePODetailId;
                    temp.Product = item.Product;
                    temp.InvoiceProductUnit = item.InvoiceProductUnit;
                    temp.ProductId = item.ProductId;
                    temp.PoDepotPositionId = item.DepotPositionId;
                    temp.InvoicePIDetailQuantity = item.InvoicePODetailWHQuantity;
                    this.invoice.Details.Add(temp);
                }

                this.bindingSourcePODetail.DataSource = this.invoice.Details;
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}