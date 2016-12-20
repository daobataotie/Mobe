//------------------------------------------------------------------------------
//
// file name：PCIncomingCheckDetailManager.cs
// author: mayanjun
// create date：2015/11/8 20:10:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCIncomingCheckDetail.
    /// </summary>
    public partial class PCIncomingCheckDetailManager
    {

        /// <summary>
        /// Delete PCIncomingCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCIncomingCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCIncomingCheckDetailId);
        }

        /// <summary>
        /// Insert a PCIncomingCheckDetail.
        /// </summary>
        public void Insert(Model.PCIncomingCheckDetail pCIncomingCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCIncomingCheckDetail);
        }

        /// <summary>
        /// Update a PCIncomingCheckDetail.
        /// </summary>
        public void Update(Model.PCIncomingCheckDetail pCIncomingCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCIncomingCheckDetail);
        }

        public IList<Model.PCIncomingCheckDetail> SelectByCondition(DateTime startdate, DateTime enddate, string lotnumber)
        {
            return accessor.SelectByCondition(startdate, enddate, lotnumber);
        }
    }
}
