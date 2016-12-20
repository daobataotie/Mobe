//------------------------------------------------------------------------------
//
// file name：ProductProcessManager.cs
// author: peidun
// create date：2010-1-27 10:46:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProductProcess.
    /// </summary>
    public partial class ProductProcessManager
    {
		
		/// <summary>
		/// Delete ProductProcess by primary key.
		/// </summary>
		public void Delete(string productProcessId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(productProcessId);
		}

		/// <summary>
		/// Insert a ProductProcess.
		/// </summary>
        public void Insert(Model.ProductProcess productProcess)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(productProcess);
        }
		
		/// <summary>
		/// Update a ProductProcess.
		/// </summary>
        public void Update(Model.ProductProcess productProcess)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(productProcess);
        }
        public IList<Model.ProductProcess> Select(string productId)
        {
           return accessor.Select(productId);
        }
        public IList<Model.ProductProcess> SelectByBomId(string bomid)
        {
            return accessor.SelectByBomId(bomid);
        }
    }
}

