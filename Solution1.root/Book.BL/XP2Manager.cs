//------------------------------------------------------------------------------
//
// file name：XP2Manager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.XP2.
    /// </summary>
    public partial class XP2Manager : BaseManager
    {
		
		/// <summary>
		/// Delete XP2 by primary key.
		/// </summary>
		public void Delete(string xP2Id)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(xP2Id);
		}

		/// <summary>
		/// Insert a XP2.
		/// </summary>
        public void Insert(Model.XP2 xP2)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(xP2);
        }
		
		/// <summary>
		/// Update a XP2.
		/// </summary>
        public void Update(Model.XP2 xP2)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(xP2);
        }
		
    }
}

