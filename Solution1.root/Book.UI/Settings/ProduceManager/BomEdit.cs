using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Helper;
using Book.UI.Invoices;
using DevExpress.XtraTreeList.Nodes;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Linq;

namespace Book.UI.Settings.ProduceManager
{
    /*----------------------------------------------------------------
// Copyright (C) 2008 - 2010   咸阳飛馳軟件有限公司
//                     版權所有 圍著必究

// 编 码 人: 裴盾            完成时间:2009-11-12
// 修改原因：
// 修 改 人:                          修改时间:
// 修改原因：
// 修 改 人:                          修改时间:
//----------------------------------------------------------------*/
    public partial class BomEdit : BasicData.BaseEditForm
    {
        private BL.BomParentPartInfoManager bomParmentInfoManager = new Book.BL.BomParentPartInfoManager();
        private BL.ProductManager productManager = new Book.BL.ProductManager();
        private Model.BomParentPartInfo _bomParmentPartInfo;
        private BL.DepotManager depotManager = new Book.BL.DepotManager();
        private BL.DepotPositionManager depotPositionManager = new Book.BL.DepotPositionManager();
        private BL.ProductUnitManager productUnitManager = new Book.BL.ProductUnitManager();
        private BL.BomPackageDetailsManager package = new Book.BL.BomPackageDetailsManager();
        private Model.Customer _customer = new Book.Model.Customer();
        //private BL.ProcessingManager processingManager = new Book.BL.ProcessingManager();
        private BL.ProcessCategoryManager processCategoryManager = new Book.BL.ProcessCategoryManager();
        private BL.BOMProductProcessManager bomProductProcess = new Book.BL.BOMProductProcessManager();
        private BL.BomComponentInfoManager BomComManager = new Book.BL.BomComponentInfoManager();
        private BL.ProductProcessManager productProcessManager = new Book.BL.ProductProcessManager();
        private BL.TechonlogyHeaderManager techonlogyHeaderManager = new BL.TechonlogyHeaderManager();
        //客户包装
        //   private BL.CustomerPackageDetailManager customerPackageDetailManager = new Book.BL.CustomerPackageDetailManager();
        // private BL.MaterialTypeManager materialTypeManager = new Book.BL.MaterialTypeManager();
        private BL.CustomerManager customerManager = new Book.BL.CustomerManager();
        // private BL.ManProcedureManager manProcedureManager = new BL.ManProcedureManager();
        private BL.TechnologydetailsManager technologydetailsManager = new BL.TechnologydetailsManager();
        private Model.TechonlogyHeader _techonlogyHeader;
        // BL.CustomerProcessingDetailManager CustomerProcessingDetailManager = new Book.BL.CustomerProcessingDetailManager();
        private int flag = 0;
        private Model.BomParentPartInfo searchBom;

        #region 构造函数
        public BomEdit()
        {
            InitializeComponent();
            this.invalidValueExceptions.Add("DeleteError", new AA(Properties.Resources.DeleteError, this));
            this.requireValueExceptions.Add(Model.BomParentPartInfo.PRO_Id, new AA("請填寫Bom編號", this.textEditId));
            this.requireValueExceptions.Add(Model.BomParentPartInfo.PRO_ProductId, new AA("請選擇母件", this.newChooseContorlProductId));
            this.invalidValueExceptions.Add(Model.BomParentPartInfo.PRO_ProductId, new AA("該母件已經存在于BOM中,請選擇其他母件", this.newChooseContorlProductId));

            this.invalidValueExceptions.Add(Model.BomParentPartInfo.PRO_Id, new AA(Properties.Resources.EntityExists, this.textEditId));
            this.invalidValueExceptions.Add(Model.BomComponentInfo.PROPERTY_INDEXOFBOM, new AA(Properties.Resources.IndexOfBom, this.gridControl1 as Control));
            this.requireValueExceptions.Add(Model.BomComponentInfo.PROPERTY_INDEXOFBOM, new AA(Properties.Resources.indexofbomNullDate, this.gridControl1 as Control));


            this.newChooseContorlCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.newChooseContorlEmployeeAddId.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.newChooseContorlEmployeeUpdateId.Choose = new Settings.BasicData.Employees.ChooseEmployee();
            this.action = "insert";
            this.MdiParent = base.MdiParent;
            this.WindowState = FormWindowState.Maximized;


        }
        public BomEdit(Model.BomParentPartInfo mat)
            : this()
        {
            this._bomParmentPartInfo = mat;

            this.action = "update";
            flag = 1;
        }
        public BomEdit(Model.BomParentPartInfo mat, string action)
            : this()
        {
            this._bomParmentPartInfo = mat;
            this.action = action;
            flag = 1;
        }


        private Model.BomComponentInfo bomCom = null;
        public BomEdit(Model.BomParentPartInfo mat, string action, Model.BomComponentInfo bomCom)
            : this()
        {
            this._bomParmentPartInfo = mat;
            this.action = action;
            this.bomCom = bomCom;

        }
        #endregion

        #region 重写

        protected override void AddNew()
        {
            this._bomParmentPartInfo = new Book.Model.BomParentPartInfo();
            this._bomParmentPartInfo.BomId = Guid.NewGuid().ToString();
            this._bomParmentPartInfo.InsertTime = DateTime.Now;
            this._bomParmentPartInfo.CreateMan = BL.V.ActiveOperator.OperatorName;
            this._bomParmentPartInfo.EffectiveDate = DateTime.Now;
            this._bomParmentPartInfo.Id = this.bomParmentInfoManager.GetId();
            this.treeList1.ClearNodes();
            //  Model.BOMProductProcess bomParmentPartInfo=new Book.Model.BOMProductProcess();
            // bomParmentPartInfo.BOMProductProcessId=Guid.NewGuid().ToString();

            // this._bomParmentPartInfo.BOMProductProcess.Add(bomParmentPartInfo);




            this.action = "insert";
        }

