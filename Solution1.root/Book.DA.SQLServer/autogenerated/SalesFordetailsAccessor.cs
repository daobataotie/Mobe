﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：SalesFordetailsAccessor.autogenerated.cs
// author: peidun
// create date：2009-12-18 11:23:45
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
    public partial class SalesFordetailsAccessor
    {
		public Model.SalesFordetails Get(string id)
		{
			return this.Get<Model.SalesFordetails>(id);
		}
		
		public void Insert(Model.SalesFordetails e)
		{
			this.Insert<Model.SalesFordetails>(e);
		}
		
		public void Update(Model.SalesFordetails e)
		{
			this.Update<Model.SalesFordetails>(e);
		}
		
		public IList<Model.SalesFordetails> Select()
		{
			return this.Select<Model.SalesFordetails>();
		}
		
		public IList<Model.SalesFordetails> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.SalesFordetails>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.SalesFordetails>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.SalesFordetails>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.SalesFordetails>();
		}
		public int Count()
		{
			return this.Count<Model.SalesFordetails>();
		}
		public bool HasRowsBefore(Model.SalesFordetails e)
		{
			return sqlmapper.QueryForObject<bool>("SalesFordetails.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.SalesFordetails e)
		{
			return sqlmapper.QueryForObject<bool>("SalesFordetails.has_rows_after", e);
		}
		public Model.SalesFordetails GetFirst()
		{
			return sqlmapper.QueryForObject<Model.SalesFordetails>("SalesFordetails.get_first", null);
		}
		public Model.SalesFordetails GetLast()
		{
			return sqlmapper.QueryForObject<Model.SalesFordetails>("SalesFordetails.get_last", null);
		}
		public Model.SalesFordetails GetNext(Model.SalesFordetails e)
		{
			return sqlmapper.QueryForObject<Model.SalesFordetails>("SalesFordetails.get_next", e);
		}
		public Model.SalesFordetails GetPrev(Model.SalesFordetails e)
		{
			return sqlmapper.QueryForObject<Model.SalesFordetails>("SalesFordetails.get_prev", e);
		}
		

    }
}
