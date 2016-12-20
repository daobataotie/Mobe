//------------------------------------------------------------------------------
//
// file name：IMRSHeaderAccessor.cs
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
    /// Interface of data accessor of dbo.MRSHeader
    /// </summary>
    public partial interface IMRSHeaderAccessor : IAccessor
    {
        IList<Model.MRSHeader> SelectbySourceType(string type);
    }
}

