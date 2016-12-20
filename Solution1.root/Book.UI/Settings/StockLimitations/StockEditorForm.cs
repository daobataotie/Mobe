using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Settings.BasicData.Employees;
using System.Linq;

namespace Book.UI.Settings.StockLimitations
{
    public partial class StockEditorForm : Settings.BasicData.BaseEditForm
    {



        #region 变量对象定义
        public static Model.StockEditor _stockEditor;
        public static double? SetNums = 0;
        public static IList<Model.Product> productlist = new List<Model.Product>();
        public static Dictionary<string, Model.StockEditorDetal> dic = new Dictionary<string, Model.StockEditorDetal>();
        private BL.StockEditorManager _stockEditorManager = new Book.BL.StockEditorManager();
        private BL.StockEditorDetalManager _stockEditorDetailManager = new Book.BL.StockEditorDetalManager();
        private BL.DepotManager _depotManager = new Book.BL.DepotManager();
        private Model.StockEditorDetal _stockEditorDetail;
        private BL.ProductCategoryManager _productCategoryManager = new Book.BL.ProductCategoryManager();
        private BL.ProductManager _productManager = new Book.BL.ProductManager();
        private Model.ProductCategory category = new Book.Model.ProductCategory();
        private BL.EmployeeManager _employeeManager = new Book.BL.EmployeeManager();
        private BL.ProductUnitManager _productUnitManager = new Book.BL.ProductUnitManager();
        private BL.ProductUnitManager productUnitManager = new BL.ProductUnitManager();
        private IList<Model.ProductCategory> cateList = new List<Model.ProductCategory>();
        #endregion

        public StockEditorForm()
        {
            InitializeComponent();
            this.newChooseContorlEmployeeId.Choose = new ChooseEmployee();
            this.newChooseContorlEmployee0Id.Choose = new ChooseEmployee();
            this.bindingSourceDepot.DataSource = this._depotManager.Select();
            cateList = this._productCategoryManager.Select();
            //category = cateList.First<Model.ProductCategory>();            
            //productlist = this._productManager.DataReaderBind<Model.Product>("select DepotUnitId,Id,ProductId,ProductName,CustomerProductName,StocksQuantity from product where ProductCategoryId='" + category.ProductCategoryId + "'");
            this.action = "insert";
        }

        protected override void AddNew()
        {
            dic.Clear();
            productlist.Clear();
            _stockEditor = null;
        }

        protected override void Save()
        {
            _stockEditor.StockEditorId = this.textEditStockEditorId.Text;
            _stockEditor.StockEditorDate = this.dateEditStockEditorDate.DateTime;
            if (this.newChooseContorlEmployee0Id.EditValue != null)
            {
                _stockEditor.Employee0Id = (this.newChooseContorlEmployee0Id.EditValue as Model.Employee).EmployeeId;
            }
            if (this.newChooseContorlEmployeeId.EditValue != null)
            {
                _stockEditor.EmployeeId = (this.newChooseContorlEmployeeId.EditValue as Model.Employee).EmployeeId;
            }
            //if (this.newChooseContorlSupplier.EditValue != null)
            //    _stockEditor.SupplierId = (this.newChooseContorlSupplier.EditValue as Model.Supplier).SupplierId;
            if (this.lookUpEditDepotId.EditValue != null)
                _stockEditor.DepotId = (this.lookUpEditDepotId.EditValue as Model.Depot).DepotId;
            if (this.buttonEditProductCategoryId.EditValue != null)
            {
                _stockEditor.ProductCategoryId = (this.buttonEditProductCategoryId.EditValue as Model.ProductCategory).ProductCategoryId;
            }
            _stockEditor.Directions = memoEditDirections.Text;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this._stockEditorManager.Insert(_stockEditor);
                    break;
                case "update":
                    this._stockEditorManager.Update(_stockEditor);
                    break;
            }
        }

