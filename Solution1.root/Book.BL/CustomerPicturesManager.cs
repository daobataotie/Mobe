//------------------------------------------------------------------------------
//
// file name：CustomerPicturesManager.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.CustomerPictures.
    /// </summary>
    public partial class CustomerPicturesManager
    {
		
		/// <summary>
		/// Delete CustomerPictures by primary key.
		/// </summary>
		public void Delete(string picturesId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(picturesId);
		}

		/// <summary>
		/// Insert a CustomerPictures.
		/// </summary>
        public void Insert(Model.CustomerPictures customerPictures)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(customerPictures);
        }
		
		/// <summary>
		/// Update a CustomerPictures.
		/// </summary>
        public void Update(Model.CustomerPictures customerPictures)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(customerPictures);
        }
    }
}

