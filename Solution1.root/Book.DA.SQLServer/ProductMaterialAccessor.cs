//------------------------------------------------------------------------------
//
// file name：ProductMaterialAccessor.cs
// author: mayanjun
// create date：2010-9-23 15:27:44
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
    /// Data accessor of ProductMaterial
    /// </summary>
    public partial class ProductMaterialAccessor : EntityAccessor, IProductMaterialAccessor
    {
        public bool IsExistProductMaterialName(Model.ProductMaterial productMateridal)
        {
            Hashtable ht=new Hashtable();
            ht.Add("pid", productMateridal.ProductMaterialId == null ? "" : productMateridal.ProductMaterialId);
            ht.Add("name",productMateridal.ProductMaterialName);
            return sqlmapper.QueryForObject<bool>("ProductMaterial.IsExistProductMaterialName", ht);
        }

        public bool IsExistId(Model.ProductMaterial productMateridal)
        {
            Hashtable ht = new Hashtable();
            ht.Add("pid", productMateridal.ProductMaterialId == null ? "" : productMateridal.ProductMaterialId);
            ht.Add("id",productMateridal.Id);
            return sqlmapper.QueryForObject<bool>("ProductMaterial.IsExistId", ht);
        }
    }
}
