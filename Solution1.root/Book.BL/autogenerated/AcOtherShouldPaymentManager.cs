﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcOtherShouldPaymentManager.autogenerated.cs
// author: mayanjun
// create date：2011-6-10 10:37:55
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class AcOtherShouldPaymentManager
    {
		///<summary>
		/// Data accessor of dbo.AcOtherShouldPayment
		///</summary>
		private static readonly DA.IAcOtherShouldPaymentAccessor accessor = (DA.IAcOtherShouldPaymentAccessor)Accessors.Get("AcOtherShouldPaymentAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.AcOtherShouldPayment Get(string acOtherShouldPaymentId)
		{
			return accessor.Get(acOtherShouldPaymentId);
		}
		
		public bool HasRows(string acOtherShouldPaymentId)
		{
			return accessor.HasRows(acOtherShouldPaymentId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.AcOtherShouldPayment e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.AcOtherShouldPayment e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.AcOtherShouldPayment GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.AcOtherShouldPayment GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.AcOtherShouldPayment GetPrev(Model.AcOtherShouldPayment e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.AcOtherShouldPayment GetNext(Model.AcOtherShouldPayment e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.AcOtherShouldPayment> Select()
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
		public IList<Model.AcOtherShouldPayment> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
