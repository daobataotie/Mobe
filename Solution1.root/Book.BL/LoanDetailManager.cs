//------------------------------------------------------------------------------
//
// file name：LoanDetailManager.cs
// author: peidun
// create date：2010-3-15 14:29:52
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using Book.Model;


namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.LoanDetail.
    /// </summary>
    public partial class LoanDetailManager : BaseManager
    {


        public decimal SelectFeeSum(Model.Employee employee, int year, int month)
        {
            return accessor.SelectFeeSum(employee, year, month);
        }

        public IList<Model.LoanDetail> SelectByCondition(string employeeId, DateTime startDate, DateTime endDate)
        {
            return accessor.SelectByCondition(employeeId, startDate, endDate); ;
        }

        /// <summary>
        /// 借出记录查询，返回dataset 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public DataSet SelestLoanList(int year, int month)
        {
            return accessor.SelestLoanList(year, month);

        }

        /// <summary>
        /// 根据员工编号删除
        /// </summary>
        /// <param name="IDNo"></param>
        /// <returns></returns>
        public int DeleteByIDNo(string IDNo)
        {
            return accessor.DeleteByIDNo(IDNo);
        }


        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public DataSet SelestLoanAll()
        {
            return accessor.SelestLoanAll();

        }



        /// <summary>
        /// 修改借出记录
        /// </summary>
        /// <param name="loanDetail"></param>
        /// <returns></returns>
        public bool UpdateLoanDetail(DataSet ds, DateTime SelectDate)
        {
            try
            {
                BL.V.BeginTransaction();
                int result = accessor.UpdateLoanDetail(ds, SelectDate);
                if (result == 0)
                {
                    return false;
                }
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                return false;
                throw ex;
            }
            return true;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            accessor.Delete(id);
        }
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="loandetails"></param>
        public void Insert(Model.LoanDetail loandetails)
        {
            // Validate(loandetails);
            accessor.Insert(loandetails);
        }
        /// <summary>
        /// 更新 
        /// </summary>
        /// <param name="loandetails"></param>
        public void Update(Model.LoanDetail loandetails)
        {
            // Validate(loandetails);
            accessor.Update(loandetails);
        }


    }
}

