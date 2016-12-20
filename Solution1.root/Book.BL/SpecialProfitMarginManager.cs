//------------------------------------------------------------------------------
//
// file name：SpecialProfitMarginManager.cs
// author: peidun
// create date：2008/6/30 14:20:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.SpecialProfitMargin.
    /// </summary>
    public partial class SpecialProfitMarginManager : BaseManager
    {
		
		/// <summary>
		/// Delete SpecialProfitMargin by primary key.
		/// </summary>
		public void Delete(string specialProfitMarginId)
		{
			//
			// todo:add other logic here
			//
            accessor.Delete(specialProfitMarginId);
		}

		/// <summary>
		/// Insert a SpecialProfitMargin.
		/// </summary>
        public void Insert(Model.SpecialProfitMargin specialProfitMargin)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(specialProfitMargin);
        }
		
		/// <summary>
		/// Update a SpecialProfitMargin.
		/// </summary>
        public void Update(Model.SpecialProfitMargin specialProfitMargin)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(specialProfitMargin);
        }

        public System.Data.DataTable SelectDataTable()
        {
            return accessor.SelectDataTable();
        }

        public void UpdateDataTable(System.Data.DataTable table) 
        {
            accessor.UpdateDataTable(table);
        }
    }
}

