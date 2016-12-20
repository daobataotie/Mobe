//------------------------------------------------------------------------------
//
// file name：BGHandbookDepotOutDetailManager.cs
// author: mayanjun
// create date：2014/3/5 16:32:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookDepotOutDetail.
    /// </summary>
    public partial class BGHandbookDepotOutDetailManager
    {

        /// <summary>
        /// Delete BGHandbookDepotOutDetail by primary key.
        /// </summary>
        public void Delete(string bGHandbookDepotOutDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(bGHandbookDepotOutDetailId);
        }

        /// <summary>
        /// Insert a BGHandbookDepotOutDetail.
        /// </summary>
        public void Insert(Model.BGHandbookDepotOutDetail bGHandbookDepotOutDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(bGHandbookDepotOutDetail);
        }

        /// <summary>
        /// Update a BGHandbookDepotOutDetail.
        /// </summary>
        public void Update(Model.BGHandbookDepotOutDetail bGHandbookDepotOutDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(bGHandbookDepotOutDetail);
        }

        public IList<Model.BGHandbookDepotOutDetail> SelectByBGHandbookDepotOutId(string bGHandbookDepotOutId)
        {
            return accessor.SelectByBGHandbookDepotOutId(bGHandbookDepotOutId);
        }

        public void DeleteByBGHandbookDepotOutId(string bGHandbookDepotOutId)
        {
            accessor.DeleteByBGHandbookDepotOutId(bGHandbookDepotOutId);
        }
    }
}