        public override void Refresh()
        {
            if (_stockEditor == null)
            {
                _stockEditor = new Model.StockEditor();
                _stockEditor.StockEditorId = this._stockEditorManager.GetId(DateTime.Now);
                _stockEditor.StockEditorDate = DateTime.Now;
                _stockEditor.Employee0 = BL.V.ActiveOperator.Employee;
                //if (this.cateList.Count != 0)
                //    category = cateList[0];
                _stockEditor.Details = new List<Model.StockEditorDetal>();
                _stockEditor.ProductPositionNums.Clear();
                this.action = "insert";
            }
            else
            {
                _stockEditor.Details.Clear();
                if (this.action == "insert")
                {
                    foreach (Model.Product product in productlist)
                    {
                        Model.StockEditorDetal detail = new Book.Model.StockEditorDetal();
                        detail.StockEditorDetalId = Guid.NewGuid().ToString();
                        detail.StockEditorId = _stockEditor.StockEditorId;
                        detail.ProductName = product.ProductName;
                        detail.ProductId = product.ProductId;
                        detail.Id = product.Id;
                        detail.CustomerProductName = product.CustomerProductName;
                        detail.ProductId = product.ProductId;
                        detail.DepotPosition = product.DepotPosition;
                        detail.DepotPositionId = product.DepotPositionId;
                        detail.ProductDesc = product.ProductDescription;
                        detail.ProductVersion = product.ProductVersion;
                        //object nums = this._stockEditorDetailManager.SelectByProductIdAndStockHId(detail.ProductId, detail.StockEditorId);
                        //if (nums != null)
                        //    detail.StockEditorQuantity = Convert.ToDouble(nums);
                        //else
                        detail.StockEditorQuantity = null;
                        detail.DepotPositionId = product.DepotPositionId;
                        detail.ProductUnitName = this.productUnitManager.Get(product.DepotUnitId).CnName == null ? null : this.productUnitManager.Get(product.DepotUnitId).CnName;
                        _stockEditor.Details.Add(detail);
                        // this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                    }
                }
                else
                {
                    foreach (Model.Product product in productlist)
                    {
                        Model.StockEditorDetal detail = new Book.Model.StockEditorDetal();
                        detail.StockEditorDetalId = Guid.NewGuid().ToString();
                        detail.StockEditorId = _stockEditor.StockEditorId;
                        detail.ProductName = product.ProductName;
                        detail.ProductId = product.ProductId;
                        detail.Id = product.Id;
                        detail.CustomerProductName = product.CustomerProductName;
                        detail.ProductId = product.ProductId;
                        detail.DepotPosition = product.DepotPosition;
                        detail.DepotPositionId = product.DepotPositionId;
                        detail.ProductDesc = product.ProductDescription;
                        detail.ProductVersion = product.ProductVersion;
                        object nums = this._stockEditorDetailManager.SelectByProductIdAndStockHId(detail.ProductId, detail.StockEditorId);
                        if (nums != null)
                            detail.StockEditorQuantity = Convert.ToDouble(nums);
                        else
                            detail.StockEditorQuantity = null;
                        detail.DepotPositionId = product.DepotPositionId;
                        detail.ProductUnitName = this.productUnitManager.Get(product.DepotUnitId) == null ? null : this.productUnitManager.Get(product.DepotUnitId).CnName;
                        _stockEditor.Details.Add(detail);
                    }
                }
            }
            this.memoEditDirections.Text = _stockEditor.Directions;
            this.textEditStockEditorId.Text = _stockEditor.StockEditorId;
            this.dateEditStockEditorDate.DateTime = _stockEditor.StockEditorDate.Value;
            this.newChooseContorlEmployee0Id.EditValue = _stockEditor.Employee0;
            this.newChooseContorlEmployeeId.EditValue = _stockEditor.Employee;
            //if (_stockEditor.Supplier != null)
            //    this.newChooseContorlSupplier.EditValue = _stockEditor.Supplier;
            //else
            //    this.newChooseContorlSupplier.EditValue = null;

            this.lookUpEditDepotId.EditValue = this._depotManager.Get(_stockEditor.DepotId);
            if (_stockEditor.ProductCategoryId != null)
                this.buttonEditProductCategoryId.EditValue = this._productCategoryManager.Get(_stockEditor.ProductCategoryId);
            else
                this.buttonEditProductCategoryId.EditValue = category;
            this.bindingSourceDetail.DataSource = _stockEditor.Details;
            this.gridControl1.RefreshDataSource();
            base.Refresh();

            switch (this.action)
            {
                case "insert":
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.textEditStockEditorId.Properties.ReadOnly = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.textEditStockEditorId.Properties.ReadOnly = true;
                    break;
            }

            this.barButtonItem1.Caption = "共" + this.bindingSourceDetail.Count + "項";
        }


        protected override void MoveNext()
        {
            Model.StockEditor stockEditor = this._stockEditorManager.GetNext(_stockEditor);
            if (stockEditor == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _stockEditor = this._stockEditorManager.Get(stockEditor.StockEditorId);
        }

        protected override void MovePrev()
        {
            Model.StockEditor stockEditor = this._stockEditorManager.GetPrev(_stockEditor);
            if (stockEditor == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            _stockEditor = this._stockEditorManager.Get(stockEditor.StockEditorId);
        }

        protected override void MoveFirst()
        {
            _stockEditor = this._stockEditorManager.Get(this._stockEditorManager.GetFirst() == null ? "" : this._stockEditorManager.GetFirst().StockEditorId);
        }

        protected override void MoveLast()
        {
            _stockEditor = this._stockEditorManager.Get(this._stockEditorManager.GetLast() == null ? "" : this._stockEditorManager.GetLast().StockEditorId);
        }

        protected override bool HasRows()
        {
            return this._stockEditorManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this._stockEditorManager.HasRowsAfter(_stockEditor);
        }

        protected override bool HasRowsPrev()
        {
            return this._stockEditorManager.HasRowsBefore(_stockEditor);
        }

        private void lookUpEditDepotId_Click(object sender, EventArgs e)
        {
            this.bindingSourceDepot.DataSource = this._depotManager.Select();
        }

        protected override void Delete()
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this._stockEditorManager.Delete(_stockEditor.StockEditorId);
                _stockEditor = this._stockEditorManager.GetNext(_stockEditor);
                if (_stockEditor == null)
                {
                    _stockEditor = this._stockEditorManager.GetLast();
                }
            }
            catch
            {
                throw;
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumnProductId || e.Column == this.gridColumnProductName)
            {
                Model.StockEditorDetal detail = this.gridView1.GetRow(e.RowHandle) as Model.StockEditorDetal;
                if (detail != null)
                {
                    Model.Product p = this._productManager.Get(detail.ProductId);
                    detail.StockEditorQuantity = null;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.ProductUnitName = p.DepotUnit.CnName;
                    detail.StockEditorId = _stockEditor.StockEditorId;
                    detail.Directions = p.ProductDescription;
                    this.bindingSourceDetail.Position = this.bindingSourceDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumnProductUnitName")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.StockEditorDetal).Product;
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

        //private bool isEnter = false;

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            if (lookUpEditDepotId.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.deptNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                lookUpEditDepotId.Focus();
                return;
            }

            Model.StockEditorDetal stockEditorDetail = this.bindingSourceDetail.Current as Model.StockEditorDetal;

            StockEditorSettingForm depotPN = new StockEditorSettingForm(stockEditorDetail, (lookUpEditDepotId.EditValue as Model.Depot).DepotId);
            if (depotPN.ShowDialog(this) == DialogResult.OK)
            {
                //if (SetNums == 0)
                //    stockEditorDetail.StockEditorQuantity = null;
                //else
                stockEditorDetail.StockEditorQuantity = SetNums;
            }


            //if (this.buttonEditProductCategoryId.EditValue != null)
            //    this.bindingSourceProduct.DataSource = this._productManager.Select(this.buttonEditProductCategoryId.EditValue as Model.ProductCategory);

            this.gridControl1.RefreshDataSource();
        }

        private void buttonEditProductCategoryId_EditValueChanged(object sender, EventArgs e)
        {
            Model.ProductCategory temp = this.buttonEditProductCategoryId.EditValue as Model.ProductCategory;
            //if (_stockEditor.ProductPositionNums.Count != 0)
            //{
            //    _stockEditor.ProductCategoryId = temp.ProductCategoryId;
            //    DialogResult result = MessageBox.Show("是否清理現有詳細", this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //    switch (result)
            //    {
            //        case DialogResult.OK:
            _stockEditor.ProductPositionNums.Clear();
            // break;
            //        default:
            //            break;
            //    }
            //}
            //else
            {
                _stockEditor.ProductCategoryId = temp.ProductCategoryId;
            }
            productlist = this._productManager.DataReaderBind<Model.Product>("select DepotUnitId,Id,ProductId,ProductName,ProductVersion,CustomerProductName,StocksQuantity,ProductDescription from product where ProductCategoryId='" + temp.ProductCategoryId + "'", null, CommandType.Text);
            this.bindingSourceProduct.DataSource = productlist;
            this.Refresh();
        }

        private void buttonEditProductCategoryId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Settings.BasicData.ProductCategories.ChooseForm f = new Settings.BasicData.ProductCategories.ChooseForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.buttonEditProductCategoryId.EditValue = f.SelectedItem as Model.ProductCategory;
            }
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new StockEditorReport(_stockEditor);
        }
    }
}
