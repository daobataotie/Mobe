﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProductClassifyDetailManager.autogenerated.cs
// author: mayanjun
// create date：2017-08-24 21:38:46
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProductClassifyDetailManager
    {
		///<summary>
		/// Data accessor of dbo.ProductClassifyDetail
		///</summary>
		private static readonly DA.IProductClassifyDetailAccessor accessor = (DA.IProductClassifyDetailAccessor)Accessors.Get("ProductClassifyDetailAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ProductClassifyDetail Get(string productClassifyDetailId)
		{
			return accessor.Get(productClassifyDetailId);
		}
		
		public bool HasRows(string productClassifyDetailId)
		{
			return accessor.HasRows(productClassifyDetailId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ProductClassifyDetail> Select()
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
		public IList<Model.ProductClassifyDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
