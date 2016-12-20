//------------------------------------------------------------------------------
//
// file name:XR1Accessor.cs
// author: peidun
// create date:2008/6/6 10:00:51
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
    /// Data accessor of XR1
    /// </summary>
    public partial class XR1Accessor : EntityAccessor, IXR1Accessor
    {
        #region IXR1Accessor Members

        public IList<Book.Model.XR1> Select(Book.Model.InvoiceSK invoice)
        {
            return sqlmapper.QueryForList<Model.XR1>("XR1.select_by_invoiceskid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceSK invoice)
        {
            sqlmapper.Delete("XR1.delete_by_invoiceskid", invoice.InvoiceId);
        }

        #endregion
    }
}
