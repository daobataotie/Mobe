//------------------------------------------------------------------------------
//
// file name：TableManageAccessor.cs
// author: peidun
// create date：2009-11-23 10:26:17
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
    /// Data accessor of TableManage
    /// </summary>
    public partial class TableManageAccessor : EntityAccessor, ITableManageAccessor
    {
        public Model.TableManage GetIDbyname(string tablename)
        {
            return sqlmapper.QueryForObject<Model.TableManage>("TableManage.select_bytablename", tablename);
        }


        public Model.TableManage GetIDbyDBname(string dbtablename)
        {
            return sqlmapper.QueryForObject<Model.TableManage>("TableManage.select_byDBtablename", dbtablename);
     
        }
      
    }
}
