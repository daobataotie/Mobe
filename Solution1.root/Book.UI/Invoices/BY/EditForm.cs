using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.BY
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 報溢單的單據輸入
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-05-07
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm
    {
        protected BL.InvoiceBYManager invoiceManager = new Book.BL.InvoiceBYManager();
        protected BL.InvoiceBYDetailManager invoiceBYDetailManager = new Book.BL.InvoiceBYDetailManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        protected BL.DepotManager depotManager = new Book.BL.DepotManager();
        private Model.InvoiceBY invoice;

        public EditForm()
        {
            InitializeComponent();


            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));

            this.invalidValueExceptions.Add(Model.InvoiceBY.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.buttonEditDepot.Choose = new ChooseDepot();
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

        public EditForm(Model.InvoiceBY initInvoiceBY)
            : this()
        {
            if (initInvoiceBY == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = initInvoiceBY;
            this.action = "update";
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSource2.DataSource = this.productManager.Select();
        }

        protected override string tableCode()
        {
            return "InvoiceBY";
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

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            if (this.buttonEditDepot.EditValue != null)
            {
                this.invoice.Depot = this.buttonEditDepot.EditValue as Model.Depot;
                this.invoice.DepotId = this.invoice.Depot.DepotId;
            }
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
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

        #region Choose Object
        private void buttonEditDepot_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseDepotForm f = new ChooseDepotForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
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

        #region simpleButton_Click

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //报损项
                Model.InvoiceBYDetail detail = new Book.Model.InvoiceBYDetail();
                detail.InvoiceBYDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.InvoiceBYDetailNote = "";
                detail.InvoiceBYDetailQuantity = 0;
                //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                this.invoice.Details.Add(detail);
                this.gridControl1.RefreshDataSource();

                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);

                this.bindingSource2.DataSource = this.productManager.Select();
            }
        }
        private bool CanAdd(IList<Model.InvoiceBYDetail> list)
        {
            foreach (Model.InvoiceBYDetail detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }
        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoiceBYDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceBYDetail detail = new Model.InvoiceBYDetail();
                    detail.InvoiceBYDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceBYDetailNote = "";
                    detail.InvoiceBYDetailQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.invoice.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        #endregion

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
                if (value is Model.InvoiceBY)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceBY).InvoiceId);
                }
            }
        }

        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoice.InvoiceId);
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
            this.bindingSource3.DataSource = null;
            this.invoice = new Model.InvoiceBY();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceBYDetail>();

            if (this.action == "insert")
            {
                Model.InvoiceBYDetail detail = new Model.InvoiceBYDetail();
                detail.InvoiceBYDetailId = Guid.NewGuid().ToString();
                detail.InvoiceBYDetailNote = "";
                detail.InvoiceBYDetailQuantity = 0;
                detail.InvoiceProductUnit = "";
                detail.Product = new Book.Model.Product();
                this.invoice.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        protected override void MoveNext()
        {
            Model.InvoiceBY invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceBY invoice = this.invoiceManager.GetPrev(this.invoice);
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
                this.invoice = new Book.Model.InvoiceBY();
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

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditDepot.EditValue = this.invoice.Depot;
            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSource1.DataSource = this.invoice.Details;

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;

                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditDepot.ShowButton = true;
                    this.buttonEditDepot.ButtonReadOnly = false;

                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    break;

                case "update":

                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditDepot.ShowButton = true;
                    this.buttonEditDepot.ButtonReadOnly = false;

                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    break;

                case "view":

                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditDepot.ShowButton = false;
                    this.buttonEditDepot.ButtonReadOnly = true;

                    this.buttonEditEmployee.ShowButton = false;
                    this.buttonEditEmployee.ButtonReadOnly = true;

                    this.gridView1.OptionsBehavior.Editable = false;

                    this.simpleButtonAppend.Enabled = false;
                    this.simpleButtonRemove.Enabled = false;
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
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceBYDetail).Product;

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
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this });
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.CanAdd(this.invoice.Details))
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        Model.InvoiceBYDetail detail = new Model.InvoiceBYDetail();
                        detail.InvoiceBYDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceBYDetailNote = "";
                        detail.InvoiceBYDetailQuantity = 0;
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

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.InvoiceBYDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceBYDetail;
            if (e.Column == this.colProductId)
            {

                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.InvoiceBYDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = this.invoice;
                    detail.InvoiceBYDetailNote = "";
                    detail.InvoiceBYDetailQuantity = 1;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            if (e.Column == this.gridColumnPositionId)
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
                Model.Depot depot = this.buttonEditDepot.EditValue as Model.Depot;
                this.bindingSource3.DataSource = new BL.DepotPositionManager().Select(depot);
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceBYDetail> details = this.bindingSource1.DataSource as IList<Model.InvoiceBYDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "colProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;


            }
        }
    }
}