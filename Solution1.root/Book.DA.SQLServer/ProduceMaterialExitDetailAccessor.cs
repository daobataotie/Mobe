//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitDetailAccessor.cs
// author: peidun
// create date：2010-1-6 10:26:19
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
    /// Data accessor of ProduceMaterialExitDetail
    /// </summary>
    public partial class ProduceMaterialExitDetailAccessor : EntityAccessor, IProduceMaterialExitDetailAccessor
    {
        public IList<Model.ProduceMaterialExitDetail> Select(Model.ProduceMaterialExit ProduceMaterialExit)
        {
            return sqlmapper.QueryForList<Model.ProduceMaterialExitDetail>("ProduceMaterialExitDetail.select_byProduceExitMaterialId", ProduceMaterialExit.ProduceMaterialExitId);
        }

        public IList<Book.Model.ProduceMaterialExitDetail> Select(string houseid, DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("houseid", houseid);
            ht.Add("startDate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceMaterialExitDetail>("ProduceMaterialExitDetail.SelectByHouseDates", ht);
        }

        public IList<Model.ProduceMaterialExitDetail> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialExitId0, string produceMaterialExitId1, Model.Product pId0, Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            Hashtable ht = new Hashtable();
            ht.Add("starDate", starDate);
            ht.Add("endDate", endDate);
            ht.Add("produceMaterialExitId0", produceMaterialExitId0);
            ht.Add("produceMaterialExitId1", produceMaterialExitId1);
            ht.Add("pId0", pId0 == null ? null : pId0.ProductName);
            ht.Add("pId1", pId1 == null ? null : pId1.ProductName);
            ht.Add("dId0", departmentId0);
            ht.Add("dId1", departmentId1);
            ht.Add("pronoteId0", PronoteHeaderId0);
            ht.Add("pronoteId1", PronoteHeaderId1);
            return sqlmapper.QueryForList<Model.ProduceMaterialExitDetail>("ProduceMaterialExitDetail.selectByCondition", ht);
        }
        public void Delete(Model.ProduceMaterialExit produceMaterialExit)
        {
            sqlmapper.Delete("ProduceMaterialExitDetail.selectByCondition", produceMaterialExit.ProduceMaterialExitId);
        }

        public double SelectSumQtyFromZuzhuang(string productId, DateTime dateStart, DateTime dateEnd, string workHouseId, string allInvoiceXOIds)
        {
            if (string.IsNullOrEmpty(allInvoiceXOIds))
                allInvoiceXOIds = "''";

            string sql = "select sum(ISNULL(ped.ProduceQuantity,0)) from ProduceMaterialExitDetail ped left join ProduceMaterialExit pe on ped.ProduceMaterialExitId=pe.ProduceMaterialExitId where ped.ProductId='" + productId + "' and pe.ProduceExitMaterialDate between '" + dateStart + "' and '" + dateEnd + "' and pe.WorkHouseId='" + workHouseId + "' and (pe.customerinvoicexoid in (select CustomerInvoiceXOId from invoicexo where InvoiceId in (" + allInvoiceXOIds + ")) or ped.InvoiceXOId in (" + allInvoiceXOIds + "))";

            return Convert.ToDouble(this.QueryObject(sql));
        }

        public IList<Model.ProduceMaterialExitDetail> SelectSumQtyFromZuzhuangByPros(string productIds, DateTime dateStart, DateTime dateEnd, string workHouseId, string allInvoiceXOIds)
        {
            if (string.IsNullOrEmpty(allInvoiceXOIds))
                allInvoiceXOIds = "''";

            //string sql = "select sum(ISNULL(ped.ProduceQuantity,0)) from ProduceMaterialExitDetail ped left join ProduceMaterialExit pe on ped.ProduceMaterialExitId=pe.ProduceMaterialExitId where ped.ProductId in (" + productIds + ") and pe.ProduceExitMaterialDate between '" + dateStart + "' and '" + dateEnd + "' and pe.WorkHouseId='" + workHouseId + "' and pe.customerinvoicexoid in (select CustomerInvoiceXOId from invoicexo where InvoiceId in (" + allInvoiceXOIds + "))";

            string sql = "select  ped.ProductId,sum(ISNULL(ped.ProduceQuantity,0)) as ProduceQuantity from ProduceMaterialExitDetail ped left join ProduceMaterialExit pe on ped.ProduceMaterialExitId=pe.ProduceMaterialExitId where ped.ProductId in (" + productIds + ") and pe.ProduceExitMaterialDate between '" + dateStart + "' and '" + dateEnd + "' and pe.WorkHouseId='" + workHouseId + "' and (pe.customerinvoicexoid in (select CustomerInvoiceXOId from invoicexo where InvoiceId in (" + allInvoiceXOIds + ")) or ped.InvoiceXOId in (" + allInvoiceXOIds + ")) group by ped.ProductId";

            return this.DataReaderBind<Model.ProduceMaterialExitDetail>(sql, null, CommandType.Text);
        }

        public double SelectSumQtyFromZuzhuangAll(string productId, DateTime dateEnd, string workHouseId)
        {
            string sql = "select sum(ISNULL(ped.ProduceQuantity,0)) from ProduceMaterialExitDetail ped left join ProduceMaterialExit pe on ped.ProduceMaterialExitId=pe.ProduceMaterialExitId where ped.ProductId='" + productId + "' and pe.ProduceExitMaterialDate <='" + dateEnd + "' and pe.WorkHouseId='" + workHouseId + "'";

            return Convert.ToDouble(this.QueryObject(sql));
        }

        public IList<Book.Model.ProduceMaterialExitDetail> SelectForListForm(DateTime startDate, DateTime endDate, string startPMEId, string endPMEId, string startPronoteHeaderId, string endPronoteHeaderId, Book.Model.Product startProduct, Book.Model.Product endProduct, string workhouseId, string invoiceXOCusId, string handBookId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" AND pe.ProduceExitMaterialDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");

            if (!string.IsNullOrEmpty(startPMEId) && !string.IsNullOrEmpty(endPMEId))
            {
                sb.Append(" AND ped.ProduceMaterialExitId BETWEEN '" + startPMEId + "' AND '" + endPMEId + "'");
            }

            if (!string.IsNullOrEmpty(startPronoteHeaderId) && !string.IsNullOrEmpty(endPronoteHeaderId))
            {
                sb.Append(" and ped.PronoteHeaderID between '" + startPronoteHeaderId + "' and '" + endPronoteHeaderId + "'");
            }

            if (startProduct != null & endProduct != null)
            {
                sb.Append(" and ped.ProductId in (select ProductId from Product where Id between '" + startProduct.Id + "' and '" + endProduct.Id + "')");
            }
            if (!string.IsNullOrEmpty(workhouseId))
            {
                sb.Append(" and pe.WorkHouseId ='" + workhouseId + "'");
            }
            if (!string.IsNullOrEmpty(invoiceXOCusId))
            {
                sb.Append(" and ped.InvoiceXOId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + invoiceXOCusId + "')");
            }
            if (!string.IsNullOrEmpty(handBookId))
            {
                sb.Append(" and ped.HandbookId='" + handBookId + "'");
            }

            return sqlmapper.QueryForList<Model.ProduceMaterialExitDetail>("ProduceMaterialExitDetail.SelectForListForm", sb.ToString());
        }

        public DataTable SelectForExcel(DateTime startDate, DateTime endDate, string startPMEId, string endPMEId, string startPronoteHeaderId, string endPronoteHeaderId, Book.Model.Product startProduct, Book.Model.Product endProduct, string workhouseId, string invoiceXOCusId, string handBookId)
        {
            StringBuilder sb = new StringBuilder("select xo.CustomerInvoiceXOId,p1.CustomerProductName,ph.PronoteDate,ph.PronoteHeaderID,p1.Id,p1.ProductName,ph.InvoiceXODetailQuantity,ph.DetailsSum,p2.Id as MId,p2.ProductName as MProductName,(select sum(pmd.Materialprocesedsum) from ProduceMaterialdetails pmd  where pmd.PronoteHeaderID=ph.PronoteHeaderID and pmd.ProductId=p2.ProductId) as Materialprocesedsum ,(select top 1 pm.ProduceMaterialDate  from ProduceMaterialdetails pmd left join ProduceMaterial pm on pm.ProduceMaterialID=pmd.ProduceMaterialID  where pmd.PronoteHeaderID=ph.PronoteHeaderID and pmd.ProductId=p2.ProductId) as ProduceMaterialDate,pe.ProduceExitMaterialDate,ped.ProduceQuantity,wh.Workhousename,ped.HandbookId,ped.HandbookProductId,ph.MRSHeaderId,p2.ProductDescription,p2.ProductId from ProduceMaterialExitDetail ped left join ProduceMaterialExit pe on pe.ProduceMaterialExitId=ped.ProduceMaterialExitId left join PronoteHeader ph on ph.PronoteHeaderID=ped.PronoteHeaderId left join InvoiceXO xo on xo.InvoiceId=ph.InvoiceXOId left join Product p1 on p1.ProductId=ph.ProductId left join Product p2 on p2.ProductId=ped.ProductId left join WorkHouse wh on wh.WorkHouseId=pe.WorkHouseId where 1=1 ");
            sb.Append(" AND pe.ProduceExitMaterialDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");

            if (!string.IsNullOrEmpty(startPMEId) && !string.IsNullOrEmpty(endPMEId))
            {
                sb.Append(" AND ped.ProduceMaterialExitId BETWEEN '" + startPMEId + "' AND '" + endPMEId + "'");
            }

            if (!string.IsNullOrEmpty(startPronoteHeaderId) && !string.IsNullOrEmpty(endPronoteHeaderId))
            {
                sb.Append(" and ped.PronoteHeaderID between '" + startPronoteHeaderId + "' and '" + endPronoteHeaderId + "'");
            }

            if (startProduct != null & endProduct != null)
            {
                sb.Append(" and p2.Id between '" + startProduct.Id + "' and '" + endProduct.Id + "'");
            }
            if (!string.IsNullOrEmpty(workhouseId))
            {
                sb.Append(" and pe.WorkHouseId ='" + workhouseId + "'");
            }
            if (!string.IsNullOrEmpty(invoiceXOCusId))
            {
                sb.Append(" and xo.CustomerInvoiceXOId='" + invoiceXOCusId + "'");
            }
            if (!string.IsNullOrEmpty(handBookId))
            {
                sb.Append(" and ped.HandbookId='" + handBookId + "'");
            }
            sb.Append(" order by pe.ProduceMaterialExitId");

            SqlDataAdapter sda = new SqlDataAdapter(sb.ToString(), sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            return dt;
        }
    }
}
