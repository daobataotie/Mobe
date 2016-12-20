//------------------------------------------------------------------------------
//
// file name:XR2Accessor.cs
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
    /// Data accessor of XR2
    /// </summary>
    public partial class XR2Accessor : EntityAccessor, IXR2Accessor
    {
        #region IXR2Accessor Members

        public IList<Book.Model.XR2> Select(Book.Model.InvoiceSK invoice)
        {
            return sqlmapper.QueryForList<Model.XR2>("XR2.select_by_invoiceskid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceSK invoice)
        {
            sqlmapper.Delete("XR2.delete_by_invoiceskid", invoice.InvoiceId);
        }

        #endregion
    }
}
