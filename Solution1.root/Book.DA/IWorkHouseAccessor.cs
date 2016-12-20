//------------------------------------------------------------------------------
//
// file name：IWorkHouseAccessor.cs
// author: peidun
// create date：2009-11-18 15:33:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.WorkHouse
    /// </summary>
    public partial interface IWorkHouseAccessor : IAccessor
    {
        bool ExistsWorkHouseCode(string WorkHouseCode);
    }
}

