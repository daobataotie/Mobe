﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCIncomingCheck.autogenerated.cs
// author: mayanjun
// create date：2015/11/8 20:10:11
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class PCIncomingCheck
	{
		#region Data

		/// <summary>
		/// 
		/// </summary>
		private string _pCIncomingCheckId;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _purchaseDate;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _incomingDate;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _checkDate;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 
		/// </summary>
		private string _materialCategory;
		
		/// <summary>
		/// 
		/// </summary>
		private string _employeeId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _note;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		public string PCIncomingCheckId
		{
			get 
			{
				return this._pCIncomingCheckId;
			}
			set 
			{
				this._pCIncomingCheckId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PurchaseDate
		{
			get 
			{
				return this._purchaseDate;
			}
			set 
			{
				this._purchaseDate = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime? IncomingDate
		{
			get 
			{
				return this._incomingDate;
			}
			set 
			{
				this._incomingDate = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CheckDate
		{
			get 
			{
				return this._checkDate;
			}
			set 
			{
				this._checkDate = value;
			}
		}

		/// <summary>
		/// 
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
		/// 
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
		/// 
		/// </summary>
		public string MaterialCategory
		{
			get 
			{
				return this._materialCategory;
			}
			set 
			{
				this._materialCategory = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string EmployeeId
		{
			get 
			{
				return this._employeeId;
			}
			set 
			{
				this._employeeId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Note
		{
			get 
			{
				return this._note;
			}
			set 
			{
				this._note = value;
			}
		}
	
		/// <summary>
		/// 员工
		/// </summary>
		public virtual Employee Employee
		{
			get
			{
				return this._employee;
			}
			set
			{
				this._employee = value;
			}
			
		}
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_PCIncomingCheckId = "PCIncomingCheckId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_PurchaseDate = "PurchaseDate";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_IncomingDate = "IncomingDate";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_CheckDate = "CheckDate";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InsertTime = "InsertTime";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_UpdateTime = "UpdateTime";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_MaterialCategory = "MaterialCategory";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_EmployeeId = "EmployeeId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_Note = "Note";
		

		#endregion
	}
}