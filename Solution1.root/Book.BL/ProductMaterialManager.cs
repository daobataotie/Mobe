//------------------------------------------------------------------------------
//
// file name：ProductMaterialManager.cs
// author: mayanjun
// create date：2010-9-23 15:27:44
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductMaterial.
    /// </summary>
    public partial class ProductMaterialManager
    {
		
		/// <summary>
		/// Delete ProductMaterial by primary key.
		/// </summary>
		public void Delete(string productMaterialId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(productMaterialId);
		}

		/// <summary>
		/// Insert a ProductMaterial.
		/// </summary>
        public void Insert(Model.ProductMaterial productMaterial)
        {
            validate(productMaterial);
            productMaterial.InsertTime = DateTime.Now;
            productMaterial.UpdateTime = DateTime.Now;
            productMaterial.ProductMaterialId = Guid.NewGuid().ToString();
            accessor.Insert(productMaterial);
        }

        private void validate(Book.Model.ProductMaterial productMaterial)
        {
            if (string.IsNullOrEmpty(productMaterial.Id))
                throw new Helper.RequireValueException(Model.ProductMaterial.PRO_Id);
            if (IsExistId(productMaterial))
                throw new Helper.InvalidValueException(Model.ProductMaterial.PRO_Id);
            if (string.IsNullOrEmpty(productMaterial.ProductMaterialName))
                throw new Helper.RequireValueException(Model.ProductMaterial.PRO_ProductMaterialName);
            if (IsExistProductMaterialName(productMaterial))
                throw new Helper.InvalidValueException(Model.ProductMaterial.PRO_ProductMaterialName);
        }
		
		/// <summary>
		/// Update a ProductMaterial.
		/// </summary>
        public void Update(Model.ProductMaterial productMaterial)
        {
            validate(productMaterial);
            productMaterial.UpdateTime = DateTime.Now;
            accessor.Update(productMaterial);
        }


        public bool IsExistProductMaterialName(Model.ProductMaterial productMateridal)
        {
            return accessor.IsExistProductMaterialName(productMateridal);
        }

        public bool IsExistId(Model.ProductMaterial productMateridal)
        {
            return accessor.IsExistId(productMateridal);
        }
    }
}

