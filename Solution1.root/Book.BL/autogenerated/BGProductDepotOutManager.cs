﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：BGProductDepotOutManager.autogenerated.cs
// author: mayanjun
// create date：2014/3/25 18:18:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class BGProductDepotOutManager
    {
		///<summary>
		/// Data accessor of dbo.BGProductDepotOut
		///</summary>
		private static readonly DA.IBGProductDepotOutAccessor accessor = (DA.IBGProductDepotOutAccessor)Accessors.Get("BGProductDepotOutAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.BGProductDepotOut Get(string bGProductDepotOutId)
		{
			return accessor.Get(bGProductDepotOutId);
		}
		
		public bool HasRows(string bGProductDepotOutId)
		{
			return accessor.HasRows(bGProductDepotOutId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.BGProductDepotOut e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.BGProductDepotOut e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.BGProductDepotOut GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.BGProductDepotOut GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.BGProductDepotOut GetPrev(Model.BGProductDepotOut e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.BGProductDepotOut GetNext(Model.BGProductDepotOut e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.BGProductDepotOut> Select()
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
		public IList<Model.BGProductDepotOut> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
