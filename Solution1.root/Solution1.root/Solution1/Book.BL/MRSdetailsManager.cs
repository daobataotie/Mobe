//------------------------------------------------------------------------------
//
// file name：MRSdetailsManager.cs
// author: peidun
// create date：2009-12-18 11:12:39
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.MRSdetails.
    /// </summary>
    public partial class MRSdetailsManager
    {
		
		/// <summary>
		/// Delete MRSdetails by primary key.
		/// </summary>
		public void Delete(string mRSdetailsID)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(mRSdetailsID);
		}

		/// <summary>
		/// Insert a MRSdetails.
		/// </summary>
        public void Insert(Model.MRSdetails mRSdetails)
        {
			//
			// todo:add other logic here
			//
            mRSdetails.MRSdetailsId = Guid.NewGuid().ToString();
            accessor.Insert(mRSdetails);
        }
		
		/// <summary>
		/// Update a MRSdetails.
		/// </summary>
        public void Update(Model.MRSdetails mRSdetails)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(mRSdetails);
        }
        public IList<Book.Model.MRSdetails> Select(Model.MRSHeader mRSheader)
        {
            return accessor.Select(mRSheader);
        }

        //public IList<Book.Model.MRSdetails> GetMrsdetailBySourceType(string sourceType)
        //{
        //    return accessor.GetMrsdetailBySourceType(sourceType);
        //}
        public IList<Book.Model.MRSdetails> Select(string mpsHeaderId, string sourceType)
        {
            return accessor.Select(mpsHeaderId, sourceType);
        }
        public IList<Book.Model.MRSdetails> GetDate(DateTime startDate, DateTime endDate, string sourceType)
        {
            return accessor.GetDate(startDate,endDate,sourceType);
        }
    }
}

