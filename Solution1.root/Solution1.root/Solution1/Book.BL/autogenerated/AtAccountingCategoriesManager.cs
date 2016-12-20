﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AtAccountingCategoriesManager.autogenerated.cs
// author: mayanjun
// create date：2011-3-3 14:30:18
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class AtAccountingCategoriesManager
    {
		///<summary>
		/// Data accessor of dbo.AtAccountingCategories
		///</summary>
		private static readonly DA.IAtAccountingCategoriesAccessor accessor = (DA.IAtAccountingCategoriesAccessor)Accessors.Get("AtAccountingCategoriesAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.AtAccountingCategories Get(string accountingCategoriesId)
		{
			return accessor.Get(accountingCategoriesId);
		}
		
		public bool HasRows(string accountingCategoriesId)
		{
			return accessor.HasRows(accountingCategoriesId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool Exists(string id)
		{
			return accessor.Exists(id);
		}
		
		public Model.AtAccountingCategories GetById(string id)
		{
			return accessor.GetById(id);
		}		
		public bool ExistsExcept(Model.AtAccountingCategories e)
		{
			return accessor.ExistsExcept(e);
		}
		
		
		public bool HasRowsBefore(Model.AtAccountingCategories e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.AtAccountingCategories e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.AtAccountingCategories GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.AtAccountingCategories GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.AtAccountingCategories GetPrev(Model.AtAccountingCategories e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.AtAccountingCategories GetNext(Model.AtAccountingCategories e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.AtAccountingCategories> Select()
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
		public IList<Model.AtAccountingCategories> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		public bool ExistsPrimary(string id)
		{
		    return accessor.ExistsPrimary(id);	
	    }
		
    }
}
