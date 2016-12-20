﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：IncomingKindAccessor.autogenerated.cs
// author: peidun
// create date：2010-4-22 11:01:39
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
    public partial class IncomingKindAccessor
    {
		public Model.IncomingKind Get(string id)
		{
			return this.Get<Model.IncomingKind>(id);
		}
		
		public void Insert(Model.IncomingKind e)
		{
			this.Insert<Model.IncomingKind>(e);
		}
		
		public void Update(Model.IncomingKind e)
		{
			this.Update<Model.IncomingKind>(e);
		}
		
		public IList<Model.IncomingKind> Select()
		{
			return this.Select<Model.IncomingKind>();
		}
		
		public IList<Model.IncomingKind> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.IncomingKind>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.IncomingKind>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.IncomingKind>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.IncomingKind>();
		}
		public int Count()
		{
			return this.Count<Model.IncomingKind>();
		}
		public bool HasRowsBefore(Model.IncomingKind e)
		{
			return sqlmapper.QueryForObject<bool>("IncomingKind.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.IncomingKind e)
		{
			return sqlmapper.QueryForObject<bool>("IncomingKind.has_rows_after", e);
		}
		public Model.IncomingKind GetFirst()
		{
			return sqlmapper.QueryForObject<Model.IncomingKind>("IncomingKind.get_first", null);
		}
		public Model.IncomingKind GetLast()
		{
			return sqlmapper.QueryForObject<Model.IncomingKind>("IncomingKind.get_last", null);
		}
		public Model.IncomingKind GetNext(Model.IncomingKind e)
		{
			return sqlmapper.QueryForObject<Model.IncomingKind>("IncomingKind.get_next", e);
		}
		public Model.IncomingKind GetPrev(Model.IncomingKind e)
		{
			return sqlmapper.QueryForObject<Model.IncomingKind>("IncomingKind.get_prev", e);
		}
		

		public bool Exists(string id)
		{
			return sqlmapper.QueryForObject<bool>("IncomingKind.exists", id);
		}
		
		public Model.IncomingKind GetById(string id)
		{
			return sqlmapper.QueryForObject<Model.IncomingKind>("IncomingKind.get_by_id", id);
		}
		
		public bool ExistsExcept(Model.IncomingKind e)
		{
			Hashtable paras = new Hashtable();
			paras.Add("newId", e.Id);
            paras.Add("oldId", Get(e.IncomingKindId)==null?null:Get(e.IncomingKindId).Id);
			return sqlmapper.QueryForObject<bool>("IncomingKind.existsexcept", paras);
		}
		
		
		
		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("IncomingKind.existsPrimary", id);
		}
    }
}
