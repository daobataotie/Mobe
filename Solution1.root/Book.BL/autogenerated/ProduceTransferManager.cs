﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceTransferManager.autogenerated.cs
// author: mayanjun
// create date：2011-4-6 10:53:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class ProduceTransferManager
    {
		///<summary>
		/// Data accessor of dbo.ProduceTransfer
		///</summary>
		private static readonly DA.IProduceTransferAccessor accessor = (DA.IProduceTransferAccessor)Accessors.Get("ProduceTransferAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.ProduceTransfer Get(string produceTransferId)
		{
			return accessor.Get(produceTransferId);
		}
		
		public bool HasRows(string produceTransferId)
		{
			return accessor.HasRows(produceTransferId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		
		public bool HasRowsBefore(Model.ProduceTransfer e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.ProduceTransfer e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.ProduceTransfer GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.ProduceTransfer GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.ProduceTransfer GetPrev(Model.ProduceTransfer e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.ProduceTransfer GetNext(Model.ProduceTransfer e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.ProduceTransfer> Select()
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
		public IList<Model.ProduceTransfer> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
