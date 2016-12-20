//------------------------------------------------------------------------------
//
// file name：BOMProductProcessManager.cs
// author: peidun
// create date：2009-11-14 9:44:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BOMProductProcess.
    /// </summary>
    public partial class BOMProductProcessManager
    {
		
		/// <summary>
		/// Delete BOMProductProcess by primary key.
		/// </summary>
		public void Delete(string bOMProductProcessId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(bOMProductProcessId);
		}

		/// <summary>
		/// Insert a BOMProductProcess.
		/// </summary>
        public void Insert(Model.BOMProductProcess bOMProductProcess)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(bOMProductProcess);
        }
		
		/// <summary>
		/// Update a BOMProductProcess.
		/// </summary>
        public void Update(Model.BOMProductProcess bOMProductProcess)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(bOMProductProcess);
        }
        public void Delete(Model.BomParentPartInfo bomParentPartInfo)
        {
            accessor.Delete(bomParentPartInfo);
        }
        public IList<Model.BOMProductProcess> Select(string bomId)
        {
            return accessor.Select(bomId);
        }
    }
}

