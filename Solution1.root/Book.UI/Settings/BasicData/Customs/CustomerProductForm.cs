using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Book.UI.Settings.BasicData.Customs
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010   咸陽飛馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 客戶產品設置
   // 文 件 名：CustomerProductForm
   // 编 码 人: 马艳军                   完成时间:2009-10-10
   // 修改原因：
   // 修 改 人:                          修改时间:
   // 修改原因：
   // 修 改 人:                          修改时间:
   //----------------------------------------------------------------*/
    public partial class CustomerProductForm : BaseEditForm
    {

        private Model.CustomerProducts _customerProduct;
        private BL.CustomerProductsManager customerProductsManager = new Book.BL.CustomerProductsManager();
        //private BL.CustomerProductProcessManager customerProductProcessManager = new Book.BL.CustomerProductProcessManager();
        //private BL.ProcessingManager processingManager = new Book.BL.ProcessingManager();
        //private BL.BomParentPartInfoManager bomParement = new Book.BL.BomParentPartInfoManager();
        //private BL.CustomerProductsBomManager customerBom = new Book.BL.CustomerProductsBomManager();
        //private BL.PackageCustomerDetailsManager package = new Book.BL.PackageCustomerDetailsManager();       
        private BL.ProductUnitManager productUnitManager = new Book.BL.ProductUnitManager();
        //private BL.ProcessCategoryManager processCategoryManager = new Book.BL.ProcessCategoryManager();
        private BL.ProductManager productManager = new Book.BL.ProductManager();
        private Model.Customer _customer = null;
        private BL.UnitGroupManager UnitGroupManager = new Book.BL.UnitGroupManager();

        public CustomerProductForm()
        {
            InitializeComponent();
            this.requireValueExceptions.Add(Model.CustomerProducts.PRO_CustomerId, new AA("請選擇或輸入客戶！", this.newChooseContorlCustomer));
            this.requireValueExceptions.Add(Model.CustomerProducts.PRO_ProductId, new AA("請選擇或輸入己方貨品！", this.newChooseContorlProduct));
            this.requireValueExceptions.Add(Model.CustomerProducts.PRO_CustomerProductId, new AA("請輸入客戶商品名稱！", this.textEditProductName));
            this.invalidValueExceptions.Add(Model.CustomerProducts.PRO_CustomerProductId + "_Exists", new AA(Properties.Resources.ExistsCustomerProductId, this.textEditProductName));
            // this.invalidValueExceptions.Add(Model.CustomerProducts.PRO_CustomerId, new AA(Properties.Resources.Existscustomerproduct, this.newChooseContorlCustomer));
            this.newChooseContorlCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();

            this.lookUpBasedUnitGroupId.Properties.DataSource = this.bindingSourceUnitGroup;
            this.lookUpCJUnit.Properties.DataSource = this.bindingSourceUnit;
            this.lookUpDepotUnit.Properties.DataSource = this.bindingSourceUnit;
            this.lookUpEditQualityTestUnitId.Properties.DataSource = this.bindingSourceUnit;
            this.lookUpProduceUnitId.Properties.DataSource = this.bindingSourceUnit;
            this.lookUpSellUnit.Properties.DataSource = this.bindingSourceUnit;
            this.bindingSourceUnitGroup.DataSource = this.UnitGroupManager.Select();
            this.newChooseUpdateEmployee.Choose = new Employees.ChooseEmployee();
            // this.newChooseContorlUnitGroup.Choose = new BasicData.UnitGroup.ChooseUnitGroup();

            this.action = "view";
        }

        public CustomerProductForm(Model.Customer customers)
            : this()
        {
            _customer = customers;
        }

        public CustomerProductForm(Model.CustomerProducts customProduct)
            : this()
        {
            _customerProduct = customProduct;
            this.action = "update";
        }

        public CustomerProductForm(Model.CustomerProducts customProduct, string action)
            : this()
        {
            _customerProduct = customProduct;
            this.action = action;
        }

        protected override void AddNew()
        {
            _customerProduct = new Book.Model.CustomerProducts();
            // _customerProduct.Customer = _customer;
            this._customerProduct.PrimaryKeyId = Guid.NewGuid().ToString();
            // Model.CustomerProductProcess 

            this.action = "insert";
        }

        protected override void Delete()
        {
            if (this._customerProduct == null)
                return;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.customerProductsManager.Delete(this._customerProduct);

            this._customerProduct = this.customerProductsManager.GetNext(this._customerProduct);
            if (this._customerProduct == null)
            {
                this._customerProduct = this.customerProductsManager.GetLast();
            }
            this.bindingSource1.DataSource = this.customerProductsManager.DataReaderBind<Model.CustomerProducts>("SELECT PrimaryKeyId PrimaryKeyId,CustomerProductId,ProductName ProductId,CustomerShortName CustomerId,Version   FROM CustomerProducts c LEFT JOIN Product p ON c.productid=p.productid LEFT JOIN Customer t ON c.CustomerId=t.CustomerId", null, CommandType.Text);
            this.gridControl1.RefreshDataSource();
        }

        protected override bool HasRows()
        {
            return this.customerProductsManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.customerProductsManager.HasRowsAfter(this._customerProduct);
        }

        protected override bool HasRowsPrev()
        {
            return this.customerProductsManager.HasRowsBefore(this._customerProduct);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this.newChooseContorlCustomer, this.newChooseContorlProduct, this });
        }

        protected override void MoveFirst()
        {
            this._customerProduct = this.customerProductsManager.GetFirst();
        }

        protected override void MoveLast()
        {

            this._customerProduct = this.customerProductsManager.GetLast();
        }

        protected override void MoveNext()
        {
            Model.CustomerProducts company = this.customerProductsManager.GetNext(this._customerProduct);
            if (company == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._customerProduct = company;
        }

        protected override void MovePrev()
        {
            Model.CustomerProducts company = this.customerProductsManager.GetPrev(this._customerProduct);
            if (company == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this._customerProduct = company;
        }

        public override void Refresh()
        {
            if (this._customerProduct == null)
            {
                this.AddNew();
            }

            if (this.action != "insert")
            {
                if (this._customerProduct.PrimaryKeyId != null)
                    this._customerProduct = this.customerProductsManager.Get(this._customerProduct.PrimaryKeyId);

                //this._customerProduct.CustomerProductProcessList = this.customerProductProcessManager.SelectProcessCategory(_customerProduct);

                //this._customerProduct.CustomerProductsBomInfos = this.customerProductsManager.SelectBomInfos(_customerProduct);
                //this._customerProduct.PackageCustomerDetails = package.Select(_customerProduct.CustomerProductId);

            }

            //Model.CustomerProductProcess customerProductProcess = new Book.Model.CustomerProductProcess();

            //this._customerProduct.CustomerProductProcessList.Add(customerProductProcess);

            //else
            //{

            //    this._customerProduct.CustomerProductProcessList = this.customerProductProcessManager.SelectProcessCategory(this._customerProduct);



            //}

            //this.bindingSourceCustomerProductProcess.DataSource = this._customerProduct.CustomerProductProcessList;

            //this.bindingSourceCustomerBom.DataSource = this._customerProduct.CustomerProductsBomInfos;

            //this.bindingSourcePackageProduct.DataSource = _customerProduct.PackageCustomerDetails;
            if (this._customerProduct != null)
            {
                if (this._customerProduct.UnitGroup != null)
                {
                    //this.newChooseContorDepotUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
                    //this.newChooseContorBuyUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
                    //this.newChooseContorQualityTestUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
                    ////this.newChooseContorlMainUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
                    //this.newChooseContorProduceUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
                    //this.newChooseContorSellUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);


                    //if (this._customerProduct.Depot != null)
                    //{
                    //    this.newChooseContorlDepotPosition.Choose = new Book.UI.Invoices.ChooseDepotPosition(this._customerProduct.Depot);
                    //}

                }
                //this.textEditProssProductNme.Text = this._customerProduct.CustomerProductProceName;
                // this.textEditCustomerProductId.Text = this._customerProduct.CustomerProductId;
                this.textEditProductName.Text = this._customerProduct.CustomerProductId;
                this.newChooseContorlCustomer.EditValue = this._customerProduct.Customer;
                // this.textEditBarCode.Text = this._customerProduct.BarCode;

                this.richTextBox1.Rtf = this._customerProduct.CustomerProductDesc;
                if (this._customerProduct.Product != null)
                {
                    this.newChooseContorlProduct.Text = this._customerProduct.Product.ProductName;
                    //  this.labelControl1.Text = this._customerProduct.Product.ProductName;
                    this.newChooseContorlProduct.Tag = this._customerProduct.Product.ProductId;


                    //  this.bindingSourceCustomerBom.DataSource = this._customerProduct.CustomerProductsBomInfos;
                }
                else
                {
                    this.newChooseContorlProduct.Text = null;
                    //  this.labelControl1.Text = null;
                    this.newChooseContorlProduct.Tag = null;
                }

                this.lookUpBasedUnitGroupId.EditValue = this._customerProduct.UnitGroupId;
                this.lookUpCJUnit.EditValue = this._customerProduct.BuyUnitId;
                this.lookUpDepotUnit.EditValue = this._customerProduct.DepotUnitId;
                this.lookUpProduceUnitId.EditValue = this._customerProduct.ProduceUnitId;
                this.lookUpSellUnit.EditValue = this._customerProduct.SellUnitId;
                this.lookUpEditQualityTestUnitId.EditValue = this._customerProduct.QualityTestUnitId;

                // this.calcEditLossRate.EditValue = this._customerProduct.LossRate;
                if (global::Helper.DateTimeParse.DateTimeEquls(this._customerProduct.VersionDate, global::Helper.DateTimeParse.NullDate))
                {
                    this.dateEditVersionDate.EditValue = null;
                }
                else
                {
                    this.dateEditVersionDate.EditValue = this._customerProduct.VersionDate;
                }
                //this.textEditVersionDescription.Text = this._customerProduct.VersionDescription;
                this.textEditVersion.Text = this._customerProduct.Version;

                this.textEditCreator.Text = this._customerProduct.Creator;
                //this.textEditModifier.Text = this._customerProduct.Modify;

                //Model.Product p = this.productManager.Get(this._customerProduct.CustomerProductProceName);
                //if (p != null)
                //{
                //    string[] XOPriAndRange = (p.XOPriceAndRange != null && p.XOPriceAndRange != "") ? p.XOPriceAndRange.Split(',') : null;
                //    if (XOPriAndRange != null)
                //    {
                //        this.XOPriceRange1_L.EditValue = (XOPriAndRange[0] != null && XOPriAndRange[0] != "") ? XOPriAndRange[0].Split('/')[0] : "0";
                //        this.XOPriceRange1_R.EditValue = (XOPriAndRange[0] != null && XOPriAndRange[0] != "") ? XOPriAndRange[0].Split('/')[1] : "0";
                //        this.XOPrice1.EditValue = (XOPriAndRange[0] != null && XOPriAndRange[0] != "") ? XOPriAndRange[0].Split('/')[2] : "0";
                //        this.XOPriceRange2_L.EditValue = (XOPriAndRange[1] != null && XOPriAndRange[1] != "") ? XOPriAndRange[1].Split('/')[0] : "0";
                //        this.XOPriceRange2_R.EditValue = (XOPriAndRange[1] != null && XOPriAndRange[1] != "") ? XOPriAndRange[1].Split('/')[1] : "0";
                //        this.XOPrice2.EditValue = (XOPriAndRange[1] != null && XOPriAndRange[1] != "") ? XOPriAndRange[1].Split('/')[2] : "0";
                //        this.XOPriceRange3_L.EditValue = (XOPriAndRange[2] != null && XOPriAndRange[2] != "") ? XOPriAndRange[2].Split('/')[0] : "0";
                //        this.XOPriceRange3_R.EditValue = (XOPriAndRange[2] != null && XOPriAndRange[2] != "") ? XOPriAndRange[2].Split('/')[1] : "0";
                //        this.XOPrice3.EditValue = (XOPriAndRange[2] != null && XOPriAndRange[2] != "") ? XOPriAndRange[2].Split('/')[2] : "0";
                //        this.XOPriceRange4_L.EditValue = (XOPriAndRange[3] != null && XOPriAndRange[3] != "") ? XOPriAndRange[3].Split('/')[0] : "0";
                //        this.XOPriceRange4_R.EditValue = (XOPriAndRange[3] != null && XOPriAndRange[3] != "") ? XOPriAndRange[3].Split('/')[1] : "0";
                //        this.XOPrice4.EditValue = (XOPriAndRange[3] != null && XOPriAndRange[3] != "") ? XOPriAndRange[3].Split('/')[2] : "0";
                //        this.XOPriceRange5_L.EditValue = (XOPriAndRange[4] != null && XOPriAndRange[4] != "") ? XOPriAndRange[4].Split('/')[0] : "0";
                //        this.XOPriceRange5_R.EditValue = (XOPriAndRange[4] != null && XOPriAndRange[4] != "") ? XOPriAndRange[4].Split('/')[1] : "0";
                //        this.XOPrice5.EditValue = (XOPriAndRange[4] != null && XOPriAndRange[4] != "") ? XOPriAndRange[4].Split('/')[2] : "0";
                //        this.XOPriceRange6_L.EditValue = (XOPriAndRange[5] != null && XOPriAndRange[5] != "") ? XOPriAndRange[5].Split('/')[0] : "0";
                //        this.XOPriceRange6_R.EditValue = (XOPriAndRange[5] != null && XOPriAndRange[5] != "") ? XOPriAndRange[5].Split('/')[1] : "0";
                //        this.XOPrice6.EditValue = (XOPriAndRange[5] != null && XOPriAndRange[5] != "") ? XOPriAndRange[5].Split('/')[2] : "0";
                //        this.XOPriceRange7_L.EditValue = (XOPriAndRange[6] != null && XOPriAndRange[6] != "") ? XOPriAndRange[6].Split('/')[0] : "0";
                //        this.XOPrice7.EditValue = (XOPriAndRange[6] != null && XOPriAndRange[6] != "") ? XOPriAndRange[6].Split('/')[2] : "0";
                //    }
                //    else
                //    {
                //        this.XOPriceRange1_L.EditValue = "0";
                //        this.XOPriceRange1_R.EditValue = "0";
                //        this.XOPrice1.EditValue = "0";
                //        this.XOPriceRange2_L.EditValue = "0";
                //        this.XOPriceRange2_R.EditValue = "0";
                //        this.XOPrice2.EditValue = "0";
                //        this.XOPriceRange3_L.EditValue = "0";
                //        this.XOPriceRange3_R.EditValue = "0";
                //        this.XOPrice3.EditValue = "0";
                //        this.XOPriceRange4_L.EditValue = "0";
                //        this.XOPriceRange4_R.EditValue = "0";
                //        this.XOPrice4.EditValue = "0";
                //        this.XOPriceRange5_L.EditValue = "0";
                //        this.XOPriceRange5_R.EditValue = "0";
                //        this.XOPrice5.EditValue = "0";
                //        this.XOPriceRange6_L.EditValue = "0";
                //        this.XOPriceRange6_R.EditValue = "0";
                //        this.XOPrice6.EditValue = "0";
                //        this.XOPriceRange7_L.EditValue = "0";
                //        this.XOPrice7.EditValue = "0";
                //    }
                //}
                //else
                //{
                //    this.XOPriceRange1_L.EditValue = "0";
                //    this.XOPriceRange1_R.EditValue = "0";
                //    this.XOPrice1.EditValue = "0";
                //    this.XOPriceRange2_L.EditValue = "0";
                //    this.XOPriceRange2_R.EditValue = "0";
                //    this.XOPrice2.EditValue = "0";
                //    this.XOPriceRange3_L.EditValue = "0";
                //    this.XOPriceRange3_R.EditValue = "0";
                //    this.XOPrice3.EditValue = "0";
                //    this.XOPriceRange4_L.EditValue = "0";
                //    this.XOPriceRange4_R.EditValue = "0";
                //    this.XOPrice4.EditValue = "0";
                //    this.XOPriceRange5_L.EditValue = "0";
                //    this.XOPriceRange5_R.EditValue = "0";
                //    this.XOPrice5.EditValue = "0";
                //    this.XOPriceRange6_L.EditValue = "0";
                //    this.XOPriceRange6_R.EditValue = "0";
                //    this.XOPrice6.EditValue = "0";
                //    this.XOPriceRange7_L.EditValue = "0";
                //    this.XOPrice7.EditValue = "0";
                //}

                //if (this._customerProduct.Setting != null)
                //{
                //    string[] paras = this._customerProduct.Setting.SettingCurrentValue.Split(',');
                //    this.spinBoxLong.EditValue = Convert.ToDecimal(paras[0]);
                //    this.spinBoxWidth.EditValue = Convert.ToDecimal(paras[1]);
                //    this.spinBoxHeight.EditValue = Convert.ToDecimal(paras[2]);
                //    this.spinBoxJWeight.EditValue = Convert.ToDecimal(paras[3]);
                //    this.spinBoxMWeight.EditValue = Convert.ToDecimal(paras[4]);
                //    this.spinBoxCaiJi.EditValue = Convert.ToDecimal(paras[5]);
                //}
                //else
                //{
                //    this.spinBoxLong.EditValue = this.spinBoxWidth.EditValue = this.spinBoxHeight.EditValue = this.spinBoxJWeight.EditValue = this.spinBoxMWeight.EditValue = this.spinBoxCaiJi.EditValue = 0;
                //}

                this.txt_Product_idNO.Text = this._customerProduct.Product != null ? this._customerProduct.Product.Id : "";
                //箱子属性
                this.spinBoxLong.EditValue = this._customerProduct.BLong == null ? 0 : this._customerProduct.BLong;
                this.spinBoxWidth.EditValue = this._customerProduct.BWide == null ? 0 : this._customerProduct.BWide;
                this.spinBoxHeight.EditValue = this._customerProduct.BHigh == null ? 0 : this._customerProduct.BHigh;
                this.spinBoxJWeight.EditValue = this._customerProduct.JWeight == null ? 0 : this._customerProduct.JWeight;
                this.spinBoxMWeight.EditValue = this._customerProduct.MWeight == null ? 0 : this._customerProduct.MWeight;
                this.spinBoxCaiJi.EditValue = this._customerProduct.Caiji == null ? 0 : this._customerProduct.Caiji;
                this.spinBoxPackingNum.EditValue = this._customerProduct.PackingSpecification == null ? 0 : this._customerProduct.PackingSpecification;

                //修改人
                this.newChooseUpdateEmployee.EditValue = this._customerProduct.UpdateEmployee;
            }

            switch (this.action)
            {
                case "insert":
                    this.textEditCreator.Text = BL.V.ActiveOperator.OperatorName;
                    // this.textEditCustomerProductId.Properties.ReadOnly = false;
                    this.newChooseContorlProduct.Properties.ReadOnly = false;
                    this.newChooseContorlProduct.Properties.Buttons[0].Visible = true;
                    this.newChooseContorlCustomer.ButtonReadOnly = false;
                    this.newChooseContorlCustomer.ShowButton = true;

                    //this.textEditBarCode.Properties.ReadOnly = false;
                    this.textEditProductName.Properties.ReadOnly = false;
                    //this.textEditVersion.Properties.ReadOnly = false;
                    //this.textEditVersionDescription.Properties.ReadOnly = false;
                    //this.dateEditVersionDate.Properties.ReadOnly = false;

                    //this.newChooseContorBuyUnit.ShowButton = true;
                    //this.newChooseContorBuyUnit.ButtonReadOnly = false;
                    //this.newChooseContorDepotUnit.ShowButton = true;
                    //this.newChooseContorDepotUnit.ButtonReadOnly = false;
                    //this.newChooseContorlDepot.ShowButton = true;
                    //this.newChooseContorlDepot.ButtonReadOnly = false;
                    //this.newChooseContorlDepotPosition.ShowButton = true;
                    //this.newChooseContorlDepotPosition.ButtonReadOnly = false;
                    //this.newChooseContorlMainUnit.ShowButton = true;
                    //this.newChooseContorlMainUnit.ButtonReadOnly = false;
                    //this.newChooseContorlUnitGroup.ShowButton = true;
                    //this.newChooseContorlUnitGroup.ButtonReadOnly = false;
                    //this.newChooseContorProduceUnit.ShowButton = true;
                    //this.newChooseContorProduceUnit.ButtonReadOnly = false;
                    //this.newChooseContorQualityTestUnit.ShowButton = true;
                    //this.newChooseContorQualityTestUnit.ButtonReadOnly = false;
                    //this.newChooseContorSellUnit.ShowButton = true;
                    //this.newChooseContorSellUnit.ButtonReadOnly = false;

                    //销售价格区间
                    //this.XOPrice1.Properties.ReadOnly = false;
                    //this.XOPrice2.Properties.ReadOnly = false;
                    //this.XOPrice3.Properties.ReadOnly = false;
                    //this.XOPrice4.Properties.ReadOnly = false;
                    //this.XOPrice5.Properties.ReadOnly = false;
                    //this.XOPrice6.Properties.ReadOnly = false;
                    //this.XOPrice7.Properties.ReadOnly = false;
                    //this.XOPriceRange1_L.Properties.ReadOnly = false;
                    //this.XOPriceRange1_R.Properties.ReadOnly = false;
                    //this.XOPriceRange2_L.Properties.ReadOnly = false;
                    //this.XOPriceRange2_R.Properties.ReadOnly = false;
                    //this.XOPriceRange3_L.Properties.ReadOnly = false;
                    //this.XOPriceRange3_R.Properties.ReadOnly = false;
                    //this.XOPriceRange4_L.Properties.ReadOnly = false;
                    //this.XOPriceRange4_R.Properties.ReadOnly = false;
                    //this.XOPriceRange5_L.Properties.ReadOnly = false;
                    //this.XOPriceRange5_R.Properties.ReadOnly = false;
                    //this.XOPriceRange6_L.Properties.ReadOnly = false;
                    //this.XOPriceRange6_R.Properties.ReadOnly = false;
                    //this.XOPriceRange7_L.Properties.ReadOnly = false;
                    this.spinBoxPackingNum.Enabled = true;

                    break;
                case "update":
                    this.textEditCreator.Text = BL.V.ActiveOperator.OperatorName;
                    this._customer = this._customerProduct.Customer;
                    // this.textEditCustomerProductId.Properties.ReadOnly = false;
                    this.newChooseContorlProduct.Properties.ReadOnly = false;
                    this.newChooseContorlProduct.Properties.Buttons[0].Visible = true;
                    this.newChooseContorlCustomer.ButtonReadOnly = false;
                    this.newChooseContorlCustomer.ShowButton = true;

                    // this.textEditBarCode.Properties.ReadOnly = false;
                    this.textEditProductName.Properties.ReadOnly = false;
                    //this.textEditVersion.Properties.ReadOnly = false;
                    //this.textEditVersionDescription.Properties.ReadOnly = false;
                    //this.dateEditVersionDate.Properties.ReadOnly = false;

                    //this.newChooseContorBuyUnit.ShowButton = true;
                    //this.newChooseContorBuyUnit.ButtonReadOnly = false;
                    //this.newChooseContorDepotUnit.ShowButton = true;
                    //this.newChooseContorDepotUnit.ButtonReadOnly = false;
                    //this.newChooseContorlDepot.ShowButton = true;
                    //this.newChooseContorlDepot.ButtonReadOnly = false;
                    //this.newChooseContorlDepotPosition.ShowButton = true;
                    //this.newChooseContorlDepotPosition.ButtonReadOnly = false;
                    //this.newChooseContorlMainUnit.ShowButton = true;
                    //this.newChooseContorlMainUnit.ButtonReadOnly = false;
                    //this.newChooseContorlUnitGroup.ShowButton = true;
                    //this.newChooseContorlUnitGroup.ButtonReadOnly = false;
                    //this.newChooseContorProduceUnit.ShowButton = true;
                    //this.newChooseContorProduceUnit.ButtonReadOnly = false;
                    //this.newChooseContorQualityTestUnit.ShowButton = true;
                    //this.newChooseContorQualityTestUnit.ButtonReadOnly = false;
                    //this.newChooseContorSellUnit.ShowButton = true;
                    //this.newChooseContorSellUnit.ButtonReadOnly = false;
                    //销售价格区间
                    //this.XOPrice1.Properties.ReadOnly = false;
                    //this.XOPrice2.Properties.ReadOnly = false;
                    //this.XOPrice3.Properties.ReadOnly = false;
                    //this.XOPrice4.Properties.ReadOnly = false;
                    //this.XOPrice5.Properties.ReadOnly = false;
                    //this.XOPrice6.Properties.ReadOnly = false;
                    //this.XOPrice7.Properties.ReadOnly = false;
                    //this.XOPriceRange1_L.Properties.ReadOnly = false;
                    //this.XOPriceRange1_R.Properties.ReadOnly = false;
                    //this.XOPriceRange2_L.Properties.ReadOnly = false;
                    //this.XOPriceRange2_R.Properties.ReadOnly = false;
                    //this.XOPriceRange3_L.Properties.ReadOnly = false;
                    //this.XOPriceRange3_R.Properties.ReadOnly = false;
                    //this.XOPriceRange4_L.Properties.ReadOnly = false;
                    //this.XOPriceRange4_R.Properties.ReadOnly = false;
                    //this.XOPriceRange5_L.Properties.ReadOnly = false;
                    //this.XOPriceRange5_R.Properties.ReadOnly = false;
                    //this.XOPriceRange6_L.Properties.ReadOnly = false;
                    //this.XOPriceRange6_R.Properties.ReadOnly = false;
                    //this.XOPriceRange7_L.Properties.ReadOnly = false;
                    this.spinBoxPackingNum.Enabled = true;

                    break;
                case "view":
                    //this.textEditCustomerProductId.Properties.ReadOnly = true;
                    this.newChooseContorlProduct.Properties.ReadOnly = true;
                    this.newChooseContorlProduct.Properties.Buttons[0].Visible = false;
                    this.newChooseContorlCustomer.ButtonReadOnly = true;
                    this.newChooseContorlCustomer.ShowButton = false;

                    //this.textEditBarCode.Properties.ReadOnly = true;
                    this.textEditProductName.Properties.ReadOnly = true;
                    //this.textEditVersion.Properties.ReadOnly = true;
                    //this.textEditVersionDescription.Properties.ReadOnly = true;
                    //this.dateEditVersionDate.Properties.ReadOnly = true;

                    //this.newChooseContorBuyUnit.ShowButton = false;
                    //this.newChooseContorBuyUnit.ButtonReadOnly = true;
                    //this.newChooseContorDepotUnit.ShowButton = false;
                    //this.newChooseContorDepotUnit.ButtonReadOnly = true;
                    //this.newChooseContorlDepot.ShowButton = false;
                    //this.newChooseContorlDepot.ButtonReadOnly = true;
                    //this.newChooseContorlDepotPosition.ShowButton = false;
                    //this.newChooseContorlDepotPosition.ButtonReadOnly = true;
                    //this.newChooseContorlMainUnit.ShowButton = false;
                    //this.newChooseContorlMainUnit.ButtonReadOnly = true;
                    //this.newChooseContorlUnitGroup.ShowButton = false;
                    //this.newChooseContorlUnitGroup.ButtonReadOnly = true;
                    //this.newChooseContorProduceUnit.ShowButton = false;
                    //this.newChooseContorProduceUnit.ButtonReadOnly = true;
                    //this.newChooseContorQualityTestUnit.ShowButton = false;
                    //this.newChooseContorQualityTestUnit.ButtonReadOnly = true;
                    //this.newChooseContorSellUnit.ShowButton = false;
                    //  this.newChooseContorSellUnit.ButtonReadOnly = true;
                    //销售价格区间
                    //this.XOPrice1.Properties.ReadOnly = true;
                    //this.XOPrice2.Properties.ReadOnly = true;
                    //this.XOPrice3.Properties.ReadOnly = true;
                    //this.XOPrice4.Properties.ReadOnly = true;
                    //this.XOPrice5.Properties.ReadOnly = true;
                    //this.XOPrice6.Properties.ReadOnly = true;
                    //this.XOPrice7.Properties.ReadOnly = true;
                    //this.XOPriceRange1_L.Properties.ReadOnly = true;
                    //this.XOPriceRange1_R.Properties.ReadOnly = true;
                    //this.XOPriceRange2_L.Properties.ReadOnly = true;
                    //this.XOPriceRange2_R.Properties.ReadOnly = true;
                    //this.XOPriceRange3_L.Properties.ReadOnly = true;
                    //this.XOPriceRange3_R.Properties.ReadOnly = true;
                    //this.XOPriceRange4_L.Properties.ReadOnly = true;
                    //this.XOPriceRange4_R.Properties.ReadOnly = true;
                    //this.XOPriceRange5_L.Properties.ReadOnly = true;
                    //this.XOPriceRange5_R.Properties.ReadOnly = true;
                    //this.XOPriceRange6_L.Properties.ReadOnly = true;
                    //this.XOPriceRange6_R.Properties.ReadOnly = true;
                    //this.XOPriceRange7_L.Properties.ReadOnly = true;
                    //this.spinBoxLong.Enabled = false;
                    //this.spinBoxWidth.Enabled = false;
                    //this.spinBoxHeight.Enabled = false;
                    //this.spinBoxJWeight.Enabled = false;
                    //this.spinBoxMWeight.Enabled = false;
                    //this.spinBoxCaiJi.Enabled = false;
                    //this.spinBoxPackingNum.Enabled = false;

                    break;
                default:
                    break;
            }
            base.Refresh();

        }

        protected override void Save()
        {
            // this._customerProduct.CustomerProductId = this.textEditCustomerProductId.Text;
            this._customerProduct.Customer = this.newChooseContorlCustomer.EditValue as Model.Customer;
            this._customerProduct.ProductId = (string)this.newChooseContorlProduct.Tag;
            //   this._customerProduct.BarCode = this.textEditBarCode.Text;

            this._customerProduct.CustomerProductId = this.textEditProductName.Text.TrimEnd(new char[] { ' ' });
            //  this._customerProduct.DepotUnit = this.newChooseContorDepotUnit.EditValue as Model.ProductUnit;
            //this._customerProduct.ProduceUnit = this.newChooseContorProduceUnit.EditValue as Model.ProductUnit;
            //this._customerProduct.QualityTestUnit = this.newChooseContorQualityTestUnit.EditValue as Model.ProductUnit;
            //this._customerProduct.SellUnit = this.newChooseContorSellUnit.EditValue as Model.ProductUnit;
            //this._customerProduct.UnitGroup = this.newChooseContorlUnitGroup.EditValue as Model.UnitGroup;
            //this._customerProduct.BuyUnit = this.newChooseContorBuyUnit.EditValue as Model.ProductUnit;

            this._customerProduct.UnitGroupId = this.lookUpBasedUnitGroupId.EditValue == null ? null : this.lookUpBasedUnitGroupId.EditValue.ToString();
            this._customerProduct.BuyUnitId = this.lookUpCJUnit.EditValue == null ? null : this.lookUpCJUnit.EditValue.ToString();
            this._customerProduct.DepotUnitId = this.lookUpDepotUnit.EditValue == null ? null : this.lookUpDepotUnit.EditValue.ToString();
            this._customerProduct.ProduceUnitId = this.lookUpProduceUnitId.EditValue == null ? null : this.lookUpProduceUnitId.EditValue.ToString();
            this._customerProduct.SellUnitId = this.lookUpSellUnit.EditValue == null ? null : this.lookUpSellUnit.EditValue.ToString();
            this._customerProduct.QualityTestUnitId = this.lookUpEditQualityTestUnitId.EditValue == null ? null : this.lookUpEditQualityTestUnitId.EditValue.ToString();



            this._customerProduct.CustomerProductDesc = this.richTextBox1.Rtf;
            this._customerProduct.Version = this.textEditVersion.Text;

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditVersionDate.DateTime, new DateTime()))
            {
                this._customerProduct.VersionDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this._customerProduct.VersionDate = this.dateEditVersionDate.DateTime;
            }

            //   this._customerProduct.VersionDescription = this.textEditVersionDescription.Text;
            this._customerProduct.Creator = this.textEditCreator.Text;
            //  this._customerProduct.Modify = this.textEditModifier.Text;

            //if (!this.gridView1.PostEditor() || !this.gridView1.UpdateCurrentRow())
            //    return;
            //if (!this.gridView2.PostEditor() || !this.gridView2.UpdateCurrentRow())
            //    return;

            //销售价格区间存储
            //StringBuilder sbPAR1 = new StringBuilder();
            //sbPAR1.Append(this.XOPriceRange1_L.Value.ToString() + "/" + this.XOPriceRange1_R.Value.ToString() + "/" + this.XOPrice1.Value.ToString() + ",");
            //sbPAR1.Append(this.XOPriceRange2_L.Value.ToString() + "/" + this.XOPriceRange2_R.Value.ToString() + "/" + this.XOPrice2.Value.ToString() + ",");
            //sbPAR1.Append(this.XOPriceRange3_L.Value.ToString() + "/" + this.XOPriceRange3_R.Value.ToString() + "/" + this.XOPrice3.Value.ToString() + ",");
            //sbPAR1.Append(this.XOPriceRange4_L.Value.ToString() + "/" + this.XOPriceRange4_R.Value.ToString() + "/" + this.XOPrice4.Value.ToString() + ",");
            //sbPAR1.Append(this.XOPriceRange5_L.Value.ToString() + "/" + this.XOPriceRange5_R.Value.ToString() + "/" + this.XOPrice5.Value.ToString() + ",");
            //sbPAR1.Append(this.XOPriceRange6_L.Value.ToString() + "/" + this.XOPriceRange6_R.Value.ToString() + "/" + this.XOPrice6.Value.ToString() + ",");
            //sbPAR1.Append(this.XOPriceRange7_L.Value.ToString() + "/999999999999/" + this.XOPrice7.Value.ToString());

            //this._customerProduct.XOPrice = sbPAR1.ToString();

            //箱子规格
            this._customerProduct.BLong = Convert.ToDouble(this.spinBoxLong.Text);
            this._customerProduct.BWide = Convert.ToDouble(this.spinBoxWidth.Text);
            this._customerProduct.BHigh = Convert.ToDouble(this.spinBoxHeight.Text);
            this._customerProduct.JWeight = Convert.ToDouble(this.spinBoxJWeight.Text);
            this._customerProduct.MWeight = Convert.ToDouble(this.spinBoxMWeight.Text);
            this._customerProduct.Caiji = Convert.ToDouble(this.spinBoxCaiJi.Text);
            this._customerProduct.PackingSpecification = Convert.ToDouble(this.spinBoxPackingNum.Text);

            //修改人
            this._customerProduct.UpdateEmployeeId = this.newChooseUpdateEmployee.EditValue == null ? null : (this.newChooseUpdateEmployee.EditValue as Model.Employee).EmployeeId;

            switch (this.action)
            {
                case "insert":
                    this._customerProduct.OrderQuantity = 0;
                    this._customerProduct.DepotQuantity = 0;
                    this.customerProductsManager.Insert(this._customerProduct);
                    break;
                case "update":
                    this.customerProductsManager.Update(this._customerProduct);
                    break;
            }
            this.bindingSource1.DataSource = this.customerProductsManager.DataReaderBind<Model.CustomerProducts>("SELECT PrimaryKeyId PrimaryKeyId,CustomerProductId,ProductName ProductId,CustomerShortName CustomerId,Version   FROM CustomerProducts c LEFT JOIN Product p ON c.productid=p.productid LEFT JOIN Customer t ON c.CustomerId=t.CustomerId", null, CommandType.Text);
        }

        protected void InitData(Model.Product product)
        {
            if (product != null)
            {
                this._customerProduct.Product = product;
                //this._customerProduct.BuyUnit = product.BuyUnit;
                //this._customerProduct.DepotUnit = product.DepotUnit;
                this._customerProduct.Depot = product.Depot;
                this._customerProduct.DepotPosition = product.DepotPosition;
                //this._customerProduct.MainUnit = product.MainUnit;
                //this._customerProduct.UnitGroup = product.BasedUnitGroup;
                //this._customerProduct.ProduceUnit = product.ProduceUnit;
                //this._customerProduct.QualityTestUnit = product.QualityTstUnit;
                //this._customerProduct.SellUnit = product.SellUnit;


                this.lookUpBasedUnitGroupId.EditValue = product.BasedUnitGroupId;
                this.lookUpCJUnit.EditValue = product.BuyUnitId;
                this.lookUpDepotUnit.EditValue = product.DepotUnitId;
                this.lookUpProduceUnitId.EditValue = product.ProduceUnitId;
                this.lookUpSellUnit.EditValue = product.SellUnitId;
                this.lookUpEditQualityTestUnitId.EditValue = product.QualityTestUnitId;


                //    Model.BomParentPartInfo bom = bomParement.Get(product);

                //    if (bom != null)
                //    {
                //        this._customerProduct.LossRate = bom.LossRate;
                //        this._customerProduct.Version = bom.BomVersion;
                //        this._customerProduct.VersionDate = bom.EffectiveDate;
                //        this._customerProduct.VersionDescription = bom.BomDescription;

                //        this._customerProduct.CustomerProductsBomInfos.Clear();


                //        foreach (Model.BomComponentInfo component in bom.Components)
                //        {
                //            Model.CustomerProductsBom b = new Book.Model.CustomerProductsBom();
                //            b.BasicUseQuantity = component.BasicUseQuantity;
                //            b.Cost = component.Cost;
                //            b.EffectsDate = component.EffectsDate;
                //            b.ExpiringDate = component.ExpiringDate;
                //            b.FoundationQuantity = component.FoundationQuantity;
                //            b.InsteadOfFlag = component.InsteadOfFlag;
                //            b.IsFixedUseQuantity = component.IsFixedUseQuantity;
                //            b.IsSelect = component.IsSelect;
                //            b.offset = component.offset;
                //            b.OutProduct = component.OutProduct;
                //            b.PlanProportion = component.PlanProportion;
                //          //  b.Process = component.Process;
                //           // b.ProcessId = component.ProcessId;
                //            b.PriamryKeyId = component.PriamryKeyId;
                //            b.PrimaryKey = this._customerProduct;
                //            b.PrimaryKeyId = component.ProductId;
                //            b.ProductType = component.ProductType;
                //            b.ProvideType = component.ProvideType;
                //            b.Remarks = component.Remarks;
                //            b.SelectRule = component.SelectRule;
                //            b.SubLoseRate = component.SubLoseRate;
                //            b.UseQuantity = component.UseQuantity;
                //            b.Product = component.Product;
                //            b.ProductId = component.ProductId;
                //            this._customerProduct.CustomerProductsBomInfos.Add(b);
                //        }
                //    }
                //    else
                //    {
                //        this._customerProduct.CustomerProductsBomInfos.Clear();
                //    }
            }
            else
            {
                this._customerProduct.Product = null;
                //this._customerProduct.BuyUnit = null;
                //this._customerProduct.DepotUnit = null;
                this._customerProduct.Depot = null;
                this._customerProduct.DepotPosition = null;
                //this._customerProduct.MainUnit = null;
                //this._customerProduct.UnitGroup = null;
                //this._customerProduct.ProduceUnit = null;
                //this._customerProduct.QualityTestUnit = null;
                //this._customerProduct.SellUnit = null;

                this._customerProduct.LossRate = 0;
                this._customerProduct.Version = "";
                this._customerProduct.VersionDate = DateTime.Now;
                this._customerProduct.VersionDescription = "";

                // this._customerProduct.CustomerProductsBomInfos.Clear();
            }

            //if (this._customerProduct.UnitGroup != null)
            //{
            //    this.newChooseContorDepotUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
            //    this.newChooseContorBuyUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
            //    this.newChooseContorQualityTestUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
            //    //this.newChooseContorlMainUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
            //    this.newChooseContorProduceUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
            //    this.newChooseContorSellUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this._customerProduct.UnitGroup.UnitGroupId);
            //}

            //if (this._customerProduct.Depot != null)
            //{
            //    this.newChooseContorlDepotPosition.Choose = new Book.UI.Invoices.ChooseDepotPosition(this._customerProduct.Depot);
            //}

            //this.newChooseContorlUnitGroup.EditValue = this._customerProduct.UnitGroup;
            //// this.newChooseContorlDepot.EditValue = this._customerProduct.Depot;

            //this.newChooseContorBuyUnit.EditValue = this._customerProduct.BuyUnit;
            //this.newChooseContorDepotUnit.EditValue = this._customerProduct.DepotUnit;
            //// this.newChooseContorlMainUnit.EditValue = this._customerProduct.MainUnit;            
            //this.newChooseContorProduceUnit.EditValue = this._customerProduct.ProduceUnit;
            //this.newChooseContorQualityTestUnit.EditValue = this._customerProduct.QualityTestUnit;
            //this.newChooseContorSellUnit.EditValue = this._customerProduct.SellUnit;

            //this.newChooseContorlDepotPosition.EditValue = this._customerProduct.DepotPosition;

            //this.calcEditLossRate.EditValue = this._customerProduct.LossRate;
            //this.dateEditVersionDate.EditValue = this._customerProduct.VersionDate;
            //this.textEditVersionDescription.Text = this._customerProduct.VersionDescription;
            //this.textEditVersion.Text = this._customerProduct.Version;

            if (this._customerProduct.Product != null)
            {
                this.richTextBox1.Rtf = this._customerProduct.Product.ProductDescription;
                this.newChooseContorlProduct.Text = this._customerProduct.Product.ProductName;
                //  this.labelControl1.Text = this._customerProduct.Product.ProductName;
                this.newChooseContorlProduct.Tag = this._customerProduct.Product.ProductId;

                // this.bindingSourceCustomerBom.DataSource = this._customerProduct.CustomerProductsBomInfos;
            }
            else
            {
                this.newChooseContorlProduct.Text = null;
                //  this.labelControl1.Text = null;
                this.newChooseContorlProduct.Tag = null;
            }

            //this.bindingSourceCustomerBom.DataSource = this._customerProduct.CustomerProductsBomInfos;
            //this.gridControl2.RefreshDataSource();
            //if (_customerProduct != null)
            //{
            //    _customerProduct.PackageCustomerDetails = package.Select(_customerProduct.CustomerProductId);
            //    this.bindingSourcePackageProduct.DataSource = package.Select(_customerProduct.CustomerProductId);
            //    this.gridControl3.RefreshDataSource();
            //}
        }

        private void newChooseContorlProduct_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.SelectedItem == null)
                {
                    return;
                }
                Model.Product product = f.SelectedItem as Model.Product;

                InitData(product);
            }
        }

        private void newChooseContorlCustomer_EditValueChanged(object sender, EventArgs e)
        {
            this._customer = this.newChooseContorlCustomer.EditValue as Model.Customer;
            this._customerProduct.Customer = this._customer;
            //this.Refresh();
        }

        #region gridview列數據展示文本
        //private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        //{
        //    if (e.ListSourceRowIndex < 0)
        //        return;
        //    Model.Product p = null;

        //    IList<Model.CustomerProductsBom> boms = this.bindingSourceCustomerBom.DataSource as IList<Model.CustomerProductsBom>;

        //    if (boms == null || boms.Count <= 0)
        //        return;
        //    p = boms[e.ListSourceRowIndex].Product;
        //    if (p == null) return;
        //    switch (e.Column.Name)
        //    {
        //        case "gridColumnGuiGe":
        //            e.DisplayText = p.ProductSpecification;
        //            break;
        //        case "gridColumnUnit":
        //            e.DisplayText = p.DepotUnit.CnName;
        //            break;
        //        case "gridColumnId":
        //            e.DisplayText = p.Id;
        //            break;
        //    }
        //}
        #endregion

        #region 添加按鈕
        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    Book.UI.Invoices.ChooseProductForm f = new Book.UI.Invoices.ChooseProductForm();
        //    if (f.ShowDialog(this) == DialogResult.OK)
        //    {
        //        Model.Product p = f.SelectedItem as Model.Product;

        //        Model.CustomerProductsBom components = new Book.Model.CustomerProductsBom();

        //        components.BasicUseQuantity = 0;
        //        components.PrimaryKey = this._customerProduct;
        //        components.EffectsDate = DateTime.Now;
        //        components.ExpiringDate = DateTime.Now.AddYears(1000);
        //        components.offset = 0;
        //        components.Product = p;
        //        components.ProductId = p.ProductId;
        //        components.UseQuantity = 1;
        //        components.Remarks = "";

        //        this._customerProduct.CustomerProductsBomInfos.Add(components);
        //        this.bindingSourceCustomerBom.Position = this.bindingSourceCustomerBom.IndexOf(components);


        //       // this.gridControl2.RefreshDataSource();
        //    }
        //}
        #endregion

        #region 刪除按鈕
        //private void simpleButton2_Click(object sender, EventArgs e)
        //{
        //    if (this.bindingSourceCustomerBom.Current != null)
        //    {
        //        this._customerProduct.CustomerProductsBomInfos.Remove(this.bindingSourceCustomerBom.Current as Book.Model.CustomerProductsBom);

        //        this.gridControl2.RefreshDataSource();
        //    }
        //}
        #endregion

        private void CustomerProductForm_Load(object sender, EventArgs e)
        {
            // this.bindingSourceProduct.DataSource = this.productManager.Select();
            // this.bindingSourceProcessCategory.DataSource = this.processCategoryManager.Select();    
            this.bindingSource1.DataSource = this.customerProductsManager.DataReaderBind<Model.CustomerProducts>("SELECT PrimaryKeyId PrimaryKeyId,CustomerProductId,ProductName ProductId,CustomerShortName CustomerId,Version   FROM CustomerProducts c LEFT JOIN Product p ON c.productid=p.productid LEFT JOIN Customer t ON c.CustomerId=t.CustomerId", null, CommandType.Text);
            //  this.bindingSource1.DataSource = this.customerProductsManager.Select();
        }

        #region
        //private bool CanAdd(IList<Model.CustomerProductsBom> list)
        //{
        //    foreach (Model.CustomerProductsBom detail in list)
        //    {
        //        if (detail.Product == null || string.IsNullOrEmpty(detail.Product.ProductId))
        //            return false;
        //    }
        //    return true;
        //}
        #endregion

        //private void gridView2_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (this.action == "insert" || this.action == "update")
        //    {
        //        if (this.CanAdd(this._customerProduct.CustomerProductsBomInfos))
        //        {
        //            if (e.KeyData == Keys.Enter)
        //            {
        //                Model.CustomerProductsBom detail = new Model.CustomerProductsBom();
        //                detail.PrimaryKey = this._customerProduct;
        //                detail.EffectsDate = DateTime.Now;
        //                detail.ExpiringDate = DateTime.Now.AddYears(1000);
        //                detail.offset = 0;
        //                detail.PriamryKeyId = Guid.NewGuid().ToString();
        //                detail.SubLoseRate = 0.0;                     
        //                detail.UseQuantity = 0;
        //                detail.Remarks = "";

        //                this._customerProduct.CustomerProductsBomInfos.Add(detail);
        //            }
        //        }
        //        if (e.KeyData == Keys.Delete)
        //        {
        //            //this.simpleButtonRemove.PerformClick();
        //        }
        //        this.gridControl2.RefreshDataSource();
        //    }
        //}

        //private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        //{
        //    Model.Depot de = this.newChooseContorlDepot.EditValue as Model.Depot;

        //    if (de != null)
        //    {
        //        this.newChooseContorlDepotPosition.Choose = new Book.UI.Invoices.ChooseDepotPosition(de);
        //        this.newChooseContorlDepotPosition.EditValue = null;
        //    }
        //    else
        //    {
        //        this.newChooseContorlDepotPosition.Choose = null;
        //        this.newChooseContorlDepotPosition.EditValue = null;
        //    }
        //}

        //private void newChooseContorlUnitGroup_EditValueChanged(object sender, EventArgs e)
        //{
        //    Model.UnitGroup group = this.newChooseContorlUnitGroup.EditValue as Model.UnitGroup;

        //    if (group != null)
        //    {
        //        string pars = group.UnitGroupId;

        //        this.newChooseContorDepotUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorDepotUnit.EditValue = null;

        //        this.newChooseContorBuyUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorBuyUnit.EditValue = null;

        //        this.newChooseContorQualityTestUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorQualityTestUnit.EditValue = null;

        //        //this.newChooseContorlMainUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        //this.newChooseContorlMainUnit.EditValue = null;

        //        this.newChooseContorProduceUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorProduceUnit.EditValue = null;

        //        this.newChooseContorSellUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorSellUnit.EditValue = null;
        //    }
        //    else
        //    {
        //        this.newChooseContorDepotUnit.Choose = null;
        //        this.newChooseContorDepotUnit.EditValue = null;

        //        this.newChooseContorBuyUnit.Choose = null;
        //        this.newChooseContorBuyUnit.EditValue = null;

        //        this.newChooseContorQualityTestUnit.Choose = null;
        //        this.newChooseContorQualityTestUnit.EditValue = null;

        //        //this.newChooseContorlMainUnit.Choose = null;
        //        //this.newChooseContorlMainUnit.EditValue = null;

        //        this.newChooseContorProduceUnit.Choose = null;
        //        this.newChooseContorProduceUnit.EditValue = null;

        //        this.newChooseContorSellUnit.Choose = null;
        //        this.newChooseContorSellUnit.EditValue = null;
        //    }
        //}

        //private void gridView2_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    if (this.gridView2.FocusedColumn == this.gridColumnUnit) 
        //    {
        //        Model.CustomerProductsBom bom = this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as Model.CustomerProductsBom;

        //        if (bom == null) return;

        //        Model.Product product = bom.Product;

        //        if (product == null)
        //            return;

        //        IList<Model.ProductUnit> units = this.productUnitManager.Select(product.BasedUnitGroup);

        //        foreach (Model.ProductUnit unit in units)
        //        {
        //            this.repositoryItemComboBox2.Items.Add(unit);
        //        }
        //    }
        //}

        //private void simpleButton2_Click_1(object sender, EventArgs e)
        //{
        //   // Model.Customer customer = new Book.Model.Customer();
        //   // if (this.newChooseContorlCustomer.EditValue != null)
        //    {

        //       //Settings.BasicData.PackageType.ChoosePackageProductsForm f = new Settings.BasicData.PackageType.ChoosePackageProductsForm();
        //        Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();
        //        if (f.ShowDialog(this) == DialogResult.OK)
        //        {
        //            Model.Product p = f.SelectedItem as Model.Product;
        //            Model.PackageCustomerDetails detail = new Book.Model.PackageCustomerDetails();

        //            // detail.PackageType = (f.SelectedItem as Model.PackageDetails).PackageType;
        //            //detail.PackageTypeId = (f.SelectedItem as Model.PackageDetails).PackageTypeId;
        //            //detail.PackageCustomerDetailsId = (f.SelectedItem as Model.PackageDetails).PackageCustomerDetailsId;
        //            detail.Product = f.SelectedItem as Model.Product;
        //            detail.ProductId = (f.SelectedItem as Model.Product).ProductId;

        //           // detail.Product = (f.SelectedItem as Model.PackageDetails).Product;
        //            detail.Quantity = 0; //(f.SelectedItem as Model.PackageDetails).Quantity;
        //            detail.ConsumeRate = 0;
        //            detail.EffectsDate = DateTime.Now;
        //            detail.ExpiringDate = DateTime.Now.AddYears(1000);
        //           // detail.Description = (f.SelectedItem as Model.PackageDetails).Description;
        //            _customerProduct.PackageCustomerDetails.Add(detail);

        //            this.bindingSourcePackageProduct.Position = this.bindingSourcePackageProduct.IndexOf(detail);
        //            this.bindingSourcePackageProdtuc.DataSource = _customerProduct.PackageCustomerDetails;
        //            this.gridControl3.RefreshDataScoure();
        //        }
        //    }
        //}

        //private void simpleButton3_Click(object sender, EventArgs e)
        //{
        //    if (this.bindingSourcePackageProduct.Current != null)
        //    {
        //        this._customerProduct.PackageCustomerDetails.Remove(this.bindingSourcePackageProduct.Current as Book.Model.PackageCustomerDetails);

        //        this.gridControl3.RefreshDataSource();
        //    }

        //}

        //private void simpleButton4_Click(object sender, EventArgs e)
        //{
        //Settings.BasicData.Customs.Processing.EditForm f = new Book.UI.Settings.BasicData.Customs.Processing.EditForm(this._customerProduct.Customer);
        //if (f.ShowDialog() != DialogResult.OK)
        //{
        //    return;
        //}
        // }

        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            this._customerProduct.PrimaryKeyId = (this.bindingSource1.Current as Model.CustomerProducts).PrimaryKeyId;
            this.action = "view";
            this.Refresh();
        }

        private void lookUpBasedUnitGroupId_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpBasedUnitGroupId.EditValue != null)
                this.bindingSourceUnit.DataSource = this.productUnitManager.Select(this.lookUpBasedUnitGroupId.EditValue.ToString());
        }

        //选取设置箱子
        private void btn_SetBox_Click(object sender, EventArgs e)
        {
            Invoices.ZX.ListForm f = new Book.UI.Invoices.ZX.ListForm();
            if (DialogResult.OK == f.ShowDialog(this))
            {
                this._customerProduct.Setting = f.SelectItem as Model.Setting;
                if (this._customerProduct.Setting != null)
                {
                    this._customerProduct.SettingId = this._customerProduct.Setting.SettingId;
                    this.spinBoxLong.EditValue = this._customerProduct.Setting.Blong;
                    this.spinBoxWidth.EditValue = this._customerProduct.Setting.BWidth;
                    this.spinBoxHeight.EditValue = this._customerProduct.Setting.BHeight;
                    this.spinBoxJWeight.EditValue = this._customerProduct.Setting.BJWeight;
                    this.spinBoxMWeight.EditValue = this._customerProduct.Setting.BMWeight;
                    this.spinBoxCaiJi.EditValue = this._customerProduct.Setting.BCaiJi;
                }
            }
        }

        private void barExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CustomerProductExportCondition f = new CustomerProductExportCondition();
            f.ShowDialog(this);
        }

        // bool isck = false;
        //private void gridView1_ShowingEditor_1(object sender, CancelEventArgs e)
        //{
        //    object value = this.gridView1.GetRowCellValue(this.gridView1.FocusedRowHandle, this.gridColumnName);
        //    //if (ck == null) return;
        //    //bool.TryParse(ck.ToString(), out isck);
        //    if (this.gridView1.FocusedColumn == this.gridColumnProcess)
        //    {
        //        //if (isck)
        //        //{
        //            this.repositoryItemComboBox1.Items.Clear();
        //            this.gridView1.SetRowCellValue(this.gridView1.FocusedRowHandle, this.gridColumnProcess, null);
        //            Model.CustomerProductProcess customerProductProcess = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Model.CustomerProductProcess;
        //            if (customerProductProcess == null) return;
        //            if (value != null)
        //                customerProductProcess.ProcessCategory = this.processCategoryManager.Get(value.ToString());
        //            if (customerProductProcess.ProcessCategory == null) return;

        //            IList<Model.Processing> processings = this.processingManager.Select(customerProductProcess.ProcessCategory);

        //            this.repositoryItemComboBox1.Items.Clear();
        //            foreach (Model.Processing processing in processings)
        //            {
        //                this.repositoryItemComboBox1.Items.Add(processing);
        //            }
        //        //}
        //        //else
        //        //{

        //        //}
        //    }
        //}
    }
}