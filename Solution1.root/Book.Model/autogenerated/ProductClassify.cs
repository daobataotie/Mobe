﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProductClassify.autogenerated.cs
// author: mayanjun
// create date：2017-08-24 21:38:46
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class ProductClassify
	{
		#region Data

		/// <summary>
		/// 
		/// </summary>
		private string _productClassifyId;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _productClassifyDate;
		
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
		private string _keyWord;
		
		/// <summary>
		/// 
		/// </summary>
		private string _employeeId;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		public string ProductClassifyId
		{
			get 
			{
				return this._productClassifyId;
			}
			set 
			{
				this._productClassifyId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime? ProductClassifyDate
		{
			get 
			{
				return this._productClassifyDate;
			}
			set 
			{
				this._productClassifyDate = value;
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
		public string KeyWord
		{
			get 
			{
				return this._keyWord;
			}
			set 
			{
				this._keyWord = value;
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
		public readonly static string PRO_ProductClassifyId = "ProductClassifyId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProductClassifyDate = "ProductClassifyDate";
		
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
		public readonly static string PRO_KeyWord = "KeyWord";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_EmployeeId = "EmployeeId";
		

		#endregion
	}
}