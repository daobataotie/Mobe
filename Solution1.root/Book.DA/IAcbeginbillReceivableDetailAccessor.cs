//------------------------------------------------------------------------------
//
// file name：IAcbeginbillReceivableDetailAccessor.cs
// author: mayanjun
// create date：2011-6-9 14:42:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.AcbeginbillReceivableDetail
    /// </summary>
    public partial interface IAcbeginbillReceivableDetailAccessor : IAccessor
    {
        IList<Model.AcbeginbillReceivableDetail> Select(Model.AcbeginbillReceivable acbeginbillReceivable);
        IList<Model.AcbeginbillReceivableDetail> SelectDefaultDetails();
        void DeleteByParentId(string id);
    }
}

