﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCEarProtectCheckManager.autogenerated.cs
// author: mayanjun
// create date：2013-09-03 15:16:59
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PCEarProtectCheckManager
    {
		///<summary>
		/// Data accessor of dbo.PCEarProtectCheck
		///</summary>
		private static readonly DA.IPCEarProtectCheckAccessor accessor = (DA.IPCEarProtectCheckAccessor)Accessors.Get("PCEarProtectCheckAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.PCEarProtectCheck Get(string pCEarProtectCheckId)
		{
			return accessor.Get(pCEarProtectCheckId);
		}
		
		public bool HasRows(string pCEarProtectCheckId)
		{
			return accessor.HasRows(pCEarProtectCheckId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.PCEarProtectCheck e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.PCEarProtectCheck e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.PCEarProtectCheck GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.PCEarProtectCheck GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.PCEarProtectCheck GetPrev(Model.PCEarProtectCheck e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.PCEarProtectCheck GetNext(Model.PCEarProtectCheck e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.PCEarProtectCheck> Select()
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
		public IList<Model.PCEarProtectCheck> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
