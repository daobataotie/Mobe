using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.CF
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 拆分單據錄入(包括入庫出庫的一些常規操作和一些詳細信息的展示)
     * 繼承了基類窗體,風格統一,介面比較美觀
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-05-09
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {

        #region 變量對象定義
        protected BL.InvoiceCFDetailManager invoiceCFDetailManager = new Book.BL.InvoiceCFDetailManager();
        protected BL.InvoiceCFManager invoiceManager = new Book.BL.InvoiceCFManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        protected Model.InvoiceCF invoice;
        #endregion

        #region Constructors

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            //this.requireValueExceptions.Add("Employee0",    new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Depot1", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot0));
            this.requireValueExceptions.Add("Depot2", new AA(Properties.Resources.RequiredDataOfDepot, this.buttonEditDepot1));
            this.requireValueExceptions.Add("Details0", new AA(Properties.Resources.RequireDataForDetails, this.gridControlOut));
            this.requireValueExceptions.Add("Details1", new AA(Properties.Resources.RequireDataForDetails, this.gridControlIn));

            this.invalidValueExceptions.Add(Model.InvoiceCF.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";
            this.buttonEditDepot0.Choose = new ChooseDepot();
            this.buttonEditDepot1.Choose = new ChooseDepot();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.EmpAudit.Choose = new ChooseEmployee();
        }

        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";
        }

        public EditForm(Model.InvoiceCF initCF)
            : this()
        {
            if (initCF == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = initCF;
            this.action = "update";
        }

        #endregion

        #region FormLoad

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSource1.DataSource = this.productManager.Select();
            this.bindingSource2.DataSource = this.productManager.Select();

        }

        protected override string tableCode()
        {
            return "InvoiceCF";
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
            this.invoice.DetailsIn = invoiceCFDetailManager.Select("I", this.invoice);
            this.invoice.DetailsOut = invoiceCFDetailManager.Select("O", this.invoice);

            global::Helper.InvoiceStatus status = (Helper.InvoiceStatus)this.invoice.InvoiceStatus.Value;

            this.textEditInvoiceId.Properties.ReadOnly = true;


            //this.textEditAbstract.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);
            this.textEditNote.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);

            this.buttonEditDepot1.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.buttonEditDepot0.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            //this.buttonEditEmployee.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.simpleButtonAppendIn.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.simpleButtonRemoveIn.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.simpleButtonAppendOut.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.simpleButtonRemoveOut.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.barButtonItemSave.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.colInvoiceZZDetailNote1.OptionsColumn.AllowEdit = (status != global::Helper.InvoiceStatus.Null);
        }

        #endregion

        #region 重載父類的方法
        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoice.InvoiceId);
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            //this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee; ;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            //this.invoice.Depot1 = this.buttonEditDepot0.EditValue as Model.Depot;
            //this.invoice.Depot2 = this.buttonEditDepot1.EditValue as Model.Depot;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            this.invoice.AuditState = this.saveAuditState;
            //this.invoice.Employee2 = BL.V.ActiveOperator;

            this.gridViewIn.UpdateCurrentRow();
            this.gridViewOut.UpdateCurrentRow();

            if (!this.gridViewIn.PostEditor() || !this.gridViewIn.UpdateCurrentRow())
                return;
            if (!this.gridViewOut.PostEditor() || !this.gridViewOut.UpdateCurrentRow())
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

        public override Book.Model.Invoice Invoice
        {
            get
            {
                return invoice;
            }
            set
            {
                if (value is Model.InvoiceCF)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceCF).InvoiceId);
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
            this.invoice = new Model.InvoiceCF();

            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;

            this.invoice.DetailsIn = new List<Model.InvoiceCFDetail>();
            this.invoice.DetailsOut = new List<Model.InvoiceCFDetail>();

            if (this.action == "insert")
            {
                Model.InvoiceCFDetail detailOut = new Model.InvoiceCFDetail();
                detailOut.InvoiceCFDetailId = Guid.NewGuid().ToString();
                detailOut.InvoiceCFDetailKind = "O";
                detailOut.InvoiceCFDetailNote = "";
                detailOut.InvoiceCFDetailPrice = 0;
                detailOut.InvoiceCFDetailQuantity = 0;
                detailOut.InvoiceCFDetailZongji = 0;
                detailOut.InvoiceProductUnit = "";
                detailOut.Product = new Book.Model.Product();
                this.invoice.DetailsOut.Add(detailOut);
                this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detailOut);

                Model.InvoiceCFDetail detailIn = new Model.InvoiceCFDetail();
                detailIn.InvoiceCFDetailId = Guid.NewGuid().ToString();
                detailIn.InvoiceCFDetailKind = "I";
                detailIn.InvoiceCFDetailNote = "";
                detailIn.InvoiceCFDetailPrice = 0;
                detailIn.InvoiceCFDetailQuantity = 0;
                detailIn.InvoiceCFDetailZongji = 0;
                detailIn.InvoiceProductUnit = "";
                detailIn.Product = new Book.Model.Product();
                this.invoice.DetailsIn.Add(detailIn);
                this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detailIn);
            }
        }

        protected override void MoveNext()
        {
            Model.InvoiceCF invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceCF invoice = this.invoiceManager.GetPrev(this.invoice);
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
                this.invoice = new Book.Model.InvoiceCF();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            //this.buttonEditEmployee.EditValue = this.invoice.Employee0;

            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            //this.buttonEditDepot1.EditValue = this.invoice.Depot2;
            //this.buttonEditDepot0.EditValue = this.invoice.Depot1;

            this.bindingSourceIn.DataSource = this.invoice.DetailsIn;
            this.bindingSourceOut.DataSource = this.invoice.DetailsOut;

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;

                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot0.ShowButton = true;
                    this.buttonEditDepot1.ShowButton = true;

                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditDepot0.ButtonReadOnly = false;
                    this.buttonEditDepot1.ButtonReadOnly = false;

                    this.simpleButtonAppendIn.Enabled = true;
                    this.simpleButtonRemoveIn.Enabled = true;

                    this.simpleButtonAppendOut.Enabled = true;
                    this.simpleButtonRemoveOut.Enabled = true;


                    this.gridViewIn.OptionsBehavior.Editable = true;
                    this.gridViewOut.OptionsBehavior.Editable = true;
                    break;

                case "update":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    //this.textEditAbstract.Properties.ReadOnly = false;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditEmployee.ShowButton = true;
                    this.buttonEditDepot0.ShowButton = true;
                    this.buttonEditDepot1.ShowButton = true;

                    this.buttonEditEmployee.ButtonReadOnly = false;
                    this.buttonEditDepot0.ButtonReadOnly = false;
                    this.buttonEditDepot1.ButtonReadOnly = false;

                    this.simpleButtonAppendIn.Enabled = false;
                    this.simpleButtonRemoveIn.Enabled = true;

                    this.simpleButtonAppendOut.Enabled = true;
                    this.simpleButtonRemoveOut.Enabled = true;

                    this.gridViewIn.OptionsBehavior.Editable = true;
                    this.gridViewOut.OptionsBehavior.Editable = true;

                    break;
                case "view":
                    this.textEditInvoiceId.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.ReadOnly = true;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = false;

                    this.textEditNote.Properties.ReadOnly = true;

                    this.buttonEditEmployee.ShowButton = false;
                    this.buttonEditDepot0.ShowButton = false;
                    this.buttonEditDepot1.ShowButton = false;

                    this.buttonEditEmployee.ButtonReadOnly = true;
                    this.buttonEditDepot0.ButtonReadOnly = true;
                    this.buttonEditDepot1.ButtonReadOnly = true;

                    this.simpleButtonAppendIn.Enabled = false;
                    this.simpleButtonRemoveIn.Enabled = false;

                    this.simpleButtonAppendOut.Enabled = false;
                    this.simpleButtonRemoveOut.Enabled = false;


                    this.gridViewIn.OptionsBehavior.Editable = false;
                    this.gridViewOut.OptionsBehavior.Editable = false;
                    break;

                default:
                    break;
            }

            base.Refresh();
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return null;
            //return new R01(this.invoice.InvoiceId);
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

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditInvoiceId, this });
        }

        #endregion

        #region 添加入庫商品按鈕
        private void simpleButtonAppendIn_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceCFDetail detail = new Book.Model.InvoiceCFDetail();
                detail.InvoiceCFDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.InvoiceCFDetailKind = "I";
                detail.InvoiceCFDetailNote = "";
                detail.InvoiceCFDetailQuantity = 1;
                detail.InvoiceCFDetailPrice = 0;
                detail.InvoiceCFDetailZongji = 0;
                this.invoice.DetailsIn.Add(detail);
                this.gridControlIn.RefreshDataSource();
                //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;

                this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detail);

                this.bindingSource2.DataSource = this.productManager.Select();
            }
        }
        #endregion

        #region 刪除入庫上篇按鈕
        private void simpleButtonRemoveIn_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceIn.Current != null)
            {
                this.invoice.DetailsIn.Remove(this.bindingSourceIn.Current as Model.InvoiceCFDetail);
                if (this.invoice.DetailsIn.Count == 0)
                {
                    Model.InvoiceCFDetail detail = new Model.InvoiceCFDetail();
                    detail.InvoiceCFDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceCFDetailKind = "I";
                    detail.InvoiceCFDetailNote = "";
                    detail.InvoiceCFDetailPrice = 0;
                    detail.InvoiceCFDetailQuantity = 0;
                    detail.InvoiceCFDetailZongji = 0;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.invoice.DetailsIn.Add(detail);
                    this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detail);
                }
                this.gridControlIn.RefreshDataSource();
            }
        }
        #endregion

        #region 員工信息選擇項
        private void buttonEditInvoiceEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }
        #endregion

        #region 部門信息選擇項
        private void buttonEditDepotOut_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseDepotForm f = new ChooseDepotForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }
        #endregion

        #region 添加出庫商品按鈕
        private void simpleButtonAppendOut_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceCFDetail detail = new Book.Model.InvoiceCFDetail();
                detail.InvoiceCFDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.InvoiceCFDetailKind = "O";
                detail.InvoiceCFDetailNote = "";
                detail.InvoiceCFDetailQuantity = 1;
                detail.InvoiceCFDetailPrice = 0;
                detail.InvoiceCFDetailZongji = 0;
                this.invoice.DetailsOut.Clear();
                this.invoice.DetailsOut.Add(detail);
                this.gridControlOut.RefreshDataSource();
                //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                this.simpleButtonAppendOut.Enabled = false;

                this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detail);

                this.bindingSource1.DataSource = this.productManager.Select();
            }
        }
        #endregion

        #region 刪除出庫商品按鈕
        private void simpleButtonRemoveOut_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceOut.Current != null)
            {
                this.invoice.DetailsOut.Remove(this.bindingSourceOut.Current as Model.InvoiceCFDetail);
                if (this.invoice.DetailsOut.Count == 0)
                {
                    Model.InvoiceCFDetail detail = new Model.InvoiceCFDetail();
                    detail.InvoiceCFDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceCFDetailKind = "O";
                    detail.InvoiceCFDetailNote = "";
                    detail.InvoiceCFDetailPrice = 0;
                    detail.InvoiceCFDetailQuantity = 0;
                    detail.InvoiceCFDetailZongji = 0;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.invoice.DetailsOut.Add(detail);
                    this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detail);
                }
                this.gridControlOut.RefreshDataSource();
                this.simpleButtonAppendOut.Enabled = true;
            }
        }
        #endregion

        #region 選擇日期文本框的失去焦點事件
        private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        {
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this.invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
        }
        #endregion

        #region 添加出庫gridview的cell單元格的值改變計算總計的事件
        private void gridViewOut_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal quantity = 1;
            decimal price = 0;
            if (e.Column == this.gridColumnOutPrice)
            {
                decimal.TryParse(this.gridViewOut.GetRowCellValue(e.RowHandle, this.gridColumnOutPrice).ToString(), out price);
                this.gridViewOut.SetRowCellValue(e.RowHandle, this.gridColumnOutZongJi, quantity * price);
                Zongji();
            }
        }
        #endregion

        #region 添加入庫gridview的cell單元格的值改變計算總計的事件
        private void gridViewIn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Zongji();
        }
        #endregion

        #region 計算總計的方法
        private void Zongji()
        {
            if (this.action == "insert" || this.action == "update")
            {
                Model.InvoiceCFDetail detailIn = null;

                decimal zongji = 0;

                if (this.invoice.DetailsOut.Count > 0)
                {
                    foreach (Model.InvoiceCFDetail detail in this.invoice.DetailsOut)
                    {
                        zongji += detail.InvoiceCFDetailZongji.Value;
                    }
                }
                if (this.invoice.DetailsIn.Count > 0)
                {
                    detailIn = this.invoice.DetailsIn[0];
                }

                if (detailIn != null)
                {
                    detailIn.InvoiceCFDetailPrice = zongji;
                    detailIn.InvoiceCFDetailZongji = Convert.ToDecimal(detailIn.InvoiceCFDetailQuantity) * zongji;
                }
                this.gridControlIn.RefreshDataSource();
            }
        }
        #endregion

        #region 入庫gridview的行改變事件
        private void gridViewIn_RowCountChanged(object sender, EventArgs e)
        {
            Zongji();
        }
        #endregion

        #region 出庫gridview的行改變事件
        private void gridViewOut_RowCountChanged(object sender, EventArgs e)
        {
            Zongji();
        }
        #endregion

        #region 出庫gridview中列'單位'選擇項
        private void gridViewOut_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridViewOut.FocusedColumn.Name == "gridColumn1")
            {
                if (this.gridViewOut.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridViewOut.GetRow(this.gridViewOut.FocusedRowHandle) as Model.InvoiceCFDetail).Product;

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
        #endregion

        #region 入庫gridview中列'單位'選擇項
        private void gridViewIn_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridViewIn.FocusedColumn.Name == "gridColumn2")
            {
                if (this.gridViewIn.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridViewIn.GetRow(this.gridViewIn.FocusedRowHandle) as Model.InvoiceCFDetail).Product;

                    this.repositoryItemComboBox2.Items.Clear();

                    //if (!string.IsNullOrEmpty(p.ProductBaseUnit))
                    //{
                    //    this.repositoryItemComboBox2.Items.Add(p.ProductBaseUnit);
                    //}
                    //if (!string.IsNullOrEmpty(p.ProductInnerPackagingUnit))
                    //{
                    //    this.repositoryItemComboBox2.Items.Add(p.ProductInnerPackagingUnit);
                    //}
                    //if (!string.IsNullOrEmpty(p.ProductOuterPackagingUnit))
                    //{
                    //    this.repositoryItemComboBox2.Items.Add(p.ProductOuterPackagingUnit);
                    //}
                }
            }
        }
        #endregion

        #region 入庫單詳細信息
        private bool CanAdd(IList<Model.InvoiceCFDetail> list)
        {
            foreach (Model.InvoiceCFDetail detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }
        #endregion

        #region 出庫gridview單元格的值改變事件
        private void gridViewOut_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId1)
            {
                Model.InvoiceCFDetail detailOut = this.gridViewOut.GetRow(e.RowHandle) as Model.InvoiceCFDetail;
                if (detailOut != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detailOut.InvoiceCFDetailId = Guid.NewGuid().ToString();
                    detailOut.InvoiceCFDetailKind = "I";
                    detailOut.InvoiceCFDetailNote = "";
                    detailOut.InvoiceCFDetailPrice = 0;
                    detailOut.InvoiceCFDetailQuantity = 0;
                    detailOut.InvoiceCFDetailZongji = 0;
                    detailOut.Product = p;
                    detailOut.ProductId = p.ProductId;
                    //detailOut.InvoiceProductUnit = detailOut.Product.ProductBaseUnit;                    
                    this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detailOut);

                    this.simpleButtonAppendOut.Enabled = false;
                }
                this.gridControlOut.RefreshDataSource();
            }
        }
        #endregion

        #region 出庫gridview的keydown事件
        private void gridViewOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButtonRemoveOut.PerformClick();
                }
                this.gridControlOut.RefreshDataSource();
            }
        }
        #endregion

        #region 入庫gridview的keydown事件
        private void gridViewIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.CanAdd(this.invoice.DetailsIn))
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        Model.InvoiceCFDetail detailIn = new Model.InvoiceCFDetail();
                        detailIn.InvoiceCFDetailId = Guid.NewGuid().ToString();
                        detailIn.InvoiceCFDetailKind = "I";
                        detailIn.InvoiceCFDetailNote = "";
                        detailIn.InvoiceCFDetailPrice = 0;
                        detailIn.InvoiceCFDetailQuantity = 0;
                        detailIn.InvoiceCFDetailZongji = 0;
                        detailIn.InvoiceProductUnit = "";
                        detailIn.Product = new Book.Model.Product();
                        this.invoice.DetailsIn.Add(detailIn);
                        this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detailIn);
                    }
                }
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButtonRemoveIn.PerformClick();
                }
                this.gridControlIn.RefreshDataSource();
            }
        }
        #endregion

        #region 入庫gridview單元格值的改變事件
        private void gridViewIn_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId)
            {
                Model.InvoiceCFDetail detailIn = this.gridViewIn.GetRow(e.RowHandle) as Model.InvoiceCFDetail;
                if (detailIn != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detailIn.InvoiceCFDetailId = Guid.NewGuid().ToString();
                    detailIn.InvoiceCFDetailKind = "I";
                    detailIn.InvoiceCFDetailNote = "";
                    detailIn.InvoiceCFDetailPrice = 0;
                    detailIn.InvoiceCFDetailQuantity = 0;
                    detailIn.InvoiceCFDetailZongji = 0;
                    detailIn.Product = p;
                    detailIn.ProductId = p.ProductId;
                    //detailIn.InvoiceProductUnit = detailIn.Product.ProductBaseUnit;                    
                    this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detailIn);
                }
                this.gridControlIn.RefreshDataSource();
            }
        }
        #endregion
    }
}