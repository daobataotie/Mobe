using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.BasicData.Supplier;
using Book.UI.Invoices;

namespace Book.UI.Settings.StockLimitations
{

    public partial class DepotInForm : Settings.BasicData.BaseEditForm
    {

        private Model.DepotIn _depotIn;
        private Model.DepotInDetail _depotInDetail;
        private BL.DepotInManager _depotInManager = new Book.BL.DepotInManager();
        private BL.DepotInDetailManager _depotInDetailManager = new Book.BL.DepotInDetailManager();
        private BL.DepotManager _depotManager = new Book.BL.DepotManager();
        private BL.EmployeeManager _employeeManager = new Book.BL.EmployeeManager();
        private BL.ProductManager _productManager = new Book.BL.ProductManager();
        private BL.DepotPositionManager _depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.ProductUnitManager _productUnitManager = new Book.BL.ProductUnitManager();
        private Model.ProduceInDepot produceInDepot = new Model.ProduceInDepot();
        private BL.PronoteHeaderManager pronoteHeaderManager = new BL.PronoteHeaderManager();
        private BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        private BL.ProduceMaterialExitDetailManager produceMaterialExitDetailManager = new Book.BL.ProduceMaterialExitDetailManager();

        public DepotInForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.DepotIn.PRO_DepotInId, new AA(Properties.Resources.deptNotNull, this.lookUpEditDepotId as Control));
            this.requireValueExceptions.Add(Model.DepotInDetail.PRO_DepotPositionId, new AA(Properties.Resources.DepotInStockQuertyIsNull, this.gridControl1 as Control));
            this.requireValueExceptions.Add("ProductDetail Is Null", new AA(Properties.Resources.ProductDetailIsNull, this.gridControl1 as Control));
            //this.invalidValueExceptions.Add(Model.DepotIn.PROPERTY_DEPOTINID, new AA(Properties.Resources.NunsIsExists, this.textEditDepotInId as Control));
            this.action = "view";

            this.newChooseContorlSupplier.Choose = new Book.UI.Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseEmp0.Choose = new ChooseEmployee();
            this.newChooseEmp1.Choose = new ChooseEmployee();
            this.newChooseEmp2.Choose = new ChooseEmployee();
            this.bindingSourceDepot.DataSource = this._depotManager.Select();

