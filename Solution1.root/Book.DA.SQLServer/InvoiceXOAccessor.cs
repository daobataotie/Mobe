//------------------------------------------------------------------------------
//
// file name:InvoiceXOAccessor.cs
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
    /// Data accessor of InvoiceXO
    /// </summary>
    public partial class InvoiceXOAccessor : EntityAccessor, IInvoiceXOAccessor
    {
        #region IInvoiceXOAccessor 成员

        public IList<Book.Model.InvoiceXO> Select(DateTime start, DateTime end)
        {
            Hashtable pars = new Hashtable();
            pars.Add("startTime", start);
            pars.Add("endTime", end);
            return sqlmapper.QueryForList<Model.InvoiceXO>("InvoiceXO.select_byTime", pars);
        }

        public IList<Book.Model.InvoiceXO> Select(Helper.InvoiceStatus status)
        {
            return sqlmapper.QueryForList<Model.InvoiceXO>("InvoiceXO.select_byStatus", (int)status);
        }

        public IList<Book.Model.InvoiceXO> SelectNoDeal()
        {
            return sqlmapper.QueryForList<Book.Model.InvoiceXO>("InvoiceXO.select_byYJRQ", DateTime.Now);
        }

        public IList<Book.Model.InvoiceXO> SelectByYJRQCustomEmpCusXOId(Model.Customer customer1, Model.Customer customer2, DateTime startDate, DateTime endDate, DateTime yjrq1, DateTime yjrq2, Model.Employee employee1, Model.Employee employee2, string xoid1, string xoid2, string cusxoidkey, Model.Product product, Model.Product product2, bool isclose, bool mpsIsClose, bool isForeigntrade, string handBookId)
        {
            StringBuilder str = new StringBuilder();
            if (customer1 != null && customer2 != null)
                str.Append(" and xocustomerId in (select CustomerId from customer where id between '" + customer1.Id + "' and '" + customer2.Id + "')");
            if (employee1 != null && employee2 != null)
                str.Append(" and  Employee0Id in(select EmployeeId from Employee where idno between '" + employee1.IDNo + "' and '" + employee2.IDNo + "') ");
            if (!string.IsNullOrEmpty(xoid1) && !string.IsNullOrEmpty(xoid2))
                str.Append(" and  InvoiceId between '" + xoid1 + "' and '" + xoid2 + "' ");
            if (!string.IsNullOrEmpty(cusxoidkey))
                str.Append(" and  CustomerInvoiceXOId = '" + cusxoidkey + "' ");
            if (product != null && product2 != null)
                str.Append(" and InvoiceId in (select invoiceid from invoicexodetail where productid in(select productid from product where productname between '" + product.ProductName + "' and '" + product2.ProductName + "'))  ");
            if (isclose)    //true 时只查询未结案
                str.Append(" and IsClose=0");
            if (mpsIsClose)  //true 只查询未排完单
                str.Append(" and InvoiceMPSState<>2");
            if (isForeigntrade)  //true 时只查询外销订单
                str.Append(" and IsForeigntrade=1");
            if (!string.IsNullOrEmpty(handBookId))
                str.Append(" and InvoiceId in (select invoiceid from invoicexodetail where HandbookId in (" + handBookId + ")) ");

            str.Append("   ORDER BY InvoiceId   ");
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate);
            ht.Add("endDate", endDate);
            ht.Add("yjrq1", yjrq1);
            ht.Add("yjrq2", yjrq2);
            ht.Add("sql", str.ToString());
            return sqlmapper.QueryForList<Book.Model.InvoiceXO>("InvoiceXO.select_byYJRQCustomEmp", ht);
        }

        public IList<Book.Model.InvoiceXO> SelectByCustomers(Model.Customer customer)
        {
            return sqlmapper.QueryForList<Model.InvoiceXO>("InvoiceXO.select_byCustomers", customer.CustomerId);
        }

        public IList<Model.InvoiceXO> SelectFlagNot2()
        {
            return sqlmapper.QueryForList<Model.InvoiceXO>("InvoiceXO.selectFlagNot2", null);
        }

        public IList<Model.InvoiceXO> SelectDateRangCusXOCustomer(DateTime startdate, DateTime enddate, string cusxoid, Model.Customer customer)
        {
            Hashtable ta = new Hashtable();
            ta.Add("startdate", startdate == null || startdate == new DateTime() ? global::Helper.DateTimeParse.NullDate : startdate);
            ta.Add("enddate", enddate == null || enddate == new DateTime() ? global::Helper.DateTimeParse.EndDate : enddate);
            ta.Add("cusxoid", string.IsNullOrEmpty(cusxoid) ? null : cusxoid);
            ta.Add("customerid", customer == null ? null : customer.CustomerId);
            return sqlmapper.QueryForList<Model.InvoiceXO>("InvoiceXO.selectDateRangCusXOCustomer", ta);
        }

        /// <summary>
        /// 0 全查  1只查詢
        /// </summary>
        /// <param name="mrsheader"></param>
        /// <param name="isclose"></param>
        /// <returns></returns>
        public Model.InvoiceXO SelectMpsIsClose(string mpsheader)
        {
            return sqlmapper.QueryForObject<Model.InvoiceXO>("InvoiceXO.get_mpsIscloseState", mpsheader);
        }

        public string SelectCusXOIdByPronoteHeaderId(string id)
        {
            return sqlmapper.QueryForObject<string>("InvoiceXO.SelectCusXOIdByPronoteHeaderId", id);
        }


        public bool IsHasMPSheader(string invoiceid)
        {
            return sqlmapper.QueryForObject<bool>("InvoiceXO.IsHasMPSheader", invoiceid);
        }

        #endregion
    }
}
