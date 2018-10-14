//------------------------------------------------------------------------------
//
// file name：DepotInAccessor.cs
// author: mayanjun
// create date：2010-10-25 16:14:48
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
    /// Data accessor of DepotIn
    /// </summary>
    public partial class DepotInAccessor : EntityAccessor, IDepotInAccessor
    {
        public IList<Model.DepotIn> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.DepotIn>("DepotIn.SelectByDateRange", ht);
        }

        public IList<Model.DepotIn> SelectByDateAndOther(DateTime startDate, DateTime endDate, Model.Product product, string depotInId, Model.Employee employee, Model.Employee employee0, string depotId, Model.Supplier supplier)
        {
            StringBuilder sql = new StringBuilder();
            //sql.Append("SELECT DepotInId,DepotInDate,Description,(SELECT EmployeeName FROM Employee WHERE Employee.EmployeeId=depotIn.EmployeeId) AS EmployeeId,(SELECT EmployeeName FROM Employee WHERE Employee.EmployeeId=depotIn.Employee0Id) AS Employee0Id,(SELECT DepotName FROM Depot WHERE Depot.DepotId=depotIn.DepotId) AS DepotId,(SELECT SupplierShortName FROM Supplier WHERE SupplierId=depotIn.SupplierId) AS SupplierId FROM depotIn WHERE DepotInDate BETWEEN '" + startDate + "' AND '" + endDate + "'");
            sql.Append("SELECT d.DepotInId,d.DepotInDate,d.Description,(SELECT EmployeeName FROM Employee WHERE Employee.EmployeeId=d.EmployeeId) AS EmployeeId,(SELECT EmployeeName FROM Employee WHERE Employee.EmployeeId=d.Employee0Id) AS Employee0Id,(SELECT DepotName FROM Depot WHERE Depot.DepotId=d.DepotId) AS DepotId,(SELECT SupplierShortName FROM Supplier WHERE SupplierId=d.SupplierId) AS SupplierId,(select ProductName from Product p where p.ProductId=did.ProductId) as ProductName FROM depotIn d right join DepotInDetail did on d.DepotInId=did.DepotInId WHERE DepotInDate BETWEEN '" + startDate + "' AND '" + endDate + "'");
            if (product != null)
                sql.Append(" AND d.DepotInId IN (SELECT DepotInId FROM DepotInDetail WHERE ProductId='" + product.ProductId + "')");
            if (depotInId != null)
                sql.Append(" AND d.DepotInId='" + depotInId + "'");
            if (employee != null)
                sql.Append(" AND d.EmployeeId='" + employee.EmployeeId + "'");
            if (employee0 != null)
                sql.Append(" AND d.Employee0Id='" + employee0.EmployeeId + "'");
            if (supplier != null)
                sql.Append(" AND d.SupplierId='" + supplier.SupplierId + "'");
            if (depotId != null)
                sql.Append(" AND d.DepotId='" + depotId + "'");
            sql.Append(" ORDER BY DepotInDate desc");

            return this.DataReaderBind<Model.DepotIn>(sql.ToString(), null, CommandType.Text);
        }
    }
}
