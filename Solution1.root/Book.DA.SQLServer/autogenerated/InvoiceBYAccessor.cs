﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceBYAccessor.autogenerated.cs
// author: peidun
// create date：2009-10-23 下午 09:47:43
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
    public partial class InvoiceBYAccessor
    {
		public Model.InvoiceBY Get(string id)
		{
			return this.Get<Model.InvoiceBY>(id);
		}
		
		public void Insert(Model.InvoiceBY e)
		{
			this.Insert<Model.InvoiceBY>(e);
		}
		
		public void Update(Model.InvoiceBY e)
		{
			this.Update<Model.InvoiceBY>(e);
		}
		
		public IList<Model.InvoiceBY> Select()
		{
			return this.Select<Model.InvoiceBY>();
		}
		
		public IList<Model.InvoiceBY> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.InvoiceBY>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.InvoiceBY>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.InvoiceBY>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.InvoiceBY>();
		}
		public int Count()
		{
			return this.Count<Model.InvoiceBY>();
		}
		public bool HasRowsBefore(Model.InvoiceBY e)
		{
			return sqlmapper.QueryForObject<bool>("InvoiceBY.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.InvoiceBY e)
		{
			return sqlmapper.QueryForObject<bool>("InvoiceBY.has_rows_after", e);
		}
		public Model.InvoiceBY GetFirst()
		{
			return sqlmapper.QueryForObject<Model.InvoiceBY>("InvoiceBY.get_first", null);
		}
		public Model.InvoiceBY GetLast()
		{
			return sqlmapper.QueryForObject<Model.InvoiceBY>("InvoiceBY.get_last", null);
		}
		public Model.InvoiceBY GetNext(Model.InvoiceBY e)
		{
			return sqlmapper.QueryForObject<Model.InvoiceBY>("InvoiceBY.get_next", e);
		}
		public Model.InvoiceBY GetPrev(Model.InvoiceBY e)
		{
			return sqlmapper.QueryForObject<Model.InvoiceBY>("InvoiceBY.get_prev", e);
		}
		

    }
}
