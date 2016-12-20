//------------------------------------------------------------------------------
//
// file name：MaterialAccessor.cs
// author: mayanjun
// create date：2013-5-4 16:09:33
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
    /// Data accessor of Material
    /// </summary>
    public partial class MaterialAccessor : EntityAccessor, IMaterialAccessor
    {
        public double CountJWeightByMaterial(string MaterialId)
        {
            string sql = string.Empty;
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(MaterialId))
                sql = "and MaterialId IN " + MaterialId + "";
            ht.Add("sql", sql.ToString());
            return sqlmapper.QueryForObject<double>("Material.CountJWeightByMaterial", ht);
        }

        public IList<Model.Material> SelectOther()
        {
            return sqlmapper.QueryForList<Model.Material>("Material.SelectOther", null);
        }

        public IList<string> SelectIdByMaterialId(string MaterialId)
        {
            string sql = string.Empty;
            Hashtable ht = new Hashtable();
            if (!string.IsNullOrEmpty(MaterialId))
                sql = "and MaterialId IN " + MaterialId + "";
            ht.Add("sql", sql.ToString());
            return sqlmapper.QueryForList<string>("Material.SelectIdByMaterialId", ht);
        }
        public Model.Material SelectMaterialByPrimary(string id)
        {
            return sqlmapper.QueryForObject<Model.Material>("Material.SelectMaterialByPrimary", id);
        }
        public IList<string> SelectMaterialCategory()
        {
            return sqlmapper.QueryForList<string>("Material.MaterialCategoryName", null);
        }

        public IList<Model.Material> SelectAll()
        {
            return sqlmapper.QueryForList<Model.Material>("Material.SelectAll", null);
        }
        public string SelectIdByPrimary(string Id) 
        {
            return sqlmapper.QueryForObject<string>("Material.SelectIdByPrimary", Id);
        }
    }
}
