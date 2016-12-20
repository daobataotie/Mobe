using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Book.UI.Settings.BasicData.Employees;
using Book.UI.Invoices;
using System.Linq;
using System.Collections;

namespace Book.UI.produceManager.ProduceOtherMaterial
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人:  裴盾              完成时间:2010-03-2
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        public static IList<Model.ProduceOtherCompactDetail> _produceOtherCompactDetail = new List<Model.ProduceOtherCompactDetail>();
        Model.ProduceOtherMaterial _produceOtherMaterial = new Book.Model.ProduceOtherMaterial();
        BL.ProduceOtherMaterialManager produceOtherMaterialManager = new Book.BL.ProduceOtherMaterialManager();
        BL.ProduceOtherCompactMaterialManager produceOhterCompactMaterialManager = new Book.BL.ProduceOtherCompactMaterialManager();
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();

        BL.ProduceOtherMaterialDetailManager produceOtherMaterialDetailManager = new Book.BL.ProduceOtherMaterialDetailManager();

        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();
        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.ProduceOtherCompactMaterialManager OtherCompactMaterialManager = new Book.BL.ProduceOtherCompactMaterialManager();
        private BL.MRSHeaderManager mRSHeaderManager = new BL.MRSHeaderManager();
        private BL.InvoiceXOManager invoiceXOManager = new BL.InvoiceXOManager();
        private BL.MPSheaderManager mPSheaderManager = new BL.MPSheaderManager();
        private BL.ProduceOtherCompactManager produceOtherCompactManager = new BL.ProduceOtherCompactManager();

        int flag = 0;
        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialId, new AA(Properties.Resources.RequireDataForId, this.textEditProduceOtherMaterialId));

            this.invalidValueExceptions.Add(Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialId, new AA(Properties.Resources.EntityExists, this.textEditProduceOtherMaterialId));
            this.action = "view";
            this.newChooseEmployee0.Choose = new ChooseEmployee();
            this.newChooseEmployee1.Choose = new ChooseEmployee();
            this.newChooseContorlSipu.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.EmpAudit.Choose = new ChooseEmployee();
        }

        public EditForm(Model.ProduceOtherMaterial produceOtherMaterial)
            : this()
        {
            this._produceOtherMaterial = produceOtherMaterial;
            this._produceOtherMaterial.Details = this.produceOtherMaterialDetailManager.Select(produceOtherMaterial);
            this.action = "view";
            this.flag = 1;
        }

        public EditForm(Model.ProduceOtherMaterial produceOtherMaterial, string action)
            : this()
        {
            this._produceOtherMaterial = produceOtherMaterial;
            this._produceOtherMaterial.Details = this.produceOtherMaterialDetailManager.Select(produceOtherMaterial);
            this.action = action;
            this.flag = 1;
        }

        protected override void Save()
        {
            this._produceOtherMaterial.ProduceOtherMaterialId = this.textEditProduceOtherMaterialId.Text;
            this._produceOtherMaterial.ProduceOtherMaterialDesc = this.textEditProduceOtherMaterialDesc.Text;
            this._produceOtherMaterial.Supplier = this.newChooseContorlSipu.EditValue as Model.Supplier;
            if (this._produceOtherMaterial.Supplier != null)
                this._produceOtherMaterial.SupplierId = this._produceOtherMaterial.Supplier.SupplierId;

            if (this._produceOtherMaterial.WorkHouse != null)
            {
                this._produceOtherMaterial.WorkHouseId = this._produceOtherMaterial.WorkHouse.WorkHouseId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceOtherMaterialDate.DateTime, new DateTime()))
            {
                this._produceOtherMaterial.ProduceOtherMaterialDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._produceOtherMaterial.ProduceOtherMaterialDate = this.dateEditProduceOtherMaterialDate.DateTime;
            }
            this._produceOtherMaterial.Employee0 = (this.newChooseEmployee0.EditValue as Model.Employee);
            if (this._produceOtherMaterial.Employee0 != null)
            {
                this._produceOtherMaterial.Employee0Id = this._produceOtherMaterial.Employee0.EmployeeId;
            }
            this._produceOtherMaterial.Employee1 = (this.newChooseEmployee1.EditValue as Model.Employee);
            if (this._produceOtherMaterial.Employee1 != null)
            {
                this._produceOtherMaterial.Employee1Id = this._produceOtherMaterial.Employee1.EmployeeId;
            }
            if (this.lookUpEditDepot.EditValue != null)
                this._produceOtherMaterial.DepotId = this.lookUpEditDepot.EditValue.ToString();
            this._produceOtherMaterial.ProduceOtherCompactId = this.textEditOtherCompact.Text;
            this._produceOtherMaterial.InvoiceCusId = this.textEditCusXOId.Text;

            this._produceOtherMaterial.AuditState = this.saveAuditState;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.produceOtherMaterialManager.Insert(this._produceOtherMaterial);
                    break;

                case "update":
                    this.produceOtherMaterialManager.Update(this._produceOtherMaterial);
                    break;
            }

        }

        protected override void Delete()
        {

            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.produceOtherMaterialManager.Delete(this._produceOtherMaterial);
                this._produceOtherMaterial = this.produceOtherMaterialManager.GetNext(this._produceOtherMaterial);
                if (this._produceOtherMaterial == null)
                {
                    this._produceOtherMaterial = this.produceOtherMaterialManager.GetLast();
                }
            }
            catch
            {
                throw;
            }

        }

        public override void Refresh()
        {

            if (this._produceOtherMaterial == null)
            {
                //this._produceOtherMaterial = new Book.Model.ProduceOtherMaterial();
                //this.action = "insert";
                this.AddNew();
            }
            else
            {
                if (this.action == "view")
                {

                    this._produceOtherMaterial = this.produceOtherMaterialManager.GetDetails(_produceOtherMaterial.ProduceOtherMaterialId);
                    if (this._produceOtherMaterial == null)
                    {
                        this.AddNew();
                        this.action = "insert";
                    }
                }
            }

            if (this.produceOtherMaterialManager.IsDepotOut(this._produceOtherMaterial.ProduceOtherMaterialId) && this.action == "update")
            {
                MessageBox.Show("已出仓，请勿修改！", this.Text, MessageBoxButtons.OK);
                this.action = "view";
            }

            this.newChooseContorlSipu.EditValue = this._produceOtherMaterial.Supplier;
            this.textEditOtherCompact.Text = this._produceOtherMaterial.ProduceOtherCompactId;

            this.textEditProduceOtherMaterialId.Text = this._produceOtherMaterial.ProduceOtherMaterialId;
            this.textEditProduceOtherMaterialDesc.Text = this._produceOtherMaterial.ProduceOtherMaterialDesc;
            this.lookUpEditDepot.EditValue = this._produceOtherMaterial.DepotId;
            //this.newChooseWorkHorseId.EditValue = this.produceOtherMaterial.WorkHouse;
            if (global::Helper.DateTimeParse.DateTimeEquls(this._produceOtherMaterial.ProduceOtherMaterialDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceOtherMaterialDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceOtherMaterialDate.EditValue = this._produceOtherMaterial.ProduceOtherMaterialDate;
            }
            this.textEditCusXOId.Text = this._produceOtherMaterial.InvoiceCusId;
            this.newChooseEmployee0.EditValue = this._produceOtherMaterial.Employee0;
            this.newChooseEmployee1.EditValue = this._produceOtherMaterial.Employee1;
            this.bindingSourceDetails.DataSource = this._produceOtherMaterial.Details;
            this.EmpAudit.EditValue = this._produceOtherMaterial.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this._produceOtherMaterial.AuditState);

            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.barButtonItem1.Enabled = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.barButtonItem1.Enabled = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.barButtonItem1.Enabled = false;
                    break;
            }
            this.newChooseEmployee0.Enabled = false;
            this.textEditProduceOtherMaterialId.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(_produceOtherMaterial.ProduceOtherMaterialId);
        }

        protected override void MoveNext()
        {
            Model.ProduceOtherMaterial produceOtherMaterial = this.produceOtherMaterialManager.GetNext(this._produceOtherMaterial);
            if (produceOtherMaterial == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceOtherMaterial = this.produceOtherMaterialManager.Get(produceOtherMaterial.ProduceOtherMaterialId);
        }

        protected override void MovePrev()
        {
            Model.ProduceOtherMaterial produceOtherMaterial = this.produceOtherMaterialManager.GetPrev(this._produceOtherMaterial);
            if (produceOtherMaterial == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceOtherMaterial = this.produceOtherMaterialManager.Get(produceOtherMaterial.ProduceOtherMaterialId);
        }

        protected override void MoveFirst()
        {
            this._produceOtherMaterial = this.produceOtherMaterialManager.Get(this.produceOtherMaterialManager.GetFirst() == null ? "" : this.produceOtherMaterialManager.GetFirst().ProduceOtherMaterialId);
        }

        protected override void MoveLast()
        {
            if (this.flag == 1)
            {
                this.flag = 0;
                return;
            }
            this._produceOtherMaterial = this.produceOtherMaterialManager.Get(this.produceOtherMaterialManager.GetLast() == null ? "" : this.produceOtherMaterialManager.GetLast().ProduceOtherMaterialId);
        }

        protected override bool HasRows()
        {
            return this.produceOtherMaterialManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceOtherMaterialManager.HasRowsAfter(this._produceOtherMaterial);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceOtherMaterialManager.HasRowsBefore(this._produceOtherMaterial);
        }

        protected override void AddNew()
        {
            this.lookUpEditDepot.EditValue = null;
            this.bindingSourceDepotPositionId.DataSource = null;
            this._produceOtherMaterial = new Model.ProduceOtherMaterial();
            this._produceOtherMaterial.ProduceOtherMaterialDate = DateTime.Now;
            this._produceOtherMaterial.ProduceOtherMaterialId = this.produceOtherMaterialManager.GetId();// Guid.NewGuid().ToString();
            this._produceOtherMaterial.Employee0 = BL.V.ActiveOperator.Employee;
            if (this._produceOtherMaterial.Employee0 != null)
                this._produceOtherMaterial.Employee0Id = this._produceOtherMaterial.Employee0.EmployeeId;
            this._produceOtherMaterial.Details = new List<Model.ProduceOtherMaterialDetail>();
            if (this.action == "insert")
            {
                Model.ProduceOtherMaterialDetail detail = new Model.ProduceOtherMaterialDetail();
                detail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                detail.OtherMaterialQuantity = 0;
                detail.OtherMaterialALLUserQuantity = 0;
                detail.ProductStock = 0;
                detail.ProductSpecification = "";
                detail.Product = new Book.Model.Product();
                this._produceOtherMaterial.Details.Add(detail);
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            }
            this.action = "insert";
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this._produceOtherMaterial.Details.Count > 0 && this._produceOtherMaterial.Details[0] != null && string.IsNullOrEmpty(this._produceOtherMaterial.Details[0].ProductId))
                    this._produceOtherMaterial.Details.RemoveAt(0);
                Model.ProduceOtherMaterialDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {

                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.ProduceOtherMaterialDetail();
                        detail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                        detail.Product = this.productManager.Get(product.ProductId);
                        detail.ProductId = product.ProductId;
                        detail.ProductSpecification = detail.Product.ProductSpecification;
                        detail.OtherMaterialQuantity = 0;
                        detail.OtherMaterialALLUserQuantity = 0;
                        detail.ProductStock = detail.Product.StocksQuantity;
                        if (!detail.Product.ProduceMaterialDistributioned.HasValue)
                            detail.Product.ProduceMaterialDistributioned = 0;
                        if (!detail.Product.OtherMaterialDistributioned.HasValue)
                            detail.Product.OtherMaterialDistributioned = 0;
                        detail.Distributioned = detail.Product.ProduceMaterialDistributioned + detail.Product.OtherMaterialDistributioned;
                        detail.Inumber = this._produceOtherMaterial.Details.Count + 1;
                        this._produceOtherMaterial.Details.Add(detail);
                    }
                }

                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.ProduceOtherMaterialDetail();
                    detail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                    detail.Product = this.productManager.Get((f.SelectedItem as Model.Product).ProductId);
                    detail.ProductId = detail.Product.ProductId;
                    detail.ProductSpecification = detail.Product.ProductSpecification;
                    detail.OtherMaterialQuantity = 0;
                    detail.OtherMaterialALLUserQuantity = 0;
                    detail.ProductStock = detail.Product.StocksQuantity;
                    if (!detail.Product.ProduceMaterialDistributioned.HasValue)
                        detail.Product.ProduceMaterialDistributioned = 0;
                    if (!detail.Product.OtherMaterialDistributioned.HasValue)
                        detail.Product.OtherMaterialDistributioned = 0;
                    detail.Distributioned = detail.Product.ProduceMaterialDistributioned + detail.Product.OtherMaterialDistributioned;
                    detail.Inumber = this._produceOtherMaterial.Details.Count + 1;
                    this._produceOtherMaterial.Details.Add(detail);
                }
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                this.gridControl1.RefreshDataSource();

            }
            f.Dispose();
            System.GC.Collect();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this._produceOtherMaterial.Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceOtherMaterialDetail);

                if (this._produceOtherMaterial.Details.Count == 0)
                {
                    Model.ProduceOtherMaterialDetail detail = new Model.ProduceOtherMaterialDetail();
                    detail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                    detail.OtherMaterialQuantity = 0;
                    detail.OtherMaterialALLUserQuantity = 0;
                    detail.ProductStock = 0;
                    detail.Distributioned = 0;
                    detail.ProductSpecification = "";
                    detail.Product = new Book.Model.Product();
                    this._produceOtherMaterial.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButtonXO_Click(object sender, EventArgs e)
        {
            _produceOtherCompactDetail.Clear();
            ProduceOtherCompact.ChooseOutContract f = new Book.UI.produceManager.ProduceOtherCompact.ChooseOutContract();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (_produceOtherCompactDetail.Count != 0)
            {
                this._produceOtherMaterial.Details.Clear();

                if (_produceOtherCompactDetail != null)
                {
                    foreach (Model.ProduceOtherCompactDetail ProduceOtherCompactDetail in _produceOtherCompactDetail)
                    {
                        Model.ProduceOtherMaterialDetail produceOtherMaterialDetail = new Book.Model.ProduceOtherMaterialDetail();
                        produceOtherMaterialDetail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                        if (ProduceOtherCompactDetail.Product != null)
                        {
                            produceOtherMaterialDetail.Product = ProduceOtherCompactDetail.Product;
                            produceOtherMaterialDetail.ProductId = ProduceOtherCompactDetail.Product.ProductId;
                            produceOtherMaterialDetail.ProductStock = ProduceOtherCompactDetail.Product.StocksQuantity;
                            if (ProduceOtherCompactDetail.Product.MainUnit != null)
                            {
                                produceOtherMaterialDetail.ProductUnit = ProduceOtherCompactDetail.Product.MainUnit.CnName;
                            }
                            produceOtherMaterialDetail.ProductSpecification = ProduceOtherCompactDetail.Product.ProductSpecification;
                        }
                        produceOtherMaterialDetail.MPSheaderId = ProduceOtherCompactDetail.MPSheaderId;
                        produceOtherMaterialDetail.OtherMaterialQuantity = ProduceOtherCompactDetail.OtherCompactCount;
                        produceOtherMaterialDetail.OtherMaterialALLUserQuantity = ProduceOtherCompactDetail.OtherCompactCount;
                        produceOtherMaterialDetail.ProduceOtherMaterial = this._produceOtherMaterial;
                        produceOtherMaterialDetail.ProduceOtherMaterialId = this._produceOtherMaterial.ProduceOtherMaterialId;

                        produceOtherMaterialDetail.InvoiceXOId = ProduceOtherCompactDetail.InvoiceXOId;
                        produceOtherMaterialDetail.InvoiceXODetailId = ProduceOtherCompactDetail.InvoiceXODetailId;
                        this._produceOtherMaterial.Details.Add(produceOtherMaterialDetail);

                    }
                }
                this.gridControl1.RefreshDataSource();

            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProduceOtherMaterialDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceOtherMaterialDetail;

            if (e.Column == this.ColProductId)
            {
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                    detail.OtherMaterialQuantity = 0;
                    detail.OtherMaterialALLUserQuantity = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.ProductStock = p.StocksQuantity;
                    detail.ProductSpecification = p.ProductSpecification;

                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            //if (e.Column == this.gridColumn8)
            //{
            //    detail.DepotPosition = null;
            //}
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherMaterialDetail> details = this.bindingSourceDetails.DataSource as IList<Model.ProduceOtherMaterialDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "ColProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.gridView1.FocusedColumn.Name == "gridColumn9")
                {
                    Model.ProduceOtherMaterialDetail detail = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceOtherMaterialDetail;
                    this.repositoryItemComboBox1.Items.Clear();
                    if (detail != null)
                    {
                        //if (detail.DepotId != null)
                        //{
                        //    IList<Model.DepotPosition> unitList = depotPositionManager.Select(detail.DepotId);
                        //    foreach (Model.DepotPosition item in unitList)
                        //    {
                        //        this.repositoryItemComboBox1.Items.Add(item.Id);
                        //    }
                        //}
                        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                    }
                    this.gridControl1.RefreshDataSource();
                }
                if (this.gridView1.FocusedColumn.Name == "gridColumnUnit")
                {

                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceOtherMaterialDetail).Product;

                        this.repositoryItemComboBox2.Items.Clear();
                        if (p != null)
                        {
                            if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                            {
                                BL.ProductUnitManager manager = new Book.BL.ProductUnitManager();
                                IList<Model.ProductUnit> unitList = manager.Select(p.BasedUnitGroupId);
                                foreach (Model.ProductUnit item in unitList)
                                {
                                    this.repositoryItemComboBox2.Items.Add(item.CnName);
                                }
                            }
                        }
                    }
                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            this.bindingSourcDepot.DataSource = depotManager.Select();
            //this.bindingSourceDepotPositionId.DataSource = depotPositionManager.Select();
            //string sql = "SELECT productid,id,productname,CustomerProductName  FROM product";
            //this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
        }

        public static Model.ProduceOtherCompact _compact;

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Model.ProduceOtherMaterialDetail detail;
            ProduceOtherInDepot.ChooseProduceOtherCompactForm form = new Book.UI.produceManager.ProduceOtherInDepot.ChooseProduceOtherCompactForm("material");
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                this._produceOtherMaterial.Details.Clear();
                this.textEditOtherCompact.Text = _compact.ProduceOtherCompactId;
                foreach (var item in produceOhterCompactMaterialManager.SelectIsInDepotMaterialDetail(_compact))
                {
                    detail = new Book.Model.ProduceOtherMaterialDetail();
                    detail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                    detail.OtherMaterialQuantity = item.ProduceQuantity;
                    detail.ProduceOtherMaterialId = this._produceOtherMaterial.ProduceOtherMaterialId;
                    detail.ProduceOtherCompactMaterialId = item.ProduceOtherCompactMaterialId;
                    detail.ProductUnit = item.ProductUnit;
                    detail.ProductId = item.ProductId;
                    //detail.ProductStock = item.ProductStock;
                    detail.Product = item.Product;
                    this._produceOtherMaterial.Details.Add(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void lookUpEditDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpEditDepot.EditValue != null)
            {
                this.bindingSourceDepotPositionId.DataSource = depotPositionManager.Select(depotManager.Get(this.lookUpEditDepot.EditValue.ToString()));
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButtonOther_Click(object sender, EventArgs e)
        {
            ProduceOtherCompact.ChooseOutContract f = new ProduceOtherCompact.ChooseOutContract();
            if (f.ShowDialog(this) != DialogResult.OK) return;

            Model.ProduceOtherCompact OtherCompact = f.SelectItem as Model.ProduceOtherCompact;
            if (OtherCompact == null) return;

            if (this._produceOtherMaterial.Details.Count > 0 && string.IsNullOrEmpty(this._produceOtherMaterial.Details[0].ProductId))
                this._produceOtherMaterial.Details.RemoveAt(0);
            this._produceOtherMaterial.Details.Clear();
            this.textEditOtherCompact.Text = OtherCompact.ProduceOtherCompactId;
            this.newChooseContorlSipu.EditValue = OtherCompact.Supplier;
            this.textEditCusXOId.Text = string.Empty;



            if (!string.IsNullOrEmpty(OtherCompact.MRSHeaderId))
            {
                Model.MRSHeader mrsHeader = this.mRSHeaderManager.Get(OtherCompact.MRSHeaderId);
                if (mrsHeader != null)
                {
                    Model.MPSheader mPSheader = this.mPSheaderManager.Get(mrsHeader.MPSheaderId);
                    if (mPSheader != null)
                    {
                        Model.InvoiceXO invoiceXO = this.invoiceXOManager.Get(mPSheader.InvoiceXOId);
                        this.textEditCusXOId.Text = invoiceXO == null ? string.Empty : invoiceXO.CustomerInvoiceXOId;
                    }
                }
            }
            //if (ProduceOtherCompact.ChooseOutContract._OtherCompactDetailList.Count == 0) return;
            //if (this._produceOtherInDepot.Details.Count > 0 && string.IsNullOrEmpty(this._produceOtherInDepot.Details[0].ProductId))
            //    this._produceOtherInDepot.Details.RemoveAt(0);
            //this._produceOtherInDepot.Details.Clear();
            //this.textEditOtherCompact.Text = ProduceOtherCompact.ChooseOutContract._OtherCompactDetailList[0].ProduceOtherCompactId;

            //this._produceOtherMaterial.Details = (from p in this.OtherCompactMaterialManager.Select(OtherCompact)
            //           group p by new { p.ProductId, p.ProductUnit } into b
            //           select new Model.ProduceOtherMaterialDetail()
            //           {
            //               ProduceOtherMaterialDetailId = Guid.NewGuid().ToString(),
            //               ProduceOtherCompactMaterialId = (from p in b select p.ProduceOtherCompactMaterialId).ToString(),
            //               ProductId = b.Key.ProductId,
            //               Product = new BL.ProductManager().Get(b.Key.ProductId),
            //               ProductUnit =b.Key.ProductUnit,
            //               OtherMaterialQuantity = (from p in b select p.ProduceQuantity).Sum(),
            //               Description = (from p in b select p.Description).ToString(),
            //           }).ToList();

            foreach (Model.ProduceOtherCompactMaterial item in this.OtherCompactMaterialManager.Select(OtherCompact))
            {
                Model.ProduceOtherMaterialDetail detail = new Model.ProduceOtherMaterialDetail();
                detail.ProduceOtherMaterialDetailId = Guid.NewGuid().ToString();
                detail.ProduceOtherCompactMaterialId = item.ProduceOtherCompactMaterialId;
                detail.Product = this.productManager.Get(item.ProductId);
                detail.ProductId = item.ProductId;
                detail.ProductUnit = item.ProductUnit;
                detail.OtherMaterialQuantity = item.ProduceQuantity;// - (item.AlreadyOutQuantity == null ? 0 : item.AlreadyOutQuantity);
                if (detail.OtherMaterialQuantity < 0)
                {
                    detail.OtherMaterialQuantity = 0;
                }
                // detail.ProcessPrice = 0;
                detail.Description = item.Description;
                detail.ParentProductId = item.ParentProductId;
                detail.ParentProduct = item.ParentProduct;
                detail.ProductStock = detail.Product.StocksQuantity;

                if (!detail.Product.ProduceMaterialDistributioned.HasValue)
                    detail.Product.ProduceMaterialDistributioned = 0;
                if (!detail.Product.OtherMaterialDistributioned.HasValue)
                    detail.Product.OtherMaterialDistributioned = 0;
                detail.Distributioned = detail.Product.OtherMaterialDistributioned + detail.Product.ProduceMaterialDistributioned;
                detail.Inumber = this._produceOtherMaterial.Details.Count + 1;
                detail.HandbookId = item.HandbookId;
                detail.HandbookProductId = item.HandbookProductId;

                this._produceOtherMaterial.Details.Add(detail);
            }
            this.gridControl1.RefreshDataSource();

        }

        private void barButtonItemSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm f = new ListForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._produceOtherMaterial = f.SelectItem as Model.ProduceOtherMaterial;
                this.action = "view";
                this.Refresh();
            }
        }

        private void textEditProduceOtherMaterialDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditProduceOtherMaterialDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核
        protected override string AuditKeyId()
        {
            return Model.ProduceOtherMaterial.PRO_ProduceOtherMaterialId;
        }

        protected override int AuditState()
        {
            return this._produceOtherMaterial.AuditState.HasValue ? this._produceOtherMaterial.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProduceOtherMaterial" + "," + this._produceOtherMaterial.ProduceOtherMaterialId;
        }
        #endregion

        private void dateEditProduceOtherMaterialDate_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
