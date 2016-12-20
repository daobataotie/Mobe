//------------------------------------------------------------------------------
//
// file name：IPronotedetailsAccessor.cs
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
    /// Interface of data accessor of dbo.Pronotedetails
    /// </summary>
    public partial interface IPronotedetailsAccessor : IAccessor
    {
        IList<Book.Model.Pronotedetails> Select(Model.PronoteHeader pronoteHeader);
        double GetByMPSdetail(string mPSDetailId);
        IList<Book.Model.Pronotedetails> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd);
    }
}

