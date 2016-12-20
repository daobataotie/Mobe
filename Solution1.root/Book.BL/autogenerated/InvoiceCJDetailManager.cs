﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceCJDetailManager.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class InvoiceCJDetailManager
    {
		///<summary>
		/// Data accessor of dbo.InvoiceCJDetail
		///</summary>
		private static readonly DA.IInvoiceCJDetailAccessor accessor = (DA.IInvoiceCJDetailAccessor)Accessors.Get("InvoiceCJDetailAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.InvoiceCJDetail Get(string invoiceCJDetailId)
		{
			return accessor.Get(invoiceCJDetailId);
		}
		
		public bool HasRows(string invoiceCJDetailId)
		{
			return accessor.HasRows(invoiceCJDetailId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.InvoiceCJDetail> Select()
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
		public IList<Model.InvoiceCJDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
