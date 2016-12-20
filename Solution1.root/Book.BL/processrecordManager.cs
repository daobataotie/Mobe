//------------------------------------------------------------------------------
//
// file name：processrecordManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.processrecord.
    /// </summary>
    public partial class processrecordManager
    {
		
		/// <summary>
		/// Delete processrecord by primary key.
		/// </summary>
		public void Delete(string processrecordID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(processrecordID);
		}

		/// <summary>
		/// Insert a processrecord.
		/// </summary>
        public void Insert(Model.processrecord processrecord)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(processrecord);
        }
		
		/// <summary>
		/// Update a processrecord.
		/// </summary>
        public void Update(Model.processrecord processrecord)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(processrecord);
        }
    }
}

