//------------------------------------------------------------------------------
//
// file name：FlextimeAccessor.cs
// author: peidun
// create date：2010-2-6 10:33:09
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
    /// Data accessor of Flextime
    /// </summary>
    public partial class FlextimeAccessor : EntityAccessor, IFlextimeAccessor
    {
        public void DeleteFilextime(string employeeId)
        {
            sqlmapper.Delete("Flextime.delete2", employeeId);
        }


        public Model.Flextime getbyempiddate(string empid, DateTime flexdate)
        {
            Hashtable parms=new Hashtable();
            parms.Add("empid",empid);
            parms.Add("flexdate",flexdate);
            return sqlmapper.QueryForObject<Model.Flextime>("Flextime.select_byempiddate", parms);
        }

        public bool selectbyempiddate(string empid, DateTime flexdate)
        {
            Hashtable parms = new Hashtable();
            parms.Add("empid", empid);
            parms.Add("flexdate", flexdate);
            return sqlmapper.QueryForObject<bool>("Flextime.get_byempiddate", parms);
        }

        public IList<Model.Flextime> getByempid(DateTime flexdate)
        {
            return sqlmapper.QueryForList<Model.Flextime>("Flextime.select_bydate", flexdate);
        }


        //根据员工编号查询
        public IList<Model.Flextime> getByempid(string employeeId)
        {
            Hashtable parms = new Hashtable();
            parms.Add("employeeid", employeeId);
            return sqlmapper.QueryForList<Model.Flextime>("Flextime.selectall_ByEmpid",parms);
        }

        //根据员工编号、日期删除
        public  void DeleteByEmpidDate(string empid,DateTime date)
        {
            Hashtable parm = new Hashtable();
            parm.Add("EmployeeId", empid);
            parm.Add("FlexDate", date);
            sqlmapper.Delete("Flextime.delete_ByEmpiddate", parm); 
        }
    }
}
