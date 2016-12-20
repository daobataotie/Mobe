using System;
using System.Collections.Generic;
using System.Text;

using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Data;

namespace Book.DA.SQLServer
{
    //public class MiscDataAccessor : Accessor, IMiscDataAccessor
    //{
    //    #region IMiscDataAccessor Members

    //    public System.Collections.IList Q03()
    //    {
    //        throw new Exception("The method or operation is not implemented.");
    //    }

    //    public System.Data.DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Book.Model.Depot depot, Helper.InvoiceStatus invoiceStatus, string QueryId)
    //    {
    //        System.Collections.Hashtable table = new System.Collections.Hashtable();
    //        table.Add("startTime", start);
    //        table.Add("endTime", end);
    //        table.Add("depotId", depot == null ? null : depot.DepotId);
    //        table.Add("empId", employee == null ? null : employee.EmployeeId);
    //        table.Add("status", (int)invoiceStatus);
    //        table.Add("companyId", company == null ? null : company.CompanyId);


    //        System.Collections.IList dt = null;

    //        System.Data.DataTable data = new System.Data.DataTable();

    //        switch (QueryId)
    //        {
    //            case "Q05":
    //               // dt = sqlmapper.QueryForList("MiscData.select_query05", table);
    //                break;
    //            case "Q07":
    //               // dt = sqlmapper.QueryForList("MiscData.select_query07", table);
    //                break;
    //            case "Q08":
    //               // dt = sqlmapper.QueryForList("MiscData.select_query08", table);
    //                break;
    //            case "Q09":
    //               // dt = sqlmapper.QueryForList("MiscData.select_query09", table);
    //                break;
    //            case "Q10":
    //                //dt = sqlmapper.QueryForList("MiscData.select_query10", table);
    //                break;
    //            case "Q11":
    //                break;
    //        }

    //        return data;
    //    }

    //    public System.Data.DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Helper.InvoiceStatus invoiceStatus, string queryId)
    //    {
    //        return new System.Data.DataTable();
    //        //System.Collections.Hashtable table = new System.Collections.Hashtable();
    //        //table.Add("startTime", start);
    //        //table.Add("endTime", end);
    //        //table.Add("empId", employee == null ? null : employee.EmployeeId);
    //        //table.Add("status", (int)invoiceStatus);
    //        //table.Add("companyId", company == null ? null : company.CompanyId);
    //        //System.Collections.IList list = null;

    //        //switch (queryId)
    //        //{
    //        //    case "Q13":
    //        //        list = sqlmapper.QueryForList("MiscData.select_query13", table);
    //        //        break;
    //        //    default:
    //        //        break;
    //        //}

    //        //return list;
    //    }

    //    public System.Data.DataTable SelectDataTable(string queryId)
    //    {
    //        System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
    //        sda.SelectCommand = new System.Data.SqlClient.SqlCommand();
    //        sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);

    //        System.Data.DataTable table = new System.Data.DataTable();

    //        string sql = "";

    //        switch (queryId)
    //        {
    //            case "Q02":
    //                sql = "SELECT Product.ProductName, SUM(Stock.StockQuantity1) as Quantity1,"
    //                    + "Product.ProductStandardCost FROM Stock INNER JOIN Product ON"
    //                    + " Stock.ProductId = Product.ProductId GROUP BY Product.ProductName,"
    //                    + "Product.ProductStandardCost";
    //                break;
    //            case "Q14":
    //                sql = "SELECT Depot.DepotName,Product.ProductName,"
    //                    + " SUM(Stock.StockQuantity1) as Quantity FROM Stock INNER JOIN "
    //                    + " Product ON Stock.ProductId = Product.ProductId INNER JOIN"
    //                    + " Depot ON Stock.DepotId = Depot.DepotId GROUP BY "
    //                    + "Depot.DepotName, Product.ProductName";
    //                break;
    //        }

    //        if (!string.IsNullOrEmpty(sql))
    //        {
    //            sda.SelectCommand.CommandText = sql;
    //            sda.Fill(table);
    //        }
    //        return table;
    //    }

    //    public System.Data.DataTable Q15(int day)
    //    {
    //        string sql = "select * from dbo.company where companypaydate = " + day;
    //        System.Data.DataTable table = new System.Data.DataTable();
    //        System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
    //        sda.Fill(table);
    //        return table;
    //    }

    //    public IList<Model.Stock> Select(string startid, string endid)
    //    {
    //        System.Collections.Hashtable table = new System.Collections.Hashtable();
    //        table.Add("startId", startid);
    //        table.Add("endId", endid);
    //        return sqlmapper.QueryForList<Model.Stock>("MiscData.Stock", table);
    //    }

    //    public System.Collections.IList Select(DateTime start, DateTime end, string startId, string endId)
    //    {
    //        System.Collections.Hashtable table = new System.Collections.Hashtable();
    //        table.Add("startDate", start);
    //        table.Add("endDate", end);
    //        table.Add("psid", startId);
    //        table.Add("peid", endId);
    //        return sqlmapper.QueryForList("MiscData.product", table);
    //    }

    //    public System.Collections.IList Select1(DateTime start, DateTime end, string startId, string endId)
    //    {
    //        System.Collections.Hashtable table = new System.Collections.Hashtable();
    //        table.Add("startDate", start);
    //        table.Add("endDate", end);
    //        table.Add("esid", startId);
    //        table.Add("eeid", endId);
    //        return sqlmapper.QueryForList("MiscData.employee", table);
    //    }

    //    public System.Collections.IList SelectDataTable(DateTime endDate)
    //    {
    //        return sqlmapper.QueryForList("MiscData.ZL", endDate);
    //    }

    //    public System.Collections.IList SelectDataTable(DateTime startDate, DateTime endDate, string startId, string endId)
    //    {
    //        System.Collections.Hashtable table = new System.Collections.Hashtable();
    //        table.Add("startDate", startDate);
    //        table.Add("endDate", endDate);
    //        table.Add("psid", startId);
    //        table.Add("peid", endId);
    //        return sqlmapper.QueryForList("MiscData.LR", table);
    //    }

    //    public System.Collections.IList SelectDataTable(DateTime startDate, DateTime endDate, Book.Model.Company company)
    //    {
    //        System.Collections.Hashtable pars = new System.Collections.Hashtable();
    //        pars.Add("companyId", company.CompanyId);
    //        pars.Add("startDate", startDate);
    //        pars.Add("endDate", endDate);
    //        switch (company.CompanyKind.Value)
    //        {
    //            case (int)Helper.CompanyKind.Customer:
    //                return sqlmapper.QueryForList("MiscData.ChongXiaoYingShou", pars);
    //            case (int)Helper.CompanyKind.Supplier:
    //                return sqlmapper.QueryForList("MiscData.ChongXiaoYingFu", pars);
    //            default:
    //                return null;
    //        }
    //    }

