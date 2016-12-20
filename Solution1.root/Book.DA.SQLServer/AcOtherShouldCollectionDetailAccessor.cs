//------------------------------------------------------------------------------
//
// file name：AcOtherShouldCollectionDetailAccessor.cs
// author: mayanjun
// create date：2011-6-10 11:19:27
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
    /// Data accessor of AcOtherShouldCollectionDetail
    /// </summary>
    public partial class AcOtherShouldCollectionDetailAccessor : EntityAccessor, IAcOtherShouldCollectionDetailAccessor
    {
        public IList<Model.AcOtherShouldCollectionDetail> Select(Model.AcOtherShouldCollection acOtherShouldCollection)
        {
            return sqlmapper.QueryForList<Model.AcOtherShouldCollectionDetail>("AcOtherShouldCollectionDetail.getByAcOtherShouldCollectionId", acOtherShouldCollection.AcOtherShouldCollectionId);
        }
    }
}
