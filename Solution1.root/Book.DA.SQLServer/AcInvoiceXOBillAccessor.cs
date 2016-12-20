//------------------------------------------------------------------------------
//
// file name：AcInvoiceXOBillAccessor.cs
// author: mayanjun
// create date：2011-09-28 08:45:16
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
    /// Data accessor of AcInvoiceXOBill
    /// </summary>
    public partial class AcInvoiceXOBillAccessor : EntityAccessor, IAcInvoiceXOBillAccessor
    {
        public IList<Model.AcInvoiceXOBill> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", startdate);
            ht.Add("EndDate", enddate);
            return sqlmapper.QueryForList<Model.AcInvoiceXOBill>("AcInvoiceXOBill.selectForDateRange", ht);
        }
        public void UpdateHeXiaoByAcInvoiceXOBillId(Hashtable param)
        {
            foreach (DictionaryEntry dic in param)
            {
                string AcInvoiceXOBillId = dic.Key.ToString();
                string HeXiao = dic.Value == null ? "0" : dic.Value.ToString();
                Hashtable ht = new Hashtable();
                ht.Add("AcInvoiceXOBillId", AcInvoiceXOBillId);
                ht.Add("HeXiao", HeXiao);
                sqlmapper.Update("AcInvoiceXOBill.UpdateHeXiaoByAcInvoiceXOBillId", ht);
            }
        }
        public DataSet SelectCuiShou(Model.Customer customer1, Model.Customer customer2, Model.Employee employee1, Model.Employee employee2,DateTime ysdate)
        {
            StringBuilder sql = new StringBuilder();
            if (customer1 != null && customer2 != null)
                sql.Append(" and a.CustomerId in( select CustomerId from Customer where id between '" + customer1.Id + "' and  '" + customer2.Id + "') and YSDate < '" + ysdate.Date.ToString("yyyyMMdd")+ "'  ");
            if (employee1 != null && employee2 != null)
                sql.Append(" and Employee1Id in( select EmployeeId from Employee where idno between '" + employee1.IDNo + "' and  '" + employee2.IDNo + "') ");

            return SQLDB.DbHelperSQL.Query("select YSDate ,isnull(datediff(d, YSDate,getdate()),0) as CQDays,AcInvoiceXOBillDate as InvoiceDate,AcInvoiceXOBillId as InvoiceId,'銷售發票' as SourceName,ZongMoney as Total,mHeXiaoJingE as HasMoney,InvoiceAllowanceTotal as ZheRang,NoHeXiaoTotal NoHeXiao,(select CustomerShortName  from Customer c where c.CustomerId=a.CustomerId) as CusomerName from AcInvoiceXOBill a where  isnull(datediff(d, YSDate,getdate()),0)>0 " + sql.ToString());

        }
        public DataSet SelectMayShou(Model.Customer customer1, Model.Customer customer2, Model.Employee employee1, Model.Employee employee2, DateTime startDate, DateTime endDate)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("  AcInvoiceXOBillDate>= '" + startDate.ToString("yyyy-MM-dd") + "' and AcInvoiceXOBillDate<'" + endDate.Date.AddDays(1).ToString("yyyy-MM-dd") + "'");
            if (customer1 != null && customer2 != null)
                sql.Append(" and CustomerId in( select CustomerId from Customer where id between '" + customer1.Id + "' and  '" + customer2.Id + "') ");
            if (employee1 != null && employee2 != null)
                sql.Append(" and Employee0Id in( select EmployeeId from Employee where idno between '" + employee1.IDNo + "' and  '" + employee2.IDNo + "') ");
            sql.Append("order by InvoiceDate");

            return SQLDB.DbHelperSQL.Query("select AcInvoiceXOBillDate as InvoiceDate,AcInvoiceXOBillId as InvoiceId,'銷售發票' as SourceName,ZongMoney as Total,mHeXiaoJingE as HasMoney,NoHeXiaoTotal NoHeXiao,(select EmployeeName from Employee e where e.Employeeid=a.Employee1Id) as Employee1Name,(select CustomerShortName  from Customer c where c.CustomerId=a.CustomerId) as CusomerName from AcInvoiceXOBill a where   " + sql.ToString());

        }
    }
}
