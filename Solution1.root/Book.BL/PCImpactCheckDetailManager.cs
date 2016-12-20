//------------------------------------------------------------------------------
//
// file name：PCImpactCheckDetailManager.cs
// author: mayanjun
// create date：2011-11-15 14:09:34
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCImpactCheckDetail.
    /// </summary>
    public partial class PCImpactCheckDetailManager
    {

        /// <summary>
        /// Delete PCImpactCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCImpactCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCImpactCheckDetailId);
        }

        /// <summary>
        /// Insert a PCImpactCheckDetail.
        /// </summary>
        public void Insert(Model.PCImpactCheckDetail pCImpactCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCImpactCheckDetail);
        }

        /// <summary>
        /// Update a PCImpactCheckDetail.
        /// </summary>
        public void Update(Model.PCImpactCheckDetail pCImpactCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCImpactCheckDetail);
        }

        public IList<Model.PCImpactCheckDetail> SelectByPCICId(string pcicId)
        {
            return accessor.Select(pcicId);
        }
    }
}

