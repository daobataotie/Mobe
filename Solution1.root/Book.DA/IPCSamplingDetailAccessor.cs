//------------------------------------------------------------------------------
//
// file name：IPCSamplingDetailAccessor.cs
// author: mayanjun
// create date：2015/10/30 17:07:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCSamplingDetail
    /// </summary>
    public partial interface IPCSamplingDetailAccessor : IAccessor
    {
        IList<Model.PCSamplingDetail> SelectByPCMaterialCheckId(string id);

        void DeleteByPCMaterialCheckId(string id);

        IList<Model.PCSamplingDetail> SelectByCondition(DateTime startDate, DateTime endDate, string startPId, string endPId, string invoiceCusId);
    }
}
