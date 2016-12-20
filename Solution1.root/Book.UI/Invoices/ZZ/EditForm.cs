using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
namespace Book.UI.Invoices.ZZ
{
    public partial class EditForm : BaseEditForm
    {
        protected BL.InvoiceZZDetailManager invoiceZZDetailManager = new Book.BL.InvoiceZZDetailManager();
        protected BL.InvoiceZZManager invoiceManager = new Book.BL.InvoiceZZManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        protected BL.DepotManager depotManager = new Book.BL.DepotManager();

        protected Model.InvoiceZZ invoice;

        #region Constructors

        public EditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Details0", new AA(Properties.Resources.RequireDataForDetails, this.gridControlOut));
            this.requireValueExceptions.Add("Details1", new AA(Properties.Resources.RequireDataForDetails, this.gridControlIn));

            this.invalidValueExceptions.Add(Model.InvoiceZZ.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";
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

        public EditForm(Model.InvoiceZZ initZZ)
            : this()
        {
            if (initZZ == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = initZZ;
            this.action = "update";
        }

        #endregion

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourceOut1.DataSource = this.productManager.Select();
            this.bindingSourceIn2.DataSource = this.productManager.Select();
        }

        protected override string tableCode()
        {
            return "InvoiceZZ";
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
            this.invoice.DetailsIn = invoiceZZDetailManager.Select("I", this.invoice);
            this.invoice.DetailsOut = invoiceZZDetailManager.Select("O", this.invoice);

            global::Helper.InvoiceStatus status = (Helper.InvoiceStatus)this.invoice.InvoiceStatus.Value;

            this.textEditInvoiceId.Properties.ReadOnly = true;


            //this.textEditAbstract.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);
            this.textEditNote.Properties.ReadOnly = (status == global::Helper.InvoiceStatus.Null);

            this.buttonEditEmployee.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.simpleButtonAppendIn.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.simpleButtonRemoveIn.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.simpleButtonAppendOut.Enabled = (status == global::Helper.InvoiceStatus.Draft);
            this.simpleButtonRemoveOut.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.barButtonItemSave.Enabled = (status == global::Helper.InvoiceStatus.Draft);

            this.colInvoiceZZDetailQuantity.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceInDepot.OptionsColumn.AllowEdit = (status != global::Helper.InvoiceStatus.Null);

            this.colInvoiceZZDetailQuantity1.OptionsColumn.AllowEdit = (status == global::Helper.InvoiceStatus.Draft);
            this.colInvoiceOutDepot.OptionsColumn.AllowEdit = (status != global::Helper.InvoiceStatus.Null);
        }

        protected override void Delete()
        {
            this.invoiceManager.Delete(this.invoice.InvoiceId);
        }

        private void simpleButtonAppendIn_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceZZDetail detail = new Book.Model.InvoiceZZDetail();
                detail.InvoiceZZDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.InvoiceZZDetailKind = "I";
                detail.InvoiceZZDetailNote = "";
                detail.InvoiceZZDetailQuantity = 1;
                detail.InvoiceZZDetailPrice = 0;
                detail.InvoiceZZDetailZongji = 0;
                //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;
                this.invoice.DetailsIn.Clear();
                this.invoice.DetailsIn.Add(detail);
                this.gridControlIn.RefreshDataSource();
                this.simpleButtonAppendIn.Enabled = false;

                this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detail);

