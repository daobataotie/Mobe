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
        #region IInvoiceXSDetailAccessor 成员


        public IList<Book.Model.InvoiceXSDetail> Select(Book.Model.InvoiceXS invoiceXS)
        {
            return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.select_by_invoiceid", invoiceXS.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceXS invoice)
        {
            sqlmapper.Delete("InvoiceXSDetail.delete_by_invoiceid", invoice.InvoiceId);
        }

        //public IList<Book.Model.InvoiceXSDetail> Select(DateTime start, DateTime end, Book.Model.Company company)
        //{
        //    Hashtable pars = new Hashtable();
        //    pars.Add("start", start);
        //    pars.Add("end", end);
        //    pars.Add("companyId", company == null ? (string)null : company.CompanyId);
        //    return sqlmapper.QueryForList<Model.InvoiceXSDetail>("InvoiceXSDetail.select_byDateRengeAndCompany", pars);
        //}

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
        //查询
         //public IList<Book.Model.InvoiceXSDetail> Select(string customerProductsId, Model.InvoiceXO invoiceXO)
         //{
         //    Hashtable pars = new Hashtable();
         //    pars.Add("PrimaryKeyId", customerProductsId);
         //    pars.Add("InvoiceId", InvoiceId);
         //    return sqlmapper.QueryForList<Book.Model.InvoiceXS>("InvoiceXS.select_bycustomerProductId", customerProductsId,invoiceXO.InvoiceId);
         //}
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
             pars.Add("employeeId",employee==null?null: employee.EmployeeId);
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

        #endregion
    }
}