    //    #endregion
    //}

    public partial class MiscDataAccessor : Accessor, IMiscDataAccessor
    {
        #region IMiscDataAccessor Members

        public System.Collections.IList Q03()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //public System.Data.DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Book.Model.Depot depot, Helper.InvoiceStatus invoiceStatus, string QueryId)
        //{
        //    //System.Collections.Hashtable table = new System.Collections.Hashtable();
        //    //table.Add("startTime", start);
        //    //table.Add("endTime", end);
        //    //table.Add("depotId", depot == null ? null : depot.DepotId);
        //    //table.Add("empId", employee == null ? null : employee.EmployeeId);
        //    //table.Add("status", (int)invoiceStatus);
        //    //table.Add("companyId", company == null ? null : company.CompanyId);
        //    //System.Collections.IList list = null;
        //    System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
        //    sda.SelectCommand = new System.Data.SqlClient.SqlCommand();
        //    sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);

        //    string sql = "";
        //    switch (QueryId)
        //    {
        //        case "Q05":
        //            #region q05
        //            //list = sqlmapper.QueryForList("MiscData.select_query05", table);
        //            sql = "SELECT Product.ProductId, Product.ProductName, Product.ProductSpecification, "
        //                + "Product.ProductModel, Product.ProductBaseUnit, "
        //                + "SUM(InvoiceXSDetail.InvoiceXSDetailQuantity) AS Quantity, "
        //                + "SUM(InvoiceXSDetail.InvoiceXSDetailMoney0) AS DetailMoney0, "
        //                + "SUM(InvoiceXSDetail.InvoiceXSDetailMoney0) "
        //                + "/ SUM(InvoiceXSDetail.InvoiceXSDetailQuantity) AS AvgPrice, "
        //                + "SUM(InvoiceXSDetail.InvoiceXSDetailMoney1) AS DetailMoney1"
        //                + " FROM ((Product INNER JOIN"
        //                + "      InvoiceXSDetail ON Product.ProductId = InvoiceXSDetail.ProductId) INNER JOIN"
        //                + "      InvoiceXS ON InvoiceXSDetail.InvoiceId = InvoiceXS.InvoiceId)"
        //                + "WHERE (InvoiceXS.InvoiceStatus = " + (int)Helper.InvoiceStatus.Normal + ")";
        //            if (employee != null && !string.IsNullOrEmpty(employee.EmployeeId))
        //            {
        //                sql += " AND (InvoiceXS.Employee0Id = '" + employee.EmployeeId + "')";
        //            }

        //            if (company != null && !string.IsNullOrEmpty(company.CompanyId))
        //            {
        //                sql += " AND (InvoiceXS.CompanyId = '" + company.CompanyId + "')";
        //            }
        //            if (depot != null && !string.IsNullOrEmpty(depot.DepotId))
        //            {
        //                sql += " AND (InvoiceXS.DepotId = '" + depot.DepotId + "')";
        //            }
        //            if (start != new DateTime(1, 1, 1, 0, 0, 0) && end != new DateTime(1, 1, 1, 0, 0, 0))
        //            {
        //                sql += " AND (InvoiceXS.InvoiceDate BETWEEN '" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            }
        //            sql += "GROUP BY Product.ProductId, Product.ProductName, Product.ProductSpecification,Product.ProductModel, Product.ProductBaseUnit";
        //            #endregion
        //            break;
        //        case "Q07":
        //            // list = sqlmapper.QueryForList("MiscData.select_query07", table);
        //            sql = "SELECT Product.ProductId, Product.ProductName, Product.ProductSpecification,"
        //                + "Product.ProductModel, Product.ProductBaseUnit, "
        //                + "SUM(InvoiceXTDetail.InvoiceXTDetailQuantity) AS Quantity,"
        //                + "SUM(InvoiceXTDetail.InvoiceXTDetailMoney0) AS DetailMoney0,"
        //                + "SUM(InvoiceXTDetail.InvoiceXTDetailMoney0) / SUM(InvoiceXTDetail.InvoiceXTDetailQuantity) AS AvgPrice, "
        //                + "SUM(InvoiceXTDetail.InvoiceXTDetailMoney1) AS DetailMoney1 "
        //                + "FROM ((Product INNER JOIN InvoiceXTDetail ON Product.ProductId = InvoiceXTDetail.ProductId) INNER JOIN InvoiceXT ON InvoiceXTDetail.InvoiceId = InvoiceXT.InvoiceId) "
        //                + "WHERE (InvoiceXT.InvoiceStatus = " + (int)Helper.InvoiceStatus.Normal + ")";

        //            if (employee != null && !string.IsNullOrEmpty(employee.EmployeeId))
        //            {
        //                sql += " AND (InvoiceXT.Employee0Id = '" + employee.EmployeeId + "')";
        //            }

        //            if (company != null && !string.IsNullOrEmpty(company.CompanyId))
        //            {
        //                sql += " AND (InvoiceXT.CompanyId = '" + company.CompanyId + "')";
        //            }
        //            if (depot != null && !string.IsNullOrEmpty(depot.DepotId))
        //            {
        //                sql += " AND (InvoiceXT.DepotId = '" + depot.DepotId + "')";
        //            }
        //            if (start != new DateTime(1, 1, 1, 0, 0, 0) && end != new DateTime(1, 1, 1, 0, 0, 0))
        //            {
        //                sql += " AND (InvoiceXT.InvoiceDate BETWEEN '" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            }
        //            sql += "GROUP BY Product.ProductId, Product.ProductName,Product.ProductSpecification, Product.ProductModel, Product.productbaseunit";
        //            break;
        //        case "Q08":
        //            //  list = sqlmapper.QueryForList("MiscData.select_query08", table);
        //            sql = "	SELECT Product.ProductId, Product.ProductName, Product.ProductSpecification, "
        //                + "Product.ProductModel, Product.ProductBaseUnit,"
        //                + "SUM(InvoiceCGDetail.InvoiceCGDetailQuantity) AS Quantity,"
        //                + "SUM(InvoiceCGDetail.InvoiceCGDetailMoney0) AS DetailMoney0,"
        //                + "SUM(InvoiceCGDetail.InvoiceCGDetailMoney0) / SUM(InvoiceCGDetail.InvoiceCGDetailQuantity) AS AvgPrice,"
        //                + "SUM(InvoiceCGDetail.InvoiceCGDetailMoney1) AS DetailMoney1 "
        //                + "FROM ((Product INNER JOIN InvoiceCGDetail ON Product.ProductId = InvoiceCGDetail.ProductId) INNER JOIN  InvoiceCG ON InvoiceCGDetail.InvoiceId = InvoiceCG.InvoiceId) " +
        //                "WHERE (InvoiceCG.InvoiceStatus = " + (int)Helper.InvoiceStatus.Normal + ")";
        //            if (employee != null && !string.IsNullOrEmpty(employee.EmployeeId))
        //            {
        //                sql += " AND (InvoiceCG.Employee0Id = '" + employee.EmployeeId + "')";
        //            }

