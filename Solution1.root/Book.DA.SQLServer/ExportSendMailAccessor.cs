//------------------------------------------------------------------------------
//
// file name：ExportSendMailAccessor.cs
// author: mayanjun
// create date：2012-6-21 10:58:40
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
    /// Data accessor of ExportSendMail
    /// </summary>
    public partial class ExportSendMailAccessor : EntityAccessor, IExportSendMailAccessor
    {
        public IList<Book.Model.ExportSendMail> SelectByDateRage(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate.ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("enddate", enddate.ToString("yyyy-MM-dd HH:mm:ss"));

            return sqlmapper.QueryForList<Model.ExportSendMail>("ExportSendMail.SelectByDateRage", ht);

        }
    }
}
