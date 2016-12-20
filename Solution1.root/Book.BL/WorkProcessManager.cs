//------------------------------------------------------------------------------
//
// file name：WorkProcessManager.cs
// author: peidun
// create date：2009-08-25 17:08:53
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.WorkProcess.
    /// </summary>
    public partial class WorkProcessManager : BaseManager
    {
		
		/// <summary>
		/// Delete WorkProcess by primary key.
		/// </summary>
		public void Delete(string 工序编号)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(工序编号);
		}

		/// <summary>
		/// Insert a WorkProcess.
		/// </summary>
        public void Insert(Model.WorkProcess workProcess)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(workProcess);
        }
		
		/// <summary>
		/// Update a WorkProcess.
		/// </summary>
        public void Update(Model.WorkProcess workProcess)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(workProcess);
        }

        //protected override string GetInvoiceKind()
        //{
        //    return base.GetInvoiceKind();
        //}

        //protected override string GetSettingId()
        //{
        //    return base.GetSettingId();
        //}
    }
}