        protected override void Delete()
        {
            if (this._bomParmentPartInfo == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            try
            {
                this.bomParmentInfoManager.Delete(this._bomParmentPartInfo);
            }
            catch (Helper.InvalidValueException ex)
            {
                if (this.invalidValueExceptions.ContainsKey(ex.Message))
                {
                    AA a = this.invalidValueExceptions[ex.Message];
                    MessageBox.Show(a.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                throw;
            }
            if (this._bomParmentPartInfo.BomId == this.searchBom.BomId)
            {
                this.treeList1.ClearNodes();
                this.action = "insert";
                this.AddNew();
                this.Refresh();
            }
            else
                this.treeLoad(this.searchBom);


            //this._bomParmentPartInfo = this.bomParmentInfoManager.GetNext(this._bomParmentPartInfo);
            //if (this._bomParmentPartInfo == null)
            //{
            //    this._bomParmentPartInfo = this.bomParmentInfoManager.GetLast();
            //}


            // this.treeLoad1();
        }

        protected override bool HasRows()
        {
            return this.bomParmentInfoManager.HasRows1();
        }

        protected override bool HasRowsNext()
        {
            return this.bomParmentInfoManager.HasRowsAfter1(this._bomParmentPartInfo);
        }

        protected override bool HasRowsPrev()
        {
            return this.bomParmentInfoManager.HasRowsBefore1(this._bomParmentPartInfo);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.textEditId, this.newChooseContorlProductId, this });
        }

        protected override void MoveFirst()
        {
            this._bomParmentPartInfo = this.bomParmentInfoManager.GetFirst1();
        }

        protected override void MoveLast()
        {

            //if (this._bomParmentPartInfo == null)
            this._bomParmentPartInfo = this.bomParmentInfoManager.GetLast1();

        }

        protected override void MoveNext()
        {


            Model.BomParentPartInfo bomParmentPartInfo = this.bomParmentInfoManager.GetNext1(this._bomParmentPartInfo);
            if (bomParmentPartInfo == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._bomParmentPartInfo = bomParmentPartInfo;

        }

        protected override void MovePrev()
        {
            Model.BomParentPartInfo bomParmentPartInfo = this.bomParmentInfoManager.GetPrev1(this._bomParmentPartInfo);
            if (bomParmentPartInfo == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._bomParmentPartInfo = bomParmentPartInfo;
        }
        protected override DevExpress.XtraReports.UI.XtraReport GetReport()
        {

            // return new XtraReport1();

            if (this._bomParmentPartInfo == null)
                return null;

            //return new MaterialXR(XRband(), this._bomParmentPartInfo);
            return new ROBOM(this._bomParmentPartInfo);

        }

        public override void Refresh()
        {

            if (this._bomParmentPartInfo == null)
            {
                this.AddNew();
                this.action = "insert";
            }
            else
            {
                if (this.action == "view")
                {
                    this._bomParmentPartInfo = this.bomParmentInfoManager.Get(_bomParmentPartInfo.BomId);

                    if (_bomParmentPartInfo != null)
                    {
                        this._bomParmentPartInfo.BomPackageDetails = this.package.Select(this._bomParmentPartInfo.BomId);
                        //   this._bomParmentPartInfo.BOMProductProcess = this.bomProductProcess.Select(this._bomParmentPartInfo.BomId);
                        this._bomParmentPartInfo.ProductProcessDetail = this.productProcessManager.SelectByBomId(this._bomParmentPartInfo.BomId);
                        // this._bomParmentPartInfo.CustomerProcessingDetail = this.CustomerProcessingDetailManager.SelectbyBomId(this._bomParmentPartInfo.BomId);

                    }
                }
                if (this._bomParmentPartInfo == null)
                    this.AddNew();
            }


            //this.newChooseCustomer.EditValue = this._bomParmentPartInfo.Customer;
            //this.textEditCustomerProductName.EditValue = this._bomParmentPartInfo.CustomerProductName;
            //this.buttonEditCustomerPackage.EditValue = this._bomParmentPartInfo.CustomerPackage;
            this.bindingSourceBomComponentInfo.DataSource = this._bomParmentPartInfo.Components;

            if (this._bomParmentPartInfo.Product != null)
            {
                if (this._bomParmentPartInfo.Product.OutSourcing != null && this._bomParmentPartInfo.Product.OutSourcing.Value)
                    this.textEditSourceType.Text = Properties.Resources.IsOutSourcing;
                if (this._bomParmentPartInfo.Product.Consume != null && this._bomParmentPartInfo.Product.Consume.Value)
                    this.textEditSourceType.Text = "耗用";
                if (this._bomParmentPartInfo.Product.TrustOut != null && this._bomParmentPartInfo.Product.TrustOut.Value)
                    this.textEditSourceType.Text = "委外";
                if (this._bomParmentPartInfo.Product.HomeMade != null && this._bomParmentPartInfo.Product.HomeMade.Value)
                    this.textEditSourceType.Text = Properties.Resources.IsHomeMade;

                this.newChooseContorlCustomer.EditValue = this._bomParmentPartInfo.Product.Customer;
            }
            else
            {
                this.textEditSourceType.Text = "";
                this.newChooseContorlCustomer.EditValue = null;
                this.buttonEditTechonlogyHeaderid.EditValue = null;
            }


            if (!string.IsNullOrEmpty(this._bomParmentPartInfo.TechonlogyHeaderId))
            {
                this._techonlogyHeader = this.techonlogyHeaderManager.Get(this._bomParmentPartInfo.TechonlogyHeaderId) as Model.TechonlogyHeader;
                this.buttonEditTechonlogyHeaderid.EditValue = this._techonlogyHeader;

            }
            else
            {
                this._techonlogyHeader = null;
                this.buttonEditTechonlogyHeaderid.EditValue = null;
            }
            //if (this.bomCom != null)
            //    this.bindingSourceBomComponentInfo.Position = this.bindingSourceBomComponentInfo.IndexOf(this.bomCom);
            //this.gridControl1.RefreshDataSource();
            //this.bomCom = null;


            // this.buttonEditTechonlogyHeaderid.EditValue = this.manProcedureManager.Select(this._bomParmentPartInfo, this._customer);

            //////this.bindingSourceBomPackageDetails.DataSource = this.customerPackageDetailManager.GetByPackageId(this._bomParmentPartInfo.CustomerPackageId);

            this.bindingSourceBOMProcess.DataSource = this._bomParmentPartInfo.ProductProcessDetail;
            // this.bindingSourceBOMProcess.DataSource=this._bomParmentPartInfo.ProductProcessDetail;
            if (this._techonlogyHeader != null)
                this.bindingSourceBOMProcess.DataSource = this.technologydetailsManager.Select(this._techonlogyHeader);
            else
                this.bindingSourceBOMProcess.DataSource = null;

            this.bindingSourceBomPackageDetails.DataSource = this._bomParmentPartInfo.BomPackageDetails;

            // this.comboBoxEditMaterialType.EditValue = this._bomParmentPartInfo.MaterialType;

            // this.textEditBomDescription.Text = this._bomParmentPartInfo.BomDescription;

            this.textEditId.Text = string.IsNullOrEmpty(this._bomParmentPartInfo.Id) ? "" : this._bomParmentPartInfo.Id;

            //  this.textEditBomType.Text = this._bomParmentPartInfo.BomType;
            this.textEditVersion.Text = this._bomParmentPartInfo.BomVersion;
            this.radioState.SelectedIndex = this._bomParmentPartInfo.Status.HasValue ? this._bomParmentPartInfo.Status.Value : 0;

            // this.calcEditLossRate.Value = this._bomParmentPartInfo.LossRate == null ? 0 : decimal.Parse(this._bomParmentPartInfo.LossRate.ToString());
            this.spinEditDefaultQuantity.Value = this._bomParmentPartInfo.DefaultQuantity == null ? 0 : decimal.Parse(this._bomParmentPartInfo.DefaultQuantity.ToString());
            // this.dateEditEffectiveDate.EditValue = this._bomParmentPartInfo.EffectiveDate;
            this.newChooseContorlProductId.EditValue = this._bomParmentPartInfo.Product == null ? null : this._bomParmentPartInfo.Product.Id;
            this.nccSupplier.EditValue = this._bomParmentPartInfo.Product == null ? null : this._bomParmentPartInfo.Product.Supplier;
            //aa = this._bomParmentPartInfo.Product == null ? null : this._bomParmentPartInfo.Product.ProductId;
            this.dateEditInsertTime.EditValue = this._bomParmentPartInfo.InsertTime;
            // this.dateEditUpdateTime.EditValue = this._bomParmentPartInfo.UpdateTime;
            this.newChooseContorlEmployeeAddId.EditValue = this._bomParmentPartInfo.EmployeeAdd;
            this.newChooseContorlEmployeeUpdateId.EditValue = this._bomParmentPartInfo.EmployeeUpdate;
            if (this._bomParmentPartInfo.Product != null)
            {
                this.textEditParentsModel.Text = this._bomParmentPartInfo.Product.ProductSpecification;
                this.textEditParentsName.Text = this._bomParmentPartInfo.Product.IsCustomerProduct == true ? this._bomParmentPartInfo.Product.ProductName + "{" + this._bomParmentPartInfo.Product.CustomerProductName + "}" : this._bomParmentPartInfo.Product.ProductName;
            }
            else
            {
                this.textEditParentsModel.Text = string.Empty;
                this.textEditParentsName.Text = string.Empty;
            }

            flag = 0;
            switch (this.action)
            {
                case "insert":


                    //this.buttonEditCustomerPackage.Properties.Buttons[0].Visible = true;
                    //this.buttonEditCustomerPackage.Properties.ReadOnly = false;
                    //this.textEditCustomerProductName.Properties.ReadOnly = true;

                    //if (this.buttonEditTechonlogyHeaderid.EditValue != null)
                    //{
                    //    this.newChooseContorlCustomer.ShowButton = true;
                    //    this.newChooseContorlCustomer.ButtonReadOnly = false;
                    //}
                    //else
                    //{
                    //    this.newChooseContorlCustomer.ShowButton = false ;
                    //    this.newChooseContorlCustomer.ButtonReadOnly = true;
                    //}


                    this.textEditParentsModel.Properties.ReadOnly = false;
                    this.textEditId.Properties.ReadOnly = false;
                    //this.textEditBomDescription.Properties.ReadOnly = false;
                    //this.comboBoxEditMaterialType.Enabled = true;
                    //this.comboBoxEditMaterialType.Properties.ReadOnly = false;

                    //this.textEditBomVersion.Properties.ReadOnly = false;

                    this.gridView1.OptionsBehavior.Editable = true;
                    this.gridView3.OptionsBehavior.Editable = false;

                    //this.dateEditEffectiveDate.Properties.ReadOnly = false;
                    //this.dateEditEffectiveDate.Properties.Buttons[0].Visible = true;

                    this.spinEditDefaultQuantity.Properties.ReadOnly = false;
                    this.spinEditDefaultQuantity.Properties.Buttons[0].Visible = true;

                    // this.calcEditLossRate.Properties.ReadOnly = false;
                    //this.calcEditLossRate.Properties.Buttons[0].Visible = true;

                    this.newChooseContorlProductId.Properties.Buttons[0].Visible = true;
                    //this.newChooseContorlProductId.Properties.ReadOnly = false;
                    //this.buttonEditCustomerPackage.Properties.ReadOnly = false;
                    //this.buttonEditCustomerPackage.Properties.Buttons[0].Visible = true;

                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.barButtonItemCopy.Enabled = false;

                    this.simpleButton1Add.Enabled = true;




                    this.barButtonItem1.Enabled = false;


                    this.simpleButtonProciss.Enabled = false;

                    this.simpleButton2Remove.Enabled = false;
                    this.simpleButton2Add.Enabled = false;



                    this.simpleButtonProciss.Visible = false;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2Remove.Visible = false;
                    this.simpleButton2Add.Visible = false;

                    this.simpleButton3Remover.Enabled = true;
                    break;
                case "update":

                    this.textEditParentsModel.Properties.ReadOnly = false;
                    this._bomParmentPartInfo.ModifyMan = BL.V.ActiveOperator.OperatorName;
                    this._bomParmentPartInfo.UpdateTime = DateTime.Now;
                    this.textEditId.Properties.ReadOnly = true;
                    this.gridView1.OptionsBehavior.Editable = true;
                    this.spinEditDefaultQuantity.Properties.ReadOnly = false;
                    this.spinEditDefaultQuantity.Properties.Buttons[0].Visible = true;
                    this.simpleButtonAppend.Enabled = true;
                    this.simpleButtonRemove.Enabled = true;
                    this.newChooseContorlProductId.Properties.Buttons[0].Visible = true;
                    this.barButtonItemCopy.Enabled = false;
                    this.simpleButton1Add.Enabled = true;
                    this.gridView3.OptionsBehavior.Editable = false;

                    this.barButtonItem1.Enabled = false;


                    this.simpleButtonProciss.Visible = false;
                    this.simpleButton1.Enabled = true;
                    this.simpleButton2Remove.Visible = false;
                    this.simpleButton2Add.Visible = false;

                    this.simpleButton3Remover.Enabled = true;

                    break;
                case "view":
                    this.newChooseContorlCustomer.ShowButton = false;
                    this.newChooseContorlCustomer.ButtonReadOnly = true;

                    this.textEditParentsModel.Properties.ReadOnly = true;
                    this.textEditId.Properties.ReadOnly = true;

                    this.gridView1.OptionsBehavior.Editable = false;

                    this.spinEditDefaultQuantity.Properties.ReadOnly = true;
                    this.spinEditDefaultQuantity.Properties.Buttons[0].Visible = false;

                    this.simpleButtonAppend.Enabled = false;
                    this.simpleButtonRemove.Enabled = false;
                    this.barButtonItemCopy.Enabled = true;
                    this.newChooseContorlProductId.Properties.Buttons[0].Visible = false;
                    this.simpleButton1Add.Enabled = false;


                    this.simpleButton2Remove.Enabled = false;
                    this.gridView3.OptionsBehavior.Editable = false;

                    this.barButtonItem1.Enabled = true;


                    this.simpleButtonProciss.Enabled = false;
                    this.simpleButton1.Enabled = false;
                    this.simpleButton2Remove.Enabled = false;
                    this.simpleButton2Add.Enabled = false;

                    this.simpleButton3Remover.Enabled = false;


                    //this.newChooseContorlProductId.Properties.ReadOnly = true;

                    break;
                default:
                    break;
            }

            base.Refresh();
            this.textEditSearchName.Properties.ReadOnly = false;
            this.simpleButtonSearch.Enabled = true;
            this.comboBoxEdit1.Properties.ReadOnly = false;
            this.comboBoxEdit1.Enabled = true;

            //if (this.action == "insert")
            //{
            //    this.textEditCustomerProductName.Properties.ReadOnly = true  ;
            //    this.newChooseCustomer.ShowButton = false;
            //    this.newChooseCustomer.ButtonReadOnly = true;
            //}
        }

        protected override void Save()
        {
            if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
                return;

            //if (this.newChooseCustomer.EditValue != null)
            //    this._bomParmentPartInfo.Customer = this.newChooseCustomer.EditValue as Model.Customer;
            //if (this._bomParmentPartInfo.Customer != null)
            //    this._bomParmentPartInfo.CustomerId = (this._bomParmentPartInfo.Customer).CustomerId;
            //this._bomParmentPartInfo.CustomerProductName = this.textEditCustomerProductName.Text;
            //if (string.IsNullOrEmpty(this._bomParmentPartInfo.CustomerId))
            //    this._bomParmentPartInfo.IsCustomerProcut = false;
            //else
            //    this._bomParmentPartInfo.IsCustomerProcut = true;


            // this._bomParmentPartInfo.MaterialType = this.comboBoxEditMaterialType.EditValue as Model.MaterialType;

            //this._bomParmentPartInfo.BomDescription = this.textEditBomDescription.Text;
            this._bomParmentPartInfo.Id = this.textEditId.Text; ;
            // this._bomParmentPartInfo.BomType = this.textEditBomType.Text;
            this._bomParmentPartInfo.BomVersion = this.textEditVersion.Text;
            this._bomParmentPartInfo.DefaultQuantity = Int32.Parse(this.spinEditDefaultQuantity.Value.ToString());
            //this._bomParmentPartInfo.EffectiveDate = this.dateEditEffectiveDate.DateTime;
            this._bomParmentPartInfo.CreateMan = BL.V.ActiveOperator.OperatorName;
            this._bomParmentPartInfo.ModifyMan = null;
            if (this.buttonEditTechonlogyHeaderid.EditValue != null && this.buttonEditTechonlogyHeaderid.Text != "")
                this._bomParmentPartInfo.TechonlogyHeaderId = (this.buttonEditTechonlogyHeaderid.EditValue as Model.TechonlogyHeader).TechonlogyHeaderId;
            else
                this._bomParmentPartInfo.TechonlogyHeaderId = null;
            double lossRate = 0;
            this._bomParmentPartInfo.LossRate = lossRate;
            if (this._bomParmentPartInfo.Product != null)
                this._bomParmentPartInfo.ProductId = this._bomParmentPartInfo.Product.ProductId;
            this._bomParmentPartInfo.Customer = this.newChooseContorlCustomer.EditValue as Model.Customer;
            this._bomParmentPartInfo.Status = this.radioState.SelectedIndex;
            switch (this.action)
            {
                case "insert":
                    this.bomParmentInfoManager.Insert(this._bomParmentPartInfo);

                    //  aa = null;
                    break;

                case "update":
                    this.bomParmentInfoManager.Update(this._bomParmentPartInfo);
                    //  aa = null;
                    break;
            }
            // flag = 1;

            if (this.action == "insert" && this.treeList1.Nodes.Count == 0)
            {
                this.searchBom = this._bomParmentPartInfo;
            }
            this.treeLoad(this.searchBom);

            //if (treeList1.Selection != null && treeList1.Selection.Count==0)
            //{
            //        string str = treeList1.Selection[0].Tag.ToString();
            //        if (str.IndexOf('-') > 0) //成品  依据bomid查询
            //            this._bomParmentPartInfo = this.bomParmentInfoManager.Get(str.Substring(str.IndexOf('-') + 1));
            //        else
            //        {                       
            //            this._bomParmentPartInfo = this.bomParmentInfoManager.SelectByProductId(str);
            //        }                                                     
            // }



        }

        #endregion

        #region 自定义列显示
        /// <summary>
        /// 自定义列显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView1.FocusedColumn == this.gridColumnUnit)
            {
                Model.BomComponentInfo comcomponentInfo = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.BomComponentInfo;
                if (comcomponentInfo == null)
                    return;

                Model.Product product = comcomponentInfo.Product;
                if (product == null)
                    return;

                IList<Model.ProductUnit> units = this.productUnitManager.Select(product.BasedUnitGroupId);

                this.repositoryItemComboBox1.Items.Clear();

                foreach (Model.ProductUnit unit in units)
                {
                    this.repositoryItemComboBox1.Items.Add(unit.CnName);
                }

            }

        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Model.BomComponentInfo components = this.gridView1.GetRow(e.RowHandle) as Model.BomComponentInfo;
            if (components.IndexOfBom == null)
            {
                components.IndexOfBom = GetMaxIndexOfBom() + 1;
            }
            if (components == null) return;

            if (e.Column == this.gridColumnId)
            {
                string id = e.Value.ToString();

                Model.Product p = productManager.Get(id);
                components.BasicUseQuantity = int.Parse(this.spinEditDefaultQuantity.Text);
                components.Bom = this._bomParmentPartInfo;
                //components.Depot = p.Depot;
                //components.DepotId = p.DepotId;
                //components.DepotPosition = p.DepotPosition;
                //components.DepotPositionId = p.DepotPositionId;
                components.EffectsDate = DateTime.Now;
                components.ExpiringDate = DateTime.Now.AddYears(1000);
                components.offset = 0;
                components.Product = p;
                components.ProductId = p.ProductId;
                components.Remarks = p.ProductDescription;
                components.UseQuantity = 1;
                components.Unit = p.ProduceUnit == null ? "" : p.ProduceUnit.CnName;
                this.bindingSourceBomComponentInfo.Position = this.bindingSourceBomComponentInfo.IndexOf(components);
                this.gridControl1.RefreshDataSource();
            }
        }
        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {
                if (this.CanAdd(this._bomParmentPartInfo.Components))
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        Model.BomComponentInfo detail = new Model.BomComponentInfo();

                        detail.Bom = this._bomParmentPartInfo;
                        detail.EffectsDate = DateTime.Now;
                        detail.ExpiringDate = DateTime.Now.AddYears(1000);
                        detail.offset = 0;
                        detail.PriamryKeyId = Guid.NewGuid().ToString();
                        detail.SubLoseRate = 0.0;

                        int defaultquntity = 1;
                        int.TryParse(this.spinEditDefaultQuantity.Text, out defaultquntity);
                        detail.UseQuantity = defaultquntity;
                        detail.Remarks = "";

                        this._bomParmentPartInfo.Components.Add(detail);
                    }
                }
                if (e.KeyData == Keys.Delete)
                {
                    //this.simpleButtonRemove.PerformClick();
                }
                this.gridControl1.RefreshDataSource();
            }

        }
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            Model.Product p = null;

