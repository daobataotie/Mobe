﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ThicknessTestDetailsManager.autogenerated.cs
// author: mayanjun
// create date：2012-4-24 10:33:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ThicknessTestDetailsManager
    {
		///<summary>
		/// Data accessor of dbo.ThicknessTestDetails
		///</summary>
		private static readonly DA.IThicknessTestDetailsAccessor accessor = (DA.IThicknessTestDetailsAccessor)Accessors.Get("ThicknessTestDetailsAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ThicknessTestDetails Get(string thicknessTestDetailsId)
		{
			return accessor.Get(thicknessTestDetailsId);
		}
		
		public bool HasRows(string thicknessTestDetailsId)
		{
			return accessor.HasRows(thicknessTestDetailsId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ThicknessTestDetails> Select()
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
		public IList<Model.ThicknessTestDetails> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
