//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsCheckDetailManager.cs
// author: mayanjun
// create date：2011-07-22 10:44:50
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceStatisticsCheckDetail.
    /// </summary>
    public partial class ProduceStatisticsCheckDetailManager
    {
		
		/// <summary>
		/// Delete ProduceStatisticsCheckDetail by primary key.
		/// </summary>
		public void Delete(string produceStatisticsCheckDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceStatisticsCheckDetailId);
		}

		/// <summary>
		/// Insert a ProduceStatisticsCheckDetail.
		/// </summary>
        public void Insert(Model.ProduceStatisticsCheckDetail produceStatisticsCheckDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceStatisticsCheckDetail);
        }
		
		/// <summary>
		/// Update a ProduceStatisticsCheckDetail.
		/// </summary>
        public void Update(Model.ProduceStatisticsCheckDetail produceStatisticsCheckDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceStatisticsCheckDetail);
        }
        public IList<Book.Model.ProduceStatisticsCheckDetail> Select(Model.ProduceStatisticsCheck produceStatisticsCheck)
        {
            return accessor.Select(produceStatisticsCheck);
        }
    }
}

