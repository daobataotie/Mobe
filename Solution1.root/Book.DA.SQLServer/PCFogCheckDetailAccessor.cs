//------------------------------------------------------------------------------
//
// file name：PCFogCheckDetailAccessor.cs
// author: mayanjun
// create date：2012-3-17 09:38:33
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
    /// Data accessor of PCFogCheckDetail
    /// </summary>
    public partial class PCFogCheckDetailAccessor : EntityAccessor, IPCFogCheckDetailAccessor
    {
        public IList<Book.Model.PCFogCheckDetail> Select(string pcfcid)
        {
            return sqlmapper.QueryForList<Model.PCFogCheckDetail>("PCFogCheckDetail.SelectbyHeaderId", pcfcid);
        }

        public void DeleteByHeaderId(string headerid)
        {
            sqlmapper.Delete("PCFogCheckDetail.DeleteByHeaderId", headerid);
        }
    }
}
