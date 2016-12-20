//------------------------------------------------------------------------------
//
// file name：WorkflowAccessor.cs
// author: peidun
// create date：2009-11-18 15:33:08
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
    /// Data accessor of Workflow
    /// </summary>
    public partial class WorkflowAccessor : EntityAccessor, IWorkflowAccessor
    {
        public void addprocess(Model.process process)
        {
            //return sqlmapper.QueryForList<Model.Setting>("Setting.select_by_tag", tag);
            sqlmapper.Insert("insertautoprocess", process);
        }

        public bool getTable(string id)
        {
            return sqlmapper.QueryForObject<bool>("Workflow.has_rows_table",id);
        }

        public Model.Workflow getWfbytable(string tableid)
        {
            return sqlmapper.QueryForObject<Model.Workflow>("Workflow.select_by_tableid", tableid);
        }
    }
}
