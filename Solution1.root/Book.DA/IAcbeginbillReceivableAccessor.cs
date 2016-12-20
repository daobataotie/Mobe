//------------------------------------------------------------------------------
//
// file name：IAcbeginbillReceivableAccessor.cs
// author: mayanjun
// create date：2011-6-9 14:42:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcbeginbillReceivable
    /// </summary>
    public partial interface IAcbeginbillReceivableAccessor : IAccessor
    {
        IList<Model.AcbeginbillReceivable> SelectByDateRange(DateTime startdate, DateTime enddate);
    }
}

