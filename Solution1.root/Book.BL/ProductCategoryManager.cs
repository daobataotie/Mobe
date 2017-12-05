//------------------------------------------------------------------------------
//
// file name：ProductCategoryManager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductCategory.
    /// </summary>
    public partial class ProductCategoryManager : BaseManager
    {

        /// <summary>
        /// Delete ProductCategory by primary key.
        /// </summary>
        public void Delete(string productCategoryId)
        {
            accessor.Delete(productCategoryId);
        }

        public void Delete(Model.ProductCategory productCategory)
        {
            try
            {
                BL.V.BeginTransaction();
                this.Delete(productCategory.ProductCategoryId);

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// Insert a ProductCategory.
        /// </summary>
        public void Insert(Model.ProductCategory productCategory)
        {
            //this.Validate(productCategory);
            //if (this.Exists(productCategory.Id))
            //{
            //    throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_ID);
            //}

            //productCategory.ProductCategoryId = Guid.NewGuid().ToString();
            //productCategory.InsertTime = DateTime.Now; 
            if (this.Exists(productCategory.Id))
            {
                throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_ID);
            }
            productCategory.InsertTime = DateTime.Now;
            productCategory.UpdateTime = DateTime.Now;
            accessor.Insert(productCategory);
        }

        /// <summary>
        /// Update a ProductCategory.
        /// </summary>
        public void Update(IList<Model.ProductCategory> productCategoryDetail)
        {
            foreach (Model.ProductCategory pc in productCategoryDetail)
            {

                if (string.IsNullOrEmpty(pc.Id) || string.IsNullOrEmpty(pc.ProductCategoryName))
                {
                    throw new Helper.RequireValueException(Model.ProductCategory.PROPERTY_ID);
                }
                if (accessor.ExistsName(pc.ProductCategoryName, pc.ProductCategoryId))
                {
                    throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_PRODUCTCATEGORYNAME);
                }
                if (accessor.ExistsExcept(pc))
                {
                    throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_ID);
                }

            }

            foreach (Model.ProductCategory pc in productCategoryDetail)
            {

                if (accessor.ExistsPrimary(pc.ProductCategoryId))
                {
                    pc.UpdateTime = DateTime.Now;
                    accessor.Update(pc);
                }

                else
                {

                    this.Insert(pc);
                }
            }
        }

        public void Update(DataSet ds)
        {
            try
            {
                BL.V.BeginTransaction();

                foreach (DataTable dt in ds.Tables)
                {
                    ValidateDT(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Model.ProductCategory pc = new Book.Model.ProductCategory();
                        pc.ProductCategoryId = dr["ProductCategoryId"].ToString();
                        pc.ProductCategoryParentId = dr["ProductCategoryParentId"].ToString() == "" ? null : dr["ProductCategoryParentId"].ToString();
                        pc.UpdateTime = DateTime.Now;
                        pc.Id = dr["Id"].ToString();
                        pc.ProductCategoryName = dr["ProductCategoryName"].ToString();
                        pc.CategoryLevel = int.Parse(dr["CategoryLevel"].ToString());

                        if (accessor.ExistsPrimary(pc.ProductCategoryId))
                        {
                            pc.InsertTime = DateTime.Parse(dr["InsertTime"].ToString());
                            accessor.Update(pc);
                        }

                        else
                        {
                            pc.InsertTime = DateTime.Now;
                            this.Insert(pc);
                        }
                    }
                }

                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }

        private void ValidateDT(DataTable dt)
        {
            foreach (DataRow pc in dt.Rows)
            {
                if (string.IsNullOrEmpty(pc["Id"].ToString()) || string.IsNullOrEmpty(pc["ProductCategoryName"].ToString()))
                {
                    throw new Helper.RequireValueException(Model.ProductCategory.PROPERTY_ID);
                }
                if (accessor.ExistsName(pc["ProductCategoryName"].ToString(), pc["ProductCategoryId"].ToString()))
                {
                    throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_PRODUCTCATEGORYNAME);
                }
                if (accessor.ExistsExceptDT(pc["Id"].ToString(), pc["ProductCategoryId"].ToString()))
                {
                    throw new Helper.InvalidValueException(Model.ProductCategory.PROPERTY_ID);
                }

            }
        }

        public IList<string> SelectALLName()
        {
            return accessor.SelectALLName();

        }

        #region Helpers

        private void Validate(Model.ProductCategory productCategory)
        {
            if (string.IsNullOrEmpty(productCategory.Id))
                throw new Helper.RequireValueException(Model.ProductCategory.PROPERTY_ID);

            if (string.IsNullOrEmpty(productCategory.ProductCategoryName))
                throw new Helper.RequireValueException(Model.ProductCategory.PROPERTY_PRODUCTCATEGORYNAME);


        }

        #endregion

        public DataTable SelectDTByFilter(string filter)
        {
            return accessor.SelectDTByFilter(filter);
        }

        public IList<Model.ProductCategory> SelectListByFilter(string CategoryLevel, string ProductCategoryParentId)
        {
            return accessor.SelectListByFilter(CategoryLevel, ProductCategoryParentId);
        }

        /// <summary>
        /// 2017-12-5 ：真正的查询所有，"Select"只查询Level为1的
        /// </summary>
        /// <returns></returns>
        public IList<Model.ProductCategory> SelectAll()
        {
            return accessor.SelectAll();
        }

        //protected override string GetInvoiceKind()
        //{
        //    return "ProductCategory";
        //}

        //protected override string GetSettingId()
        //{
        //    return "ProductCategoryRule";
        //}
    }
}

