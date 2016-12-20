//------------------------------------------------------------------------------
//
// file name：IAtBankSaveUpAccessor.cs
// author: mayanjun
// create date：2010-11-24 09:51:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtBankSaveUp
    /// </summary>
    public partial interface IAtBankSaveUpAccessor : IAccessor
    {
        IList<Book.Model.AtBankSaveUp> Select(DateTime startDate, DateTime endDate, string bankAccountId);
    }
}

