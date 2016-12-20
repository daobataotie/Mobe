﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AnnualHolidayAccessor.autogenerated.cs
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
    public partial class AnnualHolidayAccessor
    {
		public Model.AnnualHoliday Get(string id)
		{
			return this.Get<Model.AnnualHoliday>(id);
		}
		
		public void Insert(Model.AnnualHoliday e)
		{
			this.Insert<Model.AnnualHoliday>(e);
		}
		
		public void Update(Model.AnnualHoliday e)
		{
			this.Update<Model.AnnualHoliday>(e);
		}
		
		public IList<Model.AnnualHoliday> Select()
		{
			return this.Select<Model.AnnualHoliday>();
		}
		
		public IList<Model.AnnualHoliday> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return this.Select<Model.AnnualHoliday>(orderDescription,pagingDescription);
		}
		public void Delete(string id)
		{
			this.Delete<Model.AnnualHoliday>(id);
		}
		public bool HasRows(string id)
		{
			return this.HasRows<Model.AnnualHoliday>(id);
		}
		public bool HasRows()
		{
			return this.HasRows<Model.AnnualHoliday>();
		}
		public int Count()
		{
			return this.Count<Model.AnnualHoliday>();
		}

    }
}
