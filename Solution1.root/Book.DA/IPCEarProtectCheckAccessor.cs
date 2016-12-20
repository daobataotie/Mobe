//------------------------------------------------------------------------------
//
// file name：IPCEarProtectCheckAccessor.cs
// author: mayanjun
// create date：2013-09-03 15:16:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCEarProtectCheck
    /// </summary>
    public partial interface IPCEarProtectCheckAccessor : IAccessor
    {
        IList<Model.PCEarProtectCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate,bool IsReport);

        bool mHasRows(bool IsReport);
        bool mHasRowsBefore(Model.PCEarProtectCheck e);
        bool mHasRowsAfter(Model.PCEarProtectCheck e);
        Model.PCEarProtectCheck mGetFirst(bool IsReport);
        Model.PCEarProtectCheck mGetLast(bool IsReport);
        Model.PCEarProtectCheck mGetNext(Model.PCEarProtectCheck e);
        Model.PCEarProtectCheck mGetPrev(Model.PCEarProtectCheck e);
    }
}

