//------------------------------------------------------------------------------
//
// file name：DepotPositionAccessor.cs
// author: peidun
// create date：2009-07-24 11:18:43
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
    /// Data accessor of DepotPosition
    /// </summary>
    public partial class DepotPositionAccessor : EntityAccessor, IDepotPositionAccessor
    {
        #region IDepotPositionAccessor Members


        public IList<Book.Model.DepotPosition> Select(Book.Model.Depot depot)
        {
            return sqlmapper.QueryForList<Model.DepotPosition>("DepotPosition.select_by_depot", depot == null ? "" : depot.DepotId);
        }
        public IList<Book.Model.DepotPosition> Select(string depotId)
        {
            return sqlmapper.QueryForList<Model.DepotPosition>("DepotPosition.select_by_depot", depotId);
        }

        public IList<Book.Model.DepotPosition> SelectByDepot(Book.Model.Depot depot)
        {
            return sqlmapper.QueryForList<Model.DepotPosition>("DepotPosition.select_by_depot", depot == null ? "" : depot.DepotId);
        }

        public bool existsInsertName(string DepotPositionid, Model.Depot depot)
        {
            Hashtable ht = new Hashtable();
            ht.Add("DepotPositionid", DepotPositionid);
            ht.Add("depotid", depot == null ? null : depot.DepotId);
            return sqlmapper.QueryForObject<bool>("DepotPosition.existsInsertName", ht);
        }

        public IList<Model.DepotPosition> GetDepotPositionsByDepotAndProduct(string ProductId, string DepotId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", ProductId);
            ht.Add("depotId", DepotId);
            return sqlmapper.QueryForList<Model.DepotPosition>("DepotPosition.GetDepotNot0PositionsByDepotAndProduct", ht);
        }
        public IList<Model.DepotPosition> GetStockByDepotAndProduct(string ProductId, string DepotId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productId", ProductId);
            ht.Add("depotId", DepotId);
            return sqlmapper.QueryForList<Model.DepotPosition>("DepotPosition.GetStockByDepotAndProduct", ht);
        }

        #endregion
    }
}
