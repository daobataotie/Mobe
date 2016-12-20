//------------------------------------------------------------------------------
//
// file name：IPronotedetailsMaterialAccessor.cs
// author: mayanjun
// create date：2010-9-15 10:11:07
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.PronotedetailsMaterial
    /// </summary>
    public partial interface IPronotedetailsMaterialAccessor : IAccessor
    {
        IList<Model.PronotedetailsMaterial> GetByHeader(Model.PronoteHeader header);
        Model.PronotedetailsMaterial GetByHeadIdAndDetailId(string pronteheadid, string pronotedetailid);
        IList<Model.PronotedetailsMaterial> selectByHeaderIdAndPid(string PronoteHeaderID, string StartpId, string EndpId);
        void DeleteByHeaderId(string id);
    }
}

