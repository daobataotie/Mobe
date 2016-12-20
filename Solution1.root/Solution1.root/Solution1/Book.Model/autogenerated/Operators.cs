﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：Operators.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:15
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class Operators
	{
		#region Data

		/// <summary>
		/// 操作员编号
		/// </summary>
		private string _id;
		
		/// <summary>
		/// 编号
		/// </summary>
		private string _operatorsId;
		
		/// <summary>
		/// 员工编号
		/// </summary>
		private string _employeeId;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 登陆名
		/// </summary>
		private string _operatorName;
		
		/// <summary>
		/// 登陆密码
		/// </summary>
		private string _password;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee employee;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 操作员编号
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
		/// 编号
		/// </summary>
		public string OperatorsId
		{
			get 
			{
				return this._operatorsId;
			}
			set 
			{
				this._operatorsId = value;
			}
		}

		/// <summary>
		/// 员工编号
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
		/// 登陆名
		/// </summary>
		public string OperatorName
		{
			get 
			{
				return this._operatorName;
			}
			set 
			{
				this._operatorName = value;
			}
		}

		/// <summary>
		/// 登陆密码
		/// </summary>
		public string Password
		{
			get 
			{
				return this._password;
			}
			set 
			{
				this._password = value;
			}
		}
	
		/// <summary>
		/// 员工
		/// </summary>
		public virtual Employee Employee
		{
			get
			{
				return this.employee;
			}
			set
			{
				this.employee = value;
			}
			
		}
		/// <summary>
		/// 操作员编号
		/// </summary>
		public readonly static string PROPERTY_ID = "Id";
		
		/// <summary>
		/// 编号
		/// </summary>
		public readonly static string PROPERTY_OPERATORSID = "OperatorsId";
		
		/// <summary>
		/// 员工编号
		/// </summary>
		public readonly static string PROPERTY_EMPLOYEEID = "EmployeeId";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PROPERTY_INSERTTIME = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PROPERTY_UPDATETIME = "UpdateTime";
		
		/// <summary>
		/// 登陆名
		/// </summary>
		public readonly static string PROPERTY_OPERATORNAME = "OperatorName";
		
		/// <summary>
		/// 登陆密码
		/// </summary>
		public readonly static string PROPERTY_PASSWORD = "Password";
		

		#endregion
	}
}
