using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.HR
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceHRDetailManager invoiceHRDetailManager = new Book.BL.InvoiceHRDetailManager();
        protected BL.InvoiceHRManager invoiceManager = new Book.BL.InvoiceHRManager();
        protected BL.InvoiceJCDetailManager invoiceJCDetailManager = new Book.BL.InvoiceJCDetailManager();
        private Model.InvoiceHR invoice = null;
        private BL.DepotManager _depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager positionManager = new Book.BL.DepotPositionManager();
        private BL.ProductManager productManager = new Book.BL.ProductManager();
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.lookUpEditCustomer));
            this.invalidValueExceptions.Add("HaiRuTaiDuo", new AA(Properties.Resources.HaiRuTaiDuo, this.gridControl1));

            this.invalidValueExceptions.Add(Model.InvoiceHR.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";
            // this.buttonEditCompany.Choose = new ChooseCompany(global::Helper.CompanyKind.Customer);
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.bindingSourceCustomer.DataSource = new BL.CustomerManager().Select();
            this.bindingSourceDepot.DataSource = this._depotManager.Select();
            this.bindingSourceProduct.DataSource = productManager.GetProduct();
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

        public EditForm(Model.InvoiceHR invoice)
            : this()
        {
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoice;
            this.action = "update";
        }

        protected override string tableCode()
        {
            return "InvoiceHR";
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

        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseCustoms f = new ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
                //IList<Model.InvoiceJCDetail> jcdetails = this.invoiceJCDetailManager.Select(f.SelectedItem as Model.Company);                
                //this.invoice.Jcdetails = jcdetails;
                //this.bindingSource1.DataSource = jcdetails;
            }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceHR();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceHRDetail>();
            //this.invoice.Jcdetails = new List<Model.InvoiceJCDetail>();
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
                if (value is Model.InvoiceHR)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceHR).InvoiceId);
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
            Model.InvoiceHR invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceHR invoice = this.invoiceManager.GetPrev(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);

        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceHR();
                this.action = "insert";
            }
            else
            {
                if (this.action != "insert")
                {
                    this.invoice = this.invoiceManager.Get(this.invoice.InvoiceId);
                    if (this.invoice == null)
                    {
                        this.invoice = new Book.Model.InvoiceHR();
                        this.invoice.Details = new List<Model.InvoiceHRDetail>();
                        this.action = "insert";
                    }
                }
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            if (this.invoice.Customer != null)
            {
                this.lookUpEditCustomer.EditValue = this.invoice.Customer.CustomerId;
            }
            //this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.lookUpEdit_Depot.EditValue = this.invoice.DepotId;
            if (this.invoice.DepotId != null)
                this.bindingSourceDepotPosition.DataSource = this.positionManager.Select(_depotManager.Get(invoice.DepotId));

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSourceInvoiceJCDetail.DataSource = this.invoice.Details;
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
            }

            base.Refresh();
            this.buttonEditEmployee.Enabled = false;
            this.buttonEditEmployee.ButtonReadOnly = true;
            this.lookUpEdit_Depot.Properties.ReadOnly = true;
            this.lookUpEditCustomer.Properties.ReadOnly = true;
            this.textEditFromInvoice.Properties.ReadOnly = true;
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            if (this.invoice != null)
            {
                if (this.lookUpEditCustomer.EditValue != null)
                {
                    this.invoice.Customer = new BL.CustomerManager().Get(this.lookUpEditCustomer.EditValue.ToString());
                }
            }
            //this.invoice.Company = this.buttonEditCompany.EditValue as Model.Company;            
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;
            this.invoice.DepotId = this.lookUpEdit_Depot.EditValue == null ? "" : this.lookUpEdit_Depot.EditValue.ToString();
            this.invoice.JcInvoiceId = this.textEditFromInvoice.Text;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            double? quantity = 0;

            foreach (Model.InvoiceHRDetail detail in this.invoice.Details)
            {
                quantity += detail.InvoiceHRDetailQuantity;
            }

            this.invoice.InvoiceHRQuantity = quantity;

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


            //if (this.action != "view")
            //{
            //    IList<Model.InvoiceJCDetail> invoiceJcDetails = this.bindingSourceInvoiceJCDetail.DataSource as IList<Model.InvoiceJCDetail>;

            //    if (invoiceJcDetails == null || invoiceJcDetails.Count <= 0)
            //        return;
            //    p = invoiceJcDetails[e.ListSourceRowIndex].Product;
            //}
            //else
            //{
            IList<Model.InvoiceHRDetail> invoiceHrDetails = this.bindingSourceInvoiceJCDetail.DataSource as IList<Model.InvoiceHRDetail>;
            if (invoiceHrDetails == null || invoiceHrDetails.Count <= 0)
                return;
            p = invoiceHrDetails[e.ListSourceRowIndex].Product;
            //}

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
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceJCDetail).Product;

                    this.repositoryItemComboBox1.Items.Clear();

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
            if (this.lookUpEditCustomer.EditValue != null)
            {
                //IList<Model.InvoiceJCDetail> jcdetails = this.invoiceJCDetailManager.Select(new BL.CustomerManager().Get(this.lookUpEdit1.EditValue.ToString()));
                //this.invoice.Jcdetails = jcdetails;
                //this.bindingSource1.DataSource = jcdetails;
            }
        }


        public static Model.InvoiceJC invoicejc = new Book.Model.InvoiceJC();
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseInvoiceJcDetaliForm form = new ChooseInvoiceJcDetaliForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this.invoice.JcInvoiceId = invoicejc.InvoiceId;
                this.lookUpEdit_Depot.EditValue = invoicejc.DepotId;
                if (invoicejc.DepotId != null)
                    this.bindingSourceDepotPosition.DataSource = this.positionManager.Select(_depotManager.Get(invoicejc.DepotId));
                this.lookUpEditCustomer.EditValue = invoicejc.CustomerId;
                this.textEditFromInvoice.Text = invoicejc.InvoiceId;
                this.buttonEditEmployee.EditValue = invoicejc.Employee0;
                //this.invoice.Jcdetails = invoicejc.Details;
                this.invoice.Details = new List<Model.InvoiceHRDetail>();
                foreach (Model.InvoiceJCDetail item in invoicejc.Details)
                {
                    Model.InvoiceHRDetail temp = new Book.Model.InvoiceHRDetail();
                    temp.InvoiceHRDetailId = Guid.NewGuid().ToString();
                    temp.InvoiceJCDetailId = item.InvoiceJCDetailId;
                    temp.InvoiceProductUnit = item.InvoiceProductUnit;
                    temp.DepotPositionId = item.DepotPositionId;
                    temp.Product = item.Product;
                    temp.ProductId = item.ProductId;
                    temp.InvoiceHRDetailQuantity = item.InvoiceWeiHuaiRuQuantity;
                    temp.InvoiceId = invoicejc.InvoiceId;
                    this.invoice.Details.Add(temp);
                }

                this.bindingSourceInvoiceJCDetail.DataSource = this.invoice.Details;
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}