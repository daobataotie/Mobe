//------------------------------------------------------------------------------
//
// file name：ProductClassifyManager.cs
// author: mayanjun
// create date：2017-08-24 21:36:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductClassify.
    /// </summary>
    public partial class ProductClassifyManager
    {
        private DA.IProductClassifyDetailAccessor DetailAccessor = (DA.IProductClassifyDetailAccessor)Accessors.Get("ProductClassifyDetailAccessor");
        /// <summary>
        /// Delete ProductClassify by primary key.
        /// </summary>
        public void Delete(string productClassifyId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                DetailAccessor.DeleteByHeader(productClassifyId);
                accessor.Delete(productClassifyId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Insert a ProductClassify.
        /// </summary>
        public void Insert(Model.ProductClassify productClassify)
        {
            //
            // todo:add other logic here
            //
            try
            {
                this.Validate(productClassify);
                productClassify.InsertTime = DateTime.Now;
                productClassify.UpdateTime = DateTime.Now;
                BL.V.BeginTransaction();
                accessor.Insert(productClassify);
                foreach (var item in productClassify.Details)
                {
                    DetailAccessor.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Update a ProductClassify.
        /// </summary>
        public void Update(Model.ProductClassify productClassify)
        {
            //
            // todo: add other logic here.
            //
            try
            {
                BL.V.BeginTransaction();
                this.Validate(productClassify);
                productClassify.UpdateTime = DateTime.Now;

                accessor.Update(productClassify);
                DetailAccessor.DeleteByHeader(productClassify.ProductClassifyId);
                foreach (var item in productClassify.Details)
                {
                    DetailAccessor.Insert(item);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void Validate(Model.ProductClassify productClassify)
        {
            if (productClassify.ProductClassifyDate == null)
                throw new Helper.InvalidValueException(Model.ProductClassify.PRO_ProductClassifyDate);
            if (string.IsNullOrEmpty(productClassify.KeyWord))
                throw new Helper.InvalidValueException(Model.ProductClassify.PRO_KeyWord);
        }

        public Model.ProductClassify GetDetail(string productClassifyId)
        {
            Model.ProductClassify productClassify = accessor.Get(productClassifyId);
            if (productClassify != null)
                productClassify.Details = DetailAccessor.SelectByHeader(productClassify);
            return productClassify;
        }

        public bool IsExistsKeyWordForInsert(Model.ProductClassify productClassify)
        {
            return accessor.IsExistsKeyWordForInsert(productClassify);
        }

        public bool IsExistsKeyWordForUpdate(Model.ProductClassify productClassify)
        {
            return accessor.IsExistsKeyWordForUpdate(productClassify);
        }

    }
}
