//------------------------------------------------------------------------------
//
// file name：TempCardAccessor.cs
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
    /// Data accessor of TempCard
    /// </summary>
    public partial class TempCardAccessor : EntityAccessor, ITempCardAccessor
    {
        public IList<Model.TempCard> SelectbyCardnoDate(DateTime clockdate, string cadno)
        {
            Hashtable pars = new Hashtable();

            pars.Add("DutyDate", clockdate);
            pars.Add("CardNo", cadno);
            return sqlmapper.QueryForList<Model.TempCard>("TempCard.select_byCardnoDate", pars);

        }
        public IList<Book.Model.TempCard> SelectByCardType(string cardNo, string employeeId, DateTime startDate, DateTime endDate)
        {
            Hashtable ta = new Hashtable();
            ta.Add("CardNo", cardNo);
            ta.Add("EmployeeId", employeeId);
            ta.Add("startDate", startDate.Date);
            ta.Add("endDate", endDate.Date.AddDays(1).AddSeconds(-1));
            return sqlmapper.QueryForList<Book.Model.TempCard>("TempCard.select_byCardType", ta);
        }

        /// <summary>
        /// 根据员工查询临时卡
        /// </summary>
        /// <param name="empid"></param>
        /// <returns></returns>
        public IList<Model.TempCard> Selectbyemployee(string empid)
        {
            return sqlmapper.QueryForList<Model.TempCard>("TempCard.select_byempid", empid);
        }



        public IList<Model.TempCard> Selectbydate(DateTime startdate, DateTime enddate)
        {
            Hashtable pars = new Hashtable();

            pars.Add("startdate", startdate.Date);
            pars.Add("enddate", enddate.Date.AddDays(1).AddSeconds(-1));
            return sqlmapper.QueryForList<Model.TempCard>("TempCard.select_byDate", pars);
        }




        public Model.TempCard Selectbyemployeedate(string empid, DateTime startdate, DateTime enddate)
        {
            Hashtable pars = new Hashtable();
            pars.Add("empid", empid);
            pars.Add("startdate", startdate.Date);
            pars.Add("enddate", enddate.Date.AddDays(1).AddSeconds(-1));
            return sqlmapper.QueryForObject<Model.TempCard>("TempCard.select_byDateemnpid", pars);
        }


        /// <summary>
        /// 查询近三个月的临时卡信息
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public IList<Model.TempCard> SelectbyDateTop()
        {
            // Hashtable pars = new Hashtable();

            return sqlmapper.QueryForList<Model.TempCard>("TempCard.select_byDateTopThreeMonth", null);
        }

    }
}
