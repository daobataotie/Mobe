//------------------------------------------------------------------------------
//
// file name：IFlextimeAccessor.cs
// author: peidun
// create date：2010-2-6 10:33:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Flextime
    /// </summary>
    public partial interface IFlextimeAccessor : IAccessor
    {
        void DeleteFilextime(string employeeId);

        Model.Flextime getbyempiddate(string empid, DateTime flexdate);

        IList<Model.Flextime> getByempid(DateTime flexdate);
        bool selectbyempiddate(string empid, DateTime flexdate);

        //根据员工编号查询
        IList<Model.Flextime> getByempid(string employeeId);

        void DeleteByEmpidDate(string empid, DateTime date);

        IList<Model.Flextime> selectByEmpidsAndDates(string empids, string dates);
    }
}

