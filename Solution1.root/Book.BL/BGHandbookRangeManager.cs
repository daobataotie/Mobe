//------------------------------------------------------------------------------
//
// file name：BGHandbookRangeManager.cs
// author: mayanjun
// create date：2013-4-17 15:13:03
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookRange.
    /// </summary>
    public partial class BGHandbookRangeManager : BaseManager
    {

        /// <summary>
        /// Delete BGHandbookRange by primary key.
        /// </summary>
        public void Delete(string bGHandbookRangeId)
        {
            //
            // todo:add other logic here
            //
            try
            {
                BL.V.BeginTransaction();
                new BL.BGHandbookRangeDetailManager().DeleteByBGHandbookId(bGHandbookRangeId);
                accessor.Delete(bGHandbookRangeId);
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public void Delete(Model.BGHandbookRange model)
        {
            this.Delete(model.BGHandbookRangeId);
        }

        /// <summary>
        /// Insert a BGHandbookRange.
        /// </summary>
        public void Insert(Model.BGHandbookRange bGHandbookRange)
        {
            //try
            //{
            //    BL.V.BeginTransaction();
            bGHandbookRange.InsertTime = DateTime.Now;
            bGHandbookRange.UpdateTime = DateTime.Now;
            accessor.Insert(bGHandbookRange);
            foreach (Model.BGHandbookRangeDetail detail in bGHandbookRange.DetailMaterials.Count == 0 ? bGHandbookRange.DetailProducts : bGHandbookRange.DetailMaterials)
            {
                //detail.BGHandbookRangeId = bGHandbookRange.BGHandbookRangeId;
                //detail.ProductType = bGHandbookRange.ProductType;
                new BL.BGHandbookRangeDetailManager().Insert(detail);
            }
            //BL.V.CommitTransaction();
            //}
            //catch
            //{
            //    BL.V.RollbackTransaction();
            //    throw;
            //}
        }

        /// <summary>
        /// Update a BGHandbookRange.
        /// </summary>
        public void Update(Model.BGHandbookRange bGHandbookRange)
        {
            try
            {
                BL.V.BeginTransaction();
                bGHandbookRange.UpdateTime = DateTime.Now;
                new BL.BGHandbookRangeDetailManager().DeleteByBGHandbookId(bGHandbookRange.BGHandbookRangeId);
                foreach (Model.BGHandbookRangeDetail detail in bGHandbookRange.DetailMaterials.Count == 0 ? bGHandbookRange.DetailProducts : bGHandbookRange.DetailMaterials)
                {
                    //detail.BGHandbookRangeId = bGHandbookRange.BGHandbookRangeId;
                    //detail.ProductType = bGHandbookRange.ProductType;
                    new BL.BGHandbookRangeDetailManager().Insert(detail);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        public IList<Model.BGHandbookRange> SelectByDate(DateTime startDate, DateTime endDate)
        {
            return accessor.SelectByDate(startDate, endDate);
        }


    }
}

