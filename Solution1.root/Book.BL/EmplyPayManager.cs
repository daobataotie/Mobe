//------------------------------------------------------------------------------
//
// file name：EmplyPayManager.cs
// author: peidun
// create date：2010-3-24 11:21:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.EmplyPay.
    /// </summary>
    public partial class EmplyPayManager
    {
		
		/// <summary>
		/// Delete EmplyPay by primary key.
		/// </summary>
		public void Delete(string emplyPayId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(emplyPayId);
		}

		/// <summary>
		/// Insert a EmplyPay.
		/// </summary>
        public void Insert(Model.EmplyPay emplyPay)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(emplyPay);
        }
		
		/// <summary>
		/// Update a EmplyPay.
		/// </summary>
        public void Update(Model.EmplyPay emplyPay)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(emplyPay);
        }
    }
}

