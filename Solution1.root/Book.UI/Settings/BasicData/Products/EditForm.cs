using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Xml;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.BarCode;
using DevExpress.XtraTreeList.Nodes;
using Book.UI.Settings.ProduceManager.Techonlogy;
using System.Data.SqlClient;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Linq;

namespace Book.UI.Settings.BasicData.Products
{
    /*----------------------------------------------------------------
   // Copyright (C) 2008 - 2010  咸 陽飛 馳軟件有限公司
   //                     版權所有 圍著必究
   // 功能描述: 商品设置
   // 文 件 名：EditForm
   // 编 码 人: 茍波濤                   完成时间:2009-10-28
   // 修改原因：添加了商品的图档1，图档二,图档三
   // 修 改 人: 刘永亮                   修改时间:2010-08-27
   //----------------------------------------------------------------*/
    public partial class EditForm : BaseEditForm
    {
        #region 变量对象定义
        public static IList<Model.ProductProcess> _ProductProcess = new List<Model.ProductProcess>();
        public static IList<Model.Procedures> _proceduresStatic = new List<Model.Procedures>();
        /// <summary>
        /// 商品类别管理
        /// </summary>
        private Book.BL.ProductCategoryManager productCategoryManager = new Book.BL.ProductCategoryManager();
        /// <summary>
        /// 商品管理
        /// </summary>
        private Book.BL.ProductManager productManager = new Book.BL.ProductManager();
        private BL.InvoiceCODetailManager invoiceCoDetailManager = new BL.InvoiceCODetailManager();
        private Book.Model.Product product = null;
        private BL.StockManager stockManager = new Book.BL.StockManager();
        private BL.CompanyLevelManager companyLevelManager = new Book.BL.CompanyLevelManager();
        private BL.ProductUnitManager productUnitManager = new Book.BL.ProductUnitManager();
        private Model.Customer _customer = new Book.Model.Customer();
        //  private BL.ProcessingManager processingManager = new Book.BL.ProcessingManager();
        private BL.ProcessCategoryManager processCategoryManager = new Book.BL.ProcessCategoryManager();
        private BL.ProductProcessManager productProcessManager = new Book.BL.ProductProcessManager();
        private BL.UnitGroupManager UnitGroupManager = new Book.BL.UnitGroupManager();
        //  private Model.CustomerProcessing _customerProcessing = new Book.Model.CustomerProcessing();
        private BL.TechnologydetailsManager _technologydetailsManager = new Book.BL.TechnologydetailsManager();
        private BL.ProductMouldDetailManager productMouldDetailManager = new Book.BL.ProductMouldDetailManager();
        private BL.InvoiceXODetailManager invoiceXoDetailManager = new BL.InvoiceXODetailManager();
        private BL.ProductImageManager productImageManager = new BL.ProductImageManager();
        private Model.ProductImage productImage;
        private BL.TechnologydetailsManager technologydetailsManager = new BL.TechnologydetailsManager();
        private IList<Model.Product> _productList = new List<Model.Product>();
        private BL.MaterialManager materialManager = new Book.BL.MaterialManager();
        //原料编号
        private string materialIds;
        //原料数量
        private string materialNum;
        private int idIsChange = 0;
        /// <summary>
        /// 构造函数——添加
        /// </summary>

        private Model.ProductCategory cateTemp = null;
        private string productCategory = null;
        //标致是否是保存后 执行 TREELIST事件的

        #endregion

        public EditForm()
        {

            InitializeComponent();

            this.requireValueExceptions.Add(Model.Product.PRO_ProductName, new AA("请输入商品名稱。", this.textEditProductName));
            this.requireValueExceptions.Add(Model.Product.PRO_Id, new AA(Properties.Resources.RequireDataForId, this.textEditId));
            this.requireValueExceptions.Add(Model.Product.PRO_BasedUnitGroupId, new AA(Properties.Resources.RequireDataForBaseUnitGroup, this.lookUpBasedUnitGroupId));
            this.requireValueExceptions.Add(Model.Product.PRO_DepotUnitId, new AA(Properties.Resources.RequireDataForDepotUnit, this.lookUpDepotUnit));
            this.requireValueExceptions.Add(Model.Product.PRO_ProductCategoryId, new AA(Properties.Resources.ProductCategorys, this.newChooseContorlProductType));


            this.invalidValueExceptions.Add(Model.Product.PRO_ProductCategoryId, new AA(Properties.Resources.EntityExists, this.textEditId));
            this.invalidValueExceptions.Add(Model.Product.PRO_ProductName, new AA(Properties.Resources.NameIsExists, this.textEditProductName));
            //this.invalidValueExceptions.Add("ProName", new AA(Properties.Resources.NameIsExists, this.buttonEditTechonProcedures));
            // this.invalidValueExceptions.Add("ProId", new AA(Properties.Resources.RequireIdName, this.textEditProceduresId));

            this.newChooseContorSupplierId.Choose = new BasicData.Supplier.ChooseSupplier();
            this.newChooseContorlBuyEmployee.Choose = new BasicData.Employees.ChooseEmployee();
            this.newChooseContorlCustomInspectionRule.Choose = new BasicData.CustomInspectionRule.ChooseCustomInspectionRule();
            this.newChooseContorlQualityTestPlan.Choose = new BasicData.QualityTestPlan.ChooseQualityTestPlan();
            this.nccEmployeeCreator.Choose = new BasicData.Employees.ChooseEmployee();
            this.nccEmployeeChange.Choose = new BasicData.Employees.ChooseEmployee();

            this.newChooseContorlDepot.Choose = new Book.UI.Invoices.ChooseDepot();
            //this.newChooseContorlPackageType.Choose = new BasicData.PackageType.ChoosePackageType();

            this.newChooseContorlVolumeUnitGroup.Choose = new BasicData.UnitGroup.ChooseUnitGroup();
            this.newChooseContorWeightUnitGroup.Choose = new BasicData.UnitGroup.ChooseUnitGroup();
            this.action = "insert";
            this.newChooseContorlCustomer.Choose = new Settings.BasicData.Customs.ChooseCustoms();
            this.dateEditcostartdate.DateTime = System.DateTime.Now.AddMonths(-3);
            this.dateEditcoenddate.DateTime = System.DateTime.Now;
            this.dateEditxostartdate.DateTime = System.DateTime.Now.AddMonths(-3);
            this.dateEditxoenddate.DateTime = System.DateTime.Now;
            this.dateEditStockStart.DateTime = DateTime.Now.Date.AddMonths(-1);
            this.dateEditStockEnd.DateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);

        }

        //一览窗口传递类型
        private int flag1 = 0;
        private int flag5 = 0;//打开窗口
        //private int flag2 = 0;
        //private int flag3 = 0;//refresh() 种 控制band()后不执行tree事件 
        public EditForm(Model.ProductCategory ProductCategory)
            : this()
        {
            this.product = new Book.Model.Product();
            this.product.ProductCategory = ProductCategory;
            // flag1 = 1;

        }

        /// <summary>
        /// 构造函数——修改
        /// </summary>
        /// <param name="prod"></param>
        public EditForm(Book.Model.Product prod)
            : this()
        {
            //if (prod == null)
            //    throw new ArithmeticException();

            // flag2 = 1;
            this.product = prod;
            this.action = "update";
            if (this.product != null)
            {
                if (this.product.ProductCategory != null)
                    productCategory = this.product.ProductCategory.Id;
            }


        }

        /// <summary>
        /// 构造函数——多种形式
        /// </summary>
        /// <param name="prod"></param>
        /// <param name="action"></param>
        public EditForm(Book.Model.Product prod, string action)
            : this()
        {
            //if (category == null)
            //    throw new ArithmeticException();
            this.product = prod;
            this.action = action;
            // flag2 = 1;
            if (this.action == "update" || this.action == "view")
            {

                if (this.product != null)
                {
                    if (this.product.ProductCategory != null)
                        productCategory = this.product.ProductCategory.Id;
                }
            }
        }

        private System.Collections.Generic.IDictionary<string, float> dic = new System.Collections.Generic.Dictionary<string, float>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditForm_Load(object sender, EventArgs e)
        {
            flag5 = 1;

            IList<Model.ProductCategory> listCategory = this.productCategoryManager.SelectAll();
            List<Model.ProductCategory> firstLevel = listCategory.Where(l => l.CategoryLevel == 1).ToList();
            List<Model.ProductCategory> secondLevel = listCategory.Where(l => l.CategoryLevel == 2).ToList();
            List<Model.ProductCategory> thirdLevel = listCategory.Where(l => l.CategoryLevel == 3).ToList();

            foreach (Model.ProductCategory first in firstLevel)
            {
                TreeListNode firstNode = this.treeList1.AppendNode(new object[] { first.Id + "-" + first.ProductCategoryName }, null);
                firstNode.Tag = first;

                List<Model.ProductCategory> list2 = secondLevel.Where(l => l.ProductCategoryParentId == first.ProductCategoryId).ToList();
                foreach (Model.ProductCategory second in list2)
                {
                    TreeListNode secondNode = this.treeList1.AppendNode(new object[] { second.Id + "-" + second.ProductCategoryName }, firstNode);
                    secondNode.Tag = second;

                    List<Model.ProductCategory> list3 = thirdLevel.Where(l => l.ProductCategoryParentId == second.ProductCategoryId).ToList();
                    foreach (Model.ProductCategory third in list3)
                    {
                        this.treeList1.AppendNode(new object[] { third.Id + "-" + third.ProductCategoryName }, secondNode).Tag = third;
                    }
                }
            }

            this.Visibles();
            _ProductProcess.Clear();


            this.bindingSourceProcessCategory.DataSource = this.processCategoryManager.Select();
            this.bindingSourceUnitGroup.DataSource = this.UnitGroupManager.Select();
            this.openFileDialog1.Filter = "圖片文件(*.jpg,*.gif,*.bmp)|*.jpg;*.gif;*.bmp";
            this.bindingSourceMaterial.DataSource = this.materialManager.SelectOther();
        }

        //private void band()
        //{

        //    int a = flag2;
        //    this.treeList1.ClearNodes();

        //    foreach (Model.ProductCategory pc in this.productCategoryManager.Select())
        //    {
        //        treeList1.AppendNode(new object[] { pc.Id + " " + pc.ProductCategoryName }, null, pc.ProductCategoryId);
        //    }      

        //    if (treeList1.Nodes.Count > 0)
        //    {
        //        foreach (TreeListNode node in treeList1.Nodes[0].Nodes)
        //        {
        //            if (node.Tag.ToString() == this.product.ProductId)
        //                node.Selected = true;
        //        }
        //    }
        //}

        #region 重写继承方法

        public override object EditedItem
        {
            get
            {
                return this.product;
            }
        }

        int flag4 = 0;

        protected override void Delete()
        {
            if (this.product == null)
                return;
            string id = this.product.ProductId;
            if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            this.productManager.Delete(this.product);

            for (int i = 0; i < gridView5.RowCount; i++)
            {
                if (gridView5.GetRowCellValue(i, this.gridColumn27).ToString() == this.product.ProductId)
                {
                    if (cateTemp.ToString() == this.product.ProductCategory.ToString())
                    {
                        this.gridView5.DeleteRow(i);
                    }
                }
            }



            if (gridView5.RowCount > 0)
            {
                //if (index == gridView5.RowCount)
                //    this.bindingSourceProduct.Position = index - 1;
                //else
                //    this.bindingSourceProduct.Position = index;

                grid5Click();
            }
            else
            {
                this.action = "insert";
                this.AddNew();

            }


            //if (this.product == null)
            //{
            //    this.product = this.productManager.GetLast();
            //}


        }

        private double Parse(string text)
        {
            string d = "0";
            if (!string.IsNullOrEmpty(text))
            {
                d = text;
            }
            return double.Parse(d);
        }

