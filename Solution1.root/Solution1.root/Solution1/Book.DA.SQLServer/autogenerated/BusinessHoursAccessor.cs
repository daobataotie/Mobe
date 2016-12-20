﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：BusinessHoursAccessor.autogenerated.cs
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
    public partial class BusinessHoursAccessor
    {
		public Model.BusinessHours Get(string id)
		{
			return this.Get<Model.BusinessHours>(id);
		}
		
		public void Insert(Model.BusinessHours e)
		{
			this.Insert<Model.BusinessHours>(e);
		}
		
		public void Update(Model.BusinessHours e)
		{
			this.Update<Model.BusinessHours>(e);
		}
		
		public IList<Model.BusinessHours> Select()
		{
			return this.Select<Model.BusinessHours>();
		}
		
		public IList<Model.BusinessHours> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.BusinessHours>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.BusinessHours>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.BusinessHours>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.BusinessHours>();
		}
		public int Count()
		{
			return this.Count<Model.BusinessHours>();
		}
		public bool HasRowsBefore(Model.BusinessHours e)
		{
			return sqlmapper.QueryForObject<bool>("BusinessHours.has_rows_before", e);
		}
		public bool HasRowsAfter(Model.BusinessHours e)
		{
			return sqlmapper.QueryForObject<bool>("BusinessHours.has_rows_after", e);
		}
		public Model.BusinessHours GetFirst()
		{
			return sqlmapper.QueryForObject<Model.BusinessHours>("BusinessHours.get_first", null);
		}
		public Model.BusinessHours GetLast()
		{
			return sqlmapper.QueryForObject<Model.BusinessHours>("BusinessHours.get_last", null);
		}
		public Model.BusinessHours GetNext(Model.BusinessHours e)
		{
			return sqlmapper.QueryForObject<Model.BusinessHours>("BusinessHours.get_next", e);
		}
		public Model.BusinessHours GetPrev(Model.BusinessHours e)
		{
			return sqlmapper.QueryForObject<Model.BusinessHours>("BusinessHours.get_prev", e);
		}
		

    }
}
