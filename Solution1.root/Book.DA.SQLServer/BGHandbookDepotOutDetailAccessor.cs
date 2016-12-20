//------------------------------------------------------------------------------
//
// file name：BGHandbookDepotOutDetailAccessor.cs
// author: mayanjun
// create date：2014/3/5 16:32:46
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
    /// Data accessor of BGHandbookDepotOutDetail
    /// </summary>
    public partial class BGHandbookDepotOutDetailAccessor : EntityAccessor, IBGHandbookDepotOutDetailAccessor
    {
        public IList<Model.BGHandbookDepotOutDetail> SelectByBGHandbookDepotOutId(string bGHandbookDepotOutId)
        {
            return sqlmapper.QueryForList<Model.BGHandbookDepotOutDetail>("BGHandbookDepotOutDetail.SelectByBGHandbookDepotOutId", bGHandbookDepotOutId);
        }

        public void DeleteByBGHandbookDepotOutId(string bGHandbookDepotOutId)
        {
            sqlmapper.Delete("BGHandbookDepotOutDetail.DeleteByBGHandbookDepotOutId", bGHandbookDepotOutId);
        }

    }
}
