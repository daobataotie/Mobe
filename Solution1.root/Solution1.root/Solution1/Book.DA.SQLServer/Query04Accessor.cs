using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA.SQLServer
{
    public class Query04Accessor : Accessor, IQuery04Accessor
    {

        #region IQuery04Accessor ³ÉÔ±

        //public System.Data.DataTable SelectDataTable(Book.Model.Company company, DateTime? datetime)
        //{
        //    //System.Collections.Hashtable table = new System.Collections.Hashtable();
        //    //table.Add("companyid", company == null ? null : company.CompanyId);
        //    //table.Add("datetime", datetime);
        //    //return sqlmapper.QueryForList("MiscData.select_query04", table);            
        //    string sql = "select a.InvoiceId, a.CompanyId, a.Employee0Id, a.Employee1Id, a.Employee2Id, "
        //         + "a.Employee3Id, a.InvoiceLRTime, a.InvoiceGZTime, a.InvoiceZFCause, "
        //         + "a.InvoiceZFTime, a.InvoiceDate, a.InvoiceAbstract, a.InvoiceNote, a.InvoiceStatus, "
        //         + "a.Kind, a.InvoiceHeJi, a.InvoiceYHE, a.InvoiceZSE, a.InvoiceZRE, a.InvoiceTax, "
        //         + "a.InvoiceZongJi, a.InvoicePayTimeLimit, a.InvoiceOwed, a.received, "
        //         + "b.CompanyId AS Expr1, b.InsertTime, b.UpdateTime, b.CompanyName1, "
        //         + "b.CompanyDescription, b.CompanyName0, b.CompanyR0, b.CompanyR1, "
        //         + "b.CompanyP0, b.CompanyP1, b.CompanyPayDate, b.CompanyAddress, "
        //         + "b.CompanyPhone, b.CompanyFax, b.CompanyEMail, b.CompanyContact,"
        //         + "b.CompanyKind, b.CompanyManager, b.CompanyNumber, b.CompanyPhone1, "
        //         + "  b.CompanyMobile, b.CompanyExchangeDay, b.CompanyJinChuAddress, "
        //         + "    b.CompanyWebSiteAddress from "
        //         + "(select invoiceid,CompanyId,employee0id,employee1id,"
        //         + "employee2id,employee3id,InvoiceLRTime,InvoiceGZTime,InvoiceZFCause,InvoiceZFTime,"
        //         + "InvoiceDate,InvoiceAbstract,InvoiceNote,InvoiceStatus,'ct' as Kind,InvoiceHeJi,"
        //         + "InvoiceYHE,InvoiceZSE,InvoiceZRE,InvoiceTax,InvoiceZongJi,InvoicePayTimeLimit,"
        //         + "InvoiceOwed,(InvoiceZongJi-InvoiceOwed) as received from invoicect union all "
        //         + "select invoiceid,CompanyId,employee0id,employee1id,employee2id,employee3id,"
        //         + "InvoiceLRTime,InvoiceGZTime,InvoiceZFCause,InvoiceZFTime, InvoiceDate,"
        //         + "InvoiceAbstract,InvoiceNote,InvoiceStatus,'xs' as Kind,InvoiceHeJi,InvoiceYHE,"
        //         + "InvoiceZSE,InvoiceZRE,InvoiceTax,InvoiceZongJi,InvoicePayTimeLimit,InvoiceOwed,"
        //         + "(InvoiceZongJi-InvoiceOwed) as received from invoicexs) a inner join company b on a.companyid = b.companyid where InvoiceZongJi > 0 and invoiceowed>0 and InvoiceStatus = 1 ";              

        //    if (company != null)
        //    {
        //        if (!string.IsNullOrEmpty(company.CompanyId))
        //        {
        //            sql += "and (a.CompanyId= '" + company.CompanyId + "' )";
        //        }
        //    }
        //    if (datetime != null)
        //    {
        //        sql += "and (a.InvoicePayTimeLimit between '" + datetime.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and getdate())";
        //    }
        //    System.Data.SqlClient.SqlDataAdapter sda = new System.Data.SqlClient.SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
        //    System.Data.DataTable table = new System.Data.DataTable();
        //    sda.Fill(table);
        //    return table;
        //}

        #endregion
    }
}
