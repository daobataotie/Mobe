//------------------------------------------------------------------------------
//
// file name：PCEarProtectCheckDetailManager.cs
// author: mayanjun
// create date：2013-09-03 15:16:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCEarProtectCheckDetail.
    /// </summary>
    public partial class PCEarProtectCheckDetailManager
    {

        /// <summary>
        /// Delete PCEarProtectCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCEarProtectCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCEarProtectCheckDetailId);
        }

        /// <summary>
        /// Insert a PCEarProtectCheckDetail.
        /// </summary>
        public void Insert(Model.PCEarProtectCheckDetail pCEarProtectCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCEarProtectCheckDetail);
        }

        /// <summary>
        /// Update a PCEarProtectCheckDetail.
        /// </summary>
        public void Update(Model.PCEarProtectCheckDetail pCEarProtectCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCEarProtectCheckDetail);
        }

        public IList<Model.PCEarProtectCheckDetail> SelectByPCEarProtectCheckId(string PCEarProtectCheckId)
        {
            return accessor.SelectByPCEarProtectCheckId(PCEarProtectCheckId);
        }

        public void DeleteByPCEarProtectCheckId(string PCEarProtectCheckId)
        {
            accessor.DeleteByPCEarProtectCheckId(PCEarProtectCheckId);
        }
    }
}

