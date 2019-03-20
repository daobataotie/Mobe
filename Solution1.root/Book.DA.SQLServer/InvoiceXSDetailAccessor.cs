//------------------------------------------------------------------------------
//
// file name:InvoiceXSDetailAccessor.cs
// author: peidun
// create date:2008/6/6 10:00:50
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
    /// Data accessor of InvoiceXSDetail
    /// </summary>
    public partial class InvoiceXSDetailAccessor : EntityAccessor, IInvoiceXSDetailAccessor
    {
        public IList<Book.Model.InvoiceXSDetail> Select(Book.Model.InvoiceXS invoiceXS)
        {
            return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.select_by_invoiceid", invoiceXS.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceXS invoice)
        {
            sqlmapper.Delete("InvoiceXSDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        public IList<Book.Model.InvoiceXSDetail> Select(DateTime startDate, DateTime endDate, string csid, string ceid, string psid, string peid)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startDate", startDate);
            pars.Add("endDate", endDate);
            pars.Add("csid", csid);
            pars.Add("ceid", ceid);
            pars.Add("psid", psid);
            pars.Add("peid", peid);
            return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.selectbyDateReangeAndProductReangeCompanyReange", pars);

        }

        public IList<Book.Model.InvoiceXSDetail> Select(Model.InvoiceXO invoiceXO)
        {
            return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.select_count", invoiceXO.InvoiceId);
        }

        public IList<Model.InvoiceXSDetail> Select(Model.InvoiceXS invoiceXS, string productStart, string productEnd)
        {
            IList<Book.Model.InvoiceXSDetail> invoicexs = null;
            Hashtable ht = new Hashtable();

            ht.Add("invoiceId", invoiceXS.InvoiceId);
            ht.Add("productStart", productStart);
            ht.Add("productEnd", productEnd);

            if (string.IsNullOrEmpty(productEnd))
            {
                invoicexs = sqlmapper.QueryForList<Book.Model.InvoiceXSDetail>("InvoiceXSDetail.selectByProductIdQuJianEndNULL", ht);
            }
            else
            {
                invoicexs = sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.selectByProductIdQuJian", ht);
            }
            return invoicexs;
        }

        public IList<Book.Model.InvoiceXSDetail> Select(DateTime startDate, DateTime endDate, Model.Employee employee, Model.Customer customer, Model.Depot depot)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startdate", startDate);
            pars.Add("enddate", endDate);
            pars.Add("customerid", customer == null ? null : customer.CustomerId);
            pars.Add("employeeId", employee == null ? null : employee.EmployeeId);
            pars.Add("depotid", depot == null ? null : depot.DepotId);
            return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.selectByCustomEmpDepetQuJian", pars);

        }

        public Model.InvoiceXSDetail GetByProIdPosIdInvoiceId(string productId, string positionId, string invoiceId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("positionId", positionId);
            ht.Add("invoiceId", invoiceId);
            return sqlmapper.QueryForObject<Model.InvoiceXSDetail>("InvoiceXSDetail.GetByProIdPosIdInvoiceId", ht);
        }

        public double GetSumByProductIdAndInvoiceId(string productId, string invoiceId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", productId);
            ht.Add("invoiceId", invoiceId);
            return sqlmapper.QueryForObject<double>("InvoiceXSDetail.GetSumByProductIdAndInvoiceId", ht);
        }

        public IList<Model.InvoiceXSDetail> Selectbyinvoiceidfz(Model.InvoiceXS inovicexs)
        {
            return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.selectbyinvoiceidfz", inovicexs.InvoiceId);
        }

        public IList<Book.Model.InvoiceXSDetail> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.SelectByDateRange", ht);
        }

        public IList<Book.Model.InvoiceXSDetail> SelectbyConditionX(DateTime StartDate, DateTime EndDate, DateTime Yjri1, DateTime Yjri2, Book.Model.Customer Customer1, Book.Model.Customer Customer2, string XOId1, string XOId2, Book.Model.Product Product, Book.Model.Product Product2, string CusXOId, int OrderColumn, int OrderType)
        {
            StringBuilder sb = new StringBuilder();
            if (Product != null && Product2 != null)
                sb.Append(" AND ProductId BETWEEN '" + Product.Id + "' AND '" + Product2.Id + "'");
            if (!string.IsNullOrEmpty(CusXOId))
                sb.Append(" AND InvoiceXOId IN (SELECT InvoiceId FROM InvoiceXO WHERE CustomerInvoiceXOId = '" + CusXOId + "')");
            sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceXS WHERE InvoiceDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
            if (Yjri1 != global::Helper.DateTimeParse.NullDate && Yjri2 != global::Helper.DateTimeParse.EndDate)
                sb.Append(" AND InvoiceXOId IN (SELECT InvoiceId FROM InvoiceXO WHERE InvoiceYjrq BETWEEN '" + Yjri1.ToString("yyyy-MM-dd") + "' AND '" + Yjri2.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
            if (Customer1 != null && Customer2 != null)
                sb.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceXS WHERE CustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + Customer1.Id + "' AND '" + Customer2.Id + "'))");
            if (!string.IsNullOrEmpty(XOId1) && !string.IsNullOrEmpty(XOId2))
                sb.Append(" AND InvoiceId BETWEEN '" + XOId1 + "' AND '" + XOId2 + "'");

            return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.SelectbyConditionX", sb.ToString());
        }

        public DataTable SelectbyConditionXBiao(DateTime StartDate, DateTime EndDate, DateTime Yjri1, DateTime Yjri2, Book.Model.Customer Customer1, Book.Model.Customer Customer2, string XOId1, string XOId2, Book.Model.Product Product, Book.Model.Product Product2, string CusXOId, int OrderColumn, int OrderType)
        {
            StringBuilder sb_xs = new StringBuilder("SELECT InvoiceId AS CHDH,(SELECT InvoiceDate FROM InvoiceXS WHERE InvoiceId = InvoiceXSDetail.InvoiceId) AS CHRQ,(SELECT ProductName FROM Product WHERE ProductId = InvoiceXSDetail.ProductId) AS ProductName,(SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE InvoiceId = InvoiceXOId ) AS KHDDBH,InvoiceXSDetailQuantity AS BCCHSL,InvoiceProductUnit AS DanWei,InvoiceXSDetailPrice AS DanJia,InvoiceAllowance AS ZheRang,InvoiceXSDetailMoney AS JinE,InvoiceXSDetailTax AS ShuiE,InvoiceXSDetailTaxMoney AS YingShou FROM InvoiceXSDetail WHERE 1 = 1 ");
            StringBuilder sb_xt = new StringBuilder("SELECT InvoiceId AS CHDH,(SELECT InvoiceDate FROM InvoiceXT WHERE InvoiceId = InvoiceXTDetail.InvoiceId) AS CHRQ,(SELECT ProductName FROM Product WHERE ProductId = InvoiceXTDetail.ProductId ) AS ProductName,(SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE InvoiceId = InvoiceXOId) AS KHDDBH,InvoiceXTDetailQuantity AS BCCHSL,InvoiceProductUnit AS DanWei,InvoiceXTDetailPrice AS DanJia,InvoiceXTDetailDiscount AS ZheRang,(0-InvoiceXTDetailMoney1) AS JinE,(0-ISNULl(InvoiceXTDetailPrice,0)*isnull(InvoiceXTDetailQuantity,0)*0.05) AS ShuiE,(0-InvoiceXTDetailMoney0)+(0-ISNULl(InvoiceXTDetailPrice,0)*isnull(InvoiceXTDetailQuantity,0)*0.05) AS YingShou FROM InvoiceXTDetail WHERE 1 = 1 ");

            //时间日期
            sb_xs.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceXS WHERE InvoiceDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");
            sb_xt.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceXT WHERE InvoiceDate BETWEEN '" + StartDate.ToString("yyyy-MM-dd") + "' AND '" + EndDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");

            //预交日期
            if (Yjri1 != global::Helper.DateTimeParse.NullDate && Yjri2 != global::Helper.DateTimeParse.EndDate)
                sb_xs.Append(" AND InvoiceXOId IN (SELECT InvoiceId FROM InvoiceXO WHERE InvoiceYjrq BETWEEN '" + Yjri1.ToString("yyyy-MM-dd") + "' AND '" + Yjri2.Date.AddDays(1).ToString("yyyy-MM-dd") + "')");

            //客户
            if (Customer1 != null && Customer2 != null)
            {
                sb_xs.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceXS WHERE CustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + Customer1.Id + "' AND '" + Customer2.Id + "'))");
                sb_xt.Append(" AND InvoiceId IN (SELECT InvoiceId FROM InvoiceXT WHERE CustomerId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + Customer1.Id + "' AND '" + Customer2.Id + "'))");
            }

            //头编号
            if (!string.IsNullOrEmpty(XOId1) && !string.IsNullOrEmpty(XOId2))
                sb_xs.Append(" AND InvoiceId BETWEEN '" + XOId1 + "' AND '" + XOId2 + "'");

            //客户订单编号
            if (!string.IsNullOrEmpty(CusXOId))
            {
                sb_xs.Append(" AND InvoiceXOId IN (SELECT InvoiceId FROM InvoiceXO WHERE CustomerInvoiceXOId = '" + CusXOId + "')");
                sb_xt.Append(" AND InvoiceXOId IN (SELECT InvoiceId FROM InvoiceXO WHERE CustomerInvoiceXOId = '" + CusXOId + "')");
            }
            //商品
            if (Product != null && Product2 != null)
            {
                sb_xs.Append(" AND ProductId IN (SELECT Product.ProductId FROM Product WHERE Id BETWEEN '" + Product.Id + "' AND '" + Product2.Id + "')");
                sb_xt.Append(" AND ProductId IN (SELECT Product.ProductId FROM Product WHERE Id BETWEEN '" + Product.Id + "' AND '" + Product2.Id + "')");
            }

            string sql = sb_xs.ToString() + " UNION " + sb_xt.ToString() + "order by CHDH,CHRQ";

            using (SqlConnection con = new SqlConnection(sqlmapper.DataSource.ConnectionString))
            {
                SqlDataAdapter sda = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 通过手册号和项号计算对应订单出货总量
        /// </summary>
        /// <param name="handbookId"></param>
        /// <param name="handbookProductId"></param>
        /// <returns></returns>
        public double SumBeeQuantityByHandbook(string handbookId, string handbookProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("HandbookId", handbookId);
            ht.Add("HandbookProductId", handbookProductId);
            return sqlmapper.QueryForObject<double>("InvoiceXSDetail.SumBeeQuantityByHandbook", ht);
        }

        public double GetInvoiceXSDetailQuantity(string id)
        {
            return sqlmapper.QueryForObject<double>("InvoiceXSDetail.GetInvoiceXSDetailQuantity", id);
        }

        public IList<Model.InvoiceXSDetail> SelectByBGHandBook(DateTime startDate, DateTime endDate, string bgHandBookId, string bgProductId, string productId, string cusXOId)
        {
            StringBuilder sb = new StringBuilder("select xsd.HandbookId,xsd.HandbookProductId,xsd.ProductId,p.Id as PId,p.ProductName,p.CustomerProductName,xo.CustomerInvoiceXOId,SUM(xsd.InvoiceXSDetailQuantity) as InvoiceXSDetailQuantity,ph.PronoteHeaderID  from InvoiceXSDetail xsd left join InvoiceXS xs on xsd.InvoiceId=xs.InvoiceId left join InvoiceXO xo on xsd.InvoiceXOId=xo.InvoiceId left join PronoteHeader ph on ph.InvoiceXOId=xo.InvoiceId and ph.ProductId=xsd.ProductId left join Product p on xsd.ProductId=p.ProductId where xsd.HandbookId  is not null and xsd.HandbookProductId is not null and xs.InvoiceDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            if (!string.IsNullOrEmpty(bgHandBookId))
            {
                sb.Append(" and xsd.HandbookId in (" + bgHandBookId + ")");
            }
            if (!string.IsNullOrEmpty(bgProductId))
            {
                sb.Append(" and HandbookProductId='" + bgProductId + "'");
            }
            if (!string.IsNullOrEmpty(productId))
            {
                sb.Append(" and xsd.ProductId='" + productId + "'");
            }
            if (!string.IsNullOrEmpty(cusXOId))
            {
                sb.Append(" and xo.CustomerInvoiceXOId='" + cusXOId + "'");
            }

            sb.Append(" group by xsd.HandbookId,xsd.HandbookProductId,xsd.ProductId,p.Id,p.ProductName,p.CustomerProductName,xo.CustomerInvoiceXOId,ph.PronoteHeaderID order by xsd.HandbookId,xsd.HandbookProductId");

            return this.DataReaderBind<Model.InvoiceXSDetail>(sb.ToString(), null, CommandType.Text);
        }

        //年度出货查询
        public DataTable SelectAnnualShipment(string ProductId, DateTime StartDate, DateTime EndDate, string CustomerId, int showType)
        {
            string sql = "";
            if (showType == 0)
                sql = "select YEAR(x.InvoiceDate) as ShipmentYear, sum(isnull(xd.InvoiceXSDetailQuantity,0)) as ShipmentQuantity,xd.ProductId from InvoiceXSDetail xd left join InvoiceXS x on xd.InvoiceId=x.InvoiceId where xd.ProductId='" + ProductId + "' and x.InvoiceDate between '" + StartDate.ToString("yyyy-MM-dd") + "' and '" + EndDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' group by YEAR(x.InvoiceDate),x.CustomerId,xd.ProductId order by YEAR(x.InvoiceDate)";
            else
                sql = "select (cast(year(x.InvoiceDate) as varchar(10))+'.'+cast(month(x.InvoiceDate) as varchar(10))) as ShipmentYear, sum(isnull(xd.InvoiceXSDetailQuantity,0)) as ShipmentQuantity,xd.ProductId from InvoiceXSDetail xd left join InvoiceXS x on xd.InvoiceId=x.InvoiceId where xd.ProductId='" + ProductId + "' and x.InvoiceDate between '" + StartDate.ToString("yyyy-MM-dd") + "' and '" + EndDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "' group by (cast(year(x.InvoiceDate) as varchar(10))+'.'+cast(month(x.InvoiceDate) as varchar(10))),x.CustomerId,xd.ProductId order by (cast(year(x.InvoiceDate) as varchar(10))+'.'+cast(month(x.InvoiceDate) as varchar(10)))";
            try
            {
                using (SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString))
                {

                    SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
