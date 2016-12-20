//------------------------------------------------------------------------------
//
// file name：PronotedetailsMaterialAccessor.cs
// author: mayanjun
// create date：2010-9-15 10:11:09
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
    /// Data accessor of PronotedetailsMaterial
    /// </summary>
    public partial class PronotedetailsMaterialAccessor : EntityAccessor, IPronotedetailsMaterialAccessor
    {
        public IList<Model.PronotedetailsMaterial> GetByHeader(Model.PronoteHeader header)
        {
            return sqlmapper.QueryForList<Model.PronotedetailsMaterial>("PronotedetailsMaterial.getByHeader", header.PronoteHeaderID);
        }

        public Model.PronotedetailsMaterial GetByHeadIdAndDetailId(string pronteheadid, string pronotedetailid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("headid", pronteheadid);
            ht.Add("detailid", pronotedetailid);
            return sqlmapper.QueryForObject<Model.PronotedetailsMaterial>("PronotedetailsMaterial.GetByHeadIdAndDetailId", ht);
        }

        public IList<Book.Model.PronotedetailsMaterial> selectByHeaderIdAndPid(string PronoteHeaderID, string StartpId, string EndpId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PronoteHeaderID", PronoteHeaderID);
            ht.Add("StartpId", StartpId);
            ht.Add("EndpId", EndpId);
            return sqlmapper.QueryForList<Model.PronotedetailsMaterial>("PronotedetailsMaterial.selectByHeaderIdAndPid", ht);
        }

        public void DeleteByHeaderId(string id)
        {
            sqlmapper.Delete("PronotedetailsMaterial.DeleteByHeaderId", id);
        }
    }
}
