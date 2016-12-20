﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：MouldCategoryManager.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class MouldCategoryManager
    {
		///<summary>
		/// Data accessor of dbo.MouldCategory
		///</summary>
		private static readonly DA.IMouldCategoryAccessor accessor = (DA.IMouldCategoryAccessor)Accessors.Get("MouldCategoryAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.MouldCategory Get(string mouldCategoryId)
		{
			return accessor.Get(mouldCategoryId);
		}
		
		public bool HasRows(string mouldCategoryId)
		{
			return accessor.HasRows(mouldCategoryId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool Exists(string id)
		{
			return accessor.Exists(id);
		}
		
		public Model.MouldCategory GetById(string id)
		{
			return accessor.GetById(id);
		}
		
		public bool ExistsExcept(Model.MouldCategory e)
		{
			return accessor.ExistsExcept(e);
		}
		public bool HasRowsBefore(Model.MouldCategory e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.MouldCategory e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.MouldCategory GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.MouldCategory GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.MouldCategory GetPrev(Model.MouldCategory e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.MouldCategory GetNext(Model.MouldCategory e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.MouldCategory> Select()
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
		public IList<Model.MouldCategory> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
