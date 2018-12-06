//------------------------------------------------------------------------------
//
// file name：IBGHandbookAccessor.cs
// author: mayanjun
// create date：2013-4-16 11:58:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.BGHandbook
    /// </summary>
    public partial interface IBGHandbookAccessor : IAccessor
    {
        IList<Book.Model.BGHandbook> Select(string id);
        DataTable SelectIdGroupById();
        void UpdateIsEffect(string id, string effect);
        bool HasEffect(string bGHandBookId, string id);
        IList<string> SelectAllId();
    }
}

