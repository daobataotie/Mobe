//------------------------------------------------------------------------------
//
// file name：ProductMouldTestManager.cs
// author: mayanjun
// create date：2010-9-24 16:24:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductMouldTest.
    /// </summary>
    public partial class ProductMouldTestManager
    {
        private static readonly DA.IProductMouldAccessor ProductMouldAccessor = (DA.IProductMouldAccessor)Accessors.Get("ProductMouldAccessor");
        /// <summary>
        /// Delete ProductMouldTest by primary key.
        /// </summary>
        public void Delete(string productMouldTestId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(productMouldTestId);
        }

        /// <summary>
        /// Insert a ProductMouldTest.
        /// </summary>
        public void Insert(Model.ProductMouldTest productMouldTest)
        {
            //
            // todo:add other logic here
            //
            productMouldTest.ProductMouldTestId = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(productMouldTest.Id))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PROPERTY_ID);
            if (IsExistId(productMouldTest))
                throw new Helper.InvalidValueException(Model.ProductMouldTest.PROPERTY_ID);
            accessor.Insert(productMouldTest);

            IList<Model.ProductMould> list = ProductMouldAccessor.SelectProductMouldByProductMouldTestId(productMouldTest.ProductMouldTestId);

            foreach (Model.ProductMould productmould in list)
            {
                if (productmould.FirstTime == null || productmould.FirstTime == new DateTime() || productmould.FirstTime == global::Helper.DateTimeParse.NullDate)
                {
                    productmould.FirstTime = productMouldTest.ProductMouldTestDate;
                    ProductMouldAccessor.Update(productmould);
                }
            }

            //Model.ProductMould productmould = ProductMouldAccessor.Get(productMouldTest.MouldId);

            //if (productmould.FirstTime == null || productmould.FirstTime == new DateTime() || productmould.FirstTime == global::Helper.DateTimeParse.NullDate)
            //{
            //    productmould.FirstTime = productMouldTest.ProductMouldTestDate;
            //    ProductMouldAccessor.Update(productmould);
            //}
        }

        /// <summary>
        /// Update a ProductMouldTest.
        /// </summary>
        public void Update(Model.ProductMouldTest productMouldTest)
        {

            if (string.IsNullOrEmpty(productMouldTest.Id))
                throw new Helper.RequireValueException(Model.ProductMouldTest.PROPERTY_ID);
            if (IsExistId(productMouldTest))
                throw new Helper.InvalidValueException(Model.ProductMouldTest.PROPERTY_ID);

            accessor.Update(productMouldTest);

            IList<Model.ProductMould> list = ProductMouldAccessor.SelectProductMouldByProductMouldTestId(productMouldTest.ProductMouldTestId);

            foreach (Model.ProductMould productmould in list)
            {
                if (productmould.FirstTime == null || productmould.FirstTime == new DateTime() || productmould.FirstTime == global::Helper.DateTimeParse.NullDate)
                {
                    productmould.FirstTime = productMouldTest.ProductMouldTestDate;
                    ProductMouldAccessor.Update(productmould);
                }
            }
        }

        public bool IsExistId(Model.ProductMouldTest test)
        {
            return accessor.IsExistId(test);
        }
    }
}

