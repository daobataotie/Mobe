//------------------------------------------------------------------------------
//
// file name：IAtAccountSubjectAccessor.cs
// author: mayanjun
// create date：2010-11-10 11:04:51
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AtAccountSubject
    /// </summary>
    public partial interface IAtAccountSubjectAccessor : IAccessor
    {
        // void UpdateDataTable(Model.AtAccountSubject accountSubject);

        IList<Model.AtAccountSubject> selectById(string startid, string endid);
    }
}

