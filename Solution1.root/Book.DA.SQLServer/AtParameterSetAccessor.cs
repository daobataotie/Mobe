//------------------------------------------------------------------------------
//
// file name：AtParameterSetAccessor.cs
// author: mayanjun
// create date：2012-3-26 14:33:25
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
    /// Data accessor of AtParameterSet
    /// </summary>
    public partial class AtParameterSetAccessor : EntityAccessor, IAtParameterSetAccessor
    {
        public Book.Model.AtParameterSet SelectByAtCurrentlyYear(int myear)
        {
            return sqlmapper.QueryForObject<Model.AtParameterSet>("AtParameterSet.SelectByAtCurrentlyYear", myear.ToString());
        }

        public void UpdateIsThisYear(string notId)
        {
            sqlmapper.Delete("AtParameterSet.UpdateIsThisYear", notId);
        }
    }
}
