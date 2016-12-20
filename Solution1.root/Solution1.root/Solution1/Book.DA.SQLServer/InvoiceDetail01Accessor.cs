using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Book.DA.SQLServer
{
    public class InvoiceDetail01Accessor : EntityAccessor, IInvoiceDetail01Accessor
    {
        #region IInvoiceDetail01Accessor 成员


        public IList<Model.InvoiceDetail01> Select(DateTime start, DateTime end)
        {
            Hashtable table = new Hashtable();
            table.Add("start", start);
            table.Add("end", end);
            return sqlmapper.QueryForList<Model.InvoiceDetail01>("InvoiceDetail01.selectDataTableQ32", table);
        }

        #endregion

        #region IInvoiceDetail01Accessor 成员


        public IList<Book.Model.InvoiceDetail01> Select(DateTime start, DateTime end, string startId, string endId, Helper.CompanyKind companyKind)
        {
            Hashtable table = new Hashtable();
            table.Add("start", start);
            table.Add("end", end);
            table.Add("startId", startId);

            table.Add("endId", endId);
            switch (companyKind)
            {
                case global::Helper.CompanyKind.Customer:
                    return sqlmapper.QueryForList<Model.InvoiceDetail01>("InvoiceDetail01.select_byDateRengeAndCustomer", table);
                case global::Helper.CompanyKind.Supplier:
                    return sqlmapper.QueryForList<Model.InvoiceDetail01>("InvoiceDetail01.select_byDateRengeAndSupplier", table);
                default:
                    return null;
            }

            
        }

        #endregion

        #region IInvoiceDetail01Accessor 成员


        public IList<Book.Model.InvoiceDetail01> Select1(DateTime start, DateTime end, string startId, string endId, Helper.CompanyKind companyKind)
        {
            Hashtable table = new Hashtable();
            table.Add("start", start);
            table.Add("end", end);
            table.Add("startId", startId);
            table.Add("endId", endId);
            switch (companyKind)
            {
                case global::Helper.CompanyKind.Customer:
                    return sqlmapper.QueryForList<Model.InvoiceDetail01>("InvoiceDetail01.select_byDateRengeAndCustomer1", table);
                case global::Helper.CompanyKind.Supplier:
                    return sqlmapper.QueryForList<Model.InvoiceDetail01>("InvoiceDetail01.select_byDateRengeAndSupplier1", table);
                default:
                    return null;
            }
        }

        #endregion
    }
}
