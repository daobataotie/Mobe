//------------------------------------------------------------------------------
//
// file name：BGHandbookDetail2Manager.cs
// author: mayanjun
// create date：2013-4-16 11:58:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGHandbookDetail2.
    /// </summary>
    public partial class BGHandbookDetail2Manager : BaseManager
    {

        /// <summary>
        /// Delete BGHandbookDetail2 by primary key.
        /// </summary>
        public void Delete(string bGHandbookDetail2Id)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(bGHandbookDetail2Id);
        }

        /// <summary>
        /// Insert a BGHandbookDetail2.
        /// </summary>
        public void Insert(Model.BGHandbookDetail2 bGHandbookDetail2)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(bGHandbookDetail2);
        }

        /// <summary>
        /// Update a BGHandbookDetail2.
        /// </summary>

        public void Update(Model.BGHandbookDetail2 bGHandbookDetail2)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(bGHandbookDetail2);
        }

        public IList<Book.Model.BGHandbookDetail2> Select(string pac)
        {
            return accessor.Select(pac);
        }

        public void UpdateCeIn(string bgid, string lid, double quantity)
        {
            accessor.UpdateCeIn(bgid, lid, quantity);
        }

        /// <summary>
        /// 通过手册号和Id查询料件集合
        /// </summary>
        /// <param name="bgid"></param>
        /// <param name="lid"></param>
        /// <returns></returns>
        public IList<Book.Model.BGHandbookDetail2> SelectbyShouceandId(string bgid, string lid)
        {
            return accessor.SelectbyShouceandId(bgid, lid);
        }

        public Model.BGHandbookDetail2 SelectBGProduct(string BGHandBookId, string Id)
        {
            return accessor.SelectBGProduct(BGHandBookId, Id);
        }

        public IList<Model.BGHandbookDetail2> SelectByShouce(string Id)
        {
            return accessor.SelectByShouce(Id);
        }

        /// <summary>
        /// 通过手册号和ID查询符合条件的最新手册料件
        /// </summary>
        /// <param name="Shouce"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Model.BGHandbookDetail2 SelectByShouceAndId(string Shouce, int id)
        {
            return accessor.SelectByShouceAndId(Shouce, id);
        }
    }
}

