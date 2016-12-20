﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：BusinessHoursManager.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:01
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class BusinessHoursManager
    {
		///<summary>
		/// Data accessor of dbo.BusinessHours
		///</summary>
		private static readonly DA.IBusinessHoursAccessor accessor = (DA.IBusinessHoursAccessor)Accessors.Get("BusinessHoursAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.BusinessHours Get(string businessHoursId)
		{
			return accessor.Get(businessHoursId);
		}
		
		public bool HasRows(string businessHoursId)
		{
			return accessor.HasRows(businessHoursId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool HasRowsBefore(Model.BusinessHours e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.BusinessHours e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.BusinessHours GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.BusinessHours GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.BusinessHours GetPrev(Model.BusinessHours e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.BusinessHours GetNext(Model.BusinessHours e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.BusinessHours> Select()
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
		public IList<Model.BusinessHours> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
