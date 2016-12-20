//------------------------------------------------------------------------------
//
// file name：AtSummonDetailManager.cs
// author: mayanjun
// create date：2010-11-24 09:40:42
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.AtSummonDetail.
    /// </summary>
    public partial class AtSummonDetailManager
    {
		
		/// <summary>
		/// Delete AtSummonDetail by primary key.
		/// </summary>
		public void Delete(string summonDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(summonDetailId);
		}

		/// <summary>
		/// Insert a AtSummonDetail.
		/// </summary>
        public void Insert(Model.AtSummonDetail atSummonDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(atSummonDetail);
        }
		
		/// <summary>
		/// Update a AtSummonDetail.
		/// </summary>
        public void Update(Model.AtSummonDetail atSummonDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(atSummonDetail);
        }
        public IList<Model.AtSummonDetail> Select(Model.AtSummon atSummon)
        {
            return accessor.Select(atSummon);
        }
        public IList<Book.Model.AtSummonDetail> Select(DateTime startDate, DateTime endDate)
        {
            return accessor.Select(startDate, endDate);
        }
        public IList<Book.Model.AtSummonDetail> Select(DateTime startDate, DateTime endDate, string startSubjectId, string endSubjectId)
        
        {
            return accessor.Select(startDate, endDate,startSubjectId,endSubjectId);
        }
        public int CountSummonTo(string lending, string subjectId)
        {
            return accessor.CountSummonTo(lending, subjectId);
        }
    }
}

