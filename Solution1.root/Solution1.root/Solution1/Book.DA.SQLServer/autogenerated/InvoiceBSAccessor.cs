﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceBSAccessor.autogenerated.cs
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
    public partial class InvoiceBSAccessor
    {
		public Model.InvoiceBS Get(string id)
		{
			return this.Get<Model.InvoiceBS>(id);
		}
		
		public void Insert(Model.InvoiceBS e)
		{
			this.Insert<Model.InvoiceBS>(e);
		}
		
		public void Update(Model.InvoiceBS e)
		{
			this.Update<Model.InvoiceBS>(e);
		}
		
		public IList<Model.InvoiceBS> Select()
		{
			return this.Select<Model.InvoiceBS>();
		}
		
		public IList<Model.InvoiceBS> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.InvoiceBS>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.InvoiceBS>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.InvoiceBS>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.InvoiceBS>();
		}
		public int Count()
		{
			return this.Count<Model.InvoiceBS>();
		}
		public bool HasRowsBefore(Model.InvoiceBS e)
		{
			return sqlmapper.QueryForObject<bool>("InvoiceBS.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.InvoiceBS e)
		{
			return sqlmapper.QueryForObject<bool>("InvoiceBS.has_rows_after", e);
		}
		public Model.InvoiceBS GetFirst()
		{
			return sqlmapper.QueryForObject<Model.InvoiceBS>("InvoiceBS.get_first", null);
		}
		public Model.InvoiceBS GetLast()
		{
			return sqlmapper.QueryForObject<Model.InvoiceBS>("InvoiceBS.get_last", null);
		}
		public Model.InvoiceBS GetNext(Model.InvoiceBS e)
		{
			return sqlmapper.QueryForObject<Model.InvoiceBS>("InvoiceBS.get_next", e);
		}
		public Model.InvoiceBS GetPrev(Model.InvoiceBS e)
		{
			return sqlmapper.QueryForObject<Model.InvoiceBS>("InvoiceBS.get_prev", e);
		}
		

    }
}
