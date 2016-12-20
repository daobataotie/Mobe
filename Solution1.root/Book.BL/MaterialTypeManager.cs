//------------------------------------------------------------------------------
//
// file name：MaterialTypeManager.cs
// author: peidun
// create date：2009-12-2 16:19:26
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MaterialType.
    /// </summary>
    public partial class MaterialTypeManager:BaseManager
    {
		
		/// <summary>
		/// Delete MaterialType by primary key.
		/// </summary>
		public void Delete(string materialTypeID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(materialTypeID);
		}
        private void Validate(Model.MaterialType materialType)
        {
            if (string.IsNullOrEmpty(materialType.MaterialTypeName))
            {
                throw new Helper.RequireValueException(Model.MaterialType.PROPERTY_MATERIALTYPENAME);
            }
        }
		/// <summary>
		/// Insert a MaterialType.
		/// </summary>
        public void Insert(Model.MaterialType materialType)
        {
			//
			// todo:add other logic here
			//
           // Validate(materialType);
           // materialType.MaterialTypeID = Guid.NewGuid().ToString();
            materialType.InsertTime = DateTime.Now;
            materialType.UpdateTime = DateTime.Now;
            accessor.Insert(materialType);
        }
		
		/// <summary>
		/// Update a MaterialType.
		/// </summary>
        public void Update(IList<Model.MaterialType> detail)
        {
			//
			// todo: add other logic here.
			//

            foreach (Model.MaterialType materialType in detail)
            {
                if (string.IsNullOrEmpty(materialType.MaterialTypeName))
                {
                    throw new Helper.RequireValueException(Model.MaterialType.PROPERTY_MATERIALTYPENAME);
                }

                if (accessor.ExistsName(materialType.MaterialTypeName,materialType.MaterialTypeID))
                {
                    throw new Helper.InvalidValueException(Model.MaterialType.PROPERTY_MATERIALTYPENAME);
                }
            }

            foreach (Model.MaterialType materialType in detail)
            {

                if (accessor.ExistsPrimary(materialType.MaterialTypeID))
                {
                    materialType.UpdateTime = DateTime.Now;
                    accessor.Update(materialType);
                }
                else
                {

                    this.Insert(materialType);
                }
            }            
        }
        public void Delete(Model.MaterialType materialType)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(materialType.MaterialTypeID);
        }
    }
}

