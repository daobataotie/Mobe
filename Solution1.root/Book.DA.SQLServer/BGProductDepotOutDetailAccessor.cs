//------------------------------------------------------------------------------
//
// file name：BGProductDepotOutDetailAccessor.cs
// author: mayanjun
// create date：2014/3/25 18:18:02
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
    /// Data accessor of BGProductDepotOutDetail
    /// </summary>
    public partial class BGProductDepotOutDetailAccessor : EntityAccessor, IBGProductDepotOutDetailAccessor
    {

        #region IBGProductDepotOutDetailAccessor 成员


        public IList<Book.Model.BGProductDepotOutDetail> SelectByBGProductDepotOutId(string bGProductDepotOutId)
        {
            return sqlmapper.QueryForList<Model.BGProductDepotOutDetail>("BGProductDepotOutDetail.SelectByBGProductDepotOutId", bGProductDepotOutId);
        }

        public double SumQuantityByHandbook(string bGHandbookId, string bGHandbookProductId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("bGHandbookId", bGHandbookId);
            ht.Add("bGHandbookProductId", bGHandbookProductId);
            return sqlmapper.QueryForObject<double>("BGProductDepotOutDetail.SumQuantityByHandbook", ht);
        }

        #endregion
    }
}
