//------------------------------------------------------------------------------
//
// file name：AtDepreciationDetailManager.cs
// author: mayanjun
// create date：2010-11-18 14:50:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtDepreciationDetail.
    /// </summary>
    public partial class AtDepreciationDetailManager
    {
		
		/// <summary>
		/// Delete AtDepreciationDetail by primary key.
		/// </summary>
		public void Delete(string depreciationId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(depreciationId);
		}

		/// <summary>
		/// Insert a AtDepreciationDetail.
		/// </summary>
        public void Insert(Model.AtDepreciationDetail atDepreciationDetail)
        {
			//
			// todo:add other logic here
			//
            Validate(atDepreciationDetail);
            atDepreciationDetail.InsertTime = DateTime.Now;
            atDepreciationDetail.DepreciationId = Guid.NewGuid().ToString();
            accessor.Insert(atDepreciationDetail);
        }
		
		/// <summary>
		/// Update a AtDepreciationDetail.
		/// </summary>
        public void Update(Model.AtDepreciationDetail atDepreciationDetail)
        {
			//
			// todo: add other logic here.
			//
            Validate(atDepreciationDetail);
            atDepreciationDetail.UpdateTime = DateTime.Now;
            accessor.Update(atDepreciationDetail);
        }
        private void Validate(Model.AtDepreciationDetail atDepreciationDetail)
        {
            if (string.IsNullOrEmpty(atDepreciationDetail.PropertyId))
            {
                throw new Helper.RequireValueException(Model.AtDepreciationDetail.PRO_PropertyId);
            }
        }
    }
}

