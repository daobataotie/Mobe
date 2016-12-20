//------------------------------------------------------------------------------
//
// file name：IPCEarPressCheckAccessor.cs
// author: mayanjun
// create date：2013-08-23 16:50:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCEarPressCheck
    /// </summary>
    public partial interface IPCEarPressCheckAccessor : IAccessor
    {
        IList<Model.PCEarPressCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, bool IsReport);
        bool mHasRows(bool IsReport);
        bool mHasRowsBefore(Model.PCEarPressCheck e);
        bool mHasRowsAfter(Model.PCEarPressCheck e);
        Model.PCEarPressCheck mGetFirst(bool IsReport);
        Model.PCEarPressCheck mGetLast(bool IsReport);
        Model.PCEarPressCheck mGetNext(Model.PCEarPressCheck e);
        Model.PCEarPressCheck mGetPrev(Model.PCEarPressCheck e);
    }
}

