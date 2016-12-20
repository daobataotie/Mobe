//------------------------------------------------------------------------------
//
// file name：AcCollectionManager.cs
// author: mayanjun
// create date：2011-6-23 09:29:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcCollection.
    /// </summary>
    public partial class AcCollectionManager : BaseManager
    {

        private static readonly DA.IAcCollectionDetailAccessor acCollectionDetailaccessor = (DA.IAcCollectionDetailAccessor)Accessors.Get("AcCollectionDetail");
        /// <summary>
        /// Delete AcCollection by primary key.
        /// </summary>
        public void Delete(string acCollectionId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(acCollectionId);
        }

        public Model.AcCollection GetDetails(Model.AcCollection acCollection)
        {
            Model.AcCollection temp = accessor.Get(acCollection.AcCollectionId);
            if (temp != null)
                temp.Detail = acCollectionDetailaccessor.Select(temp);
            return temp;
        }

        /// <summary>
        /// Insert a AcCollection.
        /// </summary>
        public void Insert(Model.AcCollection acCollection)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(acCollection);

            foreach (Model.AcCollectionDetail item in acCollection.Detail)
            {
                acCollectionDetailaccessor.Insert(item);
            }
        }

        /// <summary>
        /// Update a AcCollection.
        /// </summary>
        public void Update(Model.AcCollection acCollection)
        {
            this.Delete(acCollection.AcCollectionId);

            this.Insert(acCollection);
        }
    }
}

