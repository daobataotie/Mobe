﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCEarPressCheckManager.autogenerated.cs
// author: mayanjun
// create date：2013-08-23 16:50:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PCEarPressCheckManager
    {
		///<summary>
		/// Data accessor of dbo.PCEarPressCheck
		///</summary>
		private static readonly DA.IPCEarPressCheckAccessor accessor = (DA.IPCEarPressCheckAccessor)Accessors.Get("PCEarPressCheckAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.PCEarPressCheck Get(string pCEarPressCheckId)
		{
			return accessor.Get(pCEarPressCheckId);
		}
		
		public bool HasRows(string pCEarPressCheckId)
		{
			return accessor.HasRows(pCEarPressCheckId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.PCEarPressCheck e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.PCEarPressCheck e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.PCEarPressCheck GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.PCEarPressCheck GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.PCEarPressCheck GetPrev(Model.PCEarPressCheck e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.PCEarPressCheck GetNext(Model.PCEarPressCheck e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.PCEarPressCheck> Select()
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
		public IList<Model.PCEarPressCheck> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }


        public IList<Model.PCEarPressCheck> SelectByDateRage(DateTime StartDate, DateTime EndDate, bool IsReport)
        {
            return accessor.SelectByDateRage(StartDate,EndDate,IsReport);
        }
    }
}
