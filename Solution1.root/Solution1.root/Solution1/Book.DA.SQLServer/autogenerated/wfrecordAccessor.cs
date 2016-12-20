﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：wfrecordAccessor.autogenerated.cs
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
    public partial class wfrecordAccessor
    {
		public Model.wfrecord Get(string id)
		{
			return this.Get<Model.wfrecord>(id);
		}
		
		public void Insert(Model.wfrecord e)
		{
			this.Insert<Model.wfrecord>(e);
		}
		
		public void Update(Model.wfrecord e)
		{
			this.Update<Model.wfrecord>(e);
		}
		
		public IList<Model.wfrecord> Select()
		{
			return this.Select<Model.wfrecord>();
		}
		
		public IList<Model.wfrecord> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.wfrecord>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.wfrecord>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.wfrecord>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.wfrecord>();
		}
		public int Count()
		{
			return this.Count<Model.wfrecord>();
		}
		public bool HasRowsBefore(Model.wfrecord e)
		{
			return sqlmapper.QueryForObject<bool>("wfrecord.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.wfrecord e)
		{
			return sqlmapper.QueryForObject<bool>("wfrecord.has_rows_after", e);
		}
		public Model.wfrecord GetFirst()
		{
			return sqlmapper.QueryForObject<Model.wfrecord>("wfrecord.get_first", null);
		}
		public Model.wfrecord GetLast()
		{
			return sqlmapper.QueryForObject<Model.wfrecord>("wfrecord.get_last", null);
		}
		public Model.wfrecord GetNext(Model.wfrecord e)
		{
			return sqlmapper.QueryForObject<Model.wfrecord>("wfrecord.get_next", e);
		}
		public Model.wfrecord GetPrev(Model.wfrecord e)
		{
			return sqlmapper.QueryForObject<Model.wfrecord>("wfrecord.get_prev", e);
		}
		

    }
}
