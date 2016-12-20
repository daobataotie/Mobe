//------------------------------------------------------------------------------
//
// file name：PCDoubleImpactCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-24 17:38:50
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
    /// Data accessor of PCDoubleImpactCheckDetail
    /// </summary>
    public partial class PCDoubleImpactCheckDetailAccessor : EntityAccessor, IPCDoubleImpactCheckDetailAccessor
    {
        public void DeleteByPCDoubleImpactCheckId(string PCDoubleImpactCheckId)
        {
            sqlmapper.Delete("PCDoubleImpactCheckDetail.DeleteByPCDoubleImpactCheckId", PCDoubleImpactCheckId);
        }

        public IList<Book.Model.PCDoubleImpactCheckDetail> Select(string PCDoubleImpactCheckId)
        {
            return sqlmapper.QueryForList<Model.PCDoubleImpactCheckDetail>("PCDoubleImpactCheckDetail.SelectByPCDoubleImpactCheckId", PCDoubleImpactCheckId);
        }
    }
}
