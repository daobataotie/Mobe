//------------------------------------------------------------------------------
//
// file name：BGHandbookDetail2Accessor.cs
// author: mayanjun
// create date：2013-4-16 11:58:59
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
    /// Data accessor of BGHandbookDetail2
    /// </summary>
    public partial class BGHandbookDetail2Accessor : EntityAccessor, IBGHandbookDetail2Accessor
    {
        public IList<Book.Model.BGHandbookDetail2> Select(string pac)
        {
            return sqlmapper.QueryForList<Model.BGHandbookDetail2>("BGHandbookDetail2.select_byheader", pac);
        }

        public void UpdateCeIn(string bgid, string lid, double quantity)
        {
            Hashtable ht = new Hashtable();
            ht.Add("BGHandbookId", bgid);
            ht.Add("id", lid);
            ht.Add("ZhuanCeInQuantity", quantity);
            sqlmapper.Update("BGHandbookDetail2.update_CeIn", ht);
        }
        public IList<Book.Model.BGHandbookDetail2> SelectbyShouceandId(string bgid, string lid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("bgid", bgid);
            ht.Add("id", lid);
            return sqlmapper.QueryForList<Model.BGHandbookDetail2>("BGHandbookDetail2.select_byShouceandId", ht);
        }

        public Model.BGHandbookDetail2 SelectBGProduct(string BGHandBookId, string Id)
        {
            Hashtable ht = new Hashtable();
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(BGHandBookId) || string.IsNullOrEmpty(Id))
                return null;
            else
            {
                sql.Append("AND BGHandbookId IN (SELECT BGHandbookId FROM BGHandbook WHERE Id='" + BGHandBookId + "' and IsEffect='1') AND id='" + Id + "' ORDER BY BGHandbookId desc");
            }
            ht.Add("sql", sql);
            return sqlmapper.QueryForObject<Model.BGHandbookDetail2>("BGHandbookDetail2.SelectBGProduct", ht);
        }

        public IList<Model.BGHandbookDetail2> SelectByShouce(string Id)
        {
            return sqlmapper.QueryForList<Model.BGHandbookDetail2>("BGHandbookDetail2.SelectByShouce", Id);
        }

        public Model.BGHandbookDetail2 SelectByShouceAndId(string Shouce, int id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Shouce", Shouce);
            ht.Add("id", id);
            return sqlmapper.QueryForObject<Model.BGHandbookDetail2>("BGHandbookDetail2.SelectByShouceAndId", ht);
        }
    }
}
