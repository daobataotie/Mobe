//------------------------------------------------------------------------------
//
// file name：ProduceStatisticsDetailManager.cs
// author: mayanjun
// create date：2011-4-8 09:17:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceStatisticsDetail.
    /// </summary>
    public partial class ProduceStatisticsDetailManager:BaseManager
    {
		
		/// <summary>
		/// Delete ProduceStatisticsDetail by primary key.
		/// </summary>
		public void Delete(string produceStatisticsDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceStatisticsDetailId);
		}

        public void Delete(Model.ProduceStatisticsDetail ptd)
        {
            this.Delete(ptd.ProduceStatisticsDetailId);
        }

		/// <summary>
		/// Insert a ProduceStatisticsDetail.
		/// </summary>
        public void Insert(Model.ProduceStatisticsDetail produceStatisticsDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceStatisticsDetail);
        }
		
		/// <summary>
		/// Update a ProduceStatisticsDetail.
		/// </summary>
        public void Update(Model.ProduceStatisticsDetail produceStatisticsDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceStatisticsDetail);
        }
        public void UpdateList(IList< Model.ProduceStatisticsDetail> produceStatisticsDetail)
        {
            foreach (Model.ProduceStatisticsDetail ps in produceStatisticsDetail)
            {

                if (accessor.ExistsPrimary(ps.ProduceStatisticsDetailId))
                {
                    ps.UpdateTime = DateTime.Now;
                    accessor.Update(ps);
                }

                else
                {

                    this.Insert(ps);
                }
            }      
            //
            // todo: add other logic here.
            //
           // accessor.Update(produceStatisticsDetail);
        }
        public IList<Book.Model.ProduceStatisticsDetail> Select(Model.ProduceStatistics produceStatistics)
        {
            return accessor.Select(produceStatistics);
        }
        public IList<Book.Model.ProduceStatisticsDetail> SelectbyPronoteHeaderProcedures(string PronoteHeaderID, string ProceduresId)
        {
            return accessor.SelectbyPronoteHeaderProcedures(PronoteHeaderID,  ProceduresId);
        }
        public Book.Model.ProduceStatisticsDetail SelectbyPronoteHeaderProceduresSum(string PronoteHeaderID, string ProceduresId)
        {
            return accessor.SelectbyPronoteHeaderProceduresSum(PronoteHeaderID, ProceduresId);
        }
        public IList<Model.ProduceStatisticsDetail> SelectByDateRangeAndPronoteHeaderId(DateTime startdate, DateTime enddate, string pronoteHeaderId)
        {
            return accessor.SelectByDateRangeAndPronoteHeaderId(startdate, enddate, pronoteHeaderId);
        }
    }
}

