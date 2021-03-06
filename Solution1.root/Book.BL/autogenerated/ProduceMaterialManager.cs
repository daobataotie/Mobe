﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceMaterialManager.autogenerated.cs
// author: mayanjun
// create date：2011-11-10 11:15:24
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProduceMaterialManager
    {
		///<summary>
		/// Data accessor of dbo.ProduceMaterial
		///</summary>
		private static readonly DA.IProduceMaterialAccessor accessor = (DA.IProduceMaterialAccessor)Accessors.Get("ProduceMaterialAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ProduceMaterial Get(string produceMaterialID)
		{
			return accessor.Get(produceMaterialID);
		}
		
		public bool HasRows(string produceMaterialID)
		{
			return accessor.HasRows(produceMaterialID);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.ProduceMaterial e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.ProduceMaterial e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.ProduceMaterial GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.ProduceMaterial GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.ProduceMaterial GetPrev(Model.ProduceMaterial e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.ProduceMaterial GetNext(Model.ProduceMaterial e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ProduceMaterial> Select()
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
		public IList<Model.ProduceMaterial> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
