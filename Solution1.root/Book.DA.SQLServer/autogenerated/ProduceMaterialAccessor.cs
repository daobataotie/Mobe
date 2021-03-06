﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceMaterialAccessor.autogenerated.cs
// author: mayanjun
// create date：2011-11-10 11:15:24
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
    public partial class ProduceMaterialAccessor
    {
		public Model.ProduceMaterial Get(string id)
		{
			return this.Get<Model.ProduceMaterial>(id);
		}
		
		public void Insert(Model.ProduceMaterial e)
		{
			this.Insert<Model.ProduceMaterial>(e);
		}
		
		public void Update(Model.ProduceMaterial e)
		{
			this.Update<Model.ProduceMaterial>(e);
		}
		
		public IList<Model.ProduceMaterial> Select()
		{
			return this.Select<Model.ProduceMaterial>();
		}
		
		public IList<Model.ProduceMaterial> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.ProduceMaterial>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.ProduceMaterial>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.ProduceMaterial>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.ProduceMaterial>();
		}
		public int Count()
		{
			return this.Count<Model.ProduceMaterial>();
		}
		public bool HasRowsBefore(Model.ProduceMaterial e)
		{
			return sqlmapper.QueryForObject<bool>("ProduceMaterial.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.ProduceMaterial e)
		{
			return sqlmapper.QueryForObject<bool>("ProduceMaterial.has_rows_after", e);
		}
		public Model.ProduceMaterial GetFirst()
		{
			return sqlmapper.QueryForObject<Model.ProduceMaterial>("ProduceMaterial.get_first", null);
		}
		public Model.ProduceMaterial GetLast()
		{
			return sqlmapper.QueryForObject<Model.ProduceMaterial>("ProduceMaterial.get_last", null);
		}
		public Model.ProduceMaterial GetNext(Model.ProduceMaterial e)
		{
			return sqlmapper.QueryForObject<Model.ProduceMaterial>("ProduceMaterial.get_next", e);
		}
		public Model.ProduceMaterial GetPrev(Model.ProduceMaterial e)
		{
			return sqlmapper.QueryForObject<Model.ProduceMaterial>("ProduceMaterial.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("ProduceMaterial.existsPrimary", id);
		}
    }
}
