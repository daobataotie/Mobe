//------------------------------------------------------------------------------
//
// file name：FlextimeManager.cs
// author: peidun
// create date：2010-2-6 10:33:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo. Flextime.
    /// </summary>
    public partial class
        FlextimeManager
    {

        /// <summary>
        /// Delete Flextime by primary key.
        /// </summary>
        public void Delete(string flextimeId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(flextimeId);
        }
        public void DeleteFilextime(string employeeId)
        {
            accessor.DeleteFilextime(employeeId);
        }
        /// <summary>
        /// Insert a Flextime.
        /// </summary>
        public void Insert(Model.Flextime flextime)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(flextime);
        }

        /// <summary>
        /// Update a Flextime.
        /// </summary>
        public void Update(Model.Flextime flextime)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(flextime);
        }
        /// <summary>
        /// 查询员工某日的排班记录
        /// </summary>
        /// <param name="empid"></param>
        /// <param name="flexdate"></param>
        /// <returns></returns>
        public Model.Flextime getbyempiddate(string empid, DateTime flexdate)
        {
            return accessor.getbyempiddate(empid, flexdate);
        }

        public bool selectbyempiddate(string empid, DateTime flexdate)
        {
            return accessor.selectbyempiddate(empid, flexdate);
        }

        public IList<Model.Flextime> getByempid(DateTime flexdate)
        {
            return accessor.getByempid(flexdate);
        }


        //根据员工编号查询
        public IList<Model.Flextime> getByempid(string employeeId)
        {
            return accessor.getByempid(employeeId);
        }


        //根据员工编号和日期删除
        public void DeleteByEmpidDate(string empid, DateTime date)
        {
            accessor.DeleteByEmpidDate(empid, date);
        }
    }
}

