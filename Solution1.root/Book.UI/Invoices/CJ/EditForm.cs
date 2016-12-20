using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using System.Data.SqlClient;
namespace Book.UI.Invoices.CJ
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 採購報價單的單據錄入(繼承了父類BaseEditForm窗體,實現了
     * 介面的風格統一)
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-05-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {

        #region 變量對象的定義
        protected BL.InvoiceCJManager invoiceManager = new Book.BL.InvoiceCJManager();
        protected BL.InvoiceCJDetailManager invoiceCJDetailManager = new Book.BL.InvoiceCJDetailManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private Model.InvoiceCJ invoice;
        #endregion

        #region Constructors

        public EditForm()
        {
            InitializeComponent();
            this.colInvoiceCJDetailPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colInvoiceCJDetailPrice.DisplayFormat.FormatString = this.GetFormat(BL.V.SetDataFormat.CGDJXiao.Value);

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));
            this.requireValueExceptions.Add("Price", new AA("請填寫單價或數量！", this.gridControl1));
            this.invalidValueExceptions.Add(Model.InvoiceCJ.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.action = "view";
            this.buttonEditCompany.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.EmpAudit.Choose = new ChooseEmployee();
        }
        int LastFlag = 0; //页面载入时是否执行 last方法
        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");

            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        public EditForm(Model.InvoiceCJ invoicecj)
            : this()
        {
            if (invoicecj == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoicecj;

            this.action = "view";
            if (this.action == "view")
                LastFlag = 1;
        }

        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {

            string sql = "SELECT productid,id,productname FROM product WHERE IsCustomerProduct IS NULL OR IsCustomerProduct =0";
            this.bindingSource2.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
        }

        protected override string getName()
        {
            string formName = this.GetType().FullName;
            formName = formName.Substring(formName.IndexOf('.') + 1).Substring(formName.Substring(formName.IndexOf('.') + 1).IndexOf('.') + 1);
            return formName;
        }

        protected override string tableCode()
        {
            return "InvoiceCJ";
        }

        protected override int AuditState()
        {
            return this.invoice.AuditState.HasValue ? this.invoice.AuditState.Value : 0;
        }

        /*
        private void update()
        {
            this.invoice.Details = this.invoiceCJDetailManager.Select(this.invoice);

            global::Helper.InvoiceStatus status = (Helper.InvoiceStatus)this.invoice.InvoiceStatus.Value;

            this.textEditInvoiceId.Properties.ReadOnly = true;
            

            //this.textEditAbstract.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);
            this.textEditNote.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);

            this.buttonEditEmployee.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.buttonEditCompany.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.colInvoiceCJDetailQuantity.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceCJDetailNote.OptionsColumn.AllowEdit = (status != global::Helper.InvoiceStatus.Null);
            this.colInvoiceCJDetailPrice.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);

            this.simpleButtonAppend.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.simpleButtonRemove.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.barButtonItemSave.Enabled = (status == global::Helper.InvoiceStatus.Draft);
        }
         * */
        #endregion

        protected override void Save(Helper.InvoiceStatus status)
        {
            //单据编号
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            //单据日期
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            //经手人
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            //供应商
            this.invoice.Supplier = this.buttonEditCompany.EditValue as Model.Supplier;
            //备注
            this.invoice.InvoiceNote = this.textEditNote.Text;
            //总金额
            this.invoice.InvoiceTotal = this.calcEditTotal.Value;
            //录单人
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            //录单时间
            this.invoice.InvoiceLRTime = DateTime.Now;
            //审核状态
            this.invoice.AuditState = this.saveAuditState;

            //this.invoice.Employee2 = BL.V.ActiveOperator;

            this.invoice.InvoiceStatus = (int)status;

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

        #region simpleButton_Click

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {

                if (this.invoice.Details.Count > 0 && this.invoice.Details[0] != null && string.IsNullOrEmpty(this.invoice.Details[0].ProductId))
                    this.invoice.Details.RemoveAt(0);
                Model.InvoiceCJDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {

                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.InvoiceCJDetail();
                        detail.Inumber = this.invoice.Details.Count + 1;
                        detail.InvoiceCJDetailId = Guid.NewGuid().ToString();
                        detail.Invoice = this.invoice;
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.InvoiceCJDetailQuantity = 1;
                        detail.InvoiceCJDetailPrice = detail.Product.NewestCost == null ? 0 : detail.Product.NewestCost.Value;
                        detail.InvoiceCJDetailMoney = detail.InvoiceCJDetailPrice;
                        detail.InvoiceCJDetailNote = "";

                        detail.InvoiceProductUnit = detail.Product.DepotUnit == null ? null : detail.Product.DepotUnit.CnName;
                        this.invoice.Details.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.InvoiceCJDetail();
                    detail.Inumber = this.invoice.Details.Count + 1;
                    detail.InvoiceCJDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = this.invoice;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    detail.InvoiceCJDetailQuantity = 1;
                    detail.InvoiceCJDetailPrice = detail.Product.NewestCost == null ? 0 : detail.Product.NewestCost.Value;
                    detail.InvoiceCJDetailMoney = detail.InvoiceCJDetailPrice;
                    detail.InvoiceCJDetailNote = "";
                    detail.InvoiceProductUnit = detail.Product.DepotUnit == null ? null : detail.Product.DepotUnit.CnName;
                    this.invoice.Details.Add(detail);

                }
                this.gridControl1.RefreshDataSource();
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                // this.bindingSource2.DataSource = this.productManager.Select();
            }
            f.Dispose();
            System.GC.Collect();
        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoiceCJDetail);

                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceCJDetail detail = new Model.InvoiceCJDetail();
                    detail.Inumber = this.invoice.Details.Count + 1;
                    detail.InvoiceCJDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceCJDetailMoney = 0;
                    detail.InvoiceCJDetailNote = "";
                    detail.InvoiceCJDetailPrice = 0;
                    detail.InvoiceCJDetailQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.invoice.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
                this.Total();
            }
        }
        #endregion

        #region CellValueChange

        private void Total()
        {
            decimal total = decimal.Zero;
            foreach (Model.InvoiceCJDetail detail in this.invoice.Details)
            {

                total += detail.InvoiceCJDetailMoney.Value;
            }
            total = GetDecimal(total, BL.V.SetDataFormat.CGZJXiao.Value);
            this.calcEditTotal.EditValue = total;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colInvoiceCJDetailPrice || e.Column == this.colInvoiceCJDetailQuantity)
            {
                decimal price = decimal.Zero;
                decimal quantity = decimal.Zero;

                if (e.Column == this.colInvoiceCJDetailPrice)
                {
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out price);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCJDetailQuantity).ToString(), out quantity);
                }
                if (e.Column == this.colInvoiceCJDetailQuantity)
                {
                    decimal.TryParse(e.Value == null ? "0" : e.Value.ToString(), out quantity);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceCJDetailPrice).ToString(), out price);
                }

                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceCJDetailMoney, GetDecimal(price * quantity, BL.V.SetDataFormat.CGJEXiao.Value));
                this.Total();
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId)
            {
                Model.InvoiceCJDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceCJDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.InvoiceCJDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = this.invoice;
                    detail.InvoiceCJDetailNote = "";
                    detail.InvoiceCJDetailQuantity = 1;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.InvoiceProductUnit = p.DepotUnit.CnName;
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
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

        private void buttonEditCompany_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseSupplier f = new ChooseSupplier();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        #endregion

        #region 重載基類的方法
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
                if (value is Model.InvoiceCJ)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceCJ).InvoiceId);
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
            this.invoice = new Model.InvoiceCJ();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceCJDetail>();
            if (this.action == "insert")
            {
                Model.InvoiceCJDetail detail = new Model.InvoiceCJDetail();
                detail.Inumber = this.invoice.Details.Count + 1;
                detail.InvoiceCJDetailId = Guid.NewGuid().ToString();
                detail.InvoiceCJDetailMoney = 0;
                detail.InvoiceCJDetailNote = "";
                detail.InvoiceCJDetailPrice = 0;
                detail.InvoiceCJDetailQuantity = 0;
                detail.InvoiceProductUnit = "";
                detail.Product = new Book.Model.Product();
                this.invoice.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        protected override void MoveNext()
        {
            Model.InvoiceCJ invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceCJ invoice = this.invoiceManager.GetPrev(this.invoice);
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
            gridView1.IndicatorWidth = 30;
            if (this.invoice == null)
            {
                this.invoice = new Book.Model.InvoiceCJ();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.EditValue = this.invoice.InvoiceDate;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditCompany.EditValue = this.invoice.Supplier;
            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSource1.DataSource = this.invoice.Details;
            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;

                    this.barButtonItemGeneral.Enabled = false;

                    //this.textEditAbstract.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.barButtonItemGeneral.Enabled = false;

                    //this.textEditAbstract.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditCompany.ShowButton = true;
                    this.buttonEditEmployee.ShowButton = true;

                    this.buttonEditCompany.ButtonReadOnly = false;
                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    break;

                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.barButtonItemGeneral.Enabled = true;

                    //this.textEditAbstract.Properties.ReadOnly = true;
                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditCompany.ShowButton = false;
                    this.buttonEditEmployee.ShowButton = false;

                    this.buttonEditCompany.ButtonReadOnly = true;
                    this.buttonEditEmployee.ButtonReadOnly = true;

                    this.gridView1.OptionsBehavior.Editable = false;

                    this.simpleButtonAppend.Enabled = false;
                    this.simpleButtonRemove.Enabled = false;
                    break;

                default:
                    break;
            }

            base.Refresh();
            this.textEditInvoiceId.Properties.ReadOnly = true;
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

        #endregion

        #region 日期選擇文本框的鼠標離開事件
        private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        {
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
        }
        #endregion


        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn == gridColumn1)
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceCJDetail).Product;

                    this.repositoryItemComboBox1.Items.Clear();

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

        #region gridview 行數改變事件
        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            this.Total();
        }
        #endregion

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this });
        }

        private bool CanAdd(IList<Model.InvoiceCJDetail> list)
        {
            foreach (Model.InvoiceCJDetail detail in list)
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
                        Model.InvoiceCJDetail detail = new Model.InvoiceCJDetail();
                        detail.Inumber = this.invoice.Details.Count + 1;
                        detail.InvoiceCJDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceCJDetailMoney = 0;
                        detail.InvoiceCJDetailNote = "";
                        detail.InvoiceCJDetailPrice = 0;
                        detail.InvoiceCJDetailQuantity = 0;
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

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            if (e.ListSourceRowIndex < 0) return;
            IList<Model.InvoiceCJDetail> details = this.bindingSource1.DataSource as IList<Model.InvoiceCJDetail>;
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

        private void dateEditInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //转单代码

            string tag = (string)e.Item.Tag;
            switch (tag)
            {
                case "CO":
                    Operations.Open("invoices.co.edit1", this.MdiParent, this.invoice);
                    break;
                default:
                    break;
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {

            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }
    }
}