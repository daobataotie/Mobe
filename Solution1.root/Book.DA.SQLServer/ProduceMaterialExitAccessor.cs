//------------------------------------------------------------------------------
//
// file name：ProduceMaterialExitAccessor.cs
// author: peidun
// create date：2010-1-6 10:20:17
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
    /// Data accessor of ProduceMaterialExit
    /// </summary>
    public partial class ProduceMaterialExitAccessor : EntityAccessor, IProduceMaterialExitAccessor
    {
        public IList<Model.ProduceMaterialExit> SelectByCondition(DateTime start, DateTime end)
        {
            Hashtable ht = new Hashtable();
            ht.Add("start", start);
            ht.Add("end", end);
            return sqlmapper.QueryForList<Model.ProduceMaterialExit>("ProduceMaterialExit.selectByDateRange", ht);
        }

        public IList<Book.Model.ProduceMaterialExit> SelectByProduceHeaderId(string produceHeaderid)
        {
            return sqlmapper.QueryForList<Model.ProduceMaterialExit>("ProduceMaterialExit.SelectByProduceHeaderId", produceHeaderid);
        }

        public IList<Book.Model.ProduceMaterialExit> SelectForListForm(DateTime startDate, DateTime endDate, string startPMEId, string endPMEId, string startPronoteHeaderId, string endPronoteHeaderId, Book.Model.Product startProduct, Book.Model.Product endProduct)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" AND ProduceExitMaterialDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + "' AND '" + endDate.ToString("yyyy-MM-dd") + "'");

            if (!string.IsNullOrEmpty(startPMEId) && !string.IsNullOrEmpty(endPMEId))
            {
                sb.Append(" AND ProduceMaterialExitId BETWEEN '" + startPMEId + "' AND '" + endPMEId + "'");
            }

            if (!string.IsNullOrEmpty(startPronoteHeaderId) && !string.IsNullOrEmpty(endPronoteHeaderId))
            {
                sb.Append(" AND PronoteHeaderID BETWEEN '" + startPronoteHeaderId + "' AND '" + endPronoteHeaderId + "'");
            }

            if (startProduct != null & endProduct != null)
            {
                sb.Append(" AND PronoteHeaderID IN (SELECT PronoteHeaderID FROM PronoteHeader WHERE ProductId BETWEEN '" + startProduct.ProductId + "' AND '" + endProduct.ProductId + "')");
            }

            return sqlmapper.QueryForList<Model.ProduceMaterialExit>("ProduceMaterialExit.SelectForListForm", sb.ToString());
        }
    }
}
