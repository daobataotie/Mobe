﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceHCManager.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class InvoiceHCManager
    {
		///<summary>
		/// Data accessor of dbo.InvoiceHC
		///</summary>
		private static readonly DA.IInvoiceHCAccessor accessor = (DA.IInvoiceHCAccessor)Accessors.Get("InvoiceHCAccessor");
		
		
		public bool HasRows(string invoiceId)
		{
			return accessor.HasRows(invoiceId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool HasRowsBefore(Model.InvoiceHC e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.InvoiceHC e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.InvoiceHC GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.InvoiceHC GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.InvoiceHC GetPrev(Model.InvoiceHC e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.InvoiceHC GetNext(Model.InvoiceHC e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.InvoiceHC> Select()
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
		public IList<Model.InvoiceHC> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
