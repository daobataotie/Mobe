//------------------------------------------------------------------------------
//
// file name：BGProductDetaiManager.cs
// author: mayanjun
// create date：2013-4-1 11:58:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.BGProductDetai.
    /// </summary>
    public partial class BGProductDetaiManager
    {

        /// <summary>
        /// Delete BGProductDetai by primary key.
        /// </summary>
        public void Delete(string bGProductDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(bGProductDetailId);
        }

        /// <summary>
        /// Insert a BGProductDetai.
        /// </summary>
        public void Insert(Model.BGProductDetai bGProductDetai)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(bGProductDetai);
        }

        /// <summary>
        /// Update a BGProductDetai.
        /// </summary>
        public void Update(Model.BGProductDetai bGProductDetai)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(bGProductDetai);
        }

        public void DeleteByBGProductId(string BGProductId)
        {
            accessor.DeleteByBGProductId(BGProductId);
        }

        /// <summary>
        /// 查询料件
        /// </summary>
        /// <param name="BGProductId"></param>
        /// <returns></returns>
        public IList<Model.BGProductDetai> SelectProductByBGProductId(string BGProductId)
        {
            return accessor.SelectProductByBGProductId(BGProductId);
        }

        /// <summary>
        /// 查询成品
        /// </summary>
        /// <param name="BGProductId"></param>
        /// <returns></returns>
        public IList<Model.BGProductDetai> SelectMaterialByBGProductId(string BGProductId)
        {
            return accessor.SelectMaterialByBGProductId(BGProductId);
        }
    }
}

