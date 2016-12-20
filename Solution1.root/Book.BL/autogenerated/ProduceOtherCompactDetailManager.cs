﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceOtherCompactDetailManager.autogenerated.cs
// author: peidun
// create date：2010-1-4 15:32:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProduceOtherCompactDetailManager
    {
		///<summary>
		/// Data accessor of dbo.ProduceOtherCompactDetail
		///</summary>
		private static readonly DA.IProduceOtherCompactDetailAccessor accessor = (DA.IProduceOtherCompactDetailAccessor)Accessors.Get("ProduceOtherCompactDetailAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ProduceOtherCompactDetail Get(string otherCompactDetailId)
		{
			return accessor.Get(otherCompactDetailId);
		}
		
		public bool HasRows(string otherCompactDetailId)
		{
			return accessor.HasRows(otherCompactDetailId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ProduceOtherCompactDetail> Select()
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
		public IList<Model.ProduceOtherCompactDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
