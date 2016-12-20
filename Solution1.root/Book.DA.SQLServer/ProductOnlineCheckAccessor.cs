//------------------------------------------------------------------------------
//
// file name：ProductOnlineCheckAccessor.cs
// author: mayanjun
// create date：2013-3-25 17:50:57
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
    /// Data accessor of ProductOnlineCheck
    /// </summary>
    public partial class ProductOnlineCheckAccessor : EntityAccessor, IProductOnlineCheckAccessor
    {
        public IList<Model.ProductOnlineCheck> SelectByDate(DateTime startDate, DateTime endDate,string InvoiceCusId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", startDate);
            ht.Add("EndDate", endDate);
            string sql = "";
            if (!string.IsNullOrEmpty(InvoiceCusId))
                sql = "AND  (InvoiceXOId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + InvoiceCusId + "') or PronoteHeaderId in (select PronoteHeaderID from PronoteHeader where  InvoiceXOId in (select InvoiceId from InvoiceXO where CustomerInvoiceXOId='" + InvoiceCusId + "')))";
            ht.Add("sql",sql);
            return sqlmapper.QueryForList<Model.ProductOnlineCheck>("ProductOnlineCheck.SelectByDate", ht);
        }
    }
}
