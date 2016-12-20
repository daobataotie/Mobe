//------------------------------------------------------------------------------
//
// file name：RalationProductMouldManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.RalationProductMould.
    /// </summary>
    public partial class RalationProductMouldManager : BaseManager
    {
		
		/// <summary>
		/// Delete RalationProductMould by primary key.
		/// </summary>
		public void Delete(string pkid)
		{
			//
			// todo:add other logic here
			//
            accessor.Delete(pkid);
		}

		/// <summary>
		/// Insert a RalationProductMould.
		/// </summary>
        public void Insert(Model.RalationProductMould ralationProductMould)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(ralationProductMould);
        }
		
		/// <summary>
		/// Update a RalationProductMould.
		/// </summary>
        public void Update(Model.RalationProductMould ralationProductMould)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(ralationProductMould);
        }
		
    }
}

