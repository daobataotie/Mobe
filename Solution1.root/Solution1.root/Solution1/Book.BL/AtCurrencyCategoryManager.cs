//------------------------------------------------------------------------------
//
// file name：AtCurrencyCategoryManager.cs
// author: mayanjun
// create date：2011-6-8 10:03:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtCurrencyCategory.
    /// </summary>
    public partial class AtCurrencyCategoryManager:BaseManager
    {
		
		/// <summary>
		/// Delete AtCurrencyCategory by primary key.
		/// </summary>
		public void Delete(string atCurrencyCategoryId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(atCurrencyCategoryId);
		}

		/// <summary>
		/// Insert a AtCurrencyCategory.
		/// </summary>
        public void Insert(Model.AtCurrencyCategory atCurrencyCategory)
        {
			//
			// todo:add other logic here
			//
            Validate(atCurrencyCategory);
            atCurrencyCategory.AtCurrencyCategoryId = Guid.NewGuid().ToString();
            accessor.Insert(atCurrencyCategory);
        }
		
		/// <summary>
		/// Update a AtCurrencyCategory.
		/// </summary>
        public void Update(Model.AtCurrencyCategory atCurrencyCategory)
        {
			//
			// todo: add other logic here.
			//
            Validate(atCurrencyCategory);
            accessor.Update(atCurrencyCategory);
        }
        private void Validate(Model.AtCurrencyCategory atCurrencyCategory)
        {
            if (string.IsNullOrEmpty(atCurrencyCategory.Id))
            {
                throw new Helper.RequireValueException(Model.AtCurrencyCategory.PRO_Id);
            }
            if (string.IsNullOrEmpty(atCurrencyCategory.AtCurrencyName))
            {
                throw new Helper.RequireValueException(Model.AtCurrencyCategory.PRO_AtCurrencyName);
            }
        }
    }
}

