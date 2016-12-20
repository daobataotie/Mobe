//------------------------------------------------------------------------------
//
// file name：PCDoubleImpactCheckDetailManager.cs
// author: mayanjun
// create date：2011-11-24 17:38:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCDoubleImpactCheckDetail.
    /// </summary>
    public partial class PCDoubleImpactCheckDetailManager
    {

        /// <summary>
        /// Delete PCDoubleImpactCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCDoubleImpactCheckDetailID)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCDoubleImpactCheckDetailID);
        }

        /// <summary>
        /// Insert a PCDoubleImpactCheckDetail.
        /// </summary>
        public void Insert(Model.PCDoubleImpactCheckDetail pCDoubleImpactCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCDoubleImpactCheckDetail);
        }

        /// <summary>
        /// Update a PCDoubleImpactCheckDetail.
        /// </summary>
        public void Update(Model.PCDoubleImpactCheckDetail pCDoubleImpactCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCDoubleImpactCheckDetail);
        }

        public IList<Model.PCDoubleImpactCheckDetail> SelectByPCDoubleICId(string pCDoubleImpactCheckid)
        {
            return accessor.Select(pCDoubleImpactCheckid);
        }
    }
}

