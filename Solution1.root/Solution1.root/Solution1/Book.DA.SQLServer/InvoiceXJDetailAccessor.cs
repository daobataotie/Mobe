//------------------------------------------------------------------------------
//
// file name:InvoiceXJDetailAccessor.cs
// author: peidun
// create date:2008/6/20 15:52:13
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
    /// Data accessor of InvoiceXJDetail
    /// </summary>
    public partial class InvoiceXJDetailAccessor : EntityAccessor, IInvoiceXJDetailAccessor
    {
        #region IInvoiceXJDetailAccessor 成员


        public IList<Book.Model.InvoiceXJDetail> Select(Book.Model.InvoiceXJ invoice)
        {
            return sqlmapper.QueryForList<Model.InvoiceXJDetail>("InvoiceXJDetail.select_by_invoiceid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceXJ invoice)
        {
            sqlmapper.Delete("InvoiceXJDetail.delete_by_invoiceid", invoice.InvoiceId);
        }
        /// <summary>
        /// 类型为公司产品
        /// </summary>
        /// <returns></returns>
        public IList<Book.Model.InvoiceXJDetail> SelectProductType()
        {
           return  sqlmapper.QueryForList<Model.InvoiceXJDetail>("InvoiceXJDetail.select_by_productType", null);
        
        }


        #endregion
    }
}
