//------------------------------------------------------------------------------
//
// file name：AtAccountingCategoryManager.cs
// author: mayanjun
// create date：2010-11-10 11:04:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtAccountingCategory.
    /// </summary>
    public partial class AtAccountingCategoryManager:BaseManager
    {
		
		/// <summary>
		/// Delete AtAccountingCategory by primary key.
		/// </summary>
		public void Delete(string accountingCategoryId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(accountingCategoryId);
		}

		/// <summary>
		/// Insert a AtAccountingCategory.
		/// </summary>
        public void Insert(Model.AtAccountingCategory atAccountingCategory)
        {
			//
			// todo:add other logic here
			//
            Validate(atAccountingCategory);
            atAccountingCategory.InsertTime = DateTime.Now;
            atAccountingCategory.AccountingCategoryId = Guid.NewGuid().ToString();
            accessor.Insert(atAccountingCategory);
        }
		
		/// <summary>
		/// Update a AtAccountingCategory.
		/// </summary>
        public void Update(Model.AtAccountingCategory atAccountingCategory)
        {
			//
			// todo: add other logic here.
			//
            Validate(atAccountingCategory);
            atAccountingCategory.UpdateTime = DateTime.Now;
            accessor.Update(atAccountingCategory);
        }
        private void Validate(Model.AtAccountingCategory atAccountingCategory)
        {
            if (string.IsNullOrEmpty(atAccountingCategory.Id))
            {
                throw new Helper.RequireValueException(Model.AtAccountingCategory.PRO_Id);
            }
            if (string.IsNullOrEmpty(atAccountingCategory.AccountingCategoriesId))
            {
                throw new Helper.RequireValueException(Model.AtAccountingCategory.PRO_AccountingCategoriesId);
            }
            if (string.IsNullOrEmpty(atAccountingCategory.AccountingCategoryName))
            {
                throw new Helper.RequireValueException(Model.AtAccountingCategory.PRO_AccountingCategoryName);
            }
        }
    }
}

