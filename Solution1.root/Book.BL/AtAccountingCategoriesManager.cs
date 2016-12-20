//------------------------------------------------------------------------------
//
// file name：AtAccountingCategoriesManager.cs
// author: mayanjun
// create date：2010-11-10 11:04:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtAccountingCategories.
    /// </summary>
    public partial class AtAccountingCategoriesManager:BaseManager
    {
		
		/// <summary>
		/// Delete AtAccountingCategories by primary key.
		/// </summary>
		public void Delete(string accountingCategoriesId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(accountingCategoriesId);
		}
        public void Delete(Model.AtAccountingCategories atAccountingCategories)
        {
            this.Delete(atAccountingCategories.AccountingCategoriesId);
        }
		/// <summary>
		/// Insert a AtAccountingCategories.
		/// </summary>
        public void Insert(Model.AtAccountingCategories atAccountingCategories)
        {
			//
			// todo:add other logic here
			//
            atAccountingCategories.AccountingCategoriesId = Guid.NewGuid().ToString();
            accessor.Insert(atAccountingCategories);
        }
		
		/// <summary>
		/// Update a AtAccountingCategories.
		/// </summary>
        public void Update(IList<Model.AtAccountingCategories> atAccountingCategories)
        {
			//
			// todo: add other logic here.
			//

            foreach (Model.AtAccountingCategories pc in atAccountingCategories)
            {

                if (accessor.ExistsPrimary(pc.AccountingCategoriesId))
                {
                    accessor.Update(pc);
                }

                else
                {
                    if (string.IsNullOrEmpty(pc.Id) || string.IsNullOrEmpty(pc.Id))
                    {
                        throw new Helper.RequireValueException(Model.AtAccountingCategories.PRO_Id);
                    }
                    this.Insert(pc);
                }
            }
        }
    }
}

