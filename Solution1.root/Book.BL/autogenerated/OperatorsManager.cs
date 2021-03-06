﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：OperatorsManager.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class OperatorsManager
    {
		///<summary>
		/// Data accessor of dbo.Operators
		///</summary>
		private static readonly DA.IOperatorsAccessor accessor = (DA.IOperatorsAccessor)Accessors.Get("OperatorsAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.Operators Get(string operatorsId)
		{
			return accessor.Get(operatorsId);
		}
		
		public bool HasRows(string operatorsId)
		{
			return accessor.HasRows(operatorsId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool Exists(string id)
		{
			return accessor.Exists(id);
		}
		
		public Model.Operators GetById(string id)
		{
			return accessor.GetById(id);
		}
		
		public bool ExistsExcept(Model.Operators e)
		{
			return accessor.ExistsExcept(e);
		}
		public bool HasRowsBefore(Model.Operators e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.Operators e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.Operators GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.Operators GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.Operators GetPrev(Model.Operators e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.Operators GetNext(Model.Operators e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.Operators> Select()
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
		public IList<Model.Operators> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
