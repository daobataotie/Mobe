//------------------------------------------------------------------------------
//
// file name：PronoteProceduresDetailManager.cs
// author: mayanjun
// create date：2010-9-16 15:57:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.PronoteProceduresDetail.
    /// </summary>
    public partial class PronoteProceduresDetailManager
    {
		
		/// <summary>
		/// Delete PronoteProceduresDetail by primary key.
		/// </summary>
		public void Delete(string pronoteProceduresDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(pronoteProceduresDetailId);
		}

		/// <summary>
		/// Insert a PronoteProceduresDetail.
		/// </summary>
        public void Insert(Model.PronoteProceduresDetail pronoteProceduresDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(pronoteProceduresDetail);
        }
		
		/// <summary>
		/// Update a PronoteProceduresDetail.
		/// </summary>
        public void Update(Model.PronoteProceduresDetail pronoteProceduresDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(pronoteProceduresDetail);
        }

        public IList<Model.PronoteProceduresDetail> GetPronotedetailsMaterialByHeaderId(Model.PronoteHeader pro)
        {
            return accessor.GetPronotedetailsMaterialByHeaderId(pro);
        }

        public IList<Model.PronoteProceduresDetail> SelectByProceduresId(string proceduresId)
        {
            return accessor.SelectByProceduresId(proceduresId);
        }
        public IList<Model.PronoteProceduresDetail> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            return accessor.SelectByDateRange(startdate, enddate);
        }
    }
}

