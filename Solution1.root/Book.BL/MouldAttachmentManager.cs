//------------------------------------------------------------------------------
//
// file name：MouldAttachmentManager.cs
// author: peidun
// create date：2009-08-03 9:37:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MouldAttachment.
    /// </summary>
    public partial class MouldAttachmentManager : BaseManager
    {
		
		/// <summary>
		/// Delete MouldAttachment by primary key.
		/// </summary>
		public void Delete(string mouldAttachmentId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(mouldAttachmentId);
		}

		/// <summary>
		/// Insert a MouldAttachment.
		/// </summary>
        public void Insert(Model.MouldAttachment mouldAttachment)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(mouldAttachment);
        }
		
		/// <summary>
		/// Update a MouldAttachment.
		/// </summary>
        public void Update(Model.MouldAttachment mouldAttachment)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(mouldAttachment);
        }

        public void DeleteByMouldid(string mouldid)
        {
            accessor.DeleteByMouldid(mouldid);
        }

        public IList<Model.MouldAttachment> SelectByMouldId(Model.ProductMould mould)
        {
            return accessor.SelectByMouldId(mould);
        }
    }
}

