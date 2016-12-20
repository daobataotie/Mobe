//------------------------------------------------------------------------------
//
// file name：IEmployeeAccessor.cs
// author: peidun
// create date：2008/6/6 10:00:47
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Employee
    /// </summary>
    public partial interface IEmployeeAccessor : IEntityAccessor
    {
        IList<Model.Employee> SelectOperators();

        IList<Model.Employee> SelectOnActive();

        IList<Model.Employee> SelectLeaveJob();

        Book.Model.Employee GetByOperatorName(string name);

        Book.Model.Employee GetbyIdNo(string idno);

        IList<Book.Model.Employee> Select(string _roleId);
        IList<Book.Model.Employee> Select(Model.Department department);
        Book.Model.Employee SelectByCardNo(string CardNo, DateTime dt);
        IList<Model.Employee> SelectbyIDsubstring(string charno);
        IList<Model.Employee> SelectbyPinYin(string pinyin);
        DataTable SelectPinyin();
        DataSet SelectOnActiveDataSet();
        void UpdateDataDataSet(DataSet dataSet);
        DataSet SelectOnActiveDataSetByEmployeeId(string employeeId);
        IList<Model.Employee> selectLeaverPayActive();
        IList<Model.Employee> selectEmployeeSearch(DateTime rzbegin, DateTime rzend, DateTime lzbegin, DateTime lzend, string type, int flag);

        /// <summary>
        /// 查找员工编号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Model.Employee GetOldIDbyEmpID(string id);

        /// <summary>
        /// 修改时判断是否存在员工编号
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        bool ExistsExceptUpdate(Model.Employee emp);

        /// <summary>
        /// 添加时判断是否存在员工编号
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        bool ExistsExceptInsert(Model.Employee emp);

        IList<Model.Employee> SelectHrDailyAttend(DateTime date);
        //更新出勤记录查询员工列表
        IList<Model.Employee> DailyEmployeeAttendInfo_EmpList(DateTime CheckDatetime);

        IList<Model.Employee> GetHasThereEmp_ListByDateTime(DateTime mdate);

        Model.Employee mGetFirst();

        Model.Employee mGetLast();

        Model.Employee mGetPrev(Model.Employee emp);

        Model.Employee mGetNext(Model.Employee emp);

        bool mHasRows();

        bool mHasRowsBefore(Model.Employee emp);

        bool mHasRowsAfter(Model.Employee emp);
        IList<Model.Employee> SelectIdAndName();
    }
}

