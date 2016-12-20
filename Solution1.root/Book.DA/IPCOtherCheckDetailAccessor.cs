//------------------------------------------------------------------------------
//
// file name：IPCOtherCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-10 15:05:56
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCOtherCheckDetail
    /// </summary>
    public partial interface IPCOtherCheckDetailAccessor : IAccessor
    {
        IList<Model.PCOtherCheckDetail> Selct(Model.PCOtherCheck _Pcoc);
        void DeleteByPCOCId(string pcocId);
        IList<Model.PCOtherCheckDetail> SelectByConditon(DateTime StartDate, DateTime EndDate, Book.Model.Product product, string CusXOId);
    }
}

