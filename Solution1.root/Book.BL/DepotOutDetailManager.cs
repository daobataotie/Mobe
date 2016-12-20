//------------------------------------------------------------------------------
//
// file name：DepotOutDetailManager.cs
// author: mayanjun
// create date：2010-10-15 15:41:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.DepotOutDetail.
    /// </summary>
    public partial class DepotOutDetailManager
    {

        /// <summary>
        /// Delete DepotOutDetail by primary key.
        /// </summary>
        public void Delete(string depotOutDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(depotOutDetailId);
        }

        /// <summary>
        /// Insert a DepotOutDetail.
        /// </summary>
        public void Insert(Model.DepotOutDetail depotOutDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(depotOutDetail);
        }

        /// <summary>
        /// Update a DepotOutDetail.
        /// </summary>
        public void Update(Model.DepotOutDetail depotOutDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(depotOutDetail);
        }

        public IList<Model.DepotOutDetail> GetDepotOutDetailByDepotOutId(string depotOutId)
        {
            return accessor.GetDepotOutDetailByDepotOutId(depotOutId);
        }
        public void Delete(Model.DepotOut depotOut)
        {
            accessor.Delete(depotOut.DepotOutId);
        }

        public IList<Model.DepotOutDetail> SelectByCondition(DateTime startDate, DateTime endDate, string DepotOutIdStart, string DepotOutIdEnd, string depotStart, string depotEnd)
        {
            return accessor.SelectByCondition(startDate, endDate, DepotOutIdStart, DepotOutIdEnd, depotStart, depotEnd);
        }

        public DataTable SelectOutAndInDepot(DateTime startDate, DateTime endDate, string depotStart, string depotEnd, string productNameStart, string productNameEnd, string productCategoryStart, string productCategoryEnd, string ProductIdStart, string ProductIdEnd)
        {
            return accessor.SelectOutAndInDepot(startDate, endDate, depotStart, depotEnd, productNameStart, productNameEnd, productCategoryStart, productCategoryEnd, ProductIdStart, ProductIdEnd);
        }

        public IList<Model.DepotOutDetail> SelectByDepotOutId(string id)
        {
            return accessor.SelectByDepotOutId(id);
        }

        public IList<Model.DepotOutDetail> SelectByDateRange(DateTime startDate, DateTime endDate, string productid)
        {
            return accessor.SelectByDateRange(startDate, endDate, productid);
        }
    }
}

