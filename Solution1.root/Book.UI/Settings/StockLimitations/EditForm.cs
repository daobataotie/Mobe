using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Book.UI.Invoices;
using Book.UI.Settings.BasicData.Employees;

namespace Book.UI.Settings.StockLimitations
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 
   // 文 件 名：EditForm
   // 编 码 人: 刘永亮                   完成时间:2010-07-30
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        #region 定义变量对象
        public static Model.StockCheck _stockCheck;
        public static double SetNums = 0;
        public static Dictionary<string, Model.StockCheckDetail> dic = new Dictionary<string, Model.StockCheckDetail>();
        private BL.StockCheckManager stockCheckManager = new Book.BL.StockCheckManager();
        private BL.StockCheckDetailManager stockCheckDetailManager = new Book.BL.StockCheckDetailManager();
        private BL.EmployeeManager employeeManager = new Book.BL.EmployeeManager();
        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.ProductManager productManager = new Book.BL.ProductManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.ProductManager _productManager = new Book.BL.ProductManager();
        private BL.ProductCategoryManager _productCategoryManager = new Book.BL.ProductCategoryManager();
        private Model.ProductCategory category = new Book.Model.ProductCategory();
        private BL.StockManager _stockManager = new Book.BL.StockManager();
        #endregion

        #region 构造函数
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.StockCheck.PROPERTY_STOCKCHECKID, new AA(Properties.Resources.NewNumbers, this.textEditStockCheckId));
            this.requireValueExceptions.Add(Model.StockCheck.PROPERTY_EMPLOYEE0ID, new AA(Properties.Resources.SelectEmployee, this.newChooseEmployee));
            this.requireValueExceptions.Add(Model.StockCheck.PROPERTY_DEPOTID, new AA(Properties.Resources.RequiredDataOfDepot, this.lookUpEditDepot));
            this.requireValueExceptions.Add(Model.StockCheckDetail.PROPERTY_DEPOTPOSITIONID, new AA(Properties.Resources.StockCheckQutyIsNull, this.gridControl1));
            this.action = "insert";
            this.newChooseEmployee.Choose = new ChooseEmployee();
            this.newChooseContorlAuditEmp.Choose = new ChooseEmployee();
            this.bindingSourceDepot.DataSource = depotManager.Select();
            this.bindingSourceEmployee.DataSource = employeeManager.Select();
            this.bindingSourceId.DataSource = this.productManager.GetProduct();
            this.bindingSourceProductCategory.DataSource = this._productCategoryManager.Select();

        }
        #endregion

        #region 重载基类方法
        protected override void Save()
        {
            //单据编号
            _stockCheck.StockCheckId = this.textEditStockCheckId.Text;
            //单据日期
            _stockCheck.StockCheckDate = this.dateEditStockCheckDate.DateTime;
            //经手人
            if (newChooseEmployee.EditValue != null)
                _stockCheck.Employee0Id = (this.newChooseEmployee.EditValue as Model.Employee).EmployeeId;
            //备注
            _stockCheck.Directions = this.textEditNote.Text;
            //录单时间
            if (lookUpEditDepot.EditValue != null)
            {
                _stockCheck.DepotId = this.lookUpEditDepot.EditValue.ToString();
                _stockCheck.Depot = this.depotManager.Get(_stockCheck.DepotId);
            }
            _stockCheck.InsertTime = DateTime.Now;

            if (this.lookUpEditProductCategory.EditValue != null)
                _stockCheck.ProductCategoryId = this.lookUpEditProductCategory.EditValue.ToString();


            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.stockCheckManager.Insert(_stockCheck);
                    break;

                case "update":
                    this.stockCheckManager.Update(_stockCheck);
                    break;
            }
        }

        protected override void Delete()
        {
            this.stockCheckManager.Delete(_stockCheck);
        }

        //protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        //{
        //    return new StockCheckReport(_stockCheck);
        //}

        protected override void AddNew()
        {
            _stockCheck = new Model.StockCheck();
            _stockCheck.StockCheckId = this.stockCheckManager.GetNewId();
            _stockCheck.StockCheckDate = DateTime.Now;
            if (BL.V.ActiveOperator.Employee != null)
            {
                _stockCheck.Employee = BL.V.ActiveOperator.Employee;
                _stockCheck.EmployeeId = _stockCheck.Employee.EmployeeId;
            }
            _stockCheck.Details = new List<Model.StockCheckDetail>();
            _stockCheck.ProductPositionNums.Clear();

            foreach (Model.Product product in this._productManager.GetProduct())
            {
                Model.StockCheckDetail detail = new Book.Model.StockCheckDetail();
                detail.StockCheckDetailId = Guid.NewGuid().ToString();
                detail.StockCheckId = _stockCheck.StockCheckId;
                detail.Product = product;
                detail.ProductId = product.ProductId;
                detail.DepotPosition = product.DepotPosition;
                detail.DepotPositionId = product.DepotPositionId;
                detail.Directions = product.ProductDescription;
                detail.StockCheckQuantity = null;
                detail.ProductUnitName = product.DepotUnit.CnName;
                _stockCheck.Details.Add(detail);
                //this.bindingSourceEmployee.Position = this.bindingSourceEmployee.IndexOf(detail);
            }
            dic.Clear();
            _stockCheck.ProductPositionNums = new List<Model.StockCheckDetail>();
        }



        protected override void MoveFirst()
        {
            _stockCheck = this.stockCheckManager.Get(this.stockCheckManager.GetFirst() == null ? "" : this.stockCheckManager.GetFirst().StockCheckId);
        }

        protected override void MoveLast()
        {
            _stockCheck = this.stockCheckManager.Get(this.stockCheckManager.GetLast() == null ? "" : this.stockCheckManager.GetLast().StockCheckId);
        }

        protected override void MoveNext()
        {
            Model.StockCheck stockCheck = this.stockCheckManager.GetNext(_stockCheck);
            if (stockCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            _stockCheck = this.stockCheckManager.Get(stockCheck.StockCheckId);
        }

        protected override void MovePrev()
        {
            Model.StockCheck stockCheck = this.stockCheckManager.GetPrev(_stockCheck);
            if (stockCheck == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            _stockCheck = this.stockCheckManager.Get(stockCheck.StockCheckId);
        }

        public override void Refresh()
        {
            if (_stockCheck == null)
            {
                _stockCheck = new Model.StockCheck();
                _stockCheck.StockCheckId = this.stockCheckManager.GetNewId();
                _stockCheck.StockCheckDate = DateTime.Now;
                _stockCheck.Details = new List<Model.StockCheckDetail>();
                _stockCheck.ProductPositionNums.Clear();

                foreach (Model.Product product in this._productManager.GetProduct())
                {
                    Model.StockCheckDetail detail = new Book.Model.StockCheckDetail();
                    detail.StockCheckDetailId = Guid.NewGuid().ToString();
                    detail.StockCheckId = _stockCheck.StockCheckId;
                    detail.Product = product;
                    detail.ProductId = product.ProductId;
                    detail.DepotPosition = product.DepotPosition;
                    detail.DepotPositionId = product.DepotPositionId;
                    detail.Directions = product.ProductDescription;
                    detail.ProductUnitName = product.DepotUnit.CnName;
                    _stockCheck.Details.Add(detail);
                    //this.bindingSourceEmployee.Position = this.bindingSourceEmployee.IndexOf(detail);
                }
                this.action = "insert";
            }
            else
            {
                if (this.lookUpEditProductCategory.EditValue != null)
                {
                    category = this._productCategoryManager.Get(this.lookUpEditProductCategory.EditValue.ToString());
                    _stockCheck.Details.Clear();
                    foreach (Model.Product product in this._productManager.Select(category))
                    {
                        Model.StockCheckDetail detail = new Book.Model.StockCheckDetail();
                        detail.StockCheckDetailId = Guid.NewGuid().ToString();
                        detail.StockCheckId = _stockCheck.StockCheckId;
                        detail.Product = product;
                        detail.Product.Id = product.Id;
                        detail.ProductId = product.ProductId;
                        detail.DepotPosition = product.DepotPosition;

                        detail.DepotPositionId = product.DepotPositionId;
                        detail.Directions = product.ProductDescription;
                        object nums = this.stockCheckDetailManager.GetNumsByProductIdAndDepositionId(detail.DepotPositionId, product.ProductId);
                        if (nums != null)
                            detail.StockCheckQuantity = Convert.ToDouble(nums);
                        else
                            detail.StockCheckQuantity = null;
                        detail.DepotPositionId = product.DepotPositionId;
                        detail.ProductUnitName = product.DepotUnit.CnName;
                        _stockCheck.Details.Add(detail);
                        this.bindingSourceEmployee.Position = this.bindingSourceEmployee.IndexOf(detail);
                    }
                }
                else
                {
                    _stockCheck.Details.Clear();
                    foreach (Model.Product product in this._productManager.GetProduct())
                    {

                        Model.StockCheckDetail detail = new Book.Model.StockCheckDetail();
                        detail.StockCheckDetailId = Guid.NewGuid().ToString();
                        detail.StockCheckId = _stockCheck.StockCheckId;
                        detail.Product = product;
                        detail.Product.Id = product.Id;
                        detail.ProductId = product.ProductId;
                        detail.DepotPosition = product.DepotPosition;
                        detail.DepotPositionId = product.DepotPositionId;
                        detail.Directions = product.ProductDescription;
                        object nums = this.stockCheckDetailManager.GetNumsByProductIdAndDepositionId(detail.DepotPositionId, product.ProductId);
                        if (nums != null)
                            detail.StockCheckQuantity = Convert.ToDouble(nums);
                        else
                            detail.StockCheckQuantity = null;
                        detail.ProductUnitName = product.DepotUnit.CnName;
                        _stockCheck.Details.Add(detail);
                        this.bindingSourceEmployee.Position = this.bindingSourceEmployee.IndexOf(detail);
                    }
                }
            }
            this.textEditStockCheckId.Text = _stockCheck.StockCheckId;
            this.textEditStockCheckId.EditValue = _stockCheck.StockCheckId;
            this.dateEditStockCheckDate.EditValue = _stockCheck.StockCheckDate;
            this.newChooseEmployee.EditValue = _stockCheck.Employee;
            this.lookUpEditDepot.EditValue = _stockCheck.Depot;
            this.textEditNote.EditValue = _stockCheck.Directions;
            this.bindingSourceStockCheckDetail.DataSource = _stockCheck.Details;

            this.newChooseContorlAuditEmp.EditValue = _stockCheck.AuditEmp;
            this.txt_AuditState.EditValue = this.GetAuditName(_stockCheck.AuditState);

            base.Refresh();

            switch (this.action)
            {
                case "insert":
                case "update":
                    this.gridControl1.Enabled = true;
                    break;
                case "view":
                    this.gridControl1.Enabled = false;
                    break;
            }
        }

        protected override bool HasRows()
        {
            return this.stockCheckManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.stockCheckManager.HasRowsAfter(_stockCheck);
        }

        protected override bool HasRowsPrev()
        {
            return this.stockCheckManager.HasRowsBefore(_stockCheck);
        }


        #endregion

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.StockCheckDetail detail = new Book.Model.StockCheckDetail();

                detail.StockCheckDetailId = Guid.NewGuid().ToString();
                detail.StockCheckId = _stockCheck.StockCheckId;
                detail.StockCheck = _stockCheck;
                detail.Product = f.SelectedItem as Model.Product;
                detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                detail.StockCheckQuantity = 1;
                detail.ProductUnitName = (f.SelectedItem as Model.Product).DepotUnit.CnName;
                _stockCheck.Details.Add(detail);
                this.bindingSourceStockCheckDetail.Position = this.bindingSourceStockCheckDetail.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumn1 || e.Column == this.gridColumn2)
            {
                Model.StockCheckDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.StockCheckDetail;
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.StockCheckDetailId = Guid.NewGuid().ToString();
                    detail.StockCheckId = _stockCheck.StockCheckId;
                    detail.StockCheck = _stockCheck;
                    detail.Directions = "";
                    detail.StockCheckQuantity = null;
                    detail.Product = p;
                    detail.Directions = p.ProductDescription;
                    detail.ProductId = p.ProductId;
                    detail.ProductUnitName = p.DepotUnit.CnName;
                    this.bindingSourceStockCheckDetail.Position = this.bindingSourceStockCheckDetail.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn.Name == "gridColumn3")
            {
                if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.StockCheckDetail).Product;

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

        private void lookUpEditDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (_stockCheck.ProductPositionNums.Count != 0)
            {
                foreach (Model.StockCheckDetail detail in _stockCheck.Details)
                {
                    detail.StockCheckQuantity = 0;
                }

                this.gridControl1.RefreshDataSource();
            }
            if (lookUpEditDepot.EditValue == null) return;

            IList<Model.DepotPosition> list = depotPositionManager.Select(lookUpEditDepot.EditValue.ToString());
            _stockCheck.Depot = this.depotManager.Get(this.lookUpEditDepot.EditValue.ToString());
            //if (list.Count == 0) return;
            //this.repositoryItemComboBox2.Items.Clear();
            //foreach (Model.DepotPosition item in list)
            //{
            //    this.repositoryItemComboBox2.Items.Add(item);
            //}

        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceStockCheckDetail.Current != null)
            {
                _stockCheck.Details.Remove(this.bindingSourceStockCheckDetail.Current as Book.Model.StockCheckDetail);

                if (_stockCheck.Details.Count == 0)
                {
                    Model.StockCheckDetail detail = new Model.StockCheckDetail();
                    detail.StockCheckDetailId = Guid.NewGuid().ToString();
                    detail.Directions = "";
                    detail.StockCheckQuantity = null;
                    detail.ProductUnitName = "";
                    detail.Product = new Book.Model.Product();
                    _stockCheck.Details.Add(detail);
                    this.bindingSourceStockCheckDetail.Position = this.bindingSourceStockCheckDetail.IndexOf(detail);
                }


                this.gridControl1.RefreshDataSource();
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.StockCheckDetail> stockDetail = this.bindingSourceStockCheckDetail.DataSource as IList<Model.StockCheckDetail>;
            if (stockDetail == null || stockDetail.Count < 1) return;
            Model.Product product = stockDetail[e.ListSourceRowIndex].Product;
            if (product == null) return;
            switch (e.Column.Name)
            {
                case "CustomerProductName":
                    e.DisplayText = product.CustomerProductName == null ? string.Empty : product.CustomerProductName;
                    break;
                case "gridColumnProductDescription":
                    e.DisplayText = product.ProductDescription;
                    break;
            }
        }

        private void lookUpEditProductCategory_EditValueChanged(object sender, EventArgs e)
        {
            _stockCheck.Details.Clear();
            if (this.lookUpEditProductCategory.EditValue != null)
                category = this._productCategoryManager.Get(this.lookUpEditProductCategory.EditValue.ToString());
            if (category != null)
            {
                foreach (Model.Product product in this._productManager.Select(category))
                {
                    Model.StockCheckDetail detail = new Book.Model.StockCheckDetail();
                    detail.StockCheckDetailId = Guid.NewGuid().ToString();
                    detail.StockCheckId = _stockCheck.StockCheckId;

                    detail.Product = product;
                    detail.ProductId = product.ProductId;
                    detail.DepotPosition = product.DepotPosition;
                    detail.DepotPositionId = product.DepotPositionId;
                    detail.StockCheckQuantity = null;
                    detail.ProductUnitName = product.DepotUnit.ToString();
                    _stockCheck.Details.Add(detail);
                    //this.bindingSourceEmployee.Position = this.bindingSourceEmployee.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void lookUpEditDepot_Click(object sender, EventArgs e)
        {
            this.bindingSourceDepot.DataSource = depotManager.Select();
        }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {

            if (lookUpEditDepot.EditValue == null)
            {
                MessageBox.Show(Properties.Resources.deptNotNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                lookUpEditDepot.Focus();
                return;
            }

            Model.StockCheckDetail stockCheckDetail = this.bindingSourceStockCheckDetail.Current as Model.StockCheckDetail;
            stockCheckDetail.DepotId = this.lookUpEditDepot.EditValue.ToString();
            stockCheckDetail.Depot = this.depotManager.Get(stockCheckDetail.DepotId);

            DepotPositionAndNumsForm depotPN = new DepotPositionAndNumsForm(stockCheckDetail);
            if (depotPN.ShowDialog(this) == DialogResult.OK)
            {
                if (SetNums == 0)
                    stockCheckDetail.StockCheckQuantity = null;
                else
                    stockCheckDetail.StockCheckQuantity = SetNums;
            }

            this.gridControl1.RefreshDataSource();
        }

        private void lookUpEditProductCategory_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (_stockCheck.ProductPositionNums.Count != 0)
            {
                DialogResult result = MessageBox.Show(Properties.Resources.DataChangedDoYouSave, this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                switch (result)
                {
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.No:
                        _stockCheck.ProductPositionNums.Clear();
                        break;
                    case DialogResult.Yes:
                        this.Save();
                        _stockCheck.ProductPositionNums.Clear();
                        MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        this.action = "insert";
                        this.AddNew();
                        _stockCheck.Depot = this.depotManager.Get(this.lookUpEditDepot.EditValue.ToString());
                        _stockCheck.DepotId = _stockCheck.Depot.DepotId;
                        this.Refresh();
                        break;
                    default:
                        break;
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StockEditorChooseForm f = new StockEditorChooseForm();
            f.Show();
        }

        #region 审核

        protected override string AuditKeyId()
        {
            return Model.StockCheck.PROPERTY_STOCKCHECKID;
        }

        protected override int AuditState()
        {
            return _stockCheck.AuditState.HasValue ? _stockCheck.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "StockCheck" + "," + _stockCheck.StockCheckId;
        }

        #endregion
    }
}
