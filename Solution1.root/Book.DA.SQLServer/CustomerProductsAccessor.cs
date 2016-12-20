//------------------------------------------------------------------------------
//
// file name：CustomerProductsAccessor.cs
// author: peidun
// create date：2009-09-14  下午 05:25:49
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
    /// Data accessor of CustomerProducts
    /// </summary>
    public partial class CustomerProductsAccessor : EntityAccessor, ICustomerProductsAccessor
    {
        public IList<Book.Model.CustomerProducts> Select(Book.Model.Customer customer)
        {
            return sqlmapper.QueryForList<Model.CustomerProducts>("CustomerProducts.select_by_customerId", customer == null ? "" : customer.CustomerId);
        }

        public bool Exists(Book.Model.CustomerProducts customerProducts)
        {
            return sqlmapper.QueryForObject<bool>("CustomerProducts.exists_by_customerId_productId_customerproductId", customerProducts);
        }

        public bool ExistsExcept(Book.Model.CustomerProducts e)
        {
            Hashtable paras = new Hashtable();
            paras.Add("newId", e.CustomerProductId);
            paras.Add("oldId", Get(e.PrimaryKeyId).CustomerProductId);
            return sqlmapper.QueryForObject<bool>("CustomerProducts.existsexcept", paras);
        }

        public Book.Model.CustomerProducts GetById(string customerProductId)
        {
            return sqlmapper.QueryForObject<Model.CustomerProducts>("CustomerProducts.get_by_CustomerProducts", customerProductId);
        }

        //public IList<Book.Model.CustomerProducts> Select(string customerStart, string customerEnd, string productStart, string productEnd, DateTime dateStart, DateTime dateEnd)
        //{
        //    Hashtable pars = new Hashtable();
        //    pars.Add("customerStart", customerStart);
        //    pars.Add("customerEnd", customerEnd);
        //    pars.Add("productStart", productStart);
        //    pars.Add("productEnd", productEnd);
        //    pars.Add("dateStart", dateStart);
        //    pars.Add("dateEnd", dateEnd);
        //    return sqlmapper.QueryForList<Model.CustomerProducts>("CustomerProducts.select_byXSDate", pars);       

        //}

        public float GetStocksQuantityById(string primaryKeyId)
        {
            return sqlmapper.QueryForObject<float>("CustomerProducts.select_by_StocksQuantity", primaryKeyId);
        }

        public bool IsExistsCustomerProductId(string customerProductId, string primaryKeyId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("PrimaryKeyId", primaryKeyId);
            ht.Add("CustomerProductId", customerProductId);
            return sqlmapper.QueryForObject<bool>("CustomerProducts.IsExistsCustomerProductId", ht);
        }

        public bool SelectByCustomerIdAndProductId(string productId, string primaryKeyId, string customerId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productid", productId);
            ht.Add("PrimaryKeyId", primaryKeyId);
            ht.Add("CustomerId", customerId);
            return sqlmapper.QueryForObject<bool>("CustomerProducts.SelectByCustomerIdAndProductId", ht);
        }

        public Book.Model.CustomerProducts SelectByCustomerProductProceId(string productid)
        {
            return sqlmapper.QueryForObject<Model.CustomerProducts>("CustomerProducts.SelectByCustomerProductProceId", productid);
        }

        public string SelectPrimaryIdByProceName(string customerProductProceName)
        {
            return sqlmapper.QueryForObject<string>("CustomerProducts.SelectPrimaryIdByProceName", customerProductProceName);
        }
    }
}
