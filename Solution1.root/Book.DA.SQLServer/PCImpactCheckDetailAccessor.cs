//------------------------------------------------------------------------------
//
// file name：PCImpactCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-15 14:09:35
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
    /// Data accessor of PCImpactCheckDetail
    /// </summary>
    public partial class PCImpactCheckDetailAccessor : EntityAccessor, IPCImpactCheckDetailAccessor
    {
        public IList<Book.Model.PCImpactCheckDetail> Select(string PCImpactCheckId)
        {
            return sqlmapper.QueryForList<Model.PCImpactCheckDetail>("PCImpactCheckDetail.select_byPCImpactCheckID", PCImpactCheckId);
        }

        public void DeleteByPCImpactCheckId(string PCImpactCheckId)
        {
            sqlmapper.Delete("PCImpactCheckDetail.DeleteByPCImpactCheckId", PCImpactCheckId);
        }

    }
}
