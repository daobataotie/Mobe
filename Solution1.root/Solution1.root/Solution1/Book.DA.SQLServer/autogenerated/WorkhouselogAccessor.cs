﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：WorkhouselogAccessor.autogenerated.cs
// author: peidun
// create date：2009-12-9 10:27:05
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
    public partial class WorkhouselogAccessor
    {
		public Model.Workhouselog Get(string id)
		{
			return this.Get<Model.Workhouselog>(id);
		}
		
		public void Insert(Model.Workhouselog e)
		{
			this.Insert<Model.Workhouselog>(e);
		}
		
		public void Update(Model.Workhouselog e)
		{
			this.Update<Model.Workhouselog>(e);
		}
		
		public IList<Model.Workhouselog> Select()
		{
			return this.Select<Model.Workhouselog>();
		}
		
		public IList<Model.Workhouselog> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.Workhouselog>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.Workhouselog>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.Workhouselog>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.Workhouselog>();
		}
		public int Count()
		{
			return this.Count<Model.Workhouselog>();
		}
		public bool HasRowsBefore(Model.Workhouselog e)
		{
			return sqlmapper.QueryForObject<bool>("Workhouselog.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.Workhouselog e)
		{
			return sqlmapper.QueryForObject<bool>("Workhouselog.has_rows_after", e);
		}
		public Model.Workhouselog GetFirst()
		{
			return sqlmapper.QueryForObject<Model.Workhouselog>("Workhouselog.get_first", null);
		}
		public Model.Workhouselog GetLast()
		{
			return sqlmapper.QueryForObject<Model.Workhouselog>("Workhouselog.get_last", null);
		}
		public Model.Workhouselog GetNext(Model.Workhouselog e)
		{
			return sqlmapper.QueryForObject<Model.Workhouselog>("Workhouselog.get_next", e);
		}
		public Model.Workhouselog GetPrev(Model.Workhouselog e)
		{
			return sqlmapper.QueryForObject<Model.Workhouselog>("Workhouselog.get_prev", e);
		}
    }
}
