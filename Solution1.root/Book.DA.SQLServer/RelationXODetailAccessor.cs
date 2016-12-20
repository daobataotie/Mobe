//------------------------------------------------------------------------------
//
// file name：RelationXODetailAccessor.cs
// author: mayanjun
// create date：2015/4/19 下午 08:06:08
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
    /// Data accessor of RelationXODetail
    /// </summary>
    public partial class RelationXODetailAccessor : EntityAccessor, IRelationXODetailAccessor
    {
        public IList<Model.RelationXODetail> SelectByHeaderId(string id)
        {
            return sqlmapper.QueryForList<Model.RelationXODetail>("RelationXODetail.SelectByHeaderId", id);
        }

        public void DeleteByHeaderId(string id)
        {
            sqlmapper.Delete("RelationXODetail.DeleteByHeaderId", id);
        }
    }
}
