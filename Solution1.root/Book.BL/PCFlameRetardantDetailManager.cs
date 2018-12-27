//------------------------------------------------------------------------------
//
// file name：PCFlameRetardantDetailManager.cs
// author: mayanjun
// create date：2018/12/27 13:17:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCFlameRetardantDetail.
    /// </summary>
    public partial class PCFlameRetardantDetailManager
    {

        /// <summary>
        /// Delete PCFlameRetardantDetail by primary key.
        /// </summary>
        public void Delete(string pCFlameRetardantDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCFlameRetardantDetailId);
        }

        /// <summary>
        /// Insert a PCFlameRetardantDetail.
        /// </summary>
        public void Insert(Model.PCFlameRetardantDetail pCFlameRetardantDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCFlameRetardantDetail);
        }

        /// <summary>
        /// Update a PCFlameRetardantDetail.
        /// </summary>
        public void Update(Model.PCFlameRetardantDetail pCFlameRetardantDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCFlameRetardantDetail);
        }

        public IList<Model.PCFlameRetardantDetail> SelectByPrimaryId(string primaryId)
        {
            return accessor.SelectByPrimaryId(primaryId);
        }

        public void DeleteByPrimaryId(string primaryid)
        {
            accessor.DeleteByPrimaryId(primaryid);
        }

        public IList<Model.PCFlameRetardantDetail> SelectByDateRage(DateTime startDate, DateTime endDate, string productid, string cusXOId)
        {
            return accessor.SelectByDateRage(startDate, endDate, productid, cusXOId);
        }
    }
}
