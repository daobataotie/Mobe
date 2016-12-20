//------------------------------------------------------------------------------
//
// file name：LunchDetailManager.cs
// author: peidun
// create date：2010-3-26 11:08:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.LunchDetail.
    /// </summary>
    public partial class LunchDetailManager
    {

        /// <summary>
        /// Delete LunchDetail by primary key.
        /// </summary>
        public void Delete(string lunchDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(lunchDetailId);
        }

        /// <summary>
        /// Insert a LunchDetail.
        /// </summary>
        public void Insert(Model.LunchDetail lunchDetail)
        {
            //
            // todo:add other logic here
            //
            lunchDetail.InsertTime = DateTime.Now;
            Validate(lunchDetail);
            accessor.Insert(lunchDetail);
        }

        /// <summary>
        /// Update a LunchDetail.
        /// </summary>
        public void Update(Model.LunchDetail lunchDetail)
        {
            //
            // todo: add other logic here.
            //
            lunchDetail.UpdateTime = DateTime.Now;
            Validate(lunchDetail);
            accessor.Update(lunchDetail);
        }

        public IList<Model.LunchDetail> GetLunchDetailbyempiddate(string empid, DateTime starttime, DateTime endtime)
        {
            return accessor.GetLunchDetailbyempiddate(empid, starttime, endtime);
        }
        private void Validate(Model.LunchDetail lunchDetail)
        {
            if (string.IsNullOrEmpty(lunchDetail.EmployeeId))
            {
                throw new Helper.RequireValueException(Model.LunchDetail.PROPERTY_EMPLOYEEID);
            }
        }

        public decimal SelectFeeSum(Model.Employee employee, int year, int month)
        {
            return accessor.SelectFeeSum(employee, year, month);
        }
        //获取时间范围内的员工信息
        public DataSet GetEmployeeInfo(DateTime date)
        {
            return accessor.GetEmployeeInfo(date);
        }



        public void UpdateLunchDetail(DataSet dataset, DateTime date)
        {
            try
            {
                BL.V.BeginTransaction();
                accessor.UpdateLunchDetail(dataset, date);
                BL.V.CommitTransaction();
            }
            catch (Exception ex)
            {
                BL.V.RollbackTransaction();
                throw ex;
            }
        }


        //根据员工和日期查询
        public IList<Model.LunchDetail> selectByempAndDate(string EmpID, int year, int month)
        {
            return accessor.selectByempAndDate(EmpID, year, month);
        }
    }
}

