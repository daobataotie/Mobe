//------------------------------------------------------------------------------
//
// file name：PCEarProtectCheckDetailAccessor.cs
// author: mayanjun
// create date：2013-09-03 15:16:59
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
    /// Data accessor of PCEarProtectCheckDetail
    /// </summary>
    public partial class PCEarProtectCheckDetailAccessor : EntityAccessor, IPCEarProtectCheckDetailAccessor
    {
        public IList<Model.PCEarProtectCheckDetail> SelectByPCEarProtectCheckId(string PCEarProtectCheckId)
        {
            return sqlmapper.QueryForList<Model.PCEarProtectCheckDetail>("PCEarProtectCheckDetail.SelectByPCEarProtectCheckId", PCEarProtectCheckId);
        }
        public void DeleteByPCEarProtectCheckId(string PCEarProtectCheckId)
        {
            sqlmapper.Delete("PCEarProtectCheckDetail.DeleteByPCEarProtectCheckId", PCEarProtectCheckId);
        }
    }
}
