//------------------------------------------------------------------------------
//
// file name：IPronoteProceduresDetailAccessor.cs
// author: mayanjun
// create date：2010-9-16 15:57:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronoteProceduresDetail
    /// </summary>
    public partial interface IPronoteProceduresDetailAccessor : IAccessor
    {
        IList<Model.PronoteProceduresDetail> GetPronotedetailsMaterialByHeaderId(Model.PronoteHeader pro);
        IList<Model.PronoteProceduresDetail> SelectByProceduresId(string proceduresId);
        IList<Model.PronoteProceduresDetail> SelectByDateRange(DateTime startdate, DateTime enddate);
    }
}

