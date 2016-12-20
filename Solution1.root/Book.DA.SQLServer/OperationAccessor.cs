//------------------------------------------------------------------------------
//
// file name:OperationAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of Operation
    /// </summary>
    public partial class OperationAccessor : EntityAccessor, IOperationAccessor
    {
        public IList<Model.Operation> Select_KeyTag0()
        {
            return sqlmapper.QueryForList<Model.Operation>("Operation.select_KeyTag0",null);        
        }
        public IList<Model.Operation> Select_ByParent(string ParentId)
        {
            return sqlmapper.QueryForList<Model.Operation>("Operation.select_ByParent", ParentId);
        }

        public string GetOperationNamebyTabel(string tableName)
        {
            string a = null;
            if (sqlmapper.QueryForObject("Operation.get_byTabelName", tableName)!=null)
                a = sqlmapper.QueryForObject("Operation.get_byTabelName", tableName).ToString();
            return a;
        }
    }
}
