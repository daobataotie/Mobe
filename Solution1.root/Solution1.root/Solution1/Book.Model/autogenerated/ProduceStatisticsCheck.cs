﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceStatisticsCheck.autogenerated.cs
// author: mayanjun
// create date：2011-07-22 10:44:55
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class ProduceStatisticsCheck
	{
		#region Data

		/// <summary>
		/// 头主键
		/// </summary>
		private string _produceStatisticsCheckId;
		
		/// <summary>
		/// 制单人
		/// </summary>
		private string _employee0Id;
		
		/// <summary>
		/// 修改人
		/// </summary>
		private string _employee1Id;
		
		/// <summary>
		/// 加工单编号
		/// </summary>
		private string _pronoteHeaderID;
		
		/// <summary>
		/// 描述
		/// </summary>
		private string _description;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 日期
		/// </summary>
		private DateTime? _produceStatisticsCheckDate;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee0;
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee1;
		/// <summary>
		/// 生产通知头
		/// </summary>
		private PronoteHeader _pronoteHeader;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 头主键
		/// </summary>
		public string ProduceStatisticsCheckId
		{
			get 
			{
				return this._produceStatisticsCheckId;
			}
			set 
			{
				this._produceStatisticsCheckId = value;
			}
		}

		/// <summary>
		/// 制单人
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
		/// 修改人
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
		/// 加工单编号
		/// </summary>
		public string PronoteHeaderID
		{
			get 
			{
				return this._pronoteHeaderID;
			}
			set 
			{
				this._pronoteHeaderID = value;
			}
		}

		/// <summary>
		/// 描述
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
		/// 日期
		/// </summary>
		public DateTime? ProduceStatisticsCheckDate
		{
			get 
			{
				return this._produceStatisticsCheckDate;
			}
			set 
			{
				this._produceStatisticsCheckDate = value;
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
		/// 生产通知头
		/// </summary>
		public virtual PronoteHeader PronoteHeader
		{
			get
			{
				return this._pronoteHeader;
			}
			set
			{
				this._pronoteHeader = value;
			}
			
		}
		/// <summary>
		/// 头主键
		/// </summary>
		public readonly static string PRO_ProduceStatisticsCheckId = "ProduceStatisticsCheckId";
		
		/// <summary>
		/// 制单人
		/// </summary>
		public readonly static string PRO_Employee0Id = "Employee0Id";
		
		/// <summary>
		/// 修改人
		/// </summary>
		public readonly static string PRO_Employee1Id = "Employee1Id";
		
		/// <summary>
		/// 加工单编号
		/// </summary>
		public readonly static string PRO_PronoteHeaderID = "PronoteHeaderID";
		
		/// <summary>
		/// 描述
		/// </summary>
		public readonly static string PRO_Description = "Description";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PRO_InsertTime = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PRO_UpdateTime = "UpdateTime";
		
		/// <summary>
		/// 日期
		/// </summary>
		public readonly static string PRO_ProduceStatisticsCheckDate = "ProduceStatisticsCheckDate";
		

		#endregion
	}
}
