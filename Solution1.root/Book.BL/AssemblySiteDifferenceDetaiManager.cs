//------------------------------------------------------------------------------
//
// file name：AssemblySiteDifferenceDetaiManager.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AssemblySiteDifferenceDetai.
    /// </summary>
    public partial class AssemblySiteDifferenceDetaiManager
    {

        /// <summary>
        /// Delete AssemblySiteDifferenceDetai by primary key.
        /// </summary>
        public void Delete(string assemblySiteDifferenceDetaiId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(assemblySiteDifferenceDetaiId);
        }

        /// <summary>
        /// Insert a AssemblySiteDifferenceDetai.
        /// </summary>
        public void Insert(Model.AssemblySiteDifferenceDetai assemblySiteDifferenceDetai)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(assemblySiteDifferenceDetai);
        }

        /// <summary>
        /// Update a AssemblySiteDifferenceDetai.
        /// </summary>
        public void Update(Model.AssemblySiteDifferenceDetai assemblySiteDifferenceDetai)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(assemblySiteDifferenceDetai);
        }

        public IList<Model.AssemblySiteDifferenceDetai> SelectByDateRage(DateTime startDate, DateTime endDate, string productid)
        {
            return accessor.SelectByDateRage(startDate, endDate, productid);
        }
    }
}
