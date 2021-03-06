﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCSamplingEarDetailAccessor.autogenerated.cs
// author: mayanjun
// create date：2015/10/31 16:25:12
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
    public partial class PCSamplingEarDetailAccessor
    {
		public Model.PCSamplingEarDetail Get(string id)
		{
			return this.Get<Model.PCSamplingEarDetail>(id);
		}
		
		public void Insert(Model.PCSamplingEarDetail e)
		{
			this.Insert<Model.PCSamplingEarDetail>(e);
		}
		
		public void Update(Model.PCSamplingEarDetail e)
		{
			this.Update<Model.PCSamplingEarDetail>(e);
		}
		
		public IList<Model.PCSamplingEarDetail> Select()
		{
			return this.Select<Model.PCSamplingEarDetail>();
		}
		
		public IList<Model.PCSamplingEarDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.PCSamplingEarDetail>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.PCSamplingEarDetail>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.PCSamplingEarDetail>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.PCSamplingEarDetail>();
		}
		public int Count()
		{
			return this.Count<Model.PCSamplingEarDetail>();
		}

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("PCSamplingEarDetail.existsPrimary", id);
		}
    }
}
