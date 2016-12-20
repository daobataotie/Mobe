﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PackageManager.autogenerated.cs
// author: peidun
// create date：2009-08-13 11:08:26
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PackageManager
    {
		///<summary>
		/// Data accessor of dbo.Package
		///</summary>
		private static readonly DA.IPackageAccessor accessor = (DA.IPackageAccessor)Accessors.Get("PackageAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.Package Get(string packageId)
		{
			return accessor.Get(packageId);
		}
		
		public bool HasRows(string packageId)
		{
			return accessor.HasRows(packageId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		public bool HasRowsBefore(Model.Package e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.Package e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.Package GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.Package GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.Package GetPrev(Model.Package e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.Package GetNext(Model.Package e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.Package> Select()
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
		public IList<Model.Package> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
