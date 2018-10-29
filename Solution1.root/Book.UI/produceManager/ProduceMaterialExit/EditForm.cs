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

namespace Book.UI.produceManager.ProduceMaterialExit
{

    //*----------------------------------------------------------------
    // Copyright (C) 2008 - 2010  咸阳飛馳軟件有限公司
    //                     版權所有 圍著必究
    // 编 码 人: 马艳军             完成时间:2010-3-18
    // 修改原因：添加选择加工单的视窗
    // 修 改 人: 刘永亮                   修改时间:2011-03-08
    // 修改原因：
    // 修 改 人:                          修改时间:
    //----------------------------------------------------------------*/

    public partial class EditForm : Settings.BasicData.BaseEditForm
    {
        //  public static IList<Model.MPSheader> _mpsheader = new List<Model.MPSheader>();

        public BL.PronoteHeaderManager pronoteHeaderManager = new BL.PronoteHeaderManager();
        public Model.ProduceMaterialExit _produceMaterialExit = new Book.Model.ProduceMaterialExit();

        BL.ProduceMaterialExitManager produceMaterialExitManager = new Book.BL.ProduceMaterialExitManager();

        protected BL.DepartmentManager departmentManager = new Book.BL.DepartmentManager();

        BL.ProduceMaterialExitDetailManager produceExitDetailManager = new Book.BL.ProduceMaterialExitDetailManager();

        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        Model.Product product = new Book.Model.Product();
        protected BL.ProductManager productManager = new Book.BL.ProductManager();

        public EditForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.ProduceMaterialExit.PRO_WorkHouseId, new AA(Properties.Resources.WorkHouse, this.newChooseWorkHorseId));
            //this.requireValueExceptions.Add(Model._produceMaterialExit.PROPERTY__produceMaterialExitID, new AA(Properties.Resources.RequireDataForId, this.textEdit_produceMaterialExitID));

