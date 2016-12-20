//------------------------------------------------------------------------------
//
// file name：AtProfitLossManager.cs
// author: mayanjun
// create date：2011-2-25 10:53:27
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtProfitLoss.
    /// </summary>
    public partial class AtProfitLossManager
    {
		
		/// <summary>
		/// Delete AtProfitLoss by primary key.
		/// </summary>
		public void Delete(string profitLossId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(profitLossId);
		}

		/// <summary>
		/// Insert a AtProfitLoss.
		/// </summary>
        public void Insert(Model.AtProfitLoss atProfitLoss)
        {
			//
			// todo:add other logic here
			//
            Validate(atProfitLoss);
            atProfitLoss.ProfitLossId = Guid.NewGuid().ToString();
            atProfitLoss.InsertTime = DateTime.Now;
            accessor.Insert(atProfitLoss);
        }
		
		/// <summary>
		/// Update a AtProfitLoss.
		/// </summary>
        public void Update(Model.AtProfitLoss atProfitLoss)
        {
			//
			// todo: add other logic here.
			//
            Validate(atProfitLoss);
            atProfitLoss.UpdateTime = DateTime.Now;
            accessor.Update(atProfitLoss);
        }
        private void Validate(Model.AtProfitLoss atProfitLoss)
        {
            if (string.IsNullOrEmpty(atProfitLoss.ProfitLossCategory))
            {
                throw new Helper.RequireValueException(Model.AtProfitLoss.PRO_ProfitLossCategory);
            }
            if (string.IsNullOrEmpty(atProfitLoss.CategoriesName))
            {
                throw new Helper.RequireValueException(Model.AtProfitLoss.PRO_CategoriesName);
            }
            if (string.IsNullOrEmpty(atProfitLoss.SubjectId))
            {
                throw new Helper.RequireValueException(Model.AtProfitLoss.PRO_SubjectId);
            }
        }
        public IList<Book.Model.AtProfitLoss> Select(DateTime startDate, DateTime endDate)
        {
            return accessor.Select(startDate, endDate);
        }
    }
}

