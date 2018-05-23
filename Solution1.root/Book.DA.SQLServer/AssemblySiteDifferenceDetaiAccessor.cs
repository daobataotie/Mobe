//------------------------------------------------------------------------------
//
// file name：AssemblySiteDifferenceDetaiAccessor.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
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
    /// Data accessor of AssemblySiteDifferenceDetai
    /// </summary>
    public partial class AssemblySiteDifferenceDetaiAccessor : EntityAccessor, IAssemblySiteDifferenceDetaiAccessor
    {
        public void DeleteByHeaderId(string id)
        {
            sqlmapper.Delete("AssemblySiteDifferenceDetai.DeleteByHeaderId", id);
        }

        public IList<Model.AssemblySiteDifferenceDetai> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.AssemblySiteDifferenceDetai>("AssemblySiteDifferenceDetai.SelectByHeaderId", id);
        }

        public IList<Model.AssemblySiteDifferenceDetai> SelectByDateRage(DateTime startDate, DateTime endDate, string productid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startDate", startDate.ToString("yyyy-MM-dd"));
            ht.Add("endDate", endDate.Date.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss"));
            ht.Add("ProductId", productid);

            return sqlmapper.QueryForList<Model.AssemblySiteDifferenceDetai>("AssemblySiteDifferenceDetai.SelectByDateRage", ht);
        }
    }
}
