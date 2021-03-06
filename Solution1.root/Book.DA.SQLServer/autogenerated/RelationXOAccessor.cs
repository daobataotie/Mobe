﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：RelationXOAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/4/19 下午 08:06:08
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
    public partial class RelationXOAccessor
    {
		public Model.RelationXO Get(string id)
		{
			return this.Get<Model.RelationXO>(id);
		}
		
		public void Insert(Model.RelationXO e)
		{
			this.Insert<Model.RelationXO>(e);
		}
		
		public void Update(Model.RelationXO e)
		{
			this.Update<Model.RelationXO>(e);
		}
		
		public IList<Model.RelationXO> Select()
		{
			return this.Select<Model.RelationXO>();
		}
		
		public IList<Model.RelationXO> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.RelationXO>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.RelationXO>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.RelationXO>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.RelationXO>();
		}
		public int Count()
		{
			return this.Count<Model.RelationXO>();
		}
		public bool HasRowsBefore(Model.RelationXO e)
		{
			return sqlmapper.QueryForObject<bool>("RelationXO.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.RelationXO e)
		{
			return sqlmapper.QueryForObject<bool>("RelationXO.has_rows_after", e);
		}
		public Model.RelationXO GetFirst()
		{
			return sqlmapper.QueryForObject<Model.RelationXO>("RelationXO.get_first", null);
		}
		public Model.RelationXO GetLast()
		{
			return sqlmapper.QueryForObject<Model.RelationXO>("RelationXO.get_last", null);
		}
		public Model.RelationXO GetNext(Model.RelationXO e)
		{
			return sqlmapper.QueryForObject<Model.RelationXO>("RelationXO.get_next", e);
		}
		public Model.RelationXO GetPrev(Model.RelationXO e)
		{
			return sqlmapper.QueryForObject<Model.RelationXO>("RelationXO.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("RelationXO.existsPrimary", id);
		}
    }
}
