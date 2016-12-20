//------------------------------------------------------------------------------
//
// file name：CustomerProductsManager.cs
// author: peidun
// create date：2009-09-14  下午 05:25:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerProducts.
    /// </summary>
    public partial class CustomerProductsManager : BaseManager
    {
        private static readonly DA.ICustomerProductProcessAccessor customerProductProcessAccessor = (DA.ICustomerProductProcessAccessor)Accessors.Get("CustomerProductProcessAccessor");
        private static readonly DA.ICustomerProductsBomAccessor customerProductsBomAccessor = (DA.ICustomerProductsBomAccessor)Accessors.Get("CustomerProductsBomAccessor");
        private static readonly DA.IPackageCustomerDetailsAccessor packageCustomerDetailsAccessor = (DA.IPackageCustomerDetailsAccessor)Accessors.Get("PackageCustomerDetailsAccessor");
        private static readonly DA.IProductAccessor productAccessor = (DA.IProductAccessor)Accessors.Get("ProductAccessor");
        /// <summary>
        /// Delete CustomerProducts by primary key.
        /// </summary>
        public void Delete(string primaryKeyId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(primaryKeyId);




        }
        /// <summary>
        /// Delete CustomerProducts by primary key.
        /// </summary>
        public void Delete(Model.CustomerProducts customerProducts)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                accessor.Delete(customerProducts.PrimaryKeyId);


                //指定客户产品对应产品编号     
                //添加的客户产品名稱添加至产品表CustomerProductName 字段



                productAccessor.Delete(customerProducts.CustomerProductProceName);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a CustomerProducts.
        /// </summary>
        public void Insert(Model.CustomerProducts customerProducts)
        {
            //
            // todo:add other logic here
            //

            this.Validate(customerProducts);

            //if (this.Exists(customerProducts))
            //{
            //    throw new Helper.InvalidValueException(Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTNAME);
            //}
            try
            {
                BL.V.BeginTransaction();
                //保存后返回第一个ID
                // string primarykeyid =null;
                string customProductName = null;
                // StringBuilder strBU = new StringBuilder();            
                customerProducts.CustomerId = customerProducts.Customer.CustomerId;
                customerProducts.InsertTime = DateTime.Now;
                //保存后返回第一个ID
                // primarykeyid = customerProducts.PrimaryKeyId;
                customProductName = customerProducts.CustomerProductName;

                customerProducts.BuyUnitId = customerProducts.BuyUnit == null ? null : customerProducts.BuyUnit.ProductUnitId;
                customerProducts.DepotId = customerProducts.Depot == null ? null : customerProducts.Depot.DepotId;
                customerProducts.DepotPositionId = customerProducts.DepotPosition == null ? null : customerProducts.DepotPosition.DepotPositionId;
                customerProducts.DepotUnitId = customerProducts.DepotUnit == null ? null : customerProducts.DepotUnit.ProductUnitId;
                // customerProducts.MainUnitId = customerProducts.MainUnit == null ? null : customerProducts.MainUnit.ProductUnitId;
                customerProducts.ProduceUnitId = customerProducts.ProduceUnit == null ? null : customerProducts.ProduceUnit.ProductUnitId;
                customerProducts.QualityTestUnitId = customerProducts.QualityTestUnit == null ? null : customerProducts.QualityTestUnit.ProductUnitId;
                customerProducts.SellUnitId = customerProducts.SellUnit == null ? null : customerProducts.SellUnit.ProductUnitId;
                customerProducts.UnitGroupId = customerProducts.UnitGroup == null ? null : customerProducts.UnitGroup.UnitGroupId;


                Model.Product product = customerProducts.Product;

                product.CustomerBeforeProductId = customerProducts.Product.ProductId;

                product.ProductId = Guid.NewGuid().ToString();
                customerProducts.CustomerProductProceName = product.ProductId;//新产生的商品ID

                if (IsExistsCustomerProductId(null, customerProducts.PrimaryKeyId))
                    throw new Helper.InvalidValueException(Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTID);
                if (SelectByCustomerIdAndProductId(customerProducts.ProductId, null, customerProducts.CustomerId))
                    throw new Helper.InvalidValueException(Model.CustomerProducts.PROPERTY_CUSTOMERID);

                accessor.Insert(customerProducts);
                //加入商品表

                product.IsCustomerProduct = true;

                //指定客户产品对应产品编号

                product.CustomerBeforeProductName = customerProducts.Product.ProductName;
                //添加的客户产品名稱添加至产品表CustomerProductName 字段
                product.CustomerProductName = customerProducts.CustomerProductId;
                product.Customer = customerProducts.Customer;
                if (product.Customer != null)
                    product.CustomerId = product.Customer.CustomerId;
                byte[] pic = new byte[] { };
                if (product.ProductImage == null)
                    product.ProductImage = pic;
                if (product.ProductImage1 == null)
                    product.ProductImage1 = pic;
                if (product.ProductImage2 == null)
                    product.ProductImage2 = pic;
                if (product.ProductImage3 == null)
                    product.ProductImage3 = pic;
                productAccessor.Insert(product);

                // customerProductProcessAccessor.Delete(customerProducts);

                //foreach (Model.CustomerProductProcess cpp in customerProducts.CustomerProductProcessList)
                //{
                //   if (cpp.ProcessCategory == null) continue;

                //   strBU.Append(cpp.ProcessCategory.ProcessCategoryName);
                //   cpp.CustomerProductProcessId = Guid.NewGuid().ToString();
                //   cpp.PrimaryKeyId = customerProducts.PrimaryKeyId;
                // //  if (cpp.IsCheck.Value)
                //  // {
                //       cpp.ProcessId = cpp.Process == null ? null : cpp.Process.ProcessId;
                //   //}
                //   //else 
                //   //{
                //   //    cpp.ProcessId = null;
                //   //}
                //   cpp.ProcessCategoryId = cpp.ProcessCategory == null ? null : cpp.ProcessCategory.ProcessCategoryId;
                //   customerProductProcessAccessor.Insert(cpp);
                // }           
                //   foreach (Model.CustomerProductsBom bom in customerProducts.CustomerProductsBomInfos)
                //  {
                //      if (bom != null)
                //       {
                //           bom.PriamryKeyId = Guid.NewGuid().ToString();
                //           if (bom.Product != null)
                //               bom.ProductId = bom.Product.ProductId;
                //           bom.PrimaryKeyId = customerProducts.PrimaryKeyId;
                //           customerProductsBomAccessor.Insert(bom);
                //       }
                //   }

                //   if (customerProducts.PackageCustomerDetails!=null)
                //   {
                //       foreach (Model.PackageCustomerDetails pac in customerProducts.PackageCustomerDetails)
                //       {
                //           if (pac != null)
                //           {
                //               pac.PackageCustomerDetailsId = Guid.NewGuid().ToString();
                //               pac.PrimaryKeyId = customerProducts.CustomerProductId;
                //               packageCustomerDetailsAccessor.Insert(pac);
                //           }
                //        }
                //   }
                /////////////////////////////////  //加工后名称添加
                //加工后名称添加
                //if (strBU.Length!= 0 )
                //{                       
                //    customerProducts.PrimaryKeyId = Guid.NewGuid().ToString(); 

                // //   customerProductProcessAccessor.Delete(customerProducts);    


                //    customerProducts.CustomerProductName +=strBU.ToString();
                //    accessor.Insert(customerProducts);
                //    foreach (Model.CustomerProductProcess cpp in customerProducts.CustomerProductProcessList)
                //    {
                //        if (cpp.ProcessCategory == null) continue;
                //        strBU.Append(customerProducts.CustomerProductName);
                //        cpp.CustomerProductProcessId = Guid.NewGuid().ToString();
                //        cpp.PrimaryKeyId = customerProducts.PrimaryKeyId;
                //        //  if (cpp.IsCheck.Value)
                //        // {
                //        cpp.ProcessId = cpp.Process == null ? null : cpp.Process.ProcessId;
                //        //}
                //        //else 
                //        //{
                //        //    cpp.ProcessId = null;
                //        //}
                //        cpp.ProcessCategoryId = cpp.ProcessCategory == null ? null : cpp.ProcessCategory.ProcessCategoryId;
                //        customerProductProcessAccessor.Insert(cpp);
                //    }


                //foreach (Model.CustomerProductsBom bom in customerProducts.CustomerProductsBomInfos)
                //{
                //    if (bom != null)
                //    {
                //        bom.PriamryKeyId = Guid.NewGuid().ToString();
                //        if (bom.Product != null)
                //            bom.ProductId = bom.Product.ProductId;
                //        bom.PrimaryKeyId = customerProducts.PrimaryKeyId;
                //        customerProductsBomAccessor.Insert(bom);
                //    }
                //}
                //if (customerProducts.PackageCustomerDetails.Count!= 0)
                //{
                //    foreach (Model.PackageCustomerDetails pac in customerProducts.PackageCustomerDetails)
                //    {
                //        if (pac != null)
                //        {
                //            pac.PackageCustomerDetailsId = Guid.NewGuid().ToString();
                //            pac.PrimaryKeyId = customerProducts.CustomerProductId;
                //            packageCustomerDetailsAccessor.Insert(pac);
                //        }
                //    }
                //}
                //    customerProducts.PrimaryKeyId = primarykeyid;
                //     customerProducts.CustomerProductName =customProductName;
                //}

                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }
        private void Validate(Model.CustomerProducts customerProducts)
        {
            if (string.IsNullOrEmpty(customerProducts.CustomerProductId))
            {
                throw new Helper.RequireValueException(Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTID);
            }

            if (customerProducts.Customer == null)
            {
                throw new Helper.RequireValueException(Model.CustomerProducts.PROPERTY_CUSTOMERID);
            }

            if (string.IsNullOrEmpty(customerProducts.ProductId))
            {
                throw new Helper.RequireValueException(Model.CustomerProducts.PROPERTY_PRODUCTID);
            }

            if (this.IsExistsCustomerProductId(customerProducts.CustomerProductId, customerProducts.PrimaryKeyId))
                throw new Helper.MessageValueException("商品型號已經存在！");
        }

        private bool Exists(Book.Model.CustomerProducts customerProducts)
        {
            return accessor.Exists(customerProducts);
        }
        /// <summary>
        /// Update a CustomerProducts.
        /// </summary>
        public void Update(Model.CustomerProducts customerProducts)
        {
            //
            // todo: add other logic here.
            //
            this.Validate(customerProducts);
            if (this.ExistsExcept(customerProducts))
            {
                throw new Helper.InvalidValueException(Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTID);
            }
            try
            {
                BL.V.BeginTransaction();
                // StringBuilder strBU = new StringBuilder();
                customerProducts.UpdateTime = DateTime.Now;
                customerProducts.CustomerId = customerProducts.Customer.CustomerId;
                customerProducts.BuyUnitId = customerProducts.BuyUnit == null ? null : customerProducts.BuyUnit.ProductUnitId;
                customerProducts.DepotId = customerProducts.Depot == null ? null : customerProducts.Depot.DepotId;
                customerProducts.DepotPositionId = customerProducts.DepotPosition == null ? null : customerProducts.DepotPosition.DepotPositionId;
                customerProducts.DepotUnitId = customerProducts.DepotUnit == null ? null : customerProducts.DepotUnit.ProductUnitId;
                customerProducts.MainUnitId = customerProducts.MainUnit == null ? null : customerProducts.MainUnit.ProductUnitId;
                customerProducts.ProduceUnitId = customerProducts.ProduceUnit == null ? null : customerProducts.ProduceUnit.ProductUnitId;
                customerProducts.QualityTestUnitId = customerProducts.QualityTestUnit == null ? null : customerProducts.QualityTestUnit.ProductUnitId;
                customerProducts.SellUnitId = customerProducts.SellUnit == null ? null : customerProducts.SellUnit.ProductUnitId;
                customerProducts.UnitGroupId = customerProducts.UnitGroup == null ? null : customerProducts.UnitGroup.UnitGroupId;

                if (IsExistsCustomerProductId(customerProducts.CustomerProductId, customerProducts.PrimaryKeyId))
                    throw new Helper.InvalidValueException(Model.CustomerProducts.PROPERTY_CUSTOMERPRODUCTID);
                if (SelectByCustomerIdAndProductId(customerProducts.ProductId, customerProducts.PrimaryKeyId, customerProducts.CustomerId))
                    throw new Helper.InvalidValueException(Model.CustomerProducts.PROPERTY_CUSTOMERID);

                accessor.Update(customerProducts);

                //修改商品
                Model.Product product = productAccessor.Get(customerProducts.CustomerProductProceName);

                //指定客户产品对应产品编号
                product.CustomerBeforeProductId = customerProducts.Product.ProductId;
                product.CustomerBeforeProductName = customerProducts.Product.ProductName;
                //添加的客户产品名稱添加至产品表CustomerProductName 字段
                product.CustomerProductName = customerProducts.CustomerProductId;
                product.Customer = customerProducts.Customer;
                if (product.Customer != null)
                    product.CustomerId = product.Customer.CustomerId;
                byte[] pic = new byte[] { };
                if (product.ProductImage == null)
                    product.ProductImage = pic;
                if (product.ProductImage1 == null)
                    product.ProductImage1 = pic;
                if (product.ProductImage2 == null)
                    product.ProductImage2 = pic;
                if (product.ProductImage3 == null)
                    product.ProductImage3 = pic;
                productAccessor.Update(product);




                //customerProductProcessAccessor.Delete(customerProducts);

                //customerProductsBomAccessor.Delete(customerProducts);

                //包装
                //packageCustomerDetailsAccessor.Delete(customerProducts);


                //foreach (Model.CustomerProductProcess cpp in customerProducts.CustomerProductProcessList)
                //{
                //    if (cpp.ProcessCategory == null) continue;
                //    strBU.Append(cpp.ProcessCategory.ProcessCategoryName) ;
                //    cpp.CustomerProductProcessId = Guid.NewGuid().ToString();
                //    cpp.PrimaryKeyId = customerProducts.PrimaryKeyId;
                //    //if (cpp.IsCheck.Value)
                //    //{
                //        cpp.ProcessId = cpp.Process == null ? null : cpp.Process.ProcessId;                        
                //    //}
                //    //else
                //    //{
                //    //    cpp.ProcessId = null;                        
                //    //}
                //    cpp.ProcessCategoryId = cpp.ProcessCategory == null ? null : cpp.ProcessCategory.ProcessCategoryId;
                //    customerProductProcessAccessor.Insert(cpp);
                //}

                //foreach (Model.CustomerProductsBom bom in customerProducts.CustomerProductsBomInfos)
                //{
                //    bom.PriamryKeyId = Guid.NewGuid().ToString();
                //    bom.ProductId = bom.Product.ProductId;
                //    bom.PrimaryKeyId = customerProducts.PrimaryKeyId;
                //    customerProductsBomAccessor.Insert(bom);
                //}
                //foreach (Model.PackageCustomerDetails pac in customerProducts.PackageCustomerDetails)
                //{
                //    pac.PackageCustomerDetailsId = Guid.NewGuid().ToString();
                //    pac.PrimaryKeyId = customerProducts.CustomerProductId;
                //    packageCustomerDetailsAccessor.Insert(pac);
                //}
                /////////////////////////////////  //加工后名称添加
                //加工后名称添加
                //if (strBU != null)
                //{                  
                //    customerProducts.PrimaryKeyId = Guid.NewGuid().ToString();
                //    customerProducts.CustomerProductName = strBU.ToString();
                //    accessor.Insert(customerProducts);
                //    foreach (Model.CustomerProductProcess cpp in customerProducts.CustomerProductProcessList)
                //    {
                //        if (cpp.ProcessCategory == null) continue;
                //        strBU.Append(customerProducts.CustomerProductName);
                //        cpp.CustomerProductProcessId = Guid.NewGuid().ToString();
                //        cpp.PrimaryKeyId = customerProducts.PrimaryKeyId;
                //        //  if (cpp.IsCheck.Value)
                //        // {
                //        cpp.ProcessId = cpp.Process == null ? null : cpp.Process.ProcessId;
                //        //}
                //        //else 
                //        //{
                //        //    cpp.ProcessId = null;
                //        //}
                //        cpp.ProcessCategoryId = cpp.ProcessCategory == null ? null : cpp.ProcessCategory.ProcessCategoryId;
                //        customerProductProcessAccessor.Insert(cpp);
                //    }
                //    foreach (Model.CustomerProductsBom bom in customerProducts.CustomerProductsBomInfos)
                //    {
                //        if (bom != null)
                //        {
                //            bom.PriamryKeyId = Guid.NewGuid().ToString();
                //            if (bom.Product != null)
                //                bom.ProductId = bom.Product.ProductId;
                //            bom.PrimaryKeyId = customerProducts.PrimaryKeyId;
                //            customerProductsBomAccessor.Insert(bom);
                //        }
                //    }
                //    if (customerProducts.PackageCustomerDetails != null)
                //    {
                //        foreach (Model.PackageCustomerDetails pac in customerProducts.PackageCustomerDetails)
                //        {
                //            if (pac != null)
                //            {
                //                pac.PackageCustomerDetailsId = Guid.NewGuid().ToString();
                //                pac.PrimaryKeyId = customerProducts.CustomerProductId;
                //                packageCustomerDetailsAccessor.Insert(pac);
                //            }
                //        }
                //    }
                //}
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public bool ExistsExcept(Book.Model.CustomerProducts customerProducts)
        {
            return accessor.ExistsExcept(customerProducts);
        }

        public IList<Model.CustomerProducts> Select(Book.Model.Customer customer)
        {
            return accessor.Select(customer);
        }


        /// <summary>
        /// Select by primary key.
        /// </summary>		
        public Model.CustomerProducts Get(string primaryKeyId)
        {
            Model.CustomerProducts product = accessor.Get(primaryKeyId);
            if (product != null)
            {
                product.CustomerProductProcessList = SelectProcessCategory(product);
                product.CustomerProductsBomInfos = SelectBomInfos(product);
            }
            return product;
        }

        public IList<Book.Model.CustomerProductsBom> SelectBomInfos(Book.Model.CustomerProducts product)
        {
            return customerProductsBomAccessor.SelectBomInfos(product);
        }

        public IList<Book.Model.CustomerProductProcess> SelectProcessCategory(Book.Model.CustomerProducts customerProducts)
        {
            return customerProductProcessAccessor.SelectProcessCategory(customerProducts);
        }

        public Book.Model.CustomerProducts GetById(string customerProductId)
        {
            return accessor.GetById(customerProductId);
        }


        public string GetNewId()
        {
            string sequencekey = "CBOM";
            SequenceManager.Increment(sequencekey);
            int sequenceval = SequenceManager.GetCurrentVal(sequencekey);
            return string.Format("{0}{1:d5}", sequencekey, sequenceval);
        }
        //public IList<Book.Model.CustomerProducts> Select(string customerStart, string customerEnd, string productStart, string productEnd, DateTime dateStart, DateTime dateEnd)
        //{
        //    return accessor.Select(  customerStart,  customerEnd,  productStart,  productEnd,  dateStart,  dateEnd);
        //}
        /// <summary>
        /// 返回客户区间的客户数据
        /// </summary>
        /// <param name="customerStart"></param>
        /// <param name="customerEnd"></param>
        /// <returns></returns>
        public float GetStocksQuantityById(string primaryKeyId)
        {
            return accessor.GetStocksQuantityById(primaryKeyId);
        }

        public bool IsExistsCustomerProductId(string customerProductId, string primaryKeyId)
        {
            return accessor.IsExistsCustomerProductId(customerProductId, primaryKeyId);
        }

        public bool SelectByCustomerIdAndProductId(string customerProductId, string primaryKeyId, string customerId)
        {
            return accessor.SelectByCustomerIdAndProductId(customerProductId, primaryKeyId, customerId);
        }
    }
}