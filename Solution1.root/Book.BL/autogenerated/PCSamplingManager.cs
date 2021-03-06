﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCSamplingManager.autogenerated.cs
// author: mayanjun
// create date：2015/10/30 17:07:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PCSamplingManager
    {
		///<summary>
		/// Data accessor of dbo.PCSampling
		///</summary>
		private static readonly DA.IPCSamplingAccessor accessor = (DA.IPCSamplingAccessor)Accessors.Get("PCSamplingAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.PCSampling Get(string pCSamplingId)
		{
			return accessor.Get(pCSamplingId);
		}
		
		public bool HasRows(string pCSamplingId)
		{
			return accessor.HasRows(pCSamplingId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.PCSampling e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.PCSampling e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.PCSampling GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.PCSampling GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.PCSampling GetPrev(Model.PCSampling e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.PCSampling GetNext(Model.PCSampling e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.PCSampling> Select()
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
		public IList<Model.PCSampling> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
