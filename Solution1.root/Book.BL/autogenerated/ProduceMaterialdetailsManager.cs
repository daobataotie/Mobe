﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceMaterialdetailsManager.autogenerated.cs
// author: peidun
// create date：2009-12-30 16:37:29
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProduceMaterialdetailsManager
    {
		///<summary>
		/// Data accessor of dbo.ProduceMaterialdetails
		///</summary>
		private static readonly DA.IProduceMaterialdetailsAccessor accessor = (DA.IProduceMaterialdetailsAccessor)Accessors.Get("ProduceMaterialdetailsAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ProduceMaterialdetails Get(string produceMaterialdetailsID)
		{
			return accessor.Get(produceMaterialdetailsID);
		}
		
		public bool HasRows(string produceMaterialdetailsID)
		{
			return accessor.HasRows(produceMaterialdetailsID);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ProduceMaterialdetails> Select()
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
		public IList<Model.ProduceMaterialdetails> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
