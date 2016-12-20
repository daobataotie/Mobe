//------------------------------------------------------------------------------
//
// file name：wfrecordAccessor.cs
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
    /// Data accessor of wfrecord
    /// </summary>
    public partial class wfrecordAccessor : EntityAccessor, IwfrecordAccessor
    {
        public IList<Model.wfrecord> GetMyexaming(Model.Operators operators)
        {
            return sqlmapper.QueryForList<Model.wfrecord>("wfrecord.select_myexaming", operators.OperatorsId);

        }



        
    }
}
