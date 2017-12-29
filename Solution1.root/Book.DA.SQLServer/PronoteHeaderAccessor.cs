//------------------------------------------------------------------------------
//
// file name：PronoteHeaderAccessor.cs
// author: peidun
// create date：2009-12-29 11:58:39
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
    /// Data accessor of PronoteHeader
    /// </summary>
    public partial class PronoteHeaderAccessor : EntityAccessor, IPronoteHeaderAccessor
    {
        /// <summary>
        /// 生产入库选择加工单
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="customer"></param>
        /// <param name="cusxoid"></param>
        /// <param name="product"></param>
        /// <param name="PronoteHeaderIdStart"></param>
        /// <param name="PronoteHeaderIdEnd"></param>
        /// <param name="sourcetype"></param>
        /// <param name="workhouseIndepot"></param>
        /// <param name="jiean"></param>
        /// <returns></returns>
        public IList<Book.Model.PronoteHeader> GetByDate(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5)
        {
            SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@xocustomerId", DbType.String), new SqlParameter("@CustomerInvoiceXOId", DbType.String), new SqlParameter("@productid", DbType.String) };
            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (customer != null)
                parames[2].Value = customer.CustomerId;
            else
                parames[2].Value = DBNull.Value;
            if (!string.IsNullOrEmpty(cusxoid))
                parames[3].Value = cusxoid;
            else
                parames[3].Value = DBNull.Value;
            if (product != null)
                parames[4].Value = product.ProductId;
            else
                parames[4].Value = DBNull.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  w.Workhousename as WorkHouseNextName,a.Checkeds,a.IsClose,a.HandbookProductId,a.HandbookId,a.PronoteHeaderID,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId,a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId, a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity");
            sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee0Id) as Employee0Name, (select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name");
            sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee2Id) as Employee2Name ");
            //sql.Append(" , (SELECT  Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId =(SELECT TOP 1 WorkHouseId FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  ) ) AS WorkHouseNextName ");
            //   sql.Append(" , (SELECT TOP 1  ProduceTransferQuantity  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  )  AS ProduceTransferQuantity");
            // 本车间合格数量
            sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and pr.ProduceInDepotId in (select ProduceInDepotid from ProduceInDepot where WorkHouseId='" + workhouseIndepot + "'))  AS HeJiCheckOutSum");
            //前车间合格数量
            sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and  WorkHouseId='" + workhouseIndepot + "') AS ProduceTransferQuantity");
            //当前部门合计生产数量,出自<生产入库详细>
            sql.Append(", (SELECT sum(isnull(p.ProceduresSum,0)) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderID AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProceduresSum");
            //当前部门合计转生产数量
            sql.Append(", (SELECT SUM(HeJiProduceTransferQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceTransferQuantity");
            //当前部门合计入库数量
            sql.Append(", (SELECT SUM(HeJiProduceQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceQuantity");
            //PronoteProceduresDate 订单交期
            sql.Append(",  i.CustomerInvoiceXOId,i.InvoiceYjrq as PronoteProceduresDate, (SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard");
            sql.Append(", (SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName");
            //if (!string.IsNullOrEmpty(workhouseIndepot))
            //{
            //    sql.Append(", (select top 1 PronoteProceduresDate from PronoteProceduresDetail u  where  u.PronoteHeaderID=a.PronoteHeaderID and u.WorkHouseId='" + workhouseIndepot + "'  order by PronoteProceduresDate ) as PronoteProceduresDate");
            //}
            sql.Append(",b.ProductName,b.id, b.CustomerProductName  FROM PronoteHeader a left join   Product b  on a.productid=b.productid  left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");

            sql.Append("  where    PronoteDate between @startdate and @enddate  ");
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and   i.CustomerInvoiceXOId  like '%'+@CustomerInvoiceXOId+'%'");
            if (customer != null)
                sql.Append(" and  i.xocustomerId=@xocustomerId");
            if (product != null)
                sql.Append(" and  a.productid=@productid");
            if (!string.IsNullOrEmpty(PronoteHeaderIdStart) && !string.IsNullOrEmpty(PronoteHeaderIdEnd))
                sql.Append(" and  a.PronoteHeaderID between '" + PronoteHeaderIdStart + "' and '" + PronoteHeaderIdEnd + "'");
            if (sourcetype != -1)   //全部时为-1
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType=" + sourcetype + ")");
            if (jiean) // 只显示未结案
                sql.Append(" and  a.IsClose=0");
            if (!string.IsNullOrEmpty(proNameKey)) // 商品名称关键字
                sql.Append(" and b.ProductName like '%" + proNameKey + "%'");
            if (!string.IsNullOrEmpty(proCusNameKey)) //客户型号名称关键字
                sql.Append(" and b.CustomerProductName like '%" + proCusNameKey + "%'");
            if (!string.IsNullOrEmpty(pronoteHeaderIdKey)) // 加工单号关键字
                sql.Append(" and  a.PronoteHeaderID like '%" + pronoteHeaderIdKey + "%'");
            //三种自制条件
            if (sourcetype0 && sourcetype4 && !sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4'))");
            else if (sourcetype0 && sourcetype5 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','5'))");
            else if (sourcetype4 && sourcetype5 && !sourcetype0)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4','5'))");
            else if (sourcetype0 && !sourcetype5 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0'))");
            else if (sourcetype4 && !sourcetype0 && !sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4'))");
            else if (sourcetype5 && !sourcetype0 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('5'))");
            else if (sourcetype0 && sourcetype4 && sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4','5'))");
            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), parames, CommandType.Text);
        }

        /// <summary>
        /// 领料单选择加工单
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="customer"></param>
        /// <param name="cusxoid"></param>
        /// <param name="product"></param>
        /// <param name="PronoteHeaderIdStart"></param>
        /// <param name="PronoteHeaderIdEnd"></param>
        /// <param name="sourcetype"></param>
        /// <param name="workhouseIndepot"></param>
        /// <param name="jiean"></param>
        /// <param name="proNameKey"></param>
        /// <param name="proCusNameKey"></param>
        /// <param name="pronoteHeaderIdKey"></param>
        /// <returns></returns>
        public IList<Book.Model.PronoteHeader> GetByDateMa(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5)
        {
            SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@xocustomerId", DbType.String), new SqlParameter("@CustomerInvoiceXOId", DbType.String), new SqlParameter("@productid", DbType.String) };
            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (customer != null)
                parames[2].Value = customer.CustomerId;
            else
                parames[2].Value = DBNull.Value;
            if (!string.IsNullOrEmpty(cusxoid))
                parames[3].Value = cusxoid;
            else
                parames[3].Value = DBNull.Value; ;
            if (product != null)
                parames[4].Value = product.ProductId;
            else
                parames[4].Value = DBNull.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  w.Workhousename,a.Checkeds,a.IsClose,a.HandbookProductId,a.HandbookId,a.PronoteHeaderID,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId,a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId, a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity");
            sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee0Id) as Employee0Name, (select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name");
            //  sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee2Id) as Employee2Name ");
            //  sql.Append(" , (SELECT  Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId =(SELECT TOP 1 WorkHouseId FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  ) ) AS WorkHouseNextName ");
            //   sql.Append(" , (SELECT TOP 1  ProduceTransferQuantity  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  )  AS ProduceTransferQuantity");
            // 本车间合格数量
            //  sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and pr.ProduceInDepotId in (select ProduceInDepotid from ProduceInDepot where WorkHouseId='" + workhouseIndepot + "'))  AS HeJiCheckOutSum");
            //前车间合格数量
            //  sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and  WorkHouseId='" + workhouseIndepot + "') AS ProduceTransferQuantity");
            //当前部门合计生产数量,出自<生产入库详细>
            // sql.Append(", (SELECT sum(isnull(p.ProceduresSum,0)) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderID AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProceduresSum");
            //当前部门合计转生产数量
            //   sql.Append(", (SELECT SUM(HeJiProduceTransferQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceTransferQuantity");
            //当前部门合计入库数量
            // sql.Append(", (SELECT SUM(HeJiProduceQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceQuantity");
            //PronoteProceduresDate 订单交期
            sql.Append(", i.CustomerInvoiceXOId,i.InvoiceYjrq as PronoteProceduresDate, (SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard");
            sql.Append(", (SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName");
            //if (!string.IsNullOrEmpty(workhouseIndepot))
            //{
            //    sql.Append(", (select top 1 PronoteProceduresDate from PronoteProceduresDetail u  where  u.PronoteHeaderID=a.PronoteHeaderID and u.WorkHouseId='" + workhouseIndepot + "'  order by PronoteProceduresDate ) as PronoteProceduresDate");
            //}
            sql.Append(",b.ProductName,b.id, b.CustomerProductName, b.ProductDescription as ProductDesc  FROM PronoteHeader a left join   Product b  on a.productid=b.productid  left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");

            sql.Append("  where    PronoteDate between @startdate and @enddate  ");
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and   i.CustomerInvoiceXOId  like '%'+@CustomerInvoiceXOId+'%'");
            if (customer != null)
                sql.Append(" and  i.xocustomerId=@xocustomerId");
            if (product != null)
                sql.Append(" and  a.productid=@productid");
            if (!string.IsNullOrEmpty(PronoteHeaderIdStart) && !string.IsNullOrEmpty(PronoteHeaderIdEnd))
                sql.Append(" and  a.PronoteHeaderID between '" + PronoteHeaderIdStart + "' and '" + PronoteHeaderIdEnd + "'");
            if (sourcetype != -1)   //全部时为-1
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType=" + sourcetype + ")");
            if (jiean) // 只显示未结案
                sql.Append(" and  a.IsClose=0");
            if (!string.IsNullOrEmpty(proNameKey)) // 商品名称关键字
                sql.Append(" and b.ProductName like '%" + proNameKey + "%'");
            if (!string.IsNullOrEmpty(proCusNameKey)) //客户型号名称关键字
                sql.Append(" and b.CustomerProductName like '%" + proCusNameKey + "%'");
            if (!string.IsNullOrEmpty(pronoteHeaderIdKey)) // 加工单号关键字
                sql.Append(" and  a.PronoteHeaderID like '%" + pronoteHeaderIdKey + "%'");
            if (!string.IsNullOrEmpty(workhouseIndepot))   //公司部门
                sql.Append(" and w.WorkHouseId='" + workhouseIndepot + "'");
            //三种自制条件
            if (sourcetype0 && sourcetype4 && !sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4'))");
            else if (sourcetype0 && sourcetype5 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','5'))");
            else if (sourcetype4 && sourcetype5 && !sourcetype0)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4','5'))");
            else if (sourcetype0 && !sourcetype5 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0'))");
            else if (sourcetype4 && !sourcetype0 && !sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4'))");
            else if (sourcetype5 && !sourcetype0 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('5'))");
            else if (sourcetype0 && sourcetype4 && sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4','5'))");
            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), parames, CommandType.Text);
        }

        //质检选择加工单      
        public IList<Book.Model.PronoteHeader> GetByDateZJ(DateTime startDate, DateTime endDate, Model.Customer customer, string cusxoid, Model.Product product, string PronoteHeaderIdStart, string PronoteHeaderIdEnd, int sourcetype, string workhouseIndepot, bool jiean, string proNameKey, string proCusNameKey, string pronoteHeaderIdKey, bool sourcetype0, bool sourcetype4, bool sourcetype5)
        {
            SqlParameter[] parames = { new SqlParameter("@startdate", DbType.DateTime), new SqlParameter("@enddate", DbType.DateTime), new SqlParameter("@xocustomerId", DbType.String), new SqlParameter("@CustomerInvoiceXOId", DbType.String), new SqlParameter("@productid", DbType.String) };
            parames[0].Value = startDate;
            parames[1].Value = endDate;
            if (customer != null)
                parames[2].Value = customer.CustomerId;
            else
                parames[2].Value = DBNull.Value;
            if (!string.IsNullOrEmpty(cusxoid))
                parames[3].Value = cusxoid;
            else
                parames[3].Value = DBNull.Value; ;
            if (product != null)
                parames[4].Value = product.ProductId;
            else
                parames[4].Value = DBNull.Value;
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT  w.Workhousename as WorkHouseNextName,a.Checkeds,a.IsClose,a.HandbookProductId,a.HandbookId,a.PronoteHeaderID,a.InvoiceXOId,a.PronoteHeaderID,a.InvoiceCusId,a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId, a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity");
            sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee0Id) as Employee0Name, (select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name");
            sql.Append(",  (SELECT  EmployeeName FROM employee where employee.employeeid=a.Employee2Id) as Employee2Name ");
            //  sql.Append(" , (SELECT  Workhousename FROM WorkHouse WHERE WorkHouse.WorkHouseId =(SELECT TOP 1 WorkHouseId FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  ) ) AS WorkHouseNextName ");
            //   sql.Append(" , (SELECT TOP 1  ProduceTransferQuantity  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID ORDER BY ProduceInDepotId DESC  )  AS ProduceTransferQuantity");
            // 本车间合格数量
            //  sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and pr.ProduceInDepotId in (select ProduceInDepotid from ProduceInDepot where WorkHouseId='" + workhouseIndepot + "'))  AS HeJiCheckOutSum");
            //前车间合格数量
            sql.Append(" , (SELECT sum(CheckOutSum)  FROM ProduceInDepotDetail pr WHERE pr.PronoteHeaderId= a.PronoteHeaderID and  WorkHouseId = (select WorkHouseId from WorkHouse where WorkHousename = '射出' )) AS ProduceTransferQuantity");
            //当前部门合计生产数量,出自<生产入库详细>
            // sql.Append(", (SELECT sum(isnull(p.ProceduresSum,0)) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderID AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProceduresSum");
            //当前部门合计转生产数量
            //   sql.Append(", (SELECT SUM(HeJiProduceTransferQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceTransferQuantity");
            //当前部门合计入库数量
            // sql.Append(", (SELECT SUM(HeJiProduceQuantity) FROM ProduceInDepotDetail p WHERE p.PronoteHeaderId = a.PronoteHeaderId AND p.ProduceInDepotId IN (SELECT ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId = '" + workhouseIndepot + "')) AS HeJiProduceQuantity");
            //PronoteProceduresDate 订单交期
            sql.Append(",  i.CustomerInvoiceXOId,i.InvoiceYjrq as PronoteProceduresDate, (SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard");
            sql.Append(", (SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName");
            //if (!string.IsNullOrEmpty(workhouseIndepot))
            //{
            //    sql.Append(", (select top 1 PronoteProceduresDate from PronoteProceduresDetail u  where  u.PronoteHeaderID=a.PronoteHeaderID and u.WorkHouseId='" + workhouseIndepot + "'  order by PronoteProceduresDate ) as PronoteProceduresDate");
            //}
            sql.Append(",b.ProductName,b.id, b.CustomerProductName FROM PronoteHeader a left join   Product b  on a.productid=b.productid  left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");

            sql.Append("  where    PronoteDate between @startdate and @enddate  ");
            if (!string.IsNullOrEmpty(cusxoid))
                sql.Append(" and   i.CustomerInvoiceXOId  like '%'+@CustomerInvoiceXOId+'%'");
            if (customer != null)
                sql.Append(" and  i.xocustomerId=@xocustomerId");
            if (product != null)
                sql.Append(" and  a.productid=@productid");
            if (!string.IsNullOrEmpty(PronoteHeaderIdStart) && !string.IsNullOrEmpty(PronoteHeaderIdEnd))
                sql.Append(" and  a.PronoteHeaderID between '" + PronoteHeaderIdStart + "' and '" + PronoteHeaderIdEnd + "'");
            if (sourcetype != -1)   //全部时为-1
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType=" + sourcetype + ")");
            if (jiean) // 只显示未结案
                sql.Append(" and  a.IsClose=0");
            if (!string.IsNullOrEmpty(proNameKey)) // 商品名称关键字
                sql.Append(" and b.ProductName like '%" + proNameKey + "%'");
            if (!string.IsNullOrEmpty(proCusNameKey)) //客户型号名称关键字
                sql.Append(" and b.CustomerProductName like '%" + proCusNameKey + "%'");
            if (!string.IsNullOrEmpty(pronoteHeaderIdKey)) // 加工单号关键字
                sql.Append(" and  a.PronoteHeaderID like '%" + pronoteHeaderIdKey + "%'");
            //三种自制条件
            if (sourcetype0 && sourcetype4 && !sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4'))");
            else if (sourcetype0 && sourcetype5 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','5'))");
            else if (sourcetype4 && sourcetype5 && !sourcetype0)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4','5'))");
            else if (sourcetype0 && !sourcetype5 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0'))");
            else if (sourcetype4 && !sourcetype0 && !sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('4'))");
            else if (sourcetype5 && !sourcetype0 && !sourcetype4)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('5'))");
            else if (sourcetype0 && sourcetype4 && sourcetype5)
                sql.Append(" and  a.MRSHeaderId IN(SELECT MRSHeaderId FROM MRSHeader WHERE SourceType in ('0','4','5'))");
            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), parames, CommandType.Text);
        }

        public IList<Book.Model.PronoteHeader> Select(string customerStart, string customerEnd, DateTime dateStart, DateTime dateEnd, string CusXOId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startcustomerid", customerStart);
            ht.Add("endcustomerid", customerEnd);
            ht.Add("startdate", dateStart);
            ht.Add("enddate", dateEnd);
            ht.Add("cusxoid", CusXOId);
            return sqlmapper.QueryForList<Book.Model.PronoteHeader>("PronoteHeader.select_byCustomerANDdate", ht);
        }

        public IList<Book.Model.PronoteHeader> Select(Model.MRSHeader mrsheader)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT w.Workhousename,a.Checkeds,a.InvoiceXOId,a.HandbookProductId,a.HandbookId,a.PronoteHeaderID,a.PronoteHeaderID,a.InvoiceCusId, a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId,a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity,(select  EmployeeName from employee where employee.employeeid=a.Employee0Id) as Employee0Name,(select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name,(select  EmployeeName from employee where employee.employeeid=a.Employee2Id) as Employee2Name,i.CustomerInvoiceXOId,(SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard,(SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName,b.ProductName,b.id,b.CustomerProductName,b.ProductDescription as ProductDesc FROM PronoteHeader a left join   Product b  on a.productid=b.productid left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");
            sql.Append("  where  a.MRSHeaderId= '" + mrsheader.MRSHeaderId + "'");
            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), null, CommandType.Text);

            // return sqlmapper.QueryForList<Book.Model.PronoteHeader>("PronoteHeader.select_bymrsheader", mrsheader.MRSHeaderId);
        }

        #region 加工单 生产入库单合格数量数量
        //public void UpdatePronoteIsClose(string pronoteheaderid, double? indepotquantity) 
        //{
        //    Hashtable ht = new Hashtable();
        //    ht.Add("pronoteHeaderid", pronoteheaderid);
        //    ht.Add("inDepotSum", indepotquantity);
        //    sqlmapper.Update("PronoteHeader.update_PronoteHeaderIsClse", ht);
        //}
        #endregion

        //加工单 生产入库单合格数量数量
        public void UpdateHeaderIsClse(string pronoteheaderid, bool isclose)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pronoteHeaderid", pronoteheaderid);
            ht.Add("isclose", isclose);
            sqlmapper.Update("PronoteHeader.update_HeaderIsClse", ht);
        }

        public void UpdateHeaderIsClseByXOId(string InvoiceXOId, bool isclose)
        {
            Hashtable ht = new Hashtable();
            ht.Add("InvoiceXOId", InvoiceXOId);
            ht.Add("isclose", isclose);
            sqlmapper.Update("PronoteHeader.update_HeaderIsClseByXOId", ht);
        }

        public IList<Book.Model.PronoteHeader> Select(IList<string> ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" a.PronoteHeaderID='" + ids[0] + "'");
            for (int i = 1; i < ids.Count; i++)
            {
                sb.Append(" or a.PronoteHeaderID='" + ids[i] + "'");

            }


            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT w.Workhousename,a.Checkeds,a.InvoiceXOId,a.HandbookProductId,a.HandbookId,a.PronoteHeaderID,a.PronoteHeaderID,a.InvoiceCusId, a.InvoiceXODetailQuantity,a.PronoteDate,a.Pronotedesc,a.MRSHeaderId,a.MRSdetailsId,a.DetailsSum,a.ProductId,a.ProductUnit,a.InvoiceXODetailQuantity,(select  EmployeeName from employee where employee.employeeid=a.Employee0Id) as Employee0Name,(select  EmployeeName from employee where employee.employeeid=a.Employee1Id) as Employee1Name,(select  EmployeeName from employee where employee.employeeid=a.Employee2Id) as Employee2Name,i.CustomerInvoiceXOId,(SELECT CheckedStandard FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerCheckStandard,(SELECT CustomerShortName FROM Customer c WHERE c.CustomerId = i.xocustomerId) as CustomerShortName,b.ProductName,b.id,b.CustomerProductName,b.ProductDescription as ProductDesc FROM PronoteHeader a left join   Product b  on a.productid=b.productid left join invoicexo i on a.invoicexoid=i.invoiceid left join   WorkHouse w  on a.WorkHouseId=w.WorkHouseId");
            sql.Append("  where  " + sb.ToString());
            sql.Append(" order by a.PronoteHeaderID desc ");
            return this.DataReaderBind<Model.PronoteHeader>(sql.ToString(), null, CommandType.Text);

            // return sqlmapper.QueryForList<Book.Model.PronoteHeader>("PronoteHeader.select_bymrsheader", mrsheader.MRSHeaderId);
        }

        //查询商品对应的未结案加工单
        public IList<Model.PronoteHeader> SelectNotClosed(string productid)
        {
            string sql = "select PronoteHeaderID,InvoiceXOId from PronoteHeader where ProductId='" + productid + "' and IsClose<>1";
            return this.DataReaderBind<Model.PronoteHeader>(sql, null, CommandType.Text);
        }
    }
}
