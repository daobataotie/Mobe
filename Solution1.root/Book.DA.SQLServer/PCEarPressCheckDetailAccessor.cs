//------------------------------------------------------------------------------
//
// file name：PCEarPressCheckDetailAccessor.cs
// author: mayanjun
// create date：2013-08-23 16:50:38
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
    /// Data accessor of PCEarPressCheckDetail
    /// </summary>
    public partial class PCEarPressCheckDetailAccessor : EntityAccessor, IPCEarPressCheckDetailAccessor
    {


        #region IPCEarPressCheckDetailAccessor 成员


        public IList<Book.Model.PCEarPressCheckDetail> SelectByPCEarPressCheckId(string pCEarPressCheckId)
        {
            return sqlmapper.QueryForList<Model.PCEarPressCheckDetail>("PCEarPressCheckDetail.SelectByPCEarPressCheckDetailId", pCEarPressCheckId);
        }
        #endregion

        #region IPCEarPressCheckDetailAccessor 成员


        public void DeleteByPCEarPressCheckId(string pCEarPressCheckId)
        {
            sqlmapper.QueryForList<Model.PCEarPressCheckDetail>("PCEarPressCheckDetail.DeleteByPCEarPressCheckDetailId", pCEarPressCheckId);
        }

        #endregion
    }
}
