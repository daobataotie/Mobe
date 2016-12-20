//------------------------------------------------------------------------------
//
// file name：AcCollectionDetailAccessor.cs
// author: mayanjun
// create date：2011-6-23 09:29:21
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
    /// Data accessor of AcCollectionDetail
    /// </summary>
    public partial class AcCollectionDetailAccessor : EntityAccessor, IAcCollectionDetailAccessor
    {
        public IList<Model.AcCollectionDetail> Select(Model.AcCollection acCollection)
        {
            return sqlmapper.QueryForList<Model.AcCollectionDetail>("AcCollectionDetail.selectByAcCollection", acCollection.AcCollectionId);
        }

        public void DeleteByAccid(string accid)
        {
            sqlmapper.Delete("AcCollectionDetail.DeleteByAccid", accid);
        }

    }
}
