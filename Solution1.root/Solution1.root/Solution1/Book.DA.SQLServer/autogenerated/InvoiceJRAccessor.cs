﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceJRAccessor.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:03
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
    public partial class InvoiceJRAccessor
    {
		public Model.InvoiceJR Get(string id)
		{
			return this.Get<Model.InvoiceJR>(id);
		}
		
		public void Insert(Model.InvoiceJR e)
		{
			this.Insert<Model.InvoiceJR>(e);
		}
		
		public void Update(Model.InvoiceJR e)
		{
			this.Update<Model.InvoiceJR>(e);
		}
		
		public IList<Model.InvoiceJR> Select()
		{
			return this.Select<Model.InvoiceJR>();
		}
		
		public IList<Model.InvoiceJR> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.InvoiceJR>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.InvoiceJR>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.InvoiceJR>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.InvoiceJR>();
		}
		public int Count()
		{
			return this.Count<Model.InvoiceJR>();
		}
		public bool HasRowsBefore(Model.InvoiceJR e)
		{
			return sqlmapper.QueryForObject<bool>("InvoiceJR.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.InvoiceJR e)
		{
			return sqlmapper.QueryForObject<bool>("InvoiceJR.has_rows_after", e);
		}
		public Model.InvoiceJR GetFirst()
		{
			return sqlmapper.QueryForObject<Model.InvoiceJR>("InvoiceJR.get_first", null);
		}
		public Model.InvoiceJR GetLast()
		{
			return sqlmapper.QueryForObject<Model.InvoiceJR>("InvoiceJR.get_last", null);
		}
		public Model.InvoiceJR GetNext(Model.InvoiceJR e)
		{
			return sqlmapper.QueryForObject<Model.InvoiceJR>("InvoiceJR.get_next", e);
		}
		public Model.InvoiceJR GetPrev(Model.InvoiceJR e)
		{
			return sqlmapper.QueryForObject<Model.InvoiceJR>("InvoiceJR.get_prev", e);
		}
		

    }
}
