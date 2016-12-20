﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：LunchDetailManager.autogenerated.cs
// author: peidun
// create date：2010-3-26 11:08:16
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class LunchDetailManager
    {
		///<summary>
		/// Data accessor of dbo.LunchDetail
		///</summary>
		private static readonly DA.ILunchDetailAccessor accessor = (DA.ILunchDetailAccessor)Accessors.Get("LunchDetailAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.LunchDetail Get(string lunchDetailId)
		{
			return accessor.Get(lunchDetailId);
		}
		
		public bool HasRows(string lunchDetailId)
		{
			return accessor.HasRows(lunchDetailId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool HasRowsBefore(Model.LunchDetail e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.LunchDetail e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.LunchDetail GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.LunchDetail GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.LunchDetail GetPrev(Model.LunchDetail e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.LunchDetail GetNext(Model.LunchDetail e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.LunchDetail> Select()
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
		public IList<Model.LunchDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
