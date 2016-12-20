//------------------------------------------------------------------------------
//
// file name：AcCollectionDetailManager.cs
// author: mayanjun
// create date：2011-6-23 09:29:20
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcCollectionDetail.
    /// </summary>
    public partial class AcCollectionDetailManager
    {
		
		/// <summary>
		/// Delete AcCollectionDetail by primary key.
		/// </summary>
		public void Delete(string acCollectionDetail)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(acCollectionDetail);
		}

		/// <summary>
		/// Insert a AcCollectionDetail.
		/// </summary>
        public void Insert(Model.AcCollectionDetail acCollectionDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(acCollectionDetail);
        }
		
		/// <summary>
		/// Update a AcCollectionDetail.
		/// </summary>
        public void Update(Model.AcCollectionDetail acCollectionDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(acCollectionDetail);
        }

        public IList<Model.AcCollectionDetail> Select(Model.AcCollection acCollection)
        {
            return accessor.Select(acCollection);
        }
    }
}

