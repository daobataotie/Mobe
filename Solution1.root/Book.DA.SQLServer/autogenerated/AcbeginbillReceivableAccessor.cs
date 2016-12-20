﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcbeginbillReceivableAccessor.autogenerated.cs
// author: mayanjun
// create date：2011-6-9 16:24:48
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
    public partial class AcbeginbillReceivableAccessor
    {
		public Model.AcbeginbillReceivable Get(string id)
		{
			return this.Get<Model.AcbeginbillReceivable>(id);
		}
		
		public void Insert(Model.AcbeginbillReceivable e)
		{
			this.Insert<Model.AcbeginbillReceivable>(e);
		}
		
		public void Update(Model.AcbeginbillReceivable e)
		{
			this.Update<Model.AcbeginbillReceivable>(e);
		}
		
		public IList<Model.AcbeginbillReceivable> Select()
		{
			return this.Select<Model.AcbeginbillReceivable>();
		}
		
		public IList<Model.AcbeginbillReceivable> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.AcbeginbillReceivable>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.AcbeginbillReceivable>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.AcbeginbillReceivable>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.AcbeginbillReceivable>();
		}
		public int Count()
		{
			return this.Count<Model.AcbeginbillReceivable>();
		}
		public bool HasRowsBefore(Model.AcbeginbillReceivable e)
		{
			return sqlmapper.QueryForObject<bool>("AcbeginbillReceivable.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.AcbeginbillReceivable e)
		{
			return sqlmapper.QueryForObject<bool>("AcbeginbillReceivable.has_rows_after", e);
		}
		public Model.AcbeginbillReceivable GetFirst()
		{
			return sqlmapper.QueryForObject<Model.AcbeginbillReceivable>("AcbeginbillReceivable.get_first", null);
		}
		public Model.AcbeginbillReceivable GetLast()
		{
			return sqlmapper.QueryForObject<Model.AcbeginbillReceivable>("AcbeginbillReceivable.get_last", null);
		}
		public Model.AcbeginbillReceivable GetNext(Model.AcbeginbillReceivable e)
		{
			return sqlmapper.QueryForObject<Model.AcbeginbillReceivable>("AcbeginbillReceivable.get_next", e);
		}
		public Model.AcbeginbillReceivable GetPrev(Model.AcbeginbillReceivable e)
		{
			return sqlmapper.QueryForObject<Model.AcbeginbillReceivable>("AcbeginbillReceivable.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("AcbeginbillReceivable.existsPrimary", id);
		}
    }
}
