//------------------------------------------------------------------------------
//
// file name：CompanyLevelManager.cs
// author: peidun
// create date：2008/6/30 14:20:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CompanyLevel.
    /// </summary>
    public partial class CompanyLevelManager : BaseManager
    {
		
		/// <summary>
		/// Delete CompanyLevel by primary key.
		/// </summary>
		public void Delete(string companyLevelId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(companyLevelId);
		}
        public void Delete(Model.CompanyLevel companyLevel)
        {
            this.Delete(companyLevel.CompanyLevelId);
        }
		/// <summary>
		/// Insert a CompanyLevel.
		/// </summary>
        public void Insert(Model.CompanyLevel companyLevel)
        {
			//
			// todo:add other logic here
			//
            Validate(companyLevel);

            if (this.HasRows(companyLevel.CompanyLevelId))
            {
                throw new Helper.InvalidValueException(Model.CompanyLevel.PROPERTY_COMPANYLEVELID);
            }

            companyLevel.InsertTime = DateTime.Now;
            companyLevel.UpdateTime = DateTime.Now;

            accessor.Insert(companyLevel);
        }

        private void Validate(Model.CompanyLevel companyLevel) 
        {
            if(string.IsNullOrEmpty(companyLevel.CompanyLevelId))
            {
                throw new Helper.RequireValueException(Model.CompanyLevel.PROPERTY_COMPANYLEVELID);
            }
            if (string.IsNullOrEmpty(companyLevel.CompanyLevelName)) 
            {
                throw new Helper.RequireValueException(Model.CompanyLevel.PROPERTY_COMPANYLEVELNAME);
            }
        }

		/// <summary>
		/// Update a CompanyLevel.
		/// </summary>
        public void Update(Model.CompanyLevel companyLevel)
        {
			//
			// todo: add other logic here.
			//
            Validate(companyLevel);
            companyLevel.UpdateTime = DateTime.Now;
            accessor.Update(companyLevel);
        }
        public string GetNewId()
        {
            return Guid.NewGuid().ToString();
        }
        public System.Data.DataTable SelectDateTable() 
        {
            return accessor.SelectDateTable();
        }
        public void UpdateDataTable(System.Data.DataTable table) 
        {
            accessor.UpdateDataTable(table);
        }
    }
}

