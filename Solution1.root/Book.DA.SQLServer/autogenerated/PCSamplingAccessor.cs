﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCSamplingAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/10/30 17:07:37
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
    public partial class PCSamplingAccessor
    {
		public Model.PCSampling Get(string id)
		{
			return this.Get<Model.PCSampling>(id);
		}
		
		public void Insert(Model.PCSampling e)
		{
			this.Insert<Model.PCSampling>(e);
		}
		
		public void Update(Model.PCSampling e)
		{
			this.Update<Model.PCSampling>(e);
		}
		
		public IList<Model.PCSampling> Select()
		{
			return this.Select<Model.PCSampling>();
		}
		
		public IList<Model.PCSampling> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.PCSampling>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.PCSampling>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.PCSampling>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.PCSampling>();
		}
		public int Count()
		{
			return this.Count<Model.PCSampling>();
		}
		public bool HasRowsBefore(Model.PCSampling e)
		{
			return sqlmapper.QueryForObject<bool>("PCSampling.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.PCSampling e)
		{
			return sqlmapper.QueryForObject<bool>("PCSampling.has_rows_after", e);
		}
		public Model.PCSampling GetFirst()
		{
			return sqlmapper.QueryForObject<Model.PCSampling>("PCSampling.get_first", null);
		}
		public Model.PCSampling GetLast()
		{
			return sqlmapper.QueryForObject<Model.PCSampling>("PCSampling.get_last", null);
		}
		public Model.PCSampling GetNext(Model.PCSampling e)
		{
			return sqlmapper.QueryForObject<Model.PCSampling>("PCSampling.get_next", e);
		}
		public Model.PCSampling GetPrev(Model.PCSampling e)
		{
			return sqlmapper.QueryForObject<Model.PCSampling>("PCSampling.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("PCSampling.existsPrimary", id);
		}
    }
}