                this.bindingSourceIn2.DataSource = this.productManager.Select();
            }
        }

        private void simpleButtonRemoveIn_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceIn.Current != null)
            {
                this.invoice.DetailsIn.Remove(this.bindingSourceIn.Current as Model.InvoiceZZDetail);
                if (this.invoice.DetailsIn.Count == 0)
                {
                    Model.InvoiceZZDetail detail = new Model.InvoiceZZDetail();
                    detail.InvoiceZZDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceZZDetailKind = "I";
                    detail.InvoiceZZDetailNote = "";
                    detail.InvoiceZZDetailPrice = 0;
                    detail.InvoiceZZDetailQuantity = 0;
                    detail.InvoiceZZDetailZongji = 0;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.invoice.DetailsIn.Add(detail);
                    this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detail);
                }
                this.gridControlIn.RefreshDataSource();
                this.simpleButtonAppendIn.Enabled = true;
            }
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee; ;
            //this.invoice.InvoiceAbstract = this.textEditAbstract.Text;
            this.invoice.InvoiceNote = this.textEditNote.Text;
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
                if (value is Model.InvoiceZZ)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceZZ).InvoiceId);
                }
            }
        }

        private void buttonEditInvoiceEmployee_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseEmployeeForm f = new ChooseEmployeeForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
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

        private void simpleButtonAppendOut_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceZZDetail detail = new Book.Model.InvoiceZZDetail();
                detail.InvoiceZZDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.InvoiceZZDetailKind = "O";
                detail.InvoiceZZDetailNote = "";
                detail.InvoiceZZDetailQuantity = 1;
                detail.InvoiceZZDetailZongji = 0;
                detail.InvoiceZZDetailPrice = 0;
                //detail.InvoiceProductUnit = detail.Product.ProductBaseUnit;                
                this.invoice.DetailsOut.Add(detail);
                this.gridControlOut.RefreshDataSource();

                this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detail);

                this.bindingSourceOut1.DataSource = this.productManager.Select();
            }
        }

        private void simpleButtonRemoveOut_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceOut.Current != null)
            {
                this.invoice.DetailsOut.Remove(this.bindingSourceOut.Current as Model.InvoiceZZDetail);
                if (this.invoice.DetailsOut.Count == 0)
                {
                    Model.InvoiceZZDetail detail = new Model.InvoiceZZDetail();
                    detail.InvoiceZZDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceZZDetailKind = "O";
                    detail.InvoiceZZDetailNote = "";
                    detail.InvoiceZZDetailPrice = 0;
                    detail.InvoiceZZDetailQuantity = 0;
                    detail.InvoiceZZDetailZongji = 0;
                    detail.InvoiceProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this.invoice.DetailsOut.Add(detail);
                    this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detail);
                }
                this.gridControlOut.RefreshDataSource();
            }
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceZZ();

            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;

            this.invoice.DetailsIn = new List<Model.InvoiceZZDetail>();
            this.invoice.DetailsOut = new List<Model.InvoiceZZDetail>();

            if (this.action == "insert")
            {
                Model.InvoiceZZDetail detailOut = new Model.InvoiceZZDetail();
                detailOut.InvoiceZZDetailId = Guid.NewGuid().ToString();
                detailOut.InvoiceZZDetailKind = "O";
                detailOut.InvoiceZZDetailNote = "";
                detailOut.InvoiceZZDetailPrice = 0;
                detailOut.InvoiceZZDetailQuantity = 0;
                detailOut.InvoiceZZDetailZongji = 0;
                detailOut.InvoiceProductUnit = "";
                detailOut.Product = new Book.Model.Product();
                this.invoice.DetailsOut.Add(detailOut);
                this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detailOut);

                Model.InvoiceZZDetail detailIn = new Model.InvoiceZZDetail();
                detailIn.InvoiceZZDetailId = Guid.NewGuid().ToString();
                detailIn.InvoiceZZDetailKind = "I";
                detailIn.InvoiceZZDetailNote = "";
                detailIn.InvoiceZZDetailPrice = 0;
                detailIn.InvoiceZZDetailQuantity = 0;
                detailIn.InvoiceZZDetailZongji = 0;
                detailIn.InvoiceProductUnit = "";
                detailIn.Product = new Book.Model.Product();
                this.invoice.DetailsIn.Add(detailIn);
                this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detailIn);
            }
        }

        protected override void MoveNext()
        {
            Model.InvoiceZZ invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceZZ invoice = this.invoiceManager.GetPrev(this.invoice);
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
                this.invoice = new Book.Model.InvoiceZZ();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
            }
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;

            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.bindingSourceIn.DataSource = this.invoice.DetailsIn;
            this.bindingSourceOut.DataSource = this.invoice.DetailsOut;

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.buttonEditEmployee.ShowButton = true;

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
                    this.textEditNote.Properties.ReadOnly = false;

                    this.buttonEditEmployee.ButtonReadOnly = false;

                    this.buttonEditEmployee.ShowButton = true;

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

                    this.buttonEditEmployee.ButtonReadOnly = true;

                    this.buttonEditEmployee.ShowButton = false;

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

        private void gridViewOut_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal quantity = 0;
            decimal price = 0;
            if (e.Column == this.gridColumnOutPrice || e.Column == this.colInvoiceZZDetailQuantity1)
            {
                decimal.TryParse(this.gridViewOut.GetRowCellValue(e.RowHandle, this.colInvoiceZZDetailQuantity).ToString(), out quantity);
                decimal.TryParse(this.gridViewOut.GetRowCellValue(e.RowHandle, this.gridColumnOutPrice).ToString(), out price);

                this.gridViewOut.SetRowCellValue(e.RowHandle, this.gridColumnOutZongJi, quantity * price);
                Zongji();
            }
        }

        private void gridViewIn_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colInvoiceZZDetailQuantity)
            {
                Zongji();
            }
        }
        private void Zongji()
        {
            if (this.action == "insert" || this.action == "update")
            {
                Model.InvoiceZZDetail detailIn = null;

                decimal zongji = 0;

                if (this.invoice.DetailsOut.Count > 0)
                {
                    foreach (Model.InvoiceZZDetail detail in this.invoice.DetailsOut)
                    {
                        zongji += detail.InvoiceZZDetailZongji.Value;
                    }
                }
                if (this.invoice.DetailsIn.Count > 0)
                {
                    detailIn = this.invoice.DetailsIn[0];
                }

                if (detailIn != null)
                {
                    detailIn.InvoiceZZDetailPrice = zongji;
                    detailIn.InvoiceZZDetailZongji = Convert.ToDecimal(detailIn.InvoiceZZDetailQuantity) * zongji;
                }
                this.gridControlIn.RefreshDataSource();
            }
        }

        private void gridViewIn_RowCountChanged(object sender, EventArgs e)
        {
            Zongji();
        }

        private void gridViewOut_RowCountChanged(object sender, EventArgs e)
        {
            Zongji();
        }

        private void gridViewOut_ShowingEditor(object sender, CancelEventArgs e)
        {
            #region Unit

            if (this.gridViewOut.FocusedColumn.Name == "gridColumn1")
            {
                if (this.gridViewOut.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridViewOut.GetRow(this.gridViewOut.FocusedRowHandle) as Model.InvoiceZZDetail).Product;

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

            #endregion

            #region Depot

            if (this.gridViewOut.FocusedColumn.Name == "colInvoiceOutDepot")
            {
                this.repositoryItemComboBox3.Items.Clear();
                foreach (Model.Depot depot in depotManager.Select())
                {
                    this.repositoryItemComboBox3.Items.Add(depot);
                }
            }

            #endregion
        }

        private void gridViewIn_ShowingEditor(object sender, CancelEventArgs e)
        {
            #region Unit

            if (this.gridViewIn.FocusedColumn.Name == "gridColumn2")
            {
                if (this.gridViewIn.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridViewIn.GetRow(this.gridViewIn.FocusedRowHandle) as Model.InvoiceZZDetail).Product;

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

            #endregion

            #region Depot

            if (this.gridViewIn.FocusedColumn.Name == "colInvoiceInDepot")
            {
                this.repositoryItemComboBox4.Items.Clear();
                foreach (Model.Depot depot in depotManager.Select())
                {
                    this.repositoryItemComboBox4.Items.Add(depot);
                }
            }

            #endregion
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControlOut, this.gridControlIn, this.textEditInvoiceId, this });
        }

        private void gridViewOut_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId1)
            {
                Model.InvoiceZZDetail detailOut = this.gridViewOut.GetRow(e.RowHandle) as Model.InvoiceZZDetail;
                if (detailOut != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detailOut.InvoiceZZDetailId = Guid.NewGuid().ToString();
                    detailOut.InvoiceZZDetailKind = "O";
                    detailOut.InvoiceZZDetailNote = "";
                    detailOut.InvoiceZZDetailPrice = 0;
                    detailOut.InvoiceZZDetailQuantity = 0;
                    detailOut.InvoiceZZDetailZongji = 0;
                    detailOut.Product = p;
                    detailOut.ProductId = p.ProductId;
                    //detailOut.InvoiceProductUnit = detailOut.Product.ProductBaseUnit;
                    this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detailOut);
                }
                this.gridControlOut.RefreshDataSource();
            }
        }

        private void gridViewOut_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.CanAdd(this.invoice.DetailsOut))
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        Model.InvoiceZZDetail detailOut = new Model.InvoiceZZDetail();
                        detailOut.InvoiceZZDetailId = Guid.NewGuid().ToString();
                        detailOut.InvoiceZZDetailKind = "O";
                        detailOut.InvoiceZZDetailNote = "";
                        detailOut.InvoiceZZDetailPrice = 0;
                        detailOut.InvoiceZZDetailQuantity = 0;
                        detailOut.InvoiceZZDetailZongji = 0;
                        detailOut.InvoiceProductUnit = "";
                        detailOut.Product = new Book.Model.Product();
                        this.invoice.DetailsOut.Add(detailOut);
                        this.bindingSourceOut.Position = this.bindingSourceOut.IndexOf(detailOut);
                    }
                }
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButtonRemoveOut.PerformClick();
                }
                this.gridControlOut.RefreshDataSource();
            }
        }

        private bool CanAdd(IList<Model.InvoiceZZDetail> list)
        {
            foreach (Model.InvoiceZZDetail detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }

        private void gridViewIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButtonRemoveIn.PerformClick();
                }
                this.gridControlIn.RefreshDataSource();
            }
        }

        private void gridViewIn_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colProductId)
            {
                Model.InvoiceZZDetail detailIn = this.gridViewIn.GetRow(e.RowHandle) as Model.InvoiceZZDetail;
                if (detailIn != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detailIn.InvoiceZZDetailId = Guid.NewGuid().ToString();
                    detailIn.InvoiceZZDetailKind = "I";
                    detailIn.InvoiceZZDetailNote = "";
                    detailIn.InvoiceZZDetailPrice = 0;
                    detailIn.InvoiceZZDetailQuantity = 0;
                    detailIn.InvoiceZZDetailZongji = 0;
                    detailIn.Product = p;
                    detailIn.ProductId = p.ProductId;
                    //detailIn.InvoiceProductUnit = detailIn.Product.ProductBaseUnit;
                    this.bindingSourceIn.Position = this.bindingSourceIn.IndexOf(detailIn);

                    this.simpleButtonAppendIn.Enabled = false;
                }
                this.gridControlIn.RefreshDataSource();
            }
        }

        private void dateEditInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}