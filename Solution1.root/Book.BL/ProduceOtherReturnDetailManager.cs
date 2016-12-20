//------------------------------------------------------------------------------
//
// file name：ProduceOtherReturnDetailManager.cs
// author: mayanjun
// create date：2011-08-31 15:05:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceOtherReturnDetail.
    /// </summary>
    public partial class ProduceOtherReturnDetailManager
    {

        /// <summary>
        /// Delete ProduceOtherReturnDetail by primary key.
        /// </summary>
        public void Delete(string produceOtherReturnDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceOtherReturnDetailId);
        }

        /// <summary>
        /// Insert a ProduceOtherReturnDetail.
        /// </summary>
        public void Insert(Model.ProduceOtherReturnDetail produceOtherReturnDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(produceOtherReturnDetail);
        }

        /// <summary>
        /// Update a ProduceOtherReturnDetail.
        /// </summary>
        public void Update(Model.ProduceOtherReturnDetail produceOtherReturnDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(produceOtherReturnDetail);
        }
        public System.Collections.Generic.IList<Model.ProduceOtherReturnDetail> Select(Model.ProduceOtherReturnMaterial Material)
        {
            return accessor.Select(Material);
        }

    }
}

