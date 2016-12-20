//------------------------------------------------------------------------------
//
// file name：PCBoxFootCheckDetailManager.cs
// author: mayanjun
// create date：2013-08-16 10:26:11
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCBoxFootCheckDetail.
    /// </summary>
    public partial class PCBoxFootCheckDetailManager
    {

        /// <summary>
        /// Delete PCBoxFootCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCBoxFootCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCBoxFootCheckDetailId);
        }

        /// <summary>
        /// Insert a PCBoxFootCheckDetail.
        /// </summary>
        public void Insert(Model.PCBoxFootCheckDetail pCBoxFootCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCBoxFootCheckDetail);
        }

        /// <summary>
        /// Update a PCBoxFootCheckDetail.
        /// </summary>
        public void Update(Model.PCBoxFootCheckDetail pCBoxFootCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCBoxFootCheckDetail);
        }

        public void DeleteByPCBoxFootCheckId(string id)
        {
            accessor.DeleteByPCBoxFootCheckId(id);
        }
    }
}

