//------------------------------------------------------------------------------
//
// file name：PCPGOnlineCheckDetailManager.cs
// author: mayanjun
// create date：2011-12-6 14:34:43
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCPGOnlineCheckDetail.
    /// </summary>
    public partial class PCPGOnlineCheckDetailManager
    {

        /// <summary>
        /// Delete PCPGOnlineCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCPGOnlineCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCPGOnlineCheckDetailId);
        }

        /// <summary>
        /// Insert a PCPGOnlineCheckDetail.
        /// </summary>
        public void Insert(Model.PCPGOnlineCheckDetail pCPGOnlineCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCPGOnlineCheckDetail);
        }

        /// <summary>
        /// Update a PCPGOnlineCheckDetail.
        /// </summary>
        public void Update(Model.PCPGOnlineCheckDetail pCPGOnlineCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCPGOnlineCheckDetail);
        }

        public IList<Model.PCPGOnlineCheckDetail> SelectByFromInvoiceId(string id)
        {
            return accessor.SelectByFromInvoiceId(id);
        }

        public string GetTimerListString(string PCPGOnlineCheckId)
        {
            return accessor.GetTimerListString(PCPGOnlineCheckId);
        }

        public void DeleteByPCPGOnlineCheckId(string IPCPGOnlineCheckId)
        {
            accessor.DeleteByPCPGOnlineCheckId(IPCPGOnlineCheckId);
        }

        public IList<Book.Model.PCPGOnlineCheckDetail> Select(string pcpgocId)
        {
            return accessor.Select(pcpgocId);
        }
    }
}

