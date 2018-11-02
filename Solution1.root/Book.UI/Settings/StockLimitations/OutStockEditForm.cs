using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Settings.BasicData.ProductCategories;
using Book.UI.Invoices;
using System.Linq;

namespace Book.UI.Settings.StockLimitations
{
    public partial class OutStockEditForm : Settings.BasicData.BaseEditForm
    {
        BL.DepotManager _depotManager = new Book.BL.DepotManager();
        BL.ProductManager _productManager = new Book.BL.ProductManager();
        BL.ProductUnitManager _productUnitManager = new Book.BL.ProductUnitManager();
        BL.DepotOutManager _depotOutManager = new Book.BL.DepotOutManager();
        BL.DepotOutDetailManager _depotOutDetailManager = new Book.BL.DepotOutDetailManager();
        Model.DepotOut _depotOut;
        BL.EmployeeManager _employeeManager = new Book.BL.EmployeeManager();
        Model.Employee employee = new Book.Model.Employee();
        Model.DepotOutDetail _depotOutDetail;
        public static Model.ProduceMaterial _produceMaterial = new Book.Model.ProduceMaterial();
        public static Model.ProduceOtherMaterial _ProduceOtherMaterial = new Book.Model.ProduceOtherMaterial();
        BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        BL.StockManager stockManager = new BL.StockManager();
        BL.PronoteHeaderManager pronoteHeaderManager = new BL.PronoteHeaderManager();
        BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        BL.ProduceMaterialManager produceMaterialManager = new BL.ProduceMaterialManager();
        BL.ProduceMaterialdetailsManager produceMaterialDetailManager = new BL.ProduceMaterialdetailsManager();
        BL.ProduceOtherCompactManager produceOtherCompactManager = new BL.ProduceOtherCompactManager();
        BL.ProduceOtherMaterialDetailManager produceOtherMaterialDetailManager = new BL.ProduceOtherMaterialDetailManager();
        BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        BL.InvoiceCGDetailManager cgdetailManager = new Book.BL.InvoiceCGDetailManager();
        Model.DepotOutDetail _outDetail = new Book.Model.DepotOutDetail();

        int flag = 0;
        public OutStockEditForm()
        {
            InitializeComponent();

            this.requireValueExceptions.Add(Model.DepotOut.PRO_DepotOutId, new AA(Properties.Resources.deptNotNull, this.lookUpEditDepotId as Control));
            this.requireValueExceptions.Add(Model.DepotOut.PRO_EmployeeId, new AA(Properties.Resources.EmployeeIdNotNull, this.newChooseContorlEmployee as Control));
            this.requireValueExceptions.Add(Model.DepotOut.PRO_InsertTime, new AA(Properties.Resources.DateNotNull, this.dateEditDepotOutDate as Control));
            this.requireValueExceptions.Add(Model.DepotOutDetail.PRO_DepotPositionId, new AA(Properties.Resources.DepotInStockQuertyIsNull, this.gridControl1 as Control));
            this.requireValueExceptions.Add("ProductDetail Is Null", new AA(Properties.Resources.ProductDetailIsNull, this.gridControl1 as Control));
            // this.requireValueExceptions.Add(Model.DepotPosition.PROPERTY_DEPOTPOSITIONID, new AA(Properties.Resources.RequireChoosePosition, this.gridControl1 as Control));
            this.invalidValueExceptions.Add(Model.ProduceMaterialdetails.PRO_Materialprocessum, new AA("出倉數量不能大於未领數量", this.gridControl1 as Control));
            this.newChooseContorlEmployee.Choose = new ChooseEmployee();
            this.newChooseInvoiceEmployee0.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.newChooseWorkHouse.Choose = new ProduceManager.Workhouselog.ChooseWorkHouse();
            IList<Model.Depot> list = this._depotManager.Select();
            this.bindingSourceDepot.DataSource = list;
            if (list.Count != 0)
                this.lookUpEditDepotId.EditValue = list[0].DepotId;
            //string sql = "SELECT productid,id,productname,CustomerProductName FROM product";
            //this.bindingSourceProduct.DataSource = this._productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            // this.bindingSourceProduct.DataSource = this._productManager.GetProduct();
            this.action = "view";
        }

        public OutStockEditForm(string id)
            : this()
        {
            this._depotOut = this._depotOutManager.GetDetails(id);
            this.flag = 1;
            this.action = "view";
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this._depotOutManager.Delete(this._depotOut);
                this._depotOut = this._depotOutManager.GetNext(this._depotOut);
                if (this._depotOut == null)
                {
                    this._depotOut = this._depotOutManager.GetLast();
                }
            }
            catch
            {
                throw;
            }

        }

        protected override void AddNew()
        {
            this._depotOut = new Book.Model.DepotOut();
            this._depotOut.DepotOutDate = DateTime.Now;
            this._depotOut.DepotOutId = this._depotOutManager.GetId(DateTime.Now.Date);
            this._depotOut.Employee = BL.V.ActiveOperator.Employee;
            this._depotOut.Details = new List<Model.DepotOutDetail>();

            Model.DepotOutDetail detail = new Book.Model.DepotOutDetail();
            detail.DepotOutDetailId = Guid.NewGuid().ToString();
            detail.Inumber = this._depotOut.Details.Count + 1;
            detail.Product = new Model.Product();
            this._depotOut.Details.Add(detail);
            this.bindingSourceDepotOutDetail.Position = this.bindingSourceDepotOutDetail.IndexOf(detail);
        }

