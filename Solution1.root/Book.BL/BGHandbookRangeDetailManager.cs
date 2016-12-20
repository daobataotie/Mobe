//------------------------------------------------------------------------------
//
// file name：BGHandbookRangeDetailManager.cs
// author: mayanjun
// create date：2013-4-17 15:13:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookRangeDetail.
    /// </summary>
    public partial class BGHandbookRangeDetailManager
    {

        /// <summary>
        /// Delete BGHandbookRangeDetail by primary key.
        /// </summary>
        public void Delete(string bGHandbookRangeDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(bGHandbookRangeDetailId);
        }

        /// <summary>
        /// Insert a BGHandbookRangeDetail.
        /// </summary>
        public void Insert(Model.BGHandbookRangeDetail bGHandbookRangeDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(bGHandbookRangeDetail);
        }

        /// <summary>
        /// Update a BGHandbookRangeDetail.
        /// </summary>
        public void Update(Model.BGHandbookRangeDetail bGHandbookRangeDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(bGHandbookRangeDetail);
        }

        public IList<Model.BGHandbookRangeDetail> SelectByBGHandbookId(string Id, string Type)
        {
            return accessor.SelectByBGHandbookId(Id, Type);
        }

        public void DeleteByBGHandbookId(string Id)
        {
            accessor.DeleteByBGHandbookId(Id);
        }
    }
}

