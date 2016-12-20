//------------------------------------------------------------------------------
//
// file name：InvoicePackingDetailAccessor.cs
// author: mayanjun
// create date：2013-1-14 10:58:48
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
    /// Data accessor of InvoicePackingDetail
    /// </summary>
    public partial class InvoicePackingDetailAccessor : EntityAccessor, IInvoicePackingDetailAccessor
    {
        public IList<Model.InvoicePackingDetail> SelectByInvoicePackingId(string InvoicePackingId)
        {
            return sqlmapper.QueryForList<Model.InvoicePackingDetail>("InvoicePackingDetail.SelectByInvoicePackingId", InvoicePackingId);
        }

        public void DeleteByInvoicePackingId(string InvoicePackingId)
        {
            sqlmapper.Delete("InvoicePackingDetail.DeleteByInvoicePackingId", InvoicePackingId);
        }
    }
}
