﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProductEpibolyAccessor.autogenerated.cs
// author: peidun
// create date：2009-12-16 11:41:03
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
    public partial class ProductEpibolyAccessor
    {
		public Model.ProductEpiboly Get(string id)
		{
			return this.Get<Model.ProductEpiboly>(id);
		}
		
		public void Insert(Model.ProductEpiboly e)
		{
			this.Insert<Model.ProductEpiboly>(e);
		}
		
		public void Update(Model.ProductEpiboly e)
		{
			this.Update<Model.ProductEpiboly>(e);
		}
		
		public IList<Model.ProductEpiboly> Select()
		{
			return this.Select<Model.ProductEpiboly>();
		}
		
		public IList<Model.ProductEpiboly> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.ProductEpiboly>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.ProductEpiboly>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.ProductEpiboly>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.ProductEpiboly>();
		}
		public int Count()
		{
			return this.Count<Model.ProductEpiboly>();
		}
		public bool HasRowsBefore(Model.ProductEpiboly e)
		{
			return sqlmapper.QueryForObject<bool>("ProductEpiboly.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.ProductEpiboly e)
		{
			return sqlmapper.QueryForObject<bool>("ProductEpiboly.has_rows_after", e);
		}
		public Model.ProductEpiboly GetFirst()
		{
			return sqlmapper.QueryForObject<Model.ProductEpiboly>("ProductEpiboly.get_first", null);
		}
		public Model.ProductEpiboly GetLast()
		{
			return sqlmapper.QueryForObject<Model.ProductEpiboly>("ProductEpiboly.get_last", null);
		}
		public Model.ProductEpiboly GetNext(Model.ProductEpiboly e)
		{
			return sqlmapper.QueryForObject<Model.ProductEpiboly>("ProductEpiboly.get_next", e);
		}
		public Model.ProductEpiboly GetPrev(Model.ProductEpiboly e)
		{
			return sqlmapper.QueryForObject<Model.ProductEpiboly>("ProductEpiboly.get_prev", e);
		}
		

		public bool Exists(string id)
		{
			return sqlmapper.QueryForObject<bool>("ProductEpiboly.exists", id);
		}
		
		public Model.ProductEpiboly GetById(string id)
		{
			return sqlmapper.QueryForObject<Model.ProductEpiboly>("ProductEpiboly.get_by_id", id);
		}
		
		public bool ExistsExcept(Model.ProductEpiboly e)
		{
			Hashtable paras = new Hashtable();
			paras.Add("newId", e.Id);
            paras.Add("oldId", Get(e.ProductEpibolyId).Id);
			return sqlmapper.QueryForObject<bool>("ProductEpiboly.existsexcept", paras);
		}
    }
}
