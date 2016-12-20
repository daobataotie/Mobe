//------------------------------------------------------------------------------
//
// file name：AcOtherShouldCollectionDetailManager.cs
// author: mayanjun
// create date：2011-6-10 11:19:27
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AcOtherShouldCollectionDetail.
    /// </summary>
    public partial class AcOtherShouldCollectionDetailManager
    {
		
		/// <summary>
		/// Delete AcOtherShouldCollectionDetail by primary key.
		/// </summary>
		public void Delete(string acOtherShouldCollectionDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(acOtherShouldCollectionDetailId);
		}

		/// <summary>
		/// Insert a AcOtherShouldCollectionDetail.
		/// </summary>
        public void Insert(Model.AcOtherShouldCollectionDetail acOtherShouldCollectionDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(acOtherShouldCollectionDetail);
        }
		
		/// <summary>
		/// Update a AcOtherShouldCollectionDetail.
		/// </summary>
        public void Update(Model.AcOtherShouldCollectionDetail acOtherShouldCollectionDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(acOtherShouldCollectionDetail);
        }
        public IList<Model.AcOtherShouldCollectionDetail> Select(Model.AcOtherShouldCollection acOtherShouldCollection)
        {
            return accessor.Select(acOtherShouldCollection);
        }
    }
}

