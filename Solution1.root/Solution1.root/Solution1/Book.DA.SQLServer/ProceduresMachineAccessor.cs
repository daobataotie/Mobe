//------------------------------------------------------------------------------
//
// file name：ProceduresMachineAccessor.cs
// author: mayanjun
// create date：2010-9-17 16:47:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of ProceduresMachine
    /// </summary>
    public partial class ProceduresMachineAccessor : EntityAccessor, IProceduresMachineAccessor
    {
        public void DelelteByProduresMachines(string ProceduresId)
        {
            sqlmapper.Delete("ProceduresMachine.deleteByProceduresId", ProceduresId);
        }
    }
}