        //            if (company != null && !string.IsNullOrEmpty(company.CompanyId))
        //            {
        //                sql += " AND (InvoiceCG.CompanyId = '" + company.CompanyId + "')";
        //            }
        //            if (depot != null && !string.IsNullOrEmpty(depot.DepotId))
        //            {
        //                sql += " AND (InvoiceCG.DepotId = '" + depot.DepotId + "')";
        //            }
        //            if (start != new DateTime(1, 1, 1, 0, 0, 0) && end != new DateTime(1, 1, 1, 0, 0, 0))
        //            {
        //                sql += " AND (InvoiceCG.InvoiceDate BETWEEN '" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            }
        //            sql += "GROUP BY Product.ProductId, Product.ProductName,Product.ProductSpecification, Product.ProductModel, Product.productbaseunit";

        //            break;
        //        case "Q09":
        //            // list = sqlmapper.QueryForList("MiscData.select_query09", table);

        //            sql = "	SELECT Product.ProductId, Product.ProductName, Product.ProductSpecification, "
        //               + "Product.ProductModel, Product.ProductBaseUnit,"
        //               + "SUM(InvoiceCTDetail.InvoiceCTDetailQuantity) AS Quantity,"
        //               + "SUM(InvoiceCTDetail.InvoiceCTDetailMoney0) AS DetailMoney0,"
        //               + "SUM(InvoiceCTDetail.InvoiceCTDetailMoney0) / SUM(InvoiceCTDetail.InvoiceCTDetailQuantity) AS AvgPrice,"
        //               + "SUM(InvoiceCTDetail.InvoiceCTDetailMoney1) AS DetailMoney1 "
        //               + "FROM ((Product INNER JOIN InvoiceCTDetail ON Product.ProductId = InvoiceCTDetail.ProductId) INNER JOIN  InvoiceCT ON InvoiceCTDetail.InvoiceId = InvoiceCT.InvoiceId) " +
        //               "WHERE (InvoiceCT.InvoiceStatus = " + (int)Helper.InvoiceStatus.Normal + ")";

        //            if (employee != null && !string.IsNullOrEmpty(employee.EmployeeId))
        //            {
        //                sql += " AND (InvoiceCT.Employee0Id = '" + employee.EmployeeId + "')";
        //            }
        //            if (company != null && !string.IsNullOrEmpty(company.CompanyId))
        //            {
        //                sql += " AND (InvoiceCT.CompanyId = '" + company.CompanyId + "')";
        //            }
        //            if (depot != null && !string.IsNullOrEmpty(depot.DepotId))
        //            {
        //                sql += " AND (InvoiceCT.DepotId = '" + depot.DepotId + "')";
        //            }
        //            if (start != new DateTime(1, 1, 1, 0, 0, 0) && end != new DateTime(1, 1, 1, 0, 0, 0))
        //            {
        //                sql += " AND (InvoiceCT.InvoiceDate BETWEEN '" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            }
        //            sql += "GROUP BY Product.ProductId, Product.ProductName, Product.ProductSpecification,Product.ProductModel, Product.ProductBaseUnit";
        //            break;
        //        case "Q10":
        //            #region q10
        //            sql = "SELECT Product.ProductId, Product.ProductName, Product.ProductSpecification, Product.ProductModel, Product.ProductBaseUnit,"
        //                + "SUM(InvoiceZSDetail.InvoiceZSDetailQuantity) AS Quantity, SUM(InvoiceZSDetail.InvoiceZSDetailMoney) AS DetailMoney "
        //                + "FROM ((InvoiceZSDetail INNER JOIN Product ON InvoiceZSDetail.ProductId = Product.ProductId) INNER JOIN InvoiceZS ON InvoiceZSDetail.InvoiceId = InvoiceZS.InvoiceId) "
        //                + "WHERE (InvoiceZS.InvoiceStatus = " + (int)Helper.InvoiceStatus.Normal + ")";

        //            if (employee != null && !string.IsNullOrEmpty(employee.EmployeeId))
        //            {
        //                sql += " AND (InvoiceZS.Employee0Id = '" + employee.EmployeeId + "')";
        //            }
        //            if (company != null && !string.IsNullOrEmpty(company.CompanyId))
        //            {
        //                sql += " AND (InvoiceZS.CompanyId = '" + company.CompanyId + "')";
        //            }
        //            if (depot != null && !string.IsNullOrEmpty(depot.DepotId))
        //            {
        //                sql += " AND (InvoiceZS.DepotId = '" + depot.DepotId + "')";
        //            }
        //            if (start != new DateTime(1, 1, 1, 0, 0, 0) && end != new DateTime(1, 1, 1, 0, 0, 0))
        //            {
        //                sql += " AND (InvoiceZS.InvoiceDate BETWEEN '" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            }
        //            sql += "GROUP BY Product.ProductId, Product.ProductName, Product.ProductSpecification,Product.ProductModel, Product.ProductBaseUnit";
        //            // list = sqlmapper.QueryForList("MiscData.select_query10", table);

        //            #endregion
        //            break;
        //        case "Q11":
        //            #region q10
        //            sql = "SELECT Product.ProductId, Product.ProductName, Product.ProductSpecification, Product.ProductModel, Product.ProductBaseUnit,"
        //                + "SUM(InvoiceHZDetail.InvoiceHZDetailQuantity) AS Quantity, SUM(InvoiceHZDetail.InvoiceHZDetailMoney) AS DetailMoney "
        //                + "FROM ((InvoiceHZDetail INNER JOIN Product ON InvoiceHZDetail.ProductId = Product.ProductId) INNER JOIN InvoiceHZ ON InvoiceHZDetail.InvoiceId = InvoiceHZ.InvoiceId) "
        //                + "WHERE (InvoiceHZ.InvoiceStatus = " + (int)Helper.InvoiceStatus.Normal + ")";

