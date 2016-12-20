using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;

namespace Book.UI.Invoices.XJ
{
    public partial class ProductEditForm : BaseEditForm
    {
      protected BL.InvoiceXJManager invoiceManager = new Book.BL.InvoiceXJManager();
        protected BL.InvoiceXJDetailManager invoiceXJDetailManager = new Book.BL.InvoiceXJDetailManager();
        protected BL.ProductManager ProductsManager = new Book.BL.ProductManager();
        protected BL.ProductUnitManager productUnitManager = new Book.BL.ProductUnitManager();
        private Model.InvoiceXJ invoice;

       // private int tags=0;//默认

        #region Constructors
        public ProductEditForm()
        {

            InitializeComponent();

            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl1));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));

            this.invalidValueExceptions.Add(Model.InvoiceXJ.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));

            this.action = "view";
            this.buttonEditCompany.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
        }

        public ProductEditForm(string invoiceId)
            : this()
        {
            this.invoice = this.invoiceManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";
        }

        public ProductEditForm(Model.InvoiceXJ invoicecj)
        {
            if (invoicecj == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = invoicecj;
            this.action = "update";
        }
        #endregion

        #region Form Load

        private void EditForm_Load(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Save

        protected override void Save(Helper.InvoiceStatus status)
        {
            this.invoice.ProductType = 1;//客户产品           
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            this.invoice.Customer = this.buttonEditCompany.EditValue as Model.Customer;
            this.invoice.InvoiceTotal = this.calcEditTotal.Value;
            this.invoice.InvoiceYxrq = this.dateEditInvoiceYxrq.DateTime.Date;

            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;

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

        #endregion

        #region simpleButton_Click

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            Model.Customer customer = this.buttonEditCompany.EditValue as Model.Customer;
            if (customer == null)
            {
                MessageBox.Show("請選則客戶！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.bindingSourceproduct.DataSource = this.ProductsManager.Select();
            Book.UI.Invoices.ChooseProductForm f = new  Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Product product = f.SelectedItem as Model.Product;
                Model.InvoiceXJDetail detail = new Book.Model.InvoiceXJDetail();
                detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                detail.Invoice = this.invoice;
                detail.Product = product;
                if(product!=null)
                detail.ProductId = product.ProductId;
                detail.InvoiceXJDetailQuantity = 1;
                detail.InvoiceXJDetailPrice = decimal.Zero;
                detail.InvoiceXJDetailMoney = decimal.Zero;
                detail.InvoiceXJDetailNote = "";
                detail.InvoiceProductUnit = product.MainUnit.CnName;
                this.invoice.Details.Add(detail);
                this.gridControl1.RefreshDataSource();

                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);                
            }
        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSource1.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoiceXJDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceXJDetail detail = new Model.InvoiceXJDetail();
                    detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                    detail.InvoiceXJDetailMoney = 0;
                    detail.InvoiceXJDetailNote = "";
                    detail.InvoiceXJDetailPrice = 0;
                    detail.InvoiceXJDetailQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    detail.Product= new Book.Model.Product();
                    this.invoice.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.Total();
            }
        }
        #endregion

        #region CellValueChange

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.colInvoiceXJDetailPrice || e.Column == this.colInvoiceXJDetailQuantity)
            {
                decimal price = decimal.Zero;
                decimal quantity = decimal.Zero;

                if (e.Column == this.colInvoiceXJDetailPrice)
                {
                    decimal.TryParse(e.Value.ToString(), out price);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceXJDetailQuantity).ToString(), out quantity);
                }
                if (e.Column == this.colInvoiceXJDetailQuantity)
                {
                    decimal.TryParse(e.Value.ToString(), out quantity);
                    decimal.TryParse(this.gridView1.GetRowCellValue(e.RowHandle, this.colInvoiceXJDetailPrice).ToString(), out price);
                }

                this.gridView1.SetRowCellValue(e.RowHandle, this.colInvoiceXJDetailMoney, price * quantity);

                this.Total();
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
            ChooseCustoms f = new ChooseCustoms();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                (sender as ButtonEdit).EditValue = f.SelectedItem;
            }
        }

        #endregion

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
                if (value is Model.InvoiceXJ)
                {
                    invoice = invoiceManager.Get((value as Model.InvoiceXJ).InvoiceId);
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

        private void Total()
        {
            decimal total = decimal.Zero;
            foreach (Model.InvoiceXJDetail detail in this.invoice.Details)
            {
                total += detail.InvoiceXJDetailMoney.Value;
            }
            this.calcEditTotal.EditValue = total;
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceXJ();
            this.invoice.InvoiceId = this.invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceXJDetail>();
            this.invoice.InvoiceYxrq = DateTime.Now;
            if (this.action == "insert")
            {
                Model.InvoiceXJDetail detail = new Model.InvoiceXJDetail();
                detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                detail.InvoiceXJDetailMoney = 0;
                detail.InvoiceXJDetailNote = "";
                detail.InvoiceXJDetailPrice = 0;
                detail.InvoiceXJDetailQuantity = 1;
                detail.InvoiceProductUnit = "";
                detail.Product = new Book.Model.Product();
                this.invoice.Details.Add(detail);
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
        }

        protected override void MoveNext()
        {
            Model.InvoiceXJ invoice = this.invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceXJ invoice = this.invoiceManager.GetPrev(this.invoice);
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
                this.invoice = new Book.Model.InvoiceXJ();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this.invoiceManager.Get(invoice.InvoiceId);
                else 
                {
                    this.bindingSourceproduct.DataSource = this.ProductsManager.Select();
                }
            }


            
                    //if(tags==1)
                    //{
                    //    this.colProduct.FieldName = "ProductId";
             
                    //    DevExpress.XtraEditors.Controls.LookUpColumnInfo a = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Id");
                    //    this.repositoryItemLookUpEdit1.Columns.Add(a);
                    //    DevExpress.XtraEditors.Controls.LookUpColumnInfo a = new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ProductName");
                    //    this.repositoryItemLookUpEdit1.Columns.Add(a);
                    //    DevExpress.XtraEditors.Controls.LookUpColumnInfo a=new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ProductId");                      
                    //    this.repositoryItemLookUpEdit1.Columns.Add(a);
                    //    a.Visible = false;
                        
                        
                    //}

            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.DateTime = this.invoice.InvoiceDate.Value;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;
            this.dateEditInvoiceYxrq.EditValue = this.invoice.InvoiceYxrq;

            this.bindingSource1.DataSource = this.invoice.Details;

            switch (this.action)
            {
                case "insert":
                    this.textEditInvoiceId.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.ReadOnly = false;
                    this.dateEditInvoiceDate.Properties.Buttons[0].Visible = true;

                  //  this.barButtonItemGeneral.Enabled = false;

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

                  //  this.barButtonItemGeneral.Enabled = false;

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

                    //this.barButtonItemGeneral.Enabled = true;

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

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string tag = (string)e.Item.Tag;
            switch (tag)
            {
                case "XO":
                    Operations.Open("invoices.xo.edit1", this.MdiParent, this.invoice);
                    break;
                case "XS":
                    Operations.Open("invoices.xs.edit1", this.MdiParent, this.invoice);
                    break;
                default:
                    break;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == this.colInvoiceProductUnit.Name)
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.CustomerProducts p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.InvoiceXJDetail).PrimaryKey;

                    this.repositoryItemComboBox1.Items.Clear();

                    IList<Model.ProductUnit> units = productUnitManager.Select(p.UnitGroupId);

                    foreach (Model.ProductUnit ut in units)
                    {
                        this.repositoryItemComboBox1.Items.Add(ut.CnName);
                    }                    
                }
            }
        }

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            this.Total();
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.gridControl1, this.textEditInvoiceId, this });
        }

        private bool CanAdd(IList<Model.InvoiceXJDetail> list)
        {
            foreach (Model.InvoiceXJDetail detail in list)
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
                        Model.InvoiceXJDetail detail = new Model.InvoiceXJDetail();
                        detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                        detail.InvoiceXJDetailMoney = 0;
                        detail.InvoiceXJDetailNote = "";
                        detail.InvoiceXJDetailPrice = 0;
                        detail.InvoiceXJDetailQuantity = 1;
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
            if (e.Column == this.colProductId)
            {
                Model.InvoiceXJDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.InvoiceXJDetail;
                if (detail != null)
                {
                    Model.Product p = ProductsManager.Get(e.Value.ToString());
                    detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                    detail.Invoice = this.invoice;
                    detail.InvoiceXJDetailNote = "";
                    detail.InvoiceXJDetailQuantity = 1;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.InvoiceProductUnit = p.MainUnit == null ? "" : p.MainUnit.CnName;
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.InvoiceXJDetail> details = this.bindingSource1.DataSource as IList<Model.InvoiceXJDetail>;
            //if (details == null || details.Count < 1) return;
            //Model.CustomerProducts product = details[e.ListSourceRowIndex].PrimaryKey;
            //if (product == null) return;
            //switch (e.Column.Name)
            //{
            //    case "colProductId":
            //        e.DisplayText = product.CustomerProductId;
            //        break;
            //}
        }

        private void buttonEditCompany_EditValueChanged(object sender, EventArgs e)
        {
        //    if (this.buttonEditCompany.EditValue != null)
        //    {
        //        this.bindingSourceproduct.DataSource = this.customerProductsManager.Select(this.buttonEditCompany.EditValue as Model.Customer);                
        //    }
        //    else 
        //    {                  
        //        this.bindingSourceproduct.Clear();
        //        IList<Model.InvoiceXJDetail> list = this.bindingSource1.DataSource as IList<Model.InvoiceXJDetail>;
        //        if (list == null)
        //            return;
        //        foreach (Model.InvoiceXJDetail detail in list)
        //        {
        //            detail.P = null;
        //            detail.PrimaryKey = null;
        //            detail.InvoiceProductUnit = "";
        //        }
        //        this.gridControl1.RefreshDataSource();
        //    }
        }
    }
}