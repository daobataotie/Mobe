//------------------------------------------------------------------------------
//
// file name：InvoiceZGDetailAccessor.cs
// author: mayanjun
// create date：2012-11-19 14:13:51
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
    /// Data accessor of InvoiceZGDetail
    /// </summary>
    public partial class InvoiceZGDetailAccessor : EntityAccessor, IInvoiceZGDetailAccessor
    {
        public void DeleteByInvoiceZGId(string InvoiceZGId)
        {
            sqlmapper.Delete("InvoiceZGDetail.DeleteByInvoiceZGId", InvoiceZGId);
        }

        public IList<Model.InvoiceZGDetail> SelectByInvoiceZGId(string InvoiceZGId)
        {
            return sqlmapper.QueryForList<Model.InvoiceZGDetail>("InvoiceZGDetail.SelectByInvoiceZGId", InvoiceZGId);
        }
    }
}
