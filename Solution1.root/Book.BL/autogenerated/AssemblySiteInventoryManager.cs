﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AssemblySiteInventoryManager.autogenerated.cs
// author: mayanjun
// create date：2018-05-14 19:16:32
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class AssemblySiteInventoryManager
    {
		///<summary>
		/// Data accessor of dbo.AssemblySiteInventory
		///</summary>
		private static readonly DA.IAssemblySiteInventoryAccessor accessor = (DA.IAssemblySiteInventoryAccessor)Accessors.Get("AssemblySiteInventoryAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.AssemblySiteInventory Get(string assemblySiteInventoryId)
		{
			return accessor.Get(assemblySiteInventoryId);
		}
		
		public bool HasRows(string assemblySiteInventoryId)
		{
			return accessor.HasRows(assemblySiteInventoryId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.AssemblySiteInventory e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.AssemblySiteInventory e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.AssemblySiteInventory GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.AssemblySiteInventory GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.AssemblySiteInventory GetPrev(Model.AssemblySiteInventory e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.AssemblySiteInventory GetNext(Model.AssemblySiteInventory e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.AssemblySiteInventory> Select()
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
		public IList<Model.AssemblySiteInventory> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}