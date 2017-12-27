//------------------------------------------------------------------------------
//
// file name：ProduceInDepotDetailAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of ProduceInDepotDetail
    /// </summary>
    public partial class ProduceInDepotDetailAccessor : EntityAccessor, IProduceInDepotDetailAccessor
    {
        public IList<Book.Model.ProduceInDepotDetail> Select(Model.ProduceInDepot produceInDepot)
        {
            return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.select_byProduceInDepotId", produceInDepot.ProduceInDepotId);
        }

        public IList<Book.Model.ProduceInDepotDetail> Select(string PronoteHeaderId, DateTime startDate, DateTime endDate, string workhouseId, Model.Product product, string CusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pronoteid", PronoteHeaderId);
            ht.Add("startdate", startDate);
            ht.Add("enddate", endDate);
            ht.Add("workhouseId", workhouseId);
            ht.Add("productid", product == null ? null : product.ProductId);
            ht.Add("cusxoid", CusXOId == null ? null : CusXOId);
            return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.selectByDateRange", ht);
        }

        //ProductState -1全部 0 正常 1非   //用于listForm
        public IList<Book.Model.ProduceInDepotDetail> SelectList(string startPronoteHeader, string endPronoteHeader, DateTime startDate, DateTime endDate, Book.Model.Product product, Book.Model.WorkHouse work, Book.Model.Depot mDepot, Book.Model.DepotPosition mDepotPositioin, string id1, string id2, string cusxoid, Book.Model.Customer customer1, Book.Model.Customer customer2, int ProductState)
        {
            //Hashtable ht = new Hashtable();
            //ht.Add("startpronoteid", string.IsNullOrEmpty(startPronoteHeader) ? null : startPronoteHeader);
            //ht.Add("endpronoteid", string.IsNullOrEmpty(endPronoteHeader) ? null : endPronoteHeader);
            //ht.Add("startdate", startDate);
            //ht.Add("enddate", endDate);
            //ht.Add("productid", product == null ? null : product.ProductId);
            StringBuilder str = new StringBuilder();
            str.Append("   select d.*,i.ProduceInDepotDate as mProduceInDepotDate,pr.IsClose as PIsClose , p.ProductName as ProductName,(SELECT Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId = (SELECT ProduceInDepot.WorkHouseId FROM ProduceInDepot WHERE ProduceInDepot.ProduceInDepotId = d.ProduceInDepotId)) as WorkHousename,pr.InvoiceCusId AS CusXOId,(SELECT LEFT(a,isnull(len(a)-1,0)) AS c FROM (SELECT (SELECT (cast(PronoteMachineId AS varchar)+',')  FROM PronoteProceduresDetail WHERE PronoteHeaderID=d.PronoteHeaderId AND PronoteMachineId IS NOT NULL AND PronoteMachineId<>'' FOR xml path('')) AS a) AS b) AS Machine,pr.DetailsSum as SCSL from ProduceInDepotDetail d left join PronoteHeader pr on pr.PronoteHeaderId=d.PronoteHeaderId  left join ProduceInDepot i on d.ProduceInDepotId=i.ProduceInDepotId left join  product p on d.productid=p.productid where d.ProduceInDepotId in(select ProduceInDepotId from ProduceInDepot where ProduceInDepotDate between '" + startDate.ToShortDateString() + "' and '" + endDate.Date.AddDays(1).ToShortDateString() + "') ");
            if (!string.IsNullOrEmpty(startPronoteHeader) && !string.IsNullOrEmpty(endPronoteHeader))
            {
                str.Append(" and (d.PronoteHeaderId between '" + startPronoteHeader + "' and  '" + endPronoteHeader + "')");
            }
            if (product != null)
            {
                str.Append(" and d.productid='" + product.ProductId + "'");
            }
            if (work != null)
            {
                str.Append(" and d.ProduceInDepotId in(select ProduceInDepotId from ProduceInDepot where WorkHouseId ='" + work.WorkHouseId + "') ");
            }
            if (mDepotPositioin != null)
            {
                str.Append(" and d.DepotPositionId = '" + mDepotPositioin.DepotPositionId + "'");
            }
            if (mDepot != null)
            {
                str.Append(" and d.DepotPositionId IN (SELECT DepotPosition.DepotPositionId FROM DepotPosition WHERE DepotId = '" + mDepot.DepotId + "')");
            }
            if (!string.IsNullOrEmpty(id1) && !string.IsNullOrEmpty(id2))
            {
                str.Append(" and d.ProduceInDepotId between '" + id1 + "' and '" + id2 + "' ");
            }
            if (!string.IsNullOrEmpty(cusxoid))
            {
                str.Append(" and (select CustomerInvoiceXOId from InvoiceXO where InvoiceId=(select InvoiceXOId from PronoteHeader where PronoteHeaderID=pr.PronoteHeaderId))='" + cusxoid + "'");
            }
            if (customer1 != null && customer2 != null)
            {
                str.Append(" and d.PronoteHeaderId in(select PronoteHeaderId from PronoteHeader where InvoiceXOId IN(SELECT invoiceid FROM InvoiceXO WHERE CustomerId IN(SELECT CustomerId FROM Customer WHERE Id BETWEEN  '" + customer1.Id + "' AND '" + customer2.Id + "')))");
            }
            switch (ProductState)
            {
                case 0:
                    str.Append(" AND d.ProductId IN (SELECT Product.ProductId FROM Product WHERE ProductType = '0')");
                    break;
                case 1:
                    str.Append(" AND d.ProductId IN (SELECT Product.ProductId FROM Product WHERE ProductType = '1')");
                    break;
            }
            str.Append(" order by mProduceInDepotDate,ProduceInDepotId");
            // ht.Add("sql", str.ToString());
            // return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.select_byProduceInDateAndPronote", ht);
            return this.DataReaderBind<Model.ProduceInDepotDetail>(str.ToString(), null, CommandType.Text);
        }

        //ProductState -1全部 0 正常 1非
        public IList<Book.Model.ProduceInDepotDetail> Select(string startPronoteHeader, string endPronoteHeader, DateTime startDate, DateTime endDate, Book.Model.Product product, Book.Model.WorkHouse work, Book.Model.Depot mDepot, Book.Model.DepotPosition mDepotPositioin, string id1, string id2, string cusxoid, Book.Model.Customer customer1, Book.Model.Customer customer2, int ProductState)
        {
            //Hashtable ht = new Hashtable();
            //ht.Add("startpronoteid", string.IsNullOrEmpty(startPronoteHeader) ? null : startPronoteHeader);
            //ht.Add("endpronoteid", string.IsNullOrEmpty(endPronoteHeader) ? null : endPronoteHeader);
            //ht.Add("startdate", startDate);
            //ht.Add("enddate", endDate);
            //ht.Add("productid", product == null ? null : product.ProductId);
            //StringBuilder str = new StringBuilder();
            //if (work != null)
            //{
            //    str.Append(" and ProduceInDepotId in(select ProduceInDepotId from ProduceInDepot where WorkHouseId ='" + work.WorkHouseId + "') ");
            //}
            //if (mDepotPositioin != null)
            //{
            //    str.Append(" and DepotPositionId = '" + mDepotPositioin.DepotPositionId + "'");
            //}
            //if (mDepot != null)
            //{
            //    str.Append(" and DepotPositionId IN (SELECT DepotPositionId FROM DepotPosition WHERE DepotId = '" + mDepot.DepotId + "')");
            //}
            //if (!string.IsNullOrEmpty(id1) && !string.IsNullOrEmpty(id2))
            //{
            //    str.Append(" and ProduceInDepotId between '" + id1 + "' and '" + id2 + "' ");
            //}
            //if (!string.IsNullOrEmpty(cusxoid))
            //{
            //    str.Append(" and PronoteHeaderId in(select PronoteHeaderId from PronoteHeader where InvoiceXOId in (select invoiceId from InvoiceXO where CustomerInvoiceXOId= '" + cusxoid + "'))");
            //}
            //if (customer1 != null && customer2 != null)
            //{
            //    str.Append(" and PronoteHeaderId in(select PronoteHeaderId from PronoteHeader where InvoiceXOId IN(SELECT invoiceid FROM InvoiceXO WHERE CustomerId IN(SELECT CustomerId FROM Customer WHERE Id BETWEEN  '" + customer1.Id + "' AND '" + customer2.Id + "')))");
            //}
            //switch (ProductState)
            //{
            //    case 0:
            //        str.Append(" AND ProductId IN (SELECT Product.ProductId FROM Product WHERE ProductType = '0')");
            //        break;
            //    case 1:
            //        str.Append(" AND ProductId IN (SELECT Product.ProductId FROM Product WHERE ProductType = '1')");
            //        break;
            //}
            //ht.Add("sql", str.ToString());
            //return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.select_byProduceInDateAndPronote", ht);

            StringBuilder sb = new StringBuilder("select pid.ProduceInDepotId,pi.ProduceInDepotDate as HeaderDate,pid.PronoteHeaderId,p.ProductName,wh.Workhousename,pid.ProceduresSum,pid.ProduceTransferQuantity,pid.ProduceQuantity,pid.ProductUnit,d.DepotName, (select CustomerInvoiceXOId from InvoiceXO where InvoiceId=(select InvoiceXOId from PronoteHeader where PronoteHeaderID=pid.PronoteHeaderId)) as CusXOId  from ProduceInDepotDetail pid left join Product p on p.ProductId=pid.ProductId left join ProduceInDepot pi on pi.ProduceInDepotId=pid.ProduceInDepotId  left join WorkHouse wh on wh.WorkHouseId=pi.WorkHouseId left join Depot d on d.DepotId=pi.DepotId where pi.ProduceInDepotDate between '" + startDate + "' and '" + endDate + "'");

            if (!string.IsNullOrEmpty(startPronoteHeader) && !string.IsNullOrEmpty(endPronoteHeader))
                sb.Append(" and pid.PronoteHeaderId between '" + startPronoteHeader + "' and '" + endPronoteHeader + "'");
            if (product != null)
                sb.Append("  and pid.ProductId='" + product.ProductId + "'");
            if (work != null)
                sb.Append(" and pi.WorkHouseId='" + work.WorkHouseId + "'");
            if (mDepot != null)
                sb.Append(" and pi.DepotId='" + mDepot.DepotId + "'");
            if (mDepotPositioin != null)
                sb.Append(" and pid.DepotPositionId='" + mDepotPositioin.DepotPositionId + "'");
            if (!string.IsNullOrEmpty(id1) && !string.IsNullOrEmpty(id2))
                sb.Append(" and pid.ProduceInDepotId between '" + id1 + "' and '" + id2 + "'");
            if (!string.IsNullOrEmpty(cusxoid))
                sb.Append(" and pid.PronoteHeaderId in(select PronoteHeaderId from PronoteHeader where InvoiceXOId in (select invoiceId from InvoiceXO where CustomerInvoiceXOId= '" + cusxoid + "'))");
            if (customer1 != null && customer2 != null)
                sb.Append(" and pid.PronoteHeaderId in (select PronoteHeaderId from PronoteHeader where InvoiceXOId IN(SELECT invoiceid FROM InvoiceXO WHERE CustomerId IN(SELECT CustomerId FROM Customer WHERE Id BETWEEN  '" + customer1.Id + "' AND '" + customer2.Id + "')))");
            switch (ProductState)
            {
                case 0:
                    sb.Append(" AND pid.ProductId IN (SELECT ProductId FROM Product WHERE ProductType = '0')");
                    break;
                case 1:
                    sb.Append(" AND pid.ProductId IN (SELECT ProductId FROM Product WHERE ProductType = '1')");
                    break;
            }
            sb.Append(" order by pid.ProduceInDepotId desc");

            return this.DataReaderBind<Model.ProduceInDepotDetail>(sb.ToString(), null, CommandType.Text);
        }

        public double? select_SumPronoteHeaderWorkhouseDateRang(DateTime startdate, DateTime enddate, string PronoteHeaderId, string WorkHouseId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            ht.Add("PronoteHeaderId", PronoteHeaderId);
            ht.Add("WorkHouseId", WorkHouseId);

            return sqlmapper.QueryForObject<double>("ProduceInDepotDetail.select_pronoheaWorkhouseDateRang", ht);
        }

        public double? select_CheckOutSumPronoteHeaderWorkhouseDateRang(DateTime startdate, DateTime enddate, string PronoteHeaderId, string WorkHouseId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            ht.Add("PronoteHeaderId", PronoteHeaderId);
            ht.Add("WorkHouseId", WorkHouseId);

            return sqlmapper.QueryForObject<double>("ProduceInDepotDetail.select_checkOutSumpronoheaWorkhouseDateRang", ht);
        }

        //查询 前生产站转入数量
        public double? select_TransferSumyPronHeaderWorkHouse(string PronoteHeaderId, string WorkHouseId, DateTime? dt)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderId", PronoteHeaderId);
            ht.Add("WorkHouseId", WorkHouseId);
            if (dt != null)
                ht.Add("sql", "and ProduceInDepotId in (select ProduceInDepotId from ProduceInDepot where Year(ProduceInDepotDate)='" + dt.Value.Year + "' and MONTH(ProduceInDepotDate)='" + dt.Value.Month + "' and Day(ProduceInDepotDate)='" + dt.Value.Day + "')");

            return sqlmapper.QueryForObject<double>("ProduceInDepotDetail.select_TransferSumyPronHeaderWorkHouse", ht);
        }

        public void DeleteByHeader(Model.ProduceInDepot produceInDepot)
        {
            if (produceInDepot != null && !string.IsNullOrEmpty(produceInDepot.ProduceInDepotId))
                sqlmapper.Delete("ProduceInDepotDetail.delete_byheader", produceInDepot.ProduceInDepotId);
        }

        //生产不良率统计表, 返回list
        public IList<Book.Model.ProduceInDepotDetail> Select_ChooseDefectRateCls(DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Book.Model.Product StartProduct, Book.Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Book.Model.WorkHouse StartWorkHouse, Book.Model.WorkHouse EndWorkHouse, Book.Model.Customer StartCustomer, Book.Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" AND ProduceInDepotId IN (SELECT ProduceInDepot.ProduceInDepotId FROM ProduceInDepot WHERE ProduceInDepotDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
            if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
            {
                if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                {
                    if (StartProduceInDepotId != EndProduceInDepotId)
                    {
                        sb.Append(" AND ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                    }
                }
                else
                {
                    sb.Append(" AND ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                }
            }

            if (!(StartProduct == null && EndProduct == null))
            {
                if (StartProduct != null && EndProduct != null)
                {
                    sb.Append(" AND ProductId IN (SELECT ProductId FROM Product WHERE ProductName BETWEEN '" + StartProduct.ProductName + "' AND '" + EndProduct.ProductName + "')");
                }
                else
                {
                    sb.Append(" AND ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                }
            }

            if (!(string.IsNullOrEmpty(StartPronoteHeaderId) && string.IsNullOrEmpty(EndPronoteHeaderId)))
            {
                if (!string.IsNullOrEmpty(StartPronoteHeaderId) && !string.IsNullOrEmpty(EndPronoteHeaderId))
                {
                    if (StartProduceInDepotId != EndProduceInDepotId)
                    {
                        sb.Append(" AND PronoteHeaderId BETWEEN '" + StartPronoteHeaderId + "' AND '" + EndPronoteHeaderId + "'");
                    }
                }
                else
                {
                    sb.Append(" AND PronoteHeaderId = '" + (string.IsNullOrEmpty(StartPronoteHeaderId) ? EndPronoteHeaderId : StartPronoteHeaderId) + "'");
                }
            }

            if (!(StartWorkHouse == null && EndWorkHouse == null))
            {
                if (StartWorkHouse != null && EndWorkHouse != null)
                {
                    sb.Append(" AND ProduceInDepotId IN (SELECT ProduceInDepot.ProduceInDepotId FROM ProduceInDepot WHERE ProduceInDepot.WorkHouseId BETWEEN '" + StartWorkHouse.WorkHouseId + "' AND '" + EndWorkHouse.WorkHouseId + "')");
                }
                else
                {
                    sb.Append(" AND ProduceInDepotId IN (SELECT ProduceInDepot.ProduceInDepotId FROM ProduceInDepot WHERE ProduceInDepot.WorkHouseId = '" + (StartWorkHouse == null ? EndWorkHouse.WorkHouseId : StartWorkHouse.WorkHouseId) + "')");
                }
            }

            if (!(StartCustomer == null && EndCustomer == null))
            {
                if (StartCustomer != null && EndCustomer != null)
                {
                    sb.Append(" AND PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId BETWEEN '" + StartCustomer.CustomerId + "' AND '" + EndCustomer.CustomerId + "'))");
                }
                else
                {
                    sb.Append(" AND PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId ='" + (StartCustomer == null ? EndCustomer.CustomerId : StartCustomer.CustomerId) + "'))");
                }
            }

            if (attrQiangHua || attrWuDu || attrWuQiangHuaWuDu || (attrProductStates != 0))
            {
                sb.Append(" AND ProductId IN (SELECT Product.ProductId FROM Product WHERE 1=1 ");
                if (attrQiangHua)
                    sb.Append(" AND IsQiangHua = 1");
                if (attrWuDu)
                    sb.Append(" AND IsFangWu = 1");
                if (attrWuQiangHuaWuDu)
                    sb.Append(" AND IsNoQiangFang = 1");

                if (attrProductStates == 1)
                    sb.Append(" AND ProductType = '0'");
                else
                    sb.Append(" AND ProductType = '1'");
                sb.Append(")");
            }

            if (EnableBLV)
            {
                sb.Append(" AND RejectionRate " + RejectionRateCompare + " " + RejectionRate.ToString());
            }



            return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.Select_ChooseDefectRateCls", sb.ToString());
        }

        //生产不良率统计表，DataTable获取数据
        DataTable IProduceInDepotDetailAccessor.DTSelect_ChooseDefectRateCls(int DateType, DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Book.Model.Product StartProduct, Book.Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Book.Model.WorkHouse StartWorkHouse, Book.Model.WorkHouse EndWorkHouse, Book.Model.Customer StartCustomer, Book.Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType, string StarInvoiceXOId, string EndInvoiceXOId, int InvoiceStates)
        {
            StringBuilder sbMain = new StringBuilder();
            StringBuilder sbContion = new StringBuilder();

            //条件筛选
            if (attrJiLuFangShi)
            {
                #region 展开
                sbMain.Append("SELECT Substring(CONVERT(varchar,h.ProduceInDepotDate,120),0,11) AS ProduceInDepotDate,h.WorkHouseId,");
                sbMain.Append("CASE d.ProceduresSum WHEN 0 THEN '0%' ELSE RTrim(CONVERT(varchar,round(1 - isnull(d.CheckOutSum,0)/isnull(d.ProceduresSum,1),4)*100))+'%' END AS RejectionRate_1,");
                sbMain.Append("w.Workhousename,CASE p.ProductType WHEN 0 THEN '常態' WHEN 1 THEN '特殊' ELSE '' END AS ProductType,");
                sbMain.Append("CASE p.IsQiangHua WHEN 1 THEN '強化' ELSE (CASE p.IsFangWu WHEN 1 THEN '防霧' ELSE (CASE p.IsNoQiangFang WHEN 1 THEN '無強化防霧' ELSE '' END) END ) END AS ProductWDQHua,");
                sbMain.Append("p.ProductName,d.ProductUnit AS ProductUnit,isnull(ProceduresSum,0) AS ProceduresSum,");
                sbMain.Append("isnull(CheckOutSum,0) AS CheckOutSum,isnull(mYuanliaowenti,0) AS mYuanliaowenti,isnull(mChouliaowenti,0) AS mChouliaowenti,");
                sbMain.Append("isnull(mPaoguanwenti,0) AS mPaoguanwenti,isnull(mJingdiangudingdian,0) AS mJingdiangudingdian,isnull(mChapiancashang,0) AS mChapiancashang,");
                sbMain.Append("isnull(mWanMocashang,0) AS mWanMocashang,isnull(mGuaiShouZhuangShang,0) AS mGuaiShouZhuangShang,isnull(mHuabancashang,0) AS mHuabancashang,");
                sbMain.Append("isnull(mGuohuojizhua,0) AS mGuohuojizhua,isnull(mBaiyanHeiYan,0) AS mBaiyanHeiYan,isnull(mJieHeXianHuiwen,0) AS mJieHeXianHuiwen,");
                sbMain.Append("isnull(mSuoShui,0) AS mSuoShui,isnull(mQiPao,0) AS mQiPao,isnull(mShechuqita,0) AS mShechuqita,");
                sbMain.Append("isnull(mCaMoSunHua,0) AS mCaMoSunHua,isnull(mChaipiancashang,0) AS mChaipiancashang,");
                sbMain.Append("isnull(mHeidianzazhi,0) AS mHeidianzazhi,isnull(mQianghuaqiancashang,0) as mQianghuaqiancashang,isnull(mQianghuahoucashang,0) AS mQianghuahoucashang,isnull(mHanyao,0) AS mHanyao,");
                sbMain.Append("isnull(mKeLimianxu,0) AS mKeLimianxu,isnull(mLiuheng,0) AS mLiuheng,isnull(mPengYaodiyao,0) AS mPengYaodiyao,");
                sbMain.Append("isnull(mQianghuafangwuxian,0) AS mQianghuafangwuxian,isnull(mYoudian,0) AS mYoudian,isnull(mQianghuaQiTa,0) AS mQianghuaQiTa,");
                sbMain.Append("isnull(mChangshangbuliang,0) AS mChangshangbuliang,isnull(mZuzhuangcashang,0) AS mZuzhuangcashang,isnull(mCashang,0) AS mCashang ");
                sbMain.Append("FROM ProduceInDepotDetail d ");
                sbMain.Append(" LEFT JOIN Product p ON p.ProductId = d.ProductId");
                sbMain.Append(" LEFT JOIN ProduceInDepot h ON h.ProduceInDepotId = d.ProduceInDepotId");
                sbMain.Append(" LEFT JOIN WorkHouse w ON w.WorkHouseId = h.WorkHouseId");
                sbMain.Append(" WHERE 1 = 1 ");
                #endregion

                #region 条件

                if (DateType == 0)
                {
                    sbContion.Append(" AND h.ProduceInDepotDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");
                }

                if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
                {
                    if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                    {
                        sbContion.Append(" AND d.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND d.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                    }
                }

                if (!(StartProduct == null && EndProduct == null))
                {
                    if (StartProduct != null && EndProduct != null)
                    {
                        sbContion.Append(" AND p.Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND p.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                    }
                }

                if (!(string.IsNullOrEmpty(StartPronoteHeaderId) && string.IsNullOrEmpty(EndPronoteHeaderId)))
                {
                    if (!string.IsNullOrEmpty(StartPronoteHeaderId) && !string.IsNullOrEmpty(EndPronoteHeaderId))
                    {
                        sbContion.Append(" AND d.PronoteHeaderId BETWEEN '" + StartPronoteHeaderId + "' AND '" + EndPronoteHeaderId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND d.PronoteHeaderId = '" + (string.IsNullOrEmpty(StartPronoteHeaderId) ? EndPronoteHeaderId : StartPronoteHeaderId) + "'");
                    }
                }

                if (!(StartWorkHouse == null && EndWorkHouse == null))
                {
                    if (StartWorkHouse != null && EndWorkHouse != null)
                    {
                        sbContion.Append("AND w.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND w.WorkHouseId = '" + (StartWorkHouse == null ? EndWorkHouse.WorkHouseId : StartWorkHouse.WorkHouseId) + "'");
                    }
                }

                if (!(StartCustomer == null && EndCustomer == null))
                {
                    if (StartCustomer != null && EndCustomer != null)
                    {
                        sbContion.Append(" AND d.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + StartCustomer.Id + "' AND '" + EndCustomer.Id + "')))");
                    }
                    else
                    {
                        sbContion.Append(" AND d.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId ='" + (StartCustomer == null ? EndCustomer.CustomerId : StartCustomer.CustomerId) + "'))");
                    }
                }

                if (EnableBLV)
                {
                    sbContion.Append(" AND d.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
                }

                if (attrQiangHua)
                    sbContion.Append(" AND p.IsQiangHua = 1");
                if (attrWuDu)
                    sbContion.Append(" AND p.IsFangWu = 1");
                if (attrWuQiangHuaWuDu)
                    sbContion.Append(" AND p.IsNoQiangFang = 1");

                switch (attrProductStates)
                {
                    case 1:
                        sbContion.Append("  AND p.ProductType = 0");
                        break;
                    case 2:
                        sbContion.Append("  AND p.ProductType = 1");
                        break;
                }

                //客户订单编号
                if (!(string.IsNullOrEmpty(StarInvoiceXOId) && string.IsNullOrEmpty(EndInvoiceXOId)))
                {
                    if (!string.IsNullOrEmpty(StarInvoiceXOId) && !string.IsNullOrEmpty(EndInvoiceXOId))
                    {
                        sbContion.Append(" and d.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId BETWEEN '" + StarInvoiceXOId + " ' AND '" + EndInvoiceXOId + "' )");
                    }
                    else
                    {
                        sbContion.Append(" and d.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId='" + (StarInvoiceXOId == null ? EndInvoiceXOId : StarInvoiceXOId) + "')");
                    }
                }

                //订单状态
                switch (InvoiceStates)
                {
                    case 1:   //已结案
                        sbContion.Append(" AND d.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 1 AND PronoteHeader.JieAnDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "')");
                        break;
                    case 2:  //未结案
                        sbContion.Append(" AND d.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 0)");
                        break;
                    default:
                        break;
                }

                #endregion

                #region 查询集合以外满足条件的在加工单相关数据
                sbContion.Append(" OR d.ProduceInDepotDetailId IN (");
                sbContion.Append(" SELECT ino.ProduceInDepotDetailId FROM ProduceInDepotDetail ino");
                sbContion.Append(" LEFT JOIN Product inp ON inp.ProductId = ino.ProductId");
                sbContion.Append(" LEFT JOIN ProduceInDepot inh ON inh.ProduceInDepotId = ino.ProduceInDepotId");
                sbContion.Append(" LEFT JOIN WorkHouse inw ON inw.WorkHouseId = inh.WorkHouseId");
                sbContion.Append(" WHERE 1 = 1");
                #region  _in条件
                if (DateType == 0)
                {
                    sbContion.Append(" AND (inh.ProduceInDepotDate < '" + StartDate.ToString("yyyy-MM-dd") + "' OR inh.ProduceInDepotDate > '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
                }

                if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
                {
                    if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                    {
                        sbContion.Append(" AND ino.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND ino.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                    }
                }

                if (!(StartProduct == null && EndProduct == null))
                {
                    if (StartProduct != null && EndProduct != null)
                    {
                        sbContion.Append(" AND inp.Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND inp.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                    }
                }
                if (!(StartWorkHouse == null && EndWorkHouse == null))
                {

                    if (StartWorkHouse != null && EndWorkHouse != null)
                    {
                        sbContion.Append(" AND inw.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                    }
                    else
                    {
                        string mwhcode = StartWorkHouse == null ? EndWorkHouse.WorkhouseCode : StartWorkHouse.WorkhouseCode;
                        if (!string.IsNullOrEmpty(mwhcode))
                            sbContion.Append(" AND inw.WorkhouseCode='" + mwhcode + "'");
                    }
                }

                sbContion.Append(" AND ino.PronoteHeaderId IN (");
                sbContion.Append(" SELECT DISTINCT inno.PronoteHeaderId FROM ProduceInDepotDetail inno");
                sbContion.Append(" LEFT JOIN ProduceInDepot innh ON innh.ProduceInDepotId = inno.ProduceInDepotId");
                sbContion.Append(" LEFT JOIN WorkHouse innw ON innw.WorkHouseId = innh.WorkHouseId");
                sbContion.Append(" LEFT JOIN Product innp ON innp.ProductId = inno.ProductId");
                sbContion.Append(" WHERE inno.PronoteHeaderId IS NOT NULL");

                #region  _inn条件
                if (DateType == 0)
                {
                    sbContion.Append(" AND innh.ProduceInDepotDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");
                }
                if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
                {
                    if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                    {
                        sbContion.Append(" AND inno.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND inno.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                    }
                }

                if (!(StartProduct == null && EndProduct == null))
                {
                    if (StartProduct != null && EndProduct != null)
                    {
                        sbContion.Append(" AND innp.Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND innp.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                    }
                }

                if (!(string.IsNullOrEmpty(StartPronoteHeaderId) && string.IsNullOrEmpty(EndPronoteHeaderId)))
                {
                    if (!string.IsNullOrEmpty(StartPronoteHeaderId) && !string.IsNullOrEmpty(EndPronoteHeaderId))
                    {
                        sbContion.Append(" AND inno.PronoteHeaderId BETWEEN '" + StartPronoteHeaderId + "' AND '" + EndPronoteHeaderId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND inno.PronoteHeaderId = '" + (string.IsNullOrEmpty(StartPronoteHeaderId) ? EndPronoteHeaderId : StartPronoteHeaderId) + "'");
                    }
                }

                if (!(StartWorkHouse == null && EndWorkHouse == null))
                {

                    if (StartWorkHouse != null && EndWorkHouse != null)
                    {
                        sbContion.Append(" AND innw.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                    }
                    else
                    {
                        string mwhcode = StartWorkHouse == null ? EndWorkHouse.WorkhouseCode : StartWorkHouse.WorkhouseCode;
                        if (!string.IsNullOrEmpty(mwhcode))
                            sbContion.Append(" AND innw.WorkhouseCode='" + mwhcode + "'");
                    }
                }

                if (!(StartCustomer == null && EndCustomer == null))
                {
                    if (StartCustomer != null && EndCustomer != null)
                    {
                        sbContion.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + StartCustomer.Id + "' AND '" + EndCustomer.Id + "')))");
                    }
                    else
                    {
                        sbContion.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId ='" + (StartCustomer == null ? EndCustomer.CustomerId : StartCustomer.CustomerId) + "'))");
                    }
                }

                //订单状态
                switch (InvoiceStates)
                {
                    case 1:   //已结案
                        sbContion.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 1 AND PronoteHeader.JieAnDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "')");
                        break;
                    case 2:  //未结案
                        sbContion.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 0)");
                        break;
                    default:
                        break;
                }


                if (EnableBLV)
                {
                    sbContion.Append(" AND inno.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
                }

                if (attrQiangHua)
                    sbContion.Append(" AND innp.IsQiangHua = 1");
                if (attrWuDu)
                    sbContion.Append(" AND innp.IsFangWu = 1");
                if (attrWuQiangHuaWuDu)
                    sbContion.Append(" AND innp.IsNoQiangFang = 1");

                switch (attrProductStates)
                {
                    case 1:
                        sbContion.Append("  AND innp.ProductType = 0");
                        break;
                    case 2:
                        sbContion.Append("  AND innp.ProductType = 1");
                        break;
                }

                #endregion

                sbContion.Append(")");

                if (EnableBLV)
                {
                    sbContion.Append(" AND ino.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
                }

                if (attrQiangHua)
                    sbContion.Append(" AND inp.IsQiangHua = 1");
                if (attrWuDu)
                    sbContion.Append(" AND inp.IsFangWu = 1");
                if (attrWuQiangHuaWuDu)
                    sbContion.Append(" AND inp.IsNoQiangFang = 1");

                switch (attrProductStates)
                {
                    case 1:
                        sbContion.Append("  AND inp.ProductType = 0");
                        break;
                    case 2:
                        sbContion.Append("  AND inp.ProductType = 1");
                        break;
                }
                //客户订单编号
                if (!(string.IsNullOrEmpty(StarInvoiceXOId) && string.IsNullOrEmpty(EndInvoiceXOId)))
                {
                    if (!string.IsNullOrEmpty(StarInvoiceXOId) && !string.IsNullOrEmpty(EndInvoiceXOId))
                    {
                        sbContion.Append(" and ino.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId BETWEEN '" + StarInvoiceXOId + " ' AND '" + EndInvoiceXOId + "')");
                    }
                    else
                    {
                        sbContion.Append(" and ino.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId='" + (StarInvoiceXOId == null ? EndInvoiceXOId : StarInvoiceXOId) + "')");
                    }
                }

                #endregion
                sbContion.Append(")");
                #endregion

                if (!string.IsNullOrEmpty(sbContion.ToString()))
                    sbMain.Append(sbContion.ToString());

                #region 排序 末尾
                switch (attrOrderColumn)
                {
                    case 0:
                        sbMain.Append(" ORDER BY ProduceInDepotDate");
                        break;
                    case 1:
                        sbMain.Append(" ORDER BY ProductName");
                        break;
                    case 2:
                        sbMain.Append(" ORDER BY Workhousename");
                        break;
                }

                if (attrOrderType != 0)
                    sbMain.Append(" DESC ");
                #endregion
            }
            else
            {
                #region 合并
                sbMain.Append("SELECT aa.*,case op.IsQiangHua WHEN 1 THEN '強化' else (case op.IsFangWU  WHEN 1 THEN '防霧' else(case op.IsNoQiangFang WHEN 1 THEN ' 無強化防霧 ' else '' end) END) END AS ProductWDQHua FROM (");
                sbMain.Append("SELECT o.ProductId,h.WorkHouseId,w.Workhousename,ProductUnit,isnull(sum(ProceduresSum),0) AS ProceduresSum,");
                sbMain.Append("isnull(sum(CheckOutSum),0) AS CheckOutSum,CASE sum(ProceduresSum) WHEN 0 THEN '0%'ELSE RTrim(CONVERT(varchar,round(1 - isnull(sum(CheckOutSum),0)/isnull(sum(ProceduresSum),1),4)*100))+'%' END AS RejectionRate_1,");
                sbMain.Append("CASE p.ProductType WHEN 0 THEN '常態' WHEN 1 THEN '特殊' else '' end AS ProductType,");
                sbMain.Append("p.ProductName,'" + StartDate.ToString("yyyy-MM-dd") + "~" + EndDate.ToString("yyyy-MM-dd") + "' AS ProduceInDepotDate,isnull(sum(mYuanliaowenti),0) AS mYuanliaowenti,");
                sbMain.Append("ISNULL(sum(mChouliaowenti),0) AS mChouliaowenti,isnull(sum(mPaoguanwenti),0) AS mPaoguanwenti,isnull(sum(mJingdiangudingdian),0) AS mJingdiangudingdian,");
                sbMain.Append("isnull(sum(mChapiancashang),0) AS mChapiancashang,isnull(sum(mWanMocashang),0) AS mWanMocashang,");
                sbMain.Append("isnull(sum(mGuaiShouZhuangShang),0) AS mGuaiShouZhuangShang,isnull(sum(mHuabancashang),0) AS mHuabancashang,isnull(sum(mGuohuojizhua),0) AS mGuohuojizhua,isnull(sum(mBaiyanHeiYan),0) AS mBaiyanHeiYan,");
                sbMain.Append("isnull(sum(mJieHeXianHuiwen),0) AS mJieHeXianHuiwen,isnull(sum(mSuoShui),0) AS mSuoShui,isnull(sum(mQiPao),0) AS mQiPao,");
                sbMain.Append("isnull(sum(mShechuqita),0) AS mShechuqita,isnull(sum(mCaMoSunHua),0) AS mCaMoSunHua,");
                sbMain.Append("isnull(sum(mChaipiancashang),0) AS mChaipiancashang,isnull(sum(mHeidianzazhi),0) AS mHeidianzazhi,isnull(sum(mQianghuaqiancashang),0) AS mQianghuaqiancashang,");
                sbMain.Append("isnull(sum(mQianghuahoucashang),0) AS mQianghuahoucashang,isnull(sum(mHanyao),0) AS mHanyao,isnull(sum(mKeLimianxu),0) AS mKeLimianxu,");
                sbMain.Append("isnull(sum(mLiuheng),0) AS mLiuheng,isnull(sum(mPengYaodiyao),0) AS mPengYaodiyao,isnull(sum(mQianghuafangwuxian),0) AS mQianghuafangwuxian,");
                sbMain.Append("isnull(sum(mYoudian),0) AS mYoudian,isnull(sum(mQianghuaQiTa),0) AS mQianghuaQiTa,");
                sbMain.Append("isnull(sum(mChangshangbuliang),0) AS mChangshangbuliang,isnull(sum(mZuzhuangcashang),0) AS mZuzhuangcashang,isnull(sum(mCashang),0) AS mCashang ");
                sbMain.Append("FROM ProduceInDepotDetail o ");
                sbMain.Append(" LEFT JOIN ProduceInDepot h ON h.ProduceInDepotId = o.ProduceInDepotId ");
                sbMain.Append(" LEFT JOIN Product p on p.ProductId = o.ProductId");
                sbMain.Append(" LEFT JOIN WorkHouse w on h.WorkHouseId = w.WorkHouseId");
                sbMain.Append(" WHERE 1 = 1  ");

                #region 注释
                //sb.Append("SELECT ");
                //sb.Append("(SELECT Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId=(SELECT WorkHouseId FROM ProduceInDepot WHERE ProduceInDepot.ProduceInDepotId=o.ProduceInDepotId)) AS Workhousename,");
                //sb.Append("ProductUnit AS ProductUnit,");
                //sb.Append("isnull(sum(ProceduresSum),0) AS ProceduresSum,isnull(sum(CheckOutSum),0) AS CheckOutSum,");
                //sb.Append("CASE sum(ProceduresSum) WHEN 0 THEN '0%' ELSE RTrim(CONVERT(char(5),round(1 - isnull(sum(CheckOutSum),0)/isnull(sum(ProceduresSum),1),2)*100))+'%' END AS RejectionRate_1,");
                //sb.Append("CASE (SELECT ProductType FROM Product WHERE Product.ProductId=o.ProductId) WHEN 0 THEN '  常態  ' else '' end AS ProductType,");
                //sb.Append("CASE (SELECT IsQiangHua FROM Product WHERE Product.ProductId=o.ProductId) WHEN 1 THEN '  強化  ' else");
                //sb.Append("(CASE (SELECT IsFangWu FROM Product WHERE Product.ProductId=o.ProductId)  WHEN 1 THEN '  防霧  ' else");
                //sb.Append("(CASE (SELECT IsNoQiangFang FROM Product WHERE Product.ProductId=o.ProductId) WHEN 1 THEN ' 無強化防霧 ' else '' end) END) END AS ProductWDQHua,");
                //sb.Append("(SELECT ProductName FROM Product WHERE Product.ProductId=o.ProductId) AS ProductName,ProductId, ");
                //sb.Append("'" + StartDate.ToString("yyyy/MM/dd") + "~" + EndDate.ToString("yyyy/MM/dd") + "' AS ProduceInDepotDate,");
                //sb.Append("isnull(sum(HeiDian),0) AS HeiDian,ISNULL(sum(ZaZhi),0) AS ZaZhi,isnull(sum(mJinDian),0) AS mJinDian,isnull(sum(mLiaoDian),0) AS mLiaoDian,isnull(sum(mCaiShang),0) AS mCaiShang,isnull(sum(mWanMo),0) AS mWanMo,isnull(sum(mSuoShui),0) AS mSuoShui,isnull(sum(mGuohuo),0) AS mGuohuo,isnull(sum(mBaiYan),0) AS mBaiYan,isnull(sum(mHeiYan),0) AS mHeiYan,isnull(sum(mJieHeXian),0) AS mJieHeXian,isnull(sum(mHuiWen),0) AS mHuiWen,");
                //sb.Append("isnull(sum(mQiPao),0) AS mQiPao,isnull(sum(mLengLiao),0) AS mLengLiao,isnull(sum(mGuaiShouZhuangShang),0) AS mGuaiShouZhuangShang,isnull(sum(mPengXu),0) AS mPengXu,isnull(sum(mCaMoSunHua),0) AS mCaMoSunHua,isnull(sum(mCaMoCiShu),0) AS mCaMoCiShu,isnull(sum(mChaiCa),0) AS mChaiCa,isnull(sum(mSheCa),0) AS mSheCa,isnull(sum(mQiangCa),0) AS mQiangCa,isnull(sum(mKeLi),0) AS mKeLi,isnull(sum(mLiuheng),0) AS mLiuheng,isnull(sum(mPengYao),0) AS mPengYao,isnull(sum(mYuHuoJiZhua),0) AS mYuHuoJiZhua ,isnull(sum(mZaZhiJingDian),0) AS mZaZhiJingDian,isnull(sum(mQiTa),0) AS mQiTa");
                //sb.Append(" FROM ProduceInDepotDetail o");
                #endregion

                #endregion

                #region 条件
                if (DateType == 0)
                {
                    sbContion.Append(" AND h.ProduceInDepotDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");
                }

                if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
                {
                    if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                    {
                        sbContion.Append(" AND o.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND o.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                    }
                }

                if (!(StartProduct == null && EndProduct == null))
                {
                    if (StartProduct != null && EndProduct != null)
                    {
                        sbContion.Append(" AND p.Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND p.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                    }
                }

                if (!(string.IsNullOrEmpty(StartPronoteHeaderId) && string.IsNullOrEmpty(EndPronoteHeaderId)))
                {
                    if (!string.IsNullOrEmpty(StartPronoteHeaderId) && !string.IsNullOrEmpty(EndPronoteHeaderId))
                    {
                        sbContion.Append(" AND o.PronoteHeaderId BETWEEN '" + StartPronoteHeaderId + "' AND '" + EndPronoteHeaderId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND o.PronoteHeaderId = '" + (string.IsNullOrEmpty(StartPronoteHeaderId) ? EndPronoteHeaderId : StartPronoteHeaderId) + "'");
                    }
                }

                if (!(StartWorkHouse == null && EndWorkHouse == null))
                {

                    if (StartWorkHouse != null && EndWorkHouse != null)
                    {
                        sbContion.Append(" AND w.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                    }
                    else
                    {
                        string mwhcode = StartWorkHouse == null ? EndWorkHouse.WorkhouseCode : StartWorkHouse.WorkhouseCode;
                        if (!string.IsNullOrEmpty(mwhcode))
                            sbContion.Append(" AND w.WorkhouseCode='" + mwhcode + "'");
                    }
                }

                if (!(StartCustomer == null && EndCustomer == null))
                {
                    if (StartCustomer != null && EndCustomer != null)
                    {
                        sbContion.Append(" AND o.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + StartCustomer.Id + "' AND '" + EndCustomer.Id + "')))");
                    }
                    else
                    {
                        sbContion.Append(" AND o.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId ='" + (StartCustomer == null ? EndCustomer.CustomerId : StartCustomer.CustomerId) + "'))");
                    }
                }

                if (EnableBLV)
                {
                    sbContion.Append(" AND o.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
                }

                if (attrQiangHua)
                    sbContion.Append(" AND p.IsQiangHua = 1");
                if (attrWuDu)
                    sbContion.Append(" AND p.IsFangWu = 1");
                if (attrWuQiangHuaWuDu)
                    sbContion.Append(" AND p.IsNoQiangFang = 1");

                switch (attrProductStates)
                {
                    case 1:
                        sbContion.Append("  AND p.ProductType = 0");
                        break;
                    case 2:
                        sbContion.Append("  AND p.ProductType = 1");
                        break;
                }

                //客户订单编号
                if (!(string.IsNullOrEmpty(StarInvoiceXOId) && string.IsNullOrEmpty(EndInvoiceXOId)))
                {
                    if (!string.IsNullOrEmpty(StarInvoiceXOId) && !string.IsNullOrEmpty(EndInvoiceXOId))
                    {
                        sbContion.Append(" and o.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId BETWEEN '" + StarInvoiceXOId + " ' AND '" + EndInvoiceXOId + "' )");
                    }
                    else
                    {
                        sbContion.Append(" and o.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId='" + (StarInvoiceXOId == null ? EndInvoiceXOId : StarInvoiceXOId) + "')");
                    }
                }

                //订单状态
                switch (InvoiceStates)
                {
                    case 1:   //已结案
                        sbContion.Append(" AND o.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 1 AND PronoteHeader.JieAnDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "')");
                        break;
                    case 2:  //未结案
                        sbContion.Append(" AND o.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 0)");
                        break;
                    default:
                        break;
                }

                #endregion

                #region 查询集合以外满足条件的在加工单相关数据
                sbContion.Append(" OR o.ProduceInDepotDetailId IN (");
                sbContion.Append(" SELECT ino.ProduceInDepotDetailId FROM ProduceInDepotDetail ino");
                sbContion.Append(" LEFT JOIN Product inp ON inp.ProductId = ino.ProductId");
                sbContion.Append(" LEFT JOIN ProduceInDepot inh ON inh.ProduceInDepotId = ino.ProduceInDepotId");
                sbContion.Append(" LEFT JOIN WorkHouse inw ON inw.WorkHouseId = inh.WorkHouseId");
                sbContion.Append(" WHERE 1 = 1");
                #region  _in条件

                if (DateType == 0)
                {
                    sbContion.Append(" AND (inh.ProduceInDepotDate < '" + StartDate.ToString("yyyy-MM-dd") + "' OR inh.ProduceInDepotDate > '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
                }

                if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
                {
                    if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                    {
                        sbContion.Append(" AND ino.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND ino.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                    }
                }

                if (!(StartProduct == null && EndProduct == null))
                {
                    if (StartProduct != null && EndProduct != null)
                    {
                        sbContion.Append(" AND inp.Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND inp.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                    }
                }
                if (!(StartWorkHouse == null && EndWorkHouse == null))
                {

                    if (StartWorkHouse != null && EndWorkHouse != null)
                    {
                        sbContion.Append(" AND inw.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                    }
                    else
                    {
                        string mwhcode = StartWorkHouse == null ? EndWorkHouse.WorkhouseCode : StartWorkHouse.WorkhouseCode;
                        if (!string.IsNullOrEmpty(mwhcode))
                            sbContion.Append(" AND inw.WorkhouseCode='" + mwhcode + "'");
                    }
                }

                sbContion.Append(" AND ino.PronoteHeaderId IN (");
                sbContion.Append(" SELECT DISTINCT inno.PronoteHeaderId FROM ProduceInDepotDetail inno");
                sbContion.Append(" LEFT JOIN ProduceInDepot innh ON innh.ProduceInDepotId = inno.ProduceInDepotId");
                sbContion.Append(" LEFT JOIN WorkHouse innw ON innw.WorkHouseId = innh.WorkHouseId");
                sbContion.Append(" LEFT JOIN Product innp ON innp.ProductId = inno.ProductId");
                sbContion.Append(" WHERE inno.PronoteHeaderId IS NOT NULL");

                #region  _inn条件
                if (DateType == 0)
                {
                    sbContion.Append(" AND innh.ProduceInDepotDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");
                }

                if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
                {
                    if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                    {
                        sbContion.Append(" AND inno.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND inno.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                    }
                }

                if (!(StartProduct == null && EndProduct == null))
                {
                    if (StartProduct != null && EndProduct != null)
                    {
                        sbContion.Append(" AND innp.Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND innp.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                    }
                }

                if (!(string.IsNullOrEmpty(StartPronoteHeaderId) && string.IsNullOrEmpty(EndPronoteHeaderId)))
                {
                    if (!string.IsNullOrEmpty(StartPronoteHeaderId) && !string.IsNullOrEmpty(EndPronoteHeaderId))
                    {
                        sbContion.Append(" AND inno.PronoteHeaderId BETWEEN '" + StartPronoteHeaderId + "' AND '" + EndPronoteHeaderId + "'");
                    }
                    else
                    {
                        sbContion.Append(" AND inno.PronoteHeaderId = '" + (string.IsNullOrEmpty(StartPronoteHeaderId) ? EndPronoteHeaderId : StartPronoteHeaderId) + "'");
                    }
                }

                if (!(StartWorkHouse == null && EndWorkHouse == null))
                {

                    if (StartWorkHouse != null && EndWorkHouse != null)
                    {
                        sbContion.Append(" AND innw.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                    }
                    else
                    {
                        string mwhcode = StartWorkHouse == null ? EndWorkHouse.WorkhouseCode : StartWorkHouse.WorkhouseCode;
                        if (!string.IsNullOrEmpty(mwhcode))
                            sbContion.Append(" AND innw.WorkhouseCode='" + mwhcode + "'");
                    }
                }

                if (!(StartCustomer == null && EndCustomer == null))
                {
                    if (StartCustomer != null && EndCustomer != null)
                    {
                        sbContion.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + StartCustomer.Id + "' AND '" + EndCustomer.Id + "')))");
                    }
                    else
                    {
                        sbContion.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId ='" + (StartCustomer == null ? EndCustomer.CustomerId : StartCustomer.CustomerId) + "'))");
                    }
                }

                switch (InvoiceStates)
                {
                    case 1:   //已结案
                        sbContion.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 1 AND PronoteHeader.JieAnDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "')");
                        break;
                    case 2:  //未结案
                        sbContion.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 0)");
                        break;
                    default:
                        break;
                }

                if (EnableBLV)
                {
                    sbContion.Append(" AND inno.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
                }

                if (attrQiangHua)
                    sbContion.Append(" AND innp.IsQiangHua = 1");
                if (attrWuDu)
                    sbContion.Append(" AND innp.IsFangWu = 1");
                if (attrWuQiangHuaWuDu)
                    sbContion.Append(" AND innp.IsNoQiangFang = 1");

                switch (attrProductStates)
                {
                    case 1:
                        sbContion.Append("  AND innp.ProductType = 0");
                        break;
                    case 2:
                        sbContion.Append("  AND innp.ProductType = 1");
                        break;
                }

                #endregion

                sbContion.Append(")");


                if (EnableBLV)
                {
                    sbContion.Append(" AND ino.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
                }

                if (attrQiangHua)
                    sbContion.Append(" AND inp.IsQiangHua = 1");
                if (attrWuDu)
                    sbContion.Append(" AND inp.IsFangWu = 1");
                if (attrWuQiangHuaWuDu)
                    sbContion.Append(" AND inp.IsNoQiangFang = 1");

                switch (attrProductStates)
                {
                    case 1:
                        sbContion.Append("  AND inp.ProductType = 0");
                        break;
                    case 2:
                        sbContion.Append("  AND inp.ProductType = 1");
                        break;
                }

                //客户订单编号
                if (!(string.IsNullOrEmpty(StarInvoiceXOId) && string.IsNullOrEmpty(EndInvoiceXOId)))
                {
                    if (!string.IsNullOrEmpty(StarInvoiceXOId) && !string.IsNullOrEmpty(EndInvoiceXOId))
                    {
                        sbContion.Append(" and ino.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId BETWEEN '" + StarInvoiceXOId + " ' AND '" + EndInvoiceXOId + "')");
                    }
                    else
                    {
                        sbContion.Append(" and ino.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId='" + (StarInvoiceXOId == null ? EndInvoiceXOId : StarInvoiceXOId) + "')");
                    }
                }

                #endregion
                sbContion.Append(")");
                #endregion

                if (!string.IsNullOrEmpty(sbContion.ToString()))
                    sbMain.Append(sbContion.ToString());

                #region 排序 末尾
                sbMain.Append(" GROUP BY o.ProductId,h.WorkHouseId,o.ProductUnit,p.ProductType,w.WorkHousename,p.ProductName");
                sbMain.Append(" ) aa left join product op on aa.productid = op.productId where 1=1 ");

                switch (attrOrderColumn)
                {
                    case 1:
                        sbMain.Append(" ORDER BY op.Id");
                        break;
                    case 2:
                        sbMain.Append(" ORDER BY aa.WorkHousename");
                        break;
                }

                if (attrOrderType > 0)
                    sbMain.Append(" DESC ");
                #endregion
            }

            string sql = sbMain.ToString();
            string coon = sqlmapper.DataSource.ConnectionString;
            SqlDataAdapter sda = new SqlDataAdapter(sql, coon);
            sda.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds.Tables[0];
        }

        //生产不良率统计，按照部门进行生产数量、合格数量、不良率统计(合计)
        public DataTable SUMDTSelect_ChooseDefectRateCls(int DateType, DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Book.Model.Product StartProduct, Book.Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Book.Model.WorkHouse StartWorkHouse, Book.Model.WorkHouse EndWorkHouse, Book.Model.Customer StartCustomer, Book.Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType, string StarInvoiceXOId, string EndInvoiceXOId, int InvoiceStates)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT w.Workhousename,ISNULL(sum(d.ProceduresSum),0) AS ProceduresSum,ISNULL(sum(d.CheckOutSum),0) AS CheckOutSum,");
            sb.Append("CASE sum(d.ProceduresSum) WHEN 0 THEN '0%' ELSE RTrim(CONVERT(varchar,round(1 - isnull(sum(d.CheckOutSum),0)/isnull(sum(d.ProceduresSum),1),4)*100))+'%' END AS RejectionRate_1 ");
            sb.Append("FROM ProduceInDepotDetail d");
            sb.Append(" LEFT JOIN Product p ON p.ProductId = d.ProductId");
            sb.Append(" LEFT JOIN ProduceInDepot h ON h.ProduceInDepotId = d.ProduceInDepotId ");
            sb.Append(" LEFT JOIN WorkHouse w ON w.WorkHouseId = h.WorkHouseId ");
            sb.Append(" WHERE 1 = 1");

            if (DateType == 0)
            {
                sb.Append(" AND h.ProduceInDepotDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");
            }

            if (EnableBLV)
            {
                sb.Append(" AND d.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
            }

            if (attrQiangHua)
                sb.Append(" AND p.IsQiangHua = 1");
            if (attrWuDu)
                sb.Append(" AND p.IsFangWu = 1");
            if (attrWuQiangHuaWuDu)
                sb.Append(" AND p.IsNoQiangFang = 1");

            switch (attrProductStates)
            {
                case 1:
                    sb.Append("  AND p.ProductType = 0");
                    break;
                case 2:
                    sb.Append("  AND p.ProductType = 1");
                    break;
            }

            if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
            {
                if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                {
                    sb.Append(" AND h.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                }
                else
                {
                    sb.Append(" AND h.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                }
            }

            if (!(StartProduct == null && EndProduct == null))
            {
                if (StartProduct != null && EndProduct != null)
                {
                    sb.Append(" AND d.ProductId IN (SELECT Product.ProductId FROM Product WHERE Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "')");
                }
                else
                {
                    sb.Append(" AND d.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                }
            }

            if (!(string.IsNullOrEmpty(StartPronoteHeaderId) && string.IsNullOrEmpty(EndPronoteHeaderId)))
            {
                if (!string.IsNullOrEmpty(StartPronoteHeaderId) && !string.IsNullOrEmpty(EndPronoteHeaderId))
                {
                    sb.Append(" AND d.PronoteHeaderId BETWEEN '" + StartPronoteHeaderId + "' AND '" + EndPronoteHeaderId + "'");
                }
                else
                {
                    sb.Append(" AND d.PronoteHeaderId = '" + (string.IsNullOrEmpty(StartPronoteHeaderId) ? EndPronoteHeaderId : StartPronoteHeaderId) + "'");
                }
            }

            if (!(StartWorkHouse == null && EndWorkHouse == null))
            {
                if (StartWorkHouse != null && EndWorkHouse != null)
                {
                    sb.Append(" AND w.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                }
                else
                {
                    string mwhcode = StartWorkHouse == null ? EndWorkHouse.WorkhouseCode : StartWorkHouse.WorkhouseCode;
                    if (!string.IsNullOrEmpty(mwhcode))
                        sb.Append(" AND w.WorkhouseCode = '" + mwhcode + "'");
                }
            }

            if (!(StartCustomer == null && EndCustomer == null))
            {
                if (StartCustomer != null && EndCustomer != null)
                {
                    sb.Append(" AND d.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId IN (SELECT Customer.CustomerId FROM Customer WHERE Customer.Id BETWEEN '" + StartCustomer.Id + "' AND '" + EndCustomer.Id + "')))");
                }
                else
                {
                    sb.Append(" AND d.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId ='" + (StartCustomer == null ? EndCustomer.CustomerId : StartCustomer.CustomerId) + "'))");
                }
            }
            //客户订单编号
            if (!(string.IsNullOrEmpty(StarInvoiceXOId) && string.IsNullOrEmpty(EndInvoiceXOId)))
            {
                if (!string.IsNullOrEmpty(StarInvoiceXOId) && !string.IsNullOrEmpty(EndInvoiceXOId))
                {
                    sb.Append(" and d.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId BETWEEN '" + StarInvoiceXOId + " ' AND '" + EndInvoiceXOId + "')");
                }
                else
                {
                    sb.Append(" and d.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId='" + (StarInvoiceXOId == null ? EndInvoiceXOId : StarInvoiceXOId) + "')");
                }
            }

            //订单状态
            switch (InvoiceStates)
            {
                case 1:   //已结案
                    sb.Append(" AND d.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose= 1 AND PronoteHeader.JieAnDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "')");
                    break;
                case 2:  //未结案
                    sb.Append(" AND d.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose= 0)");
                    break;
                default:
                    break;
            }

            #region 查询集合以外满足条件的在加工单相关数据
            sb.Append(" OR d.ProduceInDepotDetailId IN (");
            sb.Append(" SELECT ino.ProduceInDepotDetailId FROM ProduceInDepotDetail ino");
            sb.Append(" LEFT JOIN Product inp ON inp.ProductId = ino.ProductId");
            sb.Append(" LEFT JOIN ProduceInDepot inh ON inh.ProduceInDepotId = ino.ProduceInDepotId");
            sb.Append(" LEFT JOIN WorkHouse inw ON inw.WorkHouseId = inh.WorkHouseId");
            sb.Append(" WHERE 1 = 1");
            #region  _in条件
            if (DateType == 0)
            {
                sb.Append(" AND (inh.ProduceInDepotDate < '" + StartDate.ToString("yyyy-MM-dd") + "' OR inh.ProduceInDepotDate > '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
            }

            if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
            {
                if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                {
                    sb.Append(" AND ino.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                }
                else
                {
                    sb.Append(" AND ino.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                }
            }

            if (!(StartProduct == null && EndProduct == null))
            {
                if (StartProduct != null && EndProduct != null)
                {
                    sb.Append(" AND inp.Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "'");
                }
                else
                {
                    sb.Append(" AND inp.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                }
            }
            if (!(StartWorkHouse == null && EndWorkHouse == null))
            {

                if (StartWorkHouse != null && EndWorkHouse != null)
                {
                    sb.Append(" AND inw.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                }
                else
                {
                    string mwhcode = StartWorkHouse == null ? EndWorkHouse.WorkhouseCode : StartWorkHouse.WorkhouseCode;
                    if (!string.IsNullOrEmpty(mwhcode))
                        sb.Append(" AND inw.WorkhouseCode='" + mwhcode + "'");
                }
            }

            sb.Append(" AND ino.PronoteHeaderId IN (");
            sb.Append(" SELECT DISTINCT inno.PronoteHeaderId FROM ProduceInDepotDetail inno");
            sb.Append(" LEFT JOIN ProduceInDepot innh ON innh.ProduceInDepotId = inno.ProduceInDepotId");
            sb.Append(" LEFT JOIN WorkHouse innw ON innw.WorkHouseId = innh.WorkHouseId");
            sb.Append(" LEFT JOIN Product innp ON innp.ProductId = inno.ProductId");
            sb.Append(" WHERE inno.PronoteHeaderId IS NOT NULL");

            #region  _inn条件
            if (DateType == 0)
            {
                sb.Append(" AND innh.ProduceInDepotDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");
            }

            if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
            {
                if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                {
                    sb.Append(" AND inno.ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                }
                else
                {
                    sb.Append(" AND inno.ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                }
            }

            if (!(StartProduct == null && EndProduct == null))
            {
                if (StartProduct != null && EndProduct != null)
                {
                    sb.Append(" AND innp.Id BETWEEN '" + StartProduct.Id + "' AND '" + EndProduct.Id + "'");
                }
                else
                {
                    sb.Append(" AND innp.ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                }
            }

            if (!(string.IsNullOrEmpty(StartPronoteHeaderId) && string.IsNullOrEmpty(EndPronoteHeaderId)))
            {
                if (!string.IsNullOrEmpty(StartPronoteHeaderId) && !string.IsNullOrEmpty(EndPronoteHeaderId))
                {
                    sb.Append(" AND inno.PronoteHeaderId BETWEEN '" + StartPronoteHeaderId + "' AND '" + EndPronoteHeaderId + "'");
                }
                else
                {
                    sb.Append(" AND inno.PronoteHeaderId = '" + (string.IsNullOrEmpty(StartPronoteHeaderId) ? EndPronoteHeaderId : StartPronoteHeaderId) + "'");
                }
            }

            if (!(StartWorkHouse == null && EndWorkHouse == null))
            {

                if (StartWorkHouse != null && EndWorkHouse != null)
                {
                    sb.Append(" AND innw.WorkhouseCode BETWEEN '" + StartWorkHouse.WorkhouseCode + "' AND '" + EndWorkHouse.WorkhouseCode + "'");
                }
                else
                {
                    string mwhcode = StartWorkHouse == null ? EndWorkHouse.WorkhouseCode : StartWorkHouse.WorkhouseCode;
                    if (!string.IsNullOrEmpty(mwhcode))
                        sb.Append(" AND innw.WorkhouseCode='" + mwhcode + "'");
                }
            }

            if (!(StartCustomer == null && EndCustomer == null))
            {
                if (StartCustomer != null && EndCustomer != null)
                {
                    sb.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + StartCustomer.Id + "' AND '" + EndCustomer.Id + "')))");
                }
                else
                {
                    sb.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId ='" + (StartCustomer == null ? EndCustomer.CustomerId : StartCustomer.CustomerId) + "'))");
                }
            }

            if (EnableBLV)
            {
                sb.Append(" AND inno.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
            }

            if (attrQiangHua)
                sb.Append(" AND innp.IsQiangHua = 1");
            if (attrWuDu)
                sb.Append(" AND innp.IsFangWu = 1");
            if (attrWuQiangHuaWuDu)
                sb.Append(" AND innp.IsNoQiangFang = 1");

            switch (attrProductStates)
            {
                case 1:
                    sb.Append("  AND innp.ProductType = 0");
                    break;
                case 2:
                    sb.Append("  AND innp.ProductType = 1");
                    break;
            }

            //订单状态
            switch (InvoiceStates)
            {
                case 1:   //已结案
                    sb.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 1  AND  PronoteHeader.JieAnDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.ToString("yyyy-MM-dd") + "')");
                    break;
                case 2:  //未结案
                    sb.Append(" AND inno.PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.IsClose = 0)");
                    break;
                default:
                    break;
            }

            #endregion

            sb.Append(")");


            if (EnableBLV)
            {
                sb.Append(" AND ino.RejectionRate " + RejectionRateCompare + " " + (RejectionRate / 100).ToString());
            }

            if (attrQiangHua)
                sb.Append(" AND inp.IsQiangHua = 1");
            if (attrWuDu)
                sb.Append(" AND inp.IsFangWu = 1");
            if (attrWuQiangHuaWuDu)
                sb.Append(" AND inp.IsNoQiangFang = 1");

            switch (attrProductStates)
            {
                case 1:
                    sb.Append("  AND inp.ProductType = 0");
                    break;
                case 2:
                    sb.Append("  AND inp.ProductType = 1");
                    break;
            }

            //客户订单编号
            if (!(string.IsNullOrEmpty(StarInvoiceXOId) && string.IsNullOrEmpty(EndInvoiceXOId)))
            {
                if (!string.IsNullOrEmpty(StarInvoiceXOId) && !string.IsNullOrEmpty(EndInvoiceXOId))
                {
                    sb.Append(" and ino.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId BETWEEN '" + StarInvoiceXOId + " ' AND '" + EndInvoiceXOId + "')");
                }
                else
                {
                    sb.Append(" and ino.PronoteHeaderId IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE InvoiceCusId='" + (StarInvoiceXOId == null ? EndInvoiceXOId : StarInvoiceXOId) + "')");
                }
            }
            #endregion
            sb.Append(")");
            #endregion

            sb.Append(" GROUP BY w.Workhousename");

            string sql = sb.ToString();
            string coon = sqlmapper.DataSource.ConnectionString;
            SqlDataAdapter sd = new SqlDataAdapter(sql, coon);
            sd.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            sd.Fill(ds);
            return ds.Tables[0];
        }

        //商品不良率统计表
        public DataTable PTSelect_ChooseDefectRateCls(DateTime StartDate, DateTime EndDate, string StartProduceInDepotId, string EndProduceInDepotId, Book.Model.Product StartProduct, Book.Model.Product EndProduct, string StartPronoteHeaderId, string EndPronoteHeaderId, Book.Model.WorkHouse StartWorkHouse, Book.Model.WorkHouse EndWorkHouse, Book.Model.Customer StartCustomer, Book.Model.Customer EndCustomer, bool attrJiLuFangShi, bool attrQiangHua, bool attrWuDu, bool attrWuQiangHuaWuDu, int attrProductStates, double RejectionRate, string RejectionRateCompare, bool EnableBLV, int attrOrderColumn, int attrOrderType)
        {
            StringBuilder sb = new StringBuilder();

            //条件筛选
            if (attrJiLuFangShi)
            {
                sb.Append("SELECT (SELECT Substring(CONVERT(varchar,ProduceInDepotDate,120),0,11) FROM ProduceInDepot WHERE ProduceInDepot.ProduceInDepotId = o.ProduceInDepotId) AS ProduceInDepotDate,");
                sb.Append("(SELECT WorkHouseId FROM ProduceInDepot WHERE ProduceInDepot.ProduceInDepotId=o.ProduceInDepotId) AS WorkHouseId,");
                sb.Append("(SELECT CASE ProceduresSum WHEN 0 THEN '0%' ELSE RTrim(CONVERT(varchar,round(1 - isnull(CheckOutSum,0)/isnull(ProceduresSum,1),4)*100))+'%' END FROM ProduceInDepotDetail WHERE ProduceInDepotDetail.ProduceInDepotDetailId = o.ProduceInDepotDetailId)  AS RejectionRate_1,");
                sb.Append("(SELECT Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId=(SELECT WorkHouseId FROM ProduceInDepot WHERE ProduceInDepot.ProduceInDepotId=o.ProduceInDepotId)) AS Workhousename,");
                sb.Append("CASE (SELECT ProductType FROM Product WHERE Product.ProductId=o.ProductId) WHEN 0 THEN ' 常態 ' else '' end AS ProductType,");
                sb.Append("CASE (SELECT IsQiangHua FROM Product WHERE Product.ProductId=o.ProductId) WHEN 1 THEN '  強化  ' else");
                sb.Append("(CASE (SELECT IsFangWu FROM Product WHERE Product.ProductId=o.ProductId)  WHEN 1 THEN '  防霧  ' else");
                sb.Append("(CASE (SELECT IsNoQiangFang FROM Product WHERE Product.ProductId=o.ProductId) WHEN 1 THEN ' 無強化防霧 ' else '' end) END) END AS ProductWDQHua,");
                sb.Append("(SELECT ProductName FROM Product WHERE Product.ProductId=o.ProductId) AS ProductName,ProductId,WorkHouseId,o.ProduceInDepotId,ProductUnit AS ProductUnit,ProceduresSum AS ProceduresSum,CheckOutSum AS CheckOutSum,mYuanliaowenti AS mYuanliaowenti,mChouliaowenti AS mChouliaowenti,");
                sb.Append("mPaoguanwenti AS mPaoguanwenti,mJingdiangudingdian AS mJingdiangudingdian,mChapiancashang AS mChapiancashang,mWanMocashang AS mWanMocashang,mGuaiShouZhuangShang AS  mGuaiShouZhuangShang,mHuabancashang AS mHuabancashang,mGuohuojizhua AS mGuohuojizhua,mBaiyanHeiYan AS mBaiyanHeiYan,mJieHeXianHuiwen AS mJieHeXianHuiwen,mSuoShui AS mSuoShui,mQiPao AS mQiPao,mShechuqita AS mShechuqita,mCaMoSunHua AS mCaMoSunHua,");
                sb.Append("mChaipiancashang AS mChaipiancashang,mHeidianzazhi AS mHeidianzazhi,mQianghuaqiancashang AS mQianghuaqiancashang,mQianghuahoucashang AS mQianghuahoucashang,mHanyao AS mHanyao,mKeLimianxu AS mKeLimianxu,mLiuheng AS mLiuheng,mPengYaodiyao AS mPengYaodiyao,mQianghuafangwuxian AS  mQianghuafangwuxian,mYoudian AS mYoudian,mQianghuaQiTa AS mQianghuaQiTa,mChangshangbuliang as mChangshangbuliang,mZuzhuangcashang as mZuzhuangcashang,mCashang as mCashang,ProceduresSum AS ProceduresSum,CheckOutSum AS CheckOutSum ");
                sb.Append("FROM ProduceInDepotDetail o");
            }
            else
            {
                sb.Append("SELECT ");
                sb.Append("(SELECT Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId=(SELECT WorkHouseId FROM ProduceInDepot WHERE ProduceInDepot.ProduceInDepotId=o.ProduceInDepotId)) AS Workhousename,");
                sb.Append("ProductUnit AS ProductUnit,");
                sb.Append("isnull(sum(ProceduresSum),0) AS ProceduresSum,isnull(sum(CheckOutSum),0) AS CheckOutSum,");
                sb.Append("CASE sum(ProceduresSum) WHEN 0 THEN '0%' ELSE RTrim(CONVERT(varchar,round(1 - isnull(sum(CheckOutSum),0)/isnull(sum(ProceduresSum),1),4)*100))+'%' END AS RejectionRate_1,");
                sb.Append("CASE (SELECT ProductType FROM Product WHERE Product.ProductId=o.ProductId) WHEN 0 THEN ' 常態 ' else '' end AS ProductType,");
                sb.Append("CASE (SELECT IsQiangHua FROM Product WHERE Product.ProductId=o.ProductId) WHEN 1 THEN ' 強化 ' else");
                sb.Append("(CASE (SELECT IsFangWu FROM Product WHERE Product.ProductId=o.ProductId)  WHEN 1 THEN ' 防霧 ' else");
                sb.Append("(CASE (SELECT IsNoQiangFang FROM Product WHERE Product.ProductId=o.ProductId) WHEN 1 THEN ' 無強化防霧 ' else '' end) END) END AS ProductWDQHua,");
                sb.Append("(SELECT ProductName FROM Product WHERE Product.ProductId=o.ProductId) AS ProductName,ProductId, ");
                sb.Append("'" + StartDate.ToString("yyyy/MM/dd") + "~" + EndDate.ToString("yyyy/MM/dd") + "' AS ProduceInDepotDate,");
                sb.Append("isnull(sum(mYuanliaowenti),0) AS mYuanliaowenti,ISNULL(sum(mChouliaowenti),0) AS mChouliaowenti,isnull(sum(mPaoguanwenti),0) AS mPaoguanwenti,isnull(sum(mJingdiangudingdian),0) AS mJingdiangudingdian,isnull(sum(mChapiancashang),0) AS mChapiancashang,isnull(sum(mWanMocashang),0) AS mWanMocashang,isnull(sum(mGuaiShouZhuangShang),0) AS mGuaiShouZhuangShang,isnull(sum(mHuabancashang),0) AS mHuabancashang,isnull(sum(mGuohuojizhua),0) AS mGuohuojizhua,isnull(sum(mBaiyanHeiYan),0) AS mBaiyanHeiYan,isnull(sum(mJieHeXianHuiwen),0) AS mJieHeXianHuiwen,isnull(sum(mSuoShui),0) AS mSuoShui,");
                sb.Append("isnull(sum(mQiPao),0) AS mQiPao,isnull(sum(mShechuqita),0) AS mShechuqita,isnull(sum(mCaMoSunHua),0) AS mCaMoSunHua,isnull(sum(mChaipiancashang),0) AS mChaipiancashang,isnull(sum(mHeidianzazhi),0) AS mHeidianzazhi,isnull(sum(mQianghuaqiancashang),0) AS mQianghuaqiancashang,isnull(sum(mQianghuahoucashang),0) AS mQianghuahoucashang,isnull(sum(mHanyao),0) AS mHanyao,isnull(sum(mKeLimianxu),0) AS mKeLimianxu,isnull(sum(mLiuheng),0) AS mLiuheng,isnull(sum(mPengYaodiyao),0) AS mPengYaodiyao,isnull(sum(mQianghuafangwuxian),0) AS mQianghuafangwuxian,isnull(sum(mYoudian),0) AS mYoudian ,isnull(sum(mQianghuaQiTa),0) AS mQianghuaQiTa,isnull(sum(mChangshangbuliang),0) AS mChangshangbuliang,isnull(sum(mZuzhuangcashang),0) as mZuzhuangcashang,isnull(sum(mCashang),0) as mCashang");
                sb.Append(" FROM ProduceInDepotDetail o");
            }

            sb.Append(" WHERE ProduceInDepotId IN (SELECT ProduceInDepot.ProduceInDepotId FROM ProduceInDepot WHERE ProduceInDepotDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
            if (!(string.IsNullOrEmpty(StartProduceInDepotId) && string.IsNullOrEmpty(EndProduceInDepotId)))
            {
                if (!string.IsNullOrEmpty(StartProduceInDepotId) && !string.IsNullOrEmpty(EndProduceInDepotId))
                {
                    if (StartProduceInDepotId != EndProduceInDepotId)
                    {
                        sb.Append(" AND ProduceInDepotId BETWEEN '" + StartProduceInDepotId + "' AND '" + EndProduceInDepotId + "'");
                    }
                }
                else
                {
                    sb.Append(" AND ProduceInDepotId = '" + (string.IsNullOrEmpty(StartProduceInDepotId) ? EndProduceInDepotId : StartProduceInDepotId) + "'");
                }
            }

            if (!(StartProduct == null && EndProduct == null))
            {
                if (StartProduct != null && EndProduct != null)
                {
                    sb.Append(" AND ProductId IN (SELECT ProductId FROM Product WHERE ProductName BETWEEN '" + StartProduct.ProductName + "' AND '" + EndProduct.ProductName + "')");
                }
                else
                {
                    sb.Append(" AND ProductId = '" + StartProduct == null ? EndProduct.ProductId : StartProduct.ProductId + "'");
                }
            }

            if (!(string.IsNullOrEmpty(StartPronoteHeaderId) && string.IsNullOrEmpty(EndPronoteHeaderId)))
            {
                if (!string.IsNullOrEmpty(StartPronoteHeaderId) && !string.IsNullOrEmpty(EndPronoteHeaderId))
                {
                    if (StartProduceInDepotId != EndProduceInDepotId)
                    {
                        sb.Append(" AND PronoteHeaderId BETWEEN '" + StartPronoteHeaderId + "' AND '" + EndPronoteHeaderId + "'");
                    }
                }
                else
                {
                    sb.Append(" AND PronoteHeaderId = '" + (string.IsNullOrEmpty(StartPronoteHeaderId) ? EndPronoteHeaderId : StartPronoteHeaderId) + "'");
                }
            }

            if (!(StartWorkHouse == null && EndWorkHouse == null))
            {
                if (StartWorkHouse != null && EndWorkHouse != null)
                {
                    sb.Append(" AND ProduceInDepotId IN (SELECT ProduceInDepot.ProduceInDepotId FROM ProduceInDepot WHERE ProduceInDepot.WorkHouseId BETWEEN '" + StartWorkHouse.WorkHouseId + "' AND '" + EndWorkHouse.WorkHouseId + "')");
                }
                else
                {
                    sb.Append(" AND ProduceInDepotId IN (SELECT ProduceInDepot.ProduceInDepotId FROM ProduceInDepot WHERE ProduceInDepot.WorkHouseId = '" + (StartWorkHouse == null ? EndWorkHouse.WorkHouseId : StartWorkHouse.WorkHouseId) + "')");
                }
            }

            if (!(StartCustomer == null && EndCustomer == null))
            {
                if (StartCustomer != null && EndCustomer != null)
                {
                    sb.Append(" AND PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId BETWEEN '" + StartCustomer.CustomerId + "' AND '" + EndCustomer.CustomerId + "'))");
                }
                else
                {
                    sb.Append(" AND PronoteHeaderId IN (SELECT PronoteHeader.PronoteHeaderID FROM PronoteHeader WHERE PronoteHeader.InvoiceXOId IN (SELECT InvoiceXO.InvoiceId FROM InvoiceXO WHERE xocustomerId ='" + (StartCustomer == null ? EndCustomer.CustomerId : StartCustomer.CustomerId) + "'))");
                }
            }

            if (EnableBLV)
            {
                sb.Append(" AND RejectionRate " + RejectionRateCompare + " " + RejectionRate.ToString());
            }

            if (attrJiLuFangShi)
            {

            }
            else
            {
                sb.Append(" GROUP BY ProduceInDepotId,ProductUnit");
            }

            string sql = sb.ToString();
            string coon = sqlmapper.DataSource.ConnectionString;
            SqlDataAdapter sda = new SqlDataAdapter(sql, coon);
            sda.SelectCommand.CommandTimeout = 0;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds.Tables[0];
        }

        public IList<Book.Model.ProduceInDepotDetail> SelectHejiSumByPronoteHeader(string PronoteHeaderIds)
        {
            return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.SelectHejiSumByPronoteHeader", PronoteHeaderIds);
        }

        public IList<Book.Model.ProduceInDepotDetail> Select_ByWorkHosueAndPronoteId(string WorkHouseid, string PronoteHeaderId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("WorkHouseid", WorkHouseid);
            ht.Add("PronoteHeaderId", PronoteHeaderId);
            return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.Select_ByWorkHosueAndPronoteId", ht);
        }

        //加工单 ,工作中心 查询合计生产数量
        public double? select_SumbyPronHeaderId(string PronoteHeaderId, string WorkHouseId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderId", PronoteHeaderId);
            ht.Add("WorkHouseId", WorkHouseId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<double>("ProduceInDepotDetail.select_sumbyPronHeaderIdWorkHouse", ht);
        }

        //加工单 ,工作中心 查询合计合格生产数量
        public double? select_CheckOutSumByPronHeaderId(string PronoteHeaderId, string WorkHouseId, string ProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderId", PronoteHeaderId);
            ht.Add("WorkHouseId", WorkHouseId);
            ht.Add("ProductId", ProductId);
            return sqlmapper.QueryForObject<double>("ProduceInDepotDetail.select_checkOutSumyPronHeaderIdWorkHouse", ht);
        }

        public double? select_FrontSumByProduceIndepotIdAndProId(string ProduceIndepotId, string ProductId, int Inumber)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProduceIndepotId", ProduceIndepotId);
            ht.Add("ProductId", ProductId);
            ht.Add("Inumber", Inumber);
            return sqlmapper.QueryForObject<double>("ProduceInDepotDetail.select_FrontSumByProduceIndepotIdAndProId", ht);

        }

        public double? select_FrontCheckoutSumByProduceIndepotIdAndProId(string ProduceIndepotId, string ProductId, int Inumber)
        {
            Hashtable ht = new Hashtable();
            ht.Add("ProduceIndepotId", ProduceIndepotId);
            ht.Add("ProductId", ProductId);
            ht.Add("Inumber", Inumber);
            return sqlmapper.QueryForObject<double>("ProduceInDepotDetail.select_FrontCheckoutSumByProduceIndepotIdAndProId", ht);
        }

        //获取合计数量
        public double? Get_HJForColumnName(string PronoteHeaderId, string WorkHouseId, string ProductId, string ProduceIndepotId, int Inumber, DateTime InsertTime, string GetColumn)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderId", PronoteHeaderId);
            ht.Add("WorkHouseId", WorkHouseId);
            ht.Add("ProductId", ProductId);
            ht.Add("ProduceIndepotId", ProduceIndepotId);
            ht.Add("Inumber", Inumber);
            ht.Add("InsertTime", InsertTime.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("GetColumn", GetColumn);

            return sqlmapper.QueryForObject<double>("ProduceInDepotDetail.Get_HJForColumnName_Header", ht);
        }

        //加工单 ,工作中心,商品主键,添加时间 查询后面单据 用于更新累计合格的数量 累计生产数量
        public IList<Book.Model.ProduceInDepotDetail> select_NextbyPronHeaderId(string PronoteHeaderId, string WorkHouseId, string productid, DateTime insertTime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderId", PronoteHeaderId);
            ht.Add("WorkHouseId", WorkHouseId);
            ht.Add("productid", productid);
            ht.Add("insertTime", insertTime);
            return sqlmapper.QueryForList<Model.ProduceInDepotDetail>("ProduceInDepotDetail.select_NextbyPronHeaderIdWorkHouse", ht);
        }

        //查询价格区间
        public string GetSupplierProductPriceRange(string productId, string WorkHouseName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productid", productId);
            ht.Add("WorkHouseid", WorkHouseName);
            return sqlmapper.QueryForObject<string>("ProduceInDepotDetail.GetSupplierProductPriceRange", ht);
        }
    }
}
