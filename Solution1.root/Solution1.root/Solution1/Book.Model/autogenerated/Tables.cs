﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：Tables.autogenerated.cs
// author: peidun
// create date：2009-12-11 14:53:06
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class Tables
	{
		#region Data

		/// <summary>
		/// 表单编号
		/// </summary>
		private string _tablesID;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 表单名
		/// </summary>
		private string _tablename;
		
		/// <summary>
		/// 表名
		/// </summary>
		private string _tableCode;
		
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 表单编号
		/// </summary>
		public string TablesID
		{
			get 
			{
				return this._tablesID;
			}
			set 
			{
				this._tablesID = value;
			}
		}

		/// <summary>
		/// 插入时间
		/// </summary>
		public DateTime? InsertTime
		{
			get 
			{
				return this._insertTime;
			}
			set 
			{
				this._insertTime = value;
			}
		}

		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime? UpdateTime
		{
			get 
			{
				return this._updateTime;
			}
			set 
			{
				this._updateTime = value;
			}
		}

		/// <summary>
		/// 表单名
		/// </summary>
		public string Tablename
		{
			get 
			{
				return this._tablename;
			}
			set 
			{
				this._tablename = value;
			}
		}

		/// <summary>
		/// 表名
		/// </summary>
		public string TableCode
		{
			get 
			{
				return this._tableCode;
			}
			set 
			{
				this._tableCode = value;
			}
		}
	
		/// <summary>
		/// 表单编号
		/// </summary>
		public readonly static string PROPERTY_TABLESID = "TablesID";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PROPERTY_INSERTTIME = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PROPERTY_UPDATETIME = "UpdateTime";
		
		/// <summary>
		/// 表单名
		/// </summary>
		public readonly static string PROPERTY_TABLENAME = "Tablename";
		
		/// <summary>
		/// 表名
		/// </summary>
		public readonly static string PROPERTY_TABLECODE = "TableCode";
		

		#endregion
	}
}
