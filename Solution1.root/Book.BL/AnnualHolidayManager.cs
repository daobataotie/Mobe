//------------------------------------------------------------------------------
//
// file name：AnnualHolidayManager.cs
// author: peidun
// create date：2010-2-6 10:33:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AnnualHoliday.
    /// </summary>
    public partial class AnnualHolidayManager
    {

        /// <summary>
        /// Delete AnnualHoliday by primary key.
        /// </summary>
        public void Delete(string annualHolidayId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(annualHolidayId);
        }

        /// <summary>
        /// Insert a AnnualHoliday.
        /// </summary>
        public void Insert(Model.AnnualHoliday annualHoliday)
        {
            //
            // todo:add other logic here
            //
            Validate(annualHoliday);
            accessor.Insert(annualHoliday);
        }
        /// <summary>
        /// Update a AnnualHoliday.
        /// </summary>
        public void Update(Model.AnnualHoliday annualHoliday)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(annualHoliday);
        }
        public void Validate(Model.AnnualHoliday annualHoliday)
        {
            if (string.IsNullOrEmpty(annualHoliday.HolidayName))
            {
                throw new Helper.RequireValueException(Model.AnnualHoliday.PRO_HolidayName);
            }
        }
        public System.Data.DataSet SelectAllAnnualInfo()
        {
            return accessor.SelectAllAnnualInfo();
        }
        public void SaveAnnualInfo(System.Data.DataTable table, string years)
        {
            accessor.SaveAnnualInfo(table, years);
        }
        public System.Data.DataSet SelectSingleAnnualInfo(DateTime HolidayDate)
        {
            return accessor.SelectSingleAnnualInfo(HolidayDate);
        }
        public System.Data.DataSet SelectAnnualInfoByDueDate(DateTime dueDate)
        {
            return accessor.SelectAnnualInfoByDueDate(dueDate);
        }
        //月份例假数
        public int SelectCountByMonth(int year, int month)
        {
            return accessor.SelectCountByMonth(year, month);
        }
        public System.Data.DataSet SelectAnnualInfoByYear(int year)
        {
            return accessor.SelectAnnualInfoByYear(year);
        }
        public IList<Model.AnnualHoliday> SelectAnnualInfoByYear_list(int year)
        {
            return accessor.SelectAnnualInfoByYear_list(year);
        }
        /// <summary>
        /// 该年是否已排假
        /// </summary>
        /// <param name="years"></param>
        /// <returns></returns>
        public bool ExistsAutoYear(string years)
        {
            return accessor.ExistsAutoYear(years);
        }

        public bool IsExistForHolidayDate(System.Data.DataRow dr)
        {
            return accessor.IsExistForHolidayDate(dr);
        }

        /// <summary>
        /// 取得某一时间之后所有假期
        /// </summary>
        /// <param name="limitdate">最小时间</param>
        /// <returns></returns>
        public IList<Model.AnnualHoliday> SelectBigThanDate(DateTime limitdate)
        {
            return accessor.SelectBigThanDate(limitdate);
        }
    }
}

