//------------------------------------------------------------------------------
//
// file name：PronotedetailsMaterialManager.cs
// author: mayanjun
// create date：2010-9-15 10:11:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PronotedetailsMaterial.
    /// </summary>
    public partial class PronotedetailsMaterialManager
    {

        /// <summary>
        /// Delete PronotedetailsMaterial by primary key.
        /// </summary>
        public void Delete(string pronotedetailsMaterialId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pronotedetailsMaterialId);
        }

        /// <summary>
        /// Insert a PronotedetailsMaterial.
        /// </summary>
        public void Insert(Model.PronotedetailsMaterial pronotedetailsMaterial)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pronotedetailsMaterial);
        }

        /// <summary>
        /// Update a PronotedetailsMaterial.
        /// </summary>
        public void Update(Model.PronotedetailsMaterial pronotedetailsMaterial)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pronotedetailsMaterial);
        }

        public IList<Model.PronotedetailsMaterial> GetByHeader(Model.PronoteHeader header)
        {
            return accessor.GetByHeader(header);
        }

        public Model.PronotedetailsMaterial GetByHeadIdAndDetailId(string pronteheadid, string pronotedetailid)
        {
            return accessor.GetByHeadIdAndDetailId(pronteheadid, pronotedetailid);
        }

        public IList<Model.PronotedetailsMaterial> selectByHeaderIdAndPid(string PronoteHeaderID, string StartpId, string EndpId)
        {
            return accessor.selectByHeaderIdAndPid(PronoteHeaderID, StartpId, EndpId);
        }
    }
}

