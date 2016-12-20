﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：DepotInDetailAccessor.autogenerated.cs
// author: mayanjun
// create date：2010-10-25 16:14:47
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
    public partial class DepotInDetailAccessor
    {
		public Model.DepotInDetail Get(string id)
		{
			return this.Get<Model.DepotInDetail>(id);
		}
		
		public void Insert(Model.DepotInDetail e)
		{
			this.Insert<Model.DepotInDetail>(e);
		}
		
		public void Update(Model.DepotInDetail e)
		{
			this.Update<Model.DepotInDetail>(e);
		}
		
		public IList<Model.DepotInDetail> Select()
		{
			return this.Select<Model.DepotInDetail>();
		}
		
		public IList<Model.DepotInDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.DepotInDetail>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.DepotInDetail>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.DepotInDetail>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.DepotInDetail>();
		}
		public int Count()
		{
			return this.Count<Model.DepotInDetail>();
		}

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("DepotInDetail.existsPrimary", id);
		}
    }
}
