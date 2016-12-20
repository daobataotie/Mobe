//------------------------------------------------------------------------------
//
// file name：IPCMaterialCheckDetailAccessor.cs
// author: mayanjun
// create date：2015/10/24 17:47:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PCMaterialCheckDetail
    /// </summary>
    public partial interface IPCMaterialCheckDetailAccessor : IAccessor
    {
        IList<Model.PCMaterialCheckDetail> SelectByPCMaterialCheckId(string id);

        void DeleteByPCMaterialCheckId(string id);

        IList<Model.PCMaterialCheckDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string StartPId, string EndPId, string InvoiceCusId);
    }
}
