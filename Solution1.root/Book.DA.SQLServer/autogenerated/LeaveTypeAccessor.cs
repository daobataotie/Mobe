﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：LeaveTypeAccessor.autogenerated.cs
// author: peidun
// create date：2010-2-6 10:33:09
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
    public partial class LeaveTypeAccessor
    {
		public Model.LeaveType Get(string id)
		{
			return this.Get<Model.LeaveType>(id);
		}
		
		public void Insert(Model.LeaveType e)
		{
			this.Insert<Model.LeaveType>(e);
		}
		
		public void Update(Model.LeaveType e)
		{
			this.Update<Model.LeaveType>(e);
		}
		
		public IList<Model.LeaveType> Select()
		{
			return this.Select<Model.LeaveType>();
		}
		
		public IList<Model.LeaveType> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.LeaveType>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.LeaveType>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.LeaveType>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.LeaveType>();
		}
		public int Count()
		{
			return this.Count<Model.LeaveType>();
		}
		public bool HasRowsBefore(Model.LeaveType e)
		{
			return sqlmapper.QueryForObject<bool>("LeaveType.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.LeaveType e)
		{
			return sqlmapper.QueryForObject<bool>("LeaveType.has_rows_after", e);
		}
		public Model.LeaveType GetFirst()
		{
			return sqlmapper.QueryForObject<Model.LeaveType>("LeaveType.get_first", null);
		}
		public Model.LeaveType GetLast()
		{
			return sqlmapper.QueryForObject<Model.LeaveType>("LeaveType.get_last", null);
		}
		public Model.LeaveType GetNext(Model.LeaveType e)
		{
			return sqlmapper.QueryForObject<Model.LeaveType>("LeaveType.get_next", e);
		}
		public Model.LeaveType GetPrev(Model.LeaveType e)
		{
			return sqlmapper.QueryForObject<Model.LeaveType>("LeaveType.get_prev", e);
		}
		

    }
}
