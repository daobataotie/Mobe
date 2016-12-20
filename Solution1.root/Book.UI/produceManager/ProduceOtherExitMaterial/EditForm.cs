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

namespace Book.UI.produceManager.ProduceOtherExitMaterial
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2010-4-1
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/

    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        public static IList<Model.ProduceOtherCompactDetail> _produceOtherCompactDetail = new List<Model.ProduceOtherCompactDetail>();
        Model.ProduceOtherExitMaterial produceOtherExitMaterial = new Book.Model.ProduceOtherExitMaterial();
        BL.ProduceOtherExitMaterialManager produceOtherExitMaterialManager = new Book.BL.ProduceOtherExitMaterialManager();
        //部门管理
        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();

        BL.ProduceOtherExitDetailManager produceOtherExitDetailManager = new Book.BL.ProduceOtherExitDetailManager();
        BL.InvoiceXOManager invoiceXOManager = new Book.BL.InvoiceXOManager();
        //商品
        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();

        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceOtherExitMaterial.PRO_ProduceOtherExitMaterialId, new AA(Properties.Resources.RequireDataForId, this.textEditProduceOtherExitMaterialId));
            //this.requireValueExceptions.Add(Model.ProduceOtherExitMaterial.PROPERTY_WORKHOUSEID, new AA(Properties.Resources.WorkHouse, this.newChooseWorkHorseId));
            this.invalidValueExceptions.Add(Model.ProduceOtherExitMaterial.PRO_ProduceOtherExitMaterialId, new AA(Properties.Resources.EntityExists, this.textEditProduceOtherExitMaterialId));
            this.requireValueExceptions.Add(Model.ProduceOtherExitMaterial.PRO_DepotId, new AA(Properties.Resources.deptNotNull, this.newChooseContorlDepot));
            this.requireValueExceptions.Add(Model.ProduceOtherExitDetail.PRO_DepotPositionId, new AA(Properties.Resources.RequireChoosePosition, this.gridControl1));

            this.action = "view";

            this.newChooseEmployee0.Choose = new ChooseEmployee();
            this.newChooseEmployee1.Choose = new ChooseEmployee();
            //this.newChooseWorkHorseId.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.newChooseContorlSipu.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlDepot.Choose = new ChooseDepot();
            this.EmpAudit.Choose = new ChooseEmployee();
            //this.bindingSourcDepot.DataSource = depotManager.Select();
        }

        public EditForm(Model.ProduceOtherExitMaterial produceOtherExitMaterial)
            : this()
        {
            this.produceOtherExitMaterial = produceOtherExitMaterial;
            this.produceOtherExitMaterial.Details = this.produceOtherExitDetailManager.Select(produceOtherExitMaterial);
            this.action = "update";
        }

        public EditForm(Model.ProduceOtherExitMaterial produceOtherExitMaterial, string action)
            : this()
        {
            this.produceOtherExitMaterial = produceOtherExitMaterial;
            this.produceOtherExitMaterial.Details = this.produceOtherExitDetailManager.Select(produceOtherExitMaterial);
            this.action = action;
        }

        protected override void Save()
        {
            this.produceOtherExitMaterial.ProduceOtherExitMaterialId = this.textEditProduceOtherExitMaterialId.Text;
            this.produceOtherExitMaterial.ProduceOtherExitMaterialDesc = this.textEditProduceOtherExitMaterialDesc.Text;
            //this.produceOtherExitMaterial.WorkHouse = this.newChooseWorkHorseId.EditValue as Model.WorkHouse;
            this.produceOtherExitMaterial.Supplier = this.newChooseContorlSipu.EditValue as Model.Supplier;
            if (this.produceOtherExitMaterial.Supplier != null)
                this.produceOtherExitMaterial.SupplierId = this.produceOtherExitMaterial.Supplier.SupplierId;


            if (this.produceOtherExitMaterial.WorkHouse != null)
            {
                this.produceOtherExitMaterial.WorkHouseId = this.produceOtherExitMaterial.WorkHouse.WorkHouseId;
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProduceOtherExitMaterialDate.DateTime, new DateTime()))
            {
                this.produceOtherExitMaterial.ProduceOtherExitMaterialDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.produceOtherExitMaterial.ProduceOtherExitMaterialDate = this.dateEditProduceOtherExitMaterialDate.DateTime;
            }
            this.produceOtherExitMaterial.Employee0 = (this.newChooseEmployee0.EditValue as Model.Employee);
            if (this.produceOtherExitMaterial.Employee0 != null)
            {
                this.produceOtherExitMaterial.Employee0Id = this.produceOtherExitMaterial.Employee0.EmployeeId;
            }
            this.produceOtherExitMaterial.Employee1 = (this.newChooseEmployee1.EditValue as Model.Employee);
            if (this.produceOtherExitMaterial.Employee1 != null)
            {
                this.produceOtherExitMaterial.Employee1Id = this.produceOtherExitMaterial.Employee1.EmployeeId;
            }
            if (this.newChooseContorlDepot.EditValue != null)
            {
                this.produceOtherExitMaterial.Depot = this.newChooseContorlDepot.EditValue as Model.Depot;
                this.produceOtherExitMaterial.DepotId = this.produceOtherExitMaterial.Depot.DepotId;
            }
            this.produceOtherExitMaterial.ProduceOtherCompactId = this.textEditCompact.Text;
            this.produceOtherExitMaterial.AuditState = this.saveAuditState;
            this.produceOtherExitMaterial.InvoiceCusId = this.textEditCusXOId.Text;

            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.produceOtherExitMaterialManager.Insert(this.produceOtherExitMaterial);
                    break;

                case "update":
                    this.produceOtherExitMaterialManager.Update(this.produceOtherExitMaterial);
                    break;
            }

        }

        protected override void Delete()
        {
            this.produceOtherExitMaterialManager.Delete(this.produceOtherExitMaterial.ProduceOtherExitMaterialId);
        }

        public override void Refresh()
        {

            if (this.produceOtherExitMaterial == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this.produceOtherExitMaterial = this.produceOtherExitMaterialManager.GetDetails(produceOtherExitMaterial.ProduceOtherExitMaterialId);

                }
            }

            this.newChooseContorlSipu.EditValue = this.produceOtherExitMaterial.Supplier;
            this.textEditProduceOtherExitMaterialId.Text = this.produceOtherExitMaterial.ProduceOtherExitMaterialId;
            this.textEditProduceOtherExitMaterialDesc.Text = this.produceOtherExitMaterial.ProduceOtherExitMaterialDesc;
            //this.newChooseWorkHorseId.EditValue = this.produceOtherExitMaterial.WorkHouse;
            if (global::Helper.DateTimeParse.DateTimeEquls(this.produceOtherExitMaterial.ProduceOtherExitMaterialDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProduceOtherExitMaterialDate.EditValue = null;
            }
            else
            {
                this.dateEditProduceOtherExitMaterialDate.EditValue = this.produceOtherExitMaterial.ProduceOtherExitMaterialDate;
            }
            this.newChooseEmployee0.EditValue = this.produceOtherExitMaterial.Employee0;
            this.newChooseEmployee1.EditValue = this.produceOtherExitMaterial.Employee1;
            this.newChooseContorlDepot.EditValue = this.produceOtherExitMaterial.Depot;
            this.textEditCompact.EditValue = this.produceOtherExitMaterial.ProduceOtherCompactId;
            this.textEditCusXOId.EditValue = this.produceOtherExitMaterial.InvoiceCusId;
            this.bindingSourceDetails.DataSource = this.produceOtherExitMaterial.Details;
            this.EmpAudit.EditValue = this.produceOtherExitMaterial.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this.produceOtherExitMaterial.AuditState);

            base.Refresh();

            this.newChooseEmployee0.Enabled = false;

            switch (this.action)
            {
                case "insert":
                case "update":
                    this.barButtonItem1.Enabled = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    break;
                case "view":
                    this.barButtonItem1.Enabled = false;
                    this.gridView1.OptionsBehavior.Editable = false;
                    break;
            }

            this.textEditProduceOtherExitMaterialId.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(produceOtherExitMaterial.ProduceOtherExitMaterialId);
        }

        protected override void MoveNext()
        {
            Model.ProduceOtherExitMaterial produceOtherExitMaterial = this.produceOtherExitMaterialManager.GetNext(this.produceOtherExitMaterial);
            if (produceOtherExitMaterial == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.produceOtherExitMaterial = this.produceOtherExitMaterialManager.Get(produceOtherExitMaterial.ProduceOtherExitMaterialId);
        }

        protected override void MovePrev()
        {
            Model.ProduceOtherExitMaterial produceOtherExitMaterial = this.produceOtherExitMaterialManager.GetPrev(this.produceOtherExitMaterial);
            if (produceOtherExitMaterial == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this.produceOtherExitMaterial = this.produceOtherExitMaterialManager.Get(produceOtherExitMaterial.ProduceOtherExitMaterialId);
        }

        protected override void MoveFirst()
        {
            this.produceOtherExitMaterial = this.produceOtherExitMaterialManager.Get(this.produceOtherExitMaterialManager.GetFirst() == null ? "" : this.produceOtherExitMaterialManager.GetFirst().ProduceOtherExitMaterialId);
        }

        protected override void MoveLast()
        {
            // if (produceOtherExitMaterial == null)
            {
                this.produceOtherExitMaterial = this.produceOtherExitMaterialManager.Get(this.produceOtherExitMaterialManager.GetLast() == null ? "" : this.produceOtherExitMaterialManager.GetLast().ProduceOtherExitMaterialId);
            }
        }

        protected override bool HasRows()
        {
            return this.produceOtherExitMaterialManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceOtherExitMaterialManager.HasRowsAfter(this.produceOtherExitMaterial);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceOtherExitMaterialManager.HasRowsBefore(this.produceOtherExitMaterial);
        }

        protected override void AddNew()
        {
            this.bindingSourceDepotPositionId.DataSource = null;
            this.produceOtherExitMaterial = new Model.ProduceOtherExitMaterial();
            this.produceOtherExitMaterial.ProduceOtherExitMaterialDate = DateTime.Now;
            this.produceOtherExitMaterial.Employee0 = BL.V.ActiveOperator.Employee;
            this.produceOtherExitMaterial.ProduceOtherExitMaterialId = this.produceOtherExitMaterialManager.GetId();// Guid.NewGuid().ToString();

            this.produceOtherExitMaterial.Details = new List<Model.ProduceOtherExitDetail>();
            if (this.action == "insert")
            {
                Model.ProduceOtherExitDetail detail = new Model.ProduceOtherExitDetail();
                detail.ProduceOtherExitDetailId = Guid.NewGuid().ToString();
                detail.ProduceQuantity = 0;
                detail.ProduceAllUserQuantity = 0;
                detail.CriterionUserQuantity = 0;
                detail.ProductStock = 0;
                detail.ProductSpecification = "";
                detail.Product = new Book.Model.Product();
                this.produceOtherExitMaterial.Details.Add(detail);
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            }
        }

        /// 选择外发合同
        private void simpleButtonXO_Click(object sender, EventArgs e)
        {
            //_produceOtherCompactDetail.Clear();
            //ProduceOtherCompact.ChooseOutContract f = new Book.UI.produceManager.ProduceOtherCompact.ChooseOutContract();
            //if (f.ShowDialog(this) != DialogResult.OK) return;
            //if (_produceOtherCompactDetail.Count != 0)
            //{
            //    this.produceOtherExitMaterial.Details.Clear();

            //    if (_produceOtherCompactDetail != null)
            //    {
            //        foreach (Model.ProduceOtherCompactDetail ProduceOtherCompactDetail in _produceOtherCompactDetail)
            //        {
            //            Model.ProduceOtherExitDetail produceOtherExitDetail = new Book.Model.ProduceOtherExitDetail();
            //            produceOtherExitDetail.ProduceOtherExitDetailId = Guid.NewGuid().ToString();
            //            if (ProduceOtherCompactDetail.Product != null)
            //            {
            //                produceOtherExitDetail.Product = ProduceOtherCompactDetail.Product;
            //                produceOtherExitDetail.ProductId = ProduceOtherCompactDetail.Product.ProductId;
            //                produceOtherExitDetail.ProductStock = ProduceOtherCompactDetail.Product.StocksQuantity;
            //                if (ProduceOtherCompactDetail.Product.MainUnit != null)
            //                {
            //                    produceOtherExitDetail.ProductUnit = ProduceOtherCompactDetail.Product.MainUnit.CnName;
            //                }
            //                produceOtherExitDetail.ProductSpecification = ProduceOtherCompactDetail.Product.ProductSpecification;
            //            }
            //            produceOtherExitDetail.InvoiceXOId = ProduceOtherCompactDetail.InvoiceXOId;
            //            produceOtherExitDetail.InvoiceXODetailId = ProduceOtherCompactDetail.InvoiceXODetailId;

            //            produceOtherExitDetail.MPSheaderId = ProduceOtherCompactDetail.MPSheaderId;
            //            produceOtherExitDetail.ProduceQuantity = ProduceOtherCompactDetail.OtherCompactCount;
            //            produceOtherExitDetail.ProduceAllUserQuantity = ProduceOtherCompactDetail.OtherCompactCount;
            //            produceOtherExitDetail.CriterionUserQuantity = ProduceOtherCompactDetail.InDepotCount;
            //            produceOtherExitDetail.ProduceOtherExitMaterial = this.produceOtherExitMaterial;
            //            produceOtherExitDetail.ProduceOtherExitMaterialId = this.produceOtherExitMaterial.ProduceOtherExitMaterialId;
            //            this.produceOtherExitMaterial.Details.Add(produceOtherExitDetail);

            //        }
            //    }
            //    this.gridControl1.RefreshDataSource();

            //}
        }

        //"+"
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this.produceOtherExitMaterial.Details.Count > 0 && this.produceOtherExitMaterial.Details[0] != null && string.IsNullOrEmpty(this.produceOtherExitMaterial.Details[0].ProductId))
                    this.produceOtherExitMaterial.Details.RemoveAt(0);
                Model.ProduceOtherExitDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.ProduceOtherExitDetail();
                        detail.ProduceOtherExitDetailId = Guid.NewGuid().ToString();
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.ProductSpecification = product.ProductSpecification;

                        detail.ProduceQuantity = 0;
                        detail.ProduceAllUserQuantity = 0;
                        detail.CriterionUserQuantity = 0;
                        detail.ProductStock = product.StocksQuantity;
                        this.produceOtherExitMaterial.Details.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    Model.Product product = f.SelectedItem as Model.Product;
                    detail = new Book.Model.ProduceOtherExitDetail();
                    detail.ProduceOtherExitDetailId = Guid.NewGuid().ToString();
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    detail.ProductSpecification = (f.SelectedItem as Model.Product).ProductSpecification;
                    detail.ProduceQuantity = 0;
                    detail.ProduceAllUserQuantity = 0;
                    detail.CriterionUserQuantity = 0;
                    detail.ProductStock = (f.SelectedItem as Model.Product).StocksQuantity;
                    this.produceOtherExitMaterial.Details.Add(detail);
                }



                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
                //this.bindingSourceProductId.DataSource = productManager.Select();
            }
        }

        //"-"
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this.produceOtherExitMaterial.Details.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceOtherExitDetail);

                if (this.produceOtherExitMaterial.Details.Count == 0)
                {
                    Model.ProduceOtherExitDetail detail = new Model.ProduceOtherExitDetail();
                    detail.ProduceOtherExitDetailId = Guid.NewGuid().ToString();
                    detail.ProduceQuantity = 0;
                    detail.ProduceAllUserQuantity = 0;
                    detail.CriterionUserQuantity = 0;
                    detail.ProductStock = 0;
                    detail.ProductSpecification = "";
                    detail.Product = new Book.Model.Product();
                    this.produceOtherExitMaterial.Details.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProduceOtherExitDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceOtherExitDetail;

            if (e.Column == this.ColProductId)
            {
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.ProduceOtherExitDetailId = Guid.NewGuid().ToString();
                    detail.ProduceQuantity = 0;
                    detail.ProduceAllUserQuantity = 0;
                    detail.CriterionUserQuantity = 0;
                    detail.Product = p;
                    detail.ProductId = p.ProductId;
                    detail.ProductStock = p.StocksQuantity;
                    detail.ProductSpecification = p.ProductSpecification;

                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "gridColumn8":
                    this.gridControl1.RefreshDataSource();
                    break;
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceOtherExitDetail> details = this.bindingSourceDetails.DataSource as IList<Model.ProduceOtherExitDetail>;
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
                //if (this.gridView1.FocusedColumn.Name == "gridColumn9")
                //{
                //    Model.ProduceOtherExitDetail detail = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceOtherExitDetail;
                //    this.repositoryItemComboBox1.Items.Clear();
                //    if (detail != null)
                //    {
                //        if (detail.DepotId != null)
                //        {
                //            IList<Model.DepotPosition> unitList = depotPositionManager.Select(detail.DepotId);
                //            foreach (Model.DepotPosition item in unitList)
                //            {
                //                this.repositoryItemComboBox1.Items.Add(item.Id);
                //            }
                //        }
                //        this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                //    }

                //}
                if (this.gridView1.FocusedColumn.Name == "gridColumnUnit")
                {
                    if (this.gridView1.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                    {
                        Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceOtherExitDetail).Product;

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

                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {

        }

        private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseContorlDepot.EditValue != null)
            {
                this.bindingSourceDepotPositionId.DataSource = this.depotPositionManager.Select(this.newChooseContorlDepot.EditValue as Model.Depot);
            }
            this.gridControl1.RefreshDataSource();
        }

        //选择委外合同
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProduceOtherCompact.ChooseOutContract f = new ProduceOtherCompact.ChooseOutContract(1);
            if (f.ShowDialog(this) == DialogResult.OK)
            {               // this.newChooseContorlDepot.EditValue = form.ProduceMaterialExit.Depot;

                if (this.produceOtherExitMaterial.Details.Count > 0 && this.produceOtherExitMaterial.Details[0] != null && string.IsNullOrEmpty(this.produceOtherExitMaterial.Details[0].ProductId))
                    this.produceOtherExitMaterial.Details.RemoveAt(0);
                if (f.keyMaterial != null && f.keyMaterial.Count > 0)
                {
                    this.newChooseContorlSipu.EditValue = f.keyMaterial[0].ProduceOtherCompact.Supplier;
                    this.textEditCompact.EditValue = f.keyMaterial[0].ProduceOtherCompact.ProduceOtherCompactId;
                    //if (f.keyMaterial[0].ProduceOtherCompact.InvoiceXOId != null)
                    //{
                    //    Model.InvoiceXO xo = this.invoiceXOManager.Get(f.keyMaterial[0].ProduceOtherCompact.InvoiceXOId);
                    //    if (xo != null)
                    this.textEditCusXOId.EditValue = f.keyMaterial[0].ProduceOtherCompact.CustomerInvoiceXOId;
                    //}
                    //else
                    //    this.textEditCusXOId.Text = null;
                    foreach (Model.ProduceOtherCompactMaterial item in f.keyMaterial)
                    {
                        Model.ProduceOtherExitDetail temp = new Book.Model.ProduceOtherExitDetail();
                        temp.ProduceOtherExitDetailId = Guid.NewGuid().ToString();
                        temp.ProduceOtherExitMaterialId = item.ProduceOtherCompactMaterialId;
                        temp.ProductId = item.ProductId;
                        temp.Product = item.Product;
                        temp.ProduceQuantity = 0;
                        //temp.ProduceRepayQuantity = 0;
                        temp.ProductStock = item.Product.StocksQuantity;
                        //   temp.InvoiceXOId = item.InvoiceXOId;
                        //temp.InvoiceXODetailId = item.InvoiceXODetailId;
                        temp.ProductUnit = item.ProductUnit;
                        temp.HandbookId = item.HandbookId;
                        temp.HandbookProductId = item.HandbookProductId;

                        this.produceOtherExitMaterial.Details.Add(temp);

                    }
                    this.bindingSourceDetails.DataSource = this.produceOtherExitMaterial.Details;
                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        private void textEditProduceOtherExitMaterialDesc_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditProduceOtherExitMaterialDesc.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        protected override string AuditKeyId()
        {
            return Model.ProduceOtherExitMaterial.PRO_ProduceOtherExitMaterialId;
        }

        protected override int AuditState()
        {
            return this.produceOtherExitMaterial.AuditState.HasValue ? this.produceOtherExitMaterial.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProduceOtherExitMaterial" + "," + this.produceOtherExitMaterial.ProduceOtherExitMaterialId;
        }

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProduceOtherExitMaterial currentModel = form.SelectItem as Model.ProduceOtherExitMaterial;
                if (currentModel != null)
                {
                    this.produceOtherExitMaterial = currentModel;
                    this.produceOtherExitMaterial = this.produceOtherExitMaterialManager.Get(this.produceOtherExitMaterial.ProduceOtherExitMaterialId);
                    this.Refresh();
                }
            }
            form.Dispose();
            GC.Collect();
        }
    }
}