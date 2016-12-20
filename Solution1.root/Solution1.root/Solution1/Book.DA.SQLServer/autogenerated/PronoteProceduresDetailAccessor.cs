﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PronoteProceduresDetailAccessor.autogenerated.cs
// author: mayanjun
// create date：2010-9-16 15:59:34
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
    public partial class PronoteProceduresDetailAccessor
    {
		public Model.PronoteProceduresDetail Get(string id)
		{
			return this.Get<Model.PronoteProceduresDetail>(id);
		}
		
		public void Insert(Model.PronoteProceduresDetail e)
		{
			this.Insert<Model.PronoteProceduresDetail>(e);
		}
		
		public void Update(Model.PronoteProceduresDetail e)
		{
			this.Update<Model.PronoteProceduresDetail>(e);
		}
		
		public IList<Model.PronoteProceduresDetail> Select()
		{
			return this.Select<Model.PronoteProceduresDetail>();
		}
		
		public IList<Model.PronoteProceduresDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.PronoteProceduresDetail>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.PronoteProceduresDetail>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.PronoteProceduresDetail>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.PronoteProceduresDetail>();
		}
		public int Count()
		{
			return this.Count<Model.PronoteProceduresDetail>();
		}

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("PronoteProceduresDetail.existsPrimary", id);
		}
    }
}
