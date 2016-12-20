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

        public void Updates(Model.InvoiceXO invoiceXO)
        {
            this.Update<Model.InvoiceXO>(invoiceXO);
        }
        public IList<Book.Model.InvoiceXO> SelectByYJRQCustomEmp(Model.Customer customer, string startDate, string endDate, Model.Employee employee)
        {
            Hashtable ta = new Hashtable();            
            ta.Add("custormerId", customer==null ?null:customer.CustomerId);
            ta.Add("startDate", string.IsNullOrEmpty(startDate) ? null : startDate);
            ta.Add("endDate", string.IsNullOrEmpty(endDate) ? null : endDate);
            ta.Add("roleIdEmp",employee==null?null:employee.EmployeeId);
            return sqlmapper.QueryForList<Book.Model.InvoiceXO>("InvoiceXO.select_byYJRQCustomEmp", ta);
        }
        public IList<Book.Model.InvoiceXO> SelectByCustomers(Model.Customer customer)
        {
            return sqlmapper.QueryForList<Model.InvoiceXO>("InvoiceXO.select_byCustomers", customer.CustomerId);
        }
        public IList<Model.InvoiceXO> SelectFlagNot2()
        {
            return sqlmapper.QueryForList<Model.InvoiceXO>("InvoiceXO.selectFlagNot2", null);
        }
      
        #endregion
    }
}
