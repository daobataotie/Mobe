//------------------------------------------------------------------------------
//
// file name：IProduceInDepotDetailAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Book.DA
{
    /// <summary>
    /// Interface of data accessor of dbo.ProduceInDepotDetail
    /// </summary>
    public partial interface IProduceInDepotDetailAccessor : IAccessor
    {
        IList<Book.Model.ProduceInDepotDetail> Select(Model.ProduceInDepot produceInDepot);
        IList<Book.Model.ProduceInDepotDetail> Select(string startPronoteHeader, string endPronoteHeader, DateTime startDate, DateTime endDate, Model.Product product, Model.WorkHouse work, Model.Depot mDepot, Model.DepotPosition mDepotPositioin, string id1, string id2, string cusxoid, Model.Customer customer1, Model.Customer customer2, int ProductState);
        IList<Book.Model.ProduceInDepotDetail> SelectList(string startPronoteHeader, string endPronoteHeader, DateTime startDate, DateTime endDate, Model.Product product, Model.WorkHouse work, Model.Depot mDepot, Model.DepotPosition mDepotPositioin, string id1, string id2, string cusxoid, Model.Customer customer1, Model.Customer customer2, int ProductState);

        IList<Book.Model.ProduceInDepotDetail> Select(string PronoteHeaderId, DateTime startDate, DateTime endDate, string workhouseId, Model.Product product, string CusXOId);

        double? select_CheckOutSumPronoteHeaderWorkhouseDateRang(DateTime startdate, DateTime enddate, string PronoteHeaderId, string WorkHouseId);
        double? select_SumPronoteHeaderWorkhouseDateRang(DateTime startdate, DateTime enddate, string PronoteHeaderId, string WorkHouseId);
        double? select_TransferSumyPronHeaderWorkHouse(string PronoteHeaderId, string WorkHouseId, DateTime? dt);
        void DeleteByHeader(Model.ProduceInDepot produceInDepot);
        IList<Book.Model.ProduceInDepotDetail> select_NextbyPronHeaderId(string PronoteHeaderId, string WorkHouseId, string productid, DateTime insertTime);

        IList<Model.ProduceInDepotDetail> Select_ChooseDefectRateCls(DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Model.Product StartProduct, Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Model.WorkHouse StartWorkHouse, Model.WorkHouse EndWorkHouse, Model.Customer StartCustomer, Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV);

        IList<Model.ProduceInDepotDetail> SelectHejiSumByPronoteHeader(string PronoteHeaderIds);

        IList<Model.ProduceInDepotDetail> Select_ByWorkHosueAndPronoteId(string WorkHouseid, string PronoteHeaderId);

        double? select_SumbyPronHeaderId(string PronoteHeaderId, string WorkHouseId, string ProductId);

        double? select_CheckOutSumByPronHeaderId(string PronoteHeaderId, string WorkHouseId, string ProductId);

        double? select_FrontSumByProduceIndepotIdAndProId(string ProduceIndepotId, string ProductId, int Inumber);

        double? select_FrontCheckoutSumByProduceIndepotIdAndProId(string ProduceIndepotId, string ProductId, int Inumber);

        double? Get_HJForColumnName(string PronoteHeaderId, string WorkHouseId, string ProductId, string ProduceIndepotId, int Inumber, DateTime InsertTime, string GetColumn);

        DataTable DTSelect_ChooseDefectRateCls(int DateType, DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Model.Product StartProduct, Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Model.WorkHouse StartWorkHouse, Model.WorkHouse EndWorkHouse, Model.Customer StartCustomer, Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType, string StarInvoiceXOId, string EndInvoiceXOId, int InvoiceStates);

        DataTable SUMDTSelect_ChooseDefectRateCls(int DateType, DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Model.Product StartProduct, Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Model.WorkHouse StartWorkHouse, Model.WorkHouse EndWorkHouse, Model.Customer StartCustomer, Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType, string StarInvoiceXOId, string EndInvoiceXOId, int InvoiceStates);

        DataTable PTSelect_ChooseDefectRateCls(DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Model.Product StartProduct, Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Model.WorkHouse StartWorkHouse, Model.WorkHouse EndWorkHouse, Model.Customer StartCustomer, Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType);

        string GetSupplierProductPriceRange(string productId, string WorkHouseName);

        Model.ProduceInDepotDetail SelectByNextWorkhouse(string productid, DateTime dateTime, string workHouseId, string pronoteHeaderIds);

        Model.ProduceInDepotDetail SelectByThisWorkhouse(string productid, DateTime dateTime, string workHouseId, string pronoteHeaderIds);

        IList<Model.ProduceInDepotDetail> SelectIndepotQty(string productids, DateTime dateTime, string workHouseId, string invoiceXOIds);

        IList<Model.Product> SelectAllByDateRange(DateTime dateStart, DateTime dateEnd);

        IList<Model.ProduceInDepotDetail> SelectShechuByDateRange(DateTime dateStart, DateTime dateEnd);

        IList<Model.ProduceInDepotDetail> SelectYanpianByDateRange(DateTime dateStart, DateTime dateEnd);
    }
}

