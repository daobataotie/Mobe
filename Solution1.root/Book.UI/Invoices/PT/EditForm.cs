using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;

namespace Book.UI.Invoices.PT
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoicePTManager invoiceManager = new Book.BL.InvoicePTManager();
        protected BL.InvoicePTDetailManager invoicePTDetailManager = new Book.BL.InvoicePTDetailManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        /// <summary>
        /// 被编辑的单据
        /// </summary>
        protected Book.Model.InvoicePT invoice = null;

        #region Constructors

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot0", new AA(Properties.Resources.RequiredDataOfDepot, this.newChooseDepot1));
            this.requireValueExceptions.Add("Depot1", new AA(Properties.Resources.RequiredDataOfDepot, this.newChooseDepot2));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));

            this.invalidValueExceptions.Add(Model.InvoicePT.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.invalidValueExceptions.Add(Model.InvoicePTDetail.PROPERTY_DEPOTPOSITIONID, new AA("調撥數量不能大於當前貨位數量！", this.gridControl1));
            this.newChooseDepot1.Choose = new ChooseDepot();
            this.newChooseDepot2.Choose = new ChooseDepot();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.EmpAudit.Choose = new ChooseEmployee();
            this.action = "view";


        }
        int LastFlag = 0;
        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.InvoicePT initInvoicePt)
            : this()
        {
            if (initInvoicePt == null)
                throw new ArithmeticException("InvoicePT");
            this.invoice = initInvoicePt;
            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }
        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            this.bindingSource2.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
        }

        protected override string tableCode()
        {
            return "InvoicePT";
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

        #region Choose Object

        private void buttonEditDepotOut_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;

            if (this.newChooseDepot1.EditValue != null)
            {
                this.invoice.Depot = this.newChooseDepot1.EditValue as Model.Depot;
                this.invoice.DepotId = this.invoice.Depot.DepotId;
            }
            if (this.newChooseDepot2.EditValue != null)
            {
                this.invoice.DepotIn = this.newChooseDepot2.EditValue as Model.Depot;
                this.invoice.DepotInId = this.invoice.DepotIn.DepotId;
            }
            //this.invoice.Depot0 = this.buttonEditDepot0.EditValue as Model.Depot;   // 发货库房
            //this.invoice.Depot1 = this.buttonEditDepot1.EditValue as Model.Depot;   // 收货库房
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

        #region simpleButtonAppend_Click

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this.invoice.Details.Count > 0 && string.IsNullOrEmpty(this.invoice.Details[0].ProductId))
                    this.invoice.Details.RemoveAt(0);
                Model.InvoicePTDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.InvoicePTDetail();
                        detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                        detail.Invoice = this.invoice;
                        detail.Product = this.productManager.Get(product.ProductId);
                        detail.ProductId = detail.Product.ProductId;
                        detail.InvoicePTDetailQuantity = 0;
                        detail.InvoicePTDetailNote = "";
                        detail.InvoiceProductUnit = detail.Product.DepotUnit.CnName;
                        this.invoice.Details.Add(detail);
                        this.gridControl1.RefreshDataSource();
                        this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.InvoicePTDetail();
                    detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = this.invoice;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = detail.Product.ProductId;
                    detail.InvoicePTDetailQuantity = 0;
                    detail.InvoicePTDetailNote = "";
                    detail.InvoiceProductUnit = detail.Product.DepotUnit.CnName;
                    this.invoice.Details.Add(detail);
                    this.gridControl1.RefreshDataSource();
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                this.gridControl1.RefreshDataSource();

                // this.bindingSource2.DataSource = this.productManager.SelectNotCustomer();
            }
            f.Dispose();
            System.GC.Collect();


        }

        #endregion

        #region simpleButtonRemove_Click

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoicePTDetail);
                if (this.invoice.Details.Count == 0)
                {
                    if (this.action == "insert")
                    {
                        Model.InvoicePTDetail detail = new Model.InvoicePTDetail();
                        detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                        detail.InvoicePTDetailNote = "";
                        detail.InvoicePTDetailQuantity = 0;
                        detail.InvoiceProductUnit = "";
                        detail.Product = new Book.Model.Product();
                        this.invoice.Details.Add(detail);
                        this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                    }
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        #endregion

        protected override void Delete()
        {
            if (this.invoice != null)
            {

                this.invoiceManager.Delete(this.invoice.InvoiceId);
            }
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
                if (value is Model.InvoicePT)
                {
                    invoice = invoiceManager.Get((value as Model.InvoicePT).InvoiceId);
                }
            }
        }

        protected override void TurnNull()
        {
            if (this.invoice == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.invoiceManager.Delete(this.invoice);
            this.invoice = this.invoiceManager.GetNext(this.invoice);
            if (this.invoice == null)
            {
                this.invoice = this.invoiceManager.GetLast();
            }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoicePT();

            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoicePTDetail>();

            if (this.action == "insert")
            {
                Model.InvoicePTDetail detail = new Model.InvoicePTDetail();
                detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                detail.InvoicePTDetailNote = "";
                detail.InvoicePTDetailQuantity = 0;
                detail.InvoiceProductUnit = "";
                detail.Product = new Book.Model.Product();
                this.invoice.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        protected override void MoveNext()
        {
            Model.InvoicePT invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoicePT invoice = this.invoiceManager.GetPrev(this.invoice);
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
            if (LastFlag == 1) { LastFlag = 0; return; }
            this.invoice = this.invoiceManager.Get(this.invoiceManager.GetLast() == null ? "" : this.invoiceManager.GetLast().InvoiceId);
        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoicePT();
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
            this.newChooseDepot1.EditValue = this.invoice.Depot;
            this.newChooseDepot2.EditValue = this.invoice.DepotIn;
            //this.buttonEditDepot1.EditValue = this.invoice.Depot1;
            //this.buttonEditDepot0.EditValue = this.invoice.Depot0;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
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

                    //this.buttonEditDepot1.ButtonReadOnly = false;
                    //this.buttonEditDepot0.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    //this.buttonEditDepot1.ShowButton = true;
                    //this.buttonEditDepot0.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;

                    this.gridView1.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditNote.Properties.ReadOnly = false;


                    //this.buttonEditDepot1.ButtonReadOnly = false;
                    //this.buttonEditDepot0.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    //this.buttonEditDepot1.ShowButton = true;
                    //this.buttonEditDepot0.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;

                    this.gridView1.OptionsBehavior.Editable = true;

                    break;

                case "view":

                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;
                    this.textEditNote.Properties.ReadOnly = true;

                    //this.buttonEditDepot1.ButtonReadOnly = true;
                    // this.buttonEditDepot0.ButtonReadOnly = true;
                    this.buttonEditEmployee.ButtonReadOnly = true;
                    // this.buttonEditDepot1.ShowButton = false;
                    // this.buttonEditDepot0.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;

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
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoicePTDetail).Product;

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
                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        BL.ProductUnitManager unitManager = new Book.BL.ProductUnitManager();
                        IList<Model.ProductUnit> unitList = unitManager.Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit item in unitList)
                        {
                            this.repositoryItemComboBox1.Items.Add(item.CnName);
                        }
                    }
                }
            }
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.textEditInvoiceId, this });
        }

        private bool CanAdd(IList<Model.InvoicePTDetail> list)
        {

            foreach (Model.InvoicePTDetail detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId)
            {
                Model.InvoicePTDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoicePTDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                    detail.InvoicePTDetailNote = "";
                    detail.InvoicePTDetailQuantity = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.InvoiceProductUnit = p.DepotUnit.CnName;
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }

        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.CanAdd(this.invoice.Details))
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        Model.InvoicePTDetail detail = new Model.InvoicePTDetail();
                        detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                        detail.InvoicePTDetailNote = "";
                        detail.InvoicePTDetailQuantity = 0;
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

        private void barBtn_ChooseProduceIndepotDetails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Book.UI.produceManager.ProduceInDepot.SelectInDepotForm sf = new Book.UI.produceManager.ProduceInDepot.SelectInDepotForm();
            if (sf.ShowDialog(this) == DialogResult.OK)
            {
                if (sf.SelectItems == null || sf.SelectItems.Count == 0) return;
                if (this.invoice.Details.Count > 0 && string.IsNullOrEmpty(this.invoice.Details[0].ProductId))
                    this.invoice.Details.RemoveAt(0);
                if (!string.IsNullOrEmpty(sf.SelectItems[0].DepotPositionId))
                    this.newChooseDepot1.EditValue = sf.SelectItems[0].DepotPosition.Depot;
                foreach (Model.ProduceInDepotDetail item in sf.SelectItems)
                {
                    Model.InvoicePTDetail detail = new Book.Model.InvoicePTDetail();
                    detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = this.invoice;
                    detail.Product = item.Product;
                    detail.ProductId = item.ProductId;
                    detail.InvoicePTDetailQuantity = item.ProduceQuantity;
                    detail.InvoicePTDetailNote = "";
                    if (detail.Product != null)
                        detail.InvoiceProductUnit = detail.Product.DepotUnit == null ? null : detail.Product.DepotUnit.CnName;
                    detail.DepotPosition = item.DepotPosition;
                    detail.DepotPositionId = item.DepotPositionId;
                    detail.FromInvoiceId = item.ProduceInDepotId;
                    detail.SourceType = 1;
                    this.invoice.Details.Add(detail);
                    this.gridControl1.RefreshDataSource();
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
            }
        }

        private void barBtn_ChooseOtherCompact_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Book.UI.produceManager.ProduceOtherInDepot.ChooseProduceOtherInDepotForPCO f = new Book.UI.produceManager.ProduceOtherInDepot.ChooseProduceOtherInDepotForPCO();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectItems != null && f.SelectItems.Count > 0)
                {
                    if (this.invoice.Details.Count > 0 && string.IsNullOrEmpty(this.invoice.Details[0].ProductId))
                        this.invoice.Details.RemoveAt(0);
                    if (!string.IsNullOrEmpty(f.SelectItems[0].DepotPositionId))
                        this.newChooseDepot1.EditValue = f.SelectItems[0].DepotPosition.Depot;
                    foreach (Model.ProduceOtherInDepotDetail item in f.SelectItems)
                    {
                        Model.InvoicePTDetail detail = new Book.Model.InvoicePTDetail();
                        detail.InvoicePTDetailId = Guid.NewGuid().ToString();
                        detail.Invoice = this.invoice;
                        detail.Product = item.Product;
                        detail.ProductId = item.ProductId;
                        detail.InvoicePTDetailQuantity = item.ProduceInDepotQuantity;
                        detail.InvoicePTDetailNote = "";
                        if (detail.Product != null)
                            detail.InvoiceProductUnit = detail.Product.DepotUnit == null ? null : detail.Product.DepotUnit.CnName;
                        //detail.InvoiceProductUnit = detail.Product.DepotUnit.CnName;
                        detail.DepotPosition = item.DepotPosition;
                        detail.DepotPositionId = item.DepotPositionId;
                        detail.FromInvoiceId = item.ProduceOtherInDepotId;
                        detail.SourceType = 2;
                        this.invoice.Details.Add(detail);
                        this.gridControl1.RefreshDataSource();
                        this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                    }
                }
            }

        }

        private void newChooseDepot1_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseDepot1.EditValue != null)
            {
                this.bindingSource3.DataSource = this.depotPositionManager.Select(newChooseDepot1.EditValue as Model.Depot);
            }

        }

        private void newChooseDepot2_EditValueChanged(object sender, EventArgs e)
        {

            if (this.newChooseDepot2.EditValue != null)
            {
                this.bindingSource4.DataSource = this.depotPositionManager.Select(newChooseDepot2.EditValue as Model.Depot);
            }

        }
    }
}