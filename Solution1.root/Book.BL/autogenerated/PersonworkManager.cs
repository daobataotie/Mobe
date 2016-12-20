﻿//------------------------------------------------------------------------------
//
// 说明：该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PersonworkManager.autogenerated.cs
// author: peidun
// create date：2009-11-26 15:16:38
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.BL
{
    public partial class PersonworkManager
    {
		///<summary>
		/// Data accessor of dbo.Personwork
		///</summary>
		private static readonly DA.IPersonworkAccessor accessor = (DA.IPersonworkAccessor)Accessors.Get("PersonworkAccessor");
		
		/// <summary>
		/// Select by primary key.
		/// </summary>		
		public Model.Personwork Get(string personworkID)
		{
			return accessor.Get(personworkID);
		}
		
		public bool HasRows(string personworkID)
		{
			return accessor.HasRows(personworkID);
		}
		
		public bool HasRows()
		{
			return accessor.HasRows();
		}
		
		/// <summary>
		/// Select all.
		/// </summary>
		public IList<Model.Personwork> Select()
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
		public IList<Model.Personwork> Select(Helper.OrderDescription orderDescription, Helper.PagingDescription pagingDescription)
		{
			return accessor.Select(orderDescription, pagingDescription);
		}
		
    }
}
