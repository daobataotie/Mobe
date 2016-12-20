//------------------------------------------------------------------------------
//
// file name：IMRSdetailsAccessor.cs
// author: peidun
// create date：2009-12-18 11:12:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.MRSdetails
    /// </summary>
    public partial interface IMRSdetailsAccessor : IAccessor
    {
        IList<Book.Model.MRSdetails> Select(Model.MRSHeader mRSheader);
       // IList<Book.Model.MRSdetails> GetMrsdetailBySourceType(string sourceType);
        IList<Book.Model.MRSdetails> Select(string mpsHeaderId, string sourceType);
        IList<Book.Model.MRSdetails> GetDate(DateTime startDate, DateTime endDate, string sourceType);
    }
}

