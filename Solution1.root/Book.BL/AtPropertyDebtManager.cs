//------------------------------------------------------------------------------
//
// file name：AtPropertyDebtManager.cs
// author: mayanjun
// create date：2011-2-28 15:30:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtPropertyDebt.
    /// </summary>
    public partial class AtPropertyDebtManager
    {
		
		/// <summary>
		/// Delete AtPropertyDebt by primary key.
		/// </summary>
		public void Delete(string propertyDebtId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(propertyDebtId);
		}

		/// <summary>
		/// Insert a AtPropertyDebt.
		/// </summary>
        public void Insert(Model.AtPropertyDebt atPropertyDebt)
        {
			//
			// todo:add other logic here
			//
            Validate(atPropertyDebt);
            atPropertyDebt.PropertyDebtId = Guid.NewGuid().ToString();
            atPropertyDebt.InsertTime = DateTime.Now;
            accessor.Insert(atPropertyDebt);
        }
		
		/// <summary>
		/// Update a AtPropertyDebt.
		/// </summary>
        public void Update(Model.AtPropertyDebt atPropertyDebt)
        {
			//
			// todo: add other logic here.
			//
            Validate(atPropertyDebt);
            atPropertyDebt.UpdateTime = DateTime.Now;
            accessor.Update(atPropertyDebt);
        }
        private void Validate(Model.AtPropertyDebt atPropertyDebt)
        {
            if (string.IsNullOrEmpty(atPropertyDebt.CategoryName))
            {
                throw new Helper.RequireValueException(Model.AtPropertyDebt.PRO_CategoryName);
            }
            if (string.IsNullOrEmpty(atPropertyDebt.CategoriesName))
            {
                throw new Helper.RequireValueException(Model.AtPropertyDebt.PRO_CategoriesName);
            }
            if (string.IsNullOrEmpty(atPropertyDebt.SubjectId))
            {
                throw new Helper.RequireValueException(Model.AtPropertyDebt.PRO_SubjectId);
            }
        }
        public IList<Book.Model.AtPropertyDebt> Select(DateTime startDate, DateTime endDate)
        {
            return accessor.Select(startDate,endDate);
        }
    }
}

