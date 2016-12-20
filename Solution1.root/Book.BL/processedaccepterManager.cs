//------------------------------------------------------------------------------
//
// file name：processedaccepterManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.processedaccepter.
    /// </summary>
    public partial class processedaccepterManager
    {
		
		/// <summary>
		/// Delete processedaccepter by primary key.
		/// </summary>
		public void Delete(string processedaccepterid)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(processedaccepterid);
		}

		/// <summary>
		/// Insert a processedaccepter.
		/// </summary>
        public void Insert(Model.processedaccepter processedaccepter)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(processedaccepter);
        }
		
		/// <summary>
		/// Update a processedaccepter.
		/// </summary>
        public void Update(Model.processedaccepter processedaccepter)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(processedaccepter);
        }
    }
}

