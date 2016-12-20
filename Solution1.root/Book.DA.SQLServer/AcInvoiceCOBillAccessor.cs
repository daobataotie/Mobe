//------------------------------------------------------------------------------
//
// file name：AcInvoiceCOBillAccessor.cs
// author: mayanjun
// create date：2011-06-27 15:07:21
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Text;
using IBatisNet.DataMapper;
namespace Book.DA.SQLServer
{
    /// <summary>
    /// Data accessor of AcInvoiceCOBill
    /// </summary>
    public partial class AcInvoiceCOBillAccessor : EntityAccessor, IAcInvoiceCOBillAccessor
    {
        public IList<Model.AcInvoiceCOBill> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", startdate);
            ht.Add("EndDate", enddate);
            return sqlmapper.QueryForList<Model.AcInvoiceCOBill>("AcInvoiceCOBill.selectForDateRange", ht);
           
        }

        public void UpdateHeXiaobyAcinvoiceCOId(Hashtable ht)
        {
            foreach (DictionaryEntry dic in ht)
            {
                         

                string AcInvoiceCOBillId = dic.Key.ToString();
                string HeXiao = dic.Value == null ? "0" : dic.Value.ToString();
                Hashtable par = new Hashtable();
                par.Add("AcInvoiceCOBillId", AcInvoiceCOBillId);
                par.Add("HeXiao", HeXiao);
                sqlmapper.Update("AcInvoiceCOBill.UpdateHeXiaobyAcinvoiceCOId", par);
            }
        }

        public DataSet SelectMayShou(Model.Supplier supplier1, Model.Supplier supplier2, Model.Employee employee1, Model.Employee employee2, DateTime startDate, DateTime endDate)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select AcInvoiceCOBillDate as InvoiceDate,AcInvoiceCOBillId as InvoiceId,'採購發票' as SourceName,ZongMoney as Total,mHeXiaoJingE as HasMoney,NoHeXiaoTotal NoHeXiao,(select EmployeeName from Employee e where e.Employeeid=a.Employee1Id) as Employee1Name,(select SupplierShortName  from Supplier S where S.SupplierId=a.SupplierId) as SupplierName,isnull(HeJiMoney,0) AS JinE,isnull(TaxRateMoney,0) AS ShuiE,isnull(ZongMoney,0) AS Total from AcInvoiceCOBill a where 1=1 ");
            sql.Append(" AND AcInvoiceCOBillDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");

            if (supplier1 != null && supplier2 != null)
                sql.Append(" and SupplierId in( select SupplierId from Supplier where id between '" + supplier1.Id + "' and  '" + supplier2.Id + "') ");
            if (employee1 != null && employee2 != null)
                sql.Append(" and Employee0Id in( select EmployeeId from Employee where idno between '" + employee1.IDNo + "' and  '" + employee2.IDNo + "') ");

            sql.Append(" UNION ");

            sql.Append("SELECT InvoiceDate,InvoiceId,'採購訂單' AS SourceName,InvoiceTotal AS Total,'0' AS HasMoney,'0' AS NoHeXiao,(select EmployeeName from Employee e where e.Employeeid=ic.Employee1Id) as Employee1Name,(select SupplierShortName from Supplier S where S.SupplierId=ic.SupplierId) as SupplierName,isnull(InvoiceHeji,0) AS JinE,isnull(InvoiceTax,0) AS ShuiE,isnull(InvoiceTotal,0) AS Total FROM InvoiceCO ic WHERE 1 = 1");
            sql.Append(" AND InvoiceDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");

            if (supplier1 != null && supplier2 != null)
                sql.Append(" and SupplierId in( select SupplierId from Supplier where id between '" + supplier1.Id + "' and  '" + supplier2.Id + "') ");
            if (employee1 != null && employee2 != null)
                sql.Append(" and Employee0Id in( select EmployeeId from Employee where idno between '" + employee1.IDNo + "' and  '" + employee2.IDNo + "') ");

            //sql.Append("SELECT AcOtherShouldPaymentDate AS InvoiceDate,AcOtherShouldPaymentId AS InvoiceId,'其它應付款' as SourceName,InvoiceHeji as Total,mHeXiaoJingE as HasMoney,NoHeXiaoTotal as NoHeXiao,(SELECT e.EmployeeName FROM Employee e WHERE e.EmployeeId = asp.Employee1Id) AS Employee1Name,(SELECT s.SupplierShortName FROM Supplier s WHERE s.SupplierId = asp.SupplierId) AS SupplierName FROM AcOtherShouldPayment asp WHERE 1=1 ");
            //sql.Append(" AND AcOtherShouldPaymentDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");

            //if (supplier1 != null && supplier2 != null)
            //    sql.Append(" and SupplierId in (select SupplierId from Supplier where id between '" + supplier1.Id + "' and  '" + supplier2.Id + "') ");
            //if (employee1 != null && employee2 != null)
            //    sql.Append(" and Employee0Id in (select EmployeeId from Employee where idno between '" + employee1.IDNo + "' and  '" + employee2.IDNo + "') ");


            sql.Append(" ORDER BY InvoiceDate");
            return SQLDB.DbHelperSQL.Query("" + sql.ToString());
        }

    }
}
