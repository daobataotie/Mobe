﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoicePODetailAccessor.autogenerated.cs
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
    public partial class InvoicePODetailAccessor
    {
		public Model.InvoicePODetail Get(string id)
		{
			return this.Get<Model.InvoicePODetail>(id);
		}
		
		public void Insert(Model.InvoicePODetail e)
		{
			this.Insert<Model.InvoicePODetail>(e);
		}
		
		public void Update(Model.InvoicePODetail e)
		{
			this.Update<Model.InvoicePODetail>(e);
		}
		
		public IList<Model.InvoicePODetail> Select()
		{
			return this.Select<Model.InvoicePODetail>();
		}
		
		public IList<Model.InvoicePODetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.InvoicePODetail>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.InvoicePODetail>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.InvoicePODetail>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.InvoicePODetail>();
		}
		public int Count()
		{
			return this.Count<Model.InvoicePODetail>();
		}

    }
}
