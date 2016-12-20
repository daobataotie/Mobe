//------------------------------------------------------------------------------
//
// file name：FreightedCompanyManager.cs
// author: peidun
// create date：2009-09-28 下午 06:31:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.FreightedCompany.
    /// </summary>
    public partial class FreightedCompanyManager:BaseManager
    {
		
		/// <summary>
		/// Delete FreightedCompany by primary key.
		/// </summary>
		public void Delete(string freightedCompany)
		{
			//
			// todo:add other logic here
			//           
			accessor.Delete(freightedCompany);
		}

		/// <summary>
		/// Insert a FreightedCompany.
		/// </summary>
        public void Insert(Model.FreightedCompany freightedCompany)
        {
			//
			// todo:add other logic here
			//
            Vilidate(freightedCompany);
            if (this.Exists(freightedCompany.Id))
            {
                throw new Helper.InvalidValueException(Model.FreightedCompany.PROPERTY_ID);
            }
            freightedCompany.FreightedCompanyId = Guid.NewGuid().ToString();
            accessor.Insert(freightedCompany);
        }
		
		/// <summary>
		/// Update a FreightedCompany.
		/// </summary>
        public void Update(Model.FreightedCompany freightedCompany)
        {
			//
			// todo: add other logic here.
			//
            Vilidate(freightedCompany);
            if (this.ExistsExcept(freightedCompany))
            {
                throw new Helper.InvalidValueException(Model.FreightedCompany.PROPERTY_ID);
            }
            accessor.Update(freightedCompany);
        }
        private void Vilidate(Model.FreightedCompany freightedCompany)
        {
            if (string.IsNullOrEmpty(freightedCompany.Id))
            {
                throw new Helper.RequireValueException(Model.FreightedCompany.PROPERTY_ID);
            }
            if (string.IsNullOrEmpty(freightedCompany.FreightedCompanyName))
            {
                throw new Helper.RequireValueException(Model.FreightedCompany.PROPERTY_FREIGHTEDCOMPANYNAME);
            }
    

             
        }
    }
}

