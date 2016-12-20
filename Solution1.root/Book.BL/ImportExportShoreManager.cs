//------------------------------------------------------------------------------
//
// file name：ImportExportShoreManager.cs
// author: mayanjun
// create date：2013-4-6 15:35:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ImportExportShore.
    /// </summary>
    public partial class ImportExportShoreManager
    {
		
		/// <summary>
		/// Delete ImportExportShore by primary key.
		/// </summary>
		public void Delete(string importExportShoreId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(importExportShoreId);
		}

		/// <summary>
		/// Insert a ImportExportShore.
		/// </summary>
        public void Insert(Model.ImportExportShore importExportShore)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(importExportShore);
        }
		
		/// <summary>
		/// Update a ImportExportShore.
		/// </summary>
        public void Update(Model.ImportExportShore importExportShore)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(importExportShore);
        }
    }
}

