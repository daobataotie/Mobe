﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AssemblySiteDifferenceAccessor.autogenerated.cs
// author: mayanjun
// create date：2018-05-14 19:16:32
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
    public partial class AssemblySiteDifferenceAccessor
    {
		public Model.AssemblySiteDifference Get(string id)
		{
			return this.Get<Model.AssemblySiteDifference>(id);
		}
		
		public void Insert(Model.AssemblySiteDifference e)
		{
			this.Insert<Model.AssemblySiteDifference>(e);
		}
		
		public void Update(Model.AssemblySiteDifference e)
		{
			this.Update<Model.AssemblySiteDifference>(e);
		}
		
		public IList<Model.AssemblySiteDifference> Select()
		{
			return this.Select<Model.AssemblySiteDifference>();
		}
		
		public IList<Model.AssemblySiteDifference> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.AssemblySiteDifference>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.AssemblySiteDifference>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.AssemblySiteDifference>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.AssemblySiteDifference>();
		}
		public int Count()
		{
			return this.Count<Model.AssemblySiteDifference>();
		}
		public bool HasRowsBefore(Model.AssemblySiteDifference e)
		{
			return sqlmapper.QueryForObject<bool>("AssemblySiteDifference.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.AssemblySiteDifference e)
		{
			return sqlmapper.QueryForObject<bool>("AssemblySiteDifference.has_rows_after", e);
		}
		public Model.AssemblySiteDifference GetFirst()
		{
			return sqlmapper.QueryForObject<Model.AssemblySiteDifference>("AssemblySiteDifference.get_first", null);
		}
		public Model.AssemblySiteDifference GetLast()
		{
			return sqlmapper.QueryForObject<Model.AssemblySiteDifference>("AssemblySiteDifference.get_last", null);
		}
		public Model.AssemblySiteDifference GetNext(Model.AssemblySiteDifference e)
		{
			return sqlmapper.QueryForObject<Model.AssemblySiteDifference>("AssemblySiteDifference.get_next", e);
		}
		public Model.AssemblySiteDifference GetPrev(Model.AssemblySiteDifference e)
		{
			return sqlmapper.QueryForObject<Model.AssemblySiteDifference>("AssemblySiteDifference.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("AssemblySiteDifference.existsPrimary", id);
		}
    }
}
