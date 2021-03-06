﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：BGHandbookIdSetAccessor.autogenerated.cs
// author: mayanjun
// create date：2013-07-05 11:57:55
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
    public partial class BGHandbookIdSetAccessor
    {
		public Model.BGHandbookIdSet Get(string id)
		{
			return this.Get<Model.BGHandbookIdSet>(id);
		}
		
		public void Insert(Model.BGHandbookIdSet e)
		{
			this.Insert<Model.BGHandbookIdSet>(e);
		}
		
		public void Update(Model.BGHandbookIdSet e)
		{
			this.Update<Model.BGHandbookIdSet>(e);
		}
		
		public IList<Model.BGHandbookIdSet> Select()
		{
			return this.Select<Model.BGHandbookIdSet>();
		}
		
		public IList<Model.BGHandbookIdSet> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.BGHandbookIdSet>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.BGHandbookIdSet>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.BGHandbookIdSet>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.BGHandbookIdSet>();
		}
		public int Count()
		{
			return this.Count<Model.BGHandbookIdSet>();
		}
		public bool HasRowsBefore(Model.BGHandbookIdSet e)
		{
			return sqlmapper.QueryForObject<bool>("BGHandbookIdSet.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.BGHandbookIdSet e)
		{
			return sqlmapper.QueryForObject<bool>("BGHandbookIdSet.has_rows_after", e);
		}
		public Model.BGHandbookIdSet GetFirst()
		{
			return sqlmapper.QueryForObject<Model.BGHandbookIdSet>("BGHandbookIdSet.get_first", null);
		}
		public Model.BGHandbookIdSet GetLast()
		{
			return sqlmapper.QueryForObject<Model.BGHandbookIdSet>("BGHandbookIdSet.get_last", null);
		}
		public Model.BGHandbookIdSet GetNext(Model.BGHandbookIdSet e)
		{
			return sqlmapper.QueryForObject<Model.BGHandbookIdSet>("BGHandbookIdSet.get_next", e);
		}
		public Model.BGHandbookIdSet GetPrev(Model.BGHandbookIdSet e)
		{
			return sqlmapper.QueryForObject<Model.BGHandbookIdSet>("BGHandbookIdSet.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("BGHandbookIdSet.existsPrimary", id);
		}
    }
}
