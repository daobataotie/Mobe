//------------------------------------------------------------------------------
//
// file name:XP1Accessor.cs
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
    /// Data accessor of XP1
    /// </summary>
    public partial class XP1Accessor : EntityAccessor, IXP1Accessor
    {
        #region IXP1Accessor Members

        public IList<Book.Model.XP1> Select(Book.Model.InvoiceFK invoice)
        {
            return sqlmapper.QueryForList<Model.XP1>("XP1.select_by_invoicefkid", invoice.InvoiceId);
        }

        public void Delete(Book.Model.InvoiceFK invoice)
        {
            sqlmapper.Delete("XP1.delete_by_invoicefkid", invoice.InvoiceId);
        }

        #endregion
    }
}
