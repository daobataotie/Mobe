//------------------------------------------------------------------------------
//
// file name：IAtPropertyDebtAccessor.cs
// author: mayanjun
// create date：2011-2-28 15:30:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtPropertyDebt
    /// </summary>
    public partial interface IAtPropertyDebtAccessor : IAccessor
    {
        IList<Book.Model.AtPropertyDebt> Select(DateTime startDate, DateTime endDate);
    }
}

