//------------------------------------------------------------------------------
//
// file name：ILoanDetailAccessor.cs
// author: peidun
// create date：2010-3-15 14:29:52
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Book.Model;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.LoanDetail
    /// </summary>
    public partial interface ILoanDetailAccessor : IAccessor
    {
        decimal SelectFeeSum(Model.Employee employee, int year, int month);

        IList<Model.LoanDetail> SelectByCondition(string employeeId, DateTime startDate, DateTime endDate);

        //查询借出记录接口
        DataSet SelestLoanList(int year, int month);


        //修改借出记录
        int UpdateLoanDetail(DataSet ds,DateTime SelectDate);

        //加载查询
        DataSet SelestLoanAll();

        //删除
        int DeleteByIDNo(string IDNo);
    }
    
}

