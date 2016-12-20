//------------------------------------------------------------------------------
//
// file name：PCFogCheckDetailManager.cs
// author: mayanjun
// create date：2012-3-17 09:38:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCFogCheckDetail.
    /// </summary>
    public partial class PCFogCheckDetailManager
    {

        /// <summary>
        /// Delete PCFogCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCImpactCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCImpactCheckDetailId);
        }

        public void DeleteByHeaderId(string headerid)
        {
            accessor.DeleteByHeaderId(headerid);
        }

        /// <summary>
        /// Insert a PCFogCheckDetail.
        /// </summary>
        public void Insert(Model.PCFogCheckDetail pCFogCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCFogCheckDetail);
        }

        /// <summary>
        /// Update a PCFogCheckDetail.
        /// </summary>
        public void Update(Model.PCFogCheckDetail pCFogCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCFogCheckDetail);
        }

        public IList<Model.PCFogCheckDetail> Select(string pcfcid)
        {
            return accessor.Select(pcfcid);
        }
    }
}

