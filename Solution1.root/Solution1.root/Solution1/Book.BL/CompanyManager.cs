//------------------------------------------------------------------------------
//
// file name：CompanyManager.cs
// author: peidun
// create date：2009-09-02 上午 10:38:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL   
{
    /// <summary>
    /// Business logic for dbo.Company.
    /// </summary>
    public partial class CompanyManager:BaseManager
    {		
		/// <summary>
		/// Delete Company by primary key.
		/// </summary>
		public void Delete(string companyId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(companyId);
		}

		/// <summary>
		/// Insert a Company.
		/// </summary>
        public void Insert(Model.Company company)
        {
			//
			// todo:add other logic here
			//
            Validate(company);
            company.InsertTime = DateTime.Now;
            company.CompanyId = Guid.NewGuid().ToString();
            accessor.Insert(company);
        }
		
		/// <summary>
		/// Update a Company.
		/// </summary>
        public void Update(Model.Company company)
        {
			//
			// todo: add other logic here.
			//
            Validate(company);
            company.UpdateTime = DateTime.Now;
            accessor.Update(company);
        }        
        private void Validate(Model.Company company)
        {
            if (string.IsNullOrEmpty(company.CompanyName))
            {
                throw new Helper.RequireValueException(Model.Company.PROPERTY_COMPANYNAME);
            }
            if (accessor.IsExistsCompanyName(company.CompanyId, company.CompanyName))
                throw new Helper.InvalidValueException(Model.Company.PROPERTY_COMPANYNAME);
        }

        public Model.Company SelectIsDefaultCompany()
        {
            return accessor.SelectIsDefaultCompany();
        }
    }
}

