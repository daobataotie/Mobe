//------------------------------------------------------------------------------
//
// file name：HrAttendStatAccessor.cs
// author: mayanjun
// create date：2010-7-6 11:09:56
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
    /// Data accessor of HrAttendStat
    /// </summary>
    public partial class HrAttendStatAccessor : EntityAccessor, IHrAttendStatAccessor
    {
      public Model.HrAttendStat SelectHrAttendStatByEmpidAndYearMonth(Model.Employee employee, int year, int month)
      {
          Hashtable pt = new Hashtable();
          pt.Add("employeeid", employee.EmployeeId);
          pt.Add("year", year);
          pt.Add("month", month);
          return sqlmapper.QueryForObject<Model.HrAttendStat>("HrAttendStat.Select_HrAttendStatByEmpidStatDate", pt);

      }
    }
}
