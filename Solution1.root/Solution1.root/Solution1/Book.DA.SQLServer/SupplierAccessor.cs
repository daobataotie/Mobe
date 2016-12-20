//------------------------------------------------------------------------------
//
// file name：SupplierAccessor.cs
// author: peidun
// create date：2009-08-03 9:37:28
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
    /// Data accessor of Supplier
    /// </summary>
    public partial class SupplierAccessor : EntityAccessor, ISupplierAccessor
    {
        #region ISupplierAccessor Members


        //public void Update(Book.Model.Supplier supplier, string newId)
        //{
        //    Hashtable pars = new Hashtable();
        //    pars.Add("supplier", supplier);
        //    pars.Add("newId", newId);
        //    sqlmapper.Update("Supplier.newupdate", pars);
        //    supplier.SupplierId = newId;
        //}
       //public  IList<Model.Supplier> Select(string SupplierStart, string SupplierEnd, DateTime dateStart, DateTime dateEnd)
       // {
       //     Hashtable pars = new Hashtable();
       //     pars.Add("SupplierStart", SupplierStart);
       //     pars.Add("SupplierEnd", SupplierEnd);
       //     pars.Add("dateStart", dateStart);
       //     pars.Add("dateEnd", dateEnd);
       //     return sqlmapper.QueryForList<Model.Supplier>("Supplier.select_byQujianDate", pars);
        
       // }
        public IList<Book.Model.Supplier> Select(Model.SupplierCategory supplierCategory)
        {
            return sqlmapper.QueryForList<Model.Supplier>("Supplier.select_bySupplierCategoryId", supplierCategory.SupplierCategoryId);
        }
        #endregion
    }
}
