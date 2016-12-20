//------------------------------------------------------------------------------
//
// file name：IBusinessHoursAccessor.cs
// author: peidun
// create date：2009-09-02 上午 10:38:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;


namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BusinessHours
    /// </summary>
    public partial interface IBusinessHoursAccessor : IAccessor
    {
        DataSet SelectNoModel();
        void UpdateDataTable(DataTable accounts);
        bool IsExistsBusinessName(string BusinessHoursId, string BusinessHoursName);

    }
}

