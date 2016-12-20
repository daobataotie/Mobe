﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceOtherInDepotDetailManager.autogenerated.cs
// author: peidun
// create date：2010-1-8 13:43:35
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProduceOtherInDepotDetailManager
    {
		///<summary>
		/// Data accessor of dbo.ProduceOtherInDepotDetail
		///</summary>
		private static readonly DA.IProduceOtherInDepotDetailAccessor accessor = (DA.IProduceOtherInDepotDetailAccessor)Accessors.Get("ProduceOtherInDepotDetailAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ProduceOtherInDepotDetail Get(string produceOtherInDepotDetailId)
		{
			return accessor.Get(produceOtherInDepotDetailId);
		}
		
		public bool HasRows(string produceOtherInDepotDetailId)
		{
			return accessor.HasRows(produceOtherInDepotDetailId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ProduceOtherInDepotDetail> Select()
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
		public IList<Model.ProduceOtherInDepotDetail> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