        protected override void Save()
        {

            this._depotOut.DepotOutId = textBoxDepotOutId.Text;
            if (this.newChooseContorlEmployee.EditValue != null)
            {
                this._depotOut.Employee = this.newChooseContorlEmployee.EditValue as Model.Employee;
                this._depotOut.EmployeeId = this._depotOut.Employee.EmployeeId;
            }
            this._depotOut.SourceType = this.textBoxSourceType.Text;
            this._depotOut.InvioiceId = this.textBoxInvioiceId.Text;
            if (this.lookUpEditDepotId.EditValue != null)
            {
                this._depotOut.Depot = new BL.DepotManager().Get(this.lookUpEditDepotId.EditValue.ToString());
                this._depotOut.DepotId = this.lookUpEditDepotId.EditValue.ToString();
            }
            this._depotOut.InvioiceEmployee0 = this.newChooseInvoiceEmployee0.EditValue as Model.Employee;
            if (this._depotOut.InvioiceEmployee0 != null)
                this._depotOut.InvioiceEmployee0Id = this._depotOut.InvioiceEmployee0.EmployeeId;

            if (this._depotOut.EmployeeId == null)
                this._depotOut.EmployeeId = this._depotOut.InvioiceEmployee0Id;

            this._depotOut.description = this.memoEditdescription.Text;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditDepotOutDate.DateTime, new DateTime()))
                this._depotOut.DepotOutDate = global::Helper.DateTimeParse.NullDate;
            else
                this._depotOut.DepotOutDate = this.dateEditDepotOutDate.DateTime;

            //if (this.ProductCategoryButtonEdit.EditValue != null)
            //{
            //    this._depotOut.ProductCategory = this.ProductCategoryButtonEdit.EditValue as Model.ProductCategory;
            //    this._depotOut.ProductCategoryId = this._depotOut.ProductCategory.ProductCategoryId;
            //}
            this._depotOut.ParentProduct = this.txt_ParentProduct.Text;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._depotOutManager.Insert(this._depotOut);
                    break;
                case "update":
                    this._depotOutManager.Update(this._depotOut);
                    break;
            }

            base.Save();
        }

        public override void Refresh()
        {
            if (this._depotOut == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action != "insert")
                {
                    this._depotOut = this._depotOutManager.GetDetails(this._depotOut.DepotOutId);
                    if (this._depotOut == null)
                    {
                        this._depotOut = new Book.Model.DepotOut();
                        this._depotOut.DepotOutId = this._depotOutManager.GetId(DateTime.Now.Date);
                        this._depotOut.DepotOutDate = DateTime.Now;
                        this._depotOut.Details = new List<Model.DepotOutDetail>();
                        Model.DepotOutDetail detail = new Book.Model.DepotOutDetail();
                        detail.DepotOutDetailId = Guid.NewGuid().ToString();
                        detail.Product = new Model.Product();
                        this._depotOut.Details.Add(detail);
                        this.bindingSourceDepotOutDetail.Position = this.bindingSourceDepotOutDetail.IndexOf(detail);
                    }
                }
            }

            textBoxDepotOutId.Text = this._depotOut.DepotOutId;
            this.newChooseContorlEmployee.EditValue = this._depotOut.Employee;
            this.textBoxSourceType.Text = this._depotOut.SourceType;
            this.textBoxInvioiceId.Text = this._depotOut.InvioiceId;

            if (this._depotOut.SourceType == "領料單")
            {
                Model.ProduceMaterial ProduceMaterial = this.produceMaterialManager.Get(this._depotOut.InvioiceId);
                if (ProduceMaterial != null)
                {
                    //Model.PronoteHeader PronoteHeader = this.pronoteHeaderManager.Get(ProduceMaterial.InvoiceId);
                    //if (PronoteHeader != null)
                    //{
                    Model.InvoiceXO InvoiceXO = this.invoiceXOManager.Get(ProduceMaterial.InvoiceXOId);
                    this.textEditCostumeXOId.Text = InvoiceXO == null ? "" : InvoiceXO.CustomerInvoiceXOId;

                    if (ProduceMaterial.WorkHouse != null)
                        this.newChooseWorkHouse.EditValue = ProduceMaterial.WorkHouse;
                    // }
                }
                else
                {
                    this.textEditCostumeXOId.Text = "";
                    this.newChooseWorkHouse.EditValue = null;
                }
            }
            else if (this._depotOut.SourceType == "委外領料單")
            {
                Model.ProduceOtherMaterial ProduceOtherMaterial = new BL.ProduceOtherMaterialManager().Get(this._depotOut.InvioiceId);
                if (ProduceOtherMaterial != null)
                {
                    Model.ProduceOtherCompact ProduceOtherCompact = this.produceOtherCompactManager.Get(ProduceOtherMaterial.ProduceOtherCompactId);
                    if (ProduceOtherCompact != null)
                    {
                        if (!string.IsNullOrEmpty(ProduceOtherCompact.MRSHeaderId))
                        {
                            Model.MRSHeader mRSHeader = this.mRSHeaderManager.Get(ProduceOtherCompact.MRSHeaderId);
                            if (mRSHeader != null)
                            {
                                Model.MPSheader mPSheader = this.mPSheaderManager.Get(mRSHeader.MPSheaderId);
                                if (mPSheader != null)
                                {
                                    this.textEditCostumeXOId.Text = this.invoiceXOManager.Get(mPSheader.InvoiceXOId) == null ? "" : this.invoiceXOManager.Get(mPSheader.InvoiceXOId).CustomerInvoiceXOId;
                                }
                            }

                        }
                    }
                }
                else
                {
                    this.textEditCostumeXOId.Text = string.Empty;
                    this.newChooseWorkHouse.EditValue = null;
                }
            }
            else
                this.textEditCostumeXOId.Text = string.Empty;
            if (this._depotOut.DepotId != null)
                this.lookUpEditDepotId.EditValue = this._depotOut.DepotId;
            if (this.lookUpEditDepotId.EditValue == null)
                this.bindingSourceDepotPosition.DataSource = null;

            if (global::Helper.DateTimeParse.DateTimeEquls(this._depotOut.DepotOutDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditDepotOutDate.EditValue = null;
            }
            else
            {
                this.dateEditDepotOutDate.EditValue = this._depotOut.DepotOutDate;
            }

            //this.ProductCategoryButtonEdit.EditValue = this._depotOut.ProductCategory;
            //if (this._depotOut.ProductCategory != null)
            //    this.bindingSourceProduct.DataSource = this._productManager.Select(this._depotOut.ProductCategory);

            this.memoEditdescription.Text = this._depotOut.description;
            this.newChooseInvoiceEmployee0.EditValue = this._depotOut.InvioiceEmployee0;
            this.bindingSourceDepotOutDetail.DataSource = this._depotOut.Details;
            base.Refresh();
            this.newChooseInvoiceEmployee0.ButtonReadOnly = true;
            //this.textBoxSourceType.Properties.ReadOnly = true;
            //this.textBoxInvioiceId.Properties.ReadOnly = true;
            this.textBoxDepotOutId.Properties.ReadOnly = true;
            this.newChooseContorlEmployee.Enabled = false;

            this.newChooseContorlAuditEmp.EditValue = this._depotOut.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(this._depotOut.AuditState);

            this.txt_ParentProduct.Text = this._depotOut.ParentProduct;

            switch (this.action)
            {
                case "insert":
                    this.barButtonItem3.Enabled = true;
                    this.barButtonItem4.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.barButtonItem3.Enabled = true;
                    this.barButtonItem4.Enabled = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.barButtonItem3.Enabled = false;
                    this.barButtonItem4.Enabled = false;
                    break;
            }
            this.txt_ParentProduct.Properties.ReadOnly = true;
        }

        protected override void MoveNext()
        {
            Model.DepotOut depotOut = this._depotOutManager.GetNext(this._depotOut);
            if (depotOut == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._depotOut = this._depotOutManager.Get(depotOut.DepotOutId);
        }

        protected override void MovePrev()
        {
            Model.DepotOut depotOut = this._depotOutManager.GetPrev(this._depotOut);
            if (depotOut == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._depotOut = this._depotOutManager.Get(depotOut.DepotOutId);
        }

        protected override void MoveFirst()
        {
            this._depotOut = this._depotOutManager.Get(this._depotOutManager.GetFirst() == null ? "" : this._depotOutManager.GetFirst().DepotOutId);
        }

        protected override void MoveLast()
        {
            if (this.flag == 1)
            {
                this.flag = 0; return;
            }
            this._depotOut = this._depotOutManager.Get(this._depotOutManager.GetLast() == null ? "" : this._depotOutManager.GetLast().DepotOutId);
        }

        protected override bool HasRows()
        {
            return this._depotOutManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._depotOutManager.HasRowsAfter(this._depotOut);
        }

        protected override bool HasRowsPrev()
        {
            return this._depotOutManager.HasRowsBefore(this._depotOut);
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new Settings.StockLimitations.OutStockReport(_depotOut.DepotOutId);
        }

        protected override bool SetColumnNumber()
        {
            return true;
        }

        private void simpleButton_Append_Click(object sender, EventArgs e)
        {
            //if (ProductCategoryButtonEdit.EditValue != null)
            //{
            //    Model.ProductCategory produdctCate = this.ProductCategoryButtonEdit.EditValue as Model.ProductCategory;
            ChooseProductForm f = new ChooseProductForm();


            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(this._depotOut.Details[0].ProductId))
                    this._depotOut.Details.RemoveAt(0);
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {

                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {

                        this._depotOutDetail = new Book.Model.DepotOutDetail();
                        this._depotOutDetail.DepotOutDetailId = Guid.NewGuid().ToString();
                        this._depotOutDetail.Product = product;
                        this._depotOutDetail.Inumber = this._depotOut.Details.Count + 1;
                        this._depotOutDetail.ProductId = product.ProductId;
                        this._depotOutDetail.ProductUnit = product.ProduceUnit == null ? "" : product.ProduceUnit.CnName;
                        this._depotOutDetail.FormerPrice = 0;
                        this._depotOutDetail.CostPrice = 0;
                        this._depotOutDetail.TotalMoney = 0;
                        this._depotOutDetail.DepotOutId = this._depotOut.DepotOutId;
                        this._depotOutDetail.DepotOutDetailQuantity = 0;
                        this._depotOutDetail.Description = product.ProductDescription;
                        this._depotOutDetail.CurrentDepotQuantity = this.stockManager.GetTheCount1OfProductByProductId(product, this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                        this._depotOutDetail.CurrentStockQuantity = product == null ? 0 : product.StocksQuantity;
                        this._depotOut.Details.Add(this._depotOutDetail);


                    }
                }

                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {

                    Model.Product product = f.SelectedItem as Model.Product;
                    this._depotOutDetail = new Book.Model.DepotOutDetail();
                    this._depotOutDetail.DepotOutDetailId = Guid.NewGuid().ToString();
                    this._depotOutDetail.Inumber = this._depotOut.Details.Count + 1;
                    this._depotOutDetail.Product = product;
                    this._depotOutDetail.ProductId = product.ProductId;
                    this._depotOutDetail.ProductUnit = product.ProduceUnit == null ? "" : product.ProduceUnit.CnName;
                    this._depotOutDetail.FormerPrice = 0;
                    this._depotOutDetail.CostPrice = 0;
                    this._depotOutDetail.TotalMoney = 0;
                    this._depotOutDetail.DepotOutId = this._depotOut.DepotOutId;
                    this._depotOutDetail.DepotOutDetailQuantity = 0;
                    this._depotOutDetail.Description = product.ProductDescription;
                    this._depotOutDetail.CurrentDepotQuantity = this.stockManager.GetTheCount1OfProductByProductId(product, this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                    this._depotOutDetail.CurrentStockQuantity = product == null ? 0 : product.StocksQuantity;
                    this._depotOut.Details.Add(this._depotOutDetail);

                }
                //}
                this.gridControl1.RefreshDataSource();
                this.bindingSourceDepotOutDetail.Position = this.bindingSourceDepotOutDetail.IndexOf(this._depotOutDetail);
            }
            f.Dispose();
            System.GC.Collect();
        }

        private void lookUpEditDepotId_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditDepotId.EditValue != null)
            {
                this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select(this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                if (this._depotOut == null) return;
                foreach (var item in this._depotOut.Details == null ? new List<Model.DepotOutDetail>() : this._depotOut.Details)
                {
                    item.DepotPosition = null;
                    if (this.action != "view")
                    {
                        item.CurrentDepotQuantity = this.stockManager.GetTheCount1OfProductByProductId(item.Product, new BL.DepotManager().Get(this.lookUpEditDepotId.EditValue.ToString()));
                        item.CurrentStockQuantity = item.Product.StocksQuantity;
                    }
                    //    break;
                    this.gridControl1.RefreshDataSource();
                }

            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
            {
                Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.DepotOutDetail).Product;
                if (p == null) return;
                if (this.gridView1.FocusedColumn.Name == "gridColumnProductUnit")
                {
                    this.repositoryItemComboBox1.Items.Clear();
                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        IList<Model.ProductUnit> units = this._productUnitManager.Select(p.BasedUnitGroupId);
                        foreach (Model.ProductUnit ut in units)
                        {
                            this.repositoryItemComboBox1.Items.Add(ut.CnName);
                        }
                    }
                }

                //if (this.gridView1.FocusedColumn.Name == "gridColumnDepotPosition")
                //{
                //    this.repositoryItemComboBox2.Items.Clear();
                //    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                //    {
                //        IList<Model.DepotPosition> units = this.depotPositionManager.GetDepotPositionsByDepotAndProduct(p.ProductId, this.lookUpEditDepotId.EditValue.ToString());
                //        foreach (Model.DepotPosition ut in units)
                //        {
                //            this.repositoryItemComboBox2.Items.Add(ut);
                //        }
                //    }
                //}
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumnProId || e.Column == this.gridColumnProName)
            {
                Model.DepotOutDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.DepotOutDetail;
                if (detail != null)
                {

                    Model.Product p = this._productManager.Get(e.Value.ToString());

                    detail.CurrentDepotQuantity = this.stockManager.GetTheCount1OfProductByProductId(p, this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                    detail.CurrentStockQuantity = p == null ? 0 : p.StocksQuantity;

                    detail.DepotOutDetailId = Guid.NewGuid().ToString();
                    detail.DepotPositionId = null;
                    detail.DepotOutDetailQuantity = 1;
                    detail.CostPrice = null;
                    detail.TotalMoney = null;
                    detail.Product = p;
                    if (p != null)
                    {
                        detail.ProductId = p.ProductId;
                        detail.ProductUnit = p.DepotUnit.CnName;
                        detail.DepotOutId = this._depotOut.DepotOutId;
                        detail.Description = p.ProductDescription;
                    }
                    this.bindingSourceDepotOutDetail.Position = this.bindingSourceDepotOutDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            this._outDetail.HasOutQuantity = Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn1));
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (e.KeyData == Keys.Enter)
                {
                    Model.DepotOutDetail detail = new Model.DepotOutDetail();
                    detail.DepotOutDetailId = Guid.NewGuid().ToString();
                    detail.Inumber = this._depotOut.Details.Count + 1;
                    detail.DepotOutId = this._depotOut.DepotOutId;
                    detail.CostPrice = null;
                    detail.Description = "";
                    detail.FormerPrice = null;
                    detail.TotalMoney = null;
                    detail.ProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this._depotOut.Details.Add(detail);
                    this.bindingSourceDepotOutDetail.Position = this.bindingSourceDepotOutDetail.IndexOf(detail);
                }
                if (e.KeyData == Keys.Delete)
                {
                    this.simpleButton_Minus.PerformClick();
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private bool CanAdd(IList<Model.DepotOutDetail> list)
        {
            foreach (Model.DepotOutDetail detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }

        private void simpleButton_Minus_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDepotOutDetail.Current != null)
            {
                this._depotOut.Details.Remove(this.bindingSourceDepotOutDetail.Current as Book.Model.DepotOutDetail);
                if (this._depotOut.Details.Count == 0)
                {
                    Model.DepotOutDetail detail = new Model.DepotOutDetail();
                    detail.DepotOutDetailId = Guid.NewGuid().ToString();

                    detail.CostPrice = null;
                    detail.Description = "";
                    detail.FormerPrice = null;
                    detail.TotalMoney = null;
                    detail.ProductUnit = "";
                    detail.Product = new Book.Model.Product();
                    this._depotOut.Details.Add(detail);
                    this.bindingSourceDepotOutDetail.Position = this.bindingSourceDepotOutDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.DepotOutDetail> details = this.bindingSourceDepotOutDetail.DataSource as IList<Model.DepotOutDetail>;
            if (details == null || details.Count < 1) return;
            Model.DepotOutDetail temp = details[e.ListSourceRowIndex];
            if (temp.Product == null) return;
            switch (e.Column.Name)
            {
                //case "gridColumnStockQuantity":
                //    e.DisplayText = product.StocksQuantity == null ? "" : product.StocksQuantity.Value.ToString();
                //    break;
                case "gridColumnProductDescription":
                    e.DisplayText = temp.Product.ProductDescription;
                    break;
                case "gridColumnNoQuantity":
                    if (string.IsNullOrEmpty(this._depotOut.InvioiceId)) return;
                    if (this._depotOut.SourceType == "領料單" && !string.IsNullOrEmpty(temp.ProduceMaterialdetailsID))
                    {
                        //Model.ProduceMaterialdetails produce = this.produceMaterialDetailManager.SelectByProductIdAndHeadId(temp.Product.ProductId, this._depotOut.InvioiceId);
                        Model.ProduceMaterialdetails produce = this.produceMaterialDetailManager.Get(temp.ProduceMaterialdetailsID);
                        if (produce == null)
                        {
                            e.DisplayText = "0";
                            return;
                        }
                        e.DisplayText = (produce.Materialprocessum - (!produce.Materialprocesedsum.HasValue ? 0 : produce.Materialprocesedsum)) < 0 ? "0" : (produce.Materialprocessum - (produce.Materialprocesedsum.HasValue ? produce.Materialprocesedsum : 0)).ToString();
                    }
                    else if (this._depotOut.SourceType == "委外領料單" && !string.IsNullOrEmpty(temp.ProduceOtherMaterialDetailId))
                    {
                        Model.ProduceOtherMaterialDetail OtherMaterial = this.produceOtherMaterialDetailManager.Get(temp.ProduceOtherMaterialDetailId);
                        if (OtherMaterial == null)
                        {
                            e.DisplayText = "0";
                            return;
                        }
                        e.DisplayText = (OtherMaterial.OtherMaterialQuantity - (OtherMaterial.OtherMaterialALLUserQuantity.HasValue ? OtherMaterial.OtherMaterialALLUserQuantity : 0)) < 0 ? "0" : (OtherMaterial.OtherMaterialQuantity - (OtherMaterial.OtherMaterialALLUserQuantity.HasValue ? OtherMaterial.OtherMaterialALLUserQuantity : 0)).ToString();
                    }
                    break;
                case "gridColumnDescription":
                    Model.Stock stock = this.stockManager.GetStockByProductIdAndDepotPositionId(temp.Product.ProductId, temp.DepotPositionId);
                    e.DisplayText = stock == null ? "" : stock.Descriptions;
                    break;
                case "gridColumnProId":
                    e.DisplayText = temp.Product.Id;
                    break;
                case "gridColumnCustomerProductName":
                    e.DisplayText = temp.Product.CustomerProductName;
                    break;

            }
        }

        private void lookUpEditDepotId_Click(object sender, EventArgs e)
        {
            this.bindingSourceDepot.DataSource = this._depotManager.Select();
        }

        //bool isEnter = false;
        //private void ProductCategoryButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    Model.ProductCategory produdctCate = this.ProductCategoryButtonEdit.EditValue as Model.ProductCategory;
        //    if (this.action != "view")
        //    {
        //        Settings.BasicData.ProductCategories.ChooseForm f = new Settings.BasicData.ProductCategories.ChooseForm();
        //        if (f.ShowDialog(this) == DialogResult.OK)
        //        {
        //            if (f.SelectedItem as Model.ProductCategory != produdctCate)
        //            {

        //                if (this._depotOut.Details.Count != 0)
        //                {
        //                    foreach (Model.DepotOutDetail item in this._depotOut.Details)
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
        //                            //this._depotOut = null;
        //                            this.ProductCategoryButtonEdit.EditValue = f.SelectedItem as Model.ProductCategory;
        //                            this.bindingSourceProduct.DataSource = this._productManager.Select(f.SelectedItem as Model.ProductCategory);
        //                            //this.AddNew();
        //                            this._depotOut.ProductCategoryId = (this.ProductCategoryButtonEdit.EditValue as Model.ProductCategory).ProductCategoryId;


        //                            textBoxDepotOutId.Text = this._depotOut.DepotOutId;
        //                            if (this._depotOut.EmployeeId != null)
        //                            {
        //                                employee = this._employeeManager.Get(this._depotOut.EmployeeId);
        //                                if (employee != null)
        //                                    this.newChooseContorlEmployee.EditValue = employee;
        //                            }
        //                            this.textBoxSourceType.Text = this._depotOut.SourceType;
        //                            this.textBoxInvioiceId.Text = this._depotOut.InvioiceId;

        //                            if (this._depotOut.DepotId != null)
        //                                this.lookUpEditDepotId.EditValue = this._depotOut.DepotId;

        //                            if (global::Helper.DateTimeParse.DateTimeEquls(this._depotOut.DepotOutDate, global::Helper.DateTimeParse.NullDate))
        //                            {
        //                                this.dateEditDepotOutDate.EditValue = null;
        //                            }
        //                            else
        //                            {
        //                                this.dateEditDepotOutDate.EditValue = this._depotOut.DepotOutDate;
        //                            }

        //                            this.ProductCategoryButtonEdit.EditValue = this._depotOut.ProductCategory;
        //                            if (this._depotOut.ProductCategory != null)
        //                                this.bindingSourceProduct.DataSource = this._productManager.Select(this._depotOut.ProductCategory);

        //                            this.memoEditdescription.Text = this._depotOut.description;


        //                            this._depotOut.Details.Clear();
        //                            Model.DepotOutDetail detail = new Book.Model.DepotOutDetail();
        //                            detail.DepotOutDetailId = Guid.NewGuid().ToString();
        //                            detail.Product = new Model.Product();
        //                            this._depotOut.Details.Add(detail);
        //                            this.bindingSourceDepotOutDetail.Position = this.bindingSourceDepotOutDetail.IndexOf(detail);

        //                            this.gridControl1.RefreshDataSource();
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                else
        //                {
        //                    this.ProductCategoryButtonEdit.EditValue = f.SelectedItem as Model.ProductCategory;
        //                    this.bindingSourceProduct.DataSource = this._productManager.Select(f.SelectedItem as Model.ProductCategory);
        //                }
        //                isEnter = false;
        //            }
        //        }
        //    }
        //}

        private void repositoryItemLookUpEdit3_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.lookUpEditDepotId.EditValue == null)
            {
                MessageBox.Show("請選擇所屬庫房！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.lookUpEditDepotId.Focus();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "gridColumn1")
            {
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnDepotStock, Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnDepotStock)) + Convert.ToDouble(this._outDetail.HasOutQuantity) - Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn1)));
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnStockQuantity, Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnStockQuantity)) + Convert.ToDouble(this._outDetail.HasOutQuantity) - Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn1)));
                this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnNoQuantity, Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumnNoQuantity)) + Convert.ToDouble(this._outDetail.HasOutQuantity) - Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn1)));
            }

            if (e.Column.Name == "gridColumnDepotPosition")
            {
                Model.DepotOutDetail model = this.bindingSourceDepotOutDetail.Current as Model.DepotOutDetail;
                //if (string.IsNullOrEmpty(model.SourceTYpe))
                {
                    this.gridView1.SetRowCellValue(e.RowHandle, this.gridColumnDepotStock, stockManager.SelectStockQuantity1(model.ProductId, model.DepotPositionId) - Convert.ToDouble(this.gridView1.GetRowCellValue(e.RowHandle, this.gridColumn1)) + Convert.ToDouble(this._outDetail.HasOutQuantity));
                }
            }
        }

        //选择领料单
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((this.bindingSourceDepotOutDetail.Current as Model.DepotOutDetail) != null && string.IsNullOrEmpty((this.bindingSourceDepotOutDetail.Current as Model.DepotOutDetail).ProductId))
                this._depotOut.Details.Remove((this.bindingSourceDepotOutDetail.Current as Model.DepotOutDetail));
            if (this.lookUpEditDepotId.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.deptNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.lookUpEditDepotId.Focus();
                return;
            }

            TakeMaterialChooseForm takeForm = new TakeMaterialChooseForm();
            if (takeForm.ShowDialog(this) != DialogResult.OK) return;
            if (_produceMaterial.Details.Count == 0) return;
            //this._depotOut.Details.Clear();

            //Model.PronoteHeader PronoteHeader = this.pronoteHeaderManager.Get(_produceMaterial.Details[0].ProduceMaterial.InvoiceId);
            //if (PronoteHeader != null)
            //{
            //    Model.InvoiceXO InvoiceXO = this.invoiceXOManager.Get(PronoteHeader.InvoiceXOId);
            //    this.textEditCostumeXOId.Text = InvoiceXO == null ? "" : InvoiceXO.CustomerInvoiceXOId;
            //}

            this.textBoxSourceType.Text = "領料單";
            this.textBoxInvioiceId.Text = _produceMaterial.Details[0].ProduceMaterialID;
            this._depotOut.SourceType = "領料單";
            //this.textBoxInvioiceId.Text =
            this._depotOut.InvioiceId = _produceMaterial.Details[0].ProduceMaterialID;
            Model.ProduceMaterial ProduceMaterial = this.produceMaterialManager.Get(this._depotOut.InvioiceId);
            if (ProduceMaterial != null)
            {
                Model.InvoiceXO InvoiceXO = this.invoiceXOManager.Get(ProduceMaterial.InvoiceXOId);
                this.textEditCostumeXOId.Text = InvoiceXO == null ? "" : InvoiceXO.CustomerInvoiceXOId;

                if (ProduceMaterial.WorkHouse != null)
                    this.newChooseWorkHouse.EditValue = ProduceMaterial.WorkHouse;
            }
            if (!string.IsNullOrEmpty(_produceMaterial.Details[0].ProduceMaterial.InvoiceId))
            {
                Model.PronoteHeader pronoteHeader = this.pronoteHeaderManager.Get(_produceMaterial.Details[0].ProduceMaterial.InvoiceId);
                if (pronoteHeader != null)
                {
                    if (pronoteHeader.Product != null)
                        this.txt_ParentProduct.Text = string.IsNullOrEmpty(pronoteHeader.Product.CustomerProductName) ? pronoteHeader.Product.ProductName : pronoteHeader.Product.ProductName + "{" + pronoteHeader.Product.CustomerProductName + "}";

                }
                else
                    this.txt_ParentProduct.Text = "";
            }
            else
                this.txt_ParentProduct.Text = "";

            this.newChooseInvoiceEmployee0.EditValue = _produceMaterial.Details[0].ProduceMaterial.Employee0;
            //this.newChooseWorkHouse.EditValue = _produceMaterial.WorkHouse;
            double? NotOutDepotNumber = 0;
            foreach (Model.ProduceMaterialdetails item in _produceMaterial.Details)
            {
                NotOutDepotNumber = (item.Materialprocessum - (item.Materialprocesedsum == null ? 0 : item.Materialprocesedsum.Value)) < 0 ? 0 : (item.Materialprocessum - (item.Materialprocesedsum == null ? 0 : item.Materialprocesedsum.Value));
                IList<Model.Stock> tempstock = this.stockManager.SelectNotZeroByPidAndDid(item.ProductId, this.lookUpEditDepotId.EditValue.ToString());
                if (tempstock != null && tempstock.Count > 0)
                {
                    foreach (Model.Stock stock in tempstock)
                    {
                        this._depotOutDetail = new Book.Model.DepotOutDetail();
                        this._depotOutDetail.DepotOutDetailId = Guid.NewGuid().ToString();
                        this._depotOutDetail.Inumber = this._depotOut.Details.Count + 1;
                        this._depotOutDetail.DepotPositionId = stock.DepotPositionId;
                        this._depotOutDetail.DepotPosition = stock.DepotPosition;
                        this._depotOutDetail.ProductId = item.ProductId;
                        this._depotOutDetail.Product = item.Product;
                        this._depotOutDetail.CGDate = this.cgdetailManager.SelectLastInvoiceCGDate(item.ProductId, stock.DepotPositionId);
                        this._depotOutDetail.HandbookId = item.HandbookId;
                        this._depotOutDetail.HandbookProductId = item.HandbookProductId;
                        this._depotOutDetail.SourceTYpe = "領料單";
                        this._depotOutDetail.InvoiceId = item.ProduceMaterialID;
                        this._depotOutDetail.Pihao = item.Pihao;
                        if (global::Helper.DateTimeParse.DateTimeEquls(this._depotOutDetail.CGDate, new DateTime()))
                        {
                            this._depotOutDetail.CGDate = null;
                        }

                        if (NotOutDepotNumber != 0)
                        {
                            if (stock.StockQuantity1.Value > NotOutDepotNumber)
                            {
                                this._depotOutDetail.DepotOutDetailQuantity = NotOutDepotNumber;
                                NotOutDepotNumber = 0;
                            }
                            else
                            {
                                this._depotOutDetail.DepotOutDetailQuantity = stock.StockQuantity1.Value;
                                NotOutDepotNumber -= stock.StockQuantity1.Value;
                            }
                        }
                        else
                            this._depotOutDetail.DepotOutDetailQuantity = 0;
                        this._depotOutDetail.StockDesc = stock.Descriptions;
                        this._depotOutDetail.ProductUnit = item.ProductUnit;
                        this._depotOutDetail.ProduceMaterialdetailsID = item.ProduceMaterialdetailsID;
                        this._depotOutDetail.InvoiceXOId = item.InvoiceXOId;
                        this._depotOutDetail.CurrentDepotQuantity = stock.StockQuantity1 - this._depotOutDetail.DepotOutDetailQuantity; //this.stockManager.GetTheCount1OfProductByProductId(item.Product, this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                        this._depotOutDetail.CurrentStockQuantity = (item.Product == null ? 0 : item.Product.StocksQuantity) - this._depotOutDetail.DepotOutDetailQuantity;
                        this._depotOutDetail.DepotPositionDesc = "0";
                        this._depotOut.Details.Add(this._depotOutDetail);
                    }
                }
                else
                {
                    this._depotOutDetail = new Book.Model.DepotOutDetail();
                    this._depotOutDetail.DepotOutDetailId = Guid.NewGuid().ToString();
                    this._depotOutDetail.Inumber = this._depotOut.Details.Count + 1;
                    //this._depotOutDetail.DepotPositionId = stock.DepotPositionId;
                    //this._depotOutDetail.DepotPosition = stock.DepotPosition;
                    this._depotOutDetail.ProductId = item.ProductId;
                    this._depotOutDetail.Product = item.Product;
                    //this._depotOutDetail.CGDate = this.cgdetailManager.SelectLastInvoiceCGDate(item.ProductId, stock.DepotPositionId);
                    this._depotOutDetail.HandbookId = item.HandbookId;
                    this._depotOutDetail.HandbookProductId = item.HandbookProductId;
                    this._depotOutDetail.SourceTYpe = "領料單";
                    this._depotOutDetail.InvoiceId = item.ProduceMaterialID;
                    this._depotOutDetail.DepotOutDetailQuantity = 0;
                    //this._depotOutDetail.StockDesc = stock.Descriptions;
                    this._depotOutDetail.ProductUnit = item.ProductUnit;
                    this._depotOutDetail.ProduceMaterialdetailsID = item.ProduceMaterialdetailsID;
                    this._depotOutDetail.InvoiceXOId = item.InvoiceXOId;
                    this._depotOutDetail.Pihao = item.Pihao;
                    //this._depotOutDetail.CurrentDepotQuantity = stock.StockQuantity1 - this._depotOutDetail.DepotOutDetailQuantity; //this.stockManager.GetTheCount1OfProductByProductId(item.Product, this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                    this._depotOutDetail.CurrentStockQuantity = (item.Product == null ? 0 : item.Product.StocksQuantity) - this._depotOutDetail.DepotOutDetailQuantity;
                    this._depotOutDetail.DepotPositionDesc = "0";

                    this._depotOut.Details.Add(this._depotOutDetail);
                }
            }
            this.gridControl1.RefreshDataSource();
            takeForm.Dispose();
            GC.Collect();
        }

        //选择委外领料单
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ((this.bindingSourceDepotOutDetail.Current as Model.DepotOutDetail) != null && string.IsNullOrEmpty((this.bindingSourceDepotOutDetail.Current as Model.DepotOutDetail).ProductId))
                this._depotOut.Details.Remove((this.bindingSourceDepotOutDetail.Current as Model.DepotOutDetail));
            if (this.lookUpEditDepotId.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.deptNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.lookUpEditDepotId.Focus();
                return;
            }

            produceManager.ProduceOtherMaterial.ChooseProduceOtherMaterialForm takeForm = new produceManager.ProduceOtherMaterial.ChooseProduceOtherMaterialForm();
            if (takeForm.ShowDialog(this) != DialogResult.OK) return;

            if (_ProduceOtherMaterial.Details.Count == 0) return;
            //this._depotOut.Details.Clear();

            this.textBoxSourceType.Text = "委外領料單";
            this.textBoxInvioiceId.Text = _ProduceOtherMaterial.Details[0].ProduceOtherMaterialId;
            this._depotOut.SourceType = "委外領料單";
            //this.textBoxInvioiceId.Text = 
            this._depotOut.InvioiceId = _ProduceOtherMaterial.Details[0].ProduceOtherMaterialId;
            Model.ProduceOtherCompact ProduceOtherCompact = this.produceOtherCompactManager.Get(_ProduceOtherMaterial.Details[0].ProduceOtherMaterial.ProduceOtherCompactId);
            if (ProduceOtherCompact != null)
            {
                if (!string.IsNullOrEmpty(ProduceOtherCompact.MRSHeaderId))
                {
                    Model.MRSHeader mRSHeader = this.mRSHeaderManager.Get(ProduceOtherCompact.MRSHeaderId);
                    if (mRSHeader != null)
                    {
                        Model.MPSheader mPSheader = this.mPSheaderManager.Get(mRSHeader.MPSheaderId);
                        if (mPSheader != null)
                        {
                            this.textEditCostumeXOId.Text = this.invoiceXOManager.Get(mPSheader.InvoiceXOId) == null ? "" : this.invoiceXOManager.Get(mPSheader.InvoiceXOId).CustomerInvoiceXOId;
                        }
                    }
                }
            }
            this.newChooseInvoiceEmployee0.EditValue = _ProduceOtherMaterial.Employee0;
            double? NotOutDepotNumber = 0;
            foreach (Model.ProduceOtherMaterialDetail item in _ProduceOtherMaterial.Details)
            {
                NotOutDepotNumber = (item.OtherMaterialQuantity - (item.OtherMaterialALLUserQuantity.HasValue ? item.OtherMaterialALLUserQuantity : 0)) < 0 ? 0 : (item.OtherMaterialQuantity - (item.OtherMaterialALLUserQuantity.HasValue ? item.OtherMaterialALLUserQuantity : 0));
                IList<Model.Stock> tempstock = this.stockManager.SelectNotZeroByPidAndDid(item.ProductId, this.lookUpEditDepotId.EditValue.ToString());
                if (tempstock != null && tempstock.Count > 0)
                {
                    foreach (Model.Stock stock in tempstock)
                    {
                        this._depotOutDetail = new Book.Model.DepotOutDetail();
                        this._depotOutDetail.DepotOutDetailId = Guid.NewGuid().ToString();
                        this._depotOutDetail.Inumber = this._depotOut.Details.Count + 1;
                        this._depotOutDetail.DepotPosition = stock.DepotPosition;
                        this._depotOutDetail.DepotPositionId = stock.DepotPositionId;
                        this._depotOutDetail.ProductId = item.ProductId;
                        this._depotOutDetail.Product = item.Product;
                        this._depotOutDetail.DepotPositionDesc = "0";
                        this._depotOutDetail.CGDate = this.cgdetailManager.SelectLastInvoiceCGDate(item.ProductId, stock.DepotPositionId);
                        _depotOutDetail.HandbookId = item.HandbookId;
                        _depotOutDetail.HandbookProductId = item.HandbookProductId;
                        _depotOutDetail.SourceTYpe = "委外領料單";
                        _depotOutDetail.InvoiceId = item.ProduceOtherMaterialId;
                        this._depotOutDetail.Pihao = item.PiHao;
                        if (global::Helper.DateTimeParse.DateTimeEquls(this._depotOutDetail.CGDate, new DateTime()))
                        {
                            this._depotOutDetail.CGDate = null;
                        }

                        if (NotOutDepotNumber != 0)
                        {
                            if (stock.StockQuantity1.Value > NotOutDepotNumber)
                            {
                                this._depotOutDetail.DepotOutDetailQuantity = NotOutDepotNumber;
                                NotOutDepotNumber = 0;
                            }
                            else
                            {
                                this._depotOutDetail.DepotOutDetailQuantity = stock.StockQuantity1.Value;
                                NotOutDepotNumber -= stock.StockQuantity1.Value;
                            }
                        }
                        else
                            this._depotOutDetail.DepotOutDetailQuantity = 0;
                        this._depotOutDetail.StockDesc = stock.Descriptions;
                        this._depotOutDetail.ProductUnit = item.ProductUnit;
                        this._depotOutDetail.ProduceOtherMaterialDetailId = item.ProduceOtherMaterialDetailId;
                        this._depotOutDetail.InvoiceXOId = item.InvoiceXOId;
                        this._depotOutDetail.CurrentDepotQuantity = stock.StockQuantity1 - this._depotOutDetail.DepotOutDetailQuantity; //this.stockManager.GetTheCount1OfProductByProductId(item.Product, this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                        this._depotOutDetail.CurrentStockQuantity = (item.Product == null ? 0 : item.Product.StocksQuantity) - this._depotOutDetail.DepotOutDetailQuantity;
                        //this._depotOutDetail.CurrentDepotQuantity = this.stockManager.GetTheCount1OfProductByProductId(item.Product, this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                        //this._depotOutDetail.CurrentStockQuantity = item.Product == null ? 0 : item.Product.StocksQuantity;
                        this._depotOut.Details.Add(this._depotOutDetail);
                    }
                }
                else
                {
                    this._depotOutDetail = new Book.Model.DepotOutDetail();
                    this._depotOutDetail.DepotOutDetailId = Guid.NewGuid().ToString();
                    this._depotOutDetail.Inumber = this._depotOut.Details.Count + 1;
                    //this._depotOutDetail.DepotPosition = stock.DepotPosition;
                    //this._depotOutDetail.DepotPositionId = stock.DepotPositionId;
                    this._depotOutDetail.ProductId = item.ProductId;
                    this._depotOutDetail.Product = item.Product;
                    this._depotOutDetail.DepotPositionDesc = "0";
                    //this._depotOutDetail.CGDate = this.cgdetailManager.SelectLastInvoiceCGDate(item.ProductId, stock.DepotPositionId);
                    _depotOutDetail.HandbookId = item.HandbookId;
                    _depotOutDetail.HandbookProductId = item.HandbookProductId;
                    _depotOutDetail.SourceTYpe = "委外領料單";
                    _depotOutDetail.InvoiceId = item.ProduceOtherMaterialId;
                    this._depotOutDetail.DepotOutDetailQuantity = 0;
                    //this._depotOutDetail.StockDesc = stock.Descriptions;
                    this._depotOutDetail.ProductUnit = item.ProductUnit;
                    this._depotOutDetail.ProduceOtherMaterialDetailId = item.ProduceOtherMaterialDetailId;
                    this._depotOutDetail.InvoiceXOId = item.InvoiceXOId;
                    //this._depotOutDetail.CurrentDepotQuantity = stock.StockQuantity1 - this._depotOutDetail.DepotOutDetailQuantity;
                    //this.stockManager.GetTheCount1OfProductByProductId(item.Product, this._depotManager.Get(this.lookUpEditDepotId.EditValue.ToString()));
                    this._depotOutDetail.CurrentStockQuantity = (item.Product == null ? 0 : item.Product.StocksQuantity) - this._depotOutDetail.DepotOutDetailQuantity;

                    this._depotOut.Details.Add(this._depotOutDetail);
                }
            }
            this.gridControl1.RefreshDataSource();
            takeForm.Dispose();
            GC.Collect();
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void gridView1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseOutStockDepot form = new ChooseOutStockDepot();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this._depotOut = this._depotOutManager.GetDetails((form.SelectedItem as Model.DepotOutDetail) == null ? "" : (form.SelectedItem as Model.DepotOutDetail).DepotOutId);
                this.Refresh();
            }
        }

        //查看库存
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            Model.DepotOutDetail _dod = this.bindingSourceDepotOutDetail.Current as Model.DepotOutDetail;
            ViewOutInRecord f = new ViewOutInRecord(_dod.ProductId);
            f.ShowDialog();
        }


        #region 审核

        protected override string AuditKeyId()
        {
            return Model.DepotOut.PRO_DepotOutId;
        }

        protected override int AuditState()
        {
            return this._depotOut.AuditState.HasValue ? this._depotOut.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "DepotOut" + "," + this._depotOut.DepotOutId;
        }

        #endregion

        private void dateEditDepotOutDate_EditValueChanged(object sender, EventArgs e)
        {
            if (this.dateEditDepotOutDate.DateTime != null && this.action == "insert")
            {
                this._depotOut.DepotOutId = this._depotOutManager.GetId(this.dateEditDepotOutDate.DateTime);
                this.textBoxDepotOutId.Text = this._depotOut.DepotOutId;
            }
        }
    }
}