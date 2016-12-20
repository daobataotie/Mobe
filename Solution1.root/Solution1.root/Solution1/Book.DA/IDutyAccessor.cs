//------------------------------------------------------------------------------
//
// file name：IDutyAccessor.cs
// author: peidun
// create date：2008-11-24 11:10:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.Duty
    /// </summary>
    public partial interface IDutyAccessor : IAccessor
    {
        DataSet SelectNoModel();
        void UpdateDataTable(DataTable accounts);
        bool IsExistsName(string dutyid, string dutyname);
        bool ExistsPrimary(string id);
    }
}

