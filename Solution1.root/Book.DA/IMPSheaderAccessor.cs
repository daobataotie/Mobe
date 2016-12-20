//------------------------------------------------------------------------------
//
// file name：IMPSheaderAccessor.cs
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
    /// Interface of data accessor of dbo.MPSheader
    /// </summary>
    public partial interface IMPSheaderAccessor : IAccessor
    {
        IList<Book.Model.MPSheader> SelectById(string mPSheaderId);
        IList<Book.Model.MPSheader> Select(DateTime start, DateTime end);
        
    }
}

