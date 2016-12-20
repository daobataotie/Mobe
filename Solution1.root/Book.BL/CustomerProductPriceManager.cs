//------------------------------------------------------------------------------
//
// file name：CustomerProductPriceManager.cs
// author: mayanjun
// create date：2013-3-8 16:09:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerProductPrice.
    /// </summary>
    public partial class CustomerProductPriceManager
    {

        /// <summary>
        /// Delete CustomerProductPrice by primary key.
        /// </summary>
        public void Delete(string customerProductPriceId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(customerProductPriceId);
        }

        /// <summary>
        /// Insert a CustomerProductPrice.
        /// </summary>
        public void Insert(Model.CustomerProductPrice customerProductPrice)
        {
            //
            // todo:add other logic here
            //
            Validate(customerProductPrice);
            customerProductPrice.InsertTime = DateTime.Now;
            customerProductPrice.UpdateTime = DateTime.Now;
            accessor.Insert(customerProductPrice);
        }

        /// <summary>
        /// Update a CustomerProductPrice.
        /// </summary>
        public void Update(Model.CustomerProductPrice customerProductPrice)
        {
            //
            // todo: add other logic here.
            //
            Validate(customerProductPrice);
            customerProductPrice.UpdateTime = DateTime.Now;
            accessor.Update(customerProductPrice);
        }

        private void Validate(Book.Model.CustomerProductPrice customerProductPrice)
        {
            if (customerProductPrice.ProductId == null)
                throw new Helper.RequireValueException(Model.CustomerProductPrice.PRO_ProductId);
        }

        public IList<Model.CustomerProductPrice> SelectByCustomerId(string CustomerId)
        {
            return accessor.SelectByCustomerId(CustomerId);
        }

        public IList<Model.CustomerProductPrice> SelectByProductId(string ProductId)
        {
            return accessor.SelectByProductId(ProductId);
        }

        public void UpdateByCustomerProductsId(Model.CustomerProductPrice model)
        {
            accessor.UpdateByCustomerProductsId(model);
        }

        public string SelectPriceByProductId(string ProductId)
        {
            return accessor.SelectPriceByProductId(ProductId);
        }
    }
}

