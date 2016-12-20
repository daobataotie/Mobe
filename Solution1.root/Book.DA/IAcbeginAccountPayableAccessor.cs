//------------------------------------------------------------------------------
//
// file name：IAcbeginAccountPayableAccessor.cs
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
    /// Interface of data accessor of dbo.AcbeginAccountPayable
    /// </summary>
    public partial interface IAcbeginAccountPayableAccessor : IAccessor
    {
        IList<Model.AcbeginAccountPayable> SelectByDateRange(DateTime startdate, DateTime enddate);
    }
}

