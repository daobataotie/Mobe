//------------------------------------------------------------------------------
//
// file name：CustomerMarksAccessor.cs
// author: mayanjun
// create date：2013-5-8 13:43:43
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
    /// Data accessor of CustomerMarks
    /// </summary>
    public partial class CustomerMarksAccessor : EntityAccessor, ICustomerMarksAccessor
    {
        public IList<Model.CustomerMarks> SelectByCustomerId(string customerId)
        {
            return sqlmapper.QueryForList<Model.CustomerMarks>("CustomerMarks.SelectByCustomerId", customerId);
        }

        public void DeleteByCustomerId(string customerId)
        {
            sqlmapper.Delete("CustomerMarks.DeleteByCustomerId", customerId);
        }

        public IList<Model.CustomerMarks> SelectByInvoicePackingId(string InvoicePackingId)
        {
            return sqlmapper.QueryForList<Model.CustomerMarks>("CustomerMarks.SelectByInvoicePackingId", InvoicePackingId);
        }

        public void DeleteByInvoicePackingId(string InvoicePackingId)
        {
            sqlmapper.Delete("CustomerMarks.DeleteByInvoicePackingId", InvoicePackingId);
        }
    }
}
