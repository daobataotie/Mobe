﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AssemblySiteDifference.autogenerated.cs
// author: mayanjun
// create date：2018-05-14 19:16:33
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class AssemblySiteDifference
	{
		#region Data

		/// <summary>
		/// 
		/// </summary>
		private string _assemblySiteDifferenceId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _assemblySiteInventoryId;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _invoiceDate;
		
		/// <summary>
		/// 
		/// </summary>
		private string _employeeId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _note;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		public string AssemblySiteDifferenceId
		{
			get 
			{
				return this._assemblySiteDifferenceId;
			}
			set 
			{
				this._assemblySiteDifferenceId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string AssemblySiteInventoryId
		{
			get 
			{
				return this._assemblySiteInventoryId;
			}
			set 
			{
				this._assemblySiteInventoryId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime? InvoiceDate
		{
			get 
			{
				return this._invoiceDate;
			}
			set 
			{
				this._invoiceDate = value;
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
		public readonly static string PRO_AssemblySiteDifferenceId = "AssemblySiteDifferenceId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_AssemblySiteInventoryId = "AssemblySiteInventoryId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InvoiceDate = "InvoiceDate";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_EmployeeId = "EmployeeId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_Note = "Note";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InsertTime = "InsertTime";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_UpdateTime = "UpdateTime";
		

		#endregion
	}
}