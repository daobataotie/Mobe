//------------------------------------------------------------------------------
//
// file name：StockCheckAccessor.cs
// author: mayanjun
// create date：2010-7-30  11:43:34
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
    /// Data accessor of StockCheck
    /// </summary>
    public partial class StockCheckAccessor : EntityAccessor, IStockCheckAccessor
    {
        public void Delete(Book.Model.StockCheck invoice)
        {
            sqlmapper.Delete("StockCheck.Delete_by_StockCheckId", invoice.StockCheckId);
        }

        public Model.StockCheck SelectByStockCheckId(string stockid)
        {
            return sqlmapper.QueryForObject<Model.StockCheck>("StockCheck.Select_by_StockCheckId", stockid);
        }

        public IList<Book.Model.StockCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("StartDate", StartDate.ToString("yyyy-MM-dd"));
            ht.Add("EndDate", EndDate);
            return sqlmapper.QueryForList<Model.StockCheck>("StockCheck.SelectByDateRage", ht);
        }

    }
}
