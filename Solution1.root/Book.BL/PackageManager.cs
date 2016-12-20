//------------------------------------------------------------------------------
//
// file name：PackageManager.cs
// author: peidun
// create date：2009-08-12 9:45:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Package.
    /// </summary>
    public partial class PackageManager
    {
		
		/// <summary>
		/// Delete Package by primary key.
		/// </summary>
		public void Delete(string packageId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(packageId);
		}

		/// <summary>
		/// Insert a Package.
		/// </summary>
        public void Insert(Model.Package package)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(package);
        }
		
		/// <summary>
		/// Update a Package.
		/// </summary>
        public void Update(Model.Package package)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(package);
        }
		
    }
}

