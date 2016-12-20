//------------------------------------------------------------------------------
//
// file name：ProductEpibolyManager.cs
// author: peidun
// create date：2009-12-16 11:15:04
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductEpiboly.
    /// </summary>
    public partial class ProductEpibolyManager
    {
		
		/// <summary>
		/// Delete ProductEpiboly by primary key.
		/// </summary>
		public void Delete(string productEpibolyId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(productEpibolyId);
		}

		/// <summary>
		/// Insert a ProductEpiboly.
		/// </summary>
        public void Insert(Model.ProductEpiboly productEpiboly)
        {
			//
			// todo:add other logic here
			//
            Validate(productEpiboly);
            productEpiboly.InsertTime = DateTime.Now;
            productEpiboly.ProductEpibolyId = Guid.NewGuid().ToString();
            accessor.Insert(productEpiboly);
        }
		
		/// <summary>
		/// Update a ProductEpiboly.
		/// </summary>
        public void Update(Model.ProductEpiboly productEpiboly)
        {
			//
			// todo: add other logic here.
			//
            Validate(productEpiboly);
            productEpiboly.UpdateTime = DateTime.Now;
            accessor.Update(productEpiboly);
        }
        private void Validate(Model.ProductEpiboly productEpiboly)
        {
            if (string.IsNullOrEmpty(productEpiboly.ProductId))
            {
                throw new Helper.RequireValueException(Model.ProductEpiboly.PROPERTY_PRODUCTID);
            }
            if (string.IsNullOrEmpty(productEpiboly.SupplierId))
            {
                throw new Helper.RequireValueException(Model.ProductEpiboly.PROPERTY_SUPPLIERID);
            }
        }
    }
}

