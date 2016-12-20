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

        Book.Model.Employee GetbyIdNo(string idno);

        IList<Book.Model.Employee> Select(string _roleId);
        IList<Book.Model.Employee> Select(Model.Department department);
        Book.Model.Employee SelectByCardNo(string CardNo,DateTime dt);
        IList<Model.Employee> SelectbyIDsubstring(string charno);
        IList<Model.Employee> SelectbyPinYin(string pinyin);
        DataTable SelectPinyin();
        DataSet  SelectOnActiveDataSet();
        void UpdateDataDataSet(DataSet dataSet);
        DataSet SelectOnActiveDataSetByEmployeeId(string employeeId);
        IList<Model.Employee> selectLeaverPayActive();
        IList<Model.Employee> selectEmployeeSearch(DateTime rzbegin, DateTime rzend, DateTime lzbegin, DateTime lzend, string type,int flag);



    }
}

