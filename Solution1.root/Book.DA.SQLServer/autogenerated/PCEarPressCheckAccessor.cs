﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCEarPressCheckAccessor.autogenerated.cs
// author: mayanjun
// create date：2013-08-23 16:50:38
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
    public partial class PCEarPressCheckAccessor
    {
		public Model.PCEarPressCheck Get(string id)
		{
			return this.Get<Model.PCEarPressCheck>(id);
		}
		
		public void Insert(Model.PCEarPressCheck e)
		{
			this.Insert<Model.PCEarPressCheck>(e);
		}
		
		public void Update(Model.PCEarPressCheck e)
		{
			this.Update<Model.PCEarPressCheck>(e);
		}
		
		public IList<Model.PCEarPressCheck> Select()
		{
			return this.Select<Model.PCEarPressCheck>();
		}
		
		public IList<Model.PCEarPressCheck> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.PCEarPressCheck>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.PCEarPressCheck>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.PCEarPressCheck>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.PCEarPressCheck>();
		}
		public int Count()
		{
			return this.Count<Model.PCEarPressCheck>();
		}
		public bool HasRowsBefore(Model.PCEarPressCheck e)
		{
			return sqlmapper.QueryForObject<bool>("PCEarPressCheck.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.PCEarPressCheck e)
		{
			return sqlmapper.QueryForObject<bool>("PCEarPressCheck.has_rows_after", e);
		}
		public Model.PCEarPressCheck GetFirst()
		{
			return sqlmapper.QueryForObject<Model.PCEarPressCheck>("PCEarPressCheck.get_first", null);
		}
		public Model.PCEarPressCheck GetLast()
		{
			return sqlmapper.QueryForObject<Model.PCEarPressCheck>("PCEarPressCheck.get_last", null);
		}
		public Model.PCEarPressCheck GetNext(Model.PCEarPressCheck e)
		{
			return sqlmapper.QueryForObject<Model.PCEarPressCheck>("PCEarPressCheck.get_next", e);
		}
		public Model.PCEarPressCheck GetPrev(Model.PCEarPressCheck e)
		{
			return sqlmapper.QueryForObject<Model.PCEarPressCheck>("PCEarPressCheck.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("PCEarPressCheck.existsPrimary", id);
		}
    }
}
