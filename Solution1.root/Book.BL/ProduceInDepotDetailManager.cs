//------------------------------------------------------------------------------
//
// file name：ProduceInDepotDetailManager.cs
// author: peidun
// create date：2010-1-8 13:43:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Book.BL
{
    /// <summary>
    /// Business logic for dbo.ProduceInDepotDetail.
    /// </summary>
    public partial class ProduceInDepotDetailManager : BaseManager
    {

        /// <summary>
        /// Delete ProduceInDepotDetail by primary key.
        /// </summary>
        public void Delete(string produceInDepotDetailId)
        {
            //
            // todo:add other logic here
            //
            accessor.Delete(produceInDepotDetailId);
        }

        /// <summary>
        /// Insert a ProduceInDepotDetail.
        /// </summary>
        public void Insert(Model.ProduceInDepotDetail produceInDepotDetail)
        {
            //
            // todo:add other logic here
            //
            accessor.Insert(produceInDepotDetail);
        }

        /// <summary>
        /// Update a ProduceInDepotDetail.
        /// </summary>
        public void Update(Model.ProduceInDepotDetail produceInDepotDetail)
        {
            //
            // todo: add other logic here.
            //
            accessor.Update(produceInDepotDetail);
        }

        public IList<Book.Model.ProduceInDepotDetail> Select(Model.ProduceInDepot produceInDepot)
        {
            return accessor.Select(produceInDepot);
        }

        public IList<Book.Model.ProduceInDepotDetail> Select(string startPronoteHeader, string endPronoteHeader, DateTime startDate, DateTime endDate, Model.Product product, Model.WorkHouse work, Model.Depot mDepot, Model.DepotPosition mDepotPosition, string id1, string id2, string cusxoid, Model.Customer customer1, Model.Customer customer2, int ProductState)
        {
            return accessor.Select(startPronoteHeader, endPronoteHeader, startDate, endDate, product, work, mDepot, mDepotPosition, id1, id2, cusxoid, customer1, customer2, ProductState);

        }

        public IList<Book.Model.ProduceInDepotDetail> Select(string PronoteHeaderId, DateTime startDate, DateTime endDate, string workhouseId, Model.Product product, string CusXOId)
        {
            return accessor.Select(PronoteHeaderId, startDate, endDate, workhouseId, product, CusXOId);
        }

        public IList<Book.Model.ProduceInDepotDetail> SelectList(string startPronoteHeader, string endPronoteHeader, DateTime startDate, DateTime endDate, Model.Product product, Model.WorkHouse work, Model.Depot mDepot, Model.DepotPosition mDepotPosition, string id1, string id2, string cusxoid, Model.Customer customer1, Model.Customer customer2, int ProductState)
        {
            return accessor.SelectList(startPronoteHeader, endPronoteHeader, startDate, endDate, product, work, mDepot, mDepotPosition, id1, id2, cusxoid, customer1, customer2, ProductState);

        }

        /// <summary>
        ///  根据加工单生产站 求 合计生产数量
        /// </summary>
        /// <param name="PronoteHeaderId"></param>
        /// <param name="WorkHouseId"></param>
        /// <returns></returns>
        public double? select_SumbyPronHeaderId(string PronoteHeaderId, string WorkHouseId, string ProductId)
        {
            return accessor.select_SumbyPronHeaderId(PronoteHeaderId, WorkHouseId, ProductId);
        }

        /// <summary>
        /// 根据加工单生产站 求 合计合格生产数量
        /// </summary>
        /// <param name="PronoteHeaderId"></param>
        /// <param name="WorkHouseId"></param>
        /// <returns></returns>
        public double? select_CheckOutSumByPronHeaderId(string PronoteHeaderId, string WorkHouseId, string ProductId)
        {
            return accessor.select_CheckOutSumByPronHeaderId(PronoteHeaderId, WorkHouseId, ProductId);
        }

        /// <summary>
        /// 根据日前区间加工单生产站 求 生产数量和
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="PronoteHeaderId"></param>
        /// <param name="WorkHouseId"></param>
        /// <returns></returns>
        public double? select_CheckOutSumPronoteHeaderWorkhouseDateRang(DateTime startdate, DateTime enddate, string PronoteHeaderId, string WorkHouseId)
        {
            return accessor.select_CheckOutSumPronoteHeaderWorkhouseDateRang(startdate, enddate, PronoteHeaderId, WorkHouseId);
        }

        /// <summary>
        /// 根据日前区间加工单生产站 求 合格数量和
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="PronoteHeaderId"></param>
        /// <param name="WorkHouseId"></param>
        /// <returns></returns>
        public double? select_SumPronoteHeaderWorkhouseDateRang(DateTime startdate, DateTime enddate, string PronoteHeaderId, string WorkHouseId)
        {
            return accessor.select_SumPronoteHeaderWorkhouseDateRang(startdate, enddate, PronoteHeaderId, WorkHouseId);
        }

        public double? select_TransferSumyPronHeaderWorkHouse(string PronoteHeaderId, string WorkHouseId, DateTime? dt)
        {
            return accessor.select_TransferSumyPronHeaderWorkHouse(PronoteHeaderId, WorkHouseId, dt);
        }

        public IList<Model.ProduceInDepotDetail> Select_ChooseDefectRateCls(DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Model.Product StartProduct, Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Model.WorkHouse StartWorkHouse, Model.WorkHouse EndWorkHouse, Model.Customer StartCustomer, Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV)
        {
            return accessor.Select_ChooseDefectRateCls(StartDate, EndDate, StartProduceInDepotId, EndProduceInDepotId, StartProduct, EndProduct, StartPronoteHeaderId, EndPronoteHeaderId, StartWorkHouse, EndWorkHouse, StartCustomer, EndCustomer, attrJiLuFangShi, attrQiangHua, attrWuDu, attrWuQiangHuaWuDu, attrProductStates, RejectionRate, RejectionRateCompare, EnableBLV);
        }

        //生产不良率统计
        public DataTable DTSelect_ChooseDefectRateCls(int DateType, DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Model.Product StartProduct, Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Model.WorkHouse StartWorkHouse, Model.WorkHouse EndWorkHouse, Model.Customer StartCustomer, Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType, string StarInvoiceXOId, string EndInvoiceXOId, int InvoiceStates)
        {
            return accessor.DTSelect_ChooseDefectRateCls(DateType, StartDate, EndDate, StartProduceInDepotId, EndProduceInDepotId, StartProduct, EndProduct, StartPronoteHeaderId, EndPronoteHeaderId, StartWorkHouse, EndWorkHouse, StartCustomer, EndCustomer, attrJiLuFangShi, attrQiangHua, attrWuDu, attrWuQiangHuaWuDu, attrProductStates, RejectionRate, RejectionRateCompare, EnableBLV, attrOrderColumn, attrOrderType, StarInvoiceXOId, EndInvoiceXOId, InvoiceStates);
        }

        //生产不良率统计，按照部门进行生产数量、合格数量、不良率统计
        public DataTable SUMDTSelect_ChooseDefectRateCls(int DateType, DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Model.Product StartProduct, Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Model.WorkHouse StartWorkHouse, Model.WorkHouse EndWorkHouse, Model.Customer StartCustomer, Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType, string StarInvoiceXOId, string EndInvoiceXOId, int InvoiceStates)
        {
            return accessor.SUMDTSelect_ChooseDefectRateCls(DateType, StartDate, EndDate, StartProduceInDepotId, EndProduceInDepotId, StartProduct, EndProduct, StartPronoteHeaderId, EndPronoteHeaderId, StartWorkHouse, EndWorkHouse, StartCustomer, EndCustomer, attrJiLuFangShi, attrQiangHua, attrWuDu, attrWuQiangHuaWuDu, attrProductStates, RejectionRate, RejectionRateCompare, EnableBLV, attrOrderColumn, attrOrderType, StarInvoiceXOId, EndInvoiceXOId, InvoiceStates);
        }

        //商品不良率统计(2012年12月10日16:06:48没有调用过)
        public DataTable PTSelect_ChooseDefectRateCls(DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Model.Product StartProduct, Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Model.WorkHouse StartWorkHouse, Model.WorkHouse EndWorkHouse, Model.Customer StartCustomer, Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType)
        {
            return accessor.PTSelect_ChooseDefectRateCls(StartDate, EndDate, StartProduceInDepotId, EndProduceInDepotId, StartProduct, EndProduct, StartPronoteHeaderId, EndPronoteHeaderId, StartWorkHouse, EndWorkHouse, StartCustomer, EndCustomer, attrJiLuFangShi, attrQiangHua, attrWuDu, attrWuQiangHuaWuDu, attrProductStates, RejectionRate, RejectionRateCompare, EnableBLV, attrOrderColumn, attrOrderType);
        }

        public IList<Model.ProduceInDepotDetail> SelectHejiSumByPronoteHeader(string PronoteHeaderIds)
        {
            return accessor.SelectHejiSumByPronoteHeader(PronoteHeaderIds);
        }

        //查询价格区间
        public string GetSupplierProductPriceRange(string productId, string WorkHouseName)
        {
            return accessor.GetSupplierProductPriceRange(productId, WorkHouseName);
        }

        //public IList<Model.ProduceInDepotDetail> SelectSceneQuantity(string productid, DateTime dateTime, string workHouseId, string pronoteHeaderId)
        //{
        //    return accessor.SelectSceneQuantity(productid, dateTime, workHouseId, pronoteHeaderId);
        //}

        /// <summary>
        /// 根据下个生产站查询商品入库详细
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="dateTime"></param>
        /// <param name="workHouseId"></param>
        /// <param name="pronoteHeaderIds"></param>
        /// <returns></returns>
        public Model.ProduceInDepotDetail SelectByNextWorkhouse(string productid, DateTime dateTime, string workHouseId, string pronoteHeaderIds)
        {
            return accessor.SelectByNextWorkhouse(productid, dateTime, workHouseId, pronoteHeaderIds);
        }

        /// <summary>
        /// 根据本次生产站查询商品入库详细
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="dateTime"></param>
        /// <param name="workHouseId"></param>
        /// <param name="pronoteHeaderIds"></param>
        /// <returns></returns>
        public Model.ProduceInDepotDetail SelectByThisWorkhouse(string productid, DateTime dateTime, string workHouseId, string pronoteHeaderIds)
        {
            return accessor.SelectByThisWorkhouse(productid, dateTime, workHouseId, pronoteHeaderIds);
        }
    }
}

