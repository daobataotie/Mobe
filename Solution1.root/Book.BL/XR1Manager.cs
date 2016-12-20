//------------------------------------------------------------------------------
//
// file name：XR1Manager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.XR1.
    /// </summary>
    public partial class XR1Manager : BaseManager
    {
		
		/// <summary>
		/// Delete XR1 by primary key.
		/// </summary>
		public void Delete(string xR1Id)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(xR1Id);
		}

		/// <summary>
		/// Insert a XR1.
		/// </summary>
        public void Insert(Model.XR1 xR1)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(xR1);
        }
		
		/// <summary>
		/// Update a XR1.
		/// </summary>
        public void Update(Model.XR1 xR1)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(xR1);
        }
		
    }
}

