//------------------------------------------------------------------------------
//
// file name：ANSIPCImpactCheckDetailManager.cs
// author: mayanjun
// create date：2011-11-23 09:50:26
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ANSIPCImpactCheckDetail.
    /// </summary>
    public partial class ANSIPCImpactCheckDetailManager
    {

        /// <summary>
        /// Delete ANSIPCImpactCheckDetail by primary key.
        /// </summary>
        public void Delete(string aNSIPCImpactCheckDetailsID)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(aNSIPCImpactCheckDetailsID);
        }

        /// <summary>
        /// Insert a ANSIPCImpactCheckDetail.
        /// </summary>
        public void Insert(Model.ANSIPCImpactCheckDetail aNSIPCImpactCheckDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(aNSIPCImpactCheckDetail);
        }

        /// <summary>
        /// Update a ANSIPCImpactCheckDetail.
        /// </summary>
        public void Update(Model.ANSIPCImpactCheckDetail aNSIPCImpactCheckDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(aNSIPCImpactCheckDetail);
        }

        public IList<Model.ANSIPCImpactCheckDetail> SelectByAnispcicID(string AnsipcicId)
        {
            return accessor.Select(AnsipcicId);
        }
    }
}

