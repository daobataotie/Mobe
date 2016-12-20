//------------------------------------------------------------------------------
//
// file name：PCMaterialCheckDetailManager.cs
// author: mayanjun
// create date：2015/10/24 17:47:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PCMaterialCheckDetail.
    /// </summary>
    public partial class PCMaterialCheckDetailManager
    {

        /// <summary>
        /// Delete PCMaterialCheckDetail by primary key.
        /// </summary>
        public void Delete(string pCMaterialCheckDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(pCMaterialCheckDetailId);
        }

        /// <summary>
        /// Insert a PCMaterialCheckDetail.
        /// </summary>
        public void Insert(Model.PCMaterialCheckDetail pCMaterialCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(pCMaterialCheckDetail);
        }

        /// <summary>
        /// Update a PCMaterialCheckDetail.
        /// </summary>
        public void Update(Model.PCMaterialCheckDetail pCMaterialCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(pCMaterialCheckDetail);
        }

        public IList<Model.PCMaterialCheckDetail> SelectByCondition(DateTime StartDate, DateTime EndDate, string StartPId, string EndPId, string InvoiceCusId)
        {
            return accessor.SelectByCondition(StartDate, EndDate, StartPId, EndPId, InvoiceCusId);
        }
    }
}
