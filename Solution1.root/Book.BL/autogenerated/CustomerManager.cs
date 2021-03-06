﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：CustomerManager.autogenerated.cs
// author: peidun
// create date：2009-10-26 下午 05:56:40
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class CustomerManager
    {
		///<summary>
		/// Data accessor of dbo.Customer
		///</summary>
		private static readonly DA.ICustomerAccessor accessor = (DA.ICustomerAccessor)Accessors.Get("CustomerAccessor");
				
		public bool HasRows(string customerId)
		{
			return accessor.HasRows(customerId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool Exists(string id)
		{
			return accessor.Exists(id);
		}
		
		public Model.Customer GetById(string id)
		{
			return accessor.GetById(id);
		}
		
		public bool ExistsExcept(Model.Customer e)
		{
			return accessor.ExistsExcept(e);
		}
		public bool HasRowsBefore(Model.Customer e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.Customer e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.Customer GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.Customer GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.Customer GetPrev(Model.Customer e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.Customer GetNext(Model.Customer e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.Customer> Select()
		{
			return accessor.Select();
		}
		
		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int Count()
		{
			return accessor.Count();
		}
		
		/// <summary>
		/// 获取指定状态、指定分页，并按指定要求排序的记录
		/// </summary>
		public IList<Model.Customer> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
