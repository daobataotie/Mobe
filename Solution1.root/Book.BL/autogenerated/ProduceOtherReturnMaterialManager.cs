﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceOtherReturnMaterialManager.autogenerated.cs
// author: mayanjun
// create date：2011-08-31 15:05:10
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProduceOtherReturnMaterialManager
    {
		///<summary>
		/// Data accessor of dbo.ProduceOtherReturnMaterial
		///</summary>
		private static readonly DA.IProduceOtherReturnMaterialAccessor accessor = (DA.IProduceOtherReturnMaterialAccessor)Accessors.Get("ProduceOtherReturnMaterialAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ProduceOtherReturnMaterial Get(string produceOtherReturnMaterialId)
		{
			return accessor.Get(produceOtherReturnMaterialId);
		}
		
		public bool HasRows(string produceOtherReturnMaterialId)
		{
			return accessor.HasRows(produceOtherReturnMaterialId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.ProduceOtherReturnMaterial e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.ProduceOtherReturnMaterial e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.ProduceOtherReturnMaterial GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.ProduceOtherReturnMaterial GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.ProduceOtherReturnMaterial GetPrev(Model.ProduceOtherReturnMaterial e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.ProduceOtherReturnMaterial GetNext(Model.ProduceOtherReturnMaterial e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ProduceOtherReturnMaterial> Select()
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
		public IList<Model.ProduceOtherReturnMaterial> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
