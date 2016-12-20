//------------------------------------------------------------------------------
//
// file name：PCClarityCheckDetailAccessor.cs
// author: mayanjun
// create date：2013-08-19 15:44:12
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
    /// Data accessor of PCClarityCheckDetail
    /// </summary>
    public partial class PCClarityCheckDetailAccessor : EntityAccessor, IPCClarityCheckDetailAccessor
    {
        public IList<Model.PCClarityCheckDetail> SelectByPCClarityCheckId(string PCClarityCheckId)
        {
            return sqlmapper.QueryForList<Model.PCClarityCheckDetail>("PCClarityCheckDetail.SelectByPCClarityCheckId", PCClarityCheckId);
        }
        public void DeleteByPCClarityCheckID(string PCClarityCheckId)
        {
            sqlmapper.Delete("PCClarityCheckDetail.DeleteByPCClarityCheckID", PCClarityCheckId);
        }
    }
}
