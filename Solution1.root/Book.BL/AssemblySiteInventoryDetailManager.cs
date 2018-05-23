//------------------------------------------------------------------------------
//
// file name：AssemblySiteInventoryDetailManager.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AssemblySiteInventoryDetail.
    /// </summary>
    public partial class AssemblySiteInventoryDetailManager
    {

        /// <summary>
        /// Delete AssemblySiteInventoryDetail by primary key.
        /// </summary>
        public void Delete(string assemblySiteInventoryDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(assemblySiteInventoryDetailId);
        }

        /// <summary>
        /// Insert a AssemblySiteInventoryDetail.
        /// </summary>
        public void Insert(Model.AssemblySiteInventoryDetail assemblySiteInventoryDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(assemblySiteInventoryDetail);
        }

        /// <summary>
        /// Update a AssemblySiteInventoryDetail.
        /// </summary>
        public void Update(Model.AssemblySiteInventoryDetail assemblySiteInventoryDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(assemblySiteInventoryDetail);
        }

        public IList<Model.AssemblySiteInventoryDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productid, bool state)
        {
            return accessor.SelectByDateRage(startDate, endDate, productid, state);
        }
    }
}
