//------------------------------------------------------------------------------
//
// file name：PCFirstOnlineCheckDetailManager.cs
// author: mayanjun
// create date：2020/10/30 22:05:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCFirstOnlineCheckDetail.
    /// </summary>
    public partial class PCFirstOnlineCheckDetailManager
    {

        /// <summary>
        /// Delete PCFirstOnlineCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCFirstOnlineCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCFirstOnlineCheckDetailId);
        }

        /// <summary>
        /// Insert a PCFirstOnlineCheckDetail.
        /// </summary>
        public void Insert(Model.PCFirstOnlineCheckDetail pCFirstOnlineCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCFirstOnlineCheckDetail);
        }

        /// <summary>
        /// Update a PCFirstOnlineCheckDetail.
        /// </summary>
        public void Update(Model.PCFirstOnlineCheckDetail pCFirstOnlineCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCFirstOnlineCheckDetail);
        }

        public IList<Model.PCFirstOnlineCheckDetail> SelectByHeaderId(string pCFirstOnlineCheckId)
        {
            return accessor.SelectByHeaderId(pCFirstOnlineCheckId);
        }

        public IList<Model.PCFirstOnlineCheckDetail> SelectByCondition(DateTime startDate, DateTime endDate, string CustomerInvoiceXOId)
        {
            return accessor.SelectByCondition(startDate, endDate, CustomerInvoiceXOId);
        }
    }
}
