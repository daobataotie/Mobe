//------------------------------------------------------------------------------
//
// file name：BGProductDepotOutDetailManager.cs
// author: mayanjun
// create date：2014/3/25 18:18:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGProductDepotOutDetail.
    /// </summary>
    public partial class BGProductDepotOutDetailManager
    {

        /// <summary>
        /// Delete BGProductDepotOutDetail by primary key.
        /// </summary>
        public void Delete(string bGProductDepotOutDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(bGProductDepotOutDetailId);
        }

        /// <summary>
        /// Insert a BGProductDepotOutDetail.
        /// </summary>
        public void Insert(Model.BGProductDepotOutDetail bGProductDepotOutDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(bGProductDepotOutDetail);
        }

        /// <summary>
        /// Update a BGProductDepotOutDetail.
        /// </summary>
        public void Update(Model.BGProductDepotOutDetail bGProductDepotOutDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(bGProductDepotOutDetail);
        }

        public IList<Book.Model.BGProductDepotOutDetail> SelectByBGProductDepotOutId(string bGProductDepotOutId)
        {
            return accessor.SelectByBGProductDepotOutId(bGProductDepotOutId);
        }
        public double SumQuantityByHandbook(string bGHandbookId, string bGHandbookProductId)
        {
            return accessor.SumQuantityByHandbook(bGHandbookId, bGHandbookProductId);
        }
    }
}

