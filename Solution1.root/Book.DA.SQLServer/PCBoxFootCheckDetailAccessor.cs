//------------------------------------------------------------------------------
//
// file name：PCBoxFootCheckDetailAccessor.cs
// author: mayanjun
// create date：2013-08-16 10:26:11
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
    /// Data accessor of PCBoxFootCheckDetail
    /// </summary>
    public partial class PCBoxFootCheckDetailAccessor : EntityAccessor, IPCBoxFootCheckDetailAccessor
    {
        public IList<Book.Model.PCBoxFootCheckDetail> SelectByPCBoxFootCheckId(string id)
        {
            return sqlmapper.QueryForList<Book.Model.PCBoxFootCheckDetail>("PCBoxFootCheckDetail.SelectByPCBoxFootCheckId", id);
        }

        public void DeleteByPCBoxFootCheckId(string id)
        {
            sqlmapper.Delete("PCBoxFootCheckDetail.DeleteByPCBoxFootCheckId", id);
        }
    }
}
