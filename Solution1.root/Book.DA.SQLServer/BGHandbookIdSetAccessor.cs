//------------------------------------------------------------------------------
//
// file name：BGHandbookIdSetAccessor.cs
// author: mayanjun
// create date：2013-07-05 11:57:54
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
    /// Data accessor of BGHandbookIdSet
    /// </summary>
    public partial class BGHandbookIdSetAccessor : EntityAccessor, IBGHandbookIdSetAccessor
    {
        /// <summary>
        /// 查询“启用”的编号
        /// </summary>
        /// <returns></returns>
        public IList<Model.BGHandbookIdSet> SelectHasUsing()
        {
            return sqlmapper.QueryForList<Model.BGHandbookIdSet>("BGHandbookIdSet.SelectHasUsing", null);
        }

        public IList<string> SelectBGHandbookId()
        {
            return sqlmapper.QueryForList<string>("BGHandbookIdSet.SelectBGHandbookId", null);
        }
    }
}
