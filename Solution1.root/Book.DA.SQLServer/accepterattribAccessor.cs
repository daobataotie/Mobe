//------------------------------------------------------------------------------
//
// file name：accepterattribAccessor.cs
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
    /// Data accessor of accepterattrib
    /// </summary>
    public partial class accepterattribAccessor : EntityAccessor, IaccepterattribAccessor
    {
        public void DeleteByProcessId(string processid)
        {
            sqlmapper.Delete("accepterattrib.DeleteByProcessId", processid);
        }

        public IList<Model.accepterattrib> SelectByProcessId(string processid)
        {
            return sqlmapper.QueryForList<Model.accepterattrib>("accepterattrib.SelectByProcessId", processid);
        }
    }
}
