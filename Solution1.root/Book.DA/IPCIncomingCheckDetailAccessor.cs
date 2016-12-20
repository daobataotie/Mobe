//------------------------------------------------------------------------------
//
// file name：IPCIncomingCheckDetailAccessor.cs
// author: mayanjun
// create date：2015/11/8 20:10:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCIncomingCheckDetail
    /// </summary>
    public partial interface IPCIncomingCheckDetailAccessor : IAccessor
    {
        IList<Model.PCIncomingCheckDetail> SelectByPrimaryId(string id);
        void DeleteByPrimaryId(string id);
        IList<Model.PCIncomingCheckDetail> SelectByCondition(DateTime startdate, DateTime enddate, string lotnumber);
    }
}
