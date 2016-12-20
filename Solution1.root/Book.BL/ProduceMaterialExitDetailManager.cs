//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitDetailManager.cs
// author: peidun
// create date：2010-1-6 10:26:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceMaterialExitDetail.
    /// </summary>
    ///   
    /// 
   
    public partial class ProduceMaterialExitDetailManager
    {
     
		/// <summary>
		/// Delete ProduceMaterialExitDetail by primary key.
		/// </summary>
		public void Delete(string produceExitMaterialDetailId)
		{
			//
			// todo:add other logic here
			//
			accessor.Delete(produceExitMaterialDetailId);
		}

		/// <summary>
		/// Insert a ProduceMaterialExitDetail.
		/// </summary>
        public void Insert(Model.ProduceMaterialExitDetail produceMaterialExitDetail)
        {
			//
			// todo:add other logic here
			//
            accessor.Insert(produceMaterialExitDetail);
        }
		
		/// <summary>
		/// Update a ProduceMaterialExitDetail.
		/// </summary>
        public void Update(Model.ProduceMaterialExitDetail produceMaterialExitDetail)
        {
			//
			// todo: add other logic here.
			//
            accessor.Update(produceMaterialExitDetail);
        }
        public IList<Model.ProduceMaterialExitDetail> Select(Model.ProduceMaterialExit ProduceMaterialExit)
        {
            return accessor.Select(ProduceMaterialExit);

        }
        public IList<Book.Model.ProduceMaterialExitDetail> Select(string houseid, DateTime startDate, DateTime endDate)
        { 
         return accessor.Select(houseid,  startDate,  endDate);
        }

        public IList<Model.ProduceMaterialExitDetail> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialExitId0, string produceMaterialExitId1, Model.Product pId0, Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            return accessor.SelectBycondition(starDate, endDate, produceMaterialExitId0, produceMaterialExitId1, pId0, pId1, departmentId0, departmentId1, PronoteHeaderId0, PronoteHeaderId1);
        }
        public void Delete(Model.ProduceMaterialExit produceMaterialExit)
        {
            accessor.Delete(produceMaterialExit);
        }
    }
}

