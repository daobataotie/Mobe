//------------------------------------------------------------------------------
//
// file name：AtInvoiceSetManager.cs
// author: mayanjun
// create date：2010-11-15 10:11:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtInvoiceSet.
    /// </summary>
    public partial class AtInvoiceSetManager
    {
		
		/// <summary>
		/// Delete AtInvoiceSet by primary key.
		/// </summary>
		public void Delete(string id)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(id);
		}

		/// <summary>
		/// Insert a AtInvoiceSet.
		/// </summary>
        public void Insert(Model.AtInvoiceSet atInvoiceSet)
        {
			//
			// todo:add other logic here
			//
            atInvoiceSet.Id = Guid.NewGuid().ToString();
            atInvoiceSet.InsertTime = DateTime.Now;
            accessor.Insert(atInvoiceSet);
        }
		
		/// <summary>
		/// Update a AtInvoiceSet.
		/// </summary>
        public void Update(Model.AtInvoiceSet atInvoiceSet)
        {
			//
			// todo: add other logic here.
			//
            atInvoiceSet.UpdateTime = DateTime.Now;
            accessor.Update(atInvoiceSet);
        }
    }
}

