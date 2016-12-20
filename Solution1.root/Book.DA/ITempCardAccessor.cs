//------------------------------------------------------------------------------
//
// file name：ITempCardAccessor.cs
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
    /// Interface of data accessor of dbo.TempCard
    /// </summary>
    public partial interface ITempCardAccessor : IAccessor
    {

        IList<Model.TempCard> SelectbyCardnoDate(DateTime clockdate, string cadno);
        IList<Model.TempCard> Selectbyemployee(string empid);

        IList<Model.TempCard> Selectbydate(DateTime startdate, DateTime enddate);
        Model.TempCard Selectbyemployeedate(string empid, DateTime startdate, DateTime enddate);
        IList<Book.Model.TempCard> SelectByCardType(string cardNo, string employeeId, DateTime startDate, DateTime endDate);
        //查询近三个月的临时卡信息
        IList<Model.TempCard> SelectbyDateTop();


    }
}

