//------------------------------------------------------------------------------
//
// file name：InvoiceXJPackageDetailsAccessor.cs
// author: mayanjun
// create date：2012-8-14 17:05:01
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
    /// Data accessor of InvoiceXJPackageDetails
    /// </summary>
    public partial class InvoiceXJPackageDetailsAccessor : EntityAccessor, IInvoiceXJPackageDetailsAccessor
    {
        public IList<Book.Model.InvoiceXJPackageDetails> SelectByHeaderId(string invoiceid)
        {
            return sqlmapper.QueryForList<Model.InvoiceXJPackageDetails>("InvoiceXJPackageDetails.SelectByHeaderId", invoiceid);
        }

        public void DeleteByHeader(string invoiceid)
        {
            sqlmapper.Delete("InvoiceXJPackageDetails.DeleteByHeader", invoiceid);
        }
    }
}
