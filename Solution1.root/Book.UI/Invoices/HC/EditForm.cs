using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.HC
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceHCDetailManager invoiceHCDetailManager = new Book.BL.InvoiceHCDetailManager();
        protected BL.InvoiceHCManager invoiceManager = new Book.BL.InvoiceHCManager();
        protected BL.InvoiceJRDetailManager invoiceJRDetailManager = new Book.BL.InvoiceJRDetailManager();
        private Model.InvoiceHC invoicehc = new Book.Model.InvoiceHC();
        private BL.ProductManager _productManager = new Book.BL.ProductManager();
        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.newChooseContorlSupper));
            this.invalidValueExceptions.Add("Details", new AA("還出數量不能為0", this.gridControl1));
            this.invalidValueExceptions.Add("HaiRuTaiDuo", new AA(Properties.Resources.HaiRuTaiDuo, this.gridControl1));
            this.invalidValueExceptions.Add(Model.InvoiceHC.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";
            this.bindingSource2.DataSource = new BL.CustomerManager().Select();
            this.newChooseContorlSupper.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            string sql = "SELECT productid,id,productname FROM product";
            this.bindingSourceProduct.DataSource = this._productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);

            this.bindingSourceDepot.DataSource = this.depotManager.Select();
            this.buttonEditEmployee.Choose = new ChooseEmployee();

            this.EmpAudit.Choose = new ChooseEmployee();
        }

        public EditForm(string invoiceId)
            : this()
        {
            this.invoicehc = this.invoiceManager.Get(invoiceId);
            if (invoicehc == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";
        }

        public EditForm(Model.InvoiceHC invoice)
            : this()
        {
            if (invoicehc == null)
                throw new ArithmeticException("invoiceid");
            this.invoicehc = invoice;
            this.action = "update";
        }

        protected override string tableCode()
        {
            return "InvoiceHC";
        }

        protected override int AuditState()
        {
            return this.Invoice.AuditState.HasValue ? this.Invoice.AuditState.Value : 0;
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
                //IList<Model.InvoiceJRDetail> jrdetails = this.invoiceJRDetailManager.Select(f.SelectedItem as Model.Company);
                //this.invoice.Jrdetails = jrdetails;
                //this.bindingSource1.DataSource = jrdetails;
            }
        }

        protected override void AddNew()
        {
            this.invoicehc = new Model.InvoiceHC();
            this.invoicehc.InvoiceDate = DateTime.Now;
            this.invoicehc.Details = new List<Model.InvoiceHCDetail>();
            this.invoicehc.Jrdetails = new List<Model.InvoiceJRDetail>();
            this.invoicehc.InvoiceId = this.invoiceManager.GetNewId();
        }

        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoicehc.InvoiceId);
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
            return this.invoiceManager.HasRowsAfter(this.invoicehc);
        }

        protected override bool HasRowsPrev()
        {
            return this.invoiceManager.HasRowsBefore(this.invoicehc);
        }

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return invoicehc;
            }
            set
            {
                if (value is Model.InvoiceHC)
                {
                    invoicehc = invoiceManager.Get((value as Model.InvoiceHC).InvoiceId);
                }
            }
        }

        protected override void MoveFirst()
        {
            this.invoicehc = this.invoiceManager.Get(this.invoiceManager.GetFirst() == null ? "" : this.invoiceManager.GetFirst().InvoiceId);
        }

        protected override void MoveLast()
        {
            this.invoicehc = this.invoiceManager.Get(this.invoiceManager.GetLast() == null ? "" : this.invoiceManager.GetLast().InvoiceId);
        }

        protected override void MoveNext()
        {
            Model.InvoiceHC invoice = this.invoiceManager.GetNext(this.invoicehc);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoicehc = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceHC invoice = this.invoiceManager.GetPrev(this.invoicehc);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoicehc = this.invoiceManager.Get(invoice.InvoiceId);

        }

        public override void Refresh()
        {
            if (this.invoicehc == null)
            {
                this.invoicehc = new Book.Model.InvoiceHC();
                this.action = "insert";
            }

            else
            {
                if (this.action != "insert")
                {
                    this.invoicehc = this.invoiceManager.Get(this.invoicehc.InvoiceId);
                    if (this.invoicehc == null)
                        this.invoicehc = new Book.Model.InvoiceHC();
                }
            }
            this.textEditInvoiceId.EditValue = this.invoicehc.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoicehc.InvoiceDate.Value;
            this.textEditNote.EditValue = this.invoicehc.InvoiceNote;
            this.lookUpEditDepot.EditValue = this.invoicehc.DepotId;
            if (this.invoicehc.DepotId != null)
                this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select(depotManager.Get(this.invoicehc.DepotId));
            //if (this.invoice.Customer != null)
            //{
            //    this.lookUpEdit1.EditValue = this.invoice.Customer.CustomerId;
            //}

            textEditjrinvoiceid.Text = this.invoicehc.JrInvoiceId;
            //this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.buttonEditEmployee.EditValue = this.invoicehc.Employee0;
            this.EmpAudit.EditValue = this.Invoice.AuditEmp;
            this.textEditAuditState.Text = this.Invoice.AuditStateName;

            switch (this.action)
            {
                case "insert":
                case "update":
                    this.bindingSource1.DataSource = this.invoicehc.Details;
                    this.barBtnChooseJR.Enabled = true;
                    break;

                case "view":
                    this.bindingSource1.DataSource = this.invoiceManager.Get(this.invoicehc.InvoiceId).Details;
                    this.barBtnChooseJR.Enabled = false;
                    break;

                default:
                    break;
            }

            base.Refresh();
            this.buttonEditEmployee.Enabled = false;
            this.lookUpEditDepot.Properties.ReadOnly = true;
            this.newChooseContorlSupper.Enabled = false;
            this.textEditjrinvoiceid.Properties.ReadOnly = true;
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoicehc.InvoiceStatus = (int)status;
            this.invoicehc.InvoiceId = this.textEditInvoiceId.Text;
            this.invoicehc.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoicehc.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            //this.invoice.Company = this.buttonEditCompany.EditValue as Model.Company;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoicehc.InvoiceNote = this.textEditNote.Text;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoicehc.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;
            if (this.lookUpEditDepot.EditValue != null)
                this.invoicehc.DepotId = this.lookUpEditDepot.EditValue.ToString();
            this.invoicehc.JrInvoiceId = this.textEditjrinvoiceid.Text;

            if (this.newChooseContorlSupper.EditValue != null)
                this.invoicehc.SupplierId = (this.newChooseContorlSupper.EditValue as Model.Supplier).SupplierId;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;
            double? quantity = 0;

            foreach (Model.InvoiceHCDetail detail in this.invoicehc.Details)
            {
                quantity += detail.InvoiceHCDetailQuantity;
            }

            this.invoicehc.InvoiceHCQuantity = quantity;

            this.Invoice.AuditState = this.saveAuditState;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.invoiceManager.Insert(this.invoicehc);
                    break;

                case "update":
                    this.invoiceManager.Update(this.invoicehc);
                    break;
            }
        }

        protected override void TurnNull()
        {
            if (this.invoicehc == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.invoiceManager.TurnNull(this.invoicehc.InvoiceId);
            this.invoicehc = this.invoiceManager.GetNext(this.invoicehc);
            if (this.invoicehc == null)
            {
                this.invoicehc = this.invoiceManager.GetLast();
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            Model.Product p = null;
            Model.DepotPosition position = null;

            if (this.action != "view")
            {
                IList<Model.InvoiceJRDetail> invoiceJcDetails = this.bindingSource1.DataSource as IList<Model.InvoiceJRDetail>;

                if (invoiceJcDetails == null || invoiceJcDetails.Count <= 0)
                    return;
                p = invoiceJcDetails[e.ListSourceRowIndex].Product;
                position = invoiceJcDetails[e.ListSourceRowIndex].DepotPosition;
            }
            else
            {
                IList<Model.InvoiceHCDetail> invoiceHrDetails = this.bindingSource1.DataSource as IList<Model.InvoiceHCDetail>;
                if (invoiceHrDetails == null || invoiceHrDetails.Count <= 0)
                    return;
                p = invoiceHrDetails[e.ListSourceRowIndex].Product;
                position = invoiceHrDetails[e.ListSourceRowIndex].DepotPosition;
            }

            switch (e.Column.Name)
            {
                //case "gridColumn2":
                //    e.DisplayText = p.Id;
                //    break;
                //case "gridColumn3":
                //    e.DisplayText = p.ProductName;
                //    break;
                case "gridColumn4":
                    e.DisplayText = p.ProductSpecification;
                    break;
                case "gridColumn5":
                    e.DisplayText = p.ProductSpecification;
                    break;
                //case "gridColumn6":
                //    e.DisplayText = p.ProduceUnit == null ? "" : p.ProduceUnit.CnName;
                //    break;

                //case "gridColumn10":
                //    e.DisplayText = position == null ? "" : position.ToString();
                //    break;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumn6")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    //Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceHCDetail).Product;

                    //this.repositoryItemComboBox1.Items.Clear();

                    //if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    //{
                    //    BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                    //    IList<Model.ProductUnit> unitList = new BL.ProductUnitManager().Select(p.BasedUnitGroup);
                    //    foreach (Model.ProductUnit item in unitList)
                    //    {
                    //        this.repositoryItemComboBox1.Items.Add(item.CnName);
                    //    }
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

        public static IList<Model.InvoiceJRDetail> details = new List<Model.InvoiceJRDetail>();

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.invoicehc.Details.Clear();
            ChooseInvoiceJrForm chooseform = new ChooseInvoiceJrForm();
            if (chooseform.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Model.InvoiceJRDetail item in details)
                {
                    Model.InvoiceHCDetail tem = new Book.Model.InvoiceHCDetail();
                    tem.InvoiceHCDetailId = Guid.NewGuid().ToString();
                    tem.InvoiceJRDetailId = item.InvoiceJRDetailId;
                    tem.InvoiceHCDetailQuantity = item.InvoiceYiHuaiChuQuantity;
                    this.invoicehc.Details.Add(tem);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumn2)
            {
                Model.InvoiceHCDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceHCDetail;
                if (detail != null)
                {
                    Model.Product p = this._productManager.Get(e.Value.ToString());
                    //detail.DepotInDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceHCDetailQuantity = 1;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.InvoiceProductUnit = p.DepotUnit.CnName;
                    //this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void lookUpEditDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditDepot.EditValue != null)
                this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select(depotManager.Get(this.lookUpEditDepot.EditValue.ToString()));
        }

        //选择借入
        private void barBtnChooseJR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseInvoiceJrForm chooseform = new ChooseInvoiceJrForm();
            if (chooseform.ShowDialog(this) == DialogResult.OK)
            {
                foreach (Model.InvoiceJRDetail item in details)
                {
                    this.newChooseContorlSupper.EditValue = item.Invoice.Supplier;
                    this.buttonEditEmployee.EditValue = item.Invoice.Employee0;
                    this.lookUpEditDepot.EditValue = item.Invoice.DepotId;
                    this.textEditjrinvoiceid.Text = item.InvoiceId;
                }
                this.invoicehc.Details.Clear();
                this.invoicehc.Jrdetails = new List<Model.InvoiceJRDetail>();
                foreach (Model.InvoiceJRDetail detail in details)
                {
                    Model.InvoiceHCDetail hcdetail = new Book.Model.InvoiceHCDetail();
                    hcdetail.InvoiceId = detail.InvoiceId;
                    hcdetail.InvoiceHCDetailId = Guid.NewGuid().ToString();
                    hcdetail.InvoiceHCDetailNote = detail.InvoiceHCDetailNote;
                    hcdetail.InvoiceHCDetailQuantity = detail.InvoiceHCDetailQuantity;
                    hcdetail.InvoiceJRDetailId = detail.InvoiceJRDetailId;
                    hcdetail.InvoiceProductUnit = detail.InvoiceProductUnit;
                    hcdetail.DepotPositionId = detail.DepotPositionId;
                    hcdetail.InvoiceHCDetailQuantity = detail.InvoiceWeiHuaiChuQuantity;
                    hcdetail.ProductId = detail.ProductId;
                    this.invoicehc.Details.Add(hcdetail);
                }
                this.bindingSource1.DataSource = this.invoicehc.Details;
                this.gridControl1.RefreshDataSource();
            }
        }
    }
}