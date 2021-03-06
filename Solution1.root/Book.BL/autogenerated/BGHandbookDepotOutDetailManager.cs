﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：BGHandbookDepotOutDetailManager.autogenerated.cs
// author: mayanjun
// create date：2014/3/5 16:32:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class BGHandbookDepotOutDetailManager
    {
		///<summary>
		/// Data accessor of dbo.BGHandbookDepotOutDetail
		///</summary>
		private static readonly DA.IBGHandbookDepotOutDetailAccessor accessor = (DA.IBGHandbookDepotOutDetailAccessor)Accessors.Get("BGHandbookDepotOutDetailAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.BGHandbookDepotOutDetail Get(string bGHandbookDepotOutDetailId)
		{
			return accessor.Get(bGHandbookDepotOutDetailId);
		}
		
		public bool HasRows(string bGHandbookDepotOutDetailId)
		{
			return accessor.HasRows(bGHandbookDepotOutDetailId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.BGHandbookDepotOutDetail> Select()
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
		public IList<Model.BGHandbookDepotOutDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
