﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：CustomerProductProcessAccessor.autogenerated.cs
// author: peidun
// create date：2009-10-26 下午 05:56:41
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
    public partial class CustomerProductProcessAccessor
    {
		public Model.CustomerProductProcess Get(string id)
		{
			return this.Get<Model.CustomerProductProcess>(id);
		}
		
		public void Insert(Model.CustomerProductProcess e)
		{
			this.Insert<Model.CustomerProductProcess>(e);
		}
		
		public void Update(Model.CustomerProductProcess e)
		{
			this.Update<Model.CustomerProductProcess>(e);
		}
		
		public IList<Model.CustomerProductProcess> Select()
		{
			return this.Select<Model.CustomerProductProcess>();
		}
		
		public IList<Model.CustomerProductProcess> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.CustomerProductProcess>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.CustomerProductProcess>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.CustomerProductProcess>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.CustomerProductProcess>();
		}
		public int Count()
		{
			return this.Count<Model.CustomerProductProcess>();
		}

    }
}