        protected override void Save()
        {
            this.product.Id = this.textEditId.Text;
            // this.treeList1.Nodes.Remove
            //不執行事件treelist
            //删除节点
            //flag = 1;

            //foreach (TreeListNode listNode in this.treeList1.Nodes)
            //{

            //    for (int i = 0; i < listNode.Nodes.Count; i++)
            //    {
            //        if (listNode.Nodes[i].Tag.ToString() == this.product.ProductId && listNode.Nodes[i].ParentNode != null)
            //        {
            //           listNode.Nodes.Remove( listNode.Nodes[i]);
            //            treeList1.AppendNode(new object[] { string.IsNullOrEmpty(this.buttonEditTechonProcedures.Text) ? product.ProductName : this.buttonEditTechonProcedures.Text }, 1, product.ProductId);

            //        }
            //    }
            //}
            //flag = 0;
            this.product.AbcCategory = this.comboBoxEditAbcCategory.Text;

            if (this.lookUpBasedUnitGroupId.EditValue != null)
            {
                Model.UnitGroup unitgroup = this.UnitGroupManager.Get(this.lookUpBasedUnitGroupId.EditValue.ToString());
                if (lookUpBasedUnitGroupId.Text == unitgroup.UnitGroupName)
                    this.product.BasedUnitGroup = unitgroup;
                else
                    if (lookUpBasedUnitGroupId.Text == "")
                    {
                        this.product.BasedUnitGroup = null;
                        lookUpBasedUnitGroupId.EditValue = null;
                    }
                    else
                        this.product.BasedUnitGroup = unitgroup;
            }
            if (this.lookUpBasedUnitGroupId.EditValue == null)
                this.product.BasedUnitGroup = null;
            this.product.BeenAssigned = Parse(this.spinEditBeenAssigned.Text);
            this.product.ProduceMaterialDistributioned = double.Parse(this.calcEditMateriaFenPei.Value.ToString());
            this.product.OtherMaterialDistributioned = double.Parse(this.calcEditMateriaOtherFenPei.Value.ToString());
            this.product.BorrowQuantity = Parse(this.spinEditBorrowQuantity.Text);
            this.product.BuyEmployee = this.newChooseContorlBuyEmployee.EditValue as Model.Employee;
            this.product.CustomerId = this.newChooseContorlCustomer.EditValue == null ? null : (this.newChooseContorlCustomer.EditValue as Model.Customer).CustomerId;
            this.product.Customer = this.newChooseContorlCustomer.EditValue == null ? null : this.newChooseContorlCustomer.EditValue as Model.Customer;

            if (this.lookUpCJUnit.EditValue != null)
            {
                Model.ProductUnit unit = this.productUnitManager.Get(this.lookUpCJUnit.EditValue.ToString());
                if (this.lookUpCJUnit.Text == unit.CnName)
                    this.product.BuyUnit = unit;
                else
                    if (lookUpCJUnit.Text == "")
                    {
                        this.product.BuyUnit = null;
                        lookUpCJUnit.EditValue = null;
                    }
                    else
                        this.product.BuyUnit = unit;
            }
            if (this.lookUpCJUnit.EditValue == null)
                this.product.BuyUnit = null;
            this.product.CheckCategory = this.comboBoxEditCheckCategory.Text;
            this.product.CheckCount = Parse(this.spinEditCheckCount.Text);
            this.product.CheckRate = Parse(this.spinEditCheckRate.Text);
            this.product.CheckRule = this.comboBoxEditCheckRule.Text;
            this.product.Consume = this.checkEditConsume.Checked;
            this.product.CustomInspectionRule = this.newChooseContorlCustomInspectionRule.EditValue as Model.CustomInspectionRule;
            //this.product.CustomInspectionRuleId
            this.product.DamageRate = this.spinEditDamageRate.Value;
            this.product.Depot = this.newChooseContorlDepot.EditValue as Model.Depot;
            if (this.product.Depot != null)
                this.product.DepotId = this.product.Depot.DepotId;
            this.product.DepotPosition = this.newChooseContorlDepotPosition.EditValue as Model.DepotPosition;
            //this.product.DepotPositionId
            this.product.CustomerProductName = this.textEditCustomProductName.Text;

            if (this.lookUpDepotUnit.EditValue != null)
            {
                Model.ProductUnit unit = this.productUnitManager.Get(this.lookUpDepotUnit.EditValue.ToString());
                if (lookUpDepotUnit.Text == unit.CnName)
                    this.product.DepotUnit = unit;
                else
                    if (lookUpDepotUnit.Text == "")
                    {
                        this.product.DepotUnit = null;
                        lookUpDepotUnit.EditValue = null;
                    }
                    else
                        this.product.DepotUnit = unit;
            }
            if (this.lookUpDepotUnit.EditValue == null)
                this.product.DepotUnit = null;
            //if (this.lookUpDepotUnit.EditValue != null)
            //    this.product.DepotUnit = this.productUnitManager.Get(this.lookUpDepotUnit.EditValue.ToString());

            this.product.GrossWeight = Parse(this.spinEditGrossWeight.Text);
            this.product.Height = Parse(this.spinEditHeight.Text);
            this.product.HighestPurchasingPrice = this.spinEditHighestPurchasingPrice.Value;
            this.product.HighestStock = Parse(this.spinEditHighestStock.Text);
            this.product.HomeMade = this.checkEditHomeMade.Checked;
            //this.product.InsteadOfProductId = this.newChooseContorlInsteadOfProduct.Choose.;              
            this.product.LendQuantity = Parse(this.spinEditLendQuantity.Text);
            this.product.Length = Parse(this.spinEditLength.Text);
            this.product.LowestSellPrice = this.spinEditLowestSellPrice.Value;
            this.product.LowestStock = Parse(this.spinEditLowestStock.Text);
            // this.product.MainUnit = this.newChooseContorMainUnitId.EditValue as Model.ProductUnit;
            //this.product.MainUnitId
            this.product.NetWeight = Parse(this.spinEditNetWeight.Text);
            this.product.NewestCost = this.spinEditNewestCost.Value;
            this.product.OnMadingQuantity = Parse(this.spinEditOnMadingQuantity.Text);
            this.product.OrderOnWayQuantity = Parse(this.spinEditOrderOnWayQuantity.Text);
            this.product.OutSourcing = this.checkEditOutSourcing.Checked;
            //  this.product.PackageType = this.newChooseContorlPackageType.EditValue as Model.PackageType;
            //this.product.PackageTypeId
            this.product.PlanSellPrice = this.spinEditPlanSellPrice.Value;
            this.product.PowerGroup = this.textEditPowerGroup.Text;
            this.product.ProduceCounty = this.textEditProduceCounty.Text;
            this.product.ProductProcessDescription = this.memoEditProductDescription.Rtf;
            this.product.AttrZhengMai = this.richTextZhengMai.Rtf;
            this.product.AttrCeMai = this.richTextCeMai.Rtf;
            this.product.IsQiangHua = this.checkEditQiang.Checked;
            this.product.IsFangWu = this.checkEditFang.Checked;
            this.product.IsNoQiangFang = this.checkEditNoQiang.Checked;
            if (this.lookUpProduceUnitId.EditValue != null)
            {
                Model.ProductUnit unit = this.productUnitManager.Get(this.lookUpProduceUnitId.EditValue.ToString());
                if (this.lookUpProduceUnitId.Text == unit.CnName)
                    this.product.ProduceUnit = unit;
                else
                    if (this.lookUpProduceUnitId.Text == "")
                    {
                        this.product.ProduceUnit = null;
                        this.lookUpProduceUnitId.EditValue = null;
                    }
                    else
                        this.product.ProduceUnit = unit;
            }
            if (this.lookUpProduceUnitId.EditValue == null)
                this.product.ProduceUnit = null;
            //if (this.lookUpProduceUnitId.EditValue != null)
            //    this.product.ProduceUnit = this.productUnitManager.Get(this.lookUpProduceUnitId.EditValue.ToString());
            this.product.ProductBarCode = this.textEditProductBarCode.Text;
            this.product.ProductBatch = this.textEditProductBatch.Text;
            this.product.ProductCategory = this.newChooseContorlProductType.EditValue as Model.ProductCategory;
            this.product.ProductCategory2 = this.nccProductCategoryTwo.EditValue as Model.ProductCategory;
            this.product.ProductCategory3 = this.nccProductCategoryThree.EditValue as Model.ProductCategory;
            //this.product.ProductCategoryId            
            this.product.ProductDescription = this.memoEditProductDescription.Rtf;


            this.product.ProductName = this.textEditProductName.Text;
            this.product.ProductPlace = this.textEditProductPlace.Text;
            this.product.ProductSpecification = this.textEditProductSpecification.Text;
            this.product.QualityRequire = this.textEditQualityRequire.Text;
            this.product.QualityTestPlan = newChooseContorlQualityTestPlan.EditValue as Model.QualityTestPlan;
            //this.product.QualityTestPlanId
            if (this.lookUpEditQualityTestUnitId.EditValue != null)
                this.product.QualityTestUnit = this.productUnitManager.Get(this.lookUpEditQualityTestUnitId.EditValue.ToString());
            //this.product.QualityTestUnitId
            this.product.ReferenceCost = this.spinEditReferenceCost.Value;
            this.product.ReferenceSellPrice = this.spinEditReferenceSellPrice.Value;
            this.product.SafeStock = Parse(this.spinEditSafeStock.Text);

            //价格区间存储
            StringBuilder sbPAR = new StringBuilder();
            sbPAR.Append(this.cePriRange1_L.Value.ToString() + "/" + this.cePriRange1_R.Value.ToString() + "/" + this.cePrice1.Value.ToString() + ",");
            sbPAR.Append(this.cePriRange2_L.Value.ToString() + "/" + this.cePriRange2_R.Value.ToString() + "/" + this.cePrice2.Value.ToString() + ",");
            sbPAR.Append(this.cePriRange3_L.Value.ToString() + "/" + this.cePriRange3_R.Value.ToString() + "/" + this.cePrice3.Value.ToString() + ",");
            sbPAR.Append(this.cePriRange4_L.Value.ToString() + "/" + this.cePriRange4_R.Value.ToString() + "/" + this.cePrice4.Value.ToString() + ",");
            sbPAR.Append(this.cePriRange5_L.Value.ToString() + "/" + this.cePriRange5_R.Value.ToString() + "/" + this.cePrice5.Value.ToString() + ",");
            sbPAR.Append(this.cePriRange6_L.Value.ToString() + "/" + this.cePriRange6_R.Value.ToString() + "/" + this.cePrice6.Value.ToString() + ",");
            sbPAR.Append(this.cePriRange7_L.Value.ToString() + "/999999999999/" + this.cePrice7.Value.ToString());
            this.product.PriceAndRange = sbPAR.ToString();
            //销售价格区间存储
            StringBuilder sbPAR1 = new StringBuilder();
            sbPAR1.Append(this.XOcePriRange1_L.Value.ToString() + "/" + this.XOcePriRange1_R.Value.ToString() + "/" + this.XOcePrice1.Value.ToString() + ",");
            sbPAR1.Append(this.XOcePriRange2_L.Value.ToString() + "/" + this.XOcePriRange2_R.Value.ToString() + "/" + this.XOcePrice2.Value.ToString() + ",");
            sbPAR1.Append(this.XOcePriRange3_L.Value.ToString() + "/" + this.XOcePriRange3_R.Value.ToString() + "/" + this.XOcePrice3.Value.ToString() + ",");
            sbPAR1.Append(this.XOcePriRange4_L.Value.ToString() + "/" + this.XOcePriRange4_R.Value.ToString() + "/" + this.XOcePrice4.Value.ToString() + ",");
            sbPAR1.Append(this.XOcePriRange5_L.Value.ToString() + "/" + this.XOcePriRange5_R.Value.ToString() + "/" + this.XOcePrice5.Value.ToString() + ",");
            sbPAR1.Append(this.XOcePriRange6_L.Value.ToString() + "/" + this.XOcePriRange6_R.Value.ToString() + "/" + this.XOcePrice6.Value.ToString() + ",");
            sbPAR1.Append(this.XOcePriRange7_L.Value.ToString() + "/999999999999/" + this.XOcePrice7.Value.ToString());
            this.product.XOPriceAndRange = sbPAR1.ToString();

            if (this.lookUpSellUnit.EditValue != null)
            {
                Model.ProductUnit unit = this.productUnitManager.Get(this.lookUpSellUnit.EditValue.ToString());
                if (this.lookUpSellUnit.Text == unit.CnName)
                    this.product.SellUnit = unit;
                else
                    if (this.lookUpSellUnit.Text == "")
                    {
                        this.product.SellUnit = null;
                        this.lookUpSellUnit.EditValue = null;
                    }
                    else
                        this.product.SellUnit = unit;
            }
            if (this.lookUpSellUnit.EditValue == null)
                this.product.SellUnit = null;
            //if (this.lookUpSellUnit.EditValue != null)
            //    this.product.SellUnit = this.productUnitManager.Get(this.lookUpSellUnit.EditValue.ToString());

            this.product.StocksQuantity = Parse(this.spinEditStocksQuantity.Text);
            this.product.StockTakeCycle = this.comboBoxEditDigital.Text;
            this.product.Supplier = this.newChooseContorSupplierId.EditValue as Model.Supplier;
            //this.product.SupplierId

            this.product.TrustOut = this.checkEditTrustOut.Checked;
            this.product.IsProcee = this.checkEditIsProcess.Checked;
            //this.product.UpdateTime
            this.product.ValuationWay = this.comboBoxEditValuationWay.Text;
            this.product.Volume = Parse(this.spinEditVolume.Text);
            this.product.VolumeUnit = this.newChooseContorVolumeUnit.EditValue as Model.ProductUnit;
            //this.product.VolumeUnitId
            this.product.VolumeUnitGroup = this.newChooseContorlVolumeUnitGroup.EditValue as Model.UnitGroup;
            //this.product.VolumeUnitGroupId
            this.product.WeightUnit = this.newChooseContorWeightUnit.EditValue as Model.ProductUnit;
            //this.product.WeightUnitId
            this.product.WeightUnitGroup = this.newChooseContorWeightUnitGroup.EditValue as Model.UnitGroup;
            //this.product.WeightUnitGroupId
            this.product.Width = Parse(this.spinEditWidth.Text);
            this.product.IsRequiredCheck = checkEditIsRequiredCheck.Checked;
            this.product.IsRequiredCheckCycle = checkEditIsRequiredCheckCycle.Checked;
            this.product.CheckCycle = this.textEditCheckCycle.Text;
            this.product.ProProcessName = this.buttonEditTechonProcedures.Text;
            this.product.ProductBarCodeIsAuto = this.radioGroupBarCode.SelectedIndex == 0 ? false : true;
            this.product.ProductType = this.radioProductType.SelectedIndex;
            //this.product.ProductImage = this.
            //if (this.buttonEditProcessGroup.EditValue != null && !string.IsNullOrEmpty(this.textEdit1.Text))
            //    this.product.IsProcee = true;
            //else
            //    this.product.IsProcee = false;

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditLastStockTakeTime.DateTime, new DateTime()))
            {
                this.product.LastStockTakeTime = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.product.LastStockTakeTime = this.dateEditLastStockTakeTime.DateTime;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProductDeadDate.DateTime, new DateTime()))
            {
                this.product.ProductDeadDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.product.ProductDeadDate = this.dateEditProductDeadDate.DateTime;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProductNearCGDate.DateTime, new DateTime()))
            {
                this.product.ProductNearCGDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.product.ProductNearCGDate = this.dateEditProductNearCGDate.DateTime;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.dateEditProductNearXSDate.DateTime, new DateTime()))
            {
                this.product.ProductNearXSDate = global::Helper.DateTimeParse.NullDate;
            }
            else
            {
                this.product.ProductNearXSDate = this.dateEditProductNearXSDate.DateTime;
            }
            this.product.ProceId = this.textEditProceduresId.Text;

            // this.product.ProductProcess = _ProductProcess;
            //图片

            //string fileName = this.buttonEditProductImage.Text;
            //if (!string.IsNullOrEmpty(fileName))
            //{
            //    if (File.Exists(fileName))
            //    {
            //        this.product.ProductImage = File.ReadAllBytes(fileName);
            //    }
            //}



            ////图档一
            //string pic1 = this.ProductImage1.Text;
            //if (!string.IsNullOrEmpty(pic1))
            //{
            //    if (File.Exists(pic1))
            //    {
            //        this.product.ProductImage1 = File.ReadAllBytes(pic1);
            //    }
            //}

            ////图档二
            //string pic2 = this.ProductImage2.Text;
            //if (!string.IsNullOrEmpty(pic2))
            //{
            //    if (File.Exists(pic2))
            //    {
            //        this.product.ProductImage2 = File.ReadAllBytes(pic2);
            //    }
            //}
            ////图档三
            //string pic3 = this.ProductImage3.Text;
            //if (!string.IsNullOrEmpty(pic3))
            //{
            //    if (File.Exists(pic3))
            //    {
            //        this.product.ProductImage3 = File.ReadAllBytes(pic3);
            //    }
            //}
            //this.product.MaterialIds = this.checkedComboBoxEditJWeight.EditValue == null ? null : this.checkedComboBoxEditJWeight.EditValue.ToString();
            this.product.MaterialIds = string.IsNullOrEmpty(this.materialIds) ? this.product.MaterialIds : this.materialIds;
            this.product.MaterialNum = string.IsNullOrEmpty(this.materialNum) ? this.product.MaterialNum : this.materialNum;

            switch (this.action)
            {
                case "insert":
                    //if (string.IsNullOrEmpty(fileName))
                    //{
                    //    string defaultImage = Application.StartupPath + @"\NoImage.jpg";
                    //    if (File.Exists(defaultImage))
                    //    {
                    //        this.product.ProductImage = File.ReadAllBytes(defaultImage);
                    //    }
                    //    else
                    //    {
                    //        this.product.ProductImage = new byte[] { };
                    //    }
                    //}
                    this.productManager.Insert(this.product);
                    if (cateTemp != null && cateTemp.Equals(this.product.ProductCategory))
                    {
                        string name = (string.IsNullOrEmpty(this.product.ProductVersion) ? this.product.ProductName : this.product.ProductName + "-" + this.product.ProductVersion);




                        DataView da = this.gridView5.DataSource as DataView;
                        DataRow dr = da.Table.NewRow();
                        dr[0] = this.product.ProductId;
                        dr[1] = name;
                        da.Table.Rows.Add(dr);
                        this.gridView5.FocusedRowHandle = gridView5.RowCount - 1;
                        gridView5.RefreshData();

                    }

                    break;
                case "update":
                    this.product.EmployeeChange = BL.V.ActiveOperator.Employee;     //设置变更人
                    this.product.EmployeeChangeId = this.product.EmployeeChange == null ? "" : this.product.EmployeeChange.EmployeeId;
                    this.productManager.Update(this.product);

                    //如果修改商品類型導致商品編號變化，則商品編號編碼增加
                    if (this.idIsChange == 1)
                    {
                        this.idIsChange = 0;
                        string sequencekey = this.product.ProductCategory.Id;
                        Book.BL.SequenceManager.Increment(sequencekey);
                    }

                    for (int i = 0; i < gridView5.RowCount; i++)
                    {
                        if (gridView5.GetRowCellValue(i, this.gridColumn27).ToString() == this.product.ProductId)
                        {
                            if (cateTemp.ToString() == this.product.ProductCategory.ToString())
                            {
                                string cus = "";
                                if (this.product.IsCustomerProduct == true)
                                    cus = "{" + this.product.CustomerProductName + "}";


                                string name = (string.IsNullOrEmpty(this.product.ProductVersion) ? this.product.ProductName : this.product.ProductName + cus + "-" + this.product.ProductVersion);

                                this.gridView5.SetRowCellValue(i, this.gridColumn26, name);
                            }

                        }

                    }
                    this.gridControl5.RefreshDataSource();
                    //if (this.bindingSourceProduct.Position >= 0 &&cateTemp!=null&& cateTemp.ToString() == this.product.ProductCategory.ToString() && this.gridView5.GetRowCellValue(this.bindingSourceProduct.Position, this.gridColumn27).ToString()==this.product.ProductId)
                    //{
                    //    string name = (string.IsNullOrEmpty(this.product.ProductVersion) ? this.product.ProductName : this.product.ProductName + "-" + this.product.ProductVersion);
                    //    this.gridView5.SetRowCellValue(this.bindingSourceProduct.Position, this.gridColumn26, name);
                    //}
                    break;
                default:
                    break;
            }
            // band();
        }

        protected override void AddNew()
        {
            if (cateTemp != null && cateTemp.CategoryLevel == 1)
            {
                this.product = new Model.Product();
                this.product.ProductCategory = cateTemp;
                this.textEditId.Text = this.productManager.GetNewId(this.product.ProductCategory);

                flag1 = 1;
            }
            else
            {
                this.product = new Model.Product();
            }
            this.product.EmployeeCreator = BL.V.ActiveOperator.Employee;
            this.product.EmployeeCreatorId = this.product.EmployeeCreator == null ? null : this.product.EmployeeCreator.EmployeeId;
        }

        protected override void MoveNext()
        {
            Model.Product product = this.productManager.GetNext(this.product);
            if (product == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.product = product;
        }

        protected override void MovePrev()
        {
            Model.Product product = this.productManager.GetPrev(this.product);
            if (product == null)
                throw new InvalidOperationException(Properties.Resources.ErrorNoMoreRows);

            this.product = product;
        }

        protected override void MoveFirst()
        {
            this.product = this.productManager.GetFirst();
        }

        protected override void MoveLast()
        {
            if (this.product == null)
            {
                this.product = this.productManager.GetLast();
            }

        }

        public override void Refresh()
        {

            //this.treeList1.ClearNodes();
            //foreach (Model.ProductCategory cate in productCategoryManager.Select())
            //{

            //    DevExpress.XtraTreeList.Nodes.TreeListNode treeNode = treeList1.AppendNode(new object[] { cate.ProductCategoryName }, null, cate.ProductCategoryId);
            //    foreach (Model.Product prodcut in productManager.SelectProductByProductCategoryId(cate))
            //    {
            //        treeList1.AppendNode(new object[] { prodcut.IsCustomerProduct == true ? prodcut.ProductName + "{" + prodcut.CustomerProductName + "}" : prodcut.ProductName }, treeNode, prodcut.ProductId);
            //    }
            //}
            // this.treeListColumn1.s
            EditForm._ProductProcess.Clear();


            if (this.product == null)
            {
                // this.product = new Book.Model.Product();
                this.action = "insert";
                this.AddNew();
            }
            else
            {
                if (this.action != "insert")
                {
                    this.product.ProductMouldDetail = productMouldDetailManager.Select(this.product);
                }
            }
            //}
            //if (this._customer != null)
            //    this._customer.CustomerId = "e4247f71-4f91-49af-a543-150f8ed65654";

            if (this.action == "view")
            {
                if (this.product.BasedUnitGroup != null)
                {
                    this.bindingSourceUnit.DataSource = this.productUnitManager.Select(this.product.BasedUnitGroupId);
                    this.lookUpBasedUnitGroupId.EditValue = this.product.BasedUnitGroup.UnitGroupId;

                    //this.newChooseContorlDepotUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this.product.BasedUnitGroup.UnitGroupId);
                    //this.newChooseContorlBuyUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(this.product.BasedUnitGroup.UnitGroupId);
                    //this.newChooseContorlQualityTestUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(this.product.BasedUnitGroup.UnitGroupId);
                    ////this.newChooseContorMainUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(this.product.BasedUnitGroup.UnitGroupId);
                    //this.newChooseContorProduceUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(this.product.BasedUnitGroup.UnitGroupId);
                    //this.newChooseContorSellUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(this.product.BasedUnitGroup.UnitGroupId);
                }
                if (this.product.VolumeUnitGroup != null)
                {
                    this.newChooseContorVolumeUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this.product.VolumeUnitGroup.UnitGroupId);
                }
                if (this.product.WeightUnitGroup != null)
                {
                    this.newChooseContorWeightUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(this.product.WeightUnitGroup.UnitGroupId);
                }
                if (this.product.Depot != null)
                {
                    this.newChooseContorlDepotPosition.Choose = new Book.UI.Invoices.ChooseDepotPosition(this.product.Depot);
                }

            }



            //{
            //    this.textEditId.Text = this.productManager.GetNewId(this.product.ProductCategory);


            //}
            //else
            if (flag1 != 1)
            {
                this.textEditId.Text = (string.IsNullOrEmpty(this.product.Id) ? "" : this.product.Id);
            }
            flag1 = 0;
            this.calcEditMateriaFenPei.Text = this.product.ProduceMaterialDistributioned.HasValue ? this.product.ProduceMaterialDistributioned.Value.ToString() : "0";
            this.calcEditMateriaOtherFenPei.Text = this.product.OtherMaterialDistributioned.HasValue ? this.product.OtherMaterialDistributioned.Value.ToString() : "0";
            this.textEditProceduresId.Text = string.Empty;
            this.radioGroupBarCode.SelectedIndex = this.product.ProductBarCodeIsAuto == true ? 1 : 0;
            //this.newChooseContorlBasedUnitGroupId.EditValue = this.product.BasedUnitGroup;
            this.newChooseContorlDepot.EditValue = this.product.Depot;
            this.newChooseContorlDepotPosition.EditValue = this.product.DepotPosition;

            this.newChooseContorlVolumeUnitGroup.EditValue = this.product.VolumeUnitGroup;
            this.newChooseContorVolumeUnit.EditValue = this.product.VolumeUnit;

            this.newChooseContorWeightUnitGroup.EditValue = this.product.WeightUnitGroup;
            this.newChooseContorWeightUnit.EditValue = this.product.WeightUnit;

            this.comboBoxEditAbcCategory.Text = this.product.AbcCategory;

            this.spinEditBeenAssigned.EditValue = this.product.BeenAssigned;
            this.spinEditBorrowQuantity.EditValue = this.product.BorrowQuantity;
            this.newChooseContorlBuyEmployee.EditValue = this.product.BuyEmployee;
            if (this.product.BuyUnit != null)
                this.lookUpCJUnit.EditValue = this.product.BuyUnit.ProductUnitId;
            this.comboBoxEditCheckCategory.Text = this.product.CheckCategory;
            this.spinEditCheckCount.EditValue = this.product.CheckCount;
            this.spinEditCheckRate.EditValue = this.product.CheckRate;
            this.comboBoxEditCheckRule.Text = this.product.CheckRule;
            this.checkEditConsume.EditValue = this.product.Consume;
            this.newChooseContorlCustomInspectionRule.EditValue = this.product.CustomInspectionRule;
            this.spinEditDamageRate.EditValue = this.product.DamageRate;
            if (this.product.DepotUnit != null)
                this.lookUpDepotUnit.EditValue = this.product.DepotUnit.ProductUnitId;
            //this.newChooseContorlEmployeeChange.EditValue = this.product.EmployeeChange;
            //this.newChooseContorlEmployeeCreator.EditValue = this.product.EmployeeCreator;
            this.spinEditGrossWeight.EditValue = this.product.GrossWeight;
            this.spinEditHeight.EditValue = this.product.Height;
            this.spinEditHighestPurchasingPrice.EditValue = this.product.HighestPurchasingPrice;
            this.spinEditHighestStock.EditValue = this.product.HighestStock;
            this.checkEditHomeMade.EditValue = this.product.HomeMade;
            //this.newChooseContorlInsteadOfProduct.EditValue = this.product.InsteadOfProduct;
            this.spinEditLendQuantity.EditValue = this.product.LendQuantity;
            this.spinEditLength.EditValue = this.product.Length;
            this.spinEditLowestSellPrice.EditValue = this.product.LowestSellPrice;
            this.spinEditLowestStock.EditValue = this.product.LowestStock;
            // this.newChooseContorMainUnitId.EditValue = this.product.MainUnit;
            this.spinEditNetWeight.EditValue = this.product.NetWeight;
            this.spinEditNewestCost.EditValue = this.product.NewestCost;
            this.spinEditOnMadingQuantity.EditValue = this.product.OnMadingQuantity;
            this.spinEditOrderOnWayQuantity.EditValue = this.product.OrderOnWayQuantity;
            this.checkEditOutSourcing.EditValue = this.product.OutSourcing;
            // this.newChooseContorlPackageType.EditValue = this.product.PackageType;
            this.spinEditPlanSellPrice.EditValue = this.product.PlanSellPrice;
            this.textEditPowerGroup.Text = this.product.PowerGroup;
            this.textEditProduceCounty.Text = this.product.ProduceCounty;
            if (this.product.ProduceUnit != null)
                this.lookUpProduceUnitId.EditValue = this.product.ProduceUnit.ProductUnitId;
            this.textEditProductBarCode.Text = this.product.ProductBarCode;
            this.textEditProductBatch.Text = this.product.ProductBatch;
            this.newChooseContorlProductType.EditValue = this.product.ProductCategory;
            this.nccProductCategoryTwo.EditValue = this.product.ProductCategory2;
            this.nccProductCategoryThree.EditValue = this.product.ProductCategory3;
            this.memoEditProductDescription.Rtf = this.product.ProductDescription;
            this.richTextZhengMai.Rtf = this.product.AttrZhengMai;
            this.richTextCeMai.Rtf = this.product.AttrCeMai;

            this.textEditProductName.Text = this.product.ProductName;
            this.textEditProductPlace.Text = this.product.ProductPlace;
            this.textEditProductSpecification.Text = this.product.ProductSpecification;
            this.textEditQualityRequire.Text = this.product.QualityRequire;
            this.newChooseContorlQualityTestPlan.EditValue = this.product.QualityTestPlan;

            if (this.product.QualityTestUnit != null)
                this.lookUpEditQualityTestUnitId.EditValue = this.product.QualityTestUnit.ProductUnitId;
            this.spinEditReferenceCost.EditValue = this.product.ReferenceCost;
            this.spinEditReferenceSellPrice.EditValue = this.product.ReferenceSellPrice;
            this.spinEditSafeStock.EditValue = this.product.SafeStock;

            if (this.product.SellUnit != null)
                this.lookUpSellUnit.EditValue = this.product.SellUnit.ProductUnitId;
            this.spinEditStocksQuantity.EditValue = this.product.StocksQuantity;
            this.comboBoxEditDigital.Text = this.product.StockTakeCycle;
            this.newChooseContorSupplierId.EditValue = this.product.Supplier;

            this.checkEditTrustOut.EditValue = this.product.TrustOut;
            this.checkEditIsProcess.EditValue = this.product.IsProcee;
            this.comboBoxEditValuationWay.Text = this.product.ValuationWay;
            this.spinEditVolume.EditValue = this.product.Volume;

            this.radioProductType.SelectedIndex = this.product.ProductType == null ? 0 : this.product.ProductType.Value;

            this.spinEditWidth.EditValue = this.product.Width;
            this.checkEditIsRequiredCheck.EditValue = this.product.IsRequiredCheck;
            this.checkEditIsRequiredCheckCycle.EditValue = this.product.IsRequiredCheckCycle;
            this.textEditCheckCycle.Text = this.product.CheckCycle;
            this.textEditVersion.Text = this.product.ProductVersion;



            this.checkEditQiang.Checked = this.product.IsQiangHua.HasValue ? this.product.IsQiangHua.Value : false;
            this.checkEditFang.Checked = this.product.IsFangWu.HasValue ? this.product.IsFangWu.Value : false;
            this.checkEditNoQiang.Checked = this.product.IsNoQiangFang.HasValue ? this.product.IsNoQiangFang.Value : false;

            //显示原料重量
            if (this.product.MaterialIds != null && this.product.MaterialNum != null)
            {
                //string s = "(";
                string[] materialnums = this.product.MaterialNum.Split(',');
                string[] materialIds = this.product.MaterialIds.Split(',');
                //foreach (string str in this.product.MaterialIds.Split(','))
                //{
                //    s += "'" + str.Trim() + "',";
                //}
                //s = s.Substring(0, s.Length - 1) + ")";
                //IList<string> list = this.materialManager.SelectIdByMaterialId(s);
                string s = string.Empty;
                //foreach (string str in list)
                //{
                //    s += str + "*" + materialnums[list.IndexOf(str)] + "+";
                //}
                foreach (var item in materialIds)
                {
                    s += this.materialManager.SelectIdByPrimary(item) + "*" + materialnums[materialIds.ToList().IndexOf(item)] + "+";
                }
                this.buttonEdit1.EditValue = s == string.Empty ? null : s.Substring(0, s.Length - 1);
            }
            else
                this.buttonEdit1.EditValue = null;
            //this.checkedComboBoxEditJWeight.EditValue = this.product.MaterialIds;
            //this.checkedComboBoxEditJWeight.RefreshEditValue();
            //价格区间显示
            //this.cePriRange1_L.EditValue = this.product.PriRange1 != null ? this.product.PriRange1.Split('/')[0].ToString() : "0";
            //this.cePriRange1_R.EditValue = this.product.PriRange1 != null ? this.product.PriRange1.Split('/')[1].ToString() : "0";
            //this.cePriRange2_L.EditValue = this.product.PriRange2 != null ? this.product.PriRange2.Split('/')[0].ToString() : "0";
            //this.cePriRange2_R.EditValue = this.product.PriRange2 != null ? this.product.PriRange2.Split('/')[1].ToString() : "0";
            //this.cePriRange3_L.EditValue = this.product.PriRange3 != null ? this.product.PriRange3.Split('/')[0].ToString() : "0";
            //this.cePrice1.EditValue = this.product.Price1 != null ? this.product.Price1.Value : 0;
            //this.cePrice2.EditValue = this.product.Price2 != null ? this.product.Price2.Value : 0;
            //this.cePrice3.EditValue = this.product.Price3 != null ? this.product.Price3.Value : 0;
            string[] PriAndRange = (this.product.PriceAndRange != null && this.product.PriceAndRange != "") ? this.product.PriceAndRange.Split(',') : null;
            if (PriAndRange != null)
            {
                this.cePriRange1_L.EditValue = (PriAndRange[0] != null && PriAndRange[0] != "") ? PriAndRange[0].Split('/')[0] : "0";
                this.cePriRange1_R.EditValue = (PriAndRange[0] != null && PriAndRange[0] != "") ? PriAndRange[0].Split('/')[1] : "0";
                this.cePrice1.EditValue = (PriAndRange[0] != null && PriAndRange[0] != "") ? PriAndRange[0].Split('/')[2] : "0";
                this.cePriRange2_L.EditValue = (PriAndRange[1] != null && PriAndRange[1] != "") ? PriAndRange[1].Split('/')[0] : "0";
                this.cePriRange2_R.EditValue = (PriAndRange[1] != null && PriAndRange[1] != "") ? PriAndRange[1].Split('/')[1] : "0";
                this.cePrice2.EditValue = (PriAndRange[1] != null && PriAndRange[1] != "") ? PriAndRange[1].Split('/')[2] : "0";
                this.cePriRange3_L.EditValue = (PriAndRange[2] != null && PriAndRange[2] != "") ? PriAndRange[2].Split('/')[0] : "0";
                this.cePriRange3_R.EditValue = (PriAndRange[2] != null && PriAndRange[2] != "") ? PriAndRange[2].Split('/')[1] : "0";
                this.cePrice3.EditValue = (PriAndRange[2] != null && PriAndRange[2] != "") ? PriAndRange[2].Split('/')[2] : "0";
                this.cePriRange4_L.EditValue = (PriAndRange[3] != null && PriAndRange[3] != "") ? PriAndRange[3].Split('/')[0] : "0";
                this.cePriRange4_R.EditValue = (PriAndRange[3] != null && PriAndRange[3] != "") ? PriAndRange[3].Split('/')[1] : "0";
                this.cePrice4.EditValue = (PriAndRange[3] != null && PriAndRange[3] != "") ? PriAndRange[3].Split('/')[2] : "0";
                this.cePriRange5_L.EditValue = (PriAndRange[4] != null && PriAndRange[4] != "") ? PriAndRange[4].Split('/')[0] : "0";
                this.cePriRange5_R.EditValue = (PriAndRange[4] != null && PriAndRange[4] != "") ? PriAndRange[4].Split('/')[1] : "0";
                this.cePrice5.EditValue = (PriAndRange[4] != null && PriAndRange[4] != "") ? PriAndRange[4].Split('/')[2] : "0";
                this.cePriRange6_L.EditValue = (PriAndRange[5] != null && PriAndRange[5] != "") ? PriAndRange[5].Split('/')[0] : "0";
                this.cePriRange6_R.EditValue = (PriAndRange[5] != null && PriAndRange[5] != "") ? PriAndRange[5].Split('/')[1] : "0";
                this.cePrice6.EditValue = (PriAndRange[5] != null && PriAndRange[5] != "") ? PriAndRange[5].Split('/')[2] : "0";
                this.cePriRange7_L.EditValue = (PriAndRange[6] != null && PriAndRange[6] != "") ? PriAndRange[6].Split('/')[0] : "0";
                this.cePrice7.EditValue = (PriAndRange[6] != null && PriAndRange[6] != "") ? PriAndRange[6].Split('/')[2] : "0";
            }
            else
            {
                this.cePriRange1_L.EditValue = "0";
                this.cePriRange1_R.EditValue = "0";
                this.cePrice1.EditValue = "0";
                this.cePriRange2_L.EditValue = "0";
                this.cePriRange2_R.EditValue = "0";
                this.cePrice2.EditValue = "0";
                this.cePriRange3_L.EditValue = "0";
                this.cePriRange3_R.EditValue = "0";
                this.cePrice3.EditValue = "0";
                this.cePriRange4_L.EditValue = "0";
                this.cePriRange4_R.EditValue = "0";
                this.cePrice4.EditValue = "0";
                this.cePriRange5_L.EditValue = "0";
                this.cePriRange5_R.EditValue = "0";
                this.cePrice5.EditValue = "0";
                this.cePriRange6_L.EditValue = "0";
                this.cePriRange6_R.EditValue = "0";
                this.cePrice6.EditValue = "0";
                this.cePriRange7_L.EditValue = "0";
                this.cePrice7.EditValue = "0";
            }
            string[] XOPriAndRange = (this.product.XOPriceAndRange != null && this.product.XOPriceAndRange != "") ? this.product.XOPriceAndRange.Split(',') : null;
            if (XOPriAndRange != null)
            {
                this.XOcePriRange1_L.EditValue = (XOPriAndRange[0] != null && XOPriAndRange[0] != "") ? XOPriAndRange[0].Split('/')[0] : "0";
                this.XOcePriRange1_R.EditValue = (XOPriAndRange[0] != null && XOPriAndRange[0] != "") ? XOPriAndRange[0].Split('/')[1] : "0";
                this.XOcePrice1.EditValue = (XOPriAndRange[0] != null && XOPriAndRange[0] != "") ? XOPriAndRange[0].Split('/')[2] : "0";
                this.XOcePriRange2_L.EditValue = (XOPriAndRange[1] != null && XOPriAndRange[1] != "") ? XOPriAndRange[1].Split('/')[0] : "0";
                this.XOcePriRange2_R.EditValue = (XOPriAndRange[1] != null && XOPriAndRange[1] != "") ? XOPriAndRange[1].Split('/')[1] : "0";
                this.XOcePrice2.EditValue = (XOPriAndRange[1] != null && XOPriAndRange[1] != "") ? XOPriAndRange[1].Split('/')[2] : "0";
                this.XOcePriRange3_L.EditValue = (XOPriAndRange[2] != null && XOPriAndRange[2] != "") ? XOPriAndRange[2].Split('/')[0] : "0";
                this.XOcePriRange3_R.EditValue = (XOPriAndRange[2] != null && XOPriAndRange[2] != "") ? XOPriAndRange[2].Split('/')[1] : "0";
                this.XOcePrice3.EditValue = (XOPriAndRange[2] != null && XOPriAndRange[2] != "") ? XOPriAndRange[2].Split('/')[2] : "0";
                this.XOcePriRange4_L.EditValue = (XOPriAndRange[3] != null && XOPriAndRange[3] != "") ? XOPriAndRange[3].Split('/')[0] : "0";
                this.XOcePriRange4_R.EditValue = (XOPriAndRange[3] != null && XOPriAndRange[3] != "") ? XOPriAndRange[3].Split('/')[1] : "0";
                this.XOcePrice4.EditValue = (XOPriAndRange[3] != null && XOPriAndRange[3] != "") ? XOPriAndRange[3].Split('/')[2] : "0";
                this.XOcePriRange5_L.EditValue = (XOPriAndRange[4] != null && XOPriAndRange[4] != "") ? XOPriAndRange[4].Split('/')[0] : "0";
                this.XOcePriRange5_R.EditValue = (XOPriAndRange[4] != null && XOPriAndRange[4] != "") ? XOPriAndRange[4].Split('/')[1] : "0";
                this.XOcePrice5.EditValue = (XOPriAndRange[4] != null && XOPriAndRange[4] != "") ? XOPriAndRange[4].Split('/')[2] : "0";
                this.XOcePriRange6_L.EditValue = (XOPriAndRange[5] != null && XOPriAndRange[5] != "") ? XOPriAndRange[5].Split('/')[0] : "0";
                this.XOcePriRange6_R.EditValue = (XOPriAndRange[5] != null && XOPriAndRange[5] != "") ? XOPriAndRange[5].Split('/')[1] : "0";
                this.XOcePrice6.EditValue = (XOPriAndRange[5] != null && XOPriAndRange[5] != "") ? XOPriAndRange[5].Split('/')[2] : "0";
                this.XOcePriRange7_L.EditValue = (XOPriAndRange[6] != null && XOPriAndRange[6] != "") ? XOPriAndRange[6].Split('/')[0] : "0";
                this.XOcePrice7.EditValue = (XOPriAndRange[6] != null && XOPriAndRange[6] != "") ? XOPriAndRange[6].Split('/')[2] : "0";
            }
            else
            {
                this.XOcePriRange1_L.EditValue = "0";
                this.XOcePriRange1_R.EditValue = "0";
                this.XOcePrice1.EditValue = "0";
                this.XOcePriRange2_L.EditValue = "0";
                this.XOcePriRange2_R.EditValue = "0";
                this.XOcePrice2.EditValue = "0";
                this.XOcePriRange3_L.EditValue = "0";
                this.XOcePriRange3_R.EditValue = "0";
                this.XOcePrice3.EditValue = "0";
                this.XOcePriRange4_L.EditValue = "0";
                this.XOcePriRange4_R.EditValue = "0";
                this.XOcePrice4.EditValue = "0";
                this.XOcePriRange5_L.EditValue = "0";
                this.XOcePriRange5_R.EditValue = "0";
                this.XOcePrice5.EditValue = "0";
                this.XOcePriRange6_L.EditValue = "0";
                this.XOcePriRange6_R.EditValue = "0";
                this.XOcePrice6.EditValue = "0";
                this.XOcePriRange7_L.EditValue = "0";
                this.XOcePrice7.EditValue = "0";
            }
            if (global::Helper.DateTimeParse.DateTimeEquls(this.product.LastStockTakeTime, global::Helper.DateTimeParse.NullDate))
                this.dateEditLastStockTakeTime.EditValue = null;
            else
                this.dateEditLastStockTakeTime.EditValue = this.product.LastStockTakeTime;

            if (global::Helper.DateTimeParse.DateTimeEquls(this.product.ProductDeadDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProductDeadDate.EditValue = null;
            }
            else
            {
                this.dateEditProductDeadDate.EditValue = this.product.ProductDeadDate;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.product.ProductNearCGDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProductNearCGDate.EditValue = null;
            }
            else
            {
                this.dateEditProductNearCGDate.EditValue = this.product.ProductNearCGDate;
            }

            if (global::Helper.DateTimeParse.DateTimeEquls(this.product.ProductNearXSDate, global::Helper.DateTimeParse.NullDate))
            {
                this.dateEditProductNearXSDate.EditValue = null;
            }
            else
            {
                this.dateEditProductNearXSDate.EditValue = this.product.ProductNearXSDate;
            }

            #region 默认显示商品图片
            //图片
            //if (this.product.ProductImage != null && this.product.ProductImage.Length > 0)
            //{
            //    MemoryStream ms = new MemoryStream(this.product.ProductImage);
            //    this.pictureEditPrivew.Image = Image.FromStream(ms);
            //    ms.Close();
            //}
            //else
            //    this.pictureEditPrivew.Image = null;

            ////图档一
            //if (this.product.ProductImage1 != null && this.product.ProductImage1.Length > 0)
            //{
            //    MemoryStream ms = new MemoryStream(this.product.ProductImage1);
            //    this.pictureEditPrivew.Image = Image.FromStream(ms);
            //    ms.Close();
            //}
            ////图档二
            //if (this.product.ProductImage2 != null && this.product.ProductImage2.Length > 0)
            //{
            //    MemoryStream ms = new MemoryStream(this.product.ProductImage2);
            //    this.pictureEditPrivew.Image = Image.FromStream(ms);
            //    ms.Close();
            //}

            ////图档三
            //if (this.product.ProductImage3 != null && this.product.ProductImage3.Length > 0)
            //{
            //    MemoryStream ms = new MemoryStream(this.product.ProductImage3);
            //    this.pictureEditPrivew.Image = Image.FromStream(ms);
            //    ms.Close();
            //}
            #endregion


            //if (this.product.ProductProcess.Count==0)
            //{
            //    Model.ProductProcess productProcess = new Book.Model.ProductProcess();
            //    this.product.ProductProcess.Add(productProcess);
            //}
            //this.bindingSourceProductProcess.DataSource = this.product.ProductProcess;
            this.buttonEditTechonProcedures.Text = null;
            //if (this.product.IsProcee ==true)
            //{
            //    this.textEdit1.Text = this.product.ProductName;
            //}

            this.newChooseContorlCustomer.EditValue = this.product.Customer;
            this.newChooseContorlCustomer.Enabled = false;
            this.textEditCustomProductName.EditValue = this.product.CustomerProductName;
            this.textEditCustomProductName.Properties.ReadOnly = true;
            // this.richTextBoxProcess.Rtf=this.product.ProductProcessDescription  ;
            //  this.buttonEditProcessGroup.EditValue = this.product.CustomerProcessing;
            this.bindingSourceProductMouldDetail.DataSource = this.product.ProductMouldDetail;
            //if (this.product.IsProcee == true)
            //{
            //    this.product.ProductProcess = this.productProcessManager.Select(this.product.ProductId);

            //}
            // this.bindingSourceProductProcess.DataSource = this.product.ProductProcess;


            this.nccEmployeeCreator.EditValue = this.product.EmployeeCreator;
            this.nccEmployeeChange.EditValue = this.product.EmployeeChange;
            this.de_UpdateTime.EditValue = this.product.UpdateTime;
            this.calcEditStock0.EditValue = this.stockManager.SelectStockQuantity0(this.product.ProductId);
            this.calcEditStock1.EditValue = this.product.StocksQuantity;
            switch (this.action)
            {
                case "insert":
                    this.richTextZhengMai.ReadOnly = false;
                    this.richTextCeMai.ReadOnly = false;
                    this.barButtonItem3.Enabled = false;
                    this.gridView3.OptionsBehavior.Editable = true;
                    this.buttonEdit1.Enabled = true;
                    if (this.product.IsCustomerProduct == null || !(bool)this.product.IsCustomerProduct)
                    {
                        this.newChooseContorlProductType.Properties.Buttons[0].Enabled = true;
                        this.newChooseContorlProductType.Properties.ReadOnly = false;

                        this.comboBoxEditAbcCategory.Properties.ReadOnly = false;

                        //this.newChooseContorlBasedUnitGroupId.ShowButton = true;
                        //this.newChooseContorlBasedUnitGroupId.ButtonReadOnly = false;

                        this.spinEditBeenAssigned.Properties.ReadOnly = false;
                        this.spinEditBorrowQuantity.Properties.ReadOnly = false;

                        this.newChooseContorlBuyEmployee.ShowButton = true;
                        this.newChooseContorlBuyEmployee.ButtonReadOnly = false;

                        //this.newChooseContorlBuyUnitId.ShowButton = true;
                        //this.newChooseContorlBuyUnitId.ButtonReadOnly = false;
                        this.lookUpBasedUnitGroupId.Properties.ReadOnly = false;
                        this.lookUpCJUnit.Properties.ReadOnly = false;
                        this.lookUpDepotUnit.Properties.ReadOnly = false;
                        this.lookUpEditQualityTestUnitId.Properties.ReadOnly = false;
                        this.lookUpProduceUnitId.Properties.ReadOnly = false;
                        this.lookUpSellUnit.Properties.ReadOnly = false;

                        this.comboBoxEditCheckCategory.Properties.ReadOnly = false;
                        this.spinEditCheckCount.Properties.ReadOnly = false;
                        this.spinEditCheckRate.Properties.ReadOnly = false;
                        this.comboBoxEditCheckRule.Properties.ReadOnly = false;
                        this.checkEditConsume.Properties.ReadOnly = false;

                        this.newChooseContorlCustomInspectionRule.ShowButton = true;
                        this.newChooseContorlCustomInspectionRule.ButtonReadOnly = false;

                        this.spinEditDamageRate.Properties.ReadOnly = false;

                        this.newChooseContorlDepot.ShowButton = true;
                        this.newChooseContorlDepot.ButtonReadOnly = false;

                        this.newChooseContorlDepotPosition.ShowButton = true;
                        this.newChooseContorlDepotPosition.ButtonReadOnly = false;

                        //this.newChooseContorlDepotUnit.ShowButton = true;
                        //this.newChooseContorlDepotUnit.ButtonReadOnly = false;

                        this.newChooseContorlEmployeeChange.ShowButton = true;
                        this.newChooseContorlEmployeeChange.ButtonReadOnly = false;

                        this.newChooseContorlEmployeeCreator.ShowButton = true;
                        this.newChooseContorlEmployeeCreator.ButtonReadOnly = false;

                        this.spinEditGrossWeight.Properties.ReadOnly = false;
                        this.spinEditHeight.Properties.ReadOnly = false;
                        this.spinEditHighestPurchasingPrice.Properties.ReadOnly = false;
                        this.spinEditHighestStock.Properties.ReadOnly = false;
                        this.checkEditHomeMade.Properties.ReadOnly = false;

                        this.newChooseContorlInsteadOfProduct.ShowButton = true;
                        this.newChooseContorlInsteadOfProduct.ButtonReadOnly = false;

                        this.spinEditLendQuantity.Properties.ReadOnly = false;
                        this.spinEditLength.Properties.ReadOnly = false;
                        this.spinEditLowestSellPrice.Properties.ReadOnly = false;
                        this.spinEditLowestStock.Properties.ReadOnly = false;

                        // this.newChooseContorMainUnitId.ShowButton = true;
                        // this.newChooseContorMainUnitId.ButtonReadOnly = false;

                        this.spinEditNetWeight.Properties.ReadOnly = false;
                        this.spinEditNewestCost.Properties.ReadOnly = false;
                        this.spinEditOnMadingQuantity.Properties.ReadOnly = false;
                        this.spinEditOrderOnWayQuantity.Properties.ReadOnly = false;
                        this.checkEditOutSourcing.Properties.ReadOnly = false;

                        this.newChooseContorlPackageType.ShowButton = true;
                        this.newChooseContorlPackageType.ButtonReadOnly = false;

                        this.spinEditPlanSellPrice.Properties.ReadOnly = false;
                        this.textEditPowerGroup.Properties.ReadOnly = false;
                        this.textEditProduceCounty.Properties.ReadOnly = false;

                        //this.newChooseContorProduceUnitId.ShowButton = true;
                        //this.newChooseContorProduceUnitId.ButtonReadOnly = false;

                        this.textEditProductBarCode.Properties.ReadOnly = false;
                        this.textEditProductBatch.Properties.ReadOnly = false;
                        this.memoEditProductDescription.ReadOnly = false;
                        this.textEditId.Properties.ReadOnly = false;
                        this.textEditProductName.Properties.ReadOnly = false;
                        this.textEditProductPlace.Properties.ReadOnly = false;
                        this.textEditProductSpecification.Properties.ReadOnly = false;
                        this.textEditQualityRequire.Properties.ReadOnly = false;

                        this.newChooseContorlQualityTestPlan.ShowButton = true;
                        this.newChooseContorlQualityTestPlan.ButtonReadOnly = false;

                        //this.newChooseContorlQualityTestUnitId.ShowButton = true;
                        //this.newChooseContorlQualityTestUnitId.ButtonReadOnly = false;

                        this.spinEditReferenceCost.Properties.ReadOnly = false;
                        this.spinEditReferenceSellPrice.Properties.ReadOnly = false;
                        this.spinEditSafeStock.Properties.ReadOnly = false;

                        //this.newChooseContorSellUnitId.ShowButton = true;
                        //this.newChooseContorSellUnitId.ButtonReadOnly = false;

                        this.spinEditStocksQuantity.Properties.ReadOnly = false;
                        this.comboBoxEditDigital.Properties.ReadOnly = false;

                        this.newChooseContorSupplierId.ShowButton = true;
                        this.newChooseContorSupplierId.ButtonReadOnly = false;

                        this.checkEditTrustOut.Properties.ReadOnly = false;
                        this.comboBoxEditValuationWay.Properties.ReadOnly = false;
                        this.spinEditVolume.Properties.ReadOnly = false;
                        this.checkEditIsProcess.Properties.ReadOnly = false;

                        this.newChooseContorVolumeUnit.ShowButton = true;
                        this.newChooseContorVolumeUnit.ButtonReadOnly = false;

                        this.newChooseContorlVolumeUnitGroup.ShowButton = true;
                        this.newChooseContorlVolumeUnitGroup.ButtonReadOnly = false;

                        this.newChooseContorWeightUnit.ShowButton = true;
                        this.newChooseContorWeightUnit.ButtonReadOnly = false;

                        this.newChooseContorWeightUnitGroup.ShowButton = true;
                        this.newChooseContorWeightUnitGroup.ButtonReadOnly = false;


                        this.spinEditWidth.Properties.ReadOnly = false;
                        this.checkEditIsRequiredCheck.Properties.ReadOnly = false;
                        this.checkEditIsRequiredCheckCycle.Properties.ReadOnly = false;
                        this.textEditCheckCycle.Properties.ReadOnly = false;

                        this.radioGroupBarCode.Enabled = true;
                        this.comboBoxEditStockCheckTimeParts.Properties.ReadOnly = false;

                        this.dateEditLastStockTakeTime.Properties.ReadOnly = false;
                        this.dateEditLastStockTakeTime.Properties.Buttons[0].Visible = true;

                        this.dateEditProductDeadDate.Properties.ReadOnly = false;
                        this.dateEditProductDeadDate.Properties.Buttons[0].Visible = true;

                        this.dateEditProductNearCGDate.Properties.ReadOnly = false;
                        this.dateEditProductNearCGDate.Properties.Buttons[0].Visible = true;

                        this.dateEditProductNearXSDate.Properties.ReadOnly = false;
                        this.dateEditProductNearXSDate.Properties.Buttons[0].Visible = true;

                        this.dateEditUpdateTime.Properties.ReadOnly = false;
                        this.dateEditUpdateTime.Properties.Buttons[0].Visible = true;

                        //this.textEditProcess.Properties.ReadOnly = true;
                        //this.buttonEditProcessGroup.Properties.ReadOnly = true;
                        //this.buttonEditProcessGroup.Properties.Buttons[0].Enabled = false;

                        //this.checkEditHomeMade.Checked = false;
                        //this.checkEditConsume.Checked = false;
                        //this.checkEditOutSourcing.Checked = false;
                        //this.checkEditTrustOut.Checked = false;

                        //价格区间
                        this.cePrice1.Properties.ReadOnly = false;
                        this.cePrice2.Properties.ReadOnly = false;
                        this.cePrice3.Properties.ReadOnly = false;
                        this.cePrice4.Properties.ReadOnly = false;
                        this.cePrice5.Properties.ReadOnly = false;
                        this.cePrice6.Properties.ReadOnly = false;
                        this.cePrice7.Properties.ReadOnly = false;
                        this.cePriRange1_L.Properties.ReadOnly = false;
                        this.cePriRange1_R.Properties.ReadOnly = false;
                        this.cePriRange2_L.Properties.ReadOnly = false;
                        this.cePriRange2_R.Properties.ReadOnly = false;
                        this.cePriRange3_L.Properties.ReadOnly = false;
                        this.cePriRange3_R.Properties.ReadOnly = false;
                        this.cePriRange4_L.Properties.ReadOnly = false;
                        this.cePriRange4_R.Properties.ReadOnly = false;
                        this.cePriRange5_L.Properties.ReadOnly = false;
                        this.cePriRange5_R.Properties.ReadOnly = false;
                        this.cePriRange6_L.Properties.ReadOnly = false;
                        this.cePriRange6_R.Properties.ReadOnly = false;
                        this.cePriRange7_L.Properties.ReadOnly = false;
                        //销售价格区间
                        this.XOcePrice1.Properties.ReadOnly = false;
                        this.XOcePrice2.Properties.ReadOnly = false;
                        this.XOcePrice3.Properties.ReadOnly = false;
                        this.XOcePrice4.Properties.ReadOnly = false;
                        this.XOcePrice5.Properties.ReadOnly = false;
                        this.XOcePrice6.Properties.ReadOnly = false;
                        this.XOcePrice7.Properties.ReadOnly = false;
                        this.XOcePriRange1_L.Properties.ReadOnly = false;
                        this.XOcePriRange1_R.Properties.ReadOnly = false;
                        this.XOcePriRange2_L.Properties.ReadOnly = false;
                        this.XOcePriRange2_R.Properties.ReadOnly = false;
                        this.XOcePriRange3_L.Properties.ReadOnly = false;
                        this.XOcePriRange3_R.Properties.ReadOnly = false;
                        this.XOcePriRange4_L.Properties.ReadOnly = false;
                        this.XOcePriRange4_R.Properties.ReadOnly = false;
                        this.XOcePriRange5_L.Properties.ReadOnly = false;
                        this.XOcePriRange5_R.Properties.ReadOnly = false;
                        this.XOcePriRange6_L.Properties.ReadOnly = false;
                        this.XOcePriRange6_R.Properties.ReadOnly = false;
                        this.XOcePriRange7_L.Properties.ReadOnly = false;
                        //正麦、侧麦
                        this.richTextZhengMai.ReadOnly = false;
                        this.richTextCeMai.ReadOnly = false;

                    }
                    else
                    {
                        this.lookUpBasedUnitGroupId.Properties.ReadOnly = true;
                        this.lookUpCJUnit.Properties.ReadOnly = true;
                        this.lookUpDepotUnit.Properties.ReadOnly = true;
                        this.lookUpEditQualityTestUnitId.Properties.ReadOnly = true;
                        this.lookUpProduceUnitId.Properties.ReadOnly = true;
                        this.lookUpSellUnit.Properties.ReadOnly = true;
                    }
                    break;

                case "update":
                    this.richTextZhengMai.ReadOnly = false;
                    this.richTextCeMai.ReadOnly = false;
                    this.barButtonItem3.Enabled = false;
                    this.gridView3.OptionsBehavior.Editable = true;
                    this.buttonEdit1.Enabled = true;
                    if (this.product.IsCustomerProduct == null || !(bool)this.product.IsCustomerProduct)
                    {
                        this.lookUpBasedUnitGroupId.Properties.ReadOnly = false;
                        this.lookUpCJUnit.Properties.ReadOnly = false;
                        this.lookUpDepotUnit.Properties.ReadOnly = false;
                        this.lookUpEditQualityTestUnitId.Properties.ReadOnly = false;
                        this.lookUpProduceUnitId.Properties.ReadOnly = false;
                        this.lookUpSellUnit.Properties.ReadOnly = false;
                        this.newChooseContorlProductType.Properties.Buttons[0].Enabled = true;
                        this.newChooseContorlProductType.Properties.ReadOnly = false;

                        this.comboBoxEditAbcCategory.Properties.ReadOnly = false;

                        //this.newChooseContorlBasedUnitGroupId.ShowButton = true;
                        //this.newChooseContorlBasedUnitGroupId.ButtonReadOnly = false;

                        this.spinEditBeenAssigned.Properties.ReadOnly = false;
                        this.spinEditBorrowQuantity.Properties.ReadOnly = false;

                        this.newChooseContorlBuyEmployee.ShowButton = true;
                        this.newChooseContorlBuyEmployee.ButtonReadOnly = false;

                        //this.newChooseContorlBuyUnitId.ShowButton = true;
                        //this.newChooseContorlBuyUnitId.ButtonReadOnly = false;

                        this.comboBoxEditCheckCategory.Properties.ReadOnly = false;
                        this.spinEditCheckCount.Properties.ReadOnly = false;
                        this.spinEditCheckRate.Properties.ReadOnly = false;
                        this.comboBoxEditCheckRule.Properties.ReadOnly = false;
                        this.checkEditConsume.Properties.ReadOnly = false;

                        this.newChooseContorlCustomInspectionRule.ShowButton = true;
                        this.newChooseContorlCustomInspectionRule.ButtonReadOnly = false;

                        this.spinEditDamageRate.Properties.ReadOnly = false;

                        this.newChooseContorlDepot.ShowButton = true;
                        this.newChooseContorlDepot.ButtonReadOnly = false;

                        this.newChooseContorlDepotPosition.ShowButton = true;
                        this.newChooseContorlDepotPosition.ButtonReadOnly = false;

                        //this.newChooseContorlDepotUnit.ShowButton = true;
                        //this.newChooseContorlDepotUnit.ButtonReadOnly = false;

                        this.newChooseContorlEmployeeChange.ShowButton = true;
                        this.newChooseContorlEmployeeChange.ButtonReadOnly = false;

                        this.newChooseContorlEmployeeCreator.ShowButton = true;
                        this.newChooseContorlEmployeeCreator.ButtonReadOnly = false;

                        this.spinEditGrossWeight.Properties.ReadOnly = false;
                        this.spinEditHeight.Properties.ReadOnly = false;
                        this.spinEditHighestPurchasingPrice.Properties.ReadOnly = false;
                        this.spinEditHighestStock.Properties.ReadOnly = false;
                        this.checkEditHomeMade.Properties.ReadOnly = false;

                        this.newChooseContorlInsteadOfProduct.ShowButton = true;
                        this.newChooseContorlInsteadOfProduct.ButtonReadOnly = false;

                        this.spinEditLendQuantity.Properties.ReadOnly = false;
                        this.spinEditLength.Properties.ReadOnly = false;
                        this.spinEditLowestSellPrice.Properties.ReadOnly = false;
                        this.spinEditLowestStock.Properties.ReadOnly = false;

                        //this.newChooseContorMainUnitId.ShowButton = true;
                        //this.newChooseContorMainUnitId.ButtonReadOnly = false;

                        this.spinEditNetWeight.Properties.ReadOnly = false;
                        this.spinEditNewestCost.Properties.ReadOnly = false;
                        this.spinEditOnMadingQuantity.Properties.ReadOnly = false;
                        this.spinEditOrderOnWayQuantity.Properties.ReadOnly = false;
                        this.checkEditOutSourcing.Properties.ReadOnly = false;

                        this.newChooseContorlPackageType.ShowButton = true;
                        this.newChooseContorlPackageType.ButtonReadOnly = false;

                        this.spinEditPlanSellPrice.Properties.ReadOnly = false;
                        this.textEditPowerGroup.Properties.ReadOnly = false;
                        this.textEditProduceCounty.Properties.ReadOnly = false;

                        //this.newChooseContorProduceUnitId.ShowButton = true;
                        //this.newChooseContorProduceUnitId.ButtonReadOnly = false;

                        this.textEditProductBarCode.Properties.ReadOnly = false;
                        this.textEditProductBatch.Properties.ReadOnly = false;
                        this.memoEditProductDescription.ReadOnly = false;
                        this.textEditId.Properties.ReadOnly = false;
                        this.textEditProductName.Properties.ReadOnly = false;
                        this.textEditProductPlace.Properties.ReadOnly = false;
                        this.textEditProductSpecification.Properties.ReadOnly = false;
                        this.textEditQualityRequire.Properties.ReadOnly = false;

                        this.newChooseContorlQualityTestPlan.ShowButton = true;
                        this.newChooseContorlQualityTestPlan.ButtonReadOnly = false;

                        //this.newChooseContorlQualityTestUnitId.ShowButton = true;
                        //this.newChooseContorlQualityTestUnitId.ButtonReadOnly = false;

                        this.spinEditReferenceCost.Properties.ReadOnly = false;
                        this.spinEditReferenceSellPrice.Properties.ReadOnly = false;
                        this.spinEditSafeStock.Properties.ReadOnly = false;

                        //this.newChooseContorSellUnitId.ShowButton = true;
                        //this.newChooseContorSellUnitId.ButtonReadOnly = false;

                        this.spinEditStocksQuantity.Properties.ReadOnly = false;
                        this.comboBoxEditDigital.Properties.ReadOnly = false;

                        this.newChooseContorSupplierId.ShowButton = true;
                        this.newChooseContorSupplierId.ButtonReadOnly = false;

                        this.checkEditTrustOut.Properties.ReadOnly = false;
                        this.comboBoxEditValuationWay.Properties.ReadOnly = false;
                        this.spinEditVolume.Properties.ReadOnly = false;
                        this.checkEditIsProcess.Properties.ReadOnly = false;

                        this.newChooseContorVolumeUnit.ShowButton = true;
                        this.newChooseContorVolumeUnit.ButtonReadOnly = false;

                        this.newChooseContorlVolumeUnitGroup.ShowButton = true;
                        this.newChooseContorlVolumeUnitGroup.ButtonReadOnly = false;

                        this.newChooseContorWeightUnit.ShowButton = true;
                        this.newChooseContorWeightUnit.ButtonReadOnly = false;

                        this.newChooseContorWeightUnitGroup.ShowButton = true;
                        this.newChooseContorWeightUnitGroup.ButtonReadOnly = false;


                        this.spinEditWidth.Properties.ReadOnly = false;
                        this.checkEditIsRequiredCheck.Properties.ReadOnly = false;
                        this.checkEditIsRequiredCheckCycle.Properties.ReadOnly = false;
                        this.textEditCheckCycle.Properties.ReadOnly = false;

                        this.radioGroupBarCode.Enabled = true;

                        this.comboBoxEditStockCheckTimeParts.Properties.ReadOnly = false;

                        this.dateEditLastStockTakeTime.Properties.ReadOnly = false;
                        this.dateEditLastStockTakeTime.Properties.Buttons[0].Visible = true;

                        this.dateEditProductDeadDate.Properties.ReadOnly = false;
                        this.dateEditProductDeadDate.Properties.Buttons[0].Visible = true;

                        this.dateEditProductNearCGDate.Properties.ReadOnly = false;
                        this.dateEditProductNearCGDate.Properties.Buttons[0].Visible = true;

                        this.dateEditProductNearXSDate.Properties.ReadOnly = false;
                        this.dateEditProductNearXSDate.Properties.Buttons[0].Visible = true;

                        this.dateEditUpdateTime.Properties.ReadOnly = false;
                        this.dateEditUpdateTime.Properties.Buttons[0].Visible = true;

                        //    this.textEditProcess.Properties.ReadOnly = true;
                        //this.buttonEditProcessGroup.Properties.ReadOnly = false;
                        //this.buttonEditProcessGroup.Properties.Buttons[0].Enabled = true;

                        //价格区间
                        this.cePrice1.Properties.ReadOnly = false;
                        this.cePrice2.Properties.ReadOnly = false;
                        this.cePrice3.Properties.ReadOnly = false;
                        this.cePrice4.Properties.ReadOnly = false;
                        this.cePrice5.Properties.ReadOnly = false;
                        this.cePrice6.Properties.ReadOnly = false;
                        this.cePrice7.Properties.ReadOnly = false;
                        this.cePriRange1_L.Properties.ReadOnly = false;
                        this.cePriRange1_R.Properties.ReadOnly = false;
                        this.cePriRange2_L.Properties.ReadOnly = false;
                        this.cePriRange2_R.Properties.ReadOnly = false;
                        this.cePriRange3_L.Properties.ReadOnly = false;
                        this.cePriRange3_R.Properties.ReadOnly = false;
                        this.cePriRange4_L.Properties.ReadOnly = false;
                        this.cePriRange4_R.Properties.ReadOnly = false;
                        this.cePriRange5_L.Properties.ReadOnly = false;
                        this.cePriRange5_R.Properties.ReadOnly = false;
                        this.cePriRange6_L.Properties.ReadOnly = false;
                        this.cePriRange6_R.Properties.ReadOnly = false;
                        this.cePriRange7_L.Properties.ReadOnly = false;
                        //销售价格区间
                        this.XOcePrice1.Properties.ReadOnly = false;
                        this.XOcePrice2.Properties.ReadOnly = false;
                        this.XOcePrice3.Properties.ReadOnly = false;
                        this.XOcePrice4.Properties.ReadOnly = false;
                        this.XOcePrice5.Properties.ReadOnly = false;
                        this.XOcePrice6.Properties.ReadOnly = false;
                        this.XOcePrice7.Properties.ReadOnly = false;
                        this.XOcePriRange1_L.Properties.ReadOnly = false;
                        this.XOcePriRange1_R.Properties.ReadOnly = false;
                        this.XOcePriRange2_L.Properties.ReadOnly = false;
                        this.XOcePriRange2_R.Properties.ReadOnly = false;
                        this.XOcePriRange3_L.Properties.ReadOnly = false;
                        this.XOcePriRange3_R.Properties.ReadOnly = false;
                        this.XOcePriRange4_L.Properties.ReadOnly = false;
                        this.XOcePriRange4_R.Properties.ReadOnly = false;
                        this.XOcePriRange5_L.Properties.ReadOnly = false;
                        this.XOcePriRange5_R.Properties.ReadOnly = false;
                        this.XOcePriRange6_L.Properties.ReadOnly = false;
                        this.XOcePriRange6_R.Properties.ReadOnly = false;
                        this.XOcePriRange7_L.Properties.ReadOnly = false;
                        //正麦、侧麦
                        this.richTextZhengMai.ReadOnly = false;
                        this.richTextCeMai.ReadOnly = false;
                    }
                    else
                    {
                        this.lookUpBasedUnitGroupId.Enabled = true;
                        this.lookUpCJUnit.Enabled = true;
                        this.lookUpDepotUnit.Enabled = true;
                        this.lookUpEditQualityTestUnitId.Enabled = true;
                        this.lookUpProduceUnitId.Enabled = true;
                        this.lookUpSellUnit.Enabled = true;
                        this.lookUpBasedUnitGroupId.Properties.ReadOnly = true;
                        this.lookUpCJUnit.Properties.ReadOnly = true;
                        this.lookUpDepotUnit.Properties.ReadOnly = true;
                        this.lookUpEditQualityTestUnitId.Properties.ReadOnly = true;
                        this.lookUpProduceUnitId.Properties.ReadOnly = true;
                        this.lookUpSellUnit.Properties.ReadOnly = true;
                    }
                    break;
                case "view":
                    this.barButtonItem3.Enabled = false;
                    this.gridView3.OptionsBehavior.Editable = false;
                    this.lookUpBasedUnitGroupId.Properties.ReadOnly = true;
                    this.lookUpCJUnit.Properties.ReadOnly = true;
                    this.lookUpDepotUnit.Properties.ReadOnly = true;
                    this.lookUpEditQualityTestUnitId.Properties.ReadOnly = true;
                    this.lookUpProduceUnitId.Properties.ReadOnly = true;
                    this.lookUpSellUnit.Properties.ReadOnly = true;
                    this.newChooseContorlProductType.Properties.Buttons[0].Enabled = false;
                    this.newChooseContorlProductType.Properties.ReadOnly = true;

                    this.comboBoxEditAbcCategory.Properties.ReadOnly = true;

                    //this.newChooseContorlBasedUnitGroupId.ShowButton = false;
                    //this.newChooseContorlBasedUnitGroupId.ButtonReadOnly = true;

                    this.spinEditBeenAssigned.Properties.ReadOnly = true;
                    this.spinEditBorrowQuantity.Properties.ReadOnly = true;

                    this.newChooseContorlBuyEmployee.ShowButton = false;
                    this.newChooseContorlBuyEmployee.ButtonReadOnly = true;

                    //this.newChooseContorlBuyUnitId.ShowButton = false;
                    //this.newChooseContorlBuyUnitId.ButtonReadOnly = true;

                    this.comboBoxEditCheckCategory.Properties.ReadOnly = true;
                    this.spinEditCheckCount.Properties.ReadOnly = true;
                    this.spinEditCheckRate.Properties.ReadOnly = true;
                    this.comboBoxEditCheckRule.Properties.ReadOnly = true;
                    this.checkEditConsume.Properties.ReadOnly = true;

                    this.newChooseContorlCustomInspectionRule.ShowButton = false;
                    this.newChooseContorlCustomInspectionRule.ButtonReadOnly = true;

                    this.spinEditDamageRate.Properties.ReadOnly = true;

                    this.newChooseContorlDepot.ShowButton = false;
                    this.newChooseContorlDepot.ButtonReadOnly = true;

                    this.newChooseContorlDepotPosition.ShowButton = false;
                    this.newChooseContorlDepotPosition.ButtonReadOnly = true;

                    //this.newChooseContorlDepotUnit.ShowButton = false;
                    //this.newChooseContorlDepotUnit.ButtonReadOnly = true;

                    this.newChooseContorlEmployeeChange.ShowButton = false;
                    this.newChooseContorlEmployeeChange.ButtonReadOnly = true;

                    this.newChooseContorlEmployeeCreator.ShowButton = false;
                    this.newChooseContorlEmployeeCreator.ButtonReadOnly = true;

                    this.spinEditGrossWeight.Properties.ReadOnly = true;
                    this.spinEditHeight.Properties.ReadOnly = true;
                    this.spinEditHighestPurchasingPrice.Properties.ReadOnly = true;
                    this.spinEditHighestStock.Properties.ReadOnly = true;
                    this.checkEditHomeMade.Properties.ReadOnly = true;

                    this.newChooseContorlInsteadOfProduct.ShowButton = false;
                    this.newChooseContorlInsteadOfProduct.ButtonReadOnly = true;

                    this.spinEditLendQuantity.Properties.ReadOnly = true;
                    this.spinEditLength.Properties.ReadOnly = true;
                    this.spinEditLowestSellPrice.Properties.ReadOnly = true;
                    this.spinEditLowestStock.Properties.ReadOnly = true;

                    //this.newChooseContorMainUnitId.ShowButton = false;
                    //this.newChooseContorMainUnitId.ButtonReadOnly = true;

                    this.spinEditNetWeight.Properties.ReadOnly = true;
                    this.spinEditNewestCost.Properties.ReadOnly = true;
                    this.spinEditOnMadingQuantity.Properties.ReadOnly = true;
                    this.spinEditOrderOnWayQuantity.Properties.ReadOnly = true;
                    this.checkEditOutSourcing.Properties.ReadOnly = true;

                    this.newChooseContorlPackageType.ShowButton = false;
                    this.newChooseContorlPackageType.ButtonReadOnly = true;

                    this.spinEditPlanSellPrice.Properties.ReadOnly = true;
                    this.textEditPowerGroup.Properties.ReadOnly = true;
                    this.textEditProduceCounty.Properties.ReadOnly = true;

                    //this.newChooseContorProduceUnitId.ShowButton = false;
                    //this.newChooseContorProduceUnitId.ButtonReadOnly = true;

                    this.textEditProductBarCode.Properties.ReadOnly = true;
                    this.textEditProductBatch.Properties.ReadOnly = true;
                    this.memoEditProductDescription.ReadOnly = true;
                    this.textEditId.Properties.ReadOnly = true;
                    this.textEditProductName.Properties.ReadOnly = true;
                    this.textEditProductPlace.Properties.ReadOnly = true;
                    this.textEditProductSpecification.Properties.ReadOnly = true;
                    this.textEditQualityRequire.Properties.ReadOnly = true;

                    this.newChooseContorlQualityTestPlan.ShowButton = false;
                    this.newChooseContorlQualityTestPlan.ButtonReadOnly = true;

                    //this.newChooseContorlQualityTestUnitId.ShowButton = false;
                    //this.newChooseContorlQualityTestUnitId.ButtonReadOnly = true;

                    this.spinEditReferenceCost.Properties.ReadOnly = true;
                    this.spinEditReferenceSellPrice.Properties.ReadOnly = true;
                    this.spinEditSafeStock.Properties.ReadOnly = true;

                    //this.newChooseContorSellUnitId.ShowButton = false;
                    //this.newChooseContorSellUnitId.ButtonReadOnly = true;

                    this.spinEditStocksQuantity.Properties.ReadOnly = true;
                    this.comboBoxEditDigital.Properties.ReadOnly = true;

                    this.newChooseContorSupplierId.ShowButton = false;
                    this.newChooseContorSupplierId.ButtonReadOnly = true;

                    this.checkEditTrustOut.Properties.ReadOnly = true;
                    this.comboBoxEditValuationWay.Properties.ReadOnly = true;
                    this.spinEditVolume.Properties.ReadOnly = true;
                    this.checkEditIsProcess.Properties.ReadOnly = true;

                    this.newChooseContorVolumeUnit.ShowButton = false;
                    this.newChooseContorVolumeUnit.ButtonReadOnly = true;

                    this.newChooseContorlVolumeUnitGroup.ShowButton = false;
                    this.newChooseContorlVolumeUnitGroup.ButtonReadOnly = true;

                    this.newChooseContorWeightUnit.ShowButton = false;
                    this.newChooseContorWeightUnit.ButtonReadOnly = true;

                    this.newChooseContorWeightUnitGroup.ShowButton = false;
                    this.newChooseContorWeightUnitGroup.ButtonReadOnly = true;


                    this.spinEditWidth.Properties.ReadOnly = true;
                    this.checkEditIsRequiredCheck.Properties.ReadOnly = true;
                    this.checkEditIsRequiredCheckCycle.Properties.ReadOnly = true;
                    this.textEditCheckCycle.Properties.ReadOnly = true;

                    this.radioGroupBarCode.Enabled = false;
                    this.comboBoxEditStockCheckTimeParts.Properties.ReadOnly = true;

                    this.dateEditLastStockTakeTime.Properties.ReadOnly = true;
                    this.dateEditLastStockTakeTime.Properties.Buttons[0].Visible = false;

                    this.dateEditProductDeadDate.Properties.ReadOnly = true;
                    this.dateEditProductDeadDate.Properties.Buttons[0].Visible = false;

                    this.dateEditProductNearCGDate.Properties.ReadOnly = true;
                    this.dateEditProductNearCGDate.Properties.Buttons[0].Visible = false;

                    this.dateEditProductNearXSDate.Properties.ReadOnly = true;
                    this.dateEditProductNearXSDate.Properties.Buttons[0].Visible = false;

                    this.dateEditUpdateTime.Properties.ReadOnly = true;
                    this.dateEditUpdateTime.Properties.Buttons[0].Visible = false;

                    //   this.textEditProcess.Properties.ReadOnly = true;

                    //this.buttonEditProcessGroup.Properties.ReadOnly = true;
                    //this.buttonEditProcessGroup.Properties.Buttons[0].Enabled = false;

                    //价格区间
                    this.cePrice1.Properties.ReadOnly = true;
                    this.cePrice2.Properties.ReadOnly = true;
                    this.cePrice3.Properties.ReadOnly = true;
                    this.cePrice4.Properties.ReadOnly = true;
                    this.cePrice5.Properties.ReadOnly = true;
                    this.cePrice6.Properties.ReadOnly = true;
                    this.cePrice7.Properties.ReadOnly = true;
                    this.cePriRange1_L.Properties.ReadOnly = true;
                    this.cePriRange1_R.Properties.ReadOnly = true;
                    this.cePriRange2_L.Properties.ReadOnly = true;
                    this.cePriRange2_R.Properties.ReadOnly = true;
                    this.cePriRange3_L.Properties.ReadOnly = true;
                    this.cePriRange3_R.Properties.ReadOnly = true;
                    this.cePriRange4_L.Properties.ReadOnly = true;
                    this.cePriRange4_R.Properties.ReadOnly = true;
                    this.cePriRange5_L.Properties.ReadOnly = true;
                    this.cePriRange5_R.Properties.ReadOnly = true;
                    this.cePriRange6_L.Properties.ReadOnly = true;
                    this.cePriRange6_R.Properties.ReadOnly = true;
                    this.cePriRange7_L.Properties.ReadOnly = true;
                    //销售价格区间
                    this.XOcePrice1.Properties.ReadOnly = true;
                    this.XOcePrice2.Properties.ReadOnly = true;
                    this.XOcePrice3.Properties.ReadOnly = true;
                    this.XOcePrice4.Properties.ReadOnly = true;
                    this.XOcePrice5.Properties.ReadOnly = true;
                    this.XOcePrice6.Properties.ReadOnly = true;
                    this.XOcePrice7.Properties.ReadOnly = true;
                    this.XOcePriRange1_L.Properties.ReadOnly = true;
                    this.XOcePriRange1_R.Properties.ReadOnly = true;
                    this.XOcePriRange2_L.Properties.ReadOnly = true;
                    this.XOcePriRange2_R.Properties.ReadOnly = true;
                    this.XOcePriRange3_L.Properties.ReadOnly = true;
                    this.XOcePriRange3_R.Properties.ReadOnly = true;
                    this.XOcePriRange4_L.Properties.ReadOnly = true;
                    this.XOcePriRange4_R.Properties.ReadOnly = true;
                    this.XOcePriRange5_L.Properties.ReadOnly = true;
                    this.XOcePriRange5_R.Properties.ReadOnly = true;
                    this.XOcePriRange6_L.Properties.ReadOnly = true;
                    this.XOcePriRange6_R.Properties.ReadOnly = true;
                    this.XOcePriRange7_L.Properties.ReadOnly = true;
                    //正麦、侧麦
                    this.richTextZhengMai.ReadOnly = true;
                    this.richTextCeMai.ReadOnly = true;
                    this.buttonEdit1.Enabled = false;
                    break;

                default:
                    break;
            }
            base.Refresh();
            switch (this.action)
            {
                case "insert":
                case "update":
                    this.barButtonItem8.Enabled = false;
                    break;
                case "view":
                    this.barButtonItem8.Enabled = true;
                    break;
            }
            this.simpleButtonselectpic.Enabled = true;
            this.simpleButtonsave.Enabled = true;
            this.simpleButtonNext.Enabled = true;
            this.simpleButtonDelete.Enabled = true;
            this.dateEditcostartdate.Enabled = true;
            this.dateEditcostartdate.Properties.ReadOnly = false;
            this.dateEditcostartdate.Properties.Buttons[0].Visible = true;
            this.dateEditcoenddate.Properties.ReadOnly = false;
            this.dateEditcoenddate.Properties.Buttons[0].Visible = true;
            this.dateEditcoenddate.Enabled = true;
            this.dateEditxostartdate.Properties.ReadOnly = false;
            this.dateEditxostartdate.Properties.Buttons[0].Visible = true;
            this.dateEditxostartdate.Enabled = true;
            this.dateEditxoenddate.Properties.ReadOnly = false;
            this.dateEditxoenddate.Properties.Buttons[0].Visible = true;
            this.dateEditxoenddate.Enabled = true;
            this.simpleButton_search.Enabled = true;
            this.simpleButtonMation.Enabled = true;
            this.textEditDescription.Properties.ReadOnly = false;

            if (this.action == "update")
            {

                this.buttonEditTechonProcedures.Properties.ReadOnly = false;
                this.buttonEditTechonProcedures.Properties.Buttons[0].Visible = true;
                if (this.product.IsCustomerProduct != null && (bool)this.product.IsCustomerProduct)
                {

                    this.newChooseContorlDepot.ShowButton = true;
                    this.newChooseContorlDepot.ButtonReadOnly = false;

                    this.newChooseContorlDepotPosition.ShowButton = true;
                    this.newChooseContorlDepotPosition.ButtonReadOnly = false;

                    this.barButtonItem3.Enabled = false;
                    //this.lookUpBasedUnitGroupId.Properties.ReadOnly = true;
                    //this.lookUpCJUnit.Properties.ReadOnly = true;
                    //this.lookUpDepotUnit.Properties.ReadOnly = true;
                    //this.lookUpEditQualityTestUnitId.Properties.ReadOnly = true;
                    //this.lookUpProduceUnitId.Properties.ReadOnly = true;
                    //this.lookUpSellUnit.Properties.ReadOnly = true;
                    this.newChooseContorlProductType.Properties.Buttons[0].Enabled = false;
                    this.newChooseContorlProductType.Properties.ReadOnly = true;

                    this.comboBoxEditAbcCategory.Properties.ReadOnly = true;

                    //this.newChooseContorlBasedUnitGroupId.ShowButton = false;
                    //this.newChooseContorlBasedUnitGroupId.ButtonReadOnly = true;

                    this.spinEditBeenAssigned.Properties.ReadOnly = true;
                    this.spinEditBorrowQuantity.Properties.ReadOnly = true;

                    this.newChooseContorlBuyEmployee.ShowButton = false;
                    this.newChooseContorlBuyEmployee.ButtonReadOnly = true;

                    //this.newChooseContorlBuyUnitId.ShowButton = false;
                    //this.newChooseContorlBuyUnitId.ButtonReadOnly = true;

                    this.comboBoxEditCheckCategory.Properties.ReadOnly = true;
                    this.spinEditCheckCount.Properties.ReadOnly = true;
                    this.spinEditCheckRate.Properties.ReadOnly = true;
                    this.comboBoxEditCheckRule.Properties.ReadOnly = true;


                    this.newChooseContorlCustomInspectionRule.ShowButton = false;
                    this.newChooseContorlCustomInspectionRule.ButtonReadOnly = true;

                    this.spinEditDamageRate.Properties.ReadOnly = true;


                    //this.newChooseContorlDepotUnit.ShowButton = false;
                    //this.newChooseContorlDepotUnit.ButtonReadOnly = true;

                    this.newChooseContorlEmployeeChange.ShowButton = false;
                    this.newChooseContorlEmployeeChange.ButtonReadOnly = true;

                    this.newChooseContorlEmployeeCreator.ShowButton = false;
                    this.newChooseContorlEmployeeCreator.ButtonReadOnly = true;

                    this.spinEditGrossWeight.Properties.ReadOnly = true;
                    this.spinEditHeight.Properties.ReadOnly = true;
                    this.spinEditHighestPurchasingPrice.Properties.ReadOnly = true;
                    this.spinEditHighestStock.Properties.ReadOnly = true;


                    this.newChooseContorlInsteadOfProduct.ShowButton = false;
                    this.newChooseContorlInsteadOfProduct.ButtonReadOnly = true;

                    this.spinEditLendQuantity.Properties.ReadOnly = true;
                    this.spinEditLength.Properties.ReadOnly = true;
                    this.spinEditLowestSellPrice.Properties.ReadOnly = true;
                    this.spinEditLowestStock.Properties.ReadOnly = true;

                    //this.newChooseContorMainUnitId.ShowButton = false;
                    //this.newChooseContorMainUnitId.ButtonReadOnly = true;

                    this.spinEditNetWeight.Properties.ReadOnly = true;
                    this.spinEditNewestCost.Properties.ReadOnly = true;
                    this.spinEditOnMadingQuantity.Properties.ReadOnly = true;
                    this.spinEditOrderOnWayQuantity.Properties.ReadOnly = true;

                    this.newChooseContorlPackageType.ShowButton = false;
                    this.newChooseContorlPackageType.ButtonReadOnly = true;

                    this.spinEditPlanSellPrice.Properties.ReadOnly = true;
                    this.textEditPowerGroup.Properties.ReadOnly = true;
                    this.textEditProduceCounty.Properties.ReadOnly = true;

                    //this.newChooseContorProduceUnitId.ShowButton = false;
                    //this.newChooseContorProduceUnitId.ButtonReadOnly = true;

                    this.textEditProductBarCode.Properties.ReadOnly = true;
                    this.textEditProductBatch.Properties.ReadOnly = true;
                    this.memoEditProductDescription.ReadOnly = false;
                    this.textEditId.Properties.ReadOnly = true;
                    this.textEditProductName.Properties.ReadOnly = true;
                    this.textEditProductPlace.Properties.ReadOnly = true;
                    this.textEditProductSpecification.Properties.ReadOnly = true;
                    this.textEditQualityRequire.Properties.ReadOnly = true;

                    this.newChooseContorlQualityTestPlan.ShowButton = false;
                    this.newChooseContorlQualityTestPlan.ButtonReadOnly = true;

                    //this.newChooseContorlQualityTestUnitId.ShowButton = false;
                    //this.newChooseContorlQualityTestUnitId.ButtonReadOnly = true;

                    this.spinEditReferenceCost.Properties.ReadOnly = true;
                    this.spinEditReferenceSellPrice.Properties.ReadOnly = true;
                    this.spinEditSafeStock.Properties.ReadOnly = true;

                    //this.newChooseContorSellUnitId.ShowButton = false;
                    //this.newChooseContorSellUnitId.ButtonReadOnly = true;

                    this.spinEditStocksQuantity.Properties.ReadOnly = true;
                    this.comboBoxEditDigital.Properties.ReadOnly = true;

                    this.newChooseContorSupplierId.ShowButton = false;
                    this.newChooseContorSupplierId.ButtonReadOnly = true;


                    this.comboBoxEditValuationWay.Properties.ReadOnly = true;
                    this.spinEditVolume.Properties.ReadOnly = true;

                    this.newChooseContorVolumeUnit.ShowButton = false;
                    this.newChooseContorVolumeUnit.ButtonReadOnly = true;

                    this.newChooseContorlVolumeUnitGroup.ShowButton = false;
                    this.newChooseContorlVolumeUnitGroup.ButtonReadOnly = true;

                    this.newChooseContorWeightUnit.ShowButton = false;
                    this.newChooseContorWeightUnit.ButtonReadOnly = true;

                    this.newChooseContorWeightUnitGroup.ShowButton = false;
                    this.newChooseContorWeightUnitGroup.ButtonReadOnly = true;


                    this.spinEditWidth.Properties.ReadOnly = true;
                    this.checkEditIsRequiredCheck.Properties.ReadOnly = true;
                    this.checkEditIsRequiredCheckCycle.Properties.ReadOnly = true;
                    this.textEditCheckCycle.Properties.ReadOnly = true;

                    this.radioGroupBarCode.Enabled = false;
                    this.comboBoxEditStockCheckTimeParts.Properties.ReadOnly = true;

                    this.dateEditLastStockTakeTime.Properties.ReadOnly = true;
                    this.dateEditLastStockTakeTime.Properties.Buttons[0].Visible = false;

                    this.dateEditProductDeadDate.Properties.ReadOnly = true;
                    this.dateEditProductDeadDate.Properties.Buttons[0].Visible = false;

                    this.dateEditProductNearCGDate.Properties.ReadOnly = true;
                    this.dateEditProductNearCGDate.Properties.Buttons[0].Visible = false;

                    this.dateEditProductNearXSDate.Properties.ReadOnly = true;
                    this.dateEditProductNearXSDate.Properties.Buttons[0].Visible = false;

                    this.dateEditUpdateTime.Properties.ReadOnly = true;
                    this.dateEditUpdateTime.Properties.Buttons[0].Visible = false;

                    this.checkEditHomeMade.Properties.ReadOnly = false;
                    this.checkEditConsume.Properties.ReadOnly = false;
                    this.checkEditOutSourcing.Properties.ReadOnly = false;
                    this.checkEditTrustOut.Properties.ReadOnly = false;
                    this.checkEditIsProcess.Properties.ReadOnly = false;

                    this.textEditProductName.Enabled = true;
                    this.textEditProductName.Properties.ReadOnly = false;
                    //价格区间
                    this.cePrice1.Properties.ReadOnly = false;
                    this.cePrice2.Properties.ReadOnly = false;
                    this.cePrice3.Properties.ReadOnly = false;
                    this.cePrice4.Properties.ReadOnly = false;
                    this.cePrice5.Properties.ReadOnly = false;
                    this.cePrice6.Properties.ReadOnly = false;
                    this.cePrice7.Properties.ReadOnly = false;
                    this.cePriRange1_L.Properties.ReadOnly = false;
                    this.cePriRange1_R.Properties.ReadOnly = false;
                    this.cePriRange2_L.Properties.ReadOnly = false;
                    this.cePriRange2_R.Properties.ReadOnly = false;
                    this.cePriRange3_L.Properties.ReadOnly = false;
                    this.cePriRange3_R.Properties.ReadOnly = false;
                    this.cePriRange4_L.Properties.ReadOnly = false;
                    this.cePriRange4_R.Properties.ReadOnly = false;
                    this.cePriRange5_L.Properties.ReadOnly = false;
                    this.cePriRange5_R.Properties.ReadOnly = false;
                    this.cePriRange6_L.Properties.ReadOnly = false;
                    this.cePriRange6_R.Properties.ReadOnly = false;
                    this.cePriRange7_L.Properties.ReadOnly = false;
                    //销售价格区间
                    this.XOcePrice1.Properties.ReadOnly = false;
                    this.XOcePrice2.Properties.ReadOnly = false;
                    this.XOcePrice3.Properties.ReadOnly = false;
                    this.XOcePrice4.Properties.ReadOnly = false;
                    this.XOcePrice5.Properties.ReadOnly = false;
                    this.XOcePrice6.Properties.ReadOnly = false;
                    this.XOcePrice7.Properties.ReadOnly = false;
                    this.XOcePriRange1_L.Properties.ReadOnly = false;
                    this.XOcePriRange1_R.Properties.ReadOnly = false;
                    this.XOcePriRange2_L.Properties.ReadOnly = false;
                    this.XOcePriRange2_R.Properties.ReadOnly = false;
                    this.XOcePriRange3_L.Properties.ReadOnly = false;
                    this.XOcePriRange3_R.Properties.ReadOnly = false;
                    this.XOcePriRange4_L.Properties.ReadOnly = false;
                    this.XOcePriRange4_R.Properties.ReadOnly = false;
                    this.XOcePriRange5_L.Properties.ReadOnly = false;
                    this.XOcePriRange5_R.Properties.ReadOnly = false;
                    this.XOcePriRange6_L.Properties.ReadOnly = false;
                    this.XOcePriRange6_R.Properties.ReadOnly = false;
                    this.XOcePriRange7_L.Properties.ReadOnly = false;
                    //正麦、侧麦
                    this.richTextZhengMai.ReadOnly = false;
                    this.richTextCeMai.ReadOnly = false;

                }
                //this.textEditProcess.Properties.ReadOnly = false;
            }
            else
            {
                this.buttonEditTechonProcedures.Properties.ReadOnly = true;
                this.buttonEditTechonProcedures.Properties.Buttons[0].Visible = false;
                // this.richTextBoxProcess.Enabled = false ;

                //this.checkEditHomeMade.Properties.ReadOnly = true;
                //this.checkEditConsume.Properties.ReadOnly = true;
                //this.checkEditOutSourcing.Properties.ReadOnly = true;
                //this.checkEditTrustOut.Properties.ReadOnly = true;
                //this.textEditProcess.Properties.ReadOnly = true;
            }
            if (this.product != null)
            {
                Model.ProductImage image = this.productImageManager.GetFirsts(this.product.ProductId);
                if (image != null)
                {
                    this.productImage = image;
                    NewMethod();
                }
                else
                    this.pictureEditPrivew.Image = null;
            }
            //this.dateEditStockStart.Enabled = false;
            this.dateEditStockStart.Properties.ReadOnly = false;
            this.dateEditStockEnd.Enabled = true;
            this.dateEditStockEnd.Properties.ReadOnly = false;
            this.dateEditStockStart.Properties.Buttons[0].Visible = true;
            this.dateEditStockEnd.Properties.Buttons[0].Visible = true;
            this.simpleButtonStock.Enabled = true;
            btn_ExportExcel.Enabled = true;

            this.nccEmployeeCreator.Enabled = false;
            this.nccEmployeeChange.Enabled = false;
            this.de_UpdateTime.Enabled = false;
            this.calcEditStock0.Enabled = false;
            this.calcEditStock1.Enabled = false;

        }

        protected override bool HasRows()
        {
            return this.productManager.HasRows();
        }

        protected override bool HasRowsNext()
        {
            return this.productManager.HasRowsAfter(this.product);
        }

        protected override bool HasRowsPrev()
        {
            return this.productManager.HasRowsBefore(this.product);
        }

        protected override void IMECtrl()
        {
            Book.UI.Tools.IMEControl.IMECtrl(new Control[] { this, this.textEditCheckCycle,
                this.textEditPowerGroup, this.textEditProduceCounty, this.textEditProductBarCode,
                this.textEditProductBatch, this.textEditId, this.textEditProductPlace, 
                this.textEditProductSpecification, this.textEditQualityRequire, this.memoEditProductDescription,
                this.comboBoxEditAbcCategory, this.comboBoxEditCheckCategory, this.comboBoxEditCheckRule,
                this.comboBoxEditDigital, this.comboBoxEditStockCheckTimeParts, this.comboBoxEditValuationWay, 
                this.spinEditBeenAssigned, this.spinEditBorrowQuantity, this.spinEditCheckCount, 
                this.spinEditCheckRate, this.spinEditDamageRate, this.spinEditGrossWeight, this.spinEditHeight,
                this.spinEditHighestPurchasingPrice, this.spinEditHighestStock, this.spinEditLendQuantity,
                this.spinEditLength, this.spinEditLowestSellPrice, this.spinEditLowestStock, 
                this.spinEditNetWeight, this.spinEditNewestCost, this.spinEditOnMadingQuantity,
                this.spinEditOrderOnWayQuantity, this.spinEditPlanSellPrice, this.spinEditReferenceCost, 
                this.spinEditReferenceSellPrice, this.spinEditSafeStock, this.spinEditStocksQuantity,
                this.spinEditVolume, this.spinEditWidth,
                this.newChooseContorlBuyEmployee, 
                this.newChooseContorlCustomInspectionRule, this.newChooseContorlDepot,
                this.newChooseContorlDepotPosition, this.newChooseContorlEmployeeChange,
                this.newChooseContorlEmployeeCreator, this.newChooseContorlInsteadOfProduct, 
                this.newChooseContorlPackageType, this.newChooseContorlQualityTestPlan, 
                this.newChooseContorlVolumeUnitGroup, 
               // this.newChooseContorMainUnitId, this.newChooseContorProduceUnitId, this.newChooseContorSellUnitId, 
                this.newChooseContorSupplierId, this.newChooseContorVolumeUnit, this.newChooseContorWeightUnit, 
                this.newChooseContorWeightUnitGroup });
        }
        #endregion

        //private void ProductCategoryButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    ProductCategories.ChooseForm f = new ProductCategories.ChooseForm();
        //    f.ShowDialog(this);

        //    if (f.DialogResult == DialogResult.OK && f.SelectedItem != null)
        //    {
        //        (sender as ButtonEdit).EditValue = f.SelectedItem;
        //        //this.textEditId.Text = this.productManager.GetNewId((sender as BaseEdit).EditValue as Model.ProductCategory);
        //    }
        //}

        /// <summary>
        /// 自动输入/手工输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupBarCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.radioGroupBarCode.SelectedIndex;
            if (index == 1)
            {
                //自动输入
                this.textEditProductBarCode.Text = Guid.NewGuid().ToString();//商品条码

            }
            if (index == 0)
            {
                //手工輸入
                this.textEditProductBarCode.Text = string.Empty;
            }

        }

        private void comboBoxEditTakeCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxEditDigital.Properties.Items.Clear();
            int index = this.comboBoxEditStockCheckTimeParts.SelectedIndex;
            this.labelControlStockTakeCycle.Text = this.comboBoxEditStockCheckTimeParts.Text;
            if (index == 0)
            {
                this.comboBoxEditDigital.Enabled = false;
            }
            else
            {
                this.comboBoxEditDigital.Enabled = true;
            }
            if (index == 1)
            {

                this.comboBoxEditDigital.Properties.Items.AddRange(new object[] { 1, 2, 3, 4, 5, 6, 7 });
            }
            else
            {
                this.comboBoxEditDigital.Properties.Items.AddRange(new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30 });
            }
        }

        /// <summary>
        ///列印条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void simpleButton1_Click(object sender, EventArgs e)
        //{
        //    string bar = this.textEditProductBarCode.Text;
        //    if (string.IsNullOrEmpty(bar)) 
        //    {
        //        MessageBox.Show("??入產品條形碼!","提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    BarCode code = new BarCode(bar);
        //    code.ShowPreviewDialog();
        //}
        /// <summary>
        /// 主计量单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void newChooseContorlBasedUnitGroupId_EditValueChanged(object sender, EventArgs e)
        //{
        //    Model.UnitGroup group = this.newChooseContorlBasedUnitGroupId.EditValue as Model.UnitGroup;
        //    if (group != null)
        //    {
        //        string pars = group.UnitGroupId;
        //        string parsId=group.Id;

        //        this.newChooseContorlDepotUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorlDepotUnit.EditValue = null;

        //        this.newChooseContorlBuyUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorlBuyUnitId.EditValue = null;

        //        this.newChooseContorlQualityTestUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorlQualityTestUnitId.EditValue = null;

        //        //this.newChooseContorMainUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        //this.newChooseContorMainUnitId.EditValue = null;

        //        this.newChooseContorProduceUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorProduceUnitId.EditValue = null;

        //        this.newChooseContorSellUnitId.Choose = new BasicData.ProductUnit.ChooseProductUnit(pars);
        //        this.newChooseContorSellUnitId.EditValue = null;
        //    }
        //    else
        //    {
        //        this.newChooseContorlDepotUnit.Choose = null;
        //        this.newChooseContorlDepotUnit.EditValue = null;

        //        this.newChooseContorlBuyUnitId.Choose = null;
        //        this.newChooseContorlBuyUnitId.EditValue =null;

        //        this.newChooseContorlQualityTestUnitId.Choose = null;
        //        this.newChooseContorlQualityTestUnitId.EditValue = null;

        //        //this.newChooseContorMainUnitId.Choose = null;
        //       // this.newChooseContorMainUnitId.EditValue = null;

        //        this.newChooseContorProduceUnitId.Choose= null;
        //        this.newChooseContorProduceUnitId.EditValue = null;

        //        this.newChooseContorSellUnitId.Choose = null;
        //        this.newChooseContorSellUnitId.EditValue= null;
        //    }
        //}

        /// <summary>
        /// 默认仓库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newChooseContorlDepot_EditValueChanged(object sender, EventArgs e)
        {
            Model.Depot depot = this.newChooseContorlDepot.EditValue as Model.Depot;
            if (depot != null)
            {
                this.newChooseContorlDepotPosition.Choose = new Book.UI.Invoices.ChooseDepotPosition(depot);
                this.newChooseContorlDepotPosition.EditValue = null;
            }
            else
            {
                this.newChooseContorlDepotPosition.Choose = null;
                this.newChooseContorlDepotPosition.EditValue = null;
            }
        }

        /// <summary>
        /// 体积单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newChooseContorlVolumeUnitGroup_EditValueChanged(object sender, EventArgs e)
        {
            Model.UnitGroup group = this.newChooseContorlVolumeUnitGroup.EditValue as Model.UnitGroup;
            if (group != null)
            {
                this.newChooseContorVolumeUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(group);
                this.newChooseContorVolumeUnit.EditValue = null;
            }
            else
            {
                this.newChooseContorVolumeUnit.Choose = null;
                this.newChooseContorVolumeUnit.EditValue = null;
            }
        }

        /// <summary>
        /// 重量单位组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newChooseContorWeightUnitGroup_EditValueChanged(object sender, EventArgs e)
        {
            Model.UnitGroup group = this.newChooseContorWeightUnitGroup.EditValue as Model.UnitGroup;

            if (group != null)
            {
                this.newChooseContorWeightUnit.Choose = new BasicData.ProductUnit.ChooseProductUnit(group);
                this.newChooseContorWeightUnit.EditValue = null;
            }
            else
            {
                this.newChooseContorWeightUnit.Choose = null;
                this.newChooseContorWeightUnit.EditValue = null;

            }
        }

        private void newChooseContorlProductType_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.action != "view")
            {
                Settings.BasicData.ProductCategories.ChooseForm f = new Settings.BasicData.ProductCategories.ChooseForm();
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    (sender as DevExpress.XtraEditors.ButtonEdit).EditValue = f.SelectedItem;
                    this.product.ProductCategory = (this.newChooseContorlProductType.EditValue as Model.ProductCategory);

                    //生成编号
                    if (this.action == "update")
                    {
                        if (productCategory == this.product.ProductCategory.Id)
                            this.textEditId.Text = this.product.Id;
                        else
                        {
                            this.textEditId.Text = this.productManager.GetNewId(this.product.ProductCategory);
                            this.idIsChange = 1;
                        }
                    }

                    else
                    {
                        this.textEditId.Text = this.productManager.GetNewId(this.product.ProductCategory);
                    }


                    //foreach (TreeListNode listNode in this.treeList1.Nodes)
                    //{

                    //    if (listNode.Tag.ToString() == this.product.ProductCategory.ProductCategoryId)
                    //    {
                    //        flag2 = 1;
                    //        listNode.Selected = true;
                    //        break;
                    //    }
                    //}
                    //  flag2 = 0;

                    Model.ProductCategory category2 = this.nccProductCategoryTwo.EditValue as Model.ProductCategory;
                    if (category2 != null)
                    {
                        IList<Model.ProductCategory> list2 = productCategoryManager.SelectListByFilter("2", this.product.ProductCategory.ProductCategoryId);
                        if (!list2.Any(L => L.ProductCategoryId == category2.ProductCategoryId))
                        {
                            this.nccProductCategoryTwo.EditValue = null;
                            this.nccProductCategoryThree.EditValue = null;
                        }
                    }
                }
            }
        }

        //private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        //{
        //    if (flag == 0 && flag2 == 0)
        //    {
        //        if (e.Node != null && e.Node.ParentNode == null)
        //        {
        //            //点击类别 添加商品
        //            if (e.Node.Nodes != null && e.Node.Nodes.Count == 0)
        //            {
        //                //foreach (Model.Product prodcut in productManager.Select(this.productCategoryManager.Get(e.Node.Tag.ToString())))
        //                //{
        //                //    treeList1.AppendNode(new object[] { prodcut.IsCustomerProduct == true ? prodcut.ProductName + "{" + prodcut.CustomerProductName + "}" : prodcut.ProductName }, e.Node, prodcut.ProductId);
        //                //}
        //            }
        //            //设置商品类别
        //            cateTemp = this.productCategoryManager.Get(e.Node.Tag.ToString());
        //            if (this.action == "insert")
        //            {

        //                this.product.ProductCategory = cateTemp;
        //                this.newChooseContorlProductType.EditValue = this.product.ProductCategory as Model.ProductCategory;
        //                this.textEditId.Text = this.productManager.GetNewId(this.product.ProductCategory);

        //            }
        //        }
        //        if (e.Node != null && e.Node.ParentNode != null)
        //        {

        //            this.action = "view";
        //            this.product = productManager.Get(e.Node.Tag.ToString());
        //            this.productImage = productImageManager.GetFirsts(this.product.ProductId);
        //            this.Refresh();
        //            if (xtraTabControl1.SelectedTabPage == this.xtraTabPageStock)
        //            {
        //                this.gridControlStock.DataSource = this.stockManager.SelectReaderByPro(this.product.ProductId, this.dateEditStockStart.DateTime, this.dateEditStockEnd.DateTime);

        //            }
        //        }

        //    }

        //    flag2 = 0;
        //    this.simpleButton_search.PerformClick();
        //    this.simpleButtonMation.PerformClick();
        //}

        private void gridView3_ShowingEditor(object sender, CancelEventArgs e)
        {
            //object value = this.gridView3.GetRowCellValue(this.gridView3.FocusedRowHandle, this.gridColumn1);
            ////object tent= this.gridView3.GetRowCellValue(this.gridView3.FocusedRowHandle, this.gridColumnProcess);
            //if (this.gridView3.FocusedColumn == this.gridColumnProcess)
            //{
            //    //if (this.repositoryItemComboBox1.)

            //    this.repositoryItemComboBox1.Items.Clear();
            //    this.gridView3.SetRowCellValue(this.gridView3.FocusedRowHandle, this.gridColumnProcess, null);

            //    Model.ProductProcess ProductProcess = this.gridView3.GetRow(this.gridView3.FocusedRowHandle) as Model.ProductProcess;
            //    if (ProductProcess == null) return;
            //    if (value != null)
            //        ProductProcess.ProcessCategory = this.processCategoryManager.Get(value.ToString());
            //    if (ProductProcess.ProcessCategory == null) return;

            //    IList<Model.Processing> processings = this.processingManager.Select(ProductProcess.ProcessCategory, this._customer);
            //    this.repositoryItemComboBox1.Items.Clear();
            //    foreach (Model.Processing processing in processings)
            //    {
            //        this.repositoryItemComboBox1.Items.Add(processing);
            //    }
            //    ProductProcess.ProcessProductName = this.textEditProductName.Text + ProductProcess.ProcessCategory.Id + productManager.GetNewId();

            //}
        }

        private void gridView3_KeyDown(object sender, KeyEventArgs e)
        {
            //if (this.action == "insert" || this.action == "update")
            //{

            //    if (e.KeyData == Keys.Enter)
            //    {
            //        Model.ProductProcess detail = new Book.Model.ProductProcess();
            //        this.product.ProductProcess.Add(detail);
            //    }
            //    if (e.KeyData == Keys.Delete)
            //    {
            //        //this.simpleButtonRemove.PerformClick();
            //    }
            //    this.gridControl2.RefreshDataSource();
            //}
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Book.Model.Customer customer = new BL.CustomerManager().Get(this._customer.CustomerId);

            //Settings.BasicData.Customs.Processing.EditForm f = new Book.UI.Settings.BasicData.Customs.Processing.EditForm(customer);
            //if (f.ShowDialog() != DialogResult.OK)
            //{
            //    return;
            //}
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string bar = this.textEditProductBarCode.Text;
            if (string.IsNullOrEmpty(bar))
            {
                MessageBox.Show(Properties.Resources.NullBarCode, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BarCode code = new BarCode(bar);
            code.ShowPreviewDialog();
        }

        //private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    if (this.action == "view")
        //    {
        //        this.action = "update";
        //        base.Refresh();
        //    }

        //    if (string.IsNullOrEmpty(this.textEditProductName.Text))
        //    {
        //        MessageBox.Show("產品為空!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return;
        //    }
        //    // _ProductProcess.Clear();
        //    BasicData.Products.ProcessProductForm f = new BasicData.Products.ProcessProductForm(this.product);
        //    if (f.ShowDialog(this) != DialogResult.OK) return;
        //    if (_ProductProcess.Count > 0)
        //    {
        //        StringBuilder strBU = new StringBuilder();
        //        //foreach (Model.ProductProcess p in _ProductProcess)
        //        //{
        //        //    if (p.ProcessCategory == null) continue;
        //        //    strBU.Append(p.ProcessCategory.Id + "-");
        //        //}
        //        // if(this.action=="insert")
        //        if (this.product.IsProcee == false || this.product.IsProcee == null)
        //        {
        //            this.textEditProcess.Text = this.textEditProductName.Text + "-" + strBU + productManager.GetNewId();
        //            this.product.ProProcessName = this.textEditProductName.Text + "-" + strBU + productManager.GetNewId();
        //            this.textEditProductBarCode.Text = Guid.NewGuid().ToString();
        //        }
        //        else
        //        //  if (this.action == "update")
        //        {
        //            if (strBU.Length == 0)
        //            {
        //                this.textEditProcess.Text = this.product.ProductName;
        //            }
        //            else
        //            {
        //                if (this.productManager.Get(this.product.ProceebeforeProductId) != null)
        //                    this.textEditProcess.Text = this.productManager.Get(this.product.ProceebeforeProductId).ProductName + "-" + strBU + productManager.GetNewId();

        //                this.product.ProProcessName = this.productManager.Get(this.product.ProceebeforeProductId).ProductName + "-" + strBU + productManager.GetNewId();
        //                this.textEditProductBarCode.Text = Guid.NewGuid().ToString();
        //            }

        //        }

        //    }
        //}

        private void lookUpBasedUnitGroupId_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lookUpBasedUnitGroupId.EditValue != null)
                this.bindingSourceUnit.DataSource = this.productUnitManager.Select(this.lookUpBasedUnitGroupId.EditValue.ToString());

        }

        private void buttonEditProcessGroup_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            #region 加工組
            //    if (this.product == null) return;
            //    Customs.CustomerProcessing.EditForm f = new Book.UI.Settings.BasicData.Customs.CustomerProcessing.EditForm(this.product.CustomerProcessing, "product");
            //    if (f.ShowDialog(this) == DialogResult.OK)
            //    {
            //        this.product.CustomerProcessing = Customs.CustomerProcessing.EditForm.customerProcessing;
            //        if (this.product.CustomerProcessing != null)
            //            // this.product.CustomerProcessingId = this.product.CustomerProcessing.CustomerProcessingId;
            //            this.buttonEditProcessGroup.EditValue = this.product.CustomerProcessing;

            //        if (this.product.IsProcee != null && this.product.IsProcee.Value == true)
            //        {
            //            string name = this.productManager.Get(this.product.ProceebeforeProductId).ProductName;
            //            this.textEditProductName.Text = name + "-" + this.product.CustomerProcessing.Id;
            //        }
            //        else
            //            this.textEditProductName.Text = this.product.ProductName + "-" + this.product.CustomerProcessing.Id;

            //    }
            #endregion


        }

        private void lookUpBasedUnitGroupId_Leave(object sender, EventArgs e)
        {

            if (lookUpBasedUnitGroupId.Text == "")
            {
                lookUpBasedUnitGroupId.EditValue = null;
            }
        }

        private void lookUpCJUnit_Leave(object sender, EventArgs e)
        {
            if (lookUpCJUnit.Text == "")
            {
                lookUpCJUnit.EditValue = null;
            }
        }

        private void lookUpProduceUnitId_Leave(object sender, EventArgs e)
        {
            if (lookUpProduceUnitId.Text == "")
            {
                lookUpProduceUnitId.EditValue = null;
            }
        }

        private void lookUpDepotUnit_Leave(object sender, EventArgs e)
        {
            if (lookUpDepotUnit.Text == "")
            {
                lookUpDepotUnit.EditValue = null;
            }
        }

        private void lookUpSellUnit_Leave(object sender, EventArgs e)
        {
            if (lookUpSellUnit.Text == "")
            {
                lookUpSellUnit.EditValue = null;
            }
        }

        private void buttonEditTechonProcedures_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (this.action != "view")
            //{
            //    BasicData.Products.EditForm._proceduresStatic.Clear();
            //    ChooseTechonProceduresForm f = new ChooseTechonProceduresForm();
            //    // this.richTextBoxProcess.Visible;// = false;

            //    if (f.ShowDialog(this) == DialogResult.OK)
            //    {

            //        ////product.ProductProcess.Clear();
            //        this.memoEditProductDescription.Clear();
            //        if (this.product.IsProcee == null || !this.product.IsProcee.Value)
            //            this.memoEditProductDescription.Rtf = this.product.ProductDescription;
            //        else
            //            this.memoEditProductDescription.Rtf = this.productManager.Get(this.product.ProceebeforeProductId).ProductDescription;
            //        // this.memoEditProductDescription.SelectionStart = this.memoEditProductDescription.TextLength;

            //        if (this.memoEditProductDescription.TextLength != 0)
            //            this.memoEditProductDescription.AppendText(";");
            //        // richTextBoxProcess.Clear();
            //        string str = string.Empty;
            //      //  string name = string.Empty;
            //        try
            //        {
            //            int indexNo = product.ProductProcess.Count;
            //            foreach (Model.Procedures procedures in _proceduresStatic)
            //            {
            //                // this.memoEditProductDescription.WordWrap = false;
            //              //  str += "-" + procedures.Id;
            //                Model.ProductProcess productProcess = new Book.Model.ProductProcess();
            //                productProcess.ProductProcessId = Guid.NewGuid().ToString();
            //                productProcess.Procedures = procedures;
            //                productProcess.ProceduresId = procedures.ProceduresId;
            //                indexNo = indexNo + 1;
            //                productProcess.IndexNo = indexNo;
            //                product.ProductProcess.Add(productProcess);

            //                //商品描述加 加工工序
            //                //RichTextBox richTextBox = new RichTextBox();
            //                //richTextBox.Rtf = procedures.Procedurename;
            //                //richTextBox.SelectAll();

            //                //richTextBox.Copy();
            //                //this.memoEditProductDescription.Paste();
            //                //Clipboard.Clear();
            //                ////this.memoEditProductDescription.SelectionStart = this.memoEditProductDescription.Text.Length;
            //                ////string contentrtf = procedures.Procedurename;
            //                ////this.memoEditProductDescription.SelectedRtf = contentrtf.Remove(contentrtf.LastIndexOf(@"\par"), 4);
            //                ////if (_proceduresStatic.IndexOf(procedures) == _proceduresStatic.Count - 1)
            //                ////    break;
            //                ////this.memoEditProductDescription.AppendText(",");
            //            }

            //            foreach (Model.ProductProcess productProcess in product.ProductProcess)
            //            {
            //                this.memoEditProductDescription.SelectionStart = this.memoEditProductDescription.Text.Length;
            //                string contentrtf = productProcess.Procedures.Procedurename;
            //                this.memoEditProductDescription.SelectedRtf = contentrtf.Remove(contentrtf.LastIndexOf(@"\par"), 4);
            //                if (product.ProductProcess.IndexOf(productProcess) == product.ProductProcess.Count - 1)
            //                    break;
            //                this.memoEditProductDescription.AppendText(",");
            //            }
            //           // this.bindingSourceProductProcess.DataSource = product.ProductProcess;
            //            this.gridControl2.RefreshDataSource();

            //        }
            //        catch
            //        {

            //        }

            //        //_proceduresStatic.Clear();
            //        if (product.IsProcee == true && !string.IsNullOrEmpty(this.textEditProductName.Text))
            //        {
            //            this.buttonEditTechonProcedures.Text = this.textEditProductName.Text;
            //            this.textEditProceduresId.Text = this.textEditId.Text;
            //        }
            //        else
            //        {
            //            foreach (Model.ProductProcess productProcess in product.ProductProcess)
            //            {
            //                str += "-" + productProcess.Procedures.Id;
            //            }
            //            this.buttonEditTechonProcedures.Text = this.product.IsProcee == true ? this.productManager.Get(this.product.ProceebeforeProductId).ProductName + str : this.product.ProductName + str;
            //            this.textEditProceduresId.Text = this.product.IsProcee == true ? this.productManager.Get(this.product.ProceebeforeProductId).Id + str : this.product.Id + str;
            //        }
            //        //if (this.product.IsProcee == true)
            //        //{ 
            //        //     this.text
            //        //}
            //    }
            //    f.Dispose();
            //    GC.Collect();
            //}


        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Book.UI.produceManager.MouldCategory.ChooseProductMouldEditForm f = new Book.UI.produceManager.MouldCategory.ChooseProductMouldEditForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                Model.ProductMould productMould = f.SelectedItem as Model.ProductMould;
                Model.ProductMouldDetail detail = new Book.Model.ProductMouldDetail();
                detail.ProductMouldDetailId = Guid.NewGuid().ToString();
                detail.Mould = productMould;
                detail.MouldId = productMould.MouldId;
                detail.ProductId = productMould.ProductId;
                detail.Description = productMould.MouldName;
                this.product.ProductMouldDetail.Add(detail);
                this.bindingSourceProductMouldDetail.Position = this.bindingSourceProductMouldDetail.IndexOf(detail);
                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.action == "insert" || this.action == "update")
            {

                if (e.KeyData == Keys.Enter)
                {
                    Model.ProductMouldDetail detail = new Book.Model.ProductMouldDetail();
                    this.product.ProductMouldDetail.Add(detail);
                }
                if (e.KeyData == Keys.Delete)
                {
                    //this.simpleButtonRemove.PerformClick();
                }
                this.gridControl1.RefreshDataSource();
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (this.bindingSourceProductMouldDetail.Current != null)
            {
                this.product.ProductMouldDetail.Remove(this.bindingSourceProductMouldDetail.Current as Book.Model.ProductMouldDetail);

                if (this.product.ProductMouldDetail.Count == 0)
                {
                    Model.ProductMouldDetail detail = new Model.ProductMouldDetail();
                    detail.ProductMouldDetailId = "";
                    detail.MouldId = "";
                    detail.ProductId = "";
                    detail.Description = "";
                    this.product.ProductMouldDetail.Add(detail);
                    this.bindingSourceProductMouldDetail.Position = this.bindingSourceProductMouldDetail.IndexOf(detail);
                }

                this.gridControl1.RefreshDataSource();
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.ListSourceRowIndex < 0) return;
            IList<Model.ProductMouldDetail> details = this.bindingSourceProductMouldDetail.DataSource as IList<Model.ProductMouldDetail>;
            if (details == null || details.Count < 1) return;
            Model.ProductMould detail = details[e.ListSourceRowIndex].Mould;
            if (detail == null) return;
            switch (e.Column.Name)
            {
                case "gridColumn4":
                    if (detail == null) return;
                    e.DisplayText = detail.Id;
                    break;
                case "gridColumn7":

                    if (detail.OkTime != null)
                    {
                        if (global::Helper.DateTimeParse.DateTimeEquls(detail.OkTime.Value, global::Helper.DateTimeParse.NullDate))
                            detail.OkTime = null;
                    }
                    e.DisplayText = detail.OkTime == null ? null : detail.OkTime.Value.ToShortDateString();
                    break;
                case "gridColumn6":
                    e.DisplayText = detail.MouldSpecification;
                    break;
            }
        }

        // 複製當前的產品----liuyl
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.action = "insert";

            Model.Product productCope = new Model.Product();
            productCope.ProductId = Guid.NewGuid().ToString();
            if (this.product.ProductCategory != null)
                productCope.Id = this.productManager.GetNewId(this.product.ProductCategory);
            productCope.ProductName = this.product.ProductName;
            productCope.ProductCategory = this.product.ProductCategory;
            productCope.ProductCategoryId = this.product.ProductCategoryId;
            productCope.ProductCategory2 = this.product.ProductCategory2;
            productCope.ProductCategoryId2 = this.product.ProductCategoryId2;
            productCope.ProductCategory3 = this.product.ProductCategory3;
            productCope.ProductCategoryId3 = this.product.ProductCategoryId3;
            productCope.BasedUnitGroup = this.product.BasedUnitGroup;
            productCope.BasedUnitGroupId = this.product.BasedUnitGroupId;
            productCope.BuyUnit = this.product.BuyUnit;
            productCope.BuyUnitId = this.product.BuyUnitId;
            productCope.Depot = this.product.Depot;
            productCope.DepotId = this.product.DepotId;
            productCope.DepotUnit = this.product.DepotUnit;
            productCope.DepotUnitId = this.product.DepotUnitId;
            productCope.ProduceUnit = this.product.ProduceUnit;
            productCope.ProduceUnitId = this.product.ProduceUnitId;
            productCope.SellUnit = this.product.SellUnit;
            productCope.SellUnitId = this.product.SellUnitId;
            productCope.QualityTestUnit = this.product.QualityTestUnit;
            productCope.QualityTestUnitId = this.product.QualityTestUnitId;
            productCope.WeightUnitGroup = this.product.WeightUnitGroup;
            productCope.WeightUnitGroupId = this.product.WeightUnitGroupId;
            productCope.WeightUnit = this.product.WeightUnit;
            productCope.WeightUnitId = this.product.WeightUnitId;
            productCope.HomeMade = this.product.HomeMade;
            productCope.OutSourcing = this.product.OutSourcing;
            productCope.TrustOut = this.product.TrustOut;
            productCope.Consume = this.product.Consume;
            productCope.ProductBarCodeIsAuto = false;
            product.ProductBarCode = string.Empty;

            //if (product.ProductBarCodeIsAuto == null || (bool)product.ProductBarCodeIsAuto)
            //{
            //    product.ProductBarCodeIsAuto = false;
            //    product.ProductBarCode = string.Empty;
            //}          
            this.radioGroupBarCode.SelectedIndex = 0;
            this.product = productCope;
            this.Refresh();
        }

        public static Model.Product _product;

        //搜索
        private void barButtonItemSeach_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!productManager.HasRows())
            {
                MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            Invoices.ChooseProductForm f = new Invoices.ChooseProductForm();

            if (f.ShowDialog(this) == DialogResult.OK)
            {
                this.product = f.SelectedItem as Model.Product;
                this.product = this.productManager.Get(this.product.ProductId);
                this.action = "view";
                this.Refresh();
                GC.Collect();
                f.Dispose();
            }
        }

        private void simpleButton_search_Click(object sender, EventArgs e)
        {
            if (this.product == null) return;
            this.bindingSourceInvoiceCoDetail.DataSource = this.invoiceCoDetailManager.SelectByDateRangeAndPid(this.product.ProductId, this.dateEditcostartdate.DateTime, this.dateEditcoenddate.DateTime);
            this.gridControl3.RefreshDataSource();
        }

        private void simpleButtonMation_Click(object sender, EventArgs e)
        {
            if (this.product == null) return;
            IList<Model.InvoiceXODetail> halfwayList = this.invoiceXoDetailManager.SelectByDateRangeAndPid(this.product.ProductId, this.dateEditxostartdate.DateTime, this.dateEditxoenddate.DateTime);

            BL.InvoiceXSManager ixsm = new Book.BL.InvoiceXSManager();
            IList<Model.InvoiceXS> ixslist;
            foreach (Model.InvoiceXODetail item in halfwayList)
            {
                ixslist = ixsm.SelectDateRangAndWhere(null, null, this.dateEditxostartdate.DateTime, this.dateEditxoenddate.DateTime, global::Helper.DateTimeParse.NullDate, global::Helper.DateTimeParse.EndDate, null, item.Product, item.InvoiceId, null);
                item.InvoiceProductUnit = "";
                foreach (Model.InvoiceXS s in ixslist)
                {
                    item.InvoiceProductUnit += s.InvoiceId + ",";
                }
            }
            this.bindingSourceXoDetial.DataSource = halfwayList;
            this.gridControl4.RefreshDataSource();
        }

        private string picfilename = string.Empty;

        private void simpleButtonselectpic_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                picfilename = this.openFileDialog1.FileName;
                if (System.IO.File.Exists(picfilename))
                {
                    this.pictureEditPrivew.Image = Image.FromFile(picfilename);
                    this.textEditDescription.Text = "";
                }
                else
                {
                    MessageBox.Show(this, "fileNotFound", "文件不存在！", MessageBoxButtons.OK);
                    return;
                }
            }
        }

        private void simpleButtonsave_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(picfilename))
            {
                MessageBox.Show(Properties.Resources.ProductImageIsNull, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FileInfo f = new FileInfo(picfilename);
            productImage = new Model.ProductImage();
            productImage.ImageId = Guid.NewGuid().ToString();
            productImage.Description = this.textEditDescription.Text;
            productImage.Images = File.ReadAllBytes(picfilename);
            productImage.InsertTime = System.DateTime.Now;
            productImage.ProductId = this.product == null ? "" : this.product.ProductId;
            productImage.SuffixalName = f.Extension.ToLower();
            this.productImageManager.Insert(productImage);
            MessageBox.Show(Properties.Resources.SuccessfullySaved, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            simpleButtonlast.Enabled = true;
            simpleButtonNext.Enabled = true;
        }

        private void simpleButtonlast_Click(object sender, EventArgs e)
        {
            Model.ProductImage image = this.productImageManager.GetPrevs(this.productImage);
            if (image != null)
                simpleButtonNext.Enabled = true;
            if (this.productImageManager.GetPrevs(image) == null)
                simpleButtonlast.Enabled = false;
            //simpleButtonNext.Enabled = true;


            this.productImage = image;
            NewMethod();
        }

        private void NewMethod()
        {
            if (productImage != null)
            {
                this.textEditDescription.Text = productImage.Description;
                MemoryStream ms = new MemoryStream(productImage.Images);
                this.pictureEditPrivew.Image = Image.FromStream(ms);
                ms.Close();
            }
        }

        private void simpleButtonNext_Click(object sender, EventArgs e)
        {
            Model.ProductImage image = this.productImageManager.GetNexts(this.productImage);
            if (image != null)
                simpleButtonlast.Enabled = true;
            if (this.productImageManager.GetNexts(image) == null)
                //simpleButtonlast.Enabled = true;
                simpleButtonNext.Enabled = false;


            this.productImage = image;
            NewMethod();
        }

        private void simpleButtonDelete_Click(object sender, EventArgs e)
        {
            if (this.productImage != null)
            {
                this.productImageManager.Delete(this.productImage.ImageId);
                Model.ProductImage image = this.productImageManager.GetLasts(this.productImage == null ? null : this.productImage.ProductId);
                if (image != null)
                {
                    this.productImage = image;
                    NewMethod();
                }
                else
                {
                    image = this.productImageManager.GetLasts(this.productImage == null ? null : this.productImage.ProductId);
                    if (image != null)
                    {
                        this.productImage = image;
                        NewMethod();
                    }
                }
            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (this.product != null && this.product.ProductId != null)
            {
                if (xtraTabControl1.SelectedTabPage == this.xtraTabPageStock)
                {
                    //this.gridControlStock.DataSource = this.stockManager.SelectReaderByPro(this.product.ProductId, this.dateEditStockStart.DateTime, this.dateEditStockEnd.DateTime);

                }
            }
        }

        //库存记录
        private void simpleButtonStock_Click(object sender, EventArgs e)
        {
            double? a = 0;
            int tag = 0;
            IList<Model.StockSeach> list = new List<Model.StockSeach>();
            DateTime dateStart = string.IsNullOrEmpty(this.dateEditStockStart.Text) ? DateTime.Now.Date.AddMonths(-1) : this.dateEditStockStart.DateTime;
            DateTime dateEnd = string.IsNullOrEmpty(this.dateEditStockEnd.Text) ? DateTime.Now.Date.AddDays(1).AddSeconds(-1) : this.dateEditStockEnd.DateTime;
            list = this.stockManager.SelectReaderByPro(this.product.ProductId, dateStart, dateEnd);
            list = list.OrderBy(s => s.InvoiceDate).ToList();
            foreach (var item in list)
            {
                if (item.InvoiceType == "生產入庫單" && string.IsNullOrEmpty(item.PositionName))
                {
                    item.StockCheckBookQuantity = Math.Round(Convert.ToDouble(this.calcEditStock0.EditValue), 4) + Convert.ToDouble(a);
                    continue;
                }
                if (item.InvoiceType == "盤點核准單")
                {
                    //item.StockCheckBookQuantity = Math.Round(Convert.ToDouble(item.InvoiceQuantity), 4);
                    item.StockCheckBookQuantity = Math.Round(Convert.ToDouble(item.StockCheckBookQuantity), 4) + Math.Round(Convert.ToDouble(this.calcEditStock0.EditValue), 4) + Convert.ToDouble(a);
                    //a = item.StockCheckBookQuantity;
                    //tag = 1;
                }
                //else
                //{
                //    if (tag == 1)
                //    {
                //        item.StockCheckBookQuantity = Math.Round(Convert.ToDouble(item.StockCheckBookQuantity), 4) + Convert.ToDouble(a);
                //        tag = 0;
                //    }
                else
                    item.StockCheckBookQuantity = Math.Round(Convert.ToDouble(item.StockCheckBookQuantity), 4) + Math.Round(Convert.ToDouble(this.calcEditStock0.EditValue), 4) + Convert.ToDouble(a);
                //}
                a = item.StockCheckBookQuantity - Math.Round(Convert.ToDouble(this.calcEditStock0.EditValue), 4);
            }
            this.gridControlStock.DataSource = list;
            this.calcEditStock1.EditValue = productManager.getStockByProduct(this.product.ProductId);
        }

        private void gridView3_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //if (e.ListSourceRowIndex < 0) return;
            //IList<Model.ProductProcess> details = this.bindingSourceProductProcess.DataSource as IList<Model.ProductProcess>;
            //if (details == null || details.Count < 1) return;
            //Model.Procedures detail = details[e.ListSourceRowIndex].Procedures;
            //if (detail == null) return;
            //switch (e.Column.Name)
            //{
            //    case "gridColumnId":
            //        if (detail == null) return;
            //        e.DisplayText = detail.Id;
            //        break;
            //    case "gridColumnWH":
            //        if (detail == null) return;
            //        e.DisplayText = detail.WorkHouse.Workhousename;
            //        break;
            //}
        }

        private void barButtonItemList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!productManager.HasRows())
            //{
            //    MessageBox.Show(Properties.Resources.NoData, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;

            //}
            List1Form f = new List1Form();
            //if (f.ShowDialog(this) == DialogResult.OK)
            //{
            //    this.product = _product;
            //    this.action = "view";
            //    this.Refresh();

            //}
            f.Show();
        }
        //SqlDataReader sd;
        //private void treeList1_Click(object sender, EventArgs e)
        //{
        //this.IsDisposed || !this.IsCreateHandle
        //   return;

        //    try
        //    {
        //        string a = treeList1.Selection[0].Tag.ToString();
        //        if (treeList1.Selection[0].ParentNode == null && treeList1.Selection[0].Nodes != null && treeList1.Selection[0].Nodes.Count == 0)
        //        {
        //          //    Cursors.WaitCursor; 
        //            TreeListNode seNode = treeList1.Selection[0];
        //            SqlParameter[] par = new SqlParameter[] { new SqlParameter("@productCategoryid", SqlDbType.VarChar, 50) };
        //            par[0].Value = a;
        //            string sql = "SELECT productid,productname,isnull( CustomerProductName,'') as CustomerProductName,isnull( IsCustomerProduct,0) as IsCustomerProduct,isnull( ProductVersion,'') as ProductVersion FROM product WHERE  productCategoryid=@productCategoryid order by productname";

        //            sd = productManager.ExecuteReader(sql, par);
        //            if (sd == null || !sd.HasRows) return;



        //                  while (sd.Read())
        //                  {

        //                      treeList1.Invoke((MethodInvoker)delegate()
        //                      {
        //                          treeList1.AppendNode(new object[] { (sd.GetBoolean(3) == true ? sd.GetString(1) + "{" + sd.GetString(2) + "}" : sd.GetString(1)) + (string.IsNullOrEmpty(sd.GetString(4)) ? "" : "-" + sd.GetString(4)) }, seNode, sd.GetString(0));

        //                      });
        //                  }
        //                  sd.Close();
        //                  sd.Dispose();
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        if (sd != null || sd.HasRows)
        //        {
        //            sd.Close();
        //            sd.Dispose();
        //        }
        //        throw ex;
        //    }

        //}

        private void simpleBProceduresDelete_Click(object sender, EventArgs e)
        {
            // if (MessageBox.Show(Properties.Resources.ConfirmToDelete, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)          
            //     return;
            // Model.ProductProcess proce= this.bindingSourceProductProcess.Current as Model.ProductProcess;
            // if(proce==null) return;
            // this.product.ProductProcess.Remove(proce);


            // this.memoEditProductDescription.Clear();
            // if (this.product.IsProcee == null || !this.product.IsProcee.Value)
            //     this.memoEditProductDescription.Rtf = this.product.ProductDescription;
            // else
            //     this.memoEditProductDescription.Rtf = this.productManager.Get(this.product.ProceebeforeProductId).ProductDescription;


            // if (this.memoEditProductDescription.TextLength != 0)
            //     this.memoEditProductDescription.AppendText(";");
            // // richTextBoxProcess.Clear();
            // string str = string.Empty;
            //// string name = string.Empty;
            // try
            // {
            //     int indexNo = product.ProductProcess.Count;
            //     foreach (Model.ProductProcess productProcess in product.ProductProcess)
            //     {
            //         // this.memoEditProductDescription.WordWrap = false;
            //       //  str += "-" + productProcess.Procedures.Id;



            //         //商品描述加 加工工序
            //         //RichTextBox richTextBox = new RichTextBox();
            //         //richTextBox.Rtf = procedures.Procedurename;
            //         //richTextBox.SelectAll();

            //         //richTextBox.Copy();
            //         //this.memoEditProductDescription.Paste();
            //         //Clipboard.Clear();
            //         ////this.memoEditProductDescription.SelectionStart = this.memoEditProductDescription.Text.Length;
            //         ////string contentrtf = productProcess.Procedures.Procedurename;
            //         ////this.memoEditProductDescription.SelectedRtf = contentrtf.Remove(contentrtf.LastIndexOf(@"\par"), 4);
            //         ////if (product.ProductProcess.IndexOf(productProcess) == product.ProductProcess.Count - 1)
            //         ////    break;
            //         ////this.memoEditProductDescription.AppendText(",");
            //     }

            //     foreach (Model.ProductProcess productProcess in product.ProductProcess)
            //     {
            //         this.memoEditProductDescription.SelectionStart = this.memoEditProductDescription.Text.Length;
            //         string contentrtf = productProcess.Procedures.Procedurename;
            //         this.memoEditProductDescription.SelectedRtf = contentrtf.Remove(contentrtf.LastIndexOf(@"\par"), 4);
            //         if (product.ProductProcess.IndexOf(productProcess) == product.ProductProcess.Count - 1)
            //             break;
            //         this.memoEditProductDescription.AppendText(",");
            //     }
            //     // this.bindingSourceProductProcess.DataSource = product.ProductProcess;
            //     this.gridControl2.RefreshDataSource();
            //     if (product.IsProcee == true && !string.IsNullOrEmpty(this.textEditProductName.Text))
            //     {
            //         this.buttonEditTechonProcedures.Text = this.textEditProductName.Text;
            //         this.textEditProceduresId.Text = this.textEditId.Text;
            //     }
            //     else
            //     {
            //         foreach (Model.ProductProcess productProcess in product.ProductProcess)
            //         {
            //             str += "-" + productProcess.Procedures.Id;
            //         }
            //         this.buttonEditTechonProcedures.Text = this.product.IsProcee == true ? this.productManager.Get(this.product.ProceebeforeProductId).ProductName + str : this.product.ProductName + str;
            //         this.textEditProceduresId.Text = this.product.IsProcee == true ? this.productManager.Get(this.product.ProceebeforeProductId).Id + str : this.product.Id + str;
            //     }
            // }
            // catch
            // {

            // }
            // this.gridControl2.RefreshDataSource();
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //    if (sd != null || sd.HasRows)
            //    {
            //        sd.Close();
            //        sd.Dispose();
            //    }
        }

        private void grid5Click()
        {

            //GridHitInfo hitInfo =gridView5.CalcHitInfo(gridControl5.PointToClient(Cursor.Position));
            //if (hitInfo.InRow && !gridView5.IsGroupRow(hitInfo.RowHandle))
            //{
            //设置商品类别
            if (this.gridView5.RowCount > 0)
            {

                this.action = "view";
                string id = this.gridView5.GetRowCellValue(this.gridView5.FocusedRowHandle, this.gridColumn27).ToString();



                this.product = productManager.Get(id);
                this.productImage = productImageManager.GetFirsts(this.product.ProductId);

                if (xtraTabControl1.SelectedTabPage == this.xtraTabPageStock)
                {
                    //this.gridControlStock.DataSource = this.stockManager.SelectReaderByPro(this.product.ProductId, this.dateEditStockStart.DateTime, this.dateEditStockEnd.DateTime);
                    this.calcEditStock0.EditValue = this.stockManager.SelectStockQuantity0(this.product.ProductId);
                    this.calcEditStock1.EditValue = this.product.StocksQuantity;
                    //this.simpleButtonStock_Click(null, null);
                }

                if (xtraTabControl1.SelectedTabPage == this.xtraTabPage10)
                {
                    this.simpleButton_search.PerformClick();
                }
                else if (xtraTabControl1.SelectedTabPage == this.xtraTabPage11)
                {
                    this.simpleButtonMation.PerformClick();
                }
            }
        }

        private void gridView5_Click(object sender, EventArgs e)
        {
            GridHitInfo hitInfo = gridView5.CalcHitInfo(gridControl5.PointToClient(Cursor.Position));
            if (hitInfo.InRow && !gridView5.IsGroupRow(hitInfo.RowHandle))
            {
                grid5Click();
                this.Refresh();
            }
        }

        //private void listBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (flag5 == 1)
        //    {
        //        flag5 = 0;
        //        listBoxControl1.SelectedIndex = -1;
        //    }
        //    else
        //    {

        //        if (listBoxControl1.SelectedIndex >= 0)
        //        {
        //            SqlParameter[] pars = new SqlParameter[] { new SqlParameter("@productid", SqlDbType.VarChar, 50) };
        //            pars[0].Value = (listBoxControl1.SelectedItem as Model.ProductCategory).ProductCategoryId;

        //            this.gridControl5.DataSource = productManager.QueryProc("GetProductListByCate", pars, "producttree").Tables[0];

        //            cateTemp = listBoxControl1.SelectedItem as Model.ProductCategory;
        //            if (this.action == "insert")
        //            {

        //                this.product.ProductCategory = listBoxControl1.SelectedItem as Model.ProductCategory;
        //                this.newChooseContorlProductType.EditValue = this.product.ProductCategory;
        //                this.textEditId.Text = this.productManager.GetNewId(this.product.ProductCategory);

        //            }
        //        }
        //    }
        //    ;
        //}

        //private void checkedComboBoxEditJWeight_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (checkedComboBoxEditJWeight.EditValue != null && checkedComboBoxEditJWeight.EditValue != "")
        //    {
        //        string materialIds = "(";
        //        string[] strs = checkedComboBoxEditJWeight.EditValue.ToString().Split(',');
        //        foreach (string str in strs)
        //        {
        //            materialIds += "'" + str.Trim() + "',";
        //        }
        //        materialIds = materialIds.Substring(0, materialIds.Length - 1) + ")";

        //        this.spinEditNetWeight.EditValue = this.materialManager.CountJWeightByMaterial(materialIds);
        //    }

        //}



        // 净重-选择原料
        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            //string str2 = "(";
            double? Jweight = 0;
            this.materialIds = string.Empty;
            this.materialNum = string.Empty;
            ProductCategories.ChooseMaterialForm f = new Book.UI.Settings.BasicData.ProductCategories.ChooseMaterialForm();
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                if (f.Key != null && f.Key.Count > 0)
                {
                    foreach (Model.Material model in f.Key)
                    {
                        str += model.Id + "*" + model.Num.ToString() + "+";
                        //str2 += "'" + model.MaterialId + "',";
                        Jweight += model.JWeight == null ? 0 : model.JWeight * model.Num;
                        this.materialIds += model.MaterialId + ",";
                        this.materialNum += model.Num + ",";
                    }
                    this.buttonEdit1.EditValue = str.Substring(0, str.Length - 1);
                    //str2 = str2.Substring(0, str2.Length - 1) + ")";
                    //this.spinEditNetWeight.EditValue = this.materialManager.CountJWeightByMaterial(str2);
                    this.spinEditNetWeight.EditValue = Jweight;
                    this.materialIds = this.materialIds.Substring(0, this.materialIds.Length - 1);
                    this.materialNum = this.materialNum.Substring(0, this.materialNum.Length - 1);
                }
            }
        }

        protected override string tableCode()
        {
            return "Product" + "," + this.product.Id;
        }

        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Microsoft Excel(*.xlsx)|*.xlsx";
            fileDialog.Title = "导出文件名";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                gridControlStock.ExportToXlsx(fileDialog.FileName);
                MessageBox.Show("导出成功！", this.Text, MessageBoxButtons.OK);
            }
        }

        private void nccProductCategoryTwo_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string s = (this.newChooseContorlProductType.EditValue as Model.ProductCategory) == null ? null : (this.newChooseContorlProductType.EditValue as Model.ProductCategory).ProductCategoryId;
            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("请先选择商品类别");
                return;
            }
            Settings.BasicData.ProductCategories.ChooseForm f = new Settings.BasicData.ProductCategories.ChooseForm("2", s);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                nccProductCategoryTwo.EditValue = f.SelectedItem;
                this.product.ProductCategory2 = (this.nccProductCategoryTwo.EditValue as Model.ProductCategory);

                Model.ProductCategory category3 = this.nccProductCategoryThree.EditValue as Model.ProductCategory;
                if (category3 != null)
                {
                    IList<Model.ProductCategory> list3 = productCategoryManager.SelectListByFilter("3", this.product.ProductCategory2.ProductCategoryId);
                    if (!list3.Any(L => L.ProductCategoryId == category3.ProductCategoryId))
                    {
                        this.nccProductCategoryThree.EditValue = null;
                    }
                }
            }
        }

        private void nccProductCategoryThree_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string s = (this.nccProductCategoryTwo.EditValue as Model.ProductCategory) == null ? null : (this.nccProductCategoryTwo.EditValue as Model.ProductCategory).ProductCategoryId;
            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("请先选择二级商品类别");
                return;
            }
            Settings.BasicData.ProductCategories.ChooseForm f = new Settings.BasicData.ProductCategories.ChooseForm("3", s);
            if (f.ShowDialog(this) == DialogResult.OK)
            {
                nccProductCategoryThree.EditValue = f.SelectedItem;
                this.product.ProductCategory2 = (this.nccProductCategoryThree.EditValue as Model.ProductCategory);
            }
        }

        //点击商品类型，加载商品
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (flag5 == 1)
            {
                flag5 = 0;
                return;
            }
            else
            {
                if (treeList1.FocusedNode != null)
                {
                    SqlParameter[] pars = new SqlParameter[] { new SqlParameter("@productid", SqlDbType.VarChar, 50) };
                    Model.ProductCategory category = treeList1.FocusedNode.Tag as Model.ProductCategory;
                    pars[0].Value = category.ProductCategoryId;
                    if (category.CategoryLevel == 1)
                    {
                        //this.gridControl5.DataSource = productManager.QueryProc("GetProductListByCate1", pars, "producttree").Tables[0];
                        //点击大类，显示该类别下所有商品
                        this.gridControl5.DataSource = productManager.QueryProc("GetProductListByCate", pars, "producttree").Tables[0];
                    }
                    else if (category.CategoryLevel == 2)
                        this.gridControl5.DataSource = productManager.QueryProc("GetProductListByCate2", pars, "producttree").Tables[0];
                    else
                        this.gridControl5.DataSource = productManager.QueryProc("GetProductListByCate3", pars, "producttree").Tables[0];

                    cateTemp = treeList1.FocusedNode.Tag as Model.ProductCategory;
                }
            }

        }
    }
}