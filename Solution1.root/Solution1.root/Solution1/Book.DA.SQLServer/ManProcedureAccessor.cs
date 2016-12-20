//------------------------------------------------------------------------------
//
// file name：ManProcedureAccessor.cs
// author: peidun
// create date：2009-12-9 9:32:07
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
    /// Data accessor of ManProcedure
    /// </summary>
    public partial class ManProcedureAccessor : EntityAccessor, IManProcedureAccessor
    {
        public void Delete(Model.BomParentPartInfo bom)
        {
            if(bom!=null)
            sqlmapper.Delete("ManProcedure.delete_bybom", bom.BomId);
        }
        public Model.ManProcedure Select(Model.BomParentPartInfo bom,Model.Customer customer)
        {
            if (bom == null) return null;

            Hashtable ht = new Hashtable();
            ht.Add("BomId", bom.BomId);
            ht.Add("CustomerId", customer==null?null:customer.CustomerId);
            Model.ManProcedure manpro;
            manpro = sqlmapper.QueryForObject<Model.ManProcedure>("ManProcedure.select_bybomidcustomer", ht);
            if (manpro==null)
                manpro = sqlmapper.QueryForObject<Model.ManProcedure>("ManProcedure.select_bybomid", bom.BomId);
            return manpro;
        }
        public Model.ManProcedure Select(Model.BomParentPartInfo bom, Model.Customer customer,Model.Product MadeProduct)
        {
            if (bom == null) return null;

            Hashtable ht = new Hashtable();
            ht.Add("BomId", bom.BomId);
            ht.Add("CustomerId", customer == null ? null : customer.CustomerId);
            ht.Add("MadeProductId", MadeProduct == null ? null : MadeProduct.ProductId);
            Model.ManProcedure manpro;
            manpro = sqlmapper.QueryForObject<Model.ManProcedure>("ManProcedure.select_byBomidMadePro", ht);
            if (manpro == null)
                manpro=this.Select(bom, customer);
                //manpro = sqlmapper.QueryForObject<Model.ManProcedure>("ManProcedure.select_bybomid", bom.BomId);
            return manpro;
        }
    }
}