            string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            this.bindingSourceProduct.DataSource = this._productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            // this.bindingSourceProduct.DataSource = this._productManager.GetProduct();
            //this.bindingSourceProduct.DataSource = this._productManager.Select();

        }

        protected override void AddNew()
        {
            this._depotIn = new Book.Model.DepotIn();
            this._depotIn.DepotInId = this._depotInManager.GetId(DateTime.Now.Date);
            this._depotIn.DepotId = null;
            this._depotIn.Details = new List<Model.DepotInDetail>();
            this._depotIn.Employee = BL.V.ActiveOperator.Employee;
            this._depotInDetail = new Book.Model.DepotInDetail();
            this._depotInDetail.DepotInDetailId = Guid.NewGuid().ToString();
            this._depotInDetail.Inumber = this._depotIn.Details.Count + 1;
            this._depotIn.Details.Add(this._depotInDetail);
            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(this._depotInDetail);
        }

        protected override void Save()
        {
            this._depotIn.DepotInId = this.textEditDepotInId.Text;
            this._depotIn.DepotInDate = this.dateEditDate.DateTime;
            if (this.newChooseEmp1.EditValue != null)
            {
                this._depotIn.Employee0Id = (this.newChooseEmp1.EditValue as Model.Employee).EmployeeId;
            }
            if (this.newChooseEmp0.EditValue != null)
            {
                this._depotIn.EmployeeId = (this.newChooseEmp0.EditValue as Model.Employee).EmployeeId;
            }
            if (this.newChooseContorlSupplier.EditValue != null)
                this._depotIn.SupplierId = (this.newChooseContorlSupplier.EditValue as Model.Supplier).SupplierId;
            if (this.lookUpEditDepotId.EditValue != null)
                this._depotIn.DepotId = this.lookUpEditDepotId.EditValue.ToString();
            //if (this.buttonEditProductCategry.EditValue != null)
            //{
            //    this._depotIn.ProductCategory = this.buttonEditProductCategry.EditValue as Model.ProductCategory;
            //    this._depotIn.ProductCategoryId = this._depotIn.ProductCategory.ProductCategoryId;
            //}
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._depotInManager.Insert(this._depotIn);
                    break;
                case "update":
                    this._depotInManager.Update(this._depotIn);
                    break;
            }
        }

        public override void Refresh()
        {
            if (this._depotIn == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action != "insert")
                {
                    this._depotIn = this._depotInManager.GetDetails(this._depotIn);
                    if (this._depotIn == null)
                    {
                        this._depotIn = new Book.Model.DepotIn();
                        this._depotIn.DepotInId = this._depotInManager.GetId(DateTime.Now.Date);
                        this._depotIn.Details = new List<Model.DepotInDetail>();
                        this._depotInDetail = new Book.Model.DepotInDetail();
                        this._depotInDetail.DepotInDetailId = Guid.NewGuid().ToString();
                        this._depotIn.Details.Add(this._depotInDetail);
                        this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(this._depotInDetail);
                    }
                }
            }
            this.dateEditDate.EditValue = DateTime.Now;

            this.textEditDepotInId.Text = this._depotIn.DepotInId;
            if (this._depotIn.DepotInDate != null)
                this.dateEditDate.DateTime = this._depotIn.DepotInDate.Value;

            if (this._depotIn.Employee0Id != null)
                this.newChooseEmp1.EditValue = _depotIn.Employee0;
            //else
            //    this.newChooseEmp0.EditValue = null;


            this.newChooseEmp0.EditValue = this._depotIn.Employee;
            //else
            //    this.newChooseEmp.EditValue = null;

            if (this._depotIn.Supplier != null)
                this.newChooseContorlSupplier.EditValue = this._depotIn.Supplier;
            //else
            //    this.newChooseContorlSupplier.EditValue = null;
            this.lookUpEditDepotId.EditValue = this._depotIn.DepotId;



            this.lookUpEditDepotId.EditValue = this._depotIn.DepotId;

            if (this._depotIn.DepotId == null)
                this.bindingSourceDepotPisition.DataSource = null;
            //else this.newChooseEmp.EditValue = null;

            //this.buttonEditProductCategry.EditValue = this._depotIn.ProductCategory;
            //if (this._depotIn.ProductCategory != null)
            //    this.bindingSourceProduct.DataSource = this._productManager.Select(this._depotIn.ProductCategory);
            this.newChooseEmp2.EditValue = this._depotIn.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._depotIn.AuditState);

            this.bindingSourceDetail.DataSource = this._depotIn.Details;


            this.gridControl1.RefreshDataSource();

            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.textEditDepotInId.Properties.ReadOnly = false;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.textEditDepotInId.Properties.ReadOnly = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.textEditDepotInId.Properties.ReadOnly = true;
                    break;

            }
        }

        protected override void MoveNext()
        {
            Model.DepotIn depotIn = this._depotInManager.GetNext(this._depotIn);
            if (depotIn == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._depotIn = this._depotInManager.Get(depotIn.DepotInId);
        }

        protected override void MovePrev()
        {
            Model.DepotIn depotIn = this._depotInManager.GetPrev(this._depotIn);
            if (depotIn == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._depotIn = this._depotInManager.Get(depotIn.DepotInId);
        }

        protected override bool SetColumnNumber()
        {
            return true;
        }

        protected override void MoveFirst()
        {
            this._depotIn = this._depotInManager.Get(this._depotInManager.GetFirst() == null ? "" : this._depotInManager.GetFirst().DepotInId);
        }

        protected override void MoveLast()
        {
            this._depotIn = this._depotInManager.Get(this._depotInManager.GetLast() == null ? "" : this._depotInManager.GetLast().DepotInId);
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this._depotInManager.Delete(this._depotIn);
                this._depotIn = this._depotInManager.GetNext(this._depotIn);
                if (this._depotIn == null)
                {
                    this._depotIn = this._depotInManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        protected override bool HasRows()
        {
            return this._depotInManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._depotInManager.HasRowsAfter(this._depotIn);
        }

        protected override bool HasRowsPrev()
        {
            return this._depotInManager.HasRowsBefore(this._depotIn);
        }

        private void lookUpEditDepotId_Click(object sender, EventArgs e)
        {
            this.bindingSourceDepot.DataSource = this._depotManager.Select();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumn3 || e.Column == this.gridColumnProductId || e.Column == this.gridColumnProductName)
            {
                Model.DepotInDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.DepotInDetail;
                if (detail != null)
                {
                    Model.Product p = this._productManager.Get(e.Value.ToString());
                    //detail.DepotInDetailId = Guid.NewGuid().ToString();
                    detail.DepotPositionId = null;
                    detail.DepotInQuantity = 1;
                    detail.DepotInPrice = null;
                    detail.DepotInTotal = null;
                    detail.Product = p;
                    detail.ProductId = p == null ? "" : p.ProductId;
                    detail.ProductUnit = p == null ? "" : p.DepotUnit.CnName;
                    detail.DepotInId = this._depotIn.DepotInId;
                    detail.Description = p == null ? "" : p.ProductDescription;
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.DepotInDetail detail = new Model.DepotInDetail();
                    detail.DepotInDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = this._depotIn.Details.Count + 1;
                    detail.DepotInId = this._depotIn.DepotInId;
                    detail.DepotInPrice = null;
                    detail.Description = "";
                    detail.DepotInTotal = null;
                    detail.ProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this._depotIn.Details.Add(detail);
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }
                if (e.KeyData == Keys.Delete)
                {
                    if (this.bindingSourceDetail.Current != null)
                    {
                        this._depotIn.Details.Remove(this.bindingSourceDetail.Current as Book.Model.DepotInDetail);
                        if (this._depotIn.Details.Count == 0)
                        {
                            Model.DepotInDetail detail = new Model.DepotInDetail();
                            detail.DepotInDetailId = Guid.NewGuid().ToString();
                            detail.Inumber = this._depotIn.Details.Count + 1;
                            detail.DepotInId = this._depotIn.DepotInId;
                            detail.DepotInPrice = null;
                            detail.Description = "";
                            detail.DepotInTotal = null;
                            detail.ProductUnit = "";
                            detail.Product = new Book.Model.Product();
                            this._depotIn.Details.Add(detail);
                            this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                        }
                    }
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumnProductUnit")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.DepotInDetail).Product;
                    this.repositoryItemComboBox1.Items.Clear();
                    if (p == null)
                        return;
                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        IList<Model.ProductUnit> units = this._productUnitManager.Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit ut in units)
                        {
                            this.repositoryItemComboBox1.Items.Add(ut.CnName);
                        }
                    }
                }
            }
        }

        private void lookUpEditDepotId_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditDepotId.EditValue != null)
                this.bindingSourceDepotPisition.DataSource = this._depotPositionManager.Select(this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.DepotInDetail> details = this.bindingSourceDetail.DataSource as IList<Model.DepotInDetail>;
            if (details == null || details.Count < 1) return;
            Model.DepotInDetail detail = details[e.ListSourceRowIndex];

            switch (e.Column.Name)
            {
                case "gridColumnCusXOId":
                    Model.PronoteHeader pronoteHeader = this.pronoteHeaderManager.Get(detail.PronoteHeaderId);
                    if (pronoteHeader == null) return;
                    Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(pronoteHeader.InvoiceXOId);
                    e.DisplayText = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
                    break;
            }
        }

        private void simpleButton_Appent_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this._depotIn.Details.Count > 0 && string.IsNullOrEmpty(this._depotIn.Details[0].ProductId))
                    this._depotIn.Details.RemoveAt(0);
                Model.DepotInDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.DepotInDetail();
                        detail.DepotInDetailId = Guid.NewGuid().ToString();
                        detail.Inumber = this._depotIn.Details.Count + 1;
                        detail.DepotInId = this._depotIn.DepotInId;
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.ProductUnit = product.DepotUnit == null ? "" : product.DepotUnit.CnName;
                        detail.Description = product.ProductDescription;
                        this._depotIn.Details.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.DepotInDetail();
                    detail.DepotInDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = this._depotIn.Details.Count + 1;
                    detail.DepotInId = this._depotIn.DepotInId;
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    detail.ProductUnit = (f.SelectedItem as Model.Product).DepotUnit == null ? "" : (f.SelectedItem as Model.Product).DepotUnit.CnName;
                    detail.Description = (f.SelectedItem as Model.Product).ProductDescription;
                    this._depotIn.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
                this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
            }
            f.Dispose();
            System.GC.Collect();
        }

        private void simpleButton_remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetail.Current != null)
            {
                this._depotIn.Details.Remove(this.bindingSourceDetail.Current as Book.Model.DepotInDetail);

                if (this._depotIn.Details.Count == 0)
                {
                    Model.DepotInDetail detail = new Model.DepotInDetail();
                    detail.DepotInDetailId = Guid.NewGuid().ToString();
                    detail.Description = "";
                    detail.DepotInQuantity = 1;
                    detail.Product = new Book.Model.Product();
                    this._depotIn.Details.Add(detail);
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        //bool isEnter = false;
        //private void buttonEditProductCategry_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    Model.ProductCategory produdctCate = this.buttonEditProductCategry.EditValue as Model.ProductCategory;
        //    if (this.action != "view")
        //    {
        //        Settings.BasicData.ProductCategories.ChooseForm f = new Settings.BasicData.ProductCategories.ChooseForm();
        //        if (f.ShowDialog(this) == DialogResult.OK)
        //        {
        //            if (f.SelectedItem as Model.ProductCategory != produdctCate)
        //            {

        //                if (this._depotIn.Details.Count != 0)
        //                {
        //                    foreach (Model.DepotInDetail item in this._depotIn.Details)
        //                    {
        //                        if (item.ProductId != null && item.ProductId != "")
        //                            isEnter = true;
        //                        if (isEnter)
        //                            break;
        //                    }
        //                }

        //                if (isEnter)
        //                {
        //                    DialogResult result = MessageBox.Show("是否清理現有詳細", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        //                    switch (result)
        //                    {
        //                        case DialogResult.OK:

        //                            this.buttonEditProductCategry.EditValue = f.SelectedItem as Model.ProductCategory;
        //                            this.bindingSourceProduct.DataSource = this._productManager.Select(f.SelectedItem as Model.ProductCategory);

        //                            this.textEditDepotInId.Text = this._depotIn.DepotInId;
        //                            if (this._depotIn.DepotInDate != null)
        //                                this.dateEditDate.DateTime = this._depotIn.DepotInDate.Value;

        //                            if (this._depotIn.Employee0Id != null)
        //                                this.newChooseEmp0.EditValue = this._employeeManager.Get(this._depotIn.Employee0Id);
        //                            //else
        //                            //    this.newChooseEmp0.EditValue = null;

        //                            if (this._depotIn.EmployeeId != null)
        //                                this.newChooseEmp.EditValue = this._employeeManager.Get(this._depotIn.EmployeeId);
        //                            //else
        //                            //    this.newChooseEmp.EditValue = null;

        //                            if (this._depotIn.Supplier != null)
        //                                this.newChooseContorlSupplier.EditValue = this._depotIn.Supplier;
        //                            //else
        //                            //    this.newChooseContorlSupplier.EditValue = null;
        //                            this.lookUpEditDepotId.EditValue = this._depotIn.DepotId;

        //                            if (this._depotIn.Employee0Id != null)
        //                            {
        //                                this.newChooseEmp.EditValue = this._depotIn.Employee;
        //                            }
        //                            if (this.buttonEditProductCategry.EditValue!=null)
        //                                this.bindingSourceProduct.DataSource = this._productManager.Select(this.buttonEditProductCategry.EditValue as Model.ProductCategory);

        //                            this._depotIn.Details = new List<Model.DepotInDetail>();
        //                            this._depotInDetail = new Book.Model.DepotInDetail();
        //                            this._depotInDetail.DepotInDetailId = Guid.NewGuid().ToString();
        //                            this._depotIn.Details.Add(this._depotInDetail);
        //                            this.bindingSourceDetail.DataSource = this._depotIn.Details;

        //                            this.gridControl1.RefreshDataSource();
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                else
        //                {
        //                    this.buttonEditProductCategry.EditValue = f.SelectedItem as Model.ProductCategory;
        //                    this.bindingSourceProduct.DataSource = this._productManager.Select(f.SelectedItem as Model.ProductCategory);
        //                }
        //                isEnter = false;
        //            }
        //        }
        //    }
        //}

        private void DepotInForm_Load(object sender, EventArgs e)
        {

        }

        private void repositoryItemLookUpEdit4_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.lookUpEditDepotId.EditValue == null)
            {
                MessageBox.Show("請選擇所屬庫房！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.lookUpEditDepotId.Focus();
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Settings.StockLimitations.DepotInReport(_depotIn.DepotInId);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            produceManager.ProduceInDepot.SelectInDepotForm form = new produceManager.ProduceInDepot.SelectInDepotForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {

                //this.produceInDepot = new Model.ProduceInDepot();
                // this.produceInDepot = produceManager.ProduceInDepot.SelectInDepotForm._produceInDepotDetail;
                this._depotIn.Details.Clear();
                foreach (var item in produceManager.ProduceInDepot.SelectInDepotForm._produceInDepotDetail)
                {
                    this._depotInDetail = new Book.Model.DepotInDetail();
                    this._depotInDetail.DepotInDetailId = Guid.NewGuid().ToString();
                    _depotInDetail.Inumber = this._depotIn.Details.Count + 1;
                    //   this._depotInDetail.DepotInPrice = item.ProduceInDepotPrice.Value;
                    this._depotInDetail.DepotInQuantity = item.ProduceQuantity.Value;
                    // this._depotInDetail.DepotInTotal = item.ProduceMoney.Value;
                    this._depotInDetail.DepotPositionId = item.DepotPositionId;
                    this._depotInDetail.DepotPosition = item.DepotPosition;
                    this._depotInDetail.ProductId = item.ProductId;
                    this._depotInDetail.ProductUnit = item.ProductUnit;
                    _depotInDetail.HandbookId = item.HandbookId;
                    _depotInDetail.HandbookProductId = item.HandbookProductId;
                    //if(item.ProductProce!=null)
                    //this._depotInDetail.Product = item.ProductProce;
                    //else
                    this._depotInDetail.Product = item.Product;
                    this._depotInDetail.PronoteHeaderId = item.PronoteHeaderId;
                    // this._depotInDetail.ProduceInDepotDetailId = item.ProduceInDepotDetailId;
                    // this._depotInDetail.Description=item.de
                    this._depotIn.Details.Add(this._depotInDetail);
                }
                this.gridControl1.RefreshDataSource();
            }

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseDepotIn form = new ChooseDepotIn();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this._depotIn = form._depotIn;
                if (this._depotIn != null)
                    this.Refresh();
            }
        }

        private void simpleButtonExit_Click(object sender, EventArgs e)
        {

            produceManager.ProduceMaterialExit.ChooseProduceMaterialExit form = new produceManager.ProduceMaterialExit.ChooseProduceMaterialExit();
            if (form.ShowDialog(this) == DialogResult.OK)
            {

                if (form.key != null && form.key.Count > 0)
                {
                    if (this._depotIn.Details.Count > 0 && string.IsNullOrEmpty(this._depotIn.Details[0].ProductId))
                        this._depotIn.Details.RemoveAt(0);
                    //string[] str = (from x in xo.Details select "'" + x.ProductId + "'").Distinct().ToArray();
                    //this.bindingSourceProduct.DataSource = this.productManager.SelectByProductIds(str.Aggregate<string>((a, b) => a + "," + b));
                    //this.gridControl1.RefreshDataSource();

                    //Model.ProduceMaterialExitDetail xo = this.produceMaterialExitDetailManager.Get(form.key[0]);
                    ////invoice.InvoiceXO = xo;
                    //invoice.InvoiceXOId = xo.InvoiceId;

                    //invoice.Customer = xo.Invoice.Customer;
                    //invoice.CustomerId = xo.Invoice.CustomerId;
                    ////invoice.CustomerInvoiceXOId = xo.CustomerInvoiceXOId;
                    //invoice.XSCustomer = xo.Invoice.xocustomer;
                    //if (xo.Invoice.xocustomer != null)
                    //    invoice.XSCustomerId = xo.Invoice.xocustomer.CustomerId;

                    //invoice.InvoiceAbstract = xo.Invoice.InvoiceAbstract;
                    //invoice.InvoiceNote = xo.Invoice.InvoiceNote;
                    //invoice.Customer = xo.Customer;
                    //invoice.XSCustomer = xo.xocustomer;
                    //   textEditiInvoiceXOId.Text = xo.InvoiceId;
                    Model.DepotInDetail detail;
                    foreach (string keyid in form.key)
                    {
                        Model.ProduceMaterialExitDetail ExitDetail = this.produceMaterialExitDetailManager.Get(keyid);
                        detail = new Book.Model.DepotInDetail();
                        detail.DepotInDetailId = Guid.NewGuid().ToString();
                        detail.Inumber = this._depotIn.Details.Count + 1;
                        // detaill.DepotInQuantity = ExitDetail.ProduceQuantity;                     
                        detail.Product = ExitDetail.Product;
                        detail.ProductId = ExitDetail.ProductId;
                        detail.DepotInQuantity = ExitDetail.ProduceQuantity;
                        detail.ProductUnit = ExitDetail.ProductUnit;
                        // detaill.ProductUnit = ExitDetail.ProductUnit;
                        detail.InvoiceId = ExitDetail.ProduceMaterialExitId;
                        detail.InvoiceDetailId = ExitDetail.ProduceMaterialExitDetailId;
                        detail.HandbookId = ExitDetail.HandbookId;
                        detail.HandbookProductId = ExitDetail.HandbookProductId;

                        this._depotIn.Details.Add(detail);
                    }
                    this.gridControl1.RefreshDataSource();
                }
                form.Dispose();
                GC.Collect();
            }
        }


        #region 审核

        protected override string AuditKeyId()
        {
            return Model.DepotIn.PRO_DepotInId;
        }

        protected override int AuditState()
        {
            return this._depotIn.AuditState.HasValue ? this._depotIn.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            if (this._depotIn == null)
                return null;
            else
                return "DepotIn" + "," + this._depotIn.DepotInId;
        }

        #endregion

        private void dateEditDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.dateEditDate.DateTime != null && this.action == "insert")
            {
                this._depotIn.DepotInId = this._depotInManager.GetId(this.dateEditDate.DateTime);
                this.textEditDepotInId.Text = this._depotIn.DepotInId;
            }
        }
    }
}