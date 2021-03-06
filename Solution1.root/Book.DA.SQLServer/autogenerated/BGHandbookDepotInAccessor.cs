﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：BGHandbookDepotInAccessor.autogenerated.cs
// author: mayanjun
// create date：2013/12/27 13:34:11
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
    public partial class BGHandbookDepotInAccessor
    {
		public Model.BGHandbookDepotIn Get(string id)
		{
			return this.Get<Model.BGHandbookDepotIn>(id);
		}
		
		public void Insert(Model.BGHandbookDepotIn e)
		{
			this.Insert<Model.BGHandbookDepotIn>(e);
		}
		
		public void Update(Model.BGHandbookDepotIn e)
		{
			this.Update<Model.BGHandbookDepotIn>(e);
		}
		
		public IList<Model.BGHandbookDepotIn> Select()
		{
			return this.Select<Model.BGHandbookDepotIn>();
		}
		
		public IList<Model.BGHandbookDepotIn> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.BGHandbookDepotIn>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.BGHandbookDepotIn>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.BGHandbookDepotIn>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.BGHandbookDepotIn>();
		}
		public int Count()
		{
			return this.Count<Model.BGHandbookDepotIn>();
		}
		public bool HasRowsBefore(Model.BGHandbookDepotIn e)
		{
			return sqlmapper.QueryForObject<bool>("BGHandbookDepotIn.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.BGHandbookDepotIn e)
		{
			return sqlmapper.QueryForObject<bool>("BGHandbookDepotIn.has_rows_after", e);
		}
		public Model.BGHandbookDepotIn GetFirst()
		{
			return sqlmapper.QueryForObject<Model.BGHandbookDepotIn>("BGHandbookDepotIn.get_first", null);
		}
		public Model.BGHandbookDepotIn GetLast()
		{
			return sqlmapper.QueryForObject<Model.BGHandbookDepotIn>("BGHandbookDepotIn.get_last", null);
		}
		public Model.BGHandbookDepotIn GetNext(Model.BGHandbookDepotIn e)
		{
			return sqlmapper.QueryForObject<Model.BGHandbookDepotIn>("BGHandbookDepotIn.get_next", e);
		}
		public Model.BGHandbookDepotIn GetPrev(Model.BGHandbookDepotIn e)
		{
			return sqlmapper.QueryForObject<Model.BGHandbookDepotIn>("BGHandbookDepotIn.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("BGHandbookDepotIn.existsPrimary", id);
		}
    }
}
