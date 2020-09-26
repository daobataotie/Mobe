//------------------------------------------------------------------------------
//
// file name：ProductManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Product.
    /// </summary>
    public partial class ProductManager : BaseManager
    {
        private static readonly DA.IProductMouldDetailAccessor ProductMouldDetailAccessor = (DA.IProductMouldDetailAccessor)Accessors.Get("ProductMouldDetailAccessor");
        private static readonly DA.IProductProcessAccessor ProductProcessAccessor = (DA.IProductProcessAccessor)Accessors.Get("ProductProcessAccessor");
        private static readonly DA.IBomParentPartInfoAccessor bomParentPartInfoAccessor = (DA.IBomParentPartInfoAccessor)Accessors.Get("BomParentPartInfoAccessor");

        /// <summary>
        /// Delete Product by primary key.
        /// </summary>
        public void Delete(string productId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(productId);
        }

        public void Delete(Model.Product product)
        {

            try
            {
                V.BeginTransaction();
                this.Delete(product.ProductId);
                //if (product.IsCustomerProduct != null)
                //{
                //    if ((bool)product.IsCustomerProduct && product.Customer != null)
                //    {
                //        bomParentPartInfoAccessor.DeleteByInProductCustomer(product, product.Customer);
                //    }
                //    else
                //    {
                bomParentPartInfoAccessor.Delete(product);
                //    }
                //}
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;

            }

        }

        private void TiGuiExistsUpdate(Model.Product product)
        {

            //     string id = this.GetNewId(product.ProductCategory);
            if (this.ExistsExcept(product))
            {
                //设置KEY值
                string sequencekey = product.ProductCategory.Id;
                SequenceManager.Increment(sequencekey);
                product.Id = this.GetNewId(product.ProductCategory);
                TiGuiExistsUpdate(product);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }
        private void TiGuiExists(Model.Product product)
        {

            //     string id = this.GetNewId(product.ProductCategory);
            if (this.Exists(product.Id))
            {
                //设置KEY值
                string sequencekey = product.ProductCategory.Id;
                SequenceManager.Increment(sequencekey);
                product.Id = this.GetNewId(product.ProductCategory);
                TiGuiExists(product);
                //throw new Helper.InvalidValueException(Model.Product.PRO_Id);               
            }

        }


        /// <summary>
        /// Insert a Product.
        /// </summary>
        public void Insert(Model.Product product)
        {

            this.Validate(product);
            //if (this.Exists(product.Id))
            //{
            //throw new Helper.InvalidValueException(Model.Product.PRO_Id);
            TiGuiExists(product);
            //  }

            if (this.ExistsNameInsert(product.ProductName))
            {
                throw new Helper.InvalidValueException(Model.Product.PRO_ProductName);
            }

            //设置KEY值
            string sequencekey = product.ProductCategory.Id;
            SequenceManager.Increment(sequencekey);

            StringBuilder strBU = new StringBuilder();

            string PrId = Guid.NewGuid().ToString();
            product.ProductId = PrId;
            if (product.BasedUnitGroup != null) product.BasedUnitGroupId = product.BasedUnitGroup.UnitGroupId;
            if (product.BuyEmployee != null) product.BuyEmployeeId = product.BuyEmployee.EmployeeId;
            if (product.BuyUnit != null) product.BuyUnitId = product.BuyUnit.ProductUnitId;
            if (product.CustomInspectionRule != null) product.CustomInspectionRuleId = product.CustomInspectionRule.CustomInspectionRuleId;
            if (product.Depot != null) product.DepotId = product.Depot.DepotId;
            if (product.DepotPosition != null) product.DepotPositionId = product.DepotPosition.DepotPositionId;
            if (product.DepotUnit != null) product.DepotUnitId = product.DepotUnit.ProductUnitId;
            //if (product.InsteadOfProductId != null) product.InsteadOfProductId = product.InsteadOfProduct.ProductId;
            if (product.MainUnit != null) product.MainUnitId = product.MainUnit.ProductUnitId;
            // if (product.PackageType != null) product.PackageTypeId = product.PackageType.PackageTypeId;
            if (product.ProduceUnit != null) product.ProduceUnitId = product.ProduceUnit.ProductUnitId;
            if (product.ProductCategory != null) product.ProductCategoryId = product.ProductCategory.ProductCategoryId;
            if (product.QualityTestPlan != null) product.QualityTestPlanId = product.QualityTestPlan.QualityTestPlanId;
            if (product.QualityTestUnit != null) product.QualityTestUnitId = product.QualityTestUnit.ProductUnitId;
            if (product.SellUnit != null) product.SellUnitId = product.SellUnit.ProductUnitId;
            // if (product.Supplier != null) product.SupplierId = product.Supplier.SupplierId;
            product.SupplierId = product.Supplier == null ? null : product.Supplier.SupplierId;
            if (product.VolumeUnit != null) product.VolumeUnitId = product.VolumeUnit.ProductUnitId;
            if (product.VolumeUnitGroup != null) product.VolumeUnitGroupId = product.VolumeUnitGroup.UnitGroupId;
            if (product.WeightUnit != null) product.WeightUnitId = product.WeightUnit.ProductUnitId;
            if (product.WeightUnitGroup != null) product.WeightUnitGroupId = product.WeightUnitGroup.UnitGroupId;

            if (product.ProductCategory != null) product.ProductCategoryId = product.ProductCategory.ProductCategoryId;
            if (product.ProductCategory2 != null) product.ProductCategoryId2 = product.ProductCategory2.ProductCategoryId;
            if (product.ProductCategory3 != null) product.ProductCategoryId3 = product.ProductCategory3.ProductCategoryId;

            product.InsertTime = DateTime.Now;
            //product.UpdateTime = DateTime.Now;

            accessor.Insert(product);
            //ProductProcessAccessor.Delete(product);
            foreach (Model.ProductMouldDetail Pro in product.ProductMouldDetail)
            {
                if (string.IsNullOrEmpty(Pro.ProductMouldDetailId)) continue;
                Pro.ProductId = product.ProductId;
                ProductMouldDetailAccessor.Insert(Pro);
            }


            //foreach (Model.ProductProcess Pro in product.ProductProcess)
            //{
            //    if (string.IsNullOrEmpty(Pro.ProcessCategoryId)) continue;
            //    //strBU.Append(Pro.ProcessCategory.ProcessCategoryName);
            //    Pro.ProductProcessId = Guid.NewGuid().ToString();
            //    Pro.ProductId = product.ProductId;
            //    Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;

            //    Pro.ProcessCategoryId = Pro.ProcessCategory == null ? null : Pro.ProcessCategory.ProcessCategoryId;
            //    ProductProcessAccessor.Insert(Pro);
            //}



            //加工后名称添加
            ////if (product.ProductProcess!=null)
            ////{
            ////    if (product.ProductProcess.Count > 0)
            ////    {
            ////        product.ProductId = Guid.NewGuid().ToString();
            ////        ////product.ProductName = product.ProProcessName;0827
            ////        ProductProcessAccessor.Delete(product);
            ////        product.ProceebeforeProductId = PrId;
            ////        product.IsProcee = true;
            ////        accessor.Insert(product);
            ////        foreach (Model.ProductProcess Pro in product.ProductProcess)
            ////        {
            ////            if (Pro.ProcessCategory == null) continue;
            ////            Pro.ProductProcessId = Guid.NewGuid().ToString();
            ////            Pro.ProductId = product.ProductId;
            ////            Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;
            ////            Pro.ProcessCategoryId = Pro.ProcessCategory == null ? null : Pro.ProcessCategory.ProcessCategoryId;
            ////            ProductProcessAccessor.Insert(Pro);
            ////        }
            ////    }
            ////}
            //}
        }

        public IList<Model.Product> SelectByProductIds(string productIds)
        {
            return accessor.SelectByProductIds(productIds);
        }

        private void Validate(Model.Product product)
        {
            if (product.ProductCategory == null)
            {
                throw new Helper.RequireValueException(Model.Product.PRO_ProductCategoryId);
            }
            if (string.IsNullOrEmpty(product.Id))
            {
                throw new Helper.RequireValueException(Model.Product.PRO_Id);
            }
            if (string.IsNullOrEmpty(product.ProductName))
            {
                throw new Helper.RequireValueException(Model.Product.PRO_ProductName);
            }
            if (product.BasedUnitGroup == null)
            {
                throw new Helper.RequireValueException(Model.Product.PRO_BasedUnitGroupId);
            }
            if (product.DepotUnit == null)
            {
                throw new Helper.RequireValueException(Model.Product.PRO_DepotUnitId);
            }
        }

        public bool ExistsNameInsert(string productName)
        {
            return accessor.ExistsNameInsert(productName);
        }

        public bool ExistsNameUpdate(Model.Product product)
        {
            return accessor.ExistsNameUpdate(product);
        }

        protected override string GetInvoiceKind()
        {
            return null;
        }
        protected override string GetSettingId()
        {
            return base.GetSettingId();
        }
        /// <summary>
        /// Update a Product.
        /// </summary>
        /// 


        public void Update(Model.Product product)
        {
            try
            {

                this.Validate(product);
                TiGuiExistsUpdate(product);
                if (this.ExistsNameUpdate(product) && (product.IsCustomerProduct == null || !product.IsCustomerProduct.Value))
                {
                    throw new Helper.InvalidValueException(Model.Product.PRO_ProductName);
                }
                V.BeginTransaction();
                //if (!string.IsNullOrEmpty(product.ProProcessName))
                //{
                //    if (product.IsProcee == true)
                //    {
                //        if (this.ExistsNameUpdate(product))
                //        {
                //            throw new Helper.InvalidValueException(Model.Product.PRO_ProductName);
                //        }
                //    }
                //}

                StringBuilder strBU = new StringBuilder();
                //string PrId = product.ProductId;
                //string name = product.ProductName;
                //double? stock = product.StocksQuantity;
                //string text = product.ProductProcessDescription;
                ////bool? result = product.IsProcee;
                ////string proceebeforeProductId = product.ProceebeforeProductId;
                //string Id = product.Id;
                //string ProductBarCode = product.ProductBarCode;
                //bool? ProductBarCodeIsAuto = product.ProductBarCodeIsAuto;

                if (product.BasedUnitGroup != null) product.BasedUnitGroupId = product.BasedUnitGroup.UnitGroupId;
                if (product.BuyEmployee != null) product.BuyEmployeeId = product.BuyEmployee.EmployeeId;
                if (product.BuyUnit != null) product.BuyUnitId = product.BuyUnit.ProductUnitId;
                if (product.CustomInspectionRule != null) product.CustomInspectionRuleId = product.CustomInspectionRule.CustomInspectionRuleId;
                if (product.Depot != null) product.DepotId = product.Depot.DepotId;
                if (product.DepotPosition != null) product.DepotPositionId = product.DepotPosition.DepotPositionId;
                if (product.DepotUnit != null) product.DepotUnitId = product.DepotUnit.ProductUnitId;
                //if (product.InsteadOfProduct != null) product.InsteadOfProductId = product.InsteadOfProduct.ProductId;
                if (product.MainUnit != null) product.MainUnitId = product.MainUnit.ProductUnitId;
                //  if (product.PackageType != null) product.PackageTypeId = product.PackageType.PackageTypeId;
                if (product.ProduceUnit != null) product.ProduceUnitId = product.ProduceUnit.ProductUnitId;
                if (product.ProductCategory != null) product.ProductCategoryId = product.ProductCategory.ProductCategoryId;
                if (product.QualityTestPlan != null) product.QualityTestPlanId = product.QualityTestPlan.QualityTestPlanId;
                if (product.QualityTestUnit != null) product.QualityTestUnitId = product.QualityTestUnit.ProductUnitId;
                if (product.SellUnit != null) product.SellUnitId = product.SellUnit.ProductUnitId;
                //if (product.Supplier != null) product.SupplierId = product.Supplier.SupplierId;
                product.SupplierId = product.Supplier == null ? null : product.Supplier.SupplierId;
                if (product.VolumeUnit != null) product.VolumeUnitId = product.VolumeUnit.ProductUnitId;
                if (product.VolumeUnitGroup != null) product.VolumeUnitGroupId = product.VolumeUnitGroup.UnitGroupId;
                if (product.WeightUnit != null) product.WeightUnitId = product.WeightUnit.ProductUnitId;
                if (product.WeightUnitGroup != null) product.WeightUnitGroupId = product.WeightUnitGroup.UnitGroupId;

                //if (product.ProductImage == null)
                //    product.ProductImage = pic;
                //if (product.ProductImage1 == null)
                //    product.ProductImage1 = pic;
                //if (product.ProductImage2 == null)
                //    product.ProductImage2 = pic;
                //if (product.ProductImage3 == null)
                //    product.ProductImage3 = pic;

                if (product.ProductCategory != null) product.ProductCategoryId = product.ProductCategory.ProductCategoryId;
                if (product.ProductCategory2 != null)
                    product.ProductCategoryId2 = product.ProductCategory2.ProductCategoryId;
                else
                    product.ProductCategoryId2 = null;
                if (product.ProductCategory3 != null)
                    product.ProductCategoryId3 = product.ProductCategory3.ProductCategoryId;
                else
                    product.ProductCategoryId3 = null;

                product.UpdateTime = DateTime.Now;
                //Model.Product Prdt = product;



                //if (product.IsProcee == true && product.ProductProcess.Count > 0)
                //{
                //    product.Id = string.IsNullOrEmpty(product.ProceId) ? product.Id : product.ProceId;
                //    // product.ProductBarCode = string.IsNullOrEmpty(product.ProceId) ? null : product.ProceId;
                //    product.ProductName = string.IsNullOrEmpty(product.ProProcessName) ? product.ProductName : product.ProProcessName;
                //    product.ProductDescription = product.ProductProcessDescription;
                //}
                //if ((product.IsProcee == null || product.IsProcee == false) && product.ProductProcess.Count > 0)
                //    product.ProductDescription = accessor.Get(product.ProductId).ProductDescription;
                accessor.Update(product);

                ProductMouldDetailAccessor.Delete(product);
                foreach (Model.ProductMouldDetail Pro in product.ProductMouldDetail)
                {
                    if (string.IsNullOrEmpty(Pro.ProductMouldDetailId)) continue;
                    Pro.ProductId = product.ProductId;
                    ProductMouldDetailAccessor.Insert(Pro);
                }
                // ProductProcessAccessor.Delete(product);
                //foreach (Model.ProductProcess Pro in product.ProductProcess)
                //{
                //    if (Pro.ProcessCategory == null) continue;
                //    //strBU.Append(Pro.ProcessCategory.ProcessCategoryName);
                //    Pro.ProductProcessId = Guid.NewGuid().ToString();
                //    Pro.ProductId = product.ProductId;
                //    Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;
                //    Pro.ProcessCategoryId = Pro.Procegory == null ? null : Pro.ProcessCategory.ProcessCategoryId;
                //    ProductProcessAccessor.Insert(Pro);essCat
                //}

                //加工后名称添加

                //foreach (Model.ProductProcess Pro in product.ProductProcess)
                //{
                //    if (Pro.ProcessCategory == null) continue;
                //    product.ProceebeforeProductId = PrId;
                //    product.IsProcee = true;
                //    //string a=Pro.Product.ProductName;
                //    if (Pro.ProductId == product.ProductId && Pro.Product!=null)
                //    {
                //        accessor.Update(product);
                //        Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;

                //        Pro.ProcessCategoryId = Pro.ProcessCategory == null ? null : Pro.ProcessCategory.ProcessCategoryId;
                //        Pro.ProcessProductName = product.ProductName;
                //        ProductProcessAccessor.Update(Pro);
                //    }
                //    else
                //    {
                //        product.ProductId = Guid.NewGuid().ToString();
                //        product.ProductName = Pro.ProcessProductName;
                //        accessor.Insert(product);

                //        Pro.ProductProcessId = Guid.NewGuid().ToString();
                //        Pro.ProductId = product.ProductId;
                //        Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;

                //        Pro.ProcessCategoryId = Pro.ProcessCategory == null ? null : Pro.ProcessCategory.ProcessCategoryId;

                //        ProductProcessAccessor.Insert(Pro);
                //    }
                //}
                //加工后名称添加

                //if (product.ProductProcess.Count > 0&&(product.IsProcee==null||!(bool)(product.IsProcee)))
                //{

                //if ((product.IsProcee == null || !(bool)(product.IsProcee)) && !string.IsNullOrEmpty(product.ProProcessName))
                //{
                //    //复制商品
                //    if (this.ExistsNameUpdate(product))
                //    {
                //        throw new Helper.InvalidValueException(Model.Product.PRO_ProductName);
                //    }
                //    Model.Product product1 = product;

                //    product1.ProductId = Guid.NewGuid().ToString();
                //    product1.ProductName = product.ProProcessName;
                //    product1.StocksQuantity = 0;
                //    product1.ProceebeforeProductId = PrId;
                //    product1.ProductDescription = product.ProductProcessDescription;
                //    product1.IsProcee = true;
                //    product1.Id = string.IsNullOrEmpty(product.ProceId) ? product.Id : product.ProceId;
                //    product1.ProductBarCode = string.IsNullOrEmpty(product.ProceId) ? null : product.ProceId;
                //    product1.ProductBarCodeIsAuto = false;

                //    accessor.Insert(product1);

                //    foreach (Model.ProductProcess Pro in product.ProductProcess)
                //    {
                //        if (Pro.Procedures == null) continue;
                //        Pro.ProductProcessId = Guid.NewGuid().ToString();
                //        Pro.ProductId = product1.ProductId;
                //        Pro.ProcessProductName = product1.ProductName;
                //        //  Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;
                //        //Pro.ProcessCategoryId = Pro.ProcessCategory == null ? null : Pro.ProcessCategory.ProcessCategoryId;
                //        ProductProcessAccessor.Insert(Pro);
                //    }


                //}

                //if (product.IsProcee != null && product.IsProcee.Value && !string.IsNullOrEmpty(product.ProProcessName))
                //{
                //    ProductProcessAccessor.Delete(product);
                //    foreach (Model.ProductProcess Pro in product.ProductProcess)
                //    {
                //        if (Pro.Procedures == null) continue;
                //        Pro.ProductProcessId = Guid.NewGuid().ToString();
                //        Pro.ProductId = product.ProductId;
                //        Pro.ProcessProductName = product.ProductName;
                //        //  Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;
                //        //Pro.ProcessCategoryId = Pro.ProcessCategory == null ? null : Pro.ProcessCategory.ProcessCategoryId;
                //        ProductProcessAccessor.Insert(Pro);
                //    }

                //}

                //返回裸片的值

                //修改时商品为非加工后的
                //if (result == null || result == false)
                //{
                //    product.ProceProductId = product.ProductId;
                //    product.ProductName = name;
                //    product.ProductId = PrId;
                //    product.ProceebeforeProductId = proceebeforeProductId;
                //    product.ProductDescription = text;
                //    product.IsProcee = result;
                //    product.StocksQuantity = stock;
                //    product.Id = Id;
                //    product.ProductBarCode = ProductBarCode;
                //    product.ProductBarCodeIsAuto = ProductBarCodeIsAuto;
                //}
                V.CommitTransaction();
            }
            catch
            {
                V.RollbackTransaction();
                throw;

            }




            //foreach (Model.ProductProcess Pro in product.ProductProcess)
            //{
            //    if (Pro.ProcessCategory == null) continue;
            //    Pro.ProductProcessId = Guid.NewGuid().ToString();
            //    Pro.ProductId = product.ProductId;
            //    Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;
            //    Pro.ProcessCategoryId = Pro.ProcessCategory == null ? null : Pro.ProcessCategory.ProcessCategoryId;
            //    ProductProcessAccessor.Insert(Pro);
            //}
            // }
            //    if (product.IsProcee == true && !string.IsNullOrEmpty(product.ProceduresId))
            //{        

            //        ProductProcessAccessor.Delete(product);
            //        foreach (Model.ProductProcess Pro in product.ProductProcess)
            //        {
            //            if (Pro.ProcessCategory == null) continue;
            //            Pro.ProductProcessId = Guid.NewGuid().ToString();
            //            Pro.ProductId = product.ProductId;
            //            Pro.ProcessId = Pro.Process == null ? null : Pro.Process.ProcessId;
            //            Pro.ProcessCategoryId = Pro.ProcessCategory == null ? null : Pro.ProcessCategory.ProcessCategoryId;

            //            ProductProcessAccessor.Insert(Pro);
            //        }



        }

        /// <summary>
        /// 修改不判断 2014年6月18日 优化修改，只修改必须的，其他去掉，增加速度。
        /// </summary>
        /// <returns></returns>
        public void update(Model.Product product)
        {
            product.UpdateTime = DateTime.Now;

            //2018年7月18日16:11:45  矫正商品库存
            product.StocksQuantity = this.SelectStocksQuantityByStock(product.ProductId);

            accessor.UpdateSimple(product);
        }

        public IList<Model.Product> SelectProduct()
        {
            return accessor.SelectProduct();
        }
        public System.Data.DataTable SelectDataTable()
        {
            return accessor.SelectDataTable();
        }
        public void UpdateBeginCost(System.Data.DataTable dt)
        {
            accessor.UpdateBeginCost(dt);
        }

        public string GetNewId(Model.ProductCategory productCategory)
        {
            // this.Validate(product);
            string sequencekey = productCategory.Id;
            //SequenceManager.Increment(sequencekey);

            // SequenceManager.Increment(sequencekey);
            //  string str=  
            // SequenceManager.IncrementVal(sequencekey,);

            int sequenceval = SequenceManager.GetCurrentVal(sequencekey) + 1;
            return string.Format("{0}{1:d4}", sequencekey, sequenceval);
        }

        public IList<Model.Product> Select(Model.ProductCategory category)
        {
            return accessor.Select(category);
        }

        public IList<Model.Product> Select(Model.Depot depot)
        {
            return accessor.Select(depot);
        }
        public string GetNewId()
        {
            string sequencekey = "PRO";
            SequenceManager.Increment(sequencekey);
            int sequenceval = SequenceManager.GetCurrentVal(sequencekey);
            return string.Format("{0}{1:d2}", null, sequenceval);
        }
        //查询指定客户的货品和公司货品
        public IList<Model.Product> Select(Model.Customer customer)
        {
            return accessor.Select(customer);
        }
        //查询指定类型和客户的货品和公司货品
        public IList<Model.Product> Select(Model.Customer customer, Model.ProductCategory cate)
        {
            return accessor.Select(customer, cate);
        }
        public IList<Model.Product> SelectProductByCustomer(Model.Customer customer)
        {
            return accessor.SelectProductByCustomer(customer);

        }

        public IList<Model.Product> SelectAllProductByCustomers(string customerIds, bool isShowUnuseProduct)
        {
            return accessor.SelectAllProductByCustomers(customerIds, isShowUnuseProduct);
        }

        public Model.Product Get(Model.Customer customer, Model.Product product)
        {
            return accessor.Get(customer, product);
        }
        public void Delete(Book.Model.Product product, Model.Customer customer)
        {
            accessor.Delete(product, customer);
        }

        //CdmiN--2011年9月29日16:05:38 更新product表,使其与stock表中数据对应
        public void UpdateProduct_Stock(Book.Model.Product pro)
        {
            accessor.UpdateProduct_Stock(pro);
        }

        public IList<Model.Product> GetProduct()
        {
            return accessor.GetProduct();
        }

        public IList<Model.Product> GetProductByCondition(string ProductCategoryName, string pt, string depotid)
        {
            return accessor.GetProductByCondition(ProductCategoryName, pt, depotid);
        }

        public IList<Book.Model.Product> SelectProductByProductCategoryId(Book.Model.ProductCategory Category)
        {
            return accessor.SelectProductByProductCategoryId(Category);
        }
        public IList<Model.Product> SelectNotCustomer()
        {
            return accessor.SelectNotCustomer();
        }
        public IList<Model.Product> SelectNotCustomerByCate(string productCate)
        {
            return accessor.SelectNotCustomerByCate(productCate);

        }
        public IList<Model.Product> SelectByIdOrNameKey(string id, string productName, string customerProductName)
        {
            return accessor.SelectByIdOrNameKey(id, productName, customerProductName);
        }
        public IList<Model.Product> GetProductReader()
        {
            return accessor.GetProductReader();
        }
        /// <summary>
        /// 根据组装前半成品 查询 裸片加工后商品
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public IList<Model.Product> SelectProceProduct(Model.Product product)
        {
            return accessor.SelectProceProduct(product);

        }
        /// <summary>
        /// 查询id name productid
        /// </summary>
        /// <returns></returns>
        public IList<Model.Product> SelectNotCustomer1()
        {
            return accessor.SelectNotCustomer1();
        }
        public IList<Model.Product> SelectProceByProduct(Model.Product product)
        {
            return accessor.SelectProceByProduct(product);
        }
        public IList<Model.Product> SelectALLIdOrNameKey(string id, string productName, string customerProductName)
        {
            return accessor.SelectALLIdOrNameKey(id, productName, customerProductName);
        }
        public double? getStockByProduct(string productid)
        {
            return accessor.getStockByProduct(productid);
        }
        /// <summary>
        /// 已分配 和 庫存
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>

        public Model.Product getStockYFPByProduct(string productid)
        {
            return accessor.getStockYFPByProduct(productid);
        }

        public IList<Model.Product> StockPrompt()
        {
            return accessor.StockPrompt();
        }

        public IList<Book.Model.Product> SelectProductByCondition(string productIdStart, string productIdEnd, string DepotIdStart, string DepotIdEnd, string productCategoryIdStart, string productCategoryIdEnd)
        {
            return accessor.SelectProductByCondition(productIdStart, productIdEnd, DepotIdStart, DepotIdEnd, productCategoryIdStart, productCategoryIdEnd);
        }

        public IList<Model.Product> SelectProductsByProductIds(string productids)
        {
            return accessor.SelectProductsByProductIds(productids);
        }

        public IList<Model.Product> SelectIdAndStock(string startCategory_Id, string endCategory_Id)
        {
            return accessor.SelectIdAndStock(startCategory_Id, endCategory_Id);
        }

        public IList<Model.Product> SelectProductIdAndName()
        {
            return accessor.SelectProductIdAndName();
        }

        public DataTable SelectProductCategoryByProductIds(string productIds)
        {
            return accessor.SelectProductCategoryByProductIds(productIds);
        }

        public string SelectCustomerProductNameByProductIds(string productIds)
        {
            return accessor.SelectCustomerProductNameByProductIds(productIds);
        }

        public double SelectStocksQuantityByStock(string productId)
        {
            return accessor.SelectStocksQuantityByStock(productId);
        }

        public IList<Model.Product> GetProductBaseInfo()
        {
            return accessor.GetProductBaseInfo();
        }
    }
}

