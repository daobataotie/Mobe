//------------------------------------------------------------------------------
//
// file name：DepotOutDetailAccessor.cs
// author: mayanjun
// create date：2010-10-15 15:41:09
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
    /// Data accessor of DepotOutDetail
    /// </summary>
    public partial class DepotOutDetailAccessor : EntityAccessor, IDepotOutDetailAccessor
    {
        public IList<Model.DepotOutDetail> GetDepotOutDetailByDepotOutId(string depotOutId)
        {
            return sqlmapper.QueryForList<Model.DepotOutDetail>("DepotOutDetail.GetDepotOutDetailByDepotOutId", depotOutId);
        }
        public void Delete(Model.DepotOut depotOut)
        {
            sqlmapper.Delete("DepotOutDetail.deleteByHeader", depotOut.DepotOutId);
        }

        public IList<Model.DepotOutDetail> SelectByCondition(DateTime startDate, DateTime endDate, string DepotOutIdStart, string DepotOutIdEnd, string depotStart, string depotEnd)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate);
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(DepotOutIdStart) || !string.IsNullOrEmpty(DepotOutIdEnd))
            {
                if (!string.IsNullOrEmpty(DepotOutIdStart) && !string.IsNullOrEmpty(DepotOutIdEnd))
                    sql.Append("AND DepotOut.DepotOutId BETWEEN '" + DepotOutIdStart + "' AND '" + DepotOutIdEnd + "'");
                else
                    sql.Append("AND DepotOut.DepotOutId = '" + (string.IsNullOrEmpty(DepotOutIdStart) ? DepotOutIdEnd : DepotOutIdStart) + "'");
            }
            if (depotStart != null || depotEnd != null)
            {
                if (depotStart != null && depotEnd != null)
                    sql.Append("AND DepotOut.DepotId BETWEEN '" + depotStart + "' AND '" + depotEnd + "'");
                else
                    sql.Append("AND DepotOut.DepotId='" + (depotStart == null ? depotEnd : depotStart) + "'");
            }
            ht.Add("sql", sql);
            //if (!string.IsNullOrEmpty(InvoiceXOIdStart) || !string.IsNullOrEmpty(InvoiceXOIdEnd))
            //{
            //    if (!string.IsNullOrEmpty(InvoiceXOIdStart) && !string.IsNullOrEmpty(InvoiceXOIdEnd))
            //        sql.Append("");
            //    //else
            //        //sql.Append();
            //}
            return sqlmapper.QueryForList<Model.DepotOutDetail>("DepotOutDetail.SelectByCondition", ht);
        }

        public DataTable SelectOutAndInDepot(DateTime startDate, DateTime endDate, string depotStart, string depotEnd, string productCategoryStart, string productCategoryEnd, string ProductIdStart, string ProductIdEnd)
        {
            StringBuilder sql1 = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            StringBuilder sql3 = new StringBuilder();
            sql1.Append("SELECT di.DepotInDate AS Date,di.DepotInId AS InvoiceId,(SELECT Id FROM DepotPosition WHERE DepotPosition.DepotPositionId=dd.DepotPositionId) AS DepotPositionName,(SELECT DepotName FROM Depot WHERE Depot.DepotId=di.DepotId) AS DepotName,(SELECT ProductName FROM Product WHERE Product.ProductId=dd.ProductId) AS ProductName,dd.ProductUnit AS ProductUnit,isnull(dd.DepotInQuantity,0) AS Quantity FROM DepotInDetail dd LEFT JOIN DepotIn di ON di.DepotInId = dd.DepotInId WHERE di.DepotInDate BETWEEN '" + startDate + "' AND '" + endDate + "' ");
            sql2.Append("SELECT do.DepotOutDate AS Date,do.DepotOutId AS InvoiceId,(SELECT Id FROM DepotPosition WHERE DepotPosition.DepotPositionId=dd.DepotPositionId) AS DepotPositionName,(SELECT DepotName FROM Depot WHERE Depot.DepotId=do.DepotId) AS DepotName,(SELECT ProductName FROM Product WHERE Product.ProductId=dd.ProductId) AS ProductName,dd.ProductUnit AS ProductUnit,(0-isnull(dd.DepotOutDetailQuantity,0))AS Quantity FROM DepotOutDetail dd LEFT JOIN DepotOut do ON do.DepotOutId = dd.DepotOutId WHERE do.DepotOutDate BETWEEN '" + startDate + "' AND '" + endDate + "'");
            if (!string.IsNullOrEmpty(depotStart) || !string.IsNullOrEmpty(depotEnd))
            {
                if (!string.IsNullOrEmpty(depotStart) && !string.IsNullOrEmpty(depotEnd))
                {
                    sql1.Append(" AND di.DepotId IN (SELECT DepotId FROM Depot WHERE Id BETWEEN '" + depotStart + "' AND '" + depotEnd + "')");
                    sql2.Append(" AND do.DepotId IN (SELECT DepotId FROM Depot WHERE Id BETWEEN '" + depotStart + "' AND '" + depotEnd + "')");
                }
                else
                {
                    sql1.Append(" AND di.DepotId IN (SELECT DepotId FROM Depot WHERE Id = '" + (string.IsNullOrEmpty(depotStart) ? depotEnd : depotStart) + "')");
                    sql2.Append(" AND do.DepotId IN (SELECT DepotId FROM Depot WHERE Id = '" + (string.IsNullOrEmpty(depotStart) ? depotEnd : depotStart) + "')");
                }
            }
            if (!string.IsNullOrEmpty(productCategoryStart) || !string.IsNullOrEmpty(productCategoryEnd))
            {
                if (string.IsNullOrEmpty(productCategoryStart) && string.IsNullOrEmpty(productCategoryEnd))
                {
                    sql3.Append("  AND dd.ProductId IN (SELECT ProductId FROM Product WHERE ProductCategoryId IN (SELECT ProductCategoryId FROM ProductCategory WHERE  Id BETWEEN '" + productCategoryStart + "' AND '" + productCategoryEnd + "'))");
                }
                else
                {
                    sql3.Append(" AND dd.ProductId IN (SELECT ProductId FROM Product WHERE ProductCategoryId IN (SELECT ProductCategoryId FROM ProductCategory WHERE  Id='" + (string.IsNullOrEmpty(productCategoryStart) ? productCategoryEnd : productCategoryStart) + "'))");
                }
            }
            if (!string.IsNullOrEmpty(ProductIdStart) || !string.IsNullOrEmpty(ProductIdEnd))
            {
                if (!string.IsNullOrEmpty(ProductIdStart) && !string.IsNullOrEmpty(ProductIdEnd))
                    sql3.Append(" AND dd.ProductId IN(SELECT ProductId FROM Product WHERE Id BETWEEN '" + ProductIdStart + "' AND '" + ProductIdEnd + "')");
                else
                    sql3.Append(" AND dd.ProductId IN(SELECT ProductId FROM Product WHERE Id= '" + (string.IsNullOrEmpty(ProductIdStart) ? ProductIdEnd : ProductIdStart) + "')");
            }

            string sql = sql1.ToString() + sql3.ToString() + " UNION All " + sql2.ToString() + sql3.ToString() + " ORDER BY InvoiceId,Date";

            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public IList<Model.DepotOutDetail> SelectByDepotOutId(string id)
        {
            return sqlmapper.QueryForList<Model.DepotOutDetail>("DepotOutDetail.SelectByDepotOutId", id);
        }

        public IList<Model.DepotOutDetail> SelectByDateRange(DateTime startDate, DateTime endDate, string productid, string invoiceCusId)
        {
            //StringBuilder sql = new StringBuilder("select d.DepotOutId,d.DepotOutDate,d.SourceType,d.InvioiceId,dd.DepotOutDetailQuantity,e.EmployeeName,p.ProductName,dp.Id from DepotOutDetail dd left join DepotOut d on dd.DepotOutId=d.DepotOutId left join Employee e on e.EmployeeId=d.EmployeeId left join DepotPosition dp on dp.DepotPositionId=dd.DepotPositionId left join Product p on p.ProductId=dd.ProductId where 1=1");
            StringBuilder sql = new StringBuilder("select d.DepotOutId,d.DepotOutDate,d.SourceType,d.InvioiceId,dd.DepotOutDetailQuantity,e.EmployeeName,p.ProductName,dp.Id,(isnull(xo.CustomerInvoiceXOId,'')+isnull(xo2.CustomerInvoiceXOId,'')) as invoiceCusId from DepotOutDetail dd left join DepotOut d on dd.DepotOutId=d.DepotOutId left join Employee e on e.EmployeeId=d.EmployeeId left join DepotPosition dp on dp.DepotPositionId=dd.DepotPositionId left join Product p on p.ProductId=dd.ProductId left join ProduceMaterial pm on d.InvioiceId=pm.ProduceMaterialID left join InvoiceXO xo on pm.InvoiceXOId=xo.InvoiceId Left join ProduceOtherMaterial pom on d.InvioiceId=pom.ProduceOtherMaterialId left join ProduceOtherCompact poc on pom.ProduceOtherCompactId=poc.ProduceOtherCompactId left join InvoiceXO xo2 on poc.InvoiceXOId=xo2.InvoiceId where 1=1");
            sql.Append(" And d.DepotOutDate between '" + startDate.ToString("yyyy-MM-dd") + "' and '" + endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (!string.IsNullOrEmpty(productid))
                sql.Append(" And dd.ProductId='" + productid + "'");
            if (!string.IsNullOrEmpty(invoiceCusId))
                sql.Append(" And (isnull(xo.CustomerInvoiceXOId,'')+isnull(xo2.CustomerInvoiceXOId,'')) ='" + invoiceCusId + "'");
            sql.Append(" order by DepotOutId desc");
            return DataReaderBind<Model.DepotOutDetail>(sql.ToString(), null, CommandType.Text);
        }
    }
}
