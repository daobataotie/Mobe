//------------------------------------------------------------------------------
//
// file name：AcbeginbillReceivableDetailManager.cs
// author: mayanjun
// create date：2011-6-9 14:42:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcbeginbillReceivableDetail.
    /// </summary>
    public partial class AcbeginbillReceivableDetailManager
    {

        /// <summary>
        /// Delete AcbeginbillReceivableDetail by primary key.
        /// </summary>
        public void Delete(string acbeginbillReceivableDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acbeginbillReceivableDetailId);
        }

        /// <summary>
        /// Insert a AcbeginbillReceivableDetail.
        /// </summary>
        public void Insert(Model.AcbeginbillReceivableDetail acbeginbillReceivableDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(acbeginbillReceivableDetail);
        }

        /// <summary>
        /// Update a AcbeginbillReceivableDetail.
        /// </summary>
        public void Update(Model.AcbeginbillReceivableDetail acbeginbillReceivableDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(acbeginbillReceivableDetail);
        }

        public IList<Model.AcbeginbillReceivableDetail> Select(Model.AcbeginbillReceivable acbeginbillReceivable)
        {
            return accessor.Select(acbeginbillReceivable);
        }

        public IList<Model.AcbeginbillReceivableDetail> SelectDefaultDetails()
        {
            return accessor.SelectDefaultDetails();
        }

    }
}

