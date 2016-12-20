﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：RalationProductMouldManager.autogenerated.cs
// author: peidun
// create date：2009-08-03 10:49:37
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class RalationProductMouldManager
    {
		///<summary>
		/// Data accessor of dbo.RalationProductMould
		///</summary>
		private static readonly DA.IRalationProductMouldAccessor accessor = (DA.IRalationProductMouldAccessor)Accessors.Get("RalationProductMouldAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.RalationProductMould Get(string primaryKeyId)
		{
			return accessor.Get(primaryKeyId);
		}
		
		public bool HasRows(string primaryKeyId)
		{
			return accessor.HasRows(primaryKeyId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.RalationProductMould> Select()
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
		public IList<Model.RalationProductMould> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
