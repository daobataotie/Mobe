﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ANSIPCImpactCheckManager.autogenerated.cs
// author: mayanjun
// create date：2011-11-23 16:59:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ANSIPCImpactCheckManager
    {
		///<summary>
		/// Data accessor of dbo.ANSIPCImpactCheck
		///</summary>
		private static readonly DA.IANSIPCImpactCheckAccessor accessor = (DA.IANSIPCImpactCheckAccessor)Accessors.Get("ANSIPCImpactCheckAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ANSIPCImpactCheck Get(string aNSIPCImpactCheckID)
		{
			return accessor.Get(aNSIPCImpactCheckID);
		}
		
		public bool HasRows(string aNSIPCImpactCheckID)
		{
			return accessor.HasRows(aNSIPCImpactCheckID);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.ANSIPCImpactCheck e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.ANSIPCImpactCheck e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.ANSIPCImpactCheck GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.ANSIPCImpactCheck GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.ANSIPCImpactCheck GetPrev(Model.ANSIPCImpactCheck e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.ANSIPCImpactCheck GetNext(Model.ANSIPCImpactCheck e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ANSIPCImpactCheck> Select()
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
		public IList<Model.ANSIPCImpactCheck> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
