//------------------------------------------------------------------------------
//
// file name：BGHandbookAccessor.cs
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
    /// Data accessor of BGHandbook
    /// </summary>
    public partial class BGHandbookAccessor : EntityAccessor, IBGHandbookAccessor
    {

        public IList<Book.Model.BGHandbook> Select(string id)
        {
            return sqlmapper.QueryForList<Model.BGHandbook>("BGHandbook.select_byId", id);
        }

        public DataTable SelectIdGroupById()
        {
            string sql = "SELECT Id,IsEffect FROM BGHandbook  group by id,IsEffect";
            SqlDataAdapter sda = new SqlDataAdapter(sql, sqlmapper.DataSource.ConnectionString);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }

        public void UpdateIsEffect(string id, string effect)
        {
            //同一个手册号可以有不同版本，但只能有一个生效
            //Hashtable ht = new Hashtable();
            //ht.Add("id",id);
            //ht.Add("IsEffect", effect);
            //sqlmapper.Update("BGHandbook.UpdateIsEffect", ht);
        }

        public bool HasEffect(string bGHandBookId, string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Id", id);
            ht.Add("BGHandbookId", bGHandBookId);
            return sqlmapper.QueryForObject<bool>("BGHandbook.HasEffect", ht);
        }

        public IList<string> SelectAllId()
        {
            IList<string> list = sqlmapper.QueryForList<string>("BGHandbook.SelectAllId", null);
            return (list == null ? new List<string>() : list);
        }
    }
}
