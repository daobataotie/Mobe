﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：TablesManager.autogenerated.cs
// author: peidun
// create date：2009-12-11 14:53:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class TablesManager
    {
		///<summary>
		/// Data accessor of dbo.Tables
		///</summary>
		private static readonly DA.ITablesAccessor accessor = (DA.ITablesAccessor)Accessors.Get("TablesAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.Tables Get(string tablesID)
		{
			return accessor.Get(tablesID);
		}
		
		public bool HasRows(string tablesID)
		{
			return accessor.HasRows(tablesID);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool HasRowsBefore(Model.Tables e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.Tables e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.Tables GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.Tables GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.Tables GetPrev(Model.Tables e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.Tables GetNext(Model.Tables e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.Tables> Select()
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
		public IList<Model.Tables> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
