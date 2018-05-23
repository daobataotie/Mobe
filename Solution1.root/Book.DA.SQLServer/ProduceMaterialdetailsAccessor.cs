//------------------------------------------------------------------------------
//
// file name：ProduceMaterialdetailsAccessor.cs
// author: peidun
// create date：2009-12-30 16:33:31
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
    /// Data accessor of ProduceMaterialdetails
    /// </summary>
    public partial class ProduceMaterialdetailsAccessor : EntityAccessor, IProduceMaterialdetailsAccessor
    {
        public IList<Book.Model.ProduceMaterialdetails> Select(Model.ProduceMaterial produceMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.select_byProduceMaterialID", produceMaterial.ProduceMaterialID);
        }

        public IList<Book.Model.ProduceMaterialdetails> SelectByState(Model.ProduceMaterial produceMaterial)
        {
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.select_byState", produceMaterial.ProduceMaterialID);
        }

        public IList<Book.Model.ProduceMaterialdetails> Select(string houseid, DateTime startDate, DateTime endDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("houseid", houseid);
            ht.Add("startDate", startDate);
            ht.Add("enddate", endDate);
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.SelectByHouseDates", ht);
        }

        public IList<Model.ProduceMaterialdetails> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, string pId0, string pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            Hashtable ht = new Hashtable();
            ht.Add("starDate", starDate);
            ht.Add("endDate", endDate);
            ht.Add("produceMaterialId0", produceMaterialId0);
            ht.Add("produceMaterialId1", produceMaterialId1);
            ht.Add("pId0", pId0);
            ht.Add("pId1", pId1);
            ht.Add("dId0", departmentId0);
            ht.Add("dId1", departmentId1);
            ht.Add("pronoteId0", PronoteHeaderId0);
            ht.Add("pronoteId1", PronoteHeaderId1);
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.selectBycondition", ht);
        }

        public Model.ProduceMaterialdetails SelectByProductIdAndHeadId(string productId, string produceMaterialId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("produceMaterialId", produceMaterialId);
            ht.Add("productId", productId);
            return sqlmapper.QueryForObject<Model.ProduceMaterialdetails>("ProduceMaterialdetails.selectByproductIdAndHeadId", ht);
        }

        public IList<Model.ProduceMaterialdetails> SelectByProductIdAndHeadId(Model.Product pId0, Model.Product pId1, string produceMaterialId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * ");
            sb.Append(" ,(SELECT ProductName FROM Product WHERE Product.ProductId = ProduceMaterialdetails.ProductId) AS ProductName");
            sb.Append(" ,(SELECT CustomerProductName FROM Product WHERE Product.ProductId = ProduceMaterialdetails.ProductId) AS CustomerProductName");
            sb.Append(" FROM ProduceMaterialdetails WHERE ProduceMaterialID='" + produceMaterialId + "'");
            if (pId0 != null && pId1 != null)
            {
                sb.Append(" AND ProductId IN (SELECT ProductId FROM Product WHERE Id BETWEEN '" + pId0.Id + "' AND '" + pId1.Id + "')");
            }

            return this.DataReaderBind<Model.ProduceMaterialdetails>(sb.ToString(), null, CommandType.Text);

            #region 注释
            //Hashtable ht = new Hashtable();
            //ht.Add("produceMaterialId", produceMaterialId);
            //ht.Add("pId0", pId0 == null ? null : pId0.ProductName);
            //ht.Add("pId1", pId1 == null ? null : pId1.ProductName);
            //return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.selectByproductIdAndHeadIdRange", ht);
            #endregion
        }

        public double GetMaterialprocesedsumForPDMId(string PDMid)
        {
            return sqlmapper.QueryForObject<double>("ProduceMaterialdetails.GetMaterialprocesedsumForPDMId", PDMid);
        }

        public IList<Model.ProduceMaterialdetails> SelectTotalByProduceMaterialID(Model.Product pId0, Model.Product pId1, string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT (SELECT ProductName FROM Product WHERE Product.ProductId = ProduceMaterialdetails.ProductId) AS ProductName");
            sb.Append(",sum(Materialprocessum) AS Materialprocessum,sum(Materialprocesedsum) AS Materialprocesedsum");
            sb.Append(" FROM ProduceMaterialdetails WHERE ProduceMaterialID in (" + str + ")");
            if (pId0 != null && pId1 != null)
            {
                sb.Append(" AND ProductId IN (SELECT ProductId FROM Product WHERE Id BETWEEN '" + pId0.Id + "' AND '" + pId1.Id + "')");
            }
            sb.Append("GROUP BY ProduceMaterialdetails.ProductId");
            return this.DataReaderBind<Model.ProduceMaterialdetails>(sb.ToString(), null, CommandType.Text);
        }

        public IList<Model.ProduceMaterialdetails> SelectForDistributioned(string productid, DateTime InsertTime)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productid", productid);
            ht.Add("InsertTime", InsertTime);
            return sqlmapper.QueryForList<Model.ProduceMaterialdetails>("ProduceMaterialdetails.SelectForDistributioned", ht);
        }

        public IList<Model.ProduceMaterialdetails> SelectBycondition2(DateTime startDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, Book.Model.Product pId0, Book.Model.Product pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1, string CusInvoiceXOId)
        {
            SqlParameter[] parames = { 
                    new SqlParameter("@startDate", DbType.DateTime), 
                    new SqlParameter("@endDate", DbType.DateTime), 
                    new SqlParameter("@produceMaterialId0", DbType.String), 
                    new SqlParameter("@produceMaterialId1", DbType.String),
                    new SqlParameter("@pId0", DbType.String), 
                    new SqlParameter("@pId1", DbType.String), 
                    new SqlParameter("@departmentId0", DbType.String), 
                    new SqlParameter("@departmentId1", DbType.String),
                    new SqlParameter("@PronoteHeaderId0", DbType.String), 
                    new SqlParameter("@PronoteHeaderId1", DbType.String),
                    new SqlParameter("@CusInvoiceXOId",DbType.String)
                                     };

            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (!string.IsNullOrEmpty(produceMaterialId0))
                parames[2].Value = produceMaterialId0;
            else
                parames[2].Value = DBNull.Value;

            if (!string.IsNullOrEmpty(produceMaterialId1))
                parames[3].Value = produceMaterialId1;
            else
                parames[3].Value = DBNull.Value; ;

            if (pId0 != null)
                parames[4].Value = pId0.Id;
            else
                parames[4].Value = DBNull.Value;
            if (pId1 != null)
                parames[5].Value = pId1.Id;
            else
                parames[5].Value = DBNull.Value;

            if (departmentId0 != null)
                parames[6].Value = departmentId0;
            else
                parames[6].Value = DBNull.Value;

            if (departmentId1 != null)
                parames[7].Value = departmentId1;
            else
                parames[7].Value = DBNull.Value;

            if (PronoteHeaderId0 != null)
                parames[8].Value = PronoteHeaderId0;
            else
                parames[8].Value = DBNull.Value;
            if (PronoteHeaderId1 != null)
                parames[9].Value = PronoteHeaderId1;
            else
                parames[9].Value = DBNull.Value;

            if (!string.IsNullOrEmpty(CusInvoiceXOId))
                parames[10].Value = CusInvoiceXOId;
            else
                parames[10].Value = DBNull.Value;
            //  sql.Append(" from ProduceMaterial p left join  Workhouse w on w.WorkHouseId=p.WorkHouseId right join ProduceMaterialdetails d on p.ProduceMaterialID = d.ProduceMaterialID left join Product pro on d.ProductId = pro.ProductId");

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT p.ProduceMaterialID,p.ProduceMaterialDate,p.ProduceMaterialdesc, w.Workhousename as WorkhouseName,pro.Id as PID,pro.ProductName  ");
            sql.Append(", (SELECT CustomerInvoiceXOId FROM InvoiceXO WHERE InvoiceXO.InvoiceId = p.InvoiceXOId) AS CusXOId");
            sql.Append(" from ProduceMaterialdetails pd left join ProduceMaterial p on pd.ProduceMaterialID=p.ProduceMaterialID left join  Workhouse w on w.WorkHouseId=p.WorkHouseId left join Product pro on pd.ProductId=pro.ProductId");
            sql.Append("  WHERE p.ProduceMaterialDate between @startDate  and @endDate ");
            if (!string.IsNullOrEmpty(produceMaterialId0) && !string.IsNullOrEmpty(produceMaterialId1))
                sql.Append("  AND p.ProduceMaterialID between @produceMaterialId0  and @produceMaterialId1 ");
            if (pId0 != null && pId1 != null)
                sql.Append("  AND  pro.Id between @pId0 and @pId1");
            //if (!string.IsNullOrEmpty(departmentId0) && !string.IsNullOrEmpty(departmentId1))
            //    sql.Append(" and p.Workhousename between @departmentId0 and @departmentId1");
            if (!string.IsNullOrEmpty(departmentId0))
            {
                sql.Append(" AND p.WorkHouseId = '" + departmentId0 + "'");
            }
            if (!string.IsNullOrEmpty(PronoteHeaderId0) && !string.IsNullOrEmpty(PronoteHeaderId1))
                sql.Append(" AND p.InvoiceId between @PronoteHeaderId0 and @PronoteHeaderId1");
            if (!string.IsNullOrEmpty(CusInvoiceXOId))
                sql.Append(" AND p.InvoiceXOId = (SELECT InvoiceId FROM InvoiceXO WHERE CustomerInvoiceXOId = @CusInvoiceXOId)");
            sql.Append(" order by p.ProduceMaterialID desc ");
            return this.DataReaderBind<Model.ProduceMaterialdetails>(sql.ToString(), parames, CommandType.Text);
            //Hashtable ht = new Hashtable();
            //ht.Add("starDate", startDate);
            //ht.Add("endDate", endDate);
            //ht.Add("produceMaterialId0", produceMaterialId0);
            //ht.Add("produceMaterialId1", produceMaterialId1);
            //ht.Add("pId0", pId0 == null ? null : pId0.ProductName);
            //ht.Add("pId1", pId1 == null ? null : pId1.ProductName);
            //ht.Add("dId0", departmentId0);
            //ht.Add("dId1", departmentId1);
            //ht.Add("pronoteId0", PronoteHeaderId0);
            //ht.Add("pronoteId1", PronoteHeaderId1);
            //return sqlmapper.QueryForList<Model.ProduceMaterial>("ProduceMaterial.selectBycondition", ht);
        }

        public double SelectMaterialQty(string productid, DateTime dateEnd, string workHouseId, string invoiceXOIds)
        {
            string sql = "select sum(isnull(Materialprocessum,0)) as Materialprocessum from ProduceMaterialdetails pmd left join ProduceMaterial pm on pm.ProduceMaterialID=pmd.ProduceMaterialID where pmd.ProductId='" + productid + "' and pm.ProduceMaterialDate <= '" + dateEnd + "' and pm.WorkHouseId='" + workHouseId + "' and pm.InvoiceXOId in (" + invoiceXOIds + ")";

            return Convert.ToDouble(this.QueryObject(sql));
        }

        public double SelectMaterialQty(string productid, DateTime dateEnd, string workHouseId)
        {
            string sql = "select sum(isnull(Materialprocessum,0)) as Materialprocessum from ProduceMaterialdetails pmd left join ProduceMaterial pm on pm.ProduceMaterialID=pmd.ProduceMaterialID left join InvoiceXO xo on pm.InvoiceXOId=xo.InvoiceId where pmd.ProductId='" + productid + "' and pm.ProduceMaterialDate <= '" + dateEnd + "' and pm.WorkHouseId='" + workHouseId + "' and xo.IsClose <>1";

            return Convert.ToDouble(this.QueryObject(sql));
        }
    }
}
