//------------------------------------------------------------------------------
//
// file name：OperationRoleAccessor.cs
// author: peidun
// create date：2009-09-17 上午 11:49:42
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
    /// Data accessor of OperationRole
    /// </summary>
    public partial class OperationRoleAccessor : EntityAccessor, IOperationRoleAccessor
    {
        public void Delete(Model.Operators operators)
        {

            sqlmapper.Delete("OperationRole.deleteByOperatorsId", operators.OperatorsId);
        }
        public  IList<Model.OperationRole>  Select(Model.Operators operators)
        {
            return sqlmapper.QueryForList<Model.OperationRole>("OperationRole.selectByOperationId", operators.OperatorsId);
        }
    }
}
