//------------------------------------------------------------------------------
//
// file name：BGHandbookRangeDetailAccessor.cs
// author: mayanjun
// create date：2013-4-17 15:13:04
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
    /// Data accessor of BGHandbookRangeDetail
    /// </summary>
    public partial class BGHandbookRangeDetailAccessor : EntityAccessor, IBGHandbookRangeDetailAccessor
    {
        public IList<Model.BGHandbookRangeDetail> SelectByBGHandbookId(string Id, string type)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Id", Id);
            ht.Add("Type", type);
            return sqlmapper.QueryForList<Model.BGHandbookRangeDetail>("BGHandbookRangeDetail.SelectByBGHandbookId", ht);
        }

        public void DeleteByBGHandbookId(string Id)
        {
            sqlmapper.Delete("BGHandbookRangeDetail.DeleteByBGHandbookId", Id);
        }
    }
}
