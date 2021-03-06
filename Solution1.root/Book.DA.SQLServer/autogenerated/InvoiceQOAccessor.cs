﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceQOAccessor.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:04
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
    public partial class InvoiceQOAccessor
    {
		public Model.InvoiceQO Get(string id)
		{
			return this.Get<Model.InvoiceQO>(id);
		}
		
		public void Insert(Model.InvoiceQO e)
		{
			this.Insert<Model.InvoiceQO>(e);
		}
		
		public void Update(Model.InvoiceQO e)
		{
			this.Update<Model.InvoiceQO>(e);
		}
		
		public IList<Model.InvoiceQO> Select()
		{
			return this.Select<Model.InvoiceQO>();
		}
		
		public IList<Model.InvoiceQO> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.InvoiceQO>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.InvoiceQO>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.InvoiceQO>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.InvoiceQO>();
		}
		public int Count()
		{
			return this.Count<Model.InvoiceQO>();
		}
		public bool HasRowsBefore(Model.InvoiceQO e)
		{
			return sqlmapper.QueryForObject<bool>("InvoiceQO.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.InvoiceQO e)
		{
			return sqlmapper.QueryForObject<bool>("InvoiceQO.has_rows_after", e);
		}
		public Model.InvoiceQO GetFirst()
		{
			return sqlmapper.QueryForObject<Model.InvoiceQO>("InvoiceQO.get_first", null);
		}
		public Model.InvoiceQO GetLast()
		{
			return sqlmapper.QueryForObject<Model.InvoiceQO>("InvoiceQO.get_last", null);
		}
		public Model.InvoiceQO GetNext(Model.InvoiceQO e)
		{
			return sqlmapper.QueryForObject<Model.InvoiceQO>("InvoiceQO.get_next", e);
		}
		public Model.InvoiceQO GetPrev(Model.InvoiceQO e)
		{
			return sqlmapper.QueryForObject<Model.InvoiceQO>("InvoiceQO.get_prev", e);
		}
		

    }
}
