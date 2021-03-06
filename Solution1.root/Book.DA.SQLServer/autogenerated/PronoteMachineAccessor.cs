﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PronoteMachineAccessor.autogenerated.cs
// author: mayanjun
// create date：2010-9-16 16:37:29
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
    public partial class PronoteMachineAccessor
    {
		public Model.PronoteMachine Get(string id)
		{
			return this.Get<Model.PronoteMachine>(id);
		}
		
		public void Insert(Model.PronoteMachine e)
		{
			this.Insert<Model.PronoteMachine>(e);
		}
		
		public void Update(Model.PronoteMachine e)
		{
			this.Update<Model.PronoteMachine>(e);
		}
		
		public IList<Model.PronoteMachine> Select()
		{
			return this.Select<Model.PronoteMachine>();
		}
		
		public IList<Model.PronoteMachine> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.PronoteMachine>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.PronoteMachine>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.PronoteMachine>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.PronoteMachine>();
		}
		public int Count()
		{
			return this.Count<Model.PronoteMachine>();
		}
		public bool HasRowsBefore(Model.PronoteMachine e)
		{
			return sqlmapper.QueryForObject<bool>("PronoteMachine.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.PronoteMachine e)
		{
			return sqlmapper.QueryForObject<bool>("PronoteMachine.has_rows_after", e);
		}
		public Model.PronoteMachine GetFirst()
		{
			return sqlmapper.QueryForObject<Model.PronoteMachine>("PronoteMachine.get_first", null);
		}
		public Model.PronoteMachine GetLast()
		{
			return sqlmapper.QueryForObject<Model.PronoteMachine>("PronoteMachine.get_last", null);
		}
		public Model.PronoteMachine GetNext(Model.PronoteMachine e)
		{
			return sqlmapper.QueryForObject<Model.PronoteMachine>("PronoteMachine.get_next", e);
		}
		public Model.PronoteMachine GetPrev(Model.PronoteMachine e)
		{
			return sqlmapper.QueryForObject<Model.PronoteMachine>("PronoteMachine.get_prev", e);
		}
		

		public bool Exists(string id)
		{
			return sqlmapper.QueryForObject<bool>("PronoteMachine.exists", id);
		}
		
		public Model.PronoteMachine GetById(string id)
		{
			return sqlmapper.QueryForObject<Model.PronoteMachine>("PronoteMachine.get_by_id", id);
		}
		
		public bool ExistsExcept(Model.PronoteMachine e)
		{
			Hashtable paras = new Hashtable();
			paras.Add("newId", e.Id);
            paras.Add("oldId", Get(e.PronoteMachineId)==null?null:Get(e.PronoteMachineId).Id);
			return sqlmapper.QueryForObject<bool>("PronoteMachine.existsexcept", paras);
		}
		
		
		
		public bool ExistsPrimary(string id)
		{			
			return sqlmapper.QueryForObject<bool>("PronoteMachine.existsPrimary", id);
		}
    }
}
