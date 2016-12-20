//------------------------------------------------------------------------------
//
// file name：ProceduresMachineManager.cs
// author: mayanjun
// create date：2010-9-17 16:47:15
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProceduresMachine.
    /// </summary>
    public partial class ProceduresMachineManager
    {
		
		/// <summary>
		/// Delete ProceduresMachine by primary key.
		/// </summary>
		public void Delete(string proceduresMachineId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(proceduresMachineId);
		}

		/// <summary>
		/// Insert a ProceduresMachine.
		/// </summary>
        public void Insert(Model.ProceduresMachine proceduresMachine)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(proceduresMachine);
        }
		
		/// <summary>
		/// Update a ProceduresMachine.
		/// </summary>
        public void Update(Model.ProceduresMachine proceduresMachine)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(proceduresMachine);
        }

        public void DelelteByProduresMachines(string ProceduresId)
        {
            accessor.DelelteByProduresMachines(ProceduresId);
        }
    }
}

