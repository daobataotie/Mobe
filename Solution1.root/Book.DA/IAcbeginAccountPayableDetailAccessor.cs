//------------------------------------------------------------------------------
//
// file name：IAcbeginAccountPayableDetailAccessor.cs
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
    /// Interface of data accessor of dbo.AcbeginAccountPayableDetail
    /// </summary>
    public partial interface IAcbeginAccountPayableDetailAccessor : IAccessor
    {
        IList<Model.AcbeginAccountPayableDetail> Select(Model.AcbeginAccountPayable acbeginAccountPayable);
        IList<Model.AcbeginAccountPayableDetail> SelectDefaultDetails();
        void DeleteByAcbeginAccountPayableId(string id);
    }
}

