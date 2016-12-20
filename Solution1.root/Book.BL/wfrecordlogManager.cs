//------------------------------------------------------------------------------
//
// file name：wfrecordlogManager.cs
// author: peidun
// create date：2009-12-11 14:53:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.wfrecordlog.
    /// </summary>
    public partial class wfrecordlogManager
    {
		
		/// <summary>
		/// Delete wfrecordlog by primary key.
		/// </summary>
		public void Delete(string wfrecordlogid)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(wfrecordlogid);
		}

		/// <summary>
		/// Insert a wfrecordlog.
		/// </summary>
        public void Insert(Model.wfrecordlog wfrecordlog)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(wfrecordlog);
        }
		
		/// <summary>
		/// Update a wfrecordlog.
		/// </summary>
        public void Update(Model.wfrecordlog wfrecordlog)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(wfrecordlog);
        }
    }
}

