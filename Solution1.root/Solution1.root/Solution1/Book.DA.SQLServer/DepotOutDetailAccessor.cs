//------------------------------------------------------------------------------
//
// file name：DepotOutDetailAccessor.cs
// author: mayanjun
// create date：2010-10-15 15:41:09
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
    /// Data accessor of DepotOutDetail
    /// </summary>
    public partial class DepotOutDetailAccessor : EntityAccessor, IDepotOutDetailAccessor
    {
        public IList<Model.DepotOutDetail> GetDepotOutDetailByDepotOutId(string depotOutId)
        {
            return sqlmapper.QueryForList<Model.DepotOutDetail>("DepotOutDetail.GetDepotOutDetailByDepotOutId", depotOutId);
        }
        public void Delete(Model.DepotOut depotOut)
        {
             sqlmapper.Delete("DepotOutDetail.deleteByHeader", depotOut.DepotOutId);
        }
    }
}
