//------------------------------------------------------------------------------
//
// file name：BomPackageDetailsManager.cs
// author: peidun
// create date：2009-11-12 11:03:17
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BomPackageDetails.
    /// </summary>
    public partial class BomPackageDetailsManager:BaseManager
    {
		
		/// <summary>
		/// Delete BomPackageDetails by primary key.
		/// </summary>
		public void Delete(string bomPackageDetailsId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(bomPackageDetailsId);
		}

		/// <summary>
		/// Insert a BomPackageDetails.
		/// </summary>
        public void Insert(Model.BomPackageDetails bomPackageDetails)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(bomPackageDetails);
        }
		
		/// <summary>
		/// Update a BomPackageDetails.
		/// </summary>
        public void Update(Model.BomPackageDetails bomPackageDetails)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(bomPackageDetails);
        }
        public IList<Model.BomPackageDetails> Select(string bomId)
        {
          return    accessor.Select(bomId);
        }
        public void Delete(Model.BomParentPartInfo bom)
        {
            accessor.Delete(bom);
        }
    }
}

