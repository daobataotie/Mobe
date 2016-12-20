﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：SalesForHeader.autogenerated.cs
// author: peidun
// create date：2009-12-18 11:23:49
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class SalesForHeader
	{
		#region Data

		/// <summary>
		/// 销售预测单头编号
		/// </summary>
		private string _salesForHeaderId;
		
		/// <summary>
		/// 制表人
		/// </summary>
		private string _employee0Id;
		
		/// <summary>
		/// 预算人
		/// </summary>
		private string _employee1Id;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 表单名称
		/// </summary>
		private string _salesForName;
		
		/// <summary>
		/// 开始日期
		/// </summary>
		private DateTime? _startDate;
		
		/// <summary>
		/// 结束日期
		/// </summary>
		private DateTime? _endDate;
		
		/// <summary>
		/// 制表日期
		/// </summary>
		private DateTime? _salesForDate;
		
		/// <summary>
		/// 表单状态
		/// </summary>
		private int? _state;
		
		/// <summary>
		/// Attribute_781
		/// </summary>
		private string _id;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee0;
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee1;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 销售预测单头编号
		/// </summary>
		public string SalesForHeaderId
		{
			get 
			{
				return this._salesForHeaderId;
			}
			set 
			{
				this._salesForHeaderId = value;
			}
		}

		/// <summary>
		/// 制表人
		/// </summary>
		public string Employee0Id
		{
			get 
			{
				return this._employee0Id;
			}
			set 
			{
				this._employee0Id = value;
			}
		}

		/// <summary>
		/// 预算人
		/// </summary>
		public string Employee1Id
		{
			get 
			{
				return this._employee1Id;
			}
			set 
			{
				this._employee1Id = value;
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
		/// 表单名称
		/// </summary>
		public string SalesForName
		{
			get 
			{
				return this._salesForName;
			}
			set 
			{
				this._salesForName = value;
			}
		}

		/// <summary>
		/// 开始日期
		/// </summary>
		public DateTime? StartDate
		{
			get 
			{
				return this._startDate;
			}
			set 
			{
				this._startDate = value;
			}
		}

		/// <summary>
		/// 结束日期
		/// </summary>
		public DateTime? EndDate
		{
			get 
			{
				return this._endDate;
			}
			set 
			{
				this._endDate = value;
			}
		}

		/// <summary>
		/// 制表日期
		/// </summary>
		public DateTime? SalesForDate
		{
			get 
			{
				return this._salesForDate;
			}
			set 
			{
				this._salesForDate = value;
			}
		}

		/// <summary>
		/// 表单状态
		/// </summary>
		public int? State
		{
			get 
			{
				return this._state;
			}
			set 
			{
				this._state = value;
			}
		}

		/// <summary>
		/// Attribute_781
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
		/// 员工
		/// </summary>
		public virtual Employee Employee0
		{
			get
			{
				return this._employee0;
			}
			set
			{
				this._employee0 = value;
			}
			
		}
		/// <summary>
		/// 员工
		/// </summary>
		public virtual Employee Employee1
		{
			get
			{
				return this._employee1;
			}
			set
			{
				this._employee1 = value;
			}
			
		}
		/// <summary>
		/// 销售预测单头编号
		/// </summary>
		public readonly static string PROPERTY_SALESFORHEADERID = "SalesForHeaderId";
		
		/// <summary>
		/// 制表人
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEE0ID = "Employee0Id";
		
		/// <summary>
		/// 预算人
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEE1ID = "Employee1Id";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PROPERTY_INSERTTIME = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PROPERTY_UPDATETIME = "UpdateTime";
		
		/// <summary>
		/// 表单名称
		/// </summary>
		public readonly static string PROPERTY_SALESFORNAME = "SalesForName";
		
		/// <summary>
		/// 开始日期
		/// </summary>
		public readonly static string PROPERTY_STARTDATE = "StartDate";
		
		/// <summary>
		/// 结束日期
		/// </summary>
		public readonly static string PROPERTY_ENDDATE = "EndDate";
		
		/// <summary>
		/// 制表日期
		/// </summary>
		public readonly static string PROPERTY_SALESFORDATE = "SalesForDate";
		
		/// <summary>
		/// 表单状态
		/// </summary>
		public readonly static string PROPERTY_STATE = "State";
		
		/// <summary>
		/// Attribute_781
		/// </summary>
		public readonly static string PROPERTY_ID = "Id";
		

		#endregion
	}
}
