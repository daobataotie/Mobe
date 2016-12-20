//------------------------------------------------------------------------------
//
// file name：PronotedetailsManager.cs
// author: peidun
// create date：2009-12-29 11:58:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.Pronotedetails.
    /// </summary>
    public partial class PronotedetailsManager
    {
		
		/// <summary>
		/// Delete Pronotedetails by primary key.
		/// </summary>
		public void Delete(string pronotedetailsID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pronotedetailsID);
		}

		/// <summary>
		/// Insert a Pronotedetails.
		/// </summary>
        public void Insert(Model.Pronotedetails pronotedetails)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pronotedetails);
        }
		
		/// <summary>
		/// Update a Pronotedetails.
		/// </summary>
        public void Update(Model.Pronotedetails pronotedetails)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pronotedetails);
        }
        public IList<Book.Model.Pronotedetails> Select(Model.PronoteHeader pronoteHeader)
        {
           return accessor.Select(pronoteHeader);
        }
        public double GetByMPSdetail(string mPSDetailId)
        {
            return accessor.GetByMPSdetail(mPSDetailId);
        }
        public IList<Book.Model.Pronotedetails> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd)
        {
            return accessor.Select(customerStart,customerEnd, dateStart, dateEnd);
        }
    }
}

