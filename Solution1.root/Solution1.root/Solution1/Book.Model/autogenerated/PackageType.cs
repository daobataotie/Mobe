﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PackageType.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:15
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class PackageType
	{
		#region Data

		/// <summary>
		/// 编号
		/// </summary>
		private string _id;
		
		/// <summary>
		/// 包装类型编号
		/// </summary>
		private string _packageTypeId;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 包装类型名称
		/// </summary>
		private string _packagePypeName;
		
		/// <summary>
		/// 建立时间
		/// </summary>
		private DateTime? _createDate;
		
		/// <summary>
		/// 说明
		/// </summary>
		private string _description;
		
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 编号
		/// </summary>
		public string Id
		{
			get 
			{
				return this._id;
			}
			set 
			{
				this._id = value;
			}
		}

		/// <summary>
		/// 包装类型编号
		/// </summary>
		public string PackageTypeId
		{
			get 
			{
				return this._packageTypeId;
			}
			set 
			{
				this._packageTypeId = value;
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
		/// 包装类型名称
		/// </summary>
		public string PackagePypeName
		{
			get 
			{
				return this._packagePypeName;
			}
			set 
			{
				this._packagePypeName = value;
			}
		}

		/// <summary>
		/// 建立时间
		/// </summary>
		public DateTime? CreateDate
		{
			get 
			{
				return this._createDate;
			}
			set 
			{
				this._createDate = value;
			}
		}

		/// <summary>
		/// 说明
		/// </summary>
		public string Description
		{
			get 
			{
				return this._description;
			}
			set 
			{
				this._description = value;
			}
		}
	
		/// <summary>
		/// 编号
		/// </summary>
		public readonly static string PROPERTY_ID = "Id";
		
		/// <summary>
		/// 包装类型编号
		/// </summary>
		public readonly static string PROPERTY_PACKAGETYPEID = "PackageTypeId";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PROPERTY_INSERTTIME = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PROPERTY_UPDATETIME = "UpdateTime";
		
		/// <summary>
		/// 包装类型名称
		/// </summary>
		public readonly static string PROPERTY_PACKAGEPYPENAME = "PackagePypeName";
		
		/// <summary>
		/// 建立时间
		/// </summary>
		public readonly static string PROPERTY_CREATEDATE = "CreateDate";
		
		/// <summary>
		/// 说明
		/// </summary>
		public readonly static string PROPERTY_DESCRIPTION = "Description";
		

		#endregion
	}
}
