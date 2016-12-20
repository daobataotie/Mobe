//------------------------------------------------------------------------------
//
// file name：IaccepterattribAccessor.cs
// author: peidun
// create date：2009-11-18 15:33:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.accepterattrib
    /// </summary>
    public partial interface IaccepterattribAccessor : IAccessor
    {
        void DeleteByProcessId(string processid);
        IList<Model.accepterattrib> SelectByProcessId(string processid);
    }
}

