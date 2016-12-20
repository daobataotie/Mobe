//------------------------------------------------------------------------------
//
// file name：BomParentPartInfoAccessor.cs
// author: peidun
// create date：2009-08-25 17:08:56
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
    /// Data accessor of BomParentPartInfo
    /// </summary>
    public partial class BomParentPartInfoAccessor : EntityAccessor, IBomParentPartInfoAccessor
    {
        #region IBomParentPartInfoAccessor Members


        public Book.Model.BomParentPartInfo Get(Book.Model.Product product)
        {
            if (product == null) return null;
            return sqlmapper.QueryForObject<Model.BomParentPartInfo>("BomParentPartInfo.select_by_productId", product.ProductId);
        }
        public IList<Model.BomParentPartInfo> SelectNotContent()
        {

            return sqlmapper.QueryForList<Model.BomParentPartInfo>("BomParentPartInfo.selectnotcontent", null);
        }
        

        public Book.Model.BomParentPartInfo Get(Book.Model.Product product, Model.Customer customer)
        {
            Hashtable ht = new Hashtable();
            ht.Add("productid", product==null?null:product.ProductId);
            ht.Add("customerid",customer==null?null:customer.CustomerId);
            return sqlmapper.QueryForObject<Model.BomParentPartInfo>("BomParentPartInfo.select_by_productIdAndCustomer", ht);        
        }


        //public void Delete(Book.Model.Product product,Model.Customer customer)
        //{
        //    Hashtable ht = new Hashtable();
        //    ht.Add("productid", product.ProductId);
        //    ht.Add("customerid", customer == null ? null : customer.CustomerId);
        //    sqlmapper.Delete("BomParentPartInfo.deleteByProductCustom", ht);
        //}
        public void Delete(Book.Model.Product product)
        {
            sqlmapper.Delete("BomParentPartInfo.deleteByProduct", product.ProductId);
    
       }
           
        //public void DeleteByInProductCustomer(Book.Model.Product product,Model.Customer customer)
        //{
        //    Hashtable ht = new Hashtable();
        //    ht.Add("productid", product.ProductId);
        //    ht.Add("customerid", customer == null ? null : customer.CustomerId);
        //    sqlmapper.Delete("BomParentPartInfo.deleteByBOMInProductCustom", ht);
        //}
        public IList<Model.BomParentPartInfo> SelectNotContentByCustomer(Model.Customer customer)
        {
            return sqlmapper.QueryForList<Model.BomParentPartInfo>("BomParentPartInfo.selectnotcontentByCustomer", customer.CustomerId);
        }
        public bool Exists_Field(string sqlWhere)
        {
            return sqlmapper.QueryForObject<bool>("BomParentPartInfo.exists_field", sqlWhere);
        }
        public IList<Model.BomParentPartInfo> SelectByIdOrNameKey(string bomid ,string proid, string productName, string customerProductName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("bomid", bomid);
            ht.Add("id", proid);
            ht.Add("name", productName);
            ht.Add("customerProductName", customerProductName);
            return sqlmapper.QueryForList<Model.BomParentPartInfo>("BomParentPartInfo.select_byIdOrNameKey", ht);
        }

        //上一下一
        public Model.BomParentPartInfo GetPrev1(Model.BomParentPartInfo e)
        {
            return sqlmapper.QueryForObject<Model.BomParentPartInfo>("BomParentPartInfo.get_prev1", e);
        }

        public bool HasRowsBefore1(Model.BomParentPartInfo e)
        {
            return sqlmapper.QueryForObject<bool>("BomParentPartInfo.has_rows_before1", e);
        }
        public bool HasRowsAfter1(Model.BomParentPartInfo e)
        {
            return sqlmapper.QueryForObject<bool>("BomParentPartInfo.has_rows_after1", e);
        }
        public Model.BomParentPartInfo GetFirst1()
        {
            return sqlmapper.QueryForObject<Model.BomParentPartInfo>("BomParentPartInfo.get_first1", null);
        }
        public Model.BomParentPartInfo GetLast1()
        {
            return sqlmapper.QueryForObject<Model.BomParentPartInfo>("BomParentPartInfo.get_last1", null);
        }
        public Model.BomParentPartInfo GetNext1(Model.BomParentPartInfo e)
        {
            return sqlmapper.QueryForObject<Model.BomParentPartInfo>("BomParentPartInfo.get_next1", e);
        }
        public bool HasRows1()
        {
            return sqlmapper.QueryForObject<bool>("BomParentPartInfo.has_rows1", null);
        }
        #endregion
    }
}
