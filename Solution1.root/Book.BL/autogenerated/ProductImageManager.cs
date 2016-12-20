﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：productImageManager.autogenerated.cs
// author: mayanjun
// create date：2011-2-25 10:53:26
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProductImageManager
    {
		///<summary>
		/// Data accessor of dbo.productImage
		///</summary>
		private static readonly DA.IProductImageAccessor accessor = (DA.IProductImageAccessor)Accessors.Get("ProductImageAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
        public Model.ProductImage Get(string imageId)
		{
			return accessor.Get(imageId);
		}
		
		public bool HasRows(string imageId)
		{
			return accessor.HasRows(imageId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		/// <summary>
		/// Select all.
		/// </summary>
        public IList<Model.ProductImage> Select()
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
		public IList<Model.ProductImage> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
