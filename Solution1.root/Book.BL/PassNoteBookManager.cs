//------------------------------------------------------------------------------
//
// file name：PassNoteBookManager.cs
// author: mayanjun
// create date：2013-4-3 17:47:17
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PassNoteBook.
    /// </summary>
    public partial class PassNoteBookManager
    {
		
		/// <summary>
		/// Delete PassNoteBook by primary key.
		/// </summary>
		public void Delete(string passNoteBookId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(passNoteBookId);
		}

		/// <summary>
		/// Insert a PassNoteBook.
		/// </summary>
        public void Insert(Model.PassNoteBook passNoteBook)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(passNoteBook);
        }
		
		/// <summary>
		/// Update a PassNoteBook.
		/// </summary>
        public void Update(Model.PassNoteBook passNoteBook)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(passNoteBook);
        }
    }
}

