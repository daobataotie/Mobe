//------------------------------------------------------------------------------
//
// file name：PCMouldOnlineCheckDetailManager.cs
// author: mayanjun
// create date：2015/4/13 上午 10:11:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCMouldOnlineCheckDetail.
    /// </summary>
    public partial class PCMouldOnlineCheckDetailManager
    {

        /// <summary>
        /// Delete PCMouldOnlineCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCMouldOnlineCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCMouldOnlineCheckDetailId);
        }

        /// <summary>
        /// Insert a PCMouldOnlineCheckDetail.
        /// </summary>
        public void Insert(Model.PCMouldOnlineCheckDetail pCMouldOnlineCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCMouldOnlineCheckDetail);
        }

        /// <summary>
        /// Update a PCMouldOnlineCheckDetail.
        /// </summary>
        public void Update(Model.PCMouldOnlineCheckDetail pCMouldOnlineCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCMouldOnlineCheckDetail);
        }

        public void DeleteByHeaderId(string id)
        {
            accessor.DeleteByHeaderId(id);
        }

        public IList<Model.PCMouldOnlineCheckDetail> SelectByCondition(DateTime OnlineDateStart, DateTime OnlineDateEnd, DateTime CheckDateStart, DateTime CheckDateEnd, string productId, string invoiceCusId)
        {
            return accessor.SelectByCondition(OnlineDateStart, OnlineDateEnd, CheckDateStart, CheckDateEnd, productId, invoiceCusId);
        }

        public IList<Model.PCMouldOnlineCheckDetail> SelectByInvoiceCusId(string invoiceCusId)
        {
            return accessor.SelectByInvoiceCusId(invoiceCusId);
        }

        public IList<Model.PCMouldOnlineCheckDetail> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }
    }
}
