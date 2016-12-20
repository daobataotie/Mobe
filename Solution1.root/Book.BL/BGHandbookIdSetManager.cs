//------------------------------------------------------------------------------
//
// file name：BGHandbookIdSetManager.cs
// author: mayanjun
// create date：2013-07-05 11:57:54
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookIdSet.
    /// </summary>
    public partial class BGHandbookIdSetManager
    {

        /// <summary>
        /// Delete BGHandbookIdSet by primary key.
        /// </summary>
        public void Delete(string bGHangBookId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(bGHangBookId);
        }

        /// <summary>
        /// Insert a BGHandbookIdSet.
        /// </summary>
        public void Insert(Model.BGHandbookIdSet bGHandbookIdSet)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(bGHandbookIdSet);
        }

        /// <summary>
        /// Update a BGHandbookIdSet.
        /// </summary>
        public void Update(Model.BGHandbookIdSet bGHandbookIdSet)
        {
            //
            // todo: add other logic here.
            //
            bGHandbookIdSet.InsertTime = DateTime.Now;
            accessor.Update(bGHandbookIdSet);
        }

        public void Update(IList<Model.BGHandbookIdSet> _bgHandboolIdSetList)
        {
            this.Validate(_bgHandboolIdSetList);

            try
            {
                BL.V.BeginTransaction();

                foreach (Model.BGHandbookIdSet model in _bgHandboolIdSetList)
                {
                    if (this.ExistsPrimary(model.BGHangBookId))
                    {
                        model.UpdateTime = DateTime.Now;
                        accessor.Update(model);
                    }
                    else
                        this.Insert(model);
                }
                BL.V.CommitTransaction();
            }
            catch
            {
                BL.V.RollbackTransaction();
                throw;
            }
        }

        private void Validate(IList<Book.Model.BGHandbookIdSet> _bgHandboolIdSetList)
        {
            foreach (Model.BGHandbookIdSet model in _bgHandboolIdSetList)
            {
                if (string.IsNullOrEmpty(model.BGHangBookId))
                    throw new Helper.RequireValueException(Model.BGHandbookIdSet.PRO_BGHangBookId);
            }

            var ret = from n in _bgHandboolIdSetList
                      group n by n.BGHangBookId into s
                      select new { a = s.Count() };

            foreach (var item in ret)
            {
                if (item.a > 1)
                    throw new Helper.InvalidValueException(Model.BGHandbookIdSet.PRO_BGHangBookId);
            }
        }

        public IList<Model.BGHandbookIdSet> SelectHasUsing()
        {
            return accessor.SelectHasUsing();
        }

        public IList<string> SelectBGHandbookId()
        {
            return accessor.SelectBGHandbookId();
        }
    }
}

