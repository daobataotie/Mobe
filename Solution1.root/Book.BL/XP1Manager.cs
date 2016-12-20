//------------------------------------------------------------------------------
//
// file name：XP1Manager.cs
// author: peidun
// create date：2008/6/6 10:01:00
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.XP1.
    /// </summary>
    public partial class XP1Manager : BaseManager
    {
		
		/// <summary>
		/// Delete XP1 by primary key.
		/// </summary>
		public void Delete(string xP1Id)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(xP1Id);
		}

		/// <summary>
		/// Insert a XP1.
		/// </summary>
        public void Insert(Model.XP1 xP1)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(xP1);
        }
		
		/// <summary>
		/// Update a XP1.
		/// </summary>
        public void Update(Model.XP1 xP1)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(xP1);
        }
		
    }
}

