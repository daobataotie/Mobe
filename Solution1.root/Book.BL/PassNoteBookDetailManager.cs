//------------------------------------------------------------------------------
//
// file name：PassNoteBookDetailManager.cs
// author: mayanjun
// create date：2013-4-3 17:47:17
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PassNoteBookDetail.
    /// </summary>
    public partial class PassNoteBookDetailManager
    {
		
		/// <summary>
		/// Delete PassNoteBookDetail by primary key.
		/// </summary>
		public void Delete(string passNoteBookDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(passNoteBookDetailId);
		}

		/// <summary>
		/// Insert a PassNoteBookDetail.
		/// </summary>
        public void Insert(Model.PassNoteBookDetail passNoteBookDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(passNoteBookDetail);
        }
		
		/// <summary>
		/// Update a PassNoteBookDetail.
		/// </summary>
        public void Update(Model.PassNoteBookDetail passNoteBookDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(passNoteBookDetail);
        }
    }
}

