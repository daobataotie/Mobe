﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceFKManager.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class InvoiceFKManager
    {
		///<summary>
		/// Data accessor of dbo.InvoiceFK
		///</summary>
		private static readonly DA.IInvoiceFKAccessor accessor = (DA.IInvoiceFKAccessor)Accessors.Get("InvoiceFKAccessor");
		
		
		public bool HasRows(string invoiceId)
		{
			return accessor.HasRows(invoiceId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool HasRowsBefore(Model.InvoiceFK e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.InvoiceFK e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.InvoiceFK GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.InvoiceFK GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.InvoiceFK GetPrev(Model.InvoiceFK e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.InvoiceFK GetNext(Model.InvoiceFK e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.InvoiceFK> Select()
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
		public IList<Model.InvoiceFK> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