            IList<Model.BomComponentInfo> invoiceJcDetails = this.bindingSourceBomComponentInfo.DataSource as IList<Model.BomComponentInfo>;

            if (invoiceJcDetails == null || invoiceJcDetails.Count <= 0)
                return;
            p = invoiceJcDetails[e.ListSourceRowIndex].Product;
            if (p == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnGuiGe":
                    e.DisplayText = p.ProductSpecification;
                    break;

            }
        }
        #endregion
        private int GetMaxIndexOfBom()
        {
            int num = 0;
            foreach (Model.BomComponentInfo bomc in this._bomParmentPartInfo.Components)
            {
                int temp = 0;
                if (bomc.IndexOfBom != null)
                    temp = (int)(bomc.IndexOfBom);
                if (temp > num)
                    num = temp;
            }
            return num;
        }

        private bool CanAdd(IList<Model.BomComponentInfo> list)
        {
            foreach (Model.BomComponentInfo detail in list)
            {
                if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
                    return false;
            }
            return true;
        }

        private void BomEdit_Load(object sender, EventArgs e)
        {
            //  this.MdiParent=
            // this.Parent = new MainForm();

            // this.treeLoad();

            this.bindingSourceProducts.DataSource = this.productManager.SelectNotCustomer1();
            //this.bindingSourceDepot.DataSource = this.depotManager.Select();
            // this.bindingSourceProcessCategory.DataSource = this.processCategoryManager.Select();

            this.simpleButtonProciss.Visible = false;
            this.simpleButton2Remove.Visible = false;
            this.simpleButton2Add.Visible = false;


        }

        private void simpleButtonRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            if (this.bindingSourceBomComponentInfo.Current != null)
            {
                this._bomParmentPartInfo.Components.Remove(this.bindingSourceBomComponentInfo.Current as Book.Model.BomComponentInfo);

                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButtonAppend_Click(object sender, EventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.BomComponentInfo components = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        components = new Book.Model.BomComponentInfo();
                        components.BasicUseQuantity = int.Parse(this.spinEditDefaultQuantity.Text);
                        components.Bom = this._bomParmentPartInfo;
                        components.EffectsDate = DateTime.Now;
                        components.ExpiringDate = DateTime.Now.AddYears(1000);
                        components.offset = 0;
                        components.Product = product;
                        components.Remarks = product.ProductDescription;
                        components.ProductId = product.ProductId;
                        components.UseQuantity = 1;
                        components.Unit = product.ProduceUnit == null ? "" : product.ProduceUnit.CnName;
                        components.IndexOfBom = GetMaxIndexOfBom() + 1;
                        this._bomParmentPartInfo.Components.Add(components);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    Model.Product p = f.SelectedItem as Model.Product;
                    components = new Book.Model.BomComponentInfo();
                    components.BasicUseQuantity = int.Parse(this.spinEditDefaultQuantity.Text);
                    components.Bom = this._bomParmentPartInfo;
                    components.EffectsDate = DateTime.Now;
                    components.ExpiringDate = DateTime.Now.AddYears(1000);
                    components.offset = 0;
                    components.Product = p;
                    components.Remarks = p.ProductDescription;
                    components.ProductId = p.ProductId;
                    components.UseQuantity = 1;
                    components.Unit = p.ProduceUnit == null ? "" : p.ProduceUnit.CnName;
                    components.IndexOfBom = GetMaxIndexOfBom() + 1;
                    this._bomParmentPartInfo.Components.Add(components);

                }
                this.gridControl1.RefreshDataSource();
                this.bindingSourceBomComponentInfo.Position = this.bindingSourceBomComponentInfo.IndexOf(components);

            }
        }

        private void newChooseContorlProductId_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ChooseProductForm f = new ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.Product product = f.SelectedItem as Model.Product;
                if (product != null)
                {
                    this.textEditParentsModel.Text = product.ProductSpecification;
                    this.textEditParentsName.Text = product.IsCustomerProduct == true ? product.ProductName + "{" + product.CustomerProductName + "}" : product.ProductName;
                    ;
                    this.newChooseContorlProductId.EditValue = product.Id;
                    this.newChooseContorlCustomer.EditValue = product.Customer;

                    if (product.OutSourcing != null && product.OutSourcing.Value)
                        this.textEditSourceType.Text = Properties.Resources.IsOutSourcing;
                    if (product.Consume != null && product.Consume.Value)
                        this.textEditSourceType.Text = "耗用";
                    if (product.TrustOut != null && product.TrustOut.Value)
                        this.textEditSourceType.Text = "委外";
                    if (product.HomeMade != null && product.HomeMade.Value)
                        this.textEditSourceType.Text = Properties.Resources.IsHomeMade;

                    //修改加工工序表客户工艺
                    if (!string.IsNullOrEmpty(product.CustomerProductName))
                        this._customer = product.Customer;
                    else
                        this._customer = null;
                    //  this._bomParmentPartInfo.Components = this.BomComManager.Select(this.bomParmentInfoManager.Get(product));
                    this._bomParmentPartInfo.Product = product;
                    this.textEditVersion.Text = product.ProductVersion;
                    ////this.bindingSourceBomComponentInfo.DataSource = this._bomParmentPartInfo.Components;
                    ////this.gridControl1.RefreshDataSource();

                    this.nccSupplier.EditValue = product.Supplier;
                }

            }
            f.Dispose();
            System.GC.Collect();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item.Tag != null)
            {
                if (e.Item.Tag.ToString() == "copy")
                {
                    Model.BomParentPartInfo bom = this._bomParmentPartInfo;
                    this.action = "insert";
                    if (this.searchBom != null && this._bomParmentPartInfo.BomId == this.searchBom.BomId)
                        this.treeList1.ClearNodes();
                    bom.BomId = Guid.NewGuid().ToString();
                    this.Refresh();



                }
            }
        }

        //包材选择商品
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (this._bomParmentPartInfo.BomPackageDetails.Count > 0 && string.IsNullOrEmpty(this._bomParmentPartInfo.BomPackageDetails[0].ProductId))
                    this._bomParmentPartInfo.BomPackageDetails.RemoveAt(0);
                Model.BomPackageDetails detail = null;
                if (ChooseProductForm.ProductList != null || ChooseProductForm.ProductList.Count > 0)
                {
                    foreach (Model.Product product in ChooseProductForm.ProductList)
                    {
                        detail = new Book.Model.BomPackageDetails();
                        detail.Product = product;
                        detail.ProductId = product.ProductId;
                        detail.Quantity = 0;
                        detail.Inumber = this._bomParmentPartInfo.BomPackageDetails.Count + 1;
                        detail.ConsumeRate = 0;
                        detail.EffectsDate = DateTime.Now;
                        detail.ExpiringDate = DateTime.Now.AddYears(1000);
                        detail.Description = product.ProductDescription;
                        this._bomParmentPartInfo.BomPackageDetails.Add(detail);
                    }
                }
                if (ChooseProductForm.ProductList == null || ChooseProductForm.ProductList.Count == 0)
                {
                    Model.Product p = f.SelectedItem as Model.Product;
                    detail = new Book.Model.BomPackageDetails();
                    detail.Product = f.SelectedItem as Model.Product;
                    detail.ProductId = (f.SelectedItem as Model.Product).ProductId;
                    detail.Quantity = 0;
                    detail.ConsumeRate = 0;
                    detail.EffectsDate = DateTime.Now;
                    detail.ExpiringDate = DateTime.Now.AddYears(1000);
                    detail.Description = p.ProductDescription;
                    this._bomParmentPartInfo.BomPackageDetails.Add(detail);
                }

                this.bindingSourceBomPackageDetails.Position = this.bindingSourceBomPackageDetails.IndexOf(detail);
                //this.bindingSourceBomPackageDetails.DataSource = this._bomParmentPartInfo.BomPackageDetails;
                this.gridControl2.RefreshDataSource();
            }
        }

        //包材删除商品
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceBomPackageDetails.Current != null)
            {
                this._bomParmentPartInfo.BomPackageDetails.Remove(this.bindingSourceBomPackageDetails.Current as Book.Model.BomPackageDetails);

                this.gridControl2.RefreshDataSource();
            }
        }
        // bool isck = false;

        private void gridView3_ShowingEditor(object sender, CancelEventArgs e)
        {

            //object value = this.gridView3.GetRowCellValue(this.gridView3.FocusedRowHandle, this.gridColumn1);

            //if (this.gridView3.FocusedColumn == this.gridColumnProcess)
            //{

            //    this.repositoryItemComboBox2.Items.Clear();
            //    this.gridView3.SetRowCellValue(this.gridView3.FocusedRowHandle, this.gridColumnProcess, null);


            //    Model.BOMProductProcess BOMProductProcess = this.gridView3.GetRow(this.gridView3.FocusedRowHandle) as Model.BOMProductProcess;
            //    if (BOMProductProcess == null) return;
            //    if (value != null)
            //        BOMProductProcess.ProcessCategory = this.processCategoryManager.Get(value.ToString());
            //    if (BOMProductProcess.ProcessCategory == null) return;
            //    IList<Model.Processing> processings = this.processingManager.Select(BOMProductProcess.ProcessCategory, this.newChooseCustomer.EditValue as Model.Customer);
            //    this.repositoryItemComboBox2.Items.Clear();
            //    foreach (Model.Processing processing in processings)
            //    {
            //        this.repositoryItemComboBox2.Items.Add(processing);
            //    }
            //    this._bomParmentPartInfo.BOMProductProcess = this._bomParmentPartInfo.BOMProductProcess;

            //}
        }

        private void simpleButtonProciss_Click(object sender, EventArgs e)
        {
            //Settings.BasicData.Customs.Processing.EditForm f = new Book.UI.Settings.BasicData.Customs.Processing.EditForm(this._bomParmentPartInfo.Customer);
            //if (f.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._bomParmentPartInfo.Product == null) return;
            BomXR f = new BomXR(this._bomParmentPartInfo);
            f.ShowPreview();


        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._bomParmentPartInfo == null) return;
            BomProcessXR f = new BomProcessXR(this._bomParmentPartInfo);
            f.ShowPreview();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this._bomParmentPartInfo == null || this._bomParmentPartInfo.CustomerPackage == null) return;
            //BOMPackageXR f = new BOMPackageXR(this._bomParmentPartInfo);
            //f.ShowPreview();
        }

        //private void simpleButton3_Click(object sender, EventArgs e)
        //{
        //    BOMManagerForm f = new BOMManagerForm();
        //    f.Show();
        //}

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (this.action == "insert" || this.action == "update")
            //    {

            //        if (e.KeyData == Keys.Enter)
            //        {
            //            Model.BOMProductProcess detail = new Book.Model.BOMProductProcess();
            //            this._bomParmentPartInfo.BOMProductProcess.Add(detail);
            //        }
            //        if (e.KeyData == Keys.Delete)
            //        {
            //            this.simpleButton2Remove.PerformClick();
            //        }
            //        this.gridControl3.RefreshDataSource();
            //    }
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (this._bomParmentPartInfo != null && this._bomParmentPartInfo.BomPackageDetails != null)

            //    this._bomParmentPartInfo.BomPackageDetails.Clear();
            //BasicData.Customs.CustomerPackage.ChooseCustomerPackageForm f = new Book.UI.Settings.BasicData.Customs.CustomerPackage.ChooseCustomerPackageForm();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    Model.CustomerPackage customerPackage = f.SelectedItem as Model.CustomerPackage;
            //    if (customerPackage != null)
            //    {
            //        // this.buttonEditCustomerPackage.EditValue = customerPackage;



            //        foreach (Model.CustomerPackageDetail detail in this.customerPackageDetailManager.GetByPackageId(customerPackage.CustomerPackageId))
            //        {
            //            Model.BomPackageDetails pack = new Book.Model.BomPackageDetails();
            //            pack.BomId = this._bomParmentPartInfo.BomId;
            //            pack.BomPackageDetailsId = Guid.NewGuid().ToString();
            //            pack.ConsumeRate = detail.ConsumeRate;
            //            pack.Description = detail.Description;
            //            pack.EffectsDate = detail.EffectsDate;
            //            pack.ExpiringDate = detail.ExpiringDate;
            //            pack.PackageUnit = detail.PackageUnit;
            //            pack.Product = detail.Product;
            //            pack.ProductId = detail.ProductId;
            //            pack.Quantity = detail.Quantity;
            //            pack.UseQuantity = detail.UseQuantity;

            //            this._bomParmentPartInfo.BomPackageDetails.Add(pack);

            //        }


            //        this.gridControl2.RefreshDataSource();

            //        //   this.bindingSourceBomPackageDetails.DataSource = this.customerPackageDetailManager.GetByPackageId(customerPackage.CustomerPackageId);

            //        //   this._bomParmentPartInfo.BomPackageDetails=
            //    }
            //}
        }

        private void barButtonItemProcess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //    if(e.Item.Tag.ToString()=="process")

        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            //  this.gridView1.hitt
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Model.BomComponentInfo com = this.bindingSourceBomComponentInfo.Current as Model.BomComponentInfo;

            //if (new BL.ProductProcessManager().Select(com.Product.ProductId).Count != 0)
            //{
            //    BasicData.Products.ProcessProductForm f = new Book.UI.Settings.BasicData.Products.ProcessProductForm(com);
            //    // if (f.IsDisposed)

            //    // f.Close();
            //    f.Show();
            //}
        }

        private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0)
                return;
            Model.Product p = null;
            IList<Model.BomPackageDetails> BomPackageDetails = this.bindingSourceBomPackageDetails.DataSource as IList<Model.BomPackageDetails>;
            if (BomPackageDetails == null || BomPackageDetails.Count <= 0)
                return;
            p = BomPackageDetails[e.ListSourceRowIndex].Product;
            if (p == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnPackage":
                    e.DisplayText = p.Id;
                    break;

            }

        }

        private void simpleButton2Remove_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceBOMProcess.Current != null)
            {
                this._bomParmentPartInfo.BOMProductProcess.Remove(this.bindingSourceBOMProcess.Current as Book.Model.BOMProductProcess);
                this.gridControl3.RefreshDataSource();
            }
        }


        Model.BomComponentInfo _bomcom = new Book.Model.BomComponentInfo();
        Model.BomParentPartInfo _bomparent = new Book.Model.BomParentPartInfo();
        Model.BomParentPartInfo _bomparents = new Book.Model.BomParentPartInfo();
        //   IList<Model.BomComponentInfo> _bomcomDetail = new List<Model.BomComponentInfo>();
        IList<Model.BomComponentInfo> _comDetail = new List<Model.BomComponentInfo>();
        IList<Model.BomComponentInfo> _comDetails = new List<Model.BomComponentInfo>();
        IList<Model.BomComponentInfo> _comDetailss = new List<Model.BomComponentInfo>();

        Model.Product _product = new Model.Product();
        Model.ProductCategory _productCategory = new Model.ProductCategory();


        //private void treeLoad1()
        //{
        //    TreeListNode parentNode = null;  
        //    treeList1.Nodes.Clear();
        //    foreach (Model.BomParentPartInfo _bomparents in this.bomParmentInfoManager.SelectNotContent())
        //    {
        //        parentNode = treeList1.AppendNode(new object[] { _bomparents.Product.ProductName }, null, _bomparents.ProductId);
        //        GetAllChildNodes(_bomparents, parentNode);
        //    }
        //}

        //private void GetAllChildNodes(Model.BomParentPartInfo pBom, TreeListNode pNode)
        //{

        //    IList<Model.BomComponentInfo> bomcomList = null;
        //    bomcomList = this.BomComManager.Select(pBom);

        //    foreach (Model.BomComponentInfo comm in bomcomList)
        //    {
        //        if (comm.Product == null && !string.IsNullOrEmpty(comm.ProductId))
        //            comm.Product = this.productManager.Get(comm.ProductId);
        //        TreeListNode childeNode = this.treeList1.AppendNode(new object[] { comm.Product.ProductName }, pNode, comm.ProductId);
        //        Model.BomParentPartInfo pChildBom = this.bomParmentInfoManager.Get(comm.Product);
        //        if (pChildBom == null) continue;
        //        GetAllChildNodes(pChildBom, childeNode);

        //    }
        //}

        private void treeLoad(Model.BomParentPartInfo bom)
        {
            //IList<Model.BomParentPartInfo> list = this.bomParmentInfoManager.SelectNotContent();
            //for (int i = 0; i < list.Count; i++)
            //{
            //    treeList1.AppendNode(new object[] { !string.IsNullOrEmpty(list[i].Product.CustomerProductName) ? list[i].Product.ProductName + "{" + list[i].Product.CustomerProductName + "}" : list[i].Product.ProductName }, null, list[i].ProductId);
            //}
            //foreach (Model.BomParentPartInfo _bomparents in list)
            //{
            //    treeList1.AppendNode(new object[] { !string.IsNullOrEmpty(_bomparents.Product.CustomerProductName) ? _bomparents.Product.ProductName + "{" + _bomparents.Product.CustomerProductName + "}" : _bomparents.Product.ProductName }, null, _bomparents.ProductId);
            //}

            #region 公司BOM
            _bomparents = bom;
            TreeListNode node2 = null;
            TreeListNode node3 = null;
            TreeListNode node4 = null;
            TreeListNode node5 = null;
            TreeListNode node6 = null;
            TreeListNode node7 = null;
            TreeListNode node8 = null;
            TreeListNode node9 = null;
            TreeListNode node10 = null;
            TreeListNode node11 = null;

            IList<Model.BomComponentInfo> bomcomList = null;
            IList<Model.BomComponentInfo> bomcomList1 = null;
            IList<Model.BomComponentInfo> bomcomList2 = null;
            IList<Model.BomComponentInfo> bomcomList3 = null;
            IList<Model.BomComponentInfo> bomcomList4 = null;
            IList<Model.BomComponentInfo> bomcomList5 = null;
            IList<Model.BomComponentInfo> bomcomList6 = null;
            IList<Model.BomComponentInfo> bomcomList7 = null;
            IList<Model.BomComponentInfo> bomcomList8 = null;
            IList<Model.BomComponentInfo> bomcomList9 = null;
            treeList1.Nodes.Clear();
            if (this._bomparents != null)
                this._comDetailss.Clear();
            //  int flag = 0;


            //0415foreach (Model.BomParentPartInfo _bomparents in this.bomParmentInfoManager.SelectNotContent())
            {
                ////    TreeListNode node0 = treeList1.AppendNode(new object[] { _bomparents.IsCustomerProcut == true ? _bomparents.CustomerProductName : _bomparents.Product.ProductName }, null, _bomparents.IsCustomerProcut == true ?
                ////      _bomparents.ProductId + "customer" + _bomparents.CustomerId : _bomparents.ProductId);

                // TreeListNode node0 = treeList1.AppendNode(new object[] { _bomparents.Product.ProductName }, null, _bomparents.ProductId);
                TreeListNode node0 = treeList1.AppendNode(new object[] { _bomparents.Product.IsCustomerProduct == true ? _bomparents.Product.ProductName + "{" + _bomparents.Product.CustomerProductName + "}" : _bomparents.Product.ProductName }, null, _bomparents.ProductId + (_bomparents == null ? "" : "+" + _bomparents.BomId));
                TreeListNode node1 = null;






                // if (_bomparents.IsCustomerProcut == true)
                //  {                                            
                this._comDetailss = this.BomComManager.Select(_bomparents);

                foreach (Model.BomComponentInfo comm in this._comDetailss)
                {
                    if (comm.Product == null && !string.IsNullOrEmpty(comm.ProductId))
                        comm.Product = this.productManager.Get(comm.ProductId);
                    node1 = this.treeList1.AppendNode(new object[] { comm.Product.ProductName }, node0, comm.ProductId);
                    //如果母件是客户BOM
                    ////if (_bomparents.IsCustomerProcut == true && _bomparents.Customer != null)
                    ////{

                    ////    this._bomparent = this.bomParmentInfoManager.Get(comm.Product, comm.Bom.Customer);
                    ////}
                    ////else
                    ////{
                    this._bomparent = this.bomParmentInfoManager.Get(comm.Product);
                    //// }

                    //是否加工过
                    ////if ( comm.Product.IsProcee == true)         
                    ////{ 
                    ////    if(this.bomParmentInfoManager.Get(comm.Product)==null)

                    ////     // comm.Product=productManager.Get(comm.Product.ProceebeforeProductId);                                          //{
                    ////         this._bomparent = this.bomParmentInfoManager.Get(productManager.Get(comm.Product.ProceebeforeProductId));
                    ////   //  }
                    ////    else
                    ////        this._bomparent = this.bomParmentInfoManager.Get(comm.Product);

                    ////}
                    if (this._bomparent == null) continue;

                    bomcomList = this.BomComManager.Select(this._bomparent);

                    foreach (Model.BomComponentInfo com in bomcomList)
                    {

                        node2 = this.treeList1.AppendNode(new object[] { com.Product.ProductName }, node1, com.ProductId);
                        //4ceng

                        this._bomparent = this.bomParmentInfoManager.Get(com.Product);

                        if (this._bomparent == null) continue;
                        bomcomList1 = this.BomComManager.Select(this._bomparent);
                        foreach (Model.BomComponentInfo co in bomcomList1)
                        {
                            node3 = this.treeList1.AppendNode(new object[] { co.Product.ProductName }, node2, co.ProductId);

                            //5ceng
                            this._bomparent = this.bomParmentInfoManager.Get(co.Product);

                            if (this._bomparent == null) continue;

                            bomcomList2 = this.BomComManager.Select(this._bomparent);
                            foreach (Model.BomComponentInfo bomcom in bomcomList2)
                            {

                                node4 = this.treeList1.AppendNode(new object[] { bomcom.Product.ProductName }, node3, bomcom.ProductId);

                                //6层

                                this._bomparent = this.bomParmentInfoManager.Get(bomcom.Product);
                                if (this._bomparent == null) continue;
                                bomcomList3 = this.BomComManager.Select(this._bomparent);
                                foreach (Model.BomComponentInfo bom3 in bomcomList3)
                                {

                                    node5 = this.treeList1.AppendNode(new object[] { bom3.Product.ProductName }, node4, bom3.ProductId);

                                    // 7层

                                    this._bomparent = this.bomParmentInfoManager.Get(bom3.Product);

                                    if (this._bomparent == null) continue;

                                    bomcomList4 = this.BomComManager.Select(this._bomparent);
                                    foreach (Model.BomComponentInfo bom4 in bomcomList4)
                                    {

                                        node6 = this.treeList1.AppendNode(new object[] { bom4.Product.ProductName }, node5, bom4.ProductId);

                                        //8层

                                        this._bomparent = this.bomParmentInfoManager.Get(bom4.Product);

                                        if (this._bomparent == null) continue;

                                        bomcomList5 = this.BomComManager.Select(this._bomparent);
                                        foreach (Model.BomComponentInfo bom5 in bomcomList5)
                                        {

                                            node7 = this.treeList1.AppendNode(new object[] { bom5.Product.ProductName }, node6, bom5.ProductId);

                                            //9层
                                            this._bomparent = this.bomParmentInfoManager.Get(bom5.Product);

                                            if (this._bomparent == null) continue;

                                            bomcomList6 = this.BomComManager.Select(this._bomparent);
                                            foreach (Model.BomComponentInfo bom6 in bomcomList6)
                                            {

                                                node8 = this.treeList1.AppendNode(new object[] { bom6.Product.ProductName }, node7, bom6.ProductId);

                                                //10层
                                                this._bomparent = this.bomParmentInfoManager.Get(bom6.Product);

                                                if (this._bomparent == null) continue;

                                                bomcomList7 = this.BomComManager.Select(this._bomparent);
                                                foreach (Model.BomComponentInfo bom7 in bomcomList7)
                                                {

                                                    node9 = this.treeList1.AppendNode(new object[] { bom7.Product.ProductName }, node8, bom7.ProductId);

                                                    //11层
                                                    this._bomparent = this.bomParmentInfoManager.Get(bom7.Product);

                                                    if (this._bomparent == null) continue;

                                                    bomcomList8 = this.BomComManager.Select(this._bomparent);
                                                    foreach (Model.BomComponentInfo bom8 in bomcomList8)
                                                    {

                                                        node10 = this.treeList1.AppendNode(new object[] { bom8.Product.ProductName }, node9, bom8.ProductId);

                                                        //12层
                                                        this._bomparent = this.bomParmentInfoManager.Get(bom8.Product);

                                                        if (this._bomparent == null) continue;

                                                        bomcomList9 = this.BomComManager.Select(this._bomparent);
                                                        foreach (Model.BomComponentInfo bom9 in bomcomList9)
                                                        {

                                                            node11 = this.treeList1.AppendNode(new object[] { bom9.Product.ProductName }, node10, bom9.ProductId);

                                                            //12层



                                                        }


                                                    }

                                                }

                                            }

                                        }

                                    }




                                }
                            }
                            //

                        }

                    }

                    //    }
                }
            }
            #endregion

        }

        #region 客户BOM
        //private void treeLoad1()
        //{
        //    TreeListNode node2 = null;
        //    TreeListNode node3 = null;
        //    TreeListNode node4 = null;
        //    IList<Model.BomComponentInfo> bomcomList = null;
        //    IList<Model.BomComponentInfo> bomcomList1 = null;
        //    IList<Model.BomComponentInfo> bomcomList2 = null;
        //    treeList2.Nodes.Clear();
        //    if (this._bomparents != null)
        //        this._comDetailss.Clear();

        //    foreach (Model.Customer customer in this.customerManager.Select())
        //    {
        //        TreeListNode node = treeList2.AppendNode(new object[] { customer.CustomerShortName }, null, customer.CustomerId);

        //        foreach (Model.BomParentPartInfo _bomparents in this.bomParmentInfoManager.SelectNotContentByCustomer(customer))
        //        {
        //            TreeListNode node0 = treeList2.AppendNode(new object[] { _bomparents.CustomerProductName }, node,
        //          _bomparents.ProductId + "customer" + _bomparents.CustomerId);
        //            TreeListNode node1 = null;

        //            // if (_bomparents.IsCustomerProcut == true)
        //            //  {                                            
        //            this._comDetailss = this.BomComManager.Select(_bomparents);

        //            foreach (Model.BomComponentInfo comm in this._comDetailss)
        //            {
        //                if (comm.Product == null && !string.IsNullOrEmpty(comm.ProductId))
        //                    comm.Product = this.productManager.Get(comm.ProductId);
        //                node1 = this.treeList2.AppendNode(new object[] { comm.Product.ProductName }, node0, comm.ProductId);
        //                //如果母件是客户BOM
        //                //if (_bomparents.IsCustomerProcut == true && _bomparents.Customer != null)
        //                //{

        //                this._bomparent = this.bomParmentInfoManager.Get(comm.Product, null);//// comm.Bom.Customer);
        //                //}
        //                //else
        //                //{
        //                //    this._bomparent = this.bomParmentInfoManager.Get(comm.Product);
        //                //}

        //                //是否加工过
        //                //if (comm.Product.IsProcee == true)
        //                //{
        //                //    if (this.bomParmentInfoManager.Get(comm.Product) == null)

        //                //        // comm.Product=productManager.Get(comm.Product.ProceebeforeProductId);

        //                //        //{
        //                //        this._bomparent = this.bomParmentInfoManager.Get(productManager.Get(comm.Product.ProceebeforeProductId));
        //                //    //  }
        //                //    else
        //                //        this._bomparent = this.bomParmentInfoManager.Get(comm.Product);

        //                //}
        //                if (this._bomparent == null) continue;

        //                bomcomList = this.BomComManager.Select(this._bomparent);

        //                foreach (Model.BomComponentInfo com in bomcomList)
        //                {

        //                    node2 = this.treeList2.AppendNode(new object[] { com.Product.ProductName }, node1, com.ProductId);
        //                    //4ceng


        //                    //如果母件是客户BOM
        //                    if (com.Bom.IsCustomerProcut == true && com.Bom.Customer != null)
        //                    {
        //                        this._bomparent = this.bomParmentInfoManager.Get(com.Product, com.Bom.Customer) == null ? this.bomParmentInfoManager.Get(com.Product) : this.bomParmentInfoManager.Get(com.Product, com.Bom.Customer);
        //                    }
        //                    else
        //                    {
        //                        this._bomparent = this.bomParmentInfoManager.Get(com.Product);
        //                    }



        //                    //是否加工过
        //                    //if (com.Product.IsProcee == true)
        //                    //{
        //                    //    if (this.bomParmentInfoManager.Get(com.Product) == null)

        //                    //        // comm.Product=productManager.Get(comm.Product.ProceebeforeProductId);

        //                    //        //{
        //                    //        this._bomparent = this.bomParmentInfoManager.Get(productManager.Get(com.Product.ProceebeforeProductId));
        //                    //    //  }
        //                    //    else
        //                    //        this._bomparent = this.bomParmentInfoManager.Get(com.Product);

        //                    //}
        //                    if (this._bomparent == null) continue;


        //                    bomcomList1 = this.BomComManager.Select(this._bomparent);
        //                    foreach (Model.BomComponentInfo co in bomcomList1)
        //                    {

        //                        node3 = this.treeList2.AppendNode(new object[] { co.Product.ProductName }, node2, co.ProductId);

        //                        //5ceng

        //                        //如果母件是客户BOM
        //                        if (co.Bom.IsCustomerProcut == true && co.Bom.Customer != null)
        //                        {
        //                            this._bomparent = this.bomParmentInfoManager.Get(co.Product, co.Bom.Customer) == null ? this.bomParmentInfoManager.Get(co.Product) : this.bomParmentInfoManager.Get(co.Product, co.Bom.Customer);
        //                        }
        //                        else
        //                        {
        //                            this._bomparent = this.bomParmentInfoManager.Get(co.Product);
        //                        }

        //                        //是否加工过
        //                        //if (co.Product.IsProcee == true)
        //                        //{
        //                        //    if (this.bomParmentInfoManager.Get(co.Product) == null)

        //                        //        // comm.Product=productManager.Get(comm.Product.ProceebeforeProductId);

        //                        //        //{
        //                        //        this._bomparent = this.bomParmentInfoManager.Get(productManager.Get(co.Product.ProceebeforeProductId));
        //                        //    //  }
        //                        //    else
        //                        //        this._bomparent = this.bomParmentInfoManager.Get(co.Product);

        //                        //}

        //                        if (this._bomparent == null) continue;

        //                        bomcomList2 = this.BomComManager.Select(this._bomparent);
        //                        foreach (Model.BomComponentInfo bomcom in bomcomList2)
        //                        {

        //                            node4 = this.treeList2.AppendNode(new object[] { bomcom.Product.ProductName }, node3, bomcom.ProductId);

        //                        }
        //                        //

        //                    }

        //                }
        //            }

        //            //    }
        //        }

        //    }
        //}

        #endregion

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            //if (flag == 0)
            //{

            //    if (e.Node != null)
            //    {

            //        string productid = string.Empty;
            //        productid = e.Node.Tag.ToString();                 
            //        this._bomParmentPartInfo = this.bomParmentInfoManager.Get(this.productManager.Get(e.Node.Tag.ToString()));
            //        //如果母件是客户BOM 
            //        this.action = "view";
            //        if (this._bomParmentPartInfo == null)
            //        {
            //            this._bomParmentPartInfo = new Book.Model.BomParentPartInfo();
            //            this._bomParmentPartInfo.BomId = Guid.NewGuid().ToString();
            //            this._bomParmentPartInfo.Product = productManager.Get(e.Node.Tag.ToString());                      
            //            this._bomParmentPartInfo.ProductId = this._bomParmentPartInfo.Product.ProductId;
            //            this._bomParmentPartInfo.InsertTime = DateTime.Now;
            //            this._bomParmentPartInfo.CreateMan = BL.V.ActiveOperator.OperatorName;
            //            this._bomParmentPartInfo.EffectiveDate = DateTime.Now;
            //            this._bomParmentPartInfo.Id = this.bomParmentInfoManager.GetId();
            //            this.action = "insert";
            //        }

            //        this.Refresh();
            //    }
            //}

        }

        private void simpleButton1Add_Click(object sender, EventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {

                Model.BomComponentInfo detail = new Model.BomComponentInfo();

                detail.Bom = this._bomParmentPartInfo;
                detail.EffectsDate = DateTime.Now;
                detail.ExpiringDate = DateTime.Now.AddYears(1000);
                detail.offset = 0;
                detail.PriamryKeyId = Guid.NewGuid().ToString();
                detail.SubLoseRate = 0.0;
                int defaultquntity = 1;
                int.TryParse(this.spinEditDefaultQuantity.Text, out defaultquntity);
                detail.UseQuantity = defaultquntity;
                detail.Remarks = "";

                this._bomParmentPartInfo.Components.Add(detail);

                this.gridControl1.RefreshDataSource();
            }

        }


        private void simpleButton2Add_Click(object sender, EventArgs e)
        {

            Model.BOMProductProcess detail = new Book.Model.BOMProductProcess();
            this._bomParmentPartInfo.BOMProductProcess.Add(detail);

            this.gridControl3.RefreshDataSource();
        }

        private IList<Model.BomComponentInfo> XRband()
        {
            if (this._bomParmentPartInfo != null)
                this._comDetailss.Clear();
            Model.BomComponentInfo comm = new Model.BomComponentInfo();
            comm.Jibie = 0;
            comm.UseQuantity = 1;
            comm.Product = _bomParmentPartInfo.Product;


            comm.Product.ProductName = string.IsNullOrEmpty(_bomParmentPartInfo.Product.CustomerProductName) ? _bomParmentPartInfo.Product.ProductName : _bomParmentPartInfo.Product.ProductName + "{" + _bomParmentPartInfo.Product.CustomerProductName + "}";
            comm.ProductId = _bomParmentPartInfo.ProductId;
            comm.Customer = _bomParmentPartInfo.Customer;

            _comDetailss.Add(comm);
            foreach (Model.BomComponentInfo bomcon in this.BomComManager.Select(_bomParmentPartInfo))
            {
                bomcon.Product.ProductName = "  " + (string.IsNullOrEmpty(bomcon.Product.CustomerProductName) ? bomcon.Product.ProductName : bomcon.Product.ProductName + "{" + bomcon.Product.CustomerProductName + "}");
                this._comDetailss.Add(bomcon);
            }
            if (this._comDetailss.Count != 0)
            {
                IList<Model.BomComponentInfo> a = null;
                string strlenth = "";


                for (int i = 1; i < this._comDetailss.Count; i++)
                {
                    //在物料中查询 是否 存在此子件
                    this._bomparent = this.bomParmentInfoManager.Get(_comDetailss[i].Product);


                    //if (this._bomparent == null)
                    //{
                    //    if (this._comDetailss[i].Product.IsProcee == true)
                    //    {

                    //        this._bomparent = this.bomParmentInfoManager.Get(this.productManager.Get(_comDetailss[i].Product.ProceebeforeProductId));

                    //    }
                    //}
                    if (this._bomparent != null)
                    {
                        a = this.BomComManager.Select(this._bomparent);
                        int m = this._comDetailss.Count;
                        for (int j = i + 1; j < m; j++)
                        {
                            _comDetails.Add(this._comDetailss[i + 1]);
                            this._comDetailss.RemoveAt(i + 1);
                        }
                        foreach (Model.BomComponentInfo bom in a)
                        {
                            bom.Jibie = _comDetailss[i].Jibie + 1;
                            //bom.UseQuantity = _comDetailss[i].UseQuantity * bom.UseQuantity;
                            for (int g = 0; g < bom.Jibie; g++)
                            {
                                strlenth += "   ";
                            }
                            if (bom.Product != null)
                            {
                                bom.Product.ProductName = strlenth + (string.IsNullOrEmpty(bom.Product.CustomerProductName) ? bom.Product.ProductName : bom.Product.ProductName + "{" + bom.Product.CustomerProductName + "}");

                            }
                            this._comDetailss.Add(bom);
                            strlenth = "";
                        }

                        foreach (Model.BomComponentInfo boms in _comDetails)
                        {
                            this._comDetailss.Add(boms);
                        }
                        _comDetails.Clear();
                        a.Clear();
                    }

                }
            }

            return _comDetailss;
        }

        private void treeList2_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            //string productid = string.Empty;
            //if (flag == 0)
            //{

            //    if (e.Node != null && e.Node.ParentNode != null)
            //    {
            //        productid = e.Node.Tag.ToString();
            //        if (e.Node.Tag.ToString().IndexOf("customer") >= 0)
            //        {
            //            this._bomParmentPartInfo = this.bomParmentInfoManager.Get(this.productManager.Get(productid.Substring(0, productid.IndexOf("customer"))), this.customerManager.Get(productid.Substring(productid.IndexOf("customer") + 8)));

            //        }
            //        else
            //        {
            //            this._bomParmentPartInfo = this.bomParmentInfoManager.Get(this.productManager.Get(e.Node.Tag.ToString()), null);

            //        }
            //        this.action = "view";
            //        if (this._bomParmentPartInfo == null)
            //        {
            //        this._bomParmentPartInfo = new Book.Model.BomParentPartInfo();
            //        this._bomParmentPartInfo.BomId = Guid.NewGuid().ToString();


            //        if (e.Node.Tag.ToString().IndexOf("customer") >= 0)
            //        {
            //            this._bomParmentPartInfo.Product = this.productManager.Get(productid.Substring(0, productid.IndexOf("customer")));

            //        }
            //        else
            //        {                            // this.action = "view";
            //            this._bomParmentPartInfo.Product = productManager.Get(e.Node.Tag.ToString());
            //        }
            //        // this._bomParmentPartInfo.Product = this.productManager.Get(e.Node.Tag.ToString());

            //        this._bomParmentPartInfo.ProductId = this._bomParmentPartInfo.Product == null ? null : this._bomParmentPartInfo.Product.ProductId;
            //        this._bomParmentPartInfo.InsertTime = DateTime.Now;
            //        this._bomParmentPartInfo.CreateMan = BL.V.ActiveOperator.OperatorName;
            //        this._bomParmentPartInfo.EffectiveDate = DateTime.Now;
            //        this._bomParmentPartInfo.Id = this.bomParmentInfoManager.GetId();

            //        if (this.productManager.Get(this._bomParmentPartInfo.Product.ProductId).IsProcee == true)
            //        {
            //            this._bomParmentPartInfo.Components = this.BomComManager.Select(this.bomParmentInfoManager.Get(this.productManager.Get(this.productManager.Get(e.Node.Tag.ToString()).ProceebeforeProductId)));

            //        }
            //        this.action = "insert";
            //    }

            //    }
            //    if (e.Node != null&& e.Node.ParentNode == null)
            //    {
            //        this.newChooseCustomer.EditValue = this.customerManager.Get(e.Node.Tag.ToString());

            //    }
            //    this.Refresh();
            //}

        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column == this.gridColumnUseQuantity)
            {
                Model.BomPackageDetails detail = this.gridView2.GetRow(e.RowHandle) as Model.BomPackageDetails;
                decimal defauleQuantity = 1;
                decimal value = decimal.Parse(e.Value.ToString());
                decimal.TryParse(this.gridView2.GetRowCellValue(e.RowHandle, this.gridColumnUseQuantity).ToString(), out defauleQuantity);

                if (value != 0)
                    this.gridView2.SetRowCellValue(e.RowHandle, this.gridColumnQuantity, this.spinEditDefaultQuantity.Value / defauleQuantity);
                else
                    this.gridView2.SetRowCellValue(e.RowHandle, this.gridColumnQuantity, 1);
                //   this.gridControl2.Refresh();
            }

        }

        private void gridView2_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (this.gridView2.FocusedColumn.Name == this.gridColumn8.Name)
            {
                if (this.gridView2.FocusedColumn.ColumnEdit is DevExpress.XtraEditors.Repository.RepositoryItemComboBox)
                {

                    if ((this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as Model.BomPackageDetails) == null) return;
                    Model.Product p = (this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as Model.BomPackageDetails).Product;

                    this.repositoryItemComboBox4.Items.Clear();

                    if (!string.IsNullOrEmpty(p.BasedUnitGroupId))
                    {
                        IList<Model.ProductUnit> units = productUnitManager.Select(p.BasedUnitGroupId);

                        foreach (Model.ProductUnit ut in units)
                        {
                            this.repositoryItemComboBox4.Items.Add(ut.CnName);
                        }
                    }
                }
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this._bomParmentPartInfo.Product == null)
                return;
            #region 需求變更
            //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //int rowIndex = 1;
            //int colIndex = 0;
            //excel.Application.Workbooks.Add(true);
            //System.Collections.Generic.IList<Model.BomComponentInfo> dost = XRband();
            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt.Columns.Add("級別", typeof(string));
            //dt.Columns.Add("子件編號", typeof(string));
            //dt.Columns.Add("子件名稱", typeof(string));
            //dt.Columns.Add("規格", typeof(string));
            //dt.Columns.Add("數量", typeof(string));
            //dt.Columns.Add("計算單位", typeof(string));
            //dt.Columns.Add("損耗率%", typeof(double));
            //dt.Columns.Add("偏置期", typeof(string));
            //dt.Columns.Add("生效日期", typeof(string));
            //dt.Columns.Add("失效日期", typeof(string));
            //if (dost != null)
            //{
            //    foreach (Model.BomComponentInfo bp in dost)
            //    {
            //        DataRow dr = dt.NewRow();
            //        dr["級別"] = bp.Jibie;
            //        dr["子件編號"] = bp.Product.Id;
            //        dr["子件名稱"] = bp.Product.ProductName;
            //        dr["規格"] = bp.Product.ProductSpecification;
            //        dr["數量"] = bp.UseQuantity;
            //        dr["計算單位"] = bp.Unit;
            //        dr["損耗率%"] = bp.SubLoseRate == null ? 0 : bp.SubLoseRate;
            //        dr["偏置期"] = bp.SubLoseRate;
            //        dr["生效日期"] = Convert.ToDateTime(bp.EffectsDate).ToString("yyyy-MM-dd");
            //        dr["失效日期"] = Convert.ToDateTime(bp.ExpiringDate).ToString("yyyy-MM-dd");
            //        dt.Rows.Add(dr);
            //    }
            //}
            //foreach (DataColumn col in dt.Columns)
            //{
            //    colIndex++;
            //    excel.Cells[1, colIndex] = col.ColumnName;
            //}
            //foreach (DataRow row in dt.Rows)
            //{
            //    rowIndex++;
            //    colIndex = 0;
            //    foreach (DataColumn col in dt.Columns)
            //    {
            //        colIndex++;
            //        excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
            //    }
            //}
            ////后台处理 
            //excel.Visible = true;
            #endregion

            DataSet ds = new DataSet();
            List<Model.Technologydetails> templist = this.bindingSourceBOMProcess.DataSource as List<Model.Technologydetails>;
            string name = this._bomParmentPartInfo.Product.ProductName;
            //子件
            ds.Tables.Add(CoverBomComponentInfo(XRband(), this.xtraTabPage1.Text));
            //加工
            ds.Tables.Add(CoverBOMProductProcess(templist, this.xtraTabPage2.Text));
            //包材
            ds.Tables.Add(CoverBomPackageDetails(this._bomParmentPartInfo.BomPackageDetails, this.xtraTabPage3.Text));
            //ExportToExcel(ds);
            ExportAllBomToExcel(ds, name);
            this._bomParmentPartInfo.Product.ProductName = name;
        }

        private System.Data.DataTable CoverBomPackageDetails(IList<Model.BomPackageDetails> infor, string tableName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("包裝材料編號", typeof(string));
            dt.Columns.Add("包裝材料名稱", typeof(string));
            dt.Columns.Add("包裝用料", typeof(string));
            dt.Columns.Add("數量", typeof(string));
            dt.Columns.Add("單位", typeof(string));
            dt.Columns.Add("損耗率", typeof(double));
            dt.Columns.Add("生效日期", typeof(string));
            dt.Columns.Add("失效日期", typeof(string));
            dt.Columns.Add("備註", typeof(string));
            if (infor != null && infor.Count != 0)
            {
                foreach (Model.BomPackageDetails bp in infor)
                {
                    DataRow dr = dt.NewRow();
                    dr["包裝材料編號"] = bp.Product.Id;
                    dr["包裝材料名稱"] = bp.Product.ToString();
                    dr["包裝用料"] = bp.UseQuantity;
                    dr["數量"] = bp.Quantity;
                    dr["單位"] = bp.PackageUnit;
                    dr["損耗率"] = bp.ConsumeRate.HasValue ? bp.ConsumeRate.Value : 0;
                    dr["生效日期"] = Convert.ToDateTime(bp.EffectsDate).ToString("yyyy-MM-dd");
                    dr["失效日期"] = Convert.ToDateTime(bp.ExpiringDate).ToString("yyyy-MM-dd");
                    RichTextBox rt = new RichTextBox();
                    rt.Rtf = bp.ProductDesc;
                    rt.SelectAll();
                    dr["備註"] = rt.SelectedText;
                    dt.Rows.Add(dr);
                }
            }
            if (!string.IsNullOrEmpty(tableName))
                dt.TableName = tableName + "" + System.DateTime.Now.ToString("HHmmss");
            return dt;
        }


        private System.Data.DataTable CoverBomComponentInfo(IList<Model.BomComponentInfo> infor, string tableName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("級別", typeof(string));
            dt.Columns.Add("子件名稱", typeof(string));
            dt.Columns.Add("子件規格", typeof(string));
            dt.Columns.Add("計算單位", typeof(string));
            dt.Columns.Add("使用數量", typeof(string));
            dt.Columns.Add("損耗率%", typeof(double));
            dt.Columns.Add("生效日期", typeof(string));
            dt.Columns.Add("失效日期", typeof(string));
            dt.Columns.Add("偏置期", typeof(string));
            dt.Columns.Add("備註", typeof(string));
            if (infor != null && infor.Count != 0)
            {
                foreach (Model.BomComponentInfo bp in infor)
                {
                    DataRow dr = dt.NewRow();
                    dr["級別"] = bp.Jibie;
                    dr["子件名稱"] = bp.Product.ProductName;
                    dr["子件規格"] = bp.Product.ProductSpecification;
                    dr["計算單位"] = bp.Unit;
                    dr["使用數量"] = bp.UseQuantity;
                    dr["損耗率%"] = bp.SubLoseRate == null ? 0 : bp.SubLoseRate;
                    dr["偏置期"] = bp.SubLoseRate;
                    dr["生效日期"] = Convert.ToDateTime(bp.EffectsDate).ToString("yyyy-MM-dd");
                    dr["失效日期"] = Convert.ToDateTime(bp.ExpiringDate).ToString("yyyy-MM-dd");
                    RichTextBox rt = new RichTextBox();
                    rt.Rtf = bp.ProductDesc;
                    rt.SelectAll();
                    dr["備註"] = rt.SelectedText;
                    dt.Rows.Add(dr);
                }
            }
            if (!string.IsNullOrEmpty(tableName))
                dt.TableName = tableName;
            return dt;
        }

        private System.Data.DataTable CoverBOMProductProcess(IList<Model.Technologydetails> infor, string tableName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("序號", typeof(string));
            dt.Columns.Add("序號編號", typeof(string));
            dt.Columns.Add("名稱", typeof(string));
            dt.Columns.Add("生產站", typeof(string));
            dt.Columns.Add("是否委外", typeof(string));
            dt.Columns.Add("委外廠商", typeof(string));
            if (infor != null && infor.Count != 0)
            {
                foreach (Model.Technologydetails bp in infor)
                {
                    DataRow dr = dt.NewRow();
                    dr["序號"] = bp.TechnologydetailsNo;
                    dr["序號編號"] = bp.Procedures.Id;
                    RichTextBox rt = new RichTextBox();
                    rt.Rtf = bp.Procedures.Procedurename;
                    rt.SelectAll();
                    dr["名稱"] = rt.SelectedText;
                    dr["生產站"] = bp.Procedures.WorkHouse.ToString();
                    if (bp.Procedures.IsOtherProduceOther.HasValue && bp.Procedures.IsOtherProduceOther.Value)
                        dr["是否委外"] = "是";
                    else
                        dr["是否委外"] = "否";
                    dr["委外廠商"] = bp.Procedures.Supplier == null ? null : bp.Procedures.Supplier.ToString();
                    dt.Rows.Add(dr);
                }
            }
            if (!string.IsNullOrEmpty(tableName))
                dt.TableName = tableName;
            return dt;
        }

        public static void ExportToExcel(DataSet ds)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Worksheet sheet = null;

            int i = 1;
            excel.Application.Workbooks.Add(true);
            sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.Worksheets.get_Item(1);

            foreach (System.Data.DataTable table in ds.Tables)
            {
                int rowIndex = 1;
                int colIndex = 0;
                try
                {
                    if (i > 1)
                    {
                        excel.Worksheets.Add(System.Reflection.Missing.Value, sheet, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                        sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.Worksheets.get_Item(i);
                    }
                    sheet.Name = table.TableName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                foreach (DataColumn col in table.Columns)
                {
                    colIndex++;
                    excel.Cells[1, colIndex] = col.ColumnName;
                }

                foreach (DataRow row in table.Rows)
                {
                    rowIndex++;
                    colIndex = 0;
                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;
                        excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                    }
                }
                i++;
            }
            excel.Visible = true;

            GC.Collect();
        }

        //导出Excel
        public static void ExportAllBomToExcel(DataSet ds, string sheetName)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Worksheet sheet = null;

            //int i = 1;
            excel.Application.Workbooks.Add(true);
            sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.Worksheets.get_Item(1);
            try
            {
                sheet.Name = sheetName;
            }
            catch { }
            finally
            {
                int rowIndex = 1;
                int colIndex = 0;
                foreach (System.Data.DataTable table in ds.Tables)
                {

                    //try
                    //{
                    //    if (i > 1)
                    //    {
                    //        excel.Worksheets.Add(System.Reflection.Missing.Value, sheet, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                    //        sheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Application.Worksheets.get_Item(i);
                    //    }
                    //    sheet.Name = table.TableName ;
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(ex.Message);
                    //    return;
                    //}

                    foreach (DataColumn col in table.Columns)
                    {
                        colIndex++;
                        excel.Cells[rowIndex, colIndex] = col.ColumnName;
                    }

                    foreach (DataRow row in table.Rows)
                    {
                        rowIndex++;
                        colIndex = 0;
                        foreach (DataColumn col in table.Columns)
                        {
                            colIndex++;
                            excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
                        }
                    }
                    rowIndex += 3;
                    colIndex = 0;
                    //i++;
                }
                excel.Visible = true;
            }

            GC.Collect();
        }

        //public static System.Data.DataTable ConvertToDataTable<T>(IList<T> i_objlist, DevExpress.XtraGrid.Views.Grid.GridView view, string tableName)
        //{
        //    if (i_objlist == null || i_objlist.Count <= 0)
        //    {
        //        // return null;
        //    }


        //    Dictionary<string, string> FieldNames = new Dictionary<string, string>();
        //    foreach (DevExpress.XtraGrid.Columns.GridColumn item in view.Columns)
        //    {
        //        if (item.Visible)
        //        {
        //            FieldNames.Add(item.FieldName, item.Caption);
        //        }
        //    }



        //    System.Data.DataTable dt = new System.Data.DataTable(typeof(T).Name);
        //    DataColumn column = null;
        //    DataRow row;

        //    System.Reflection.PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        //    foreach (T t in i_objlist)
        //    {
        //        if (t == null)
        //        {
        //            continue;
        //        }

        //        row = dt.NewRow();

        //        for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
        //        {
        //            System.Reflection.PropertyInfo pi = myPropertyInfo[i];

        //            string name = pi.Name;

        //            if (FieldNames.Keys.Contains(name))
        //            {
        //                if (dt.Columns[name] == null)
        //                {
        //                    column = new DataColumn();
        //                    column.ColumnName = FieldNames[name];
        //                    column.DataType = SqlDbType.NVarChar.GetType();
        //                    dt.Columns.Add(column);
        //                }
        //                row[column.ColumnName] = pi.GetValue(t, null);
        //            }
        //        }

        //        dt.Rows.Add(row);

        //    }

        //    return dt;
        //}




        //public System.Data.DataTable ToDataTable<T>(IList<T> list, DevExpress.XtraGrid.Views.Grid.GridView view, string tableName)
        //{
        //    List<PropertyInfo> pList = new List<PropertyInfo>();
        //    Type type = typeof(T);
        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    try
        //    {
        //        Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
        //        {
        //            pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType);
        //        });
        //        foreach (var item in list)
        //        {
        //            DataRow row = dt.NewRow();
        //            pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
        //            dt.Rows.Add(row);
        //        }

        //        foreach (DevExpress.XtraGrid.Columns.GridColumn item in view.Columns)
        //        {
        //            if (item.Visible)
        //                dt.Columns[item.FieldName].ColumnName = item.Caption;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    if (!string.IsNullOrEmpty(tableName))
        //        dt.TableName = tableName;
        //    return dt;
        //}


        private void spinEditDefaultQuantity_EditValueChanged(object sender, EventArgs e)
        {
            //this.spinEditDefaultQuantity.Value = this.spinEditDefaultQuantity.Value == null ? 0 : this.spinEditDefaultQuantity.Value;
            foreach (Model.BomPackageDetails pack in this._bomParmentPartInfo.BomPackageDetails)
            {
                pack.Quantity = double.Parse(this.spinEditDefaultQuantity.Value.ToString()) / pack.UseQuantity;

            }
            this.gridControl2.RefreshDataSource();

        }

        private void buttonEditTechonlogyHeaderid_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ProduceManager.Techonlogy.ChooseTechonlogyForm f = new Book.UI.Settings.ProduceManager.Techonlogy.ChooseTechonlogyForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this._techonlogyHeader = f.SelectedItem as Model.TechonlogyHeader;

                //if (this._techonlogyHeader != null)
                //{
                this.buttonEditTechonlogyHeaderid.EditValue = this._techonlogyHeader;
                //this.newChooseContorlCustomer.ShowButton = true;
                //this.newChooseContorlCustomer.ButtonReadOnly = false;
                if (this._techonlogyHeader != null)
                    this.bindingSourceBOMProcess.DataSource = this.technologydetailsManager.Select(this._techonlogyHeader);
                else
                    this.bindingSourceBOMProcess.DataSource = null;

            }
            f.Dispose();
            GC.Collect();
        }

        private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            if (e.ListSourceRowIndex < 0)
                return;

            IList<Model.Technologydetails> Details = this.bindingSourceBOMProcess.DataSource as IList<Model.Technologydetails>;
            if (Details == null || Details.Count <= 0)
                return;
            Model.Procedures procedures = Details[e.ListSourceRowIndex].Procedures;
            if (procedures == null) return;
            switch (e.Column.Name)
            {
                case "gridColumnProceduresId":
                    e.DisplayText = procedures.Id;
                    break;
                case "gridColumnWorkHouse":
                    if (procedures.WorkHouse != null)
                        e.DisplayText = procedures.WorkHouse.Workhousename;
                    else
                        e.DisplayText = string.Empty;
                    break;

            }
        }

        private void simpleButtonSearch_Click(object sender, EventArgs e)
        {
            IList<Model.BomParentPartInfo> bomList = new List<Model.BomParentPartInfo>();
            if (comboBoxEdit1.SelectedIndex == 0)
                bomList = this.bomParmentInfoManager.SelectByIdOrNameKey(null, null, null, this.textEditSearchName.Text);
            else if (comboBoxEdit1.SelectedIndex == 1)
                bomList = this.bomParmentInfoManager.SelectByIdOrNameKey(this.textEditSearchName.Text, null, null, null);
            else if (comboBoxEdit1.SelectedIndex == 2)
                bomList = this.bomParmentInfoManager.SelectByIdOrNameKey(null, this.textEditSearchName.Text, null, null);
            else if (comboBoxEdit1.SelectedIndex == 3)
                bomList = this.bomParmentInfoManager.SelectByIdOrNameKey(null, null, this.textEditSearchName.Text, null);
            if (bomList == null || bomList.Count == 0)
                return;
            this.treeList1.ClearNodes();
            GC.Collect();
            if (bomList.Count == 1)
            {
                this.searchBom = bomList[0];
                treeLoad(this.searchBom);
            }
            // foreach (TreeListNode listNode in this.treeList1.Nodes)
            //{
            //        if (listNode.Tag.ToString() == productList[0].ProductId)
            //        {
            //            listNode.Selected=true;
            //        }                
            //}


        }

        //成品一览
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BomList listform = new BomList(1);
            if (listform.ShowDialog(this) != DialogResult.OK) return;
            Model.BomParentPartInfo parentModel = listform.SelectItem as Model.BomParentPartInfo;
            if (parentModel == null || parentModel.BomId == null) return;
            this.searchBom = this.bomParmentInfoManager.Get(parentModel.BomId);
            this.treeLoad(this.searchBom);

            //刷新
            this._bomParmentPartInfo = this.searchBom;
            this.action = "view";
            this.Refresh();

            listform.Dispose();
            GC.Collect();
        }

        //搜索
        private void barButtonItemQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BomList listform = new BomList();
            if (listform.ShowDialog(this) != DialogResult.OK) return;
             Model.BomParentPartInfo parentModel = listform.SelectItem as Model.BomParentPartInfo;
            if (parentModel == null || parentModel.BomId == null) return;
            this.searchBom = this.bomParmentInfoManager.Get(parentModel.BomId);
            this.treeLoad(this.searchBom);

            this._bomParmentPartInfo = this.searchBom;
            this.action = "view";
            this.Refresh();


            listform.Dispose();
            GC.Collect();
        }

        void ExportToXls(DevExpress.XtraGrid.Views.Grid.GridView gridview)
        {
            if (this._bomParmentPartInfo.BomPackageDetails.Count == 0) return;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel file|*.xls";
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            string file = dialog.FileName;
            gridview.ExportToXls(file);
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ExportToXls(this.gridView2);
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChooseBomIdRangeForm form = new ChooseBomIdRangeForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                //调用导出的方法
            }
        }

        private void BomEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void treeList1_Click(object sender, EventArgs e)
        {
            // if (flag == 0)
            {

                if (treeList1.Selection != null && treeList1.Selection.Count == 1)
                {
                    string str = treeList1.Selection[0].Tag.ToString();
                    if (str.IndexOf('+') > 0) //成品  依据bomid查询
                        this._bomParmentPartInfo = this.bomParmentInfoManager.Get(str.Substring(str.IndexOf('+') + 1));
                    else
                    {
                        this._bomParmentPartInfo = this.bomParmentInfoManager.SelectByProductId(str);
                    }
                    //如果母件是客户BOM 
                    this.action = "view";
                    if (this._bomParmentPartInfo == null)
                    {
                        this._bomParmentPartInfo = new Book.Model.BomParentPartInfo();
                        this._bomParmentPartInfo.BomId = Guid.NewGuid().ToString();
                        this._bomParmentPartInfo.Product = productManager.Get(str);
                        this._bomParmentPartInfo.ProductId = this._bomParmentPartInfo.Product.ProductId;
                        this._bomParmentPartInfo.InsertTime = DateTime.Now;
                        this._bomParmentPartInfo.CreateMan = BL.V.ActiveOperator.OperatorName;
                        this._bomParmentPartInfo.EffectiveDate = DateTime.Now;
                        this._bomParmentPartInfo.Id = this.bomParmentInfoManager.GetId();
                        this.action = "insert";
                    }
                    this.Refresh();
                }
            }
        }
    }
}