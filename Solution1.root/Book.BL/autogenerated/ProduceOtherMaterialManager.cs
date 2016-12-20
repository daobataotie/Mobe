﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceOtherMaterialManager.autogenerated.cs
// author: peidun
// create date：2010-1-5 15:36:45
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProduceOtherMaterialManager
    {
		///<summary>
		/// Data accessor of dbo.ProduceOtherMaterial
		///</summary>
		private static readonly DA.IProduceOtherMaterialAccessor accessor = (DA.IProduceOtherMaterialAccessor)Accessors.Get("ProduceOtherMaterialAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ProduceOtherMaterial Get(string produceOtherMaterialId)
		{
			return accessor.Get(produceOtherMaterialId);
		}
		
		public bool HasRows(string produceOtherMaterialId)
		{
			return accessor.HasRows(produceOtherMaterialId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool HasRowsBefore(Model.ProduceOtherMaterial e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.ProduceOtherMaterial e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.ProduceOtherMaterial GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.ProduceOtherMaterial GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.ProduceOtherMaterial GetPrev(Model.ProduceOtherMaterial e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.ProduceOtherMaterial GetNext(Model.ProduceOtherMaterial e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ProduceOtherMaterial> Select()
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
		public IList<Model.ProduceOtherMaterial> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
