﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceCODetailManager.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class InvoiceCODetailManager
    {
		///<summary>
		/// Data accessor of dbo.InvoiceCODetail
		///</summary>
		private static readonly DA.IInvoiceCODetailAccessor accessor = (DA.IInvoiceCODetailAccessor)Accessors.Get("InvoiceCODetailAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.InvoiceCODetail Get(string invoiceCODetailId)
		{
			return accessor.Get(invoiceCODetailId);
		}
		
		public bool HasRows(string invoiceCODetailId)
		{
			return accessor.HasRows(invoiceCODetailId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.InvoiceCODetail> Select()
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
		public IList<Model.InvoiceCODetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
