//------------------------------------------------------------------------------
//
// file name：TablesAccessor.cs
// author: peidun
// create date：2009-12-11 14:53:04
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
    /// Data accessor of Tables
    /// </summary>
    public partial class TablesAccessor : EntityAccessor, ITablesAccessor
    {
         //return sqlmapper.QueryForObject<Model.TableManage>("TableManage.select_bytablename", tablename);

        public Model.Tables GetIDbyname(string tablename)
        {
            return sqlmapper.QueryForObject<Model.Tables>("Tables.select_bytablename", tablename);
        }
      //  public Model.Tables GetIDbycode(string tablename)
      //{
      //    return sqlmapper.QueryForObject<Model.Tables>("Tables.select_bytablecode", tablename);
      //}     
    }
}
