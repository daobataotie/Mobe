//------------------------------------------------------------------------------
//
// file name：ILeaveTypeAccessor.cs
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
    /// Interface of data accessor of dbo.LeaveType
    /// </summary>
    public partial interface ILeaveTypeAccessor : IAccessor
    {
        System.Data.DataSet SelectLeaveTypeInfo();
        void SaveLeaveTypeInfo(System.Data.DataTable table);
        bool IsExitsName(string typeid, string typename);
    }
}

