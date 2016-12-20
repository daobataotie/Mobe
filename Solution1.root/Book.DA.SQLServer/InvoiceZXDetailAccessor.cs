//------------------------------------------------------------------------------
//
// file name：InvoiceZXDetailAccessor.cs
// author: mayanjun
// create date：2012-10-29 14:32:20
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
    /// Data accessor of InvoiceZXDetail
    /// </summary>
    public partial class InvoiceZXDetailAccessor : EntityAccessor, IInvoiceZXDetailAccessor
    {
        /// <summary>
        /// 通过装箱编号查询装箱详细
        /// </summary>
        /// <returns></returns>
        public IList<Model.InvoiceZXDetail> SelectByInvoiceZXId(string id)
        {
            return sqlmapper.QueryForList<Model.InvoiceZXDetail>("InvoiceZXDetail.select_by_InvoiceZXId", id);
        }

        public void DeleteByInvoiceZXId(string id)
        {
             sqlmapper.Delete("InvoiceZXDetail.delete_by_InvoiceZXId", id);
        }
    }
}
