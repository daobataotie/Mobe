using System;
using System.Collections.Generic;
using System.Text;

namespace Book.DA.SQLServer
{
    public class Invoice01Accessor : Accessor, IInvoice01Accessor
    {
        #region IInvoice01Accessor 成员

        public IList<Book.Model.Invoice01> Select(string kind, string companyId)
        {
            switch (kind.ToUpper())
            {
                case "FK":
                    return sqlmapper.QueryForList<Model.Invoice01>("Invoice01.select_all_FK", companyId);
                case "SK":
                    return sqlmapper.QueryForList<Model.Invoice01>("Invoice01.select_all_SK", companyId);
                default:
                    return null;
            }
        }

        #endregion

        #region IInvoice01Accessor 成员


        public void Update(Book.Model.Invoice01 detail, Book.Model.InvoiceFK invoiceFK)
        {
            string invoiceKind = detail.Kind.ToUpper();

            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            hash.Add("InvoiceId", detail.InvoiceId);
            hash.Add("invoiceFKId", invoiceFK.InvoiceId);
            hash.Add("XPmoney", detail.PayReceived);
            switch (invoiceKind)
            {
                case "CG":
                    sqlmapper.Update("XP1.update_invoiceid_invoicefk", hash);
                    break;
                case "XT":
                    sqlmapper.Update("XP2.update_invoiceid_invoicefk", hash);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region IInvoice01Accessor 成员


        public IList<Book.Model.Invoice01> Select(string kind, string companyId, string invoiceFkId)
        {
            System.Collections.Hashtable parps = new System.Collections.Hashtable();
            parps.Add("companyId", companyId);
            parps.Add("invoiceFkId", invoiceFkId);
            switch (kind.ToUpper())
            {   
                case "FK_UPDATE":
                    return sqlmapper.QueryForList<Model.Invoice01>("Invoice01.select_all_FK_update", parps);
                case "SK_UPDATE":
                    return sqlmapper.QueryForList<Model.Invoice01>("Invoice01.select_all_SK_update", parps);
                case "FK_VIEW":
                    return sqlmapper.QueryForList<Model.Invoice01>("Invoice01.select_all_FK_View", parps);
                    
                case "SK_VIEW":
                    return sqlmapper.QueryForList<Model.Invoice01>("Invoice01.select_all_SK_View", parps);
            }
            return null;
        }

        #endregion

        #region IInvoice01Accessor 成员


        public void Update(Book.Model.Invoice01 detail, Book.Model.InvoiceSK invoiceSK)
        {
            string invoiceKind = detail.Kind.ToUpper();

            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            hash.Add("InvoiceId", detail.InvoiceId);
            hash.Add("invoiceSKId", invoiceSK.InvoiceId);
            hash.Add("XRmoney", detail.PayReceived);
            switch (invoiceKind)
            {
                case "CT":
                    sqlmapper.Update("XR1.update_invoiceid_invoicesk", hash);
                    break;
                case "XS":
                    sqlmapper.Update("XR2.update_invoiceid_invoicesk", hash);
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
