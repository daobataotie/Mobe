//------------------------------------------------------------------------------
//
// file name：accepterattribManager.cs
// author: peidun
// create date：2009-11-18 15:33:06
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.accepterattrib.
    /// </summary>
    public partial class accepterattribManager
    {

        /// <summary>
        /// Delete accepterattrib by primary key.
        /// </summary>
        public void Delete(string accepterattribID)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(accepterattribID);
        }

        /// <summary>
        /// Insert a accepterattrib.
        /// </summary>
        public void Insert(Model.accepterattrib accepterattrib)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(accepterattrib);
        }

        /// <summary>
        /// Update a accepterattrib.
        /// </summary>
        public void Update(Model.accepterattrib accepterattrib)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(accepterattrib);
        }

        public void DeleteByProcessId(string processid)
        {
            accessor.DeleteByProcessId(processid);
        }

        public IList<Model.accepterattrib> SelectByProcessId(string processid)
        {
            return accessor.SelectByProcessId(processid);
        }
    }
}

