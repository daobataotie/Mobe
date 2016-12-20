using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;

namespace Book.UI.Invoices.XJ
{
    public partial class EditForm : BaseEditForm
    {

        protected BL.CompanyManager companyManager = new Book.BL.CompanyManager();
        protected BL.ProductUnitManager productUnitManager = new Book.BL.ProductUnitManager();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        protected BL.ProcessCategoryManager processCategoryManager = new Book.BL.ProcessCategoryManager();
        protected BL.SupplierProductManager spm = new Book.BL.SupplierProductManager();
        //private int tags=0;//默认

        private BL.BomParentPartInfoManager _BOMParentInfoManager = new Book.BL.BomParentPartInfoManager();
        private BL.BomComponentInfoManager _BOMComponentInfoManager = new Book.BL.BomComponentInfoManager();
        private BL.BomPackageDetailsManager _BOMPackageDetailsManager = new Book.BL.BomPackageDetailsManager();
        private BL.InvoiceXJManager _invoiceManager = new Book.BL.InvoiceXJManager();
        private BL.InvoiceXJDetailManager _invoiceXJDetailManager = new Book.BL.InvoiceXJDetailManager();
        private BL.InvoiceXJProcessManager _invoiceXJProcessManager = new Book.BL.InvoiceXJProcessManager();
        private BL.InvoiceXJPackageDetailsManager _invoiceXJPackageDetailsManager = new Book.BL.InvoiceXJPackageDetailsManager();

        private IList<Model.BomComponentInfo> _BOMComponentInfoS;

        //---------------------------------------------------------------------------------------------------//
        private DataSet _ds = null;
        private Model.InvoiceXJ invoice;
        private int _MaxRelations = 0;
        private Hashtable _showColumns = null;
        private double _SumProduct, _SumProcess, _SumPackage;
        private bool _IsFromListXiangXi = false;

        public EditForm()
        {
            InitializeComponent();
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.DisplayFormat.FormatString = this.GetFormat(BL.V.SetDataFormat.XSDJXiao.Value);
            this.requireValueExceptions.Add("Id", new AA(Properties.Resources.RequireDataForId, this.textEditInvoiceId));
            this.requireValueExceptions.Add("Date", new AA(Properties.Resources.RequireDataOfInvoiceDate, this.dateEditInvoiceDate));
            this.requireValueExceptions.Add("Employee0", new AA(Properties.Resources.RequiredDataOfEmployee0, this.buttonEditEmployee));
            this.requireValueExceptions.Add("Details", new AA(Properties.Resources.RequireDataForDetails, this.gridControl2));
            this.requireValueExceptions.Add("Company", new AA(Properties.Resources.RequireDataForCompany, this.buttonEditCompany));

            this.invalidValueExceptions.Add(Model.InvoiceXJ.PROPERTY_INVOICEID, new AA(Properties.Resources.EntityExists, this.textEditInvoiceId));
            this.action = "view";
            this.buttonEditCompany.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.buttonEditEmployee.Choose = new ChooseEmployee();
            this.EmpAudit.Choose = new ChooseEmployee();

            if (this._showColumns == null)
            {
                this._showColumns = new Hashtable();
                this._showColumns.Add("IsChecked", "選擇");
                this._showColumns.Add("ProductName", "商品名稱");
                this._showColumns.Add("InvoiceXJDetailPrice", "單價");
                this._showColumns.Add("InvoiceXJDetailQuantity", "使用量");
                this._showColumns.Add("Unit", "單位");
                this._showColumns.Add("InvoiceXJDetailMoney", "成本");
                this._showColumns.Add("InvoiceXJDetailQuote", "成本報價");
            }

            this.gridView2.KeyDown += new KeyEventHandler(gridView2_KeyDown);
        }

        public EditForm(string invoiceId)
            : this()
        {
            this.invoice = this._invoiceManager.Get(invoiceId);
            if (this.invoice == null)
                throw new ArithmeticException("invoiceid");
            this.action = "update";

            this._IsFromListXiangXi = true;
        }

        public EditForm(Model.InvoiceXJ invoicecj)
            : this()
        {
            if (invoicecj == null)
                throw new ArithmeticException("invoiceid");
            this.invoice = this._invoiceManager.Get(invoicecj.InvoiceId);
            this.action = "update";
            this._IsFromListXiangXi = true;

        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bsProcSupplier.DataSource = new BL.SupplierManager().Select();
            this.bsProcProcessCategory.DataSource = this.processCategoryManager.Select();
            this.bindingSourceCompany.DataSource = this.companyManager.Select();
            //this.gv_0.OptionsBehavior.Editable = true;
        }

        protected override string tableCode()
        {
            return "InvoiceXJ";
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

        public override BaseListForm GetListForm()
        {
            ListForm f = new ListForm();
            if (DialogResult.OK == f.ShowDialog(this))
            {
                if (f._invoicexj != null)
                {
                    this.invoice = f._invoicexj;
                    this.Refresh();
                }
            }
            return null;
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
                    invoice = _invoiceManager.Get((value as Model.InvoiceXJ).InvoiceId);
                }
            }
        }

        protected override void TurnNull()
        {
            if (this.invoice == null)
                return;
            this.Delete();
            //this._invoiceManager.TurnNull(this.invoice.InvoiceId);
            this.invoice = this._invoiceManager.GetNext(this.invoice);
            if (this.invoice == null)
            {
                this.invoice = this._invoiceManager.GetLast();
            }
        }

        private void Total()
        {
            //decimal total = decimal.Zero;
            //foreach (Model.InvoiceXJDetail detail in this.invoice.Details)
            //{
            //    total += detail.InvoiceXJDetailMoney.Value;
            //}
            //this.calcEditTotal.EditValue = total;
        }

        protected override void Save(Helper.InvoiceStatus status)
        {
            //this.invoice.ProductType = 1;//客户产品           
            this.invoice.InvoiceStatus = (int)status;
            this.invoice.InvoiceId = this.textEditInvoiceId.Text;
            this.invoice.InvoiceDate = this.dateEditInvoiceDate.DateTime;
            this.invoice.Employee0 = this.buttonEditEmployee.EditValue as Model.Employee;
            this.invoice.InvoiceNote = this.textEditNote.Text;
            this.invoice.Customer = this.buttonEditCompany.EditValue as Model.Customer;
            //this.invoice.InvoiceTotal = this.calcEditTotal.Value;
            this.invoice.InvoiceYxrq = this.dateEditInvoiceYxrq.DateTime.Date;
            //this.invoice.Package = this.textEditPackage.Text;
            //this.invoice.PackagePrice = this.calcEditPackagePrice.Value;
            //this.invoice.Employee1 = BL.V.ActiveOperator;
            this.invoice.InvoiceLRTime = DateTime.Now;
            //this.invoice.Employee2 = BL.V.ActiveOperator;
            if (this.lookUpEditCompany.EditValue != null)
            {
                this.invoice.Company = this.companyManager.Get(this.lookUpEditCompany.EditValue.ToString());
                if (this.invoice.Company != null)
                    this.invoice.CompanyId = this.lookUpEditCompany.EditValue.ToString();
            }

            this.invoice.InvoiceTotal = this.spinZCBjinE.EditValue == null ? 0 : this.spinZCBjinE.Value;
            this.invoice.InvoiceBJTotal = this.spinZBJjinE.EditValue == null ? 0 : this.spinZBJjinE.Value;
            this.invoice.InvoiceProductTotal = this.spinSPCBjinE.EditValue == null ? 0 : this.spinSPCBjinE.Value;
            this.invoice.InvoiceBJProductTotal = this.spinSPBJjinE.EditValue == null ? 0 : this.spinSPBJjinE.Value;
            this.invoice.InvoicePackTotal = this.spinBCCBjinE.EditValue == null ? 0 : this.spinBCCBjinE.Value;
            this.invoice.InvoiceBJPackTotal = this.spinBCBJjinE.EditValue == null ? 0 : this.spinBCBJjinE.Value;
            this.invoice.InvoiceProcessTotal = this.spinJGCBjinE.EditValue == null ? 0 : this.spinJGCBjinE.Value;
            this.invoice.InvoiceBJProcessTotal = this.spinJGBJjinE.EditValue == null ? 0 : this.spinJGBJjinE.Value;
            this.invoice.GuanXiaoPro = this.spinGuanXiaoPro.EditValue == null ? 0 : this.spinGuanXiaoPro.Value;
            this.invoice.GuanXiaoPack = this.spinGuanXiaoPack.EditValue == null ? 0 : this.spinGuanXiaoPack.Value;
            this.invoice.GuanXiaoProc = this.spinGuanXiaoProc.EditValue == null ? 0 : this.spinGuanXiaoProc.Value;
            this.invoice.IsBaoJiaOver = this.Chk_IsBaoJiaOver.Checked;
            this.invoice.AuditState = this.saveAuditState;
            //商品型号
            this.invoice.ProductModel = this.txtProductPattern.Text;
            //手册
            this.invoice.HandbookId = this.txt_HandBookId.Text;
            this.invoice.HandbookProductId = this.txt_HandBookProductId.Text;

            if (!this.gv_0.PostEditor() || !this.gv_0.UpdateCurrentRow() || !this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow() || !this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
                return;

            //构建报价详细列表
            if (this._ds != null && this._ds.Tables.Count > 0)
            {
                if (this.invoice.Details == null)
                    this.invoice.Details = new List<Model.InvoiceXJDetail>();
                else
                    this.invoice.Details.Clear();

                foreach (DataTable dt in this._ds.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr.RowState == DataRowState.Deleted || dr.RowState == DataRowState.Detached)
                            continue;
                        Model.InvoiceXJDetail d = new Book.Model.InvoiceXJDetail();
                        d.InvoiceXJDetailId = Guid.NewGuid().ToString();
                        d.InvoiceId = this.invoice.InvoiceId;
                        //if (this.action == "insert")
                        //    d.ShamParentID = dr["PriamryKeyId"] == null ? "" : dr["PriamryKeyId"].ToString();
                        //else
                        //    d.ShamParentID = dr["InvoiceXJDetailId"] == null ? "" : dr["InvoiceXJDetailId"].ToString();
                        //if (dt.Columns.Contains("InvoiceXJDetailId"))
                        d.ShamParentID = dr["InvoiceXJDetailId"] == null ? "" : dr["InvoiceXJDetailId"].ToString();
                        //else
                        //    d.ShamParentID = dr["PriamryKeyId"] == null ? "" : dr["PriamryKeyId"].ToString();
                        if (dt.TableName == "Level0")
                        {
                            d.Inumber = Convert.ToInt32(dr["Inumber"]);
                            d.ParentId = "000";
                        }
                        else
                        {
                            d.ParentId = dr["ParentId"] == null ? "" : dr["ParentId"].ToString();
                            var res = this.invoice.Details.Where(ind => ind.ShamParentID == d.ParentId).First();
                            d.ParentId = (res as Model.InvoiceXJDetail).InvoiceXJDetailId;
                        }

                        d.InvoiceXJDetailPrice = decimal.Parse(objectToDouble(dr["InvoiceXJDetailPrice"]).ToString());
                        d.InvoiceXJDetailQuantity = objectToDouble(dr["InvoiceXJDetailQuantity"]);
                        d.InvoiceXJDetailMoney = decimal.Parse(objectToDouble(dr["InvoiceXJDetailMoney"]).ToString());
                        d.InvoiceXJDetailMoney2 = decimal.Parse(objectToDouble(dr["InvoiceXJDetailMoney2"]).ToString());
                        d.InvoiceXJDetailQuote = decimal.Parse(objectToDouble(dr["InvoiceXJDetailQuote"]).ToString());
                        d.InvoiceProductUnit = dr["Unit"] == null ? "" : dr["Unit"].ToString();
                        d.ProductName = dr["ProductName"] == null ? "" : dr["ProductName"].ToString();

                        this.invoice.Details.Add(d);
                    }
                }
            }

