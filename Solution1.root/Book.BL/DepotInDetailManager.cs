//------------------------------------------------------------------------------
//
// file name：DepotInDetailManager.cs
// author: mayanjun
// create date：2010-10-25 16:14:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.DepotInDetail.
    /// </summary>
    public partial class DepotInDetailManager
    {

        /// <summary>
        /// Delete DepotInDetail by primary key.
        /// </summary>
        public void Delete(string depotInDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(depotInDetailId);
        }

        /// <summary>
        /// Insert a DepotInDetail.
        /// </summary>
        public void Insert(Model.DepotInDetail depotInDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(depotInDetail);
        }

        /// <summary>
        /// Update a DepotInDetail.
        /// </summary>
        public void Update(Model.DepotInDetail depotInDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(depotInDetail);
        }

        public IList<Model.DepotInDetail> GetDetailByDepotInId(string depotInId)
        {
            return accessor.GetDetailByDepotInId(depotInId);
        }
        public void Delete(Model.DepotIn depotIn)
        {
            accessor.Delete(depotIn);
        }

        public IList<Model.DepotInDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string InDepotIdStart, string InDepotIdEnd, string DepotIdStart, string DepotIdEnd, Model.Supplier SupplierStart, Model.Supplier SupplierEnd)
        {
            return accessor.SelectByCondition(StartDate, EndDate, InDepotIdStart, InDepotIdEnd, DepotIdStart, DepotIdEnd, SupplierStart, SupplierEnd);
        }
    }
}

