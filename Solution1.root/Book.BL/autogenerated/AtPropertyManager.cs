﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AtPropertyManager.autogenerated.cs
// author: mayanjun
// create date：2011-3-3 14:30:19
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class AtPropertyManager
    {
		///<summary>
		/// Data accessor of dbo.AtProperty
		///</summary>
		private static readonly DA.IAtPropertyAccessor accessor = (DA.IAtPropertyAccessor)Accessors.Get("AtPropertyAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.AtProperty Get(string propertyId)
		{
			return accessor.Get(propertyId);
		}
		
		public bool HasRows(string propertyId)
		{
			return accessor.HasRows(propertyId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool Exists(string id)
		{
			return accessor.Exists(id);
		}
		
		public Model.AtProperty GetById(string id)
		{
			return accessor.GetById(id);
		}		
		public bool ExistsExcept(Model.AtProperty e)
		{
			return accessor.ExistsExcept(e);
		}
		
		
		public bool HasRowsBefore(Model.AtProperty e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.AtProperty e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.AtProperty GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.AtProperty GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.AtProperty GetPrev(Model.AtProperty e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.AtProperty GetNext(Model.AtProperty e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.AtProperty> Select()
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
		public IList<Model.AtProperty> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
