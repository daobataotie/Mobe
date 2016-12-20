//------------------------------------------------------------------------------
//
// file name：PCClarityCheckDetailManager.cs
// author: mayanjun
// create date：2013-08-19 15:44:12
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCClarityCheckDetail.
    /// </summary>
    public partial class PCClarityCheckDetailManager
    {

        /// <summary>
        /// Delete PCClarityCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCClarityCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCClarityCheckDetailId);
        }

        /// <summary>
        /// Insert a PCClarityCheckDetail.
        /// </summary>
        public void Insert(Model.PCClarityCheckDetail pCClarityCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCClarityCheckDetail);
        }

        /// <summary>
        /// Update a PCClarityCheckDetail.
        /// </summary>
        public void Update(Model.PCClarityCheckDetail pCClarityCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCClarityCheckDetail);
        }

        public IList<Book.Model.PCClarityCheckDetail> SelectByPCClarityCheckId(string PCClarityCheckId)
        {
            return accessor.SelectByPCClarityCheckId(PCClarityCheckId);
        }

        public void DeleteByPCClarityCheckID(string PCCalrityCheckId)
        {
            accessor.DeleteByPCClarityCheckID(PCCalrityCheckId);
        }
    }
}