        //            if (employee != null && !string.IsNullOrEmpty(employee.EmployeeId))
        //            {
        //                sql += " AND (InvoiceHZ.Employee0Id = '" + employee.EmployeeId + "')";
        //            }
        //            if (company != null && !string.IsNullOrEmpty(company.CompanyId))
        //            {
        //                sql += " AND (InvoiceHZ.CompanyId = '" + company.CompanyId + "')";
        //            }
        //            if (depot != null && !string.IsNullOrEmpty(depot.DepotId))
        //            {
        //                sql += " AND (InvoiceHZ.DepotId = '" + depot.DepotId + "')";
        //            }
        //            if (start != new DateTime(1, 1, 1, 0, 0, 0) && end != new DateTime(1, 1, 1, 0, 0, 0))
        //            {
        //                sql += " AND (InvoiceHZ.InvoiceDate BETWEEN '" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //            }
        //            sql += "GROUP BY Product.ProductId, Product.ProductName, Product.ProductSpecification,Product.ProductModel, Product.ProductBaseUnit";
        //            // list = sqlmapper.QueryForList("MiscData.select_query10", table);

        //            #endregion
        //            // list = sqlmapper.QueryForList("MiscData.select_query11", table);
        //            break;
        //        default:
        //            break;
        //    }
        //    System.Data.DataTable table = new System.Data.DataTable();

        //    if (!string.IsNullOrEmpty(sql))
        //    {
        //        sda.SelectCommand.CommandText = sql;
        //        sda.Fill(table);
        //    }

        //    return table;
        //}

        //public System.Data.DataTable SelectDataTable(DateTime start, DateTime end, Book.Model.Company company, Book.Model.Employee employee, Helper.InvoiceStatus invoiceStatus, string queryId)
        //{
        //    string sql = "";

        //    sql = "SELECT InvoiceCODetail.ProductId, Product.ProductName, Product.ProductBaseUnit,"
        //        + "SUM(InvoiceCODetail.InvoiceCODetailQuantity) AS Quantity, "
        //        + "SUM(InvoiceCODetail.InvoiceCODetailMoney) AS DetailMoney "
        //        + "FROM ((Product INNER JOIN InvoiceCODetail ON Product.ProductId = InvoiceCODetail.ProductId) INNER JOIN InvoiceCO ON InvoiceCODetail.InvoiceId = InvoiceCO.InvoiceId) "
        //        + "WHERE (InvoiceCO.InvoiceStatus = " + (int)Helper.InvoiceStatus.Normal + ")";

        //    if (employee != null && !string.IsNullOrEmpty(employee.EmployeeId))
        //    {
        //        sql += " AND (InvoiceCO.Employee0Id = '" + employee.EmployeeId + "')";
        //    }
        //    if (company != null && !string.IsNullOrEmpty(company.CompanyId))
        //    {
        //        sql += " AND (InvoiceCO.CompanyId = '" + company.CompanyId + "')";
        //    }

        //    if (start != new DateTime(1, 1, 1, 0, 0, 0) && end != new DateTime(1, 1, 1, 0, 0, 0))
        //    {
        //        sql += " AND (InvoiceCO.InvoiceDate BETWEEN '" + start.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + end.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //    }
        //    sql += "GROUP BY InvoiceCODetail.ProductId, Product.ProductName, Product.ProductBaseUnit";
        //    System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
        //    System.Data.DataTable table = new System.Data.DataTable();
        //    sda.Fill(table);
        //    return table;
        //}

        public System.Data.DataTable SelectDataTable(string queryId)
        {
            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            sda.SelectCommand = new System.Data.SqlClient.SqlCommand();
            sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);

            System.Data.DataTable table = new System.Data.DataTable();

            string sql = "";

            switch (queryId)
            {
                case "Q02":
                    sql = "SELECT Product.ProductName, SUM(Stock.StockQuantity1) as Quantity1,"
                        + "Product.ProductStandardCost FROM Stock INNER JOIN Product ON"
                        + " Stock.ProductId = Product.ProductId GROUP BY Product.ProductName,"
                        + "Product.ProductStandardCost";
                    break;
                case "Q14":
                    sql = "select stock.productid,stock.depotpositionId as posoid , (select DepotName from Depot where DepotId = (select DepotId from depotposition where depotpositionId = stock.depotpositionId)) as DepotName,(select productName from product where productid = stock.productid) productName,(select CustomerProductName from product where productid = stock.productid) CustomerProductName,(select isnull(ProduceMaterialDistributioned,0)+isnull(OtherMaterialDistributioned,0) from product where productid = stock.productid)  yifenpen,(select ProductDescription from product where productid = stock.productid) ProductDescription,(select id from product where productid = stock.productid) spid,(select CnName from ProductUnit INNER JOIN product ON product.DepotUnitId = ProductUnit.ProductUnitId and product.productid = stock.productid) CnName,(select Id from depotposition where depotpositionId = stock.depotpositionId) as DepotPositionId,StockQuantity1 as Quantity from stock where StockQuantity1<>0";
                    break;
            }
            if (!string.IsNullOrEmpty(sql))
            {
                sda.SelectCommand.CommandText = sql;
                sda.Fill(table);
            }
            return table;
        }

