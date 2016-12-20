//------------------------------------------------------------------------------
//
// file name:XP2Accessor.cs
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
    /// Data accessor of XP2
    /// </summary>
    public partial class XP2Accessor : EntityAccessor, IXP2Accessor
    {
        #region IXP2Accessor Members

        public IList<Book.Model.XP2> Select(Book.Model.InvoiceFK invoice)
        {
            return sqlmapper.QueryForList<Model.XP2>("XP2.select_by_invoicefkid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceFK invoice)
        {
            sqlmapper.Delete("XP2.delete_by_invoicefkid", invoice.InvoiceId);
        }

        #endregion
    }
}
