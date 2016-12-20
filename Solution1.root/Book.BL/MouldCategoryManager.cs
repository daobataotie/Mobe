//------------------------------------------------------------------------------
//
// file name：MouldCategoryManager.cs
// author: peidun
// create date：2009-07-24 11:18:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MouldCategory.
    /// </summary>
    public partial class MouldCategoryManager
    {

        /// <summary>
        /// Delete MouldCategory by primary key.
        /// </summary>
        public void Delete(string mouldCategoryId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(mouldCategoryId);
        }

        /// <summary>
        /// Insert a MouldCategory.
        /// </summary>
        public void Insert(Model.MouldCategory mouldCategory)
        {
            validate(mouldCategory);
            mouldCategory.InsertTime = DateTime.Now;
            mouldCategory.UpdateTime = DateTime.Now;
            mouldCategory.MouldCategoryId = Guid.NewGuid().ToString();
            accessor.Insert(mouldCategory);
        }

        private void validate(Book.Model.MouldCategory mouldCategory)
        {
            if (string.IsNullOrEmpty(mouldCategory.Id))
                throw new Helper.RequireValueException(Model.MouldCategory.PROPERTY_ID);
            if (string.IsNullOrEmpty(mouldCategory.MouldCategoryName))
                throw new Helper.RequireValueException(Model.MouldCategory.PROPERTY_MOULDCATEGORYNAME);
            if (IsExistId(mouldCategory))
                throw new Helper.InvalidValueException(Model.MouldCategory.PROPERTY_ID);
            if (IsExistMouldCategoryName(mouldCategory))
                throw new Helper.InvalidValueException(Model.MouldCategory.PROPERTY_MOULDCATEGORYNAME);           

        }

        /// <summary>
        /// Update a MouldCategory.
        /// </summary>
        public void Update(Model.MouldCategory mouldCategory)
        {
            validate(mouldCategory);
            mouldCategory.UpdateTime = DateTime.Now;
            accessor.Update(mouldCategory);
        }

        public bool IsExistMouldCategoryName(Model.MouldCategory mould)
        {
            return accessor.IsExistMouldCategoryName(mould);
        }

        public bool IsExistId(Model.MouldCategory mould)
        {
            return accessor.IsExistId(mould);
        }
    }
}

