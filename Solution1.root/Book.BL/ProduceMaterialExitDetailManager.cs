﻿//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitDetailManager.cs
// author: peidun
// create date：2010-1-6 10:26:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

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
            return accessor.Select(houseid, startDate, endDate);
        }

        /// <summary>
        /// 暂不用
        /// </summary>
        /// <param name="starDate"></param>
        /// <param name="endDate"></param>
        /// <param name="produceMaterialExitId0"></param>
        /// <param name="produceMaterialExitId1"></param>
        /// <param name="pId0"></param>
        /// <param name="pId1"></param>
        /// <param name="departmentId0"></param>
        /// <param name="departmentId1"></param>
        /// <param name="PronoteHeaderId0"></param>
        /// <param name="PronoteHeaderId1"></param>
        /// <returns></returns>
        public IList<Model.ProduceMaterialExitDetail> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialExitId0, string produceMaterialExitId1, Model.Product pId0, Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            return accessor.SelectBycondition(starDate, endDate, produceMaterialExitId0, produceMaterialExitId1, pId0, pId1, departmentId0, departmentId1, PronoteHeaderId0, PronoteHeaderId1);
        }
        public void Delete(Model.ProduceMaterialExit produceMaterialExit)
        {
            accessor.Delete(produceMaterialExit);
        }

        public double SelectSumQtyFromZuzhuang(string productId, DateTime dateStart, DateTime dateEnd, string workHouseId, string allInvoiceXOIds)
        {
            return accessor.SelectSumQtyFromZuzhuang(productId, dateStart, dateEnd, workHouseId, allInvoiceXOIds);
        }

        public IList<Model.ProduceMaterialExitDetail> SelectSumQtyFromZuzhuangByPros(string productIds, DateTime dateStart, DateTime dateEnd, string workHouseId, string allInvoiceXOIds)
        {
            return accessor.SelectSumQtyFromZuzhuangByPros(productIds, dateStart, dateEnd, workHouseId, allInvoiceXOIds);
        }

        public double SelectSumQtyFromZuzhuangAll(string productId, DateTime dateEnd, string workHouseId)
        {
            return accessor.SelectSumQtyFromZuzhuangAll(productId, dateEnd, workHouseId);
        }

        /// <summary>
        /// 用此方法
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="startPMEId"></param>
        /// <param name="endPMEId"></param>
        /// <param name="startPronoteHeaderId"></param>
        /// <param name="endPronoteHeaderId"></param>
        /// <param name="startProduct"></param>
        /// <param name="endProduct"></param>
        /// <param name="workhouseId"></param>
        /// <param name="invoiceXOCusId"></param>
        /// <param name="handBookId"></param>
        /// <returns></returns>
        public IList<Book.Model.ProduceMaterialExitDetail> SelectForListForm(DateTime startDate, DateTime endDate, string startPMEId, string endPMEId, string startPronoteHeaderId, string endPronoteHeaderId, Book.Model.Product startProduct, Book.Model.Product endProduct, string workhouseId, string invoiceXOCusId, string handBookId)
        {
            return accessor.SelectForListForm(startDate, endDate, startPMEId, endPMEId, startPronoteHeaderId, endPronoteHeaderId, startProduct, endProduct, workhouseId, invoiceXOCusId, handBookId);
        }

        public DataTable SelectForExcel(DateTime startDate, DateTime endDate, string startPMEId, string endPMEId, string startPronoteHeaderId, string endPronoteHeaderId, Book.Model.Product startProduct, Book.Model.Product endProduct, string workhouseId, string invoiceXOCusId, string handBookId)
        {
            return accessor.SelectForExcel(startDate, endDate, startPMEId, endPMEId, startPronoteHeaderId, endPronoteHeaderId, startProduct, endProduct, workhouseId, invoiceXOCusId, handBookId);
        }
    }
}

