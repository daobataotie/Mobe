//------------------------------------------------------------------------------
//
// file name：XR2Manager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.XR2.
    /// </summary>
    public partial class XR2Manager : BaseManager
    {
		
		/// <summary>
		/// Delete XR2 by primary key.
		/// </summary>
		public void Delete(string xR2Id)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(xR2Id);
		}

		/// <summary>
		/// Insert a XR2.
		/// </summary>
        public void Insert(Model.XR2 xR2)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(xR2);
        }
		
		/// <summary>
		/// Update a XR2.
		/// </summary>
        public void Update(Model.XR2 xR2)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(xR2);
        }
		
    }
}

