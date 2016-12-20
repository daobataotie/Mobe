//------------------------------------------------------------------------------
//
// file name：PCPGOnlineCheckDetailAccessor.cs
// author: mayanjun
// create date：2011-12-6 14:34:43
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
    /// Data accessor of PCPGOnlineCheckDetail
    /// </summary>
    public partial class PCPGOnlineCheckDetailAccessor : EntityAccessor, IPCPGOnlineCheckDetailAccessor
    {
        public IList<Book.Model.PCPGOnlineCheckDetail> Select(string pcpgocId)
        {
            return sqlmapper.QueryForList<Model.PCPGOnlineCheckDetail>("PCPGOnlineCheckDetail.select_byPCPGOnlineCheckId", pcpgocId);
        }

        public void DeleteByPCPGOnlineCheckId(string IPCPGOnlineCheckId)
        {
            sqlmapper.Delete("PCPGOnlineCheckDetail.DeleteByPCPGOnlineCheckId", IPCPGOnlineCheckId);
        }

        public IList<Book.Model.PCPGOnlineCheckDetail> SelectByFromInvoiceId(string id)
        {
            return sqlmapper.QueryForList<Model.PCPGOnlineCheckDetail>("PCPGOnlineCheckDetail.SelectByFromInvoiceId", id);
        }

        public string GetTimerListString(string PCPGOnlineCheckId)
        {
            IList<Model.PCPGOnlineCheckDetail> al = sqlmapper.QueryForList<Model.PCPGOnlineCheckDetail>("PCPGOnlineCheckDetail.GetTimerListString", PCPGOnlineCheckId);
            string resultstr = string.Empty;
            foreach (Model.PCPGOnlineCheckDetail item in al)
            {
                resultstr += item.PCPGOnlineCheckDetailDate.Value.ToString() + ",";
            }
            return resultstr;
        }
    }
}
