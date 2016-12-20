//------------------------------------------------------------------------------
//
// file name：IAtProfitLossAccessor.cs
// author: mayanjun
// create date：2011-2-25 10:53:28
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtProfitLoss
    /// </summary>
    public partial interface IAtProfitLossAccessor : IAccessor
    {
        IList<Book.Model.AtProfitLoss> Select(DateTime startDate, DateTime endDate);
    }
}

