//------------------------------------------------------------------------------
//
// file name：ProcessingAccessor.cs
// author: peidun
// create date：2009-09-25 下午 05:16:42
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
    /// Data accessor of Processing
    /// </summary>
    public partial class ProcessingAccessor : EntityAccessor, IProcessingAccessor
    {
        #region IProcessingAccessor Members


        //public IList<Book.Model.Processing> Select(Book.Model.Customer customer)
        //{
        //    return sqlmapper.QueryForList<Model.Processing>("Processing.select_by_customer", customer.CustomerId);
        //}

        public IList<Book.Model.Processing> Select(Book.Model.ProcessCategory processCategory)
        {
            //Hashtable pars = new Hashtable();

            //if (processCategory == null)
            //{
            //    pars.Add("processCategoryId", "");
            //}
            //else 
            //{
            //    pars.Add("processCategoryId", processCategory.ProcessCategoryId);
            //}

            //if (customer == null)
            //{
            //    pars.Add("customerId", "");
            //}
            //else
            //{
            //    pars.Add("customerId", customer.CustomerId);
            //}

            return sqlmapper.QueryForList<Model.Processing>("Processing.select_category", processCategory.ProcessCategoryId);
        }
        //public IList<Book.Model.Processing> Selectbycategorycustomer(string processCategory, Book.Model.Customer customer)
        //{
        //    Hashtable pars = new Hashtable();

        //    if (processCategory == null)
        //    {
        //        pars.Add("processCategoryId", "");
        //    }
        //    else
        //    {
        //        pars.Add("processCategoryId", processCategory);
        //    }

        //    if (customer == null)
        //    {
        //        pars.Add("customerId", "");
        //    }
        //    else
        //    {
        //        pars.Add("customerId", customer.CustomerId);
        //    }

        //    return sqlmapper.QueryForList<Model.Processing>("Processing.select_processing_category_customer", pars);
        //}
        //public DataTable SelectProceCateByCustom(Model.Customer customer)
        //{
        //    SqlConnection conn = new SqlConnection(sqlmapper.DataSource.ConnectionString);
        //    SqlCommand command = new SqlCommand("select * from ProcessCategory where ProcessCategoryId  in (select ProcessCategoryId from Processing where CustomerId=@customer)", conn);
        //    command.Parameters.Add(new SqlParameter("@customer",SqlDbType.VarChar,50));            
        //    command.Parameters[0].Value = customer.CustomerId;

        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = command;
        //    DataTable dt = new DataTable();
        //    da.Fill(dt);
        //    return dt;
        //}
        public bool ExistsConcent(string Content, string id)
        {
            Hashtable ht = new Hashtable();
            ht.Add("Content", Content);
            ht.Add("id", id);
            return sqlmapper.QueryForObject<bool>("Processing.ExistsName", ht);
        }

        #endregion
    }
}
