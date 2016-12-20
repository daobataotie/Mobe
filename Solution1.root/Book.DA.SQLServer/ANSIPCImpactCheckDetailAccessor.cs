//------------------------------------------------------------------------------
//
// file name：ANSIPCImpactCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-11-23 09:50:26
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
    /// Data accessor of ANSIPCImpactCheckDetail
    /// </summary>
    public partial class ANSIPCImpactCheckDetailAccessor : EntityAccessor, IANSIPCImpactCheckDetailAccessor
    {
        public IList<Book.Model.ANSIPCImpactCheckDetail> Select(string aNSIPCImpactCheckID)
        {
            return sqlmapper.QueryForList<Model.ANSIPCImpactCheckDetail>("ANSIPCImpactCheckDetail.SelectByaNSIPCImpactCheckID", aNSIPCImpactCheckID);
        }

        public void DeleteByANSIPCICId(string ANSIPCICId)
        {
            sqlmapper.Delete("ANSIPCImpactCheckDetail.DeleteByANSIPCICId", ANSIPCICId);
        }
    }
}
