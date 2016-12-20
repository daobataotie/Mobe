//------------------------------------------------------------------------------
//
// file name：AcItemAccessor.cs
// author: mayanjun
// create date：2012-2-21 13:36:09
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
    /// Data accessor of AcItem
    /// </summary>
    public partial class AcItemAccessor : EntityAccessor, IAcItemAccessor
    {
        public void DeleteALL()
        {
            sqlmapper.Delete("AcItem.DeleteALL", null);
        }

        public string SelectPriIdByName(string itemname)
        {
            return sqlmapper.QueryForObject<string>("AcItem.SelectPriIdByName", itemname).ToString();
        }
    }
}
