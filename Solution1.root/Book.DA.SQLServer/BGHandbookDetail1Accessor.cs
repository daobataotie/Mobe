//------------------------------------------------------------------------------
//
// file name：BGHandbookDetail1Accessor.cs
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
    /// Data accessor of BGHandbookDetail1
    /// </summary>
    public partial class BGHandbookDetail1Accessor : EntityAccessor, IBGHandbookDetail1Accessor
    {

        public IList<Book.Model.BGHandbookDetail1> Select(string pac)
        {
            return sqlmapper.QueryForList<Model.BGHandbookDetail1>("BGHandbookDetail1.select_byheader", pac);
        }
        public string SelectProName(string BGHandBookId, string Id)
        {
            Hashtable ht = new Hashtable();
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(BGHandBookId) || string.IsNullOrEmpty(Id))
                return null;
            else
            {
                sql.Append("AND BGHandbookDetail1.BGHandbookId IN (SELECT BGHandbookId FROM BGHandbook WHERE Id='" + BGHandBookId + "' and IsEffect='1') AND Id='" + Id + "' and ProName is not null ORDER BY BGHandbookId desc");
            }
            ht.Add("sql", sql);
            return sqlmapper.QueryForObject<string>("BGHandbookDetail1.SelectProName", ht);
        }

        public Model.BGHandbookDetail1 SelectBGProduct(string BGHandBookId, string Id)
        {
            Hashtable ht = new Hashtable();
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(BGHandBookId) || string.IsNullOrEmpty(Id))
                return null;
            else
            {
                sql.Append("AND BGHandbookId IN (SELECT BGHandbookId FROM BGHandbook WHERE Id='" + BGHandBookId + "' and IsEffect='1') AND id='" + Id + "'  and ProName is not null ORDER BY BGHandbookId desc");
            }
            ht.Add("sql", sql);
            return sqlmapper.QueryForObject<Model.BGHandbookDetail1>("BGHandbookDetail1.SelectBGProduct", ht);
        }
        public System.Data.DataTable GetBGPrompt()
        {
            string sql = "SELECT bg.BGHandbookId,bg.Id,bgd.Id AS Id2,bgd.ProName,bgd.Quantity,isnull(bgd.YdycQuantity,0) YdycQuantity,bgd.YdwcQuantity,isnull(bgd.YdycQuantity,0)+isnull(bgd.YdwcQuantity,0)-bgd.Quantity AS Beyond  FROM BGHandbookDetail1 bgd LEFT JOIN BGHandbook bg ON bg.BGHandbookId = bgd.BGHandbookId WHERE isnull(bgd.YdycQuantity,0)+isnull(bgd.YdwcQuantity,0)>bgd.Quantity and bg.IsEffect='1'";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
    }
}
