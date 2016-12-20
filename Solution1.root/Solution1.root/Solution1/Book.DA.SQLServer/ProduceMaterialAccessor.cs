//------------------------------------------------------------------------------
//
// file name：ProduceMaterialAccessor.cs
// author: peidun
// create date：2009-12-30 16:33:31
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
    /// Data accessor of ProduceMaterial
    /// </summary>
    public partial class ProduceMaterialAccessor : EntityAccessor, IProduceMaterialAccessor
    {
        public IList<Model.ProduceMaterial> SelectbypronoteHeaderId(string pronoteHeaderId)
        {
            return sqlmapper.QueryForList<Model.ProduceMaterial>("ProduceMaterial.selectbypronoteHeaderId", pronoteHeaderId);
        }
        public IList<Model.ProduceMaterial> SelectState()
        {
            return sqlmapper.QueryForList<Model.ProduceMaterial>("ProduceMaterial.select_byState", null);
        }
        public void UpdateProduceMaterial(DataTable dt)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            dataAdapter.UpdateCommand = new SqlCommand("UPDATE ProduceMaterial SET Employee0Id=@Employee0Id,UpdateTime=GETDATE() WHERE ProduceMaterialID=@ProduceMaterialID", conn);
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@Employee0Id", SqlDbType.VarChar, 50, "Employee0Id"));
            dataAdapter.UpdateCommand.Parameters.Add(new SqlParameter("@ProduceMaterialID", SqlDbType.VarChar, 50, "ProduceMaterialID"));
            dataAdapter.Update(dt);
        }

        public DataTable GetbypronoteHeaderId(string pronoteHeaderId)
        {
            SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ProduceMaterial WHERE PronoteHeaderID='" + pronoteHeaderId + "'", conn);
            DataSet data = new DataSet();
            adapter.Fill(data);
            return data.Tables[0];
        }

        public IList<Model.ProduceMaterial> SelectByDateRage(DateTime startdate, DateTime enddate)
        {
            Hashtable ht = new Hashtable();
            ht.Add("startdate", startdate);
            ht.Add("enddate", enddate);
            return sqlmapper.QueryForList<Model.ProduceMaterial>("ProduceMaterial.selectByDateRage", ht);
        }
        public bool ExistsId(string id)
        {
            return sqlmapper.QueryForObject<bool>("ProduceMaterial.existsId", id);
        }
        public IList<Model.ProduceMaterial> SelectBycondition(DateTime starDate, DateTime endDate, string produceMaterialId0, string produceMaterialId1, string pId0, string pId1, string departmentId0, string departmentId1, string PronoteHeaderId0, string PronoteHeaderId1)
        {
            Hashtable ht = new Hashtable();
            ht.Add("starDate", starDate);
            ht.Add("endDate", endDate);
            ht.Add("produceMaterialId0", produceMaterialId0);
            ht.Add("produceMaterialId1", produceMaterialId1);
            ht.Add("pId0", pId0);
            ht.Add("pId1", pId1);
            ht.Add("dId0", departmentId0);
            ht.Add("dId1", departmentId1);
            ht.Add("pronoteId0", PronoteHeaderId0);
            ht.Add("pronoteId1", PronoteHeaderId1);
            return sqlmapper.QueryForList<Model.ProduceMaterial>("ProduceMaterial.selectBycondition", ht);
        }
    }
}
