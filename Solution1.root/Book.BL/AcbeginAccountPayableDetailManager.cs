//------------------------------------------------------------------------------
//
// file name：AcbeginAccountPayableDetailManager.cs
// author: mayanjun
// create date：2011-6-9 14:42:09
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcbeginAccountPayableDetail.
    /// </summary>
    public partial class AcbeginAccountPayableDetailManager
    {

        /// <summary>
        /// Delete AcbeginAccountPayableDetail by primary key.
        /// </summary>
        public void Delete(string acbeginAccountPayableDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acbeginAccountPayableDetail);
        }

        /// <summary>
        /// Insert a AcbeginAccountPayableDetail.
        /// </summary>
        public void Insert(Model.AcbeginAccountPayableDetail acbeginAccountPayableDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(acbeginAccountPayableDetail);
        }

        /// <summary>
        /// Update a AcbeginAccountPayableDetail.
        /// </summary>
        public void Update(Model.AcbeginAccountPayableDetail acbeginAccountPayableDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(acbeginAccountPayableDetail);
        }

        public IList<Model.AcbeginAccountPayableDetail> Select(Model.AcbeginAccountPayable acbeginAccountPayable)
        {
            return accessor.Select(acbeginAccountPayable);
        }

        public IList<Model.AcbeginAccountPayableDetail> SelectDefaultDetails()
        {
            return accessor.SelectDefaultDetails();
        }
    }
}

