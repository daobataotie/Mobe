//------------------------------------------------------------------------------
//
// file name：BGHandbookDepotInDetailAccessor.cs
// author: mayanjun
// create date：2013/12/19 18:37:44
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
    /// Data accessor of BGHandbookDepotInDetail
    /// </summary>
    public partial class BGHandbookDepotInDetailAccessor : EntityAccessor, IBGHandbookDepotInDetailAccessor
    {
        public IList<Model.BGHandbookDepotInDetail> SelectByBGHandbookDepotInId(string BGHandbookDepotInId)
        {
            return sqlmapper.QueryForList<Model.BGHandbookDepotInDetail>("BGHandbookDepotInDetail.SelectByBGHandbookDepotInId", BGHandbookDepotInId);
        }

        public void DeleteByBGHandbookDepotInId(string BGHandbookDepotInId)
        {
             sqlmapper.Delete("BGHandbookDepotInDetail.DeleteByBGHandbookDepotInId", BGHandbookDepotInId);
        }
    }
}
