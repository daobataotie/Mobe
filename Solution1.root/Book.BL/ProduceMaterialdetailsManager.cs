﻿//------------------------------------------------------------------------------
//
// file name：ProduceMaterialdetailsManager.cs
// author: peidun
// create date：2009-12-30 16:33:30
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceMaterialdetails.
    /// </summary>
    public partial class ProduceMaterialdetailsManager
    {

        /// <summary>
        /// Delete ProduceMaterialdetails by primary key.
        /// </summary>
        public void Delete(string produceMaterialdetailsID)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceMaterialdetailsID);
        }

        /// <summary>
        /// Insert a ProduceMaterialdetails.
        /// </summary>
        public void Insert(Model.ProduceMaterialdetails produceMaterialdetails)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(produceMaterialdetails);
        }

        /// <summary>
        /// Update a ProduceMaterialdetails.
        /// </summary>
        public void Update(Model.ProduceMaterialdetails produceMaterialdetails)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(produceMaterialdetails);
        }
        public IList<Book.Model.ProduceMaterialdetails> Select(Model.ProduceMaterial produceMaterial)
        {
            return accessor.Select(produceMaterial);
        }
        public IList<Book.Model.ProduceMaterialdetails> Select(string houseid, DateTime startDate, DateTime endDate)
        {
            return accessor.Select(houseid, startDate, endDate);
        }

        public IList<Model.ProduceMaterialdetails> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, string pId0, string pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            return accessor.SelectBycondition(starDate, endDate, produceMaterialId0, produceMaterialId1, pId0, pId1, departmentId0, departmentId1, PronoteHeaderId0, PronoteHeaderId1);
        }
        public IList<Book.Model.ProduceMaterialdetails> SelectByState(Model.ProduceMaterial produceMaterial)
        {
            return accessor.SelectByState(produceMaterial);
        }

        public Model.ProduceMaterialdetails SelectByProductIdAndHeadId(string productId, string produceMaterialId)
        {
            return accessor.SelectByProductIdAndHeadId(productId, produceMaterialId);
        }

        public IList<Model.ProduceMaterialdetails> SelectByProductIdAndHeadId(Model.Product pId0, Model.Product pId1, string produceMaterialId)
        {
            return accessor.SelectByProductIdAndHeadId(pId0, pId1, produceMaterialId);
        }

        public double GetMaterialprocesedsumForPDMId(string PDMid)
        {
            return accessor.GetMaterialprocesedsumForPDMId(PDMid);
        }

        public IList<Model.ProduceMaterialdetails> SelectTotalByProduceMaterialID(Model.Product pId0, Model.Product pId1, string str)
        {
            return accessor.SelectTotalByProduceMaterialID(pId0, pId1, str);
        }


        public IList<Model.ProduceMaterialdetails> SelectBycondition2(DateTime startDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, Book.Model.Product pId0, Book.Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1, string CusInvoiceXOId, string handBookId)
        {
            return accessor.SelectBycondition2(startDate, endDate, produceMaterialId0, produceMaterialId1, pId0, pId1, departmentId0, departmentId1, PronoteHeaderId0, PronoteHeaderId1, CusInvoiceXOId, handBookId);
        }

        public double SelectMaterialQty(string productid, DateTime dateEnd, string workHouseId, string invoiceXOIds)
        {
            return accessor.SelectMaterialQty(productid, dateEnd, workHouseId, invoiceXOIds);
        }

        public DataTable SelectMaterialQty(string productid, DateTime dateStart, DateTime dateEnd, string workHouseId, string invoiceXOIds)
        {
            return accessor.SelectMaterialQty(productid, dateStart, dateEnd, workHouseId, invoiceXOIds);
        }

        /// <summary>
        /// 筛选条件增加“手册号”
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="workHouseId"></param>
        /// <param name="invoiceXOIds"></param>
        /// <param name="handBookId"></param>
        /// <returns></returns>
        public DataTable SelectMaterialQty(string productid, DateTime dateStart, DateTime dateEnd, string workHouseId, string invoiceXOIds, string handBookId)
        {
            return accessor.SelectMaterialQty(productid, dateStart, dateEnd, workHouseId, invoiceXOIds, handBookId);
        }

        public double SelectMaterialQtyAll(string productid, DateTime dateEnd, string workHouseId)
        {
            return accessor.SelectMaterialQtyAll(productid, dateEnd, workHouseId);
        }

        public IList<Model.ProduceMaterialdetails> SelectMaterialsByProductIds(string productids, DateTime dateStart, DateTime dateEnd, string workHouseId, string invoiceXOIds)
        {
            return accessor.SelectMaterialsByProductIds(productids, dateStart, dateEnd, workHouseId, invoiceXOIds);
        }

        public IList<Model.ProduceMaterialdetails> GetDataByPronoteHeaders(string pronoteHeaderIds)
        {
            return accessor.GetDataByPronoteHeaders(pronoteHeaderIds);
        }

        public string SelectPNTOrMRSId(string produceMaterialdetailsID)
        {
            return accessor.SelectPNTOrMRSId(produceMaterialdetailsID);
        }
    }
}

