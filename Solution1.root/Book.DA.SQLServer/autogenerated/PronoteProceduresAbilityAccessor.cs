﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PronoteProceduresAbilityAccessor.autogenerated.cs
// author: mayanjun
// create date：2010-9-23 16:33:52
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
    public partial class PronoteProceduresAbilityAccessor
    {
		public Model.PronoteProceduresAbility Get(string id)
		{
			return this.Get<Model.PronoteProceduresAbility>(id);
		}
		
		public void Insert(Model.PronoteProceduresAbility e)
		{
			this.Insert<Model.PronoteProceduresAbility>(e);
		}
		
		public void Update(Model.PronoteProceduresAbility e)
		{
			this.Update<Model.PronoteProceduresAbility>(e);
		}
		
		public IList<Model.PronoteProceduresAbility> Select()
		{
			return this.Select<Model.PronoteProceduresAbility>();
		}
		
		public IList<Model.PronoteProceduresAbility> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.PronoteProceduresAbility>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.PronoteProceduresAbility>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.PronoteProceduresAbility>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.PronoteProceduresAbility>();
		}
		public int Count()
		{
			return this.Count<Model.PronoteProceduresAbility>();
		}
		public bool HasRowsBefore(Model.PronoteProceduresAbility e)
		{
			return sqlmapper.QueryForObject<bool>("PronoteProceduresAbility.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.PronoteProceduresAbility e)
		{
			return sqlmapper.QueryForObject<bool>("PronoteProceduresAbility.has_rows_after", e);
		}
		public Model.PronoteProceduresAbility GetFirst()
		{
			return sqlmapper.QueryForObject<Model.PronoteProceduresAbility>("PronoteProceduresAbility.get_first", null);
		}
		public Model.PronoteProceduresAbility GetLast()
		{
			return sqlmapper.QueryForObject<Model.PronoteProceduresAbility>("PronoteProceduresAbility.get_last", null);
		}
		public Model.PronoteProceduresAbility GetNext(Model.PronoteProceduresAbility e)
		{
			return sqlmapper.QueryForObject<Model.PronoteProceduresAbility>("PronoteProceduresAbility.get_next", e);
		}
		public Model.PronoteProceduresAbility GetPrev(Model.PronoteProceduresAbility e)
		{
			return sqlmapper.QueryForObject<Model.PronoteProceduresAbility>("PronoteProceduresAbility.get_prev", e);
		}
		

		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("PronoteProceduresAbility.existsPrimary", id);
		}
    }
}