            //this.invalidValueExceptions.Add(Model._produceMaterialExit.PROPERTY__produceMaterialExitID, new AA(Properties.Resources.EntityExists, this.textEdit_produceMaterialExitID));
            this.requireValueExceptions.Add(Model.ProduceMaterialExitDetail.PRO_DepotPositionId, new AA(Properties.Resources.RequireChoosePosition, this.gridControl1));
            this.newChooseEmployee0.Choose = new ChooseEmployee();
            this.newChooseWorkHorseId.Choose = new Settings.ProduceManager.Workhouselog.ChooseWorkHouse();
            this.nccChooseSupplier.Choose = new Settings.BasicData.Supplier.ChooseSupplier();
            this.newChooseDepot.Choose = new ChooseDepot();
            this.EmpAudit.Choose = new ChooseEmployee();
            this.action = "view";
        }

        public EditForm(Model.ProduceMaterialExit ProduceMaterialExit)
            : this()
        {
            this._produceMaterialExit = ProduceMaterialExit;
            this._produceMaterialExit.Detail = this.produceExitDetailManager.Select(_produceMaterialExit);
            this.action = "view";
        }

        public EditForm(Model.ProduceMaterialExit ProduceMaterialExit, string action)
            : this()
        {
            this._produceMaterialExit = ProduceMaterialExit;
            this._produceMaterialExit.Detail = this.produceExitDetailManager.Select(ProduceMaterialExit);
            this.action = action;
        }

        protected override void Save()
        {
            this._produceMaterialExit.ProduceExitMaterialDesc = this.textEditDescription.Text;
            this._produceMaterialExit.WorkHouse = this.newChooseWorkHorseId.EditValue as Model.WorkHouse;
            if (this._produceMaterialExit.WorkHouse != null)
                this._produceMaterialExit.WorkHouseId = this._produceMaterialExit.WorkHouse.WorkHouseId;
            if (this.newChooseEmployee0.EditValue != null)
            {
                this._produceMaterialExit.Employee0 = this.newChooseEmployee0.EditValue as Model.Employee;
                this._produceMaterialExit.Employee0Id = this._produceMaterialExit.Employee0.EmployeeId;
            }
            if (this.nccChooseSupplier.EditValue != null)
            {
                this._produceMaterialExit.Supplier = this.nccChooseSupplier.EditValue as Model.Supplier;
                this._produceMaterialExit.SupplierId = this._produceMaterialExit.Supplier.SupplierId;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEdit1.DateTime, new DateTime()))
            {
                this._produceMaterialExit.ProduceExitMaterialDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._produceMaterialExit.ProduceExitMaterialDate = this.dateEdit1.DateTime;
            }
            this._produceMaterialExit.Depot = this.newChooseDepot.EditValue as Model.Depot;

            if (this.newChooseDepot.EditValue != null)
            {
                this._produceMaterialExit.DepotId = this._produceMaterialExit.Depot.DepotId;
            }
            this._produceMaterialExit.PronoteHeaderID = this.tEtPronoteHeaderId.Text;
            this._produceMaterialExit.CustomerInvoiceXOId = this.textEditCustomerXOId.Text;
            this._produceMaterialExit.AuditState = this.saveAuditState;
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            switch (this.action)
            {
                case "insert":
                    this.produceMaterialExitManager.Insert(this._produceMaterialExit);
                    break;
                case "update":
                    this.produceMaterialExitManager.Update(this._produceMaterialExit);
                    break;
            }

        }

        protected override void Delete()
        {

            if (this._produceMaterialExit == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            this.produceMaterialExitManager.Delete(this._produceMaterialExit);
            this._produceMaterialExit = this.produceMaterialExitManager.GetNext(this._produceMaterialExit);
            if (this._produceMaterialExit == null)
            {
                this._produceMaterialExit = this.produceMaterialExitManager.GetLast();
            }
        }

        public override void Refresh()
        {

            if (this._produceMaterialExit == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._produceMaterialExit = this.produceMaterialExitManager.GetDetails(_produceMaterialExit.ProduceMaterialExitId);
                }
            }


            this.textEditExitId.EditValue = this._produceMaterialExit.ProduceMaterialExitId;
            this.textEditDescription.Text = this._produceMaterialExit.ProduceExitMaterialDesc;
            this.newChooseWorkHorseId.EditValue = this._produceMaterialExit.WorkHouse;

            this.tEtPronoteHeaderId.Text = this._produceMaterialExit.PronoteHeaderID;
            this.textEditCustomerXOId.EditValue = this._produceMaterialExit.CustomerInvoiceXOId;
            if (!string.IsNullOrEmpty(this._produceMaterialExit.PronoteHeaderID))
            {
                Model.PronoteHeader pronoteHeader = this.pronoteHeaderManager.Get(this._produceMaterialExit.PronoteHeaderID);
                if (pronoteHeader != null)
                {
                    Model.Product product = pronoteHeader.Product;
                    this.textEditProId.Text = product.Id;
                    this.textEditProName.Text = product.ProductName;
                    this.textEditCusProName.Text = product.CustomerProductName;
                }
                else
                {
                    this.textEditProId.EditValue = null;
                    this.textEditProName.EditValue = null;
                    this.textEditCusProName.EditValue = null;
                }
            }
            else
            {
                this.textEditProId.EditValue = null;
                this.textEditProName.EditValue = null;
                this.textEditCusProName.EditValue = null;

            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this._produceMaterialExit.ProduceExitMaterialDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEdit1.EditValue = null;
            }
            else
            {
                this.dateEdit1.EditValue = this._produceMaterialExit.ProduceExitMaterialDate;
            }
            this.EmpAudit.EditValue = this._produceMaterialExit.AuditEmp;
            this.textEditAuditState.Text = this.GetAuditName(this._produceMaterialExit.AuditState);

            this.newChooseEmployee0.EditValue = this._produceMaterialExit.Employee0;
            this.newChooseDepot.EditValue = this._produceMaterialExit.Depot;
            this.nccChooseSupplier.EditValue = this._produceMaterialExit.Supplier;
            this.bindingSourceDetails.DataSource = this._produceMaterialExit.Detail;
            base.Refresh();

            switch (this.action)
            {
                case "insert":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.barBtnSearch.Enabled = false;
                    this.barButtonItem1.Enabled = true;
                    break;
                case "update":
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.barBtnSearch.Enabled = false;
                    this.barButtonItem1.Enabled = true;
                    break;
                case "view":
                    this.gridView1.OptionsBehavior.Editable = false;
                    this.barBtnSearch.Enabled = true;
                    this.barButtonItem1.Enabled = false;
                    break;

            }

            this.textEditExitId.Properties.ReadOnly = true;
        }

        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {
            return new RO(_produceMaterialExit.ProduceMaterialExitId);
        }

        protected override void MoveNext()
        {
            Model.ProduceMaterialExit produceMaterialExit = this.produceMaterialExitManager.GetNext(this._produceMaterialExit);
            if (produceMaterialExit == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceMaterialExit = this.produceMaterialExitManager.Get(produceMaterialExit.ProduceMaterialExitId);
        }

        protected override void MovePrev()
        {
            Model.ProduceMaterialExit produceMaterialExit = this.produceMaterialExitManager.GetPrev(this._produceMaterialExit);
            if (produceMaterialExit == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);
            this._produceMaterialExit = this.produceMaterialExitManager.Get(produceMaterialExit.ProduceMaterialExitId);
        }

        protected override void MoveFirst()
        {
            this._produceMaterialExit = this.produceMaterialExitManager.Get(this.produceMaterialExitManager.GetFirst() == null ? "" : this.produceMaterialExitManager.GetFirst().ProduceMaterialExitId);
        }

        protected override bool SetColumnNumber()
        {
            return true;
        }

        protected override void MoveLast()
        {
            // if (_produceMaterialExit == null)
            {
                this._produceMaterialExit = this.produceMaterialExitManager.Get(this.produceMaterialExitManager.GetLast() == null ? "" : this.produceMaterialExitManager.GetLast().ProduceMaterialExitId);
            }
        }

        protected override bool HasRows()
        {
            return this.produceMaterialExitManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.produceMaterialExitManager.HasRowsAfter(this._produceMaterialExit);
        }

        protected override bool HasRowsPrev()
        {
            return this.produceMaterialExitManager.HasRowsBefore(this._produceMaterialExit);
        }

        protected override void AddNew()
        {

            this._produceMaterialExit = new Model.ProduceMaterialExit();
            this._produceMaterialExit.ProduceMaterialExitId = this.produceMaterialExitManager.GetId();// Guid.NewGuid().ToString();
            this._produceMaterialExit.ProduceExitMaterialDate = DateTime.Now;
            this._produceMaterialExit.Employee0 = BL.V.ActiveOperator.Employee;
            this._produceMaterialExit.Detail = new List<Model.ProduceMaterialExitDetail>();
            //if (this.action == "insert")
            //{
            //    Model.ProduceMaterialExitDetail detail = new Model.ProduceMaterialExitDetail();
            //    detail.Inumber = this._produceMaterialExit.Detail.Count + 1;
            //   // detail.ProduceExitMaterialDetailId = Guid.NewGuid().ToString();
            //  //  detail.MPSheaderId = null;
            //    detail.ProduceQuantity = 0;
            //    detail.ProductStock = 0;
            //    detail.Product = new Book.Model.Product();
            //    this._produceMaterialExit.Detail.Add(detail);
            //    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
            //}
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProduceMaterialExitDetail> details = this.bindingSourceDetails.DataSource as IList<Model.ProduceMaterialExitDetail>;
            if (details == null || details.Count < 1) return;
            Model.Product detail = details[e.ListSourceRowIndex].Product;
            switch (e.Column.Name)
            {
                case "colProductId":
                    if (detail == null) return;
                    e.DisplayText = string.IsNullOrEmpty(detail.Id) ? "" : detail.Id;
                    break;
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {

            if (this.action == "insert" || this.action == "update")
            {
                if (this.gridView1.FocusedColumn.Name == "gridColumnUnit")
                {
                    Model.Product p = (this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.ProduceMaterialExitDetail).Product;
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
                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {


            //this.bindingSourceDepot.DataSource = new BL.DepotManager().Select();
            //if (this.action != "insert")
            //    this.bindingSourceDepotPosition.DataSource = new BL.DepotPositionManager().Select();

        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceDetails.Current != null)
            {
                this._produceMaterialExit.Detail.Remove(this.bindingSourceDetails.Current as Book.Model.ProduceMaterialExitDetail);

                if (this._produceMaterialExit.Detail.Count == 0)
                {
                    Model.ProduceMaterialExitDetail detail = new Model.ProduceMaterialExitDetail();
                    detail.ProduceMaterialExitDetailId = Guid.NewGuid().ToString();
                    //detail.MPSheaderId = null;
                    detail.ProduceQuantity = 0;
                    detail.ProductStock = 0;
                    detail.Product = new Book.Model.Product();

                    this._produceMaterialExit.Detail.Add(detail);
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {

            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this._produceMaterialExit.Detail.Count > 0 && string.IsNullOrEmpty(this._produceMaterialExit.Detail[0].ProductId))
                    this._produceMaterialExit.Detail.RemoveAt(0);
                Model.ProduceMaterialExitDetail detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.ProduceMaterialExitDetail();
                        detail.ProduceMaterialExitDetailId = Guid.NewGuid().ToString();
                        detail.Inumber = this._produceMaterialExit.Detail.Count + 1;
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        //detail.ProduceAllUserQuantity = 0;
                        detail.ProduceQuantity = 0;
                        //  detail.ProductStock = product.StocksQuantity;
                        detail.ProductUnit = detail.Product.DepotUnit == null ? null : detail.Product.DepotUnit.CnName;
                        //detail.ProductSpecification = (f.SelectedItem as Model.Product).ProductSpecification;
                        this._produceMaterialExit.Detail.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    detail = new Book.Model.ProduceMaterialExitDetail();
                    detail.Inumber = this._produceMaterialExit.Detail.Count + 1;
                    Model.Product product = f.SelectedItem as Model.Product;
                    detail.ProduceMaterialExitDetailId = Guid.NewGuid().ToString();
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    // detail.ProduceAllUserQuantity = 0;
                    detail.ProduceQuantity = 0;
                    detail.ProductUnit = detail.Product.DepotUnit == null ? null : detail.Product.DepotUnit.CnName;
                    //   detail.ProductStock = (f.SelectedItem as Model.Product).StocksQuantity;
                    //detail. = detail.Product.MainUnit == null ? null : detail.Product.MainUnit.CnName;
                    //detail.ProductSpecification = (f.SelectedItem as Model.Product).ProductSpecification;
                    this._produceMaterialExit.Detail.Add(detail);

                }
                this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
                // this.bindingSourceProductId.DataSource = productManager.Select();
            }
        }

        private void EditForm_Load_1(object sender, EventArgs e)
        {
            string sql = "SELECT productid,id,productname FROM product";
            this.bindingSourceProductId.DataSource = this.productManager.DataReaderBind<Model.Product>(sql, null, CommandType.Text);
            // this.bindingSourceProductId.DataSource = productManager.GetProduct();

            //this.bindingSourceDepot.DataSource = new BL.DepotManager().Select();
            //if (this.action != "insert")
            //    this.bindingSourceDepotPosition.DataSource = new BL.DepotPositionManager().Select();

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridView1_CellValueChanging_1(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.ProduceMaterialExitDetail detail = this.gridView1.GetRow(e.RowHandle) as Model.ProduceMaterialExitDetail;

            if (e.Column == this.ColProductId)
            {
                if (detail != null)
                {
                    Model.Product p = productManager.Get(e.Value.ToString());
                    detail.ProduceMaterialExitDetailId = Guid.NewGuid().ToString();
                    detail.ProduceQuantity = 0;
                    // detail.ProduceAllUserQuantity = 0;
                    detail.Product = p;
                    detail.ProductStock = p.StocksQuantity;
                    detail.ProductId = p.ProductId;
                    //  detail.DepotPosition = null;
                    this.bindingSourceDetails.Position = this.bindingSourceDetails.IndexOf(detail);
                }
                this.gridControl1.RefreshDataSource();
            }
            //if (e.Column == this.gridColumnDepot)
            //{
            //    detail.DepotPosition = null;
            //}
        }

        private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        {
            if (this.newChooseDepot.EditValue != null)
                this.bindingSourceDepotPosition.DataSource = this.depotPositionManager.Select(this.newChooseDepot.EditValue as Model.Depot);
            //if (this.newChooseContorlDepot.EditValue != null)
            //{
            //    IList<Model.DepotPosition> unitList = depotPositionManager.Select(this.newChooseContorlDepot.EditValue.ToString());
            //    foreach (Model.DepotPosition item in unitList)
            //    {
            //        this.repositoryItemComboBox1.Items.Add(item.Id);
            //    }
            //}
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            PronoteHeader.ChoosePronoteHeaderForm f = new Book.UI.produceManager.PronoteHeader.ChoosePronoteHeaderForm();
            if (f.ShowDialog(this) != DialogResult.OK) return;
            if (PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList.Count != 0)
            {
                Model.PronoteHeader pronoteHeader = PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList[0].PronoteHeader;

                this._produceMaterialExit.PronoteHeaderID = pronoteHeader.PronoteHeaderID;
                this.textEditProId.Text = pronoteHeader.Product.Id;
                this.textEditProName.Text = pronoteHeader.Product.ProductName;
                this.textEditCusProName.Text = pronoteHeader.Product.CustomerProductName;
                //if (!string.IsNullOrEmpty(pronoteHeader.InvoiceXOId))
                //{
                //    Model.InvoiceXO xo = new BL.InvoiceXOManager().Get(pronoteHeader.InvoiceXOId);
                //    if (xo != null)
                this.textEditCustomerXOId.Text = pronoteHeader.InvoiceCusId;
                //    else
                //        this.textEditCustomerXOId.EditValue = null;
                //}
                //else
                //    this.textEditCustomerXOId.EditValue = null;
                this.tEtPronoteHeaderId.Text = pronoteHeader.PronoteHeaderID;
                foreach (Model.PronotedetailsMaterial pronoteMaterial in PronoteHeader.ChoosePronoteHeaderForm._PronotedetailsMaterialList)
                {
                    Model.ProduceMaterialExitDetail produceMaterialExitDetail = new Book.Model.ProduceMaterialExitDetail();
                    produceMaterialExitDetail.ProduceMaterialExitDetailId = Guid.NewGuid().ToString();
                    produceMaterialExitDetail.Product = pronoteMaterial.Product;
                    produceMaterialExitDetail.Inumber = this._produceMaterialExit.Detail.Count + 1;
                    produceMaterialExitDetail.ProductId = pronoteMaterial.ProductId;
                 
                    produceMaterialExitDetail.HandbookId = pronoteMaterial.PronoteHeader.HandbookId;
                    produceMaterialExitDetail.HandbookProductId = pronoteMaterial.PronoteHeader.HandbookProductId;

                    //   produceMaterialExitDetail.PronotedetailsMaterialId = pronoteMaterial.PronotedetailsMaterialId;
                    produceMaterialExitDetail.ProduceQuantity = 0;
                    if (produceMaterialExitDetail.Product != null)
                    {
                        if (pronoteMaterial.Product.DepotUnit != null)
                            produceMaterialExitDetail.ProductUnit = pronoteMaterial.Product.DepotUnit.CnName;
                        // produceMaterialExitDetail. = pronotedetails.Product.ProductSpecification;                                  }
                        //单位  pronotedetails. =  pronotedetails.ProductUnit;
                        //pronotedetails.InDepotQuantity = Convert.ToDouble(mpsdetail.MPSdetailssum);                     
                        //produceMaterialExitDetail.ProduceAllUserQuantity = pronotedetails.DetailsSum;
                        //produceMaterialExitDetail. = this._produceMaterialExit;
                        //produceMaterialExitDetail._produceMaterialExitID = this._produceMaterialExit._produceMaterialExitID
                        //produceMaterialExitDetail.MPSheaderId = pronotedetails.MPSheaderId;
                        //produceMaterialExitDetail.InvoiceXOId = pronotedetails.InvoiceXOId;
                        //produceMaterialExitDetail.InvoiceXODetailId = pronotedetails.InvoiceXODetailId;                      
                    }
                    this._produceMaterialExit.Detail.Add(produceMaterialExitDetail);
                }
                this.gridControl1.RefreshDataSource();

            }
        }

        //选择说明参数
        private void textEditDescription_DoubleClick(object sender, EventArgs e)
        {
            PCParameterSet.ChooseParameter cp = new Book.UI.produceManager.PCParameterSet.ChooseParameter("BeiZhuShuoMing");
            if (cp.ShowDialog(this) != DialogResult.OK) return;
            if (cp.SelectedItem != null)
            {
                Model.Setting setParam = cp.SelectedItem as Model.Setting;
                this.textEditDescription.Text += setParam.SettingCurrentValue;
            }
            cp.Dispose();
            GC.Collect();
        }

        #region 审核
        protected override string AuditKeyId()
        {
            return Model.ProduceMaterialExit.PRO_ProduceMaterialExitId;
        }

        protected override int AuditState()
        {
            return this._produceMaterialExit.AuditState.HasValue ? this._produceMaterialExit.AuditState.Value : 0;
        }

        protected override string tableCode()
        {
            return "ProduceMaterialExit" + "," + this._produceMaterialExit.ProduceMaterialExitId;
        }
        #endregion

        private void barBtnSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ListForm form = new ListForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProduceMaterialExit currentModel = form.SelectItem as Model.ProduceMaterialExit;
                if (currentModel != null)
                {
                    this._produceMaterialExit = currentModel;
                    this._produceMaterialExit = this.produceMaterialExitManager.GetDetails(_produceMaterialExit.ProduceMaterialExitId);
                    this.Refresh();
                }
            }
        }
    }
}