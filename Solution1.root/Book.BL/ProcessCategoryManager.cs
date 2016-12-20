//------------------------------------------------------------------------------
//
// file name：ProcessCategoryManager.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProcessCategory.
    /// </summary>
    public partial class ProcessCategoryManager
    {
		
		/// <summary>
		/// Delete ProcessCategory by primary key.
		/// </summary>
		public void Delete(string processCategoryId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(processCategoryId);
		}
        public void Delete(Model.ProcessCategory processCategory)
        {
            //
            // todo:add other logic here
            //
            this.Delete(processCategory.ProcessCategoryId);
        }

		/// <summary>
		/// Insert a ProcessCategory.
		/// </summary>
        public void Insert(Model.ProcessCategory processCategory)
        {
			//
			// todo:add other logic here
			//
            //Validate(processCategory);
            if (accessor.Exists(processCategory.Id))
            {
                throw new Helper.InvalidValueException(Model.ProcessCategory.PROPERTY_ID);
            }
            //processCategory.ProcessCategoryId = Guid.NewGuid().ToString();
            processCategory.InsertTime = DateTime.Now;
            accessor.Insert(processCategory);
        }
		
		/// <summary>
		/// Update a ProcessCategory.
		/// </summary>
        public void Update(IList<Model.ProcessCategory> detail)
        {
			//
			// todo: add other logic here.
			//
           // Validate(processCategory);

            foreach (Model.ProcessCategory cate in detail)
            {
                if (string.IsNullOrEmpty(cate.Id))
                {
                    throw new Helper.RequireValueException(Model.ProcessCategory.PROPERTY_ID);
                }

                if (accessor.ExistsExcept(cate))
                {
                    throw new Helper.InvalidValueException(Model.ProcessCategory.PROPERTY_ID);
                }         

                if (string.IsNullOrEmpty(cate.ProcessCategoryName))
                {
                    throw new Helper.RequireValueException(Model.ProcessCategory.PROPERTY_PROCESSCATEGORYNAME);
                }

                if(accessor.ExistsName(cate.ProcessCategoryName,cate.ProcessCategoryId))
                {
                    throw new Helper.InvalidValueException(Model.ProcessCategory.PROPERTY_PROCESSCATEGORYNAME);
                }            
            }

            foreach (Model.ProcessCategory cate in detail)
            {                

                if (accessor.ExistsPrimary(cate.ProcessCategoryId))
                {                  
                     cate.UpdateTime = DateTime.Now;
                     accessor.Update(cate);
                }
                else
                {
                 
                    this.Insert(cate);
                }
            }            
        }

        //public void Validate(IList< Model.ProcessCategory> detail)
        //{
        //    if (string.IsNullOrEmpty(processCategory.Id))
        //        throw new Helper.RequireValueException(Model.ProcessCategory.PROPERTY_ID);

        //    if (string.IsNullOrEmpty(processCategory.ProcessCategoryName))
        //        throw new Helper.RequireValueException(Model.ProcessCategory.PROPERTY_PROCESSCATEGORYNAME);
        //}
    }
}

