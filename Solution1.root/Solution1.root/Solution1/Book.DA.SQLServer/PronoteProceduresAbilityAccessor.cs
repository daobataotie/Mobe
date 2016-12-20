//------------------------------------------------------------------------------
//
// file name：PronoteProceduresAbilityAccessor.cs
// author: mayanjun
// create date：2010-9-23 14:25:13
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
    /// Data accessor of PronoteProceduresAbility
    /// </summary>
    public partial class PronoteProceduresAbilityAccessor : EntityAccessor, IPronoteProceduresAbilityAccessor
    {

        public Model.PronoteProceduresAbility GetByProcedures(string ProceduresId)
        {
            return sqlmapper.QueryForObject<Model.PronoteProceduresAbility>("PronoteProceduresAbility.getByProcedures", ProceduresId);
        }
    }
}
