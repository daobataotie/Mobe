﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ManProcedureAccessor.autogenerated.cs
// author: peidun
// create date：2009-12-11 19:41:05
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
    public partial class ManProcedureAccessor
    {
		public Model.ManProcedure Get(string id)
		{
			return this.Get<Model.ManProcedure>(id);
		}
		
		public void Insert(Model.ManProcedure e)
		{
			this.Insert<Model.ManProcedure>(e);
		}
		
		public void Update(Model.ManProcedure e)
		{
			this.Update<Model.ManProcedure>(e);
		}
		
		public IList<Model.ManProcedure> Select()
		{
			return this.Select<Model.ManProcedure>();
		}
		
		public IList<Model.ManProcedure> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.ManProcedure>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.ManProcedure>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.ManProcedure>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.ManProcedure>();
		}
		public int Count()
		{
			return this.Count<Model.ManProcedure>();
		}
		public bool HasRowsBefore(Model.ManProcedure e)
		{
			return sqlmapper.QueryForObject<bool>("ManProcedure.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.ManProcedure e)
		{
			return sqlmapper.QueryForObject<bool>("ManProcedure.has_rows_after", e);
		}
		public Model.ManProcedure GetFirst()
		{
			return sqlmapper.QueryForObject<Model.ManProcedure>("ManProcedure.get_first", null);
		}
		public Model.ManProcedure GetLast()
		{
			return sqlmapper.QueryForObject<Model.ManProcedure>("ManProcedure.get_last", null);
		}
		public Model.ManProcedure GetNext(Model.ManProcedure e)
		{
			return sqlmapper.QueryForObject<Model.ManProcedure>("ManProcedure.get_next", e);
		}
		public Model.ManProcedure GetPrev(Model.ManProcedure e)
		{
			return sqlmapper.QueryForObject<Model.ManProcedure>("ManProcedure.get_prev", e);
		}
		

    }
}
