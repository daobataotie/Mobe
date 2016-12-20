//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepotAccessor.cs
// author: peidun
// create date：2010-1-8 13:43:37
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
    /// Data accessor of ProduceOtherInDepot
    /// </summary>
    public partial class ProduceOtherInDepotAccessor : EntityAccessor, IProduceOtherInDepotAccessor
    {
        public IList<Book.Model.ProduceOtherInDepot> SelectByDateRange(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.ProduceOtherInDepot>("ProduceOtherInDepot.SelectByDateRange", ht);
        }

        public IList<Book.Model.ProduceOtherInDepot> SelectByCondition(DateTime startdate, DateTime enddate, Book.Model.Supplier supper1, Book.Model.Supplier supper2, string ProduceOtherCompactId1, string ProduceOtherCompactId2, Book.Model.Product startPro, Book.Model.Product endPro, string invouceCusidStart, string invouceCusidEnd)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            StringBuilder sql = new StringBuilder();
            if (supper1 != null && supper2 != null)
                sql.Append(" and   SupplierId IN (SELECT SupplierId FROM Supplier WHERE id between '" + supper1.Id + "' and '" + supper2.Id + "' ) ");
            if (!string.IsNullOrEmpty(ProduceOtherCompactId1) && !string.IsNullOrEmpty(ProduceOtherCompactId2))
                sql.Append(" and  ProduceOtherCompactId between '" + ProduceOtherCompactId2 + "' and '" + ProduceOtherCompactId2 + "' ");
            if (startPro != null && endPro != null)
                sql.Append(" and  ProduceOtherInDepotId in(select ProduceOtherInDepotId from ProduceOtherInDepotdetail where productId in(select productId from product where ProductName between '" + startPro.ProductName + "' and '" + endPro.ProductName + "')) ");
            if (!string.IsNullOrEmpty(invouceCusidStart) || !string.IsNullOrEmpty(invouceCusidEnd))
            {
                if (!string.IsNullOrEmpty(invouceCusidStart) && !string.IsNullOrEmpty(invouceCusidEnd))
                    sql.Append("AND ProduceOtherInDepotId IN (SELECT ProduceOtherInDepotId FROM ProduceOtherInDepotDetail WHERE ProduceOtherCompactDetailId IN (SELECT OtherCompactDetailId FROM ProduceOtherCompactDetail WHERE CustomInvoiceXOId BETWEEN '" + invouceCusidStart + "' and '" + invouceCusidEnd + "'))");
                else
                    sql.Append("AND ProduceOtherInDepotId IN (SELECT ProduceOtherInDepotId FROM ProduceOtherInDepotDetail WHERE ProduceOtherCompactDetailId IN (SELECT OtherCompactDetailId FROM ProduceOtherCompactDetail WHERE CustomInvoiceXOId = '" + (string.IsNullOrEmpty(invouceCusidStart) ? invouceCusidEnd : invouceCusidStart) + "'))");
            }
            ht.Add("sql", sql.ToString());
            return sqlmapper.QueryForList<Book.Model.ProduceOtherInDepot>("ProduceOtherInDepot.selectByDateWhere", ht);
        }
    }
}
