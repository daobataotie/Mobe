//------------------------------------------------------------------------------
//
// file name：RelationXODetailManager.cs
// author: mayanjun
// create date：2015/4/19 下午 08:06:08
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.RelationXODetail.
    /// </summary>
    public partial class RelationXODetailManager
    {
		
		/// <summary>
		/// Delete RelationXODetail by primary key.
		/// </summary>
		public void Delete(string relationXODetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(relationXODetailId);
		}

		/// <summary>
		/// Insert a RelationXODetail.
		/// </summary>
        public void Insert(Model.RelationXODetail relationXODetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(relationXODetail);
        }
		
		/// <summary>
		/// Update a RelationXODetail.
		/// </summary>
        public void Update(Model.RelationXODetail relationXODetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(relationXODetail);
        }

        public IList<Model.RelationXODetail> SelectByHeaderId(string id)
        {
            return accessor.SelectByHeaderId(id);
        }
    }
}
