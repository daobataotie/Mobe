﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCFlameRetardantManager.autogenerated.cs
// author: mayanjun
// create date：2018/12/27 13:18:23
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PCFlameRetardantManager
    {
		///<summary>
		/// Data accessor of dbo.PCFlameRetardant
		///</summary>
		private static readonly DA.IPCFlameRetardantAccessor accessor = (DA.IPCFlameRetardantAccessor)Accessors.Get("PCFlameRetardantAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.PCFlameRetardant Get(string pCFlameRetardantId)
		{
			return accessor.Get(pCFlameRetardantId);
		}
		
		public bool HasRows(string pCFlameRetardantId)
		{
			return accessor.HasRows(pCFlameRetardantId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.PCFlameRetardant e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.PCFlameRetardant e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.PCFlameRetardant GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.PCFlameRetardant GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.PCFlameRetardant GetPrev(Model.PCFlameRetardant e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.PCFlameRetardant GetNext(Model.PCFlameRetardant e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.PCFlameRetardant> Select()
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
		public IList<Model.PCFlameRetardant> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}