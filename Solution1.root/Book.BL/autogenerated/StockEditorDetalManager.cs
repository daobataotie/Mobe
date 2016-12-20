﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：StockEditorDetalManager.autogenerated.cs
// author: mayanjun
// create date：2010-11-4 11:02:31
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class StockEditorDetalManager
    {
		///<summary>
		/// Data accessor of dbo.StockEditorDetal
		///</summary>
		private static readonly DA.IStockEditorDetalAccessor accessor = (DA.IStockEditorDetalAccessor)Accessors.Get("StockEditorDetalAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.StockEditorDetal Get(string stockEditorDetalId)
		{
			return accessor.Get(stockEditorDetalId);
		}
		
		public bool HasRows(string stockEditorDetalId)
		{
			return accessor.HasRows(stockEditorDetalId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.StockEditorDetal> Select()
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
		public IList<Model.StockEditorDetal> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
