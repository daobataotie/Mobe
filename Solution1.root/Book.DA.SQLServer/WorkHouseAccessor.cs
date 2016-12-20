//------------------------------------------------------------------------------
//
// file name：WorkHouseAccessor.cs
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
    /// Data accessor of WorkHouse
    /// </summary>
    public partial class WorkHouseAccessor : EntityAccessor, IWorkHouseAccessor
    {
        public bool ExistsWorkHouseCode(string WorkHouseCode)
        {
            return sqlmapper.QueryForObject<bool>("WorkHouse.ExistsWorkHouseCode", WorkHouseCode);
        }
    }
}
