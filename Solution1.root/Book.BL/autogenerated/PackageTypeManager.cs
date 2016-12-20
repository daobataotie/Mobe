﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PackageTypeManager.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:02
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PackageTypeManager
    {
		///<summary>
		/// Data accessor of dbo.PackageType
		///</summary>
		private static readonly DA.IPackageTypeAccessor accessor = (DA.IPackageTypeAccessor)Accessors.Get("PackageTypeAccessor");
				
		public bool HasRows(string packageTypeId)
		{
			return accessor.HasRows(packageTypeId);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		public bool Exists(string id)
		{
			return accessor.Exists(id);
		}
		
		public Model.PackageType GetById(string id)
		{
			return accessor.GetById(id);
		}
		
		public bool ExistsExcept(Model.PackageType e)
		{
			return accessor.ExistsExcept(e);
		}
		public bool HasRowsBefore(Model.PackageType e)
		{
			return accessor.HasRowsBefore(e);
		}
		
		public bool HasRowsAfter(Model.PackageType e)
		{
			return accessor.HasRowsAfter(e);
		}
		
		public Model.PackageType GetFirst()
		{
			return accessor.GetFirst();
		}
		
		public Model.PackageType GetLast()
		{
			return accessor.GetLast();
		}
		
		public Model.PackageType GetPrev(Model.PackageType e)
		{
			return accessor.GetPrev(e);
		}
		
		public Model.PackageType GetNext(Model.PackageType e)
		{
			return accessor.GetNext(e);
		}
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.PackageType> Select()
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
		public IList<Model.PackageType> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