            switch (this.action)
            {
                case "insert":
                    // Workflowinsert wfinsert = new Workflowinsert();
                    this._invoiceManager.Insert(this.invoice);
                    //if (wfinsert.Checkwfbytablescode("客戶報價單"))
                    //{
                    //    wfinsert.insertwfrecord("客戶報價單", "客戶報價單", this.invoice.InvoiceId);
                    //}
                    break;
                case "update":
                    this._invoiceManager.Update(this.invoice);
                    break;
            }
        }

        protected override void Delete()
        {
            if (this.invoice == null) return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this._invoiceManager.Delete(this.invoice.InvoiceId);
        }

        protected override void AddNew()
        {
            this.invoice = new Model.InvoiceXJ();
            this.invoice.InvoiceId = this._invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.Details = new List<Model.InvoiceXJDetail>();
            this.invoice.DetailsProcess = new List<Model.InvoiceXJProcess>();
            this.invoice.DetailPackage = new List<Model.InvoiceXJPackageDetails>();
            this.invoice.InvoiceYxrq = DateTime.Now;

            //if (this.action == "insert")
            //{
            //    Model.InvoiceXJDetail detail = new Model.InvoiceXJDetail();
            //    detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
            //    detail.Inumber = this.invoice.Details.Count + 1;
            //    detail.InvoiceXJDetailMoney = 0;
            //    detail.InvoiceXJDetailNote = "";
            //    detail.InvoiceXJDetailPrice = 0;
            //    detail.InvoiceXJDetailQuantity = 1;
            //    detail.InvoiceProductUnit = "";
            //    // detail.PrimaryKey = new Book.Model.CustomerProducts();
            //    detail.Product = new Book.Model.Product();
            //    this.invoice.Details.Add(detail);
            //    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            //}
            this._invoiceXJProcessManager.Delete(this.invoice);
            this._ds = new DataSet();
            this.bindingSourceInvoiceXJDetail.DataSource = null;
            this.gridControl2.RefreshDataSource();
        }

        public override void Refresh()
        {
            if (this.invoice == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                    this.invoice = this._invoiceManager.Get(invoice.InvoiceId);
                //else 
                //{
                //    this.bindingSourceproduct.DataSource = this.productManager.Select();
                //    //this.bindingSourceproduct.DataSource = this.customerProductsManager.Select(this.invoice.Customer);
                //}
            }

            #region Note
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
            //if (this.action != "view" && this.invoice.DetailsProcess.Count == 0)
            //{
            //    Model.InvoiceXJProcess detail = new Book.Model.InvoiceXJProcess();
            //    detail.InvoiceXJProcessId = Guid.NewGuid().ToString();
            //    detail.InvoiceXJ = this.invoice;
            //    detail.InvoiceXJProcessPrice = 0;
            //    detail.Product = new Model.Product();
            //    this.invoice.DetailsProcess.Add(detail);
            //}
            #endregion

            this.lookUpEditCompany.EditValue = this.invoice.CompanyId;
            this.textEditInvoiceId.EditValue = this.invoice.InvoiceId;
            this.dateEditInvoiceDate.EditValue = this.invoice.InvoiceDate;
            this.buttonEditEmployee.EditValue = this.invoice.Employee0;
            this.buttonEditCompany.EditValue = this.invoice.Customer;
            this.BtnEditProduct.EditValue = this.invoice.Product;
            this.btnEditPackageProduct.EditValue = this.invoice.ProductPackage;
            //this.textEditAbstract.EditValue = this.invoice.InvoiceAbstract;
            this.textEditNote.EditValue = this.invoice.InvoiceNote;
            //this.calcEditTotal.EditValue = this.invoice.InvoiceTotal;
            this.dateEditInvoiceYxrq.EditValue = this.invoice.InvoiceYxrq;
            //this.textEditPackage.Text = this.invoice.Package;
            //this.calcEditPackagePrice.Value = this.invoice.PackagePrice.HasValue ? this.invoice.PackagePrice.Value : 0;

            this.EmpAudit.EditValue = this.invoice.AuditEmp;
            this.textEditAuditState.Text = this.invoice.AuditStateName;

            this.spinSPCBjinE.EditValue = this.invoice.InvoiceProductTotal;
            this.spinSPBJjinE.EditValue = this.invoice.InvoiceBJProductTotal;
            this.spinBCCBjinE.EditValue = this.invoice.InvoicePackTotal;
            this.spinBCBJjinE.EditValue = this.invoice.InvoiceBJPackTotal;
            this.spinJGCBjinE.EditValue = this.invoice.InvoiceProcessTotal;
            this.spinJGBJjinE.EditValue = this.invoice.InvoiceBJProcessTotal;
            this.spinZCBjinE.EditValue = this.invoice.InvoiceTotal;
            this.spinZBJjinE.EditValue = this.invoice.InvoiceBJTotal;
            this.spinGuanXiaoPro.EditValue = this.invoice.GuanXiaoPro;
            this.spinGuanXiaoPack.EditValue = this.invoice.GuanXiaoPack;
            this.spinGuanXiaoProc.EditValue = this.invoice.GuanXiaoProc;
            this.spinGuanXiaoZong.EditValue = this.invoice.GuanXiaoPro + this.invoice.GuanXiaoPack + this.invoice.GuanXiaoProc;
            this.Chk_IsBaoJiaOver.Checked = this.invoice.IsBaoJiaOver.HasValue && this.invoice.IsBaoJiaOver.Value ? true : false;
            //商品型号
            this.txtProductPattern.Text = this.invoice.ProductModel;

            //手册
            this.txt_HandBookId.EditValue = this.invoice.HandbookId;
            this.txt_HandBookProductId.EditValue = this.invoice.HandbookProductId;
            if (this.action == "view" || this._IsFromListXiangXi)
            {
                this._IsFromListXiangXi = false;
                //商品明细
                Hashtable result = this._invoiceXJDetailManager.getRecursiveInvoiceXJDetails(this.invoice.InvoiceId);

                //商品型号
                this.txtProductPattern.EditValue = this.invoice.ProductModel;

                if (result != null)
                {
                    this._ds = result["DS"] as DataSet;
                    if (this._ds != null && this._ds.Tables.Count > 0)
                    {
                        this._MaxRelations = int.Parse(result["MaxRelations"].ToString());
                        this.bindingSourceInvoiceXJDetail.DataSource = this._ds.Tables["Level0"];
                        this.gridControl2.RefreshDataSource();

                        this.GridViewAdjustment();
                        this.GridColumnAdjustment();
                    }
                }
            }
            this.bindingSourceProcess.DataSource = this.invoice.DetailsProcess;
            this.bindingSourcePackageDetails.DataSource = this.invoice.DetailPackage;
            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.barBtn_PrintCB.Enabled = false;
                    this.barBtn_PrintBJ.Enabled = false;
                    this.bar_btnCopyInvoice.Enabled = false;
                    break;
                case "update":
                    this.barBtn_PrintCB.Enabled = false;
                    this.barBtn_PrintBJ.Enabled = false;
                    this.bar_btnCopyInvoice.Enabled = false;
                    break;
                case "view":
                    this.barBtn_PrintCB.Enabled = true;
                    this.barBtn_PrintBJ.Enabled = true;
                    this.bar_btnCopyInvoice.Enabled = true;
                    break;
                default:
                    break;
            }
            this.textEditInvoiceId.Properties.ReadOnly = true;
            this.gv_0.OptionsBehavior.Editable = true;
            this.gridView1.OptionsBehavior.Editable = true;
            this.gridView2.OptionsBehavior.Editable = true;
            this.spinZBJjinE.Properties.ReadOnly = true;
            this.spinZCBjinE.Properties.ReadOnly = true;
            this.spinGuanXiaoZong.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.InvoiceXJ invoice = this._invoiceManager.GetNext(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this._invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MovePrev()
        {
            Model.InvoiceXJ invoice = this._invoiceManager.GetPrev(this.invoice);
            if (invoice == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.invoice = this._invoiceManager.Get(invoice.InvoiceId);
        }

        protected override void MoveFirst()
        {
            this.invoice = this._invoiceManager.Get(this._invoiceManager.GetFirst() == null ? "" : this._invoiceManager.GetFirst().InvoiceId);
        }

        protected override void MoveLast()
        {
            this.invoice = this._invoiceManager.Get(this._invoiceManager.GetLast() == null ? "" : this._invoiceManager.GetLast().InvoiceId);
        }

        protected override bool HasRows()
        {
            return this._invoiceManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._invoiceManager.HasRowsAfter(this.invoice);
        }

        protected override bool HasRowsPrev()
        {
            return this._invoiceManager.HasRowsBefore(this.invoice);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new R01(this.invoice.InvoiceId);
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
            }
        }

        private void dateEditInvoiceDate_Leave(object sender, EventArgs e)
        {
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this._invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
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

        private void gridView1_RowCountChanged(object sender, EventArgs e)
        {
            /*
            this.Total();
            IList<Model.Product> list = new List<Model.Product>();
            foreach (Model.InvoiceXJDetail detail in this.invoice.Details)
            {
                list.Add(detail.Product);
            }
            this.bindingSourceProcessProduct.DataSource = list;
             */
        }

        private bool CanAdd(IList<Model.InvoiceXJDetail> list)
        {
            foreach (Model.InvoiceXJDetail detail in list)
            {
                // if (detail.PrimaryKey == null || string.IsNullOrEmpty(detail.PrimaryKey.PrimaryKeyId))
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            /*
            Model.InvoiceXJDetail detail = this.bindingSource1.Current as Model.InvoiceXJDetail;
            if (detail == null || detail.ProductId == null) return;
            ChooseProcessForm f = new ChooseProcessForm(detail, this.invoice);
            if (f.ShowDialog() == DialogResult.OK)
            {
                invoice.DetailsProcess.Clear();
                invoice.DetailsProcess = this.invoiceXJProcessManager.Select(this.invoice);
                this.bindingSourceProcess.DataSource = invoice.DetailsProcess;
            }
             */
        }

        private void dateEditInvoiceDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.action == "insert") { this.textEditInvoiceId.EditValue = this._invoiceManager.GetNewId(this.dateEditInvoiceDate.DateTime); }
        }

        //选择主件商品
        private void BtnEditProduct_Click(object sender, EventArgs e)
        {
            if (this.action != "view")
            {
                Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    this.invoice.Product = f.SelectedItem as Model.Product;
                    if (this.invoice.Product != null)
                    {
                        this.invoice.ProductId = this.invoice.Product.ProductId;
                        this.BtnEditProduct.EditValue = this.invoice.Product;

                        //Clear
                        _MaxRelations = 0;
                        this._ds.Clear();
                        this._ds.Relations.Clear();
                        this.gridControl2.LevelTree.Nodes.Clear();
                        this.gridControl2.ViewCollection.Clear();

                        //构建报价商品明细层级
                        Hashtable result = this._invoiceXJDetailManager.getRecursiveBOM(this.invoice.ProductId);
                        this._MaxRelations = int.Parse(result["MaxRelations"].ToString());
                        this._ds = result["DS"] as DataSet;
                        if (this._ds != null && this._ds.Tables.Count > 0)
                        {

                            foreach (DataTable RecurDT in this._ds.Tables)
                            {
                                foreach (DataRow RecurDR in RecurDT.Rows)
                                {
                                    RecurDR["InvoiceXJDetailPrice"] = BL.SupplierProductManager.CountPrice(RecurDR["InvoiceXJDetailPriceRange"].ToString(), 1);
                                }
                            }
                            this.bindingSourceInvoiceXJDetail.DataSource = this._ds.Tables["Level0"];
                            this.gridControl2.RefreshDataSource();
                            this.GridViewAdjustment();
                            this.GridColumnAdjustment();

                            //默认首先计算一次
                            this.CalcInvoiceXJDetail(this._MaxRelations);
                        }

                        //加工

                        //包材
                        if (this._ds != null && this._ds.Tables.Count > 0)
                        {
                            string bomid = string.Empty;    //BOM头编号,用于查找包装
                            IList<Model.BomPackageDetails> tempBOMPackageDetails = null;
                            Model.InvoiceXJPackageDetails xpd = null;
                            IList<string> TemphasBOMId = new List<string>();    //暂时记录已查询包材
                            this.invoice.DetailPackage.Clear();     //首先清空包材列表

                            foreach (DataTable dt in this._ds.Tables)
                            {
                                if (dt.Rows != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        bomid = dr["BomId"] == null ? "" : dr["BomId"].ToString();
                                        if (!string.IsNullOrEmpty(bomid))
                                        {
                                            if (TemphasBOMId.Contains(bomid))
                                                continue;
                                            //包材
                                            tempBOMPackageDetails = this._BOMPackageDetailsManager.Select(bomid);
                                            if (tempBOMPackageDetails != null)
                                            {
                                                foreach (Model.BomPackageDetails bompackage in tempBOMPackageDetails)
                                                {
                                                    xpd = new Book.Model.InvoiceXJPackageDetails();
                                                    xpd.InvoiceXJPackageDetailsId = Guid.NewGuid().ToString();
                                                    xpd.Inumber = this.invoice.DetailPackage.Count + 1;
                                                    xpd.InvoiceId = this.invoice.InvoiceId;
                                                    //供应商& 加工需要获得
                                                    xpd.Supplier = bompackage.Product.Supplier;
                                                    if (xpd.Supplier != null)
                                                        xpd.SupplierId = xpd.Supplier.SupplierId;
                                                    if (bompackage.Product.OutSourcing.HasValue && bompackage.Product.OutSourcing.Value)
                                                        xpd.InvoiceXJPackageDetailsType = "採購";
                                                    if (bompackage.Product.TrustOut.HasValue && bompackage.Product.TrustOut.Value)
                                                    {
                                                        xpd.InvoiceXJPackageDetailsType = "委外";
                                                        //if (bompackage.Product.IsQiangHua.HasValue && bompackage.Product.IsQiangHua.Value)
                                                        //    xpd. = "";
                                                    }
                                                    if (bompackage.Product.HomeMade.HasValue && bompackage.Product.HomeMade.Value)
                                                    {
                                                        xpd.InvoiceXJPackageDetailsType = "自製";
                                                    }
                                                    //xpd.Product = bompackage.Product;
                                                    //xpd.ProductId = bompackage.ProductId;
                                                    xpd.ProductName = bompackage.Product.ToString();
                                                    xpd.InvoiceXJPackageDetailsQuantity = bompackage.UseQuantity;
                                                    xpd.Quantity = bompackage.Quantity;
                                                    xpd.UseQuantity = bompackage.UseQuantity;
                                                    if (bompackage.Product.Supplier != null)
                                                        xpd.PriceAndRange = this.spm.GetPriceRangeForSupAndProduct(bompackage.Product.SupplierId, bompackage.ProductId);
                                                    xpd.PackagePrice = BL.SupplierProductManager.CountPrice(xpd.PriceAndRange, 1);

                                                    xpd.ConsumeRate = bompackage.ConsumeRate;
                                                    xpd.EffectsDate = DateTime.Now;
                                                    xpd.ExpiringDate = DateTime.Now.AddYears(10);
                                                    xpd.Description = bompackage.Product == null ? "" : bompackage.Product.ProductDescription;
                                                    this.invoice.DetailPackage.Add(xpd);
                                                }
                                            }
                                            TemphasBOMId.Add(bomid);
                                        }
                                    }
                                }
                            }
                            this.bindingSourcePackageDetails.ResetBindings(false);
                            this.bindingSourcePackageDetails.DataSource = this.invoice.DetailPackage;
                            this.gridControl3.RefreshDataSource();
                        }
                    }
                }
                f.Dispose();
                System.GC.Collect();
            }
        }

        private void GridViewAdjustment()
        {
            GridView[] gvs = new GridView[this._MaxRelations + 1];
            ColorMaker[] colorMakers = new ColorMaker[10];
            string[] options = new String[] {
												"EvenRow",
												"OddRow",
												"FocusedRow",
												"FocusedCell",
												"GroupRow",
												"HeaderPanel",
												"GroupPanel",
												"HorzLine",
												"VertLine"};

            gvs[0] = gv_0;

            colorMakers[0] = new ColorMaker(Color.DimGray, Color.WhiteSmoke);
            colorMakers[1] = new ColorMaker(Color.DarkCyan, Color.LightCyan);
            colorMakers[2] = new ColorMaker(Color.DarkGoldenrod, Color.LightGoldenrodYellow);
            colorMakers[3] = new ColorMaker(Color.Navy, Color.Lavender);
            colorMakers[4] = new ColorMaker(Color.LightGreen, Color.LightCyan);
            colorMakers[5] = new ColorMaker(Color.DarkSalmon, Color.LightGoldenrodYellow);
            colorMakers[6] = new ColorMaker(Color.Tan, Color.WhiteSmoke);
            colorMakers[7] = new ColorMaker(Color.OliveDrab, Color.LightCyan);
            colorMakers[8] = new ColorMaker(Color.Silver, Color.Lavender);
            colorMakers[9] = new ColorMaker(Color.Pink, Color.LightGoldenrodYellow);


            for (int i = 0; i < this._MaxRelations; i++)
            {
                string prefix = string.Empty;
                int basefontsize = 8, backColor = 100, foreColor = 0, font = 0;
                FontStyle fstyle = FontStyle.Regular;

                if (i > 0)
                {
                    gvs[i] = new GridView(this.gridControl2);
                    gvs[i].Name = "gv_" + i.ToString();
                    gvs[i].OptionsDetail.ShowDetailTabs = false;
                    gvs[i].OptionsView.ShowGroupPanel = false;
                    prefix = this._ds.Relations[i - 1].RelationName;
                }

                foreach (string option in options)
                {
                    switch (option)
                    {
                        case "EvenRow":
                            backColor = 100;
                            foreColor = 0;
                            break;
                        case "OddRow":
                            backColor = 90;
                            foreColor = 0;
                            break;
                        case "FocusedRow":
                            backColor = 10;
                            foreColor = 90;
                            fstyle = FontStyle.Bold;
                            break;
                        case "FocusedCell":
                            backColor = 200;
                            foreColor = 0;
                            break;
                        case "GroupRow":
                            backColor = 60;
                            foreColor = 100;
                            fstyle = FontStyle.Italic;
                            break;
                        case "HeaderPanel":
                            backColor = 30;
                            foreColor = 100;
                            font = 1;
                            fstyle = FontStyle.Bold;
                            break;
                        case "GroupPanel":
                            backColor = 0;
                            foreColor = 100;
                            font = 2;
                            fstyle = FontStyle.Bold;
                            break;
                        case "HorzLine":
                        case "VertLine":
                            backColor = 0;
                            foreColor = 0;
                            font = 0;
                            break;
                    }
                    gvs[i].Appearance.GetAppearance(option).BackColor = colorMakers[i].ProduceColor(backColor);
                    gvs[i].Appearance.GetAppearance(option).BorderColor = colorMakers[i].ProduceColor(backColor);
                    gvs[i].Appearance.GetAppearance(option).ForeColor = colorMakers[i].ProduceColor(foreColor);
                    gvs[i].Appearance.GetAppearance(option).Font = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, basefontsize + font, fstyle);
                }

                gvs[i].CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(GridTest_CellValueChanged);
                gvs[i].CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(EditForm_CellValueChanging);
                gvs[i].KeyDown -= GV_0_KeyDown;
                gvs[i].KeyDown += GV_0_KeyDown;
                gvs[i].OptionsDetail.ShowDetailTabs = false;
                gvs[i].PaintStyleName = "MixedXP";

                switch (prefix)
                {
                    case "Rel0":
                        gridControl2.LevelTree.Nodes.Add(prefix, gvs[i]);
                        break;
                    case "Rel1":
                        gridControl2.LevelTree.Nodes["Rel0"].Nodes.Add(prefix, gvs[i]);
                        break;
                    case "Rel2":
                        gridControl2.LevelTree.Nodes["Rel0"].Nodes["Rel1"].Nodes.Add(prefix, gvs[i]);
                        break;
                    case "Rel3":
                        gridControl2.LevelTree.Nodes["Rel0"].Nodes["Rel1"].Nodes["Rel2"].Nodes.Add(prefix, gvs[i]);
                        break;
                    case "Rel4":
                        gridControl2.LevelTree.Nodes["Rel0"].Nodes["Rel1"].Nodes["Rel2"].Nodes["Rel3"].Nodes.Add(prefix, gvs[i]);
                        break;
                    case "Rel5":
                        gridControl2.LevelTree.Nodes["Rel0"].Nodes["Rel1"].Nodes["Rel2"].Nodes["Rel3"].Nodes["Rel4"].Nodes.Add(prefix, gvs[i]);
                        break;
                    case "Rel6":
                        gridControl2.LevelTree.Nodes["Rel0"].Nodes["Rel1"].Nodes["Rel2"].Nodes["Rel3"].Nodes["Rel4"].Nodes["Rel5"].Nodes.Add(prefix, gvs[i]);
                        break;
                    case "Rel7":
                        gridControl2.LevelTree.Nodes["Rel0"].Nodes["Rel1"].Nodes["Rel2"].Nodes["Rel3"].Nodes["Rel4"].Nodes["Rel5"].Nodes["Rel6"].Nodes.Add(prefix, gvs[i]);
                        break;
                }
            }
        }

        private void GridColumnAdjustment()
        {
            gridControl2.BeginUpdate();
            try
            {
                GridView gv;
                for (int i = 0; i < this._MaxRelations - 1; i++)
                {
                    gv = this.BuildLevelTreenode(i);
                    this.BuildGridClumns(gv, this._ds.Tables["Level" + i.ToString()]);
                }
            }
            finally
            {
                gridControl2.EndUpdate();
            }
        }

        private GridView BuildLevelTreenode(int max)
        {
            GridLevelNodeCollection gns = gridControl2.LevelTree.Nodes;
            GridLevelNode gn = null;

            for (int j = 0; j <= max; j++)
            {

                if (gns != null && gns.Count > 0)
                {
                    gn = gns["Rel" + j.ToString()];
                    gns = gn.Nodes;
                }
            }

            return gn.LevelTemplate as GridView;
        }

        private void BuildGridClumns(GridView gv, DataTable dt)
        {
            GridColumn col;
            foreach (DataColumn c in dt.Columns)
            {
                col = gv.Columns.Add();
                col.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                col.FieldName = c.ColumnName;
                if (col.FieldName == "IsChecked")
                    col.ColumnEdit = this.repositoryItemCheckEdit3;
                col.Width = 500;
                col.Caption = this._showColumns.ContainsKey(col.FieldName) ? this._showColumns[col.FieldName].ToString() : "";
                col.VisibleIndex = c.Ordinal;

                if (this._showColumns.ContainsKey(col.FieldName))
                    col.Visible = true;
                else
                    col.Visible = false;
            }
        }

        //更改层级显示
        private void CheckLayer(int tabindex, string ParentsId, bool IsCheckStatus)
        {
            DataTable UpdateDT = this._ds.Tables["Level" + (tabindex + 1).ToString()];
            if (UpdateDT == null || UpdateDT.Rows.Count == 0)
                return;
            StringBuilder SbSonParIds = new StringBuilder();

            DataRow[] UpRows = UpdateDT.Select(" ParentId in (" + ParentsId + ")");
            for (int i = 0; i < UpRows.Length; i++)
            {
                SbSonParIds.Append("'" + UpRows[i]["InvoiceXJDetailId"].ToString() + "',");
                UpRows[i]["IsChecked"] = IsCheckStatus;
            }
            if (string.IsNullOrEmpty(SbSonParIds.ToString()))
                return;

            string SonParIds = SbSonParIds.ToString().Substring(0, SbSonParIds.ToString().Length - 1);
            this.CheckLayer(tabindex + 1, SonParIds, IsCheckStatus);
        }

        // 商品明细增加已有商品  "+"
        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            if (this._ds.Tables.Count == 0)
            {
                MessageBox.Show("請先添加參考商品！", this.Text, MessageBoxButtons.OK);
                return;
            }
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                //if (this._ds == null || this._ds.Tables.Count == 0 || this._ds.Tables[0].Columns.Count == 0)
                //{
                //    DataTable indt = new DataTable();
                //    indt.Columns.Add("InvoiceDetailsId", typeof(string));
                //    indt.Columns.Add("BomId", typeof(string));
                //    indt.Columns.Add("ProductId", typeof(string));
                //    indt.Columns.Add("UseQuantity", typeof(int));
                //    indt.Columns.Add("Unit", typeof(string));
                //    indt.Columns.Add("ProductPrice", typeof(decimal));
                //    indt.Columns.Add("ProductName", typeof(string));
                //    indt.Columns.Add("ParentId", typeof(string));
                //    indt.Columns.Add("Const", typeof(decimal));
                //    indt.Columns.Add("QuoteConst", typeof(decimal));
                //    this._ds.Tables.Add(indt);
                //}
                //if (Invoices.ChooseProductForm.ProductList == null || Invoices.ChooseProductForm.ProductList.Count == 0)
                //{
                //    Model.Product p = f.SelectedItem as Model.Product;

                //    DataRow dr = this._ds.Tables[0].NewRow();
                //    dr["InvoiceXJDetailId"] = Guid.NewGuid().ToString();
                //    dr["InvoiceXJDetailPrice"] = 0;
                //    dr["InvoiceXJDetailPriceRange"] = this.spm.GetPriceRangeForSupAndProduct(p.SupplierId, p.ProductId);
                //    //dr["ProductId"] = p.ProductId;
                //    dr["ProductName"] = p.ToString();
                //    dr["Unit"] = p.BuyUnit == null ? "" : p.BuyUnit.ToString();
                //    dr["ParentId"] = "000";
                //    this._ds.Tables[0].Rows.Add(dr);
                //}
                if (Invoices.ChooseProductForm.ProductList != null && Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    Model.Product p = f.SelectedItem as Model.Product;

                    DataRow dr = this._ds.Tables[0].NewRow();
                    dr["InvoiceXJDetailId"] = Guid.NewGuid().ToString();
                    dr["InvoiceXJDetailPrice"] = 0;
                    //dr["ProductId"] = p.ProductId;
                    dr["ProductName"] = p.ToString();
                    dr["Unit"] = p.BuyUnit == null ? "" : p.BuyUnit.ToString();
                    dr["ParentId"] = "000";
                    this._ds.Tables[0].Rows.Add(dr);
                }
                this.gridControl2.RefreshDataSource();
            }

            #region note
            //Model.Customer customer = this.buttonEditCompany.EditValue as Model.Customer;
            //if (customer == null)
            //{
            //    MessageBox.Show("請選則客戶！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //   this.bindingSourceproduct.DataSource = this.productManager.Select(customer);// this.customerProductsManager.Select(customer);
            //Book.UI.Settings.BasicData.Customs.ChooseCustomerProductForm f = new Book.UI.Settings.BasicData.Customs.ChooseCustomerProductForm(customer);

            /*
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {

                if (this.invoice.Details.Count > 0 && string.IsNullOrEmpty(this.invoice.Details[0].ProductId))
                    this.invoice.Details.RemoveAt(0);
                Model.InvoiceXJDetail detail = null;
                if (Book.UI.Invoices.ChooseProductForm.ProductList != null || Book.UI.Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in Book.UI.Invoices.ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.InvoiceXJDetail();
                        detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                        detail.Inumber = this.invoice.Details.Count + 1;
                        detail.Invoice = this.invoice;
                        detail.Product = product;
                        if (product != null)
                            detail.ProductId = product.ProductId;
                        detail.InvoiceXJDetailQuantity = 1;
                        detail.InvoiceXJDetailPrice = decimal.Zero;
                        detail.InvoiceXJDetailMoney = decimal.Zero;
                        detail.InvoiceAllowance = 0;
                        detail.InvoiceXJDetailNote = "";
                        detail.InvoiceProductUnit = product.MainUnit == null ? "" : product.MainUnit.CnName;
                        this.invoice.Details.Add(detail);

                    }
                }
                if (Book.UI.Invoices.ChooseProductForm.ProductList == null || Book.UI.Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.InvoiceXJDetail();
                    Model.Product product = f.SelectedItem as Model.Product;
                    detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = this.invoice.Details.Count + 1;
                    detail.Invoice = this.invoice;
                    detail.Product = product;
                    if (product != null)
                        detail.ProductId = product.ProductId;
                    detail.InvoiceXJDetailQuantity = 1;
                    detail.InvoiceXJDetailPrice = decimal.Zero;
                    detail.InvoiceXJDetailMoney = decimal.Zero;
                    detail.InvoiceAllowance = 0;
                    detail.InvoiceXJDetailNote = "";
                    detail.InvoiceProductUnit = product.MainUnit == null ? "" : product.MainUnit.CnName;
                    this.invoice.Details.Add(detail);
                }

                this.gridControl2.RefreshDataSource();
                this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
            }
            f.Dispose();
            System.GC.Collect();
             */
            #endregion
        }

        // "-"
        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            #region note
            // XtraForm3 f = new XtraForm3();
            // f.Show();
            /*
            if (this.bindingSource1.Current != null)
            {
                this.invoice.Details.Remove(this.bindingSource1.Current as Book.Model.InvoiceXJDetail);
                if (this.invoice.Details.Count == 0)
                {
                    Model.InvoiceXJDetail detail = new Model.InvoiceXJDetail();
                    detail.InvoiceXJDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = this.invoice.Details.Count + 1;
                    detail.InvoiceXJDetailMoney = 0;
                    detail.InvoiceXJDetailNote = "";
                    detail.InvoiceXJDetailPrice = 0;
                    detail.InvoiceXJDetailQuantity = 0;
                    detail.InvoiceProductUnit = "";
                    //detail.PrimaryKey = new Book.Model.CustomerProducts();
                    detail.Product = new Book.Model.Product();

                    this.invoice.Details.Add(detail);
                    this.bindingSource1.Position = this.bindingSource1.IndexOf(detail);
                }
                this.gridControl2.RefreshDataSource();
                this.Total();
            }
             */
            #endregion
        }

        //底部金额总和计算
        private void FooterSpin_EditValueChanged(object sender, EventArgs e)
        {
            //CB
            decimal cb1 = this.ReturSpinCalcValue(this.spinSPCBjinE.EditValue);
            decimal cb2 = this.ReturSpinCalcValue(this.spinBCCBjinE.EditValue);
            decimal cb3 = this.ReturSpinCalcValue(this.spinJGCBjinE.EditValue);
            this.spinZCBjinE.Value = cb1 + cb2 + cb3;
            //BJ
            decimal bj1 = this.ReturSpinCalcValue(this.spinSPBJjinE.EditValue);
            decimal bj2 = this.ReturSpinCalcValue(this.spinBCBJjinE.EditValue);
            decimal bj3 = this.ReturSpinCalcValue(this.spinJGBJjinE.EditValue);
            this.spinZBJjinE.Value = bj1 + bj2 + bj3;
            //GX
            decimal gx1 = this.ReturSpinCalcValue(this.spinGuanXiaoPro.EditValue);
            decimal gx2 = this.ReturSpinCalcValue(this.spinGuanXiaoPack.EditValue);
            decimal gx3 = this.ReturSpinCalcValue(this.spinGuanXiaoProc.EditValue);
            this.spinGuanXiaoZong.Value = gx1 + gx2 + gx3;
        }

        private decimal ReturSpinCalcValue(object oValue)
        {
            if (oValue == null || string.IsNullOrEmpty(oValue.ToString()))
                return 0;
            return decimal.Parse(oValue.ToString());
        }

        //---------------------------------------------层级GridView事件----------------------------//
        private void EditForm_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            DataTable CurDT = null;
            if ((view.DataSource as DataView) != null)
                CurDT = (view.DataSource as DataView).Table;
            else if ((view.DataSource as BindingSource) != null)
            {
                CurDT = (view.DataSource as BindingSource).DataSource as DataTable;
            }
            if (e.Column.FieldName == "IsChecked")
            {
                int tabindex = int.Parse(CurDT.TableName.Replace("Level", ""));
                string ParentsId = "'" + view.GetFocusedDataRow()["InvoiceXJDetailId"].ToString() + "'";
                this.CheckLayer(tabindex, ParentsId, bool.Parse(e.Value.ToString()));
                return;
            }
        }

        private void GridTest_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;
            DataTable CurDT = null;
            if ((view.DataSource as DataView) != null)
                CurDT = (view.DataSource as DataView).Table;
            else if ((view.DataSource as BindingSource) != null)
            {
                CurDT = (view.DataSource as BindingSource).DataSource as DataTable;
            }

            string CurDTName = CurDT.TableName;
            int tableLevel = string.IsNullOrEmpty(CurDTName.Substring(CurDTName.Length - 1, 1)) ? 0 : int.Parse(CurDTName.Substring(CurDTName.Length - 1, 1));

            this.CalcInvoiceXJDetail(tableLevel);
        }

        private void CalcInvoiceXJDetail(int tableLevel)
        {
            double HeJi = 0;      //成本一
            double Heji2 = 0;     //成本二
            double QuoteHeJI = 0; //报价合计

            //记录相同父行数据父编号
            List<string> parentsId;

            for (int i = tableLevel; i >= 0; i--)
            {
                //Clear Parent Value
                //if (i > 0)
                //{
                //    DataTable pDT = this._ds.Tables["Level" + (i - 1).ToString()];
                //    foreach (DataRow dr in pDT.Rows)
                //    {
                //        dr["InvoiceXJDetailPrice"] = 0;
                //        dr["InvoiceXJDetailMoney"] = 0;
                //        dr["InvoiceXJDetailMoney2"] = 0;
                //        dr["InvoiceXJDetailQuote"] = 0;
                //    }
                //}
                parentsId = new List<string>();

                foreach (DataRow dr in this._ds.Tables[i].Rows)
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        double _price = objectToDouble(dr["InvoiceXJDetailPrice"]);
                        double _useQuantity = objectToDouble(dr["InvoiceXJDetailQuantity"]);
                        double _const = objectToDouble(dr["InvoiceXJDetailMoney"]);
                        double _QuoteConst = objectToDouble(dr["InvoiceXJDetailQuote"]);
                        dr["InvoiceXJDetailMoney"] = _price * _useQuantity;
                        //update Parent
                        if (i > 0)
                        {
                            string _parentId = dr["ParentId"].ToString();
                            DataRow _parentROW = null;
                            //if (this.action == "insert")
                            //    _parentROW = this._ds.Tables["Level" + (i - 1).ToString()].Select(" 1=1  AND PriamryKeyId = '" + _parentId + "'")[0];
                            //else
                            _parentROW = this._ds.Tables["Level" + (i - 1).ToString()].Select(" 1=1  AND InvoiceXJDetailId = '" + _parentId + "'")[0];
                            if (!parentsId.Contains(_parentId))
                            {
                                _parentROW["InvoiceXJDetailPrice"] = 0;
                                parentsId.Add(_parentId);
                            }

                            double _parentConst = objectToDouble(_parentROW["InvoiceXJDetailPrice"]);
                            double _parentUseQuantity = objectToDouble(_parentROW["InvoiceXJDetailQuantity"]);
                            double _parentQuoteConst = objectToDouble(_parentROW["InvoiceXJDetailQuote"]);
                            _parentROW["InvoiceXJDetailPrice"] = _parentConst + (_price * _useQuantity);       //更新父级合计
                            _parentROW["InvoiceXJDetailQuote"] = _parentQuoteConst + _QuoteConst * _parentUseQuantity;                //更新父级报价合计
                        }
                    }
                }
            }

            foreach (DataRow dr in this._ds.Tables["Level0"].Rows)
            {
                HeJi += objectToDouble(dr["InvoiceXJDetailMoney"]);
                QuoteHeJI += objectToDouble(dr["InvoiceXJDetailQuote"]);
            }

            this.spinZCBjinE.EditValue = Convert.ToDouble(this.spinZCBjinE.EditValue) - Convert.ToDouble(this.spinSPCBjinE.EditValue) + HeJi;
            this.spinZBJjinE.EditValue = Convert.ToDouble(this.spinZBJjinE.EditValue) - Convert.ToDouble(this.spinSPBJjinE.EditValue) + QuoteHeJI;

            this.spinSPCBjinE.EditValue = HeJi;
            this.spinSPBJjinE.EditValue = QuoteHeJI;
            //this._SumProduct = HeJi;        //记录商品成本合计
        }

        DataRow _CopyTempDr = null;
        void GV_0_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                GridView v = sender as GridView;
                DataTable o = null;
                if ((v.DataSource as DataView) != null)
                    o = (v.DataSource as DataView).Table;
                else if ((v.DataSource as BindingSource) != null)
                {
                    o = (v.DataSource as BindingSource).DataSource as DataTable;
                }
                DataRow dr = v.GetFocusedDataRow();

                if (v != null)
                {
                    switch (e.KeyData)
                    {
                        case Keys.Enter:
                            if (o != null)
                            {
                                DataRow addR = o.NewRow();
                                //if (this.action == "insert")
                                //if (addR["PriamryKeyId"] != null)
                                //    addR["PriamryKeyId"] = Guid.NewGuid().ToString();
                                //else
                                addR["Inumber"] = "0";
                                addR["InvoiceXJDetailId"] = Guid.NewGuid().ToString();
                                addR["ParentId"] = dr["ParentId"].ToString();
                                o.Rows.Add(addR);
                            }
                            break;
                        case Keys.Delete:
                            int tabindex = int.Parse(dr.Table.TableName.Replace("Level", ""));
                            string TempInvoiceXjDetailId = dr["InvoiceXJDetailId"].ToString();
                            DataTable SonDT = this._ds.Tables["Level" + (tabindex + 1).ToString()];
                            if (SonDT != null && SonDT.Rows.Count > 0)
                            {
                                DataRow[] sonList = SonDT.Select("ParentId = '" + TempInvoiceXjDetailId + "'");
                                if (sonList == null || sonList.Length == 0)
                                {
                                    if (dr != null)
                                    {
                                        o.Rows.Remove(dr);
                                    }
                                }
                                else
                                {
                                    DialogResult dres = MessageBox.Show("要保留層級關係,不全部刪除!", "提示", MessageBoxButtons.YesNoCancel);
                                    switch (dres)
                                    {
                                        case DialogResult.Yes:
                                            if (dr != null)
                                            {
                                                string TempParentId = dr["ParentId"].ToString();
                                                this.UpgradeDataRow(tabindex, TempInvoiceXjDetailId, TempParentId);
                                                o.Rows.Remove(dr);
                                            }
                                            break;
                                        case DialogResult.No:
                                            if (dr != null)
                                            {
                                                o.Rows.Remove(dr);
                                            }
                                            break;
                                        case DialogResult.Cancel:
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                o.Rows.Remove(dr);
                            }
                            break;
                    }
                    this.bindingSourceInvoiceXJDetail.DataSource = this._ds.Tables["Level0"];
                    this.gridControl2.RefreshDataSource();
                }
            }
        }

        //---------------------------------------------加工----------------------------//
        private void btn_processAdd_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Product p = f.SelectedItem as Model.Product;

                Model.InvoiceXJProcess xjp = new Book.Model.InvoiceXJProcess();
                xjp.InvoiceXJId = this.invoice.InvoiceId;
                xjp.InvoiceXJ = this.invoice;
                xjp.ProductName = p.ProductName;
                xjp.Supplier = p.Supplier;
                xjp.SupplierId = p.Supplier == null ? null : p.Supplier.SupplierId;
                if (p.Supplier != null)
                    xjp.PriceAndRange = this.spm.GetPriceRangeForSupAndProduct(p.SupplierId, p.ProductId);
                xjp.InvoiceXJProcessPrice = Convert.ToDouble(BL.SupplierProductManager.CountPrice(xjp.PriceAndRange, 1));
                this.invoice.DetailsProcess.Add(xjp);

                this.gridControl1.RefreshDataSource();
            }
        }

        private void btn_processDel_Click(object sender, EventArgs e)
        {
            Model.InvoiceXJProcess xjp = this.bindingSourceProcess.Current as Model.InvoiceXJProcess;
            if (xjp != null)
            {
                this.invoice.DetailsProcess.Remove(xjp);
                this.gridControl1.RefreshDataSource();
            }
        }

        //加工 -- 选择 委外 or 外购商品
        private void Btn_XJProcSPC_Click(object sender, EventArgs e)
        {
            Settings.BasicData.Supplier.SupplierProductProcesscategoryEdit f = new Book.UI.Settings.BasicData.Supplier.SupplierProductProcesscategoryEdit();
            if (DialogResult.OK == f.ShowDialog(this))
            {
                Model.SupplierProduct sup = f._SelectItem as Model.SupplierProduct;
                if (sup == null)
                    return;
                Model.InvoiceXJProcess detail = new Model.InvoiceXJProcess();
                detail.Supplier = sup.Supplier;
                detail.SupplierId = detail.Supplier == null ? null : detail.Supplier.SupplierId;
                detail.ProductName = sup.Product == null ? "" : sup.Product.ToString();
                detail.InvoiceXJProcessCB1 = 0;
                detail.InvoiceXJProcessType = sup.ProductType;
                detail.PriceAndRange = sup.SupplierProductPriceRange;
                detail.InvoiceXJProcessPrice = Convert.ToDouble(BL.SupplierProductManager.CountPrice(detail.PriceAndRange, 1));

                (this.bindingSourceProcess.DataSource as IList<Model.InvoiceXJProcess>).Add(detail);
                this.gridControl1.RefreshDataSource();
            }
        }

        //_ _ _ _ _ _ _ _ _ _ _ _修改加工详细
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "gcolProcCB1":
                case "gcolProcPrice":
                    double? cb1 = (this.bindingSourceProcess.DataSource as IList<Model.InvoiceXJProcess>).Sum(d => d.InvoiceXJProcessCB1);
                    double? bj = (this.bindingSourceProcess.DataSource as IList<Model.InvoiceXJProcess>).Sum(d => d.InvoiceXJProcessPrice);

                    this.spinZCBjinE.EditValue = Convert.ToDouble(this.spinZCBjinE.EditValue) - Convert.ToDouble(this.spinJGCBjinE.EditValue) + cb1;
                    this.spinZBJjinE.EditValue = Convert.ToDouble(this.spinZBJjinE.EditValue) - Convert.ToDouble(this.spinZBJjinE.EditValue) + bj;

                    this.spinJGCBjinE.EditValue = cb1;
                    this.spinJGBJjinE.EditValue = bj;
                    break;
                case "gcolProcLeiXing":
                    Model.InvoiceXJProcess xjp1 = this.bindingSourceProcess.Current as Model.InvoiceXJProcess;
                    if (e.Value.ToString() == "自製" || e.Value.ToString() == "自制")
                    {
                        xjp1.ProductId = null;
                        xjp1.Product = null;
                        xjp1.SupplierId = null;
                        xjp1.Supplier = null;
                    }
                    break;
                case "gcolProcProductQuantity":
                    Model.InvoiceXJProcess xjp2 = this.bindingSourceProcess.Current as Model.InvoiceXJProcess;
                    xjp2.InvoiceXJProcessCB1 = Convert.ToDouble(e.Value) * Convert.ToDouble(xjp2.DanJia);
                    goto case "gcolProcCB1";
                case "colProcDanJia":
                    Model.InvoiceXJProcess xjp3 = this.bindingSourceProcess.Current as Model.InvoiceXJProcess;
                    xjp3.InvoiceXJProcessCB1 = xjp3.InvoiceXJProcessQuantity * Convert.ToDouble(e.Value.ToString());
                    goto case "gcolProcCB1";
                //double? cb2 = (this.bindingSourceProcess.DataSource as IList<Model.InvoiceXJProcess>).Sum(d => d.InvoiceXJProcessCB1);
                //double? bj2 = (this.bindingSourceProcess.DataSource as IList<Model.InvoiceXJProcess>).Sum(d => d.InvoiceXJProcessPrice);

                //this.spinZCBjinE.EditValue = Convert.ToDouble(this.spinZCBjinE.EditValue) - Convert.ToDouble(this.spinJGCBjinE.EditValue) + cb2;
                //this.spinZBJjinE.EditValue = Convert.ToDouble(this.spinZBJjinE.EditValue) - Convert.ToDouble(this.spinZBJjinE.EditValue) + bj2;

                //this.spinJGCBjinE.EditValue = cb2;
                //this.spinJGBJjinE.EditValue = bj2;
                //case "gcolProcProcessCategory":
                //    Model.InvoiceXJProcess xjp2 = this.bindingSourceProcess.Current as Model.InvoiceXJProcess;
                //    xjp2.ProcessCategoryId = e.Value.ToString();
                //    xjp2.ProcessCategory = (this.bsProcProcessCategory.DataSource as IList<Model.ProcessCategory>).Where(d => d.ProcessCategoryId == e.Value.ToString()).First();
                //    break;
            }
            this.gridControl1.RefreshDataSource();
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.InvoiceXJProcess xjp = new Book.Model.InvoiceXJProcess();
                    xjp.InvoiceXJProcessId = Guid.NewGuid().ToString();
                    xjp.InvoiceXJId = this.invoice.InvoiceId;
                    xjp.InvoiceXJ = this.invoice;
                    xjp.InvoiceXJProcessCB1 = 0;
                    xjp.InvoiceXJProcessCB2 = 0;
                    xjp.InvoiceXJProcessPrice = 0;
                    this.invoice.DetailsProcess.Add(xjp);

                    this.gridControl1.RefreshDataSource();
                }
                if (e.KeyData == Keys.Delete)
                {
                    Model.InvoiceXJProcess xjp = this.bindingSourceProcess.Current as Model.InvoiceXJProcess;
                    if (xjp != null)
                    {
                        this.invoice.DetailsProcess.Remove(xjp);
                        this.gridControl1.RefreshDataSource();
                    }
                }
            }
        }

        //---------------------------------------------包材----------------------------//

        private void btn_PackageAdd_Click(object sender, EventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            Book.UI.Invoices.ChooseProductForm.ProductList = null;
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.InvoiceXJPackageDetails xjpack;
                if (Book.UI.Invoices.ChooseProductForm.ProductList == null || Book.UI.Invoices.ChooseProductForm.ProductList.Count == 0)
                {
                    Model.Product p = f.SelectedItem as Model.Product;
                    if (p != null)
                    {
                        xjpack = new Book.Model.InvoiceXJPackageDetails();
                        xjpack.InvoiceId = this.invoice.InvoiceId;
                        //xjpack.Product = p;
                        //xjpack.ProductId = p.ProductId;
                        xjpack.ProductName = p.ToString();
                        xjpack.Quantity = 0;
                        xjpack.UseQuantity = 0;
                        if (p.Supplier != null)
                            xjpack.PriceAndRange = this.spm.GetPriceRangeForSupAndProduct(p.SupplierId, p.ProductId);
                        xjpack.PackagePrice = BL.SupplierProductManager.CountPrice(xjpack.PriceAndRange, 1);
                        xjpack.Inumber = this.invoice.DetailPackage.Count + 1;
                        xjpack.ConsumeRate = 0;
                        xjpack.EffectsDate = DateTime.Now;
                        xjpack.ExpiringDate = DateTime.Now.AddYears(10);
                        xjpack.Supplier = p.Supplier;
                        if (xjpack.Supplier != null)
                            xjpack.SupplierId = xjpack.Supplier.SupplierId;
                        xjpack.Description = p.ProductDescription;
                        this.invoice.DetailPackage.Add(xjpack);
                    }
                }
                else if (Book.UI.Invoices.ChooseProductForm.ProductList != null && Book.UI.Invoices.ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product p in Book.UI.Invoices.ChooseProductForm.ProductList)
                    {
                        xjpack = new Book.Model.InvoiceXJPackageDetails();
                        xjpack.InvoiceId = this.invoice.InvoiceId;
                        //xjpack.Product = p;
                        //xjpack.ProductId = p.ProductId;
                        xjpack.ProductName = p.ToString();
                        xjpack.Quantity = 0;
                        xjpack.UseQuantity = 0;
                        if (p.Supplier != null)
                            xjpack.PriceAndRange = this.spm.GetPriceRangeForSupAndProduct(p.SupplierId, p.ProductId);
                        xjpack.PackagePrice = BL.SupplierProductManager.CountPrice(xjpack.PriceAndRange, 1);

                        xjpack.Inumber = this.invoice.DetailPackage.Count + 1;
                        xjpack.ConsumeRate = 0;
                        xjpack.EffectsDate = DateTime.Now;
                        xjpack.ExpiringDate = DateTime.Now.AddYears(10);
                        xjpack.Description = p.ProductDescription;

                        this.invoice.DetailPackage.Add(xjpack);
                    }
                }

                this.gridControl3.RefreshDataSource();
            }
        }

        private void btn_PackageDel_Click(object sender, EventArgs e)
        {
            Model.InvoiceXJPackageDetails xjpd = this.bindingSourcePackageDetails.Current as Model.InvoiceXJPackageDetails;
            if (xjpd != null)
            {
                this.invoice.DetailPackage.Remove(xjpd);
                this.gridControl3.RefreshDataSource();
            }
        }

        //包材 -- 选择 委外 or 外购 商品
        private void Btn_XJPackSPC_Click(object sender, EventArgs e)
        {
            Settings.BasicData.Supplier.SupplierProductProcesscategoryEdit f = new Book.UI.Settings.BasicData.Supplier.SupplierProductProcesscategoryEdit();
            if (DialogResult.OK == f.ShowDialog(this))
            {
                Model.InvoiceXJPackageDetails detail = new Book.Model.InvoiceXJPackageDetails();

                Model.SupplierProduct supro = f._SelectItem as Model.SupplierProduct;
                detail.Supplier = supro.Supplier;
                detail.SupplierId = detail.Supplier == null ? null : detail.Supplier.SupplierId;
                detail.ProductName = supro.Product == null ? null : supro.Product.ToString();
                detail.InvoiceXJProcessCB1 = 0;
                detail.InvoiceXJPackageDetailsType = supro.ProductType;
                detail.PriceAndRange = supro.SupplierProductPriceRange;
                detail.PackagePrice = BL.SupplierProductManager.CountPrice(detail.PriceAndRange, 1);

                this.invoice.DetailPackage.Add(detail);
                this.gridControl3.RefreshDataSource();
            }
        }

        private void gridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.InvoiceXJPackageDetails xjpd = new Book.Model.InvoiceXJPackageDetails();
                    xjpd.InvoiceXJPackageDetailsId = Guid.NewGuid().ToString();
                    xjpd.InvoiceId = this.invoice.InvoiceId;
                    xjpd.Invoice = this.invoice;
                    xjpd.Quantity = 0;
                    xjpd.UseQuantity = 0;
                    xjpd.PackagePrice = 0;
                    xjpd.Inumber = this.invoice.DetailPackage.Count + 1;
                    xjpd.ConsumeRate = 0;
                    xjpd.EffectsDate = DateTime.Now;
                    xjpd.ExpiringDate = DateTime.Now.AddYears(10);
                    this.invoice.DetailPackage.Add(xjpd);

                    this.gridControl3.RefreshDataSource();
                }
                if (e.KeyData == Keys.Delete)
                {
                    Model.InvoiceXJPackageDetails xjpd = this.bindingSourcePackageDetails.Current as Model.InvoiceXJPackageDetails;
                    if (xjpd != null)
                    {
                        this.invoice.DetailPackage.Remove(xjpd);
                        this.gridControl3.RefreshDataSource();
                    }
                }
            }
        }

        //--------修改包材价格
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "gcolPackCB1":
                case "gcolPackPrice":
                    double? cb1 = (this.bindingSourcePackageDetails.DataSource as IList<Model.InvoiceXJPackageDetails>).Sum(d => d.InvoiceXJProcessCB1);
                    decimal? bj = (this.bindingSourcePackageDetails.DataSource as IList<Model.InvoiceXJPackageDetails>).Sum(d => d.PackagePrice);

                    this.spinZCBjinE.EditValue = Convert.ToDouble(this.spinZCBjinE.EditValue) - Convert.ToDouble(this.spinBCCBjinE.EditValue) + cb1;
                    this.spinZBJjinE.EditValue = Convert.ToDouble(this.spinZBJjinE.EditValue) - Convert.ToDouble(this.spinZBJjinE.EditValue) + Convert.ToDouble(bj);
                    this.spinBCCBjinE.EditValue = cb1;
                    this.spinBCBJjinE.EditValue = bj;
                    break;
                case "gcolPackType":
                    Model.InvoiceXJPackageDetails xjpd = this.bindingSourcePackageDetails.Current as Model.InvoiceXJPackageDetails;
                    if (e.Value.ToString() == "自製" || e.Value.ToString() == "自制")
                    {
                        xjpd.Supplier = null;
                        xjpd.SupplierId = null;
                        xjpd.Product = null;
                        xjpd.ProductId = null;
                    }
                    break;
                case "gcolPackProductQuantity":
                    Model.InvoiceXJPackageDetails xjpd1 = this.bindingSourcePackageDetails.Current as Model.InvoiceXJPackageDetails;
                    xjpd1.InvoiceXJProcessCB1 = Convert.ToDouble(e.Value) * Convert.ToDouble(xjpd1.DanJia);
                    goto case "gcolPackCB1";
                case "colPackDanJia":
                    Model.InvoiceXJPackageDetails xjpd2 = this.bindingSourcePackageDetails.Current as Model.InvoiceXJPackageDetails;
                    xjpd2.InvoiceXJProcessCB1 = xjpd2.InvoiceXJPackageDetailsQuantity * Convert.ToDouble(e.Value.ToString());
                    goto case "gcolPackCB1";
            }
        }

        //--------选择包材商品
        private void btnEditPackageProduct_Click(object sender, EventArgs e)
        {
            if (this.action != "view")
            {
                Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    this.invoice.ProductPackage = f.SelectedItem as Model.Product;
                    if (this.invoice.ProductPackage != null)
                    {
                        Model.BomParentPartInfo BomParentRoot = this._BOMParentInfoManager.SelectByProductId(this.invoice.ProductPackage.ProductId);
                        if (BomParentRoot != null)
                        {
                            this.invoice.DetailPackage.Clear();
                            this.VV(BomParentRoot.BomId);
                            this.XX(BomParentRoot.BomId);
                        }

                        this.invoice.ProductIdPackage = this.invoice.ProductPackage.ProductId;
                        this.btnEditPackageProduct.EditValue = this.invoice.ProductPackage;
                        this.bindingSourcePackageDetails.DataSource = this.invoice.DetailPackage;
                        this.gridControl3.RefreshDataSource();
                    }
                }
            }
        }

        private void XX(string RootBOMid)
        {
            IList<Model.BomComponentInfo> bomcoms = this._BOMComponentInfoManager.SelectLessInfoByHeaderId(RootBOMid);
            if (bomcoms != null && bomcoms.Count > 0)
            {
                foreach (Model.BomComponentInfo incom in bomcoms)
                {
                    Model.BomParentPartInfo inParent = this._BOMParentInfoManager.SelectByProductId(incom.ProductId);
                    if (inParent != null)
                    {
                        this.VV(inParent.BomId);
                        this.XX(inParent.BomId);
                    }
                }
            }
        }

        private void VV(string BomId)
        {
            IList<Model.BomPackageDetails> bpd1 = this._BOMPackageDetailsManager.Select(BomId);
            if (bpd1 != null && bpd1.Count > 0)
            {
                Model.InvoiceXJPackageDetails xpd = null;
                foreach (Model.BomPackageDetails d in bpd1)
                {
                    xpd = new Book.Model.InvoiceXJPackageDetails();
                    xpd.InvoiceXJPackageDetailsId = Guid.NewGuid().ToString();
                    xpd.Inumber = this.invoice.DetailPackage.Count + 1;
                    xpd.InvoiceId = this.invoice.InvoiceId;
                    //xpd.Product = d.Product;
                    //xpd.ProductId = d.ProductId;
                    xpd.ProductName = d.Product.ToString();
                    xpd.Quantity = d.Quantity;
                    xpd.UseQuantity = d.UseQuantity;
                    xpd.PackagePrice = 0;
                    if (d.Product.Supplier != null)
                        xpd.PriceAndRange = this.spm.GetPriceRangeForSupAndProduct(d.Product.SupplierId, d.ProductId);
                    xpd.ConsumeRate = d.ConsumeRate;
                    xpd.EffectsDate = DateTime.Now;
                    xpd.ExpiringDate = DateTime.Now.AddYears(10);
                    xpd.Description = d.Product == null ? "" : d.Product.ProductDescription;
                    xpd.Supplier = d.Product.Supplier;
                    if (xpd.Supplier != null) xpd.SupplierId = xpd.Supplier.SupplierId;

                    this.invoice.DetailPackage.Add(xpd);
                }
            }
        }

        //------------------------------------------打印表单---------------------------------//
        private void barBtn_PrintCB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ConstructPrintData(false);
            ROChengBen RCB = new ROChengBen(this.invoice);
            RCB.ShowPreviewDialog();
        }

        private void barBtn_PrintBJ_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ConstructPrintData(true);
            ROBaoJia RBJ = new ROBaoJia(this.invoice);
            RBJ.ShowPreviewDialog();
        }

        //复制报价单
        private void bar_btnCopyInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.invoice == null) return;

            this.invoice.Details = this._invoiceXJDetailManager.Select(this.invoice);

            //修改报价单头
            this.invoice.InvoiceId = this._invoiceManager.GetNewId();
            this.invoice.InvoiceDate = DateTime.Now;
            this.invoice.InvoiceYxrq = DateTime.Now;


            //修改报价详细
            if (this.invoice.Details != null)
            {
                foreach (Model.InvoiceXJDetail item in this.invoice.Details)
                {
                    item.InvoiceId = this.invoice.InvoiceId;
                    string tempParentId = item.InvoiceXJDetailId;
                    item.InvoiceXJDetailId = Guid.NewGuid().ToString();
                    this.invoice.Details.ToList().ForEach(
                        d =>
                        {
                            if (d.ParentId == tempParentId)
                                d.ParentId = item.InvoiceXJDetailId;
                        });
                }
            }

            //修改包材部分
            if (this.invoice.DetailPackage != null)
                this.invoice.DetailPackage.ToList().ForEach(
                    d =>
                    {
                        d.InvoiceXJPackageDetailsId = Guid.NewGuid().ToString();
                        d.InvoiceId = this.invoice.InvoiceId;
                    });

            //修改加工部分
            if (this.invoice.DetailsProcess != null)
                this.invoice.DetailsProcess.ToList().ForEach(
                        d =>
                        {
                            d.InvoiceXJProcessId = Guid.NewGuid().ToString();
                            d.InvoiceXJId = this.invoice.InvoiceId;
                        });



            //  Workflowinsert wfinsert = new Workflowinsert();
            this._invoiceManager.Insert(this.invoice);
            //if (wfinsert.Checkwfbytablescode("客戶報價單"))
            //{
            //    wfinsert.insertwfrecord("客戶報價單", "客戶報價單", this.invoice.InvoiceId);
            //}

            if (DialogResult.OK == MessageBox.Show(this, "單據複製成功,是否轉到新單價?", "消息", MessageBoxButtons.OKCancel))
                this.Refresh();
        }

        //报价列印循环格式临时使用List
        List<Model.InvoiceXJDetail> tempDetails;

        private void ConstructPrintData(bool ISBaoJia)
        {
            if (this._ds != null && this._ds.Tables.Count > 0)
            {
                this.txtProductPattern.Focus();
                this.invoice.Details = new List<Model.InvoiceXJDetail>();
                this.tempDetails = new List<Book.Model.InvoiceXJDetail>();

                DataTable foreachDT;
                for (int i = 0; i < this._ds.Tables.Count; i++)
                {
                    foreachDT = this._ds.Tables[i];
                    if (foreachDT.TableName.Equals("Level0"))
                    {
                        DataView dv = foreachDT.DefaultView;
                        dv.Sort = "Inumber asc";
                        foreachDT = dv.ToTable();
                    }

                    foreach (DataRow dr in foreachDT.Rows)
                    {
                        if (ISBaoJia)
                        {
                            bool hasChecked = Convert.ToBoolean(dr["IsChecked"]);
                            if (!hasChecked) continue;
                        }

                        if (dr.RowState == DataRowState.Deleted || dr.RowState == DataRowState.Detached) continue;

                        Model.InvoiceXJDetail d = new Book.Model.InvoiceXJDetail();
                        d.InvoiceId = this.invoice.InvoiceId;
                        //d.InvoiceXJDetailId = Guid.NewGuid().ToString();
                        //d.ShamParentID = dr["InvoiceXJDetailId"] == null ? "" : dr["InvoiceXJDetailId"].ToString();
                        d.InvoiceXJDetailId = dr["InvoiceXJDetailId"] == null ? "" : dr["InvoiceXJDetailId"].ToString();
                        d.ParentId = dr["ParentId"] == null ? "" : dr["ParentId"].ToString();
                        //if (dt.TableName == "Level0")
                        //    d.ParentId = "000";
                        //else
                        //{
                        //    d.ParentId = dr["ParentId"] == null ? "" : dr["ParentId"].ToString();
                        //    var res = this.invoice.Details.Where(ind => ind.ShamParentID == d.ParentId).First();
                        //    d.ParentId = (res as Model.InvoiceXJDetail).InvoiceXJDetailId;
                        //}

                        d.InvoiceXJDetailPrice = decimal.Parse(objectToDouble(dr["InvoiceXJDetailPrice"]).ToString());
                        d.InvoiceXJDetailQuantity = objectToDouble(dr["InvoiceXJDetailQuantity"]);
                        d.InvoiceXJDetailMoney = decimal.Parse(objectToDouble(dr["InvoiceXJDetailMoney"]).ToString());
                        d.InvoiceXJDetailMoney2 = decimal.Parse(objectToDouble(dr["InvoiceXJDetailMoney2"]).ToString());
                        d.InvoiceXJDetailQuote = decimal.Parse(objectToDouble(dr["InvoiceXJDetailQuote"]).ToString());
                        d.InvoiceProductUnit = dr["Unit"] == null ? "" : dr["Unit"].ToString();
                        d.ProductName = dr["ProductName"] == null ? "" : dr["ProductName"].ToString();

                        //this.invoice.Details.Add(d);
                        this.tempDetails.Add(d);
                    }
                }

                //数据格式化--父子格式
                if (tempDetails != null && tempDetails.Count > 0)
                {
                    foreach (Model.InvoiceXJDetail item in tempDetails.Where(d => { return d.ParentId == "000"; }))
                    {
                        this.PrintPattenRecursion(item, 0);
                    }
                }
            }
        }

        //打印数据格式递归
        private void PrintPattenRecursion(Model.InvoiceXJDetail d, int Spaces)
        {
            d.ProductName = d.ProductName.PadLeft(d.ProductName.Length + Spaces);
            this.invoice.Details.Add(d);

            IList<Model.InvoiceXJDetail> templist = this.tempDetails.Where(ind => { return ind.ParentId == d.InvoiceXJDetailId; }).ToList<Model.InvoiceXJDetail>();

            if (templist != null && templist.Count > 0)
            {
                foreach (Model.InvoiceXJDetail item in templist)
                {
                    if (this.tempDetails.Any(innd => { return innd.ParentId == item.InvoiceXJDetailId; }))
                    {
                        this.PrintPattenRecursion(item, ++Spaces);
                    }
                    else
                    {
                        item.ProductName = item.ProductName.PadLeft(item.ProductName.Length + Spaces);
                        this.invoice.Details.Add(item);
                    }
                }
            }
        }

        //---------------------------------------------Help----------------------------//
        private double objectToDouble(object o)
        {
            if (o == null)
                return 0;
            return string.IsNullOrEmpty(o.ToString()) ? 0 : double.Parse(o.ToString());
        }

        //传过来的parentId是父级的主键
        private void UpgradeDataRow(int tabindex, string parentId, string newParentId)
        {
            if (this._ds.Tables.Contains("Level" + (tabindex + 1).ToString()))
            {
                DataTable currentDT = this._ds.Tables["Level" + tabindex.ToString()];
                DataTable SonDT = this._ds.Tables["Level" + (tabindex + 1).ToString()];
                DataRow[] sonList = SonDT.Select("ParentId = '" + parentId + "'");
                DataRow currentDR = currentDT.Select("InvoiceXJDetailId = '" + parentId + "'")[0];
                if (sonList == null || sonList.Length > 0)
                {
                    foreach (DataRow indr in sonList)
                    {
                        string inParentId = indr["InvoiceXJDetailId"].ToString();
                        string mynewParentId = Guid.NewGuid().ToString();

                        //首先复制到父级
                        DataRow newdr = currentDT.NewRow();
                        for (int i = 0; i < indr.ItemArray.Length; i++)
                        {
                            newdr[i] = indr.ItemArray[i];
                        }
                        newdr["InvoiceXJDetailId"] = mynewParentId;
                        //newdr["ParentId"] = currentDR["ParentId"];
                        newdr["ParentId"] = newParentId;
                        if (newdr["ParentId"].ToString() == "000")
                        {
                            newdr["Inumber"] = 0;
                        }
                        currentDT.Rows.Add(newdr);

                        //其次改变下级
                        this.UpgradeDataRow(tabindex + 1, inParentId, mynewParentId);
                    }
                }
            }
        }
    }

    public class ColorMaker
    {
        private Color startColor, endColor;

        internal ColorMaker()
        {
            startColor = SystemColors.ControlDark;
            endColor = SystemColors.Window;
        }

        internal ColorMaker(Color StartColor, Color EndColor)
        {
            startColor = StartColor;
            endColor = EndColor;
        }

        internal Color ProduceColor(int Brightness)
        {
            Color c;
            int r, g, b, a;

            if (Brightness > 100)
                Brightness = 100;

            if (Brightness < 0)
                Brightness = 0;

            r = startColor.R + (endColor.R - startColor.R) * Brightness / 100;
            g = startColor.G + (endColor.G - startColor.G) * Brightness / 100;
            b = startColor.B + (endColor.B - startColor.B) * Brightness / 100;
            a = startColor.A + (endColor.A - startColor.A) * Brightness / 100;

            c = Color.FromArgb(a, r, g, b);

            return c;
        }
    }
}
