//------------------------------------------------------------------------------
//
// file name：IPronoteHeaderAccessor.cs
// author: peidun
// create date：2009-12-29 11:58:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronoteHeader
    /// </summary>
    public partial interface IPronoteHeaderAccessor : IAccessor
    {
        IList<Book.Model.PronoteHeader> GetByDate(DateTime startDate, DateTime endDate);
        IList<Book.Model.PronoteHeader> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd);
    }
}