        public System.Data.DataTable SelectProductStock()
        {
            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            sda.SelectCommand = new System.Data.SqlClient.SqlCommand();
            sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);
            System.Data.DataTable table = new System.Data.DataTable();
            string sql = "select isnull((select DepotName from Depot where DepotId=s.DepotId),'------') as DepotName,p.productName as ProductName,p.ProductDescription as ProductDescription,p.Id as spid,p.SafeStock as SafeStock,isnull((select CnName from ProductUnit u where p.DepotUnitId = u.ProductUnitId and p.productid = s.productid),'------') as CnName,isnull((select Id from depotposition  where depotpositionId = s.depotpositionId),'------') as DepotPositionId,isnull(s.StockQuantity1,0) as Quantity from product p left join stock s on p.productId =s.productId";
            sda.SelectCommand.CommandText = sql;
            sda.Fill(table);
            return table;
        }

        public System.Data.DataTable SelectByCondition(string queryId, string depotId, string depotPositionId, string productCategoryId, string ProductNameOrId, string proname1, string proname2, string protype1, string protype2, bool check)
        {
            using (System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter())
            {
                sda.SelectCommand = new System.Data.SqlClient.SqlCommand();
                using (sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString))
                {

                    System.Data.DataTable table = new System.Data.DataTable();

                    string sql = "";

                    switch (queryId)
                    {
                        case "Q14":
                            sql = "select s.StockQuantity0,(isnull(p.ProduceMaterialDistributioned,0)+isnull(p.OtherMaterialDistributioned,0)) yifenpeiquantity,s.Stock0Date,d.DepotName,p.Id as spid,p.ProductName,p.CustomerProductName,p.ProductVersion,p.SafeStock,pu.CnName,dp.Id as DepotPositionId,s.StockQuantity1 as Quantity from stock s left join product p on s.ProductId=p.ProductId left join ProductUnit pu on pu.ProductUnitId=p.DepotUnitId left join Depot d on s.DepotId=d.DepotId left join DepotPosition dp on s.DepotPositionId=dp.DepotPositionId WHERE (d.depotid='" + depotId + "'  OR '" + depotId + "'='null' OR '" + depotId + "'='') AND (dp.DepotPositionId ='" + depotPositionId + "' OR '" + depotPositionId + "'='null' OR '" + depotPositionId + "'='') AND (p.productid IN(SELECT ProductId FROM Product WHERE ProductCategoryId='" + productCategoryId + "')  OR '" + productCategoryId + "'='null' OR '" + productCategoryId + "'=''   ) and  (p.productid IN(SELECT ProductId FROM Product WHERE ProductName  like '%" + ProductNameOrId + "%')  OR '" + ProductNameOrId + "'='null' OR '" + ProductNameOrId + "'=''   ) and StockQuantity1<>0";
                            break;
                        case "Q15":
                            sql = "select stock.StockQuantity0,stock.Stock0Date,stock.productid,stock.depotpositionId as posoid , (select DepotName from Depot where DepotId = (select DepotId from depotposition where depotpositionId = stock.depotpositionId)) as DepotName,(select productName from product where productid = stock.productid) productName, (select OrderOnWayQuantity from Product where productid = stock.productid) as OrderOnWayQuantity,(select SafeStock from product where productid = stock.productid) SafeStock,(select CustomerProductName from product where productid = stock.productid) CustomerProductName,(select id from product where productid = stock.productid) spid,(select CnName from ProductUnit INNER JOIN product ON product.DepotUnitId = ProductUnit.ProductUnitId and product.productid = stock.productid) CnName,(select Id from depotposition where depotpositionId = stock.depotpositionId)  as DepotPositionId,StockQuantity1 as Quantity,0.0 as yifenpeiquantity from stock WHERE (depotid='" + depotId + "'  OR '" + depotId + "'='null' OR '" + depotId + "'='') AND (DepotPositionId ='" + depotPositionId + "' OR '" + depotPositionId + "'='null' OR '" + depotPositionId + "'='') AND (productid IN(SELECT ProductId FROM Product WHERE ProductCategoryId='" + productCategoryId + "')  OR '" + productCategoryId + "'='null' OR '" + productCategoryId + "'=''   ) and  (productid IN(SELECT ProductId FROM Product WHERE ProductName  like '%" + ProductNameOrId + "%')  OR '" + ProductNameOrId + "'='null' OR '" + ProductNameOrId + "'=''   ) ";
                            if (check == false)
                                sql += "and StockQuantity1<>0";
                            if (!string.IsNullOrEmpty(proname1) && !string.IsNullOrEmpty(proname2))
                                sql += " and stock.productid in(select productid from product where productname between '" + proname1 + "' and '" + proname2 + "' )";
                            if (!string.IsNullOrEmpty(protype1) && !string.IsNullOrEmpty(protype2))
                                sql += " and stock.productid in(select productid from product where ProductCategoryId in (select ProductCategoryId from ProductCategory where   id between '" + protype1 + "' and '" + protype2 + "' ))  ";
                            sql += " order by productid";
                            break;
                    }
                    if (!string.IsNullOrEmpty(sql))
                    {
                        sda.SelectCommand.CommandText = sql;
                        sda.Fill(table);
                    }
                    return table;
                }
            }
        }


        public System.Data.DataTable Q15(int day)
        {
            string sql = "select * from company where companypaydate = " + day;
            System.Data.DataTable table = new System.Data.DataTable();
            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            sda.Fill(table);
            return table;
        }

        public IList<Model.Stock> Select(string startid, string endid)
        {
            //string sql = "SELECT [StockId],[ProductId],[DepotId],[StockQuantity0],[StockQuantity1],[StockQuantityD],[StockQuantityU],[StockQuantityD]-[StockQuantity1] AS DiffQuantityFROM [Stock] WHERE ([StockQuantity1] < [StockQuantityD]) AND ([ProductId] BETWEEN @startid And @endid)";


            //System.Data.SqlClient.SqlParameter s1 = new System.Data.SqlClient.SqlParameter("@startid", startid);
            //System.Data.SqlClient.SqlParameter s2 = new System.Data.SqlClient.SqlParameter("@endid", endid);

            //System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            //sda.SelectCommand = new System.Data.SqlClient.SqlCommand(sql);
            //sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);
            //sda.SelectCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] { s1, s2 });

            //System.Data.DataTable dt = new System.Data.DataTable();
            //sda.Fill(dt);
            //return dt;

            System.Collections.Hashtable table = new System.Collections.Hashtable();
            table.Add("startId", startid);
            table.Add("endId", endid);
            return sqlmapper.QueryForList<Model.Stock>("MiscData.Stock", table);
        }


        public System.Data.DataTable Select(DateTime start, DateTime end, string startId, string endId)
        {
            StringBuilder str = new StringBuilder();

            str.Append("select *, isnull(( InvoiceXSDetailQuantity-InvoiceXTDetailQuantity),0) ");
            str.Append("as InvoiceDetailQuantity, isnull((InvoiceXSDetailMoney1-InvoiceXTDetailMoney1),0) ");
            str.Append("as InvoiceDetailMoney from (select ProductId,id,ProductName,ProductSpecification,CustomerProductName,(select CnName from ProductUnit where ProductUnitid=product.SellUnitId) as CnName");
            str.Append(", (select isnull(sum(invoicexsdetailquantity),0) from invoicexsdetail where productid = ");
            str.Append("product.productid and invoiceid in (select invoiceid from invoicexs where invoiceDate ");
            str.Append("between @start and @end)) as InvoiceXSDetailQuantity");
            str.Append(",(select isnull(sum(invoicextdetailquantity),0) from invoicextdetail where ");
            str.Append("productid = product.productid and invoiceid in (select invoiceid from invoicext where ");
            str.Append("invoiceDate between @start and @end)) as InvoiceXTDetailQuantity");
            str.Append(",(select isnull(sum(InvoiceXSDetailMoney),0) from invoicexsdetail where ");
            str.Append("productid = product.productid and invoiceid in (select invoiceid from invoicexs ");
            str.Append("where invoicedate between @start and @end)) as InvoiceXSDetailMoney1");
            str.Append(", (select isnull(sum(InvoiceXTDetailMoney1),0) from invoicextdetail where ");
            str.Append("productid = product.productid and invoiceId in (select Invoiceid from invoicext ");
            str.Append("where invoicedate between @start and @end)) as InvoiceXTDetailMoney1 ");
            str.Append("from product where productid in(select productid  from invoicexsdetail) ");
            if (string.IsNullOrEmpty(startId) && !string.IsNullOrEmpty(endId))
                str.Append("  and product.productid between @startid and @endid");
            str.Append(" ) a  order by invoicexsdetailquantity desc");

            System.Data.SqlClient.SqlParameter s1 = new System.Data.SqlClient.SqlParameter("@start", start);
            System.Data.SqlClient.SqlParameter s2 = new System.Data.SqlClient.SqlParameter("@end", end);
            System.Data.SqlClient.SqlParameter s3 = new System.Data.SqlClient.SqlParameter("@startid", startId);
            System.Data.SqlClient.SqlParameter s4 = new System.Data.SqlClient.SqlParameter("@endid", endId);


            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            sda.SelectCommand = new System.Data.SqlClient.SqlCommand(str.ToString());
            sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);
            sda.SelectCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] { s1, s2, s3, s4 });

            System.Data.DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            return dt;
            //System.Collections.Hashtable table = new System.Collections.Hashtable();
            //table.Add("startDate", start);
            //table.Add("endDate", end);
            //table.Add("psid", startId);
            //table.Add("peid", endId);
            //return sqlmapper.QueryForList("MiscData.product", table);
        }

        public System.Data.DataTable Select1(DateTime start, DateTime end, string startId, string endId)
        {
            string sql = " select EmployeeId ,EmployeeName"
                + ",isnull((select sum(InvoiceHeJi) from InvoiceXS where Employee0Id = Employee.EmployeeId and InvoiceDate between @start and @end),0) as InvoiceXSMoney"
                + ",isnull((select sum(InvoiceZRE) from InvoiceXS where Employee0Id = Employee.EmployeeId and InvoiceDate between @start and @end),0) as InvoiceXSZRE"
                + ",isnull((select sum(InvoiceZongJi) from InvoiceXT where Employee0Id = Employee.EmployeeId and InvoiceDate between @start and @end),0) as InvoiceXTMoney"
                + ",isnull((select sum(InvoiceZongJi) from InvoiceXS where Employee0Id = Employee.EmployeeId and InvoiceDate between @start and @end),0) as InvoiceZongJi" +
                " from Employee where EmployeeId between @startid and @endid order by InvoiceXSMoney";

            System.Data.SqlClient.SqlParameter s1 = new System.Data.SqlClient.SqlParameter("@start", start);
            System.Data.SqlClient.SqlParameter s2 = new System.Data.SqlClient.SqlParameter("@end", end);
            System.Data.SqlClient.SqlParameter s3 = new System.Data.SqlClient.SqlParameter("@startid", startId);
            System.Data.SqlClient.SqlParameter s4 = new System.Data.SqlClient.SqlParameter("@endid", endId);

            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            sda.SelectCommand = new System.Data.SqlClient.SqlCommand(sql);
            sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);
            sda.SelectCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] { s1, s2, s3, s4 });

            System.Data.DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            return dt;
            //System.Collections.Hashtable table = new System.Collections.Hashtable();
            //table.Add("startDate", start);
            //table.Add("endDate", end);
            //table.Add("esid", startId);
            //table.Add("eeid", endId);
            //return sqlmapper.QueryForList("MiscData.employee", table);
        }

        public System.Data.DataTable SelectDataTable(DateTime endDate)
        {
            string sql = " select CompanyId, CompanyName1, CompanyR0"
                + ",isnull((select sum(InvoiceOwed) from invoicexs where CompanyId = Company.CompanyId and datediff(d,InvoiceDate,@date) between 1 and 30),0) thirtyDays"
                + ",isnull((select sum(InvoiceOwed) from invoicexs where CompanyId = Company.CompanyId and datediff(d,InvoiceDate,@date) between 31 and 60),0) sixtyDays"
                + ",isnull((select sum(InvoiceOwed) from invoicexs where CompanyId = Company.CompanyId and datediff(d,InvoiceDate,@date) between 61 and 90),0) nintyDays"
                + ",isnull((select sum(InvoiceOwed) from invoicexs where CompanyId = Company.CompanyId and datediff(d,InvoiceDate,@date) between 91 and 120),0) tenTwelveDays"
                + ",isnull((select sum(InvoiceOwed) from invoicexs where CompanyId = Company.CompanyId and datediff(d,InvoiceDate,@date) between 121 and 150),0) tenFifteenDays"
                + ",isnull((select sum(InvoiceOwed) from invoicexs where CompanyId = Company.CompanyId and datediff(d,InvoiceDate,@date) >150),0) greatTenFifteenDays"
                + ",isnull((select sum(InvoiceOwed) from invoicexs where CompanyId = Company.CompanyId),0) allMoney From Company";
            System.Data.DataTable table = new System.Data.DataTable();
            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            sda.SelectCommand = new System.Data.SqlClient.SqlCommand(sql);
            sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);
            System.Data.SqlClient.SqlParameter p = new System.Data.SqlClient.SqlParameter("@date", System.Data.SqlDbType.DateTime);
            p.Value = endDate;
            sda.SelectCommand.Parameters.Add(p);
            sda.Fill(table);
            return table;

            //return sqlmapper.QueryForList("MiscData.ZL", endDate);
        }

        public System.Data.DataTable SelectDataTable(DateTime startDate, DateTime endDate, string startId, string endId)
        {
            string sql = "select  * "
                + ",(select ProductName from Product where t.productId = Product.ProductId) as ProductName"
                + ",(select productbaseunit from Product where t.productId = Product.ProductId) as productbaseunit"
                + ",(select ProductSpecification from Product where t.productId = Product.ProductId) as ProductSpecification"
                + ",[InvoiceXSDetailQuantity]*([InvoiceXSDetailPrice]-InvoiceXSDetailCostPrice) as MaoLi"
                + ",[InvoiceXSDetailQuantity]*([InvoiceXSDetailPrice]-InvoiceXSDetailCostPrice)/[InvoiceXSDetailMoney1] as MaoLiLV "
                + "from (SELECT [InvoiceXSDetailId] ,[ProductId],[InvoiceId],[InvoiceXSDetailQuantity],[InvoiceXSDetailPrice],[InvoiceXSDetailMoney0]"
                + ",[InvoiceXSDetailZS],[InvoiceXSDetailMoney1],InvoiceXSDetailCostPrice,InvoiceXSDetailCostMoney"
                + ",(select Invoicedate from InvoiceXs where invoiceid = [InvoiceXsDetail].invoiceid) as InvoiceDate "
                + "FROM [InvoiceXSDetail] where InvoiceId in (select InvoiceId from invoicexs where InvoiceDate between @start and @end) "
                + "union SELECT [InvoiceXTDetailId],[ProductId],[InvoiceId],-[InvoiceXTDetailQuantity],[InvoiceXTDetailPrice],-[InvoiceXTDetailMoney0]"
                + ",[InvoiceXTDetailZS] ,-[InvoiceXTDetailMoney1],InvoiceXTDetailCostPrice ,-InvoiceXTDetailCostMoney"
                + ",(select Invoicedate from InvoiceXt where invoiceid = [InvoiceXTDetail].invoiceid) as InvoiceDate "
                + "FROM [InvoiceXTDetail] where InvoiceId in (select InvoiceId from invoicext where InvoiceDate between @start and @end)) t "
                + "where ([InvoiceXSDetailZS] <> 1)and ([ProductId] between @startid and @endid)";

            System.Data.SqlClient.SqlParameter s1 = new System.Data.SqlClient.SqlParameter("@start", startDate);
            System.Data.SqlClient.SqlParameter s2 = new System.Data.SqlClient.SqlParameter("@end", endDate);
            System.Data.SqlClient.SqlParameter s3 = new System.Data.SqlClient.SqlParameter("@startid", startId);
            System.Data.SqlClient.SqlParameter s4 = new System.Data.SqlClient.SqlParameter("@endid", endId);

            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            sda.SelectCommand = new System.Data.SqlClient.SqlCommand(sql);
            sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);
            sda.SelectCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] { s1, s2, s3, s4 });

            System.Data.DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            return dt;


            //System.Collections.Hashtable table = new System.Collections.Hashtable();
            //table.Add("startDate", startDate);
            //table.Add("endDate", endDate);
            //table.Add("psid", startId);
            //table.Add("peid", endId);
            //return sqlmapper.QueryForList("MiscData.LR", table);
        }

        //public System.Data.DataTable SelectDataTable(DateTime startDate, DateTime endDate, Book.Model.Company company)
        //{
        //    string sql = "";
        //    //System.Collections.Hashtable pars = new System.Collections.Hashtable();
        //    //pars.Add("companyId", company.CompanyId);
        //    //pars.Add("startDate", startDate);
        //    //pars.Add("endDate", endDate);
        //    switch (company.CompanyKind.Value)
        //    {
        //        case (int)Helper.CompanyKind.Customer:

        //            sql = "select * from (select *"
        //                + ",(Select InvoiceDate from Invoicesk where invoiceId = xr1.InvoiceskId) as InvoiceDate"
        //                + ",(Select CompanyId from Invoicesk where invoiceId = xr1.InvoiceskId) as CompanyId "
        //                + "from xr1 ) t where CompanyId = @companyid and (InvoiceDate between @start and @end)";
        //            break;
        //        case (int)Helper.CompanyKind.Supplier:
        //            sql = "select * from (select * "
        //                + ",(Select InvoiceDate from Invoicefk where invoiceId = xp1.InvoicefkId) as InvoiceDate"
        //                + ",(Select CompanyId from Invoicefk where invoiceId = xp1.InvoicefkId) as CompanyId "
        //                + "from xp1 ) t where CompanyId = @companyid and (InvoiceDate between @start and @end)";
        //            break;
        //    }

        //    System.Data.SqlClient.SqlParameter s1 = new System.Data.SqlClient.SqlParameter("@start", startDate);
        //    System.Data.SqlClient.SqlParameter s2 = new System.Data.SqlClient.SqlParameter("@end", endDate);
        //    System.Data.SqlClient.SqlParameter s3 = new System.Data.SqlClient.SqlParameter("@companyid", company.CompanyId);            

        //    System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
        //    sda.SelectCommand = new System.Data.SqlClient.SqlCommand(sql);
        //    sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);
        //    sda.SelectCommand.Parameters.AddRange(new System.Data.SqlClient.SqlParameter[] { s1, s2, s3 });

        //    System.Data.DataTable dt = new System.Data.DataTable();
        //    sda.Fill(dt);
        //    return dt;
        //}

        #endregion

        public System.Data.DataTable SelectForSupplierTransactionRank(DateTime startDate, DateTime endDate, string Startid, string EndId, string CategoryId)
        {
            //数据量大的情况下执行非常耗时会产生超时错误
            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY (s1.JinHuoJinE-s1.ZheRangJinE-s1.TuiHuoJinE) DESC) AS SortId,*,(s1.JinHuoJinE-s1.ZheRangJinE-s1.TuiHuoJinE) AS ShiJinJinE FROM ");
            //sb.Append("(SELECT Id,SupplierFullName,");

            //sb.Append("isnull(isnull((SELECT COUNT(1) FROM InvoiceCGDetail  WHERE InvoiceId IN  (SELECT InvoiceId FROM InvoiceCG WHERE InvoiceCG.SupplierId = Supplier.SupplierId AND InvoiceCG.InvoiceHisDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "')),0)+isnull(( SELECT count(1) FROM ProduceOtherInDepotDetail WHERE ProduceOtherInDepotId IN (SELECT ProduceOtherInDepotId FROM ProduceOtherInDepot WHERE ProduceOtherInDepot.SupplierId=Supplier.SupplierId AND ProduceOtherInDepot.ProduceOtherInDepotDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "')),0)+isnull((SELECT COUNT(1) FROM ProduceInDepotDetail WHERE ProduceInDepotId IN (SELECT ProduceInDepot.ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId in (SELECT WorkHouse.WorkHouseId FROM WorkHouse WHERE WorkHouse.Workhousename = Supplier.SupplierShortName) AND ProduceInDepot.ProduceInDepotDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "' )),0)	,0) AS TotalQuantity,");

            //sb.Append("isnull(isnull((SELECT SUM(InvoiceHeji) FROM InvoiceCG WHERE InvoiceCG.SupplierId = Supplier.SupplierId AND InvoiceCG.InvoiceHisDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "'),0)+isnull(( SELECT sum(ProduceOtherInDepotDetail.ProduceMoney) FROM ProduceOtherInDepotDetail WHERE ProduceOtherInDepotDetail.ProduceOtherInDepotId IN (SELECT ProduceOtherInDepotId FROM ProduceOtherInDepot WHERE ProduceOtherInDepot.SupplierId=Supplier.SupplierId AND ProduceOtherInDepot.ProduceOtherInDepotDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "')),0)+isnull((SELECT sum(ProduceInDepotDetail.ProduceMoney) FROM ProduceInDepotDetail WHERE ProduceInDepotDetail.ProduceInDepotId IN(SELECT ProduceInDepot.ProduceInDepotId FROM ProduceInDepot WHERE WorkHouseId IN(SELECT WorkHouse.WorkHouseId FROM WorkHouse WHERE WorkHouse.Workhousename=Supplier.SupplierShortName)AND ProduceInDepot.ProduceInDepotDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "')),0),0) AS JinHuoJinE,");

            //sb.Append("isnull((SELECT SUM(InvoiceAllowance) FROM InvoiceCG WHERE InvoiceCG.SupplierId = Supplier.SupplierId  AND InvoiceCG.InvoiceHisDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "'),0) AS ZheRangJinE,");
            //sb.Append("isnull((SELECT SUM(InvoiceHeJi) FROM InvoiceCT WHERE InvoiceCT.SupplierId = Supplier.SupplierId AND InvoiceCT.InvoiceDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.AddDays(1).ToString("yyyy-MM-dd") + "'),0) AS TuiHuoJinE");
            //sb.Append(" FROM Supplier Where 1=1 ");
            //if (!(string.IsNullOrEmpty(Startid) && string.IsNullOrEmpty(EndId)))
            //{
            //    if (!string.IsNullOrEmpty(Startid) && !string.IsNullOrEmpty(EndId))
            //        sb.Append(" And Supplier.Id BETWEEN '" + Startid + "' AND '" + EndId + "'");
            //    else
            //        sb.Append(" And Supplier.Id='" + (Startid == null ? EndId : Startid) + "'");
            //}
            //if (!string.IsNullOrEmpty(CategoryId))
            //    sb.Append(" And SupplierCategoryId in (" + CategoryId.Substring(0, CategoryId.LastIndexOf(',')) + ")");
            //sb.Append(") AS s1 WHERE s1.JinHuoJinE<>0 OR s1.ZheRangJinE <> 0 OR s1.TuiHuoJinE <> 0");

            //System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            //sda.SelectCommand = new System.Data.SqlClient.SqlCommand(sb.ToString());
            //sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);

            //System.Data.DataTable dt = new System.Data.DataTable();
            //sda.Fill(dt);
            //return dt;


            //存储过程
            SqlConnection sqlconn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlconn;
            cmd.CommandText = "proc_SupplierRank";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter[] parameter = { 
                                     new SqlParameter("@StartDate",SqlDbType.DateTime),
                                     new SqlParameter("@EndDate",SqlDbType.DateTime),
                                     new SqlParameter("@StartId",SqlDbType.VarChar,50),
                                     new SqlParameter("@EndId",SqlDbType.VarChar,50),
                                     new SqlParameter("@CategoryId",SqlDbType.VarChar)
                                     };
            parameter[0].Value = startDate;
            parameter[1].Value = endDate;
            parameter[2].Value = Startid;
            parameter[3].Value = EndId;
            parameter[4].Value = CategoryId == null ? null : CategoryId.Substring(0, CategoryId.LastIndexOf(','));

            cmd.Parameters.AddRange(parameter);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.SelectCommand.CommandTimeout = 0;
            sda.Fill(dt);
            return dt;
        }



        public System.Data.DataTable SelectForCustomerTransactionRank(DateTime startDate, DateTime endDate, string Startid, string EndId, string StartChuHuoId, string EndChuHuoId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ROW_NUMBER() OVER(ORDER BY Id DESC) AS SortId,*,(s1.JinHuoJinE-s1.ZheRangJinE-s1.TuiHuoJinE) AS ShiJinJinE FROM ");
            sb.Append("(SELECT Id,CustomerFullName,");
            sb.Append("isnull((SELECT SUM(InvoiceHeji) FROM InvoiceXS WHERE InvoiceXS.CustomerId = Customer.CustomerId AND InvoiceXS.InvoiceDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "' GROUP BY InvoiceXS.CustomerId),0) AS JinHuoJinE,");
            sb.Append("isnull((SELECT SUM(InvoiceAllowance) FROM InvoiceXS WHERE InvoiceXS.CustomerId = Customer.CustomerId AND InvoiceXS.InvoiceDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "' GROUP BY InvoiceXS.CustomerId),0) AS ZheRangJinE,");
            sb.Append("isnull((SELECT SUM(InvoiceHeJi) FROM InvoiceXT WHERE InvoiceXT.CustomerId = Customer.CustomerId AND InvoiceXT.InvoiceDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "' GROUP BY InvoiceXT.CustomerId),0) AS TuiHuoJinE");
            sb.Append(" FROM Customer Where 1=1 ");

            if (!string.IsNullOrEmpty(Startid) && !string.IsNullOrEmpty(EndId) && Startid != EndId)
                sb.Append(" And Customer.Id BETWEEN '" + Startid + "' AND '" + EndId + "'");
            else if (!string.IsNullOrEmpty(Startid))
                sb.Append(" And Customer.Id ='" + Startid + "'");

            if (!string.IsNullOrEmpty(StartChuHuoId) && !string.IsNullOrEmpty(EndChuHuoId) && StartChuHuoId != EndChuHuoId)
                sb.Append(" AND CustomerId IN (SELECT CustomerId FROM InvoiceXO WHERE CustomerInvoiceXOId IN (SELECT CustomerId FROM Customer WHERE Id BETWEEN '" + StartChuHuoId + "' AND '" + EndChuHuoId + "'))");
            else if (!string.IsNullOrEmpty(StartChuHuoId))
                sb.Append(" AND CustomerId IN (SELECT CustomerId FROM InvoiceXO WHERE CustomerInvoiceXOId IN (SELECT CustomerId FROM Customer WHERE Id = '" + StartChuHuoId + "'))");

            sb.Append(") AS s1 WHERE s1.JinHuoJinE<>0 OR s1.ZheRangJinE <> 0 OR s1.TuiHuoJinE <> 0");

            System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter();
            sda.SelectCommand = new System.Data.SqlClient.SqlCommand(sb.ToString());
            sda.SelectCommand.Connection = new System.Data.SqlClient.SqlConnection(sqlmapper.DataSource.ConnectionString);

            System.Data.DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}