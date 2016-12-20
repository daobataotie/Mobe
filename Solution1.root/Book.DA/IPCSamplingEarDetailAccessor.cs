//------------------------------------------------------------------------------
//
// file name：IPCSamplingEarDetailAccessor.cs
// author: mayanjun
// create date：2015/10/31 16:25:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCSamplingEarDetail
    /// </summary>
    public partial interface IPCSamplingEarDetailAccessor : IAccessor
    {
        IList<Model.PCSamplingEarDetail> SelectByPCMaterialCheckId(string id);

        void DeleteByPCMaterialCheckId(string id);

        IList<Model.PCSamplingEarDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string StartPId, string EndPId, string InvoiceCusId);
    }
}
