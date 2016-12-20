﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcbeginbillReceivable.autogenerated.cs
// author: mayanjun
// create date：2011-12-19 09:22:15
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class AcbeginbillReceivable
	{
		#region Data

		/// <summary>
		/// 主键编号
		/// </summary>
		private string _acbeginbillReceivableId;
		
		/// <summary>
		/// 操作员
		/// </summary>
		private string _employeeId;
		
		/// <summary>
		/// 头编号
		/// </summary>
		private string _atCurrencyCategoryId;
		
		/// <summary>
		/// 审核人
		/// </summary>
		private string _employee1Id;
		
		/// <summary>
		/// 日期
		/// </summary>
		private DateTime? _acbeginbillReceivableDate;
		
		/// <summary>
		/// 备注
		/// </summary>
		private string _acbeginbillReceivableDesc;
		
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
		private int? _auditingState;

        private int? _auditState;

        private string _auditEmpId;

        private Employee _auditEmp;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee;
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee1;
		/// <summary>
		/// 币种类别
		/// </summary>
		private AtCurrencyCategory _atCurrencyCategory;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 主键编号
		/// </summary>
		public string AcbeginbillReceivableId
		{
			get 
			{
				return this._acbeginbillReceivableId;
			}
			set 
			{
				this._acbeginbillReceivableId = value;
			}
		}

		/// <summary>
		/// 操作员
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
		/// 头编号
		/// </summary>
		public string AtCurrencyCategoryId
		{
			get 
			{
				return this._atCurrencyCategoryId;
			}
			set 
			{
				this._atCurrencyCategoryId = value;
			}
		}

		/// <summary>
		/// 审核人
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
		/// 日期
		/// </summary>
		public DateTime? AcbeginbillReceivableDate
		{
			get 
			{
				return this._acbeginbillReceivableDate;
			}
			set 
			{
				this._acbeginbillReceivableDate = value;
			}
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string AcbeginbillReceivableDesc
		{
			get 
			{
				return this._acbeginbillReceivableDesc;
			}
			set 
			{
				this._acbeginbillReceivableDesc = value;
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
		public int? AuditingState
		{
			get 
			{
				return this._auditingState;
			}
			set 
			{
				this._auditingState = value;
			}
		}

        public int? AuditState
        {
            get
            {
                return this._auditState;
            }
            set
            {
                this._auditState = value;
            }
        }

        public virtual string AuditEmpId
        {
            get
            {
                return this._auditEmpId;

            }
            set
            {
                this._auditEmpId = value;
            }
        }

        public virtual Employee AuditEmp
        {
            get
            {
                return this._auditEmp;
            }
            set
            {
                this._auditEmp = value;
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
		/// 币种类别
		/// </summary>
		public virtual AtCurrencyCategory AtCurrencyCategory
		{
			get
			{
				return this._atCurrencyCategory;
			}
			set
			{
				this._atCurrencyCategory = value;
			}
			
		}
		/// <summary>
		/// 主键编号
		/// </summary>
		public readonly static string PRO_AcbeginbillReceivableId = "AcbeginbillReceivableId";
		
		/// <summary>
		/// 操作员
		/// </summary>
		public readonly static string PRO_EmployeeId = "EmployeeId";
		
		/// <summary>
		/// 头编号
		/// </summary>
		public readonly static string PRO_AtCurrencyCategoryId = "AtCurrencyCategoryId";
		
		/// <summary>
		/// 审核人
		/// </summary>
		public readonly static string PRO_Employee1Id = "Employee1Id";
		
		/// <summary>
		/// 日期
		/// </summary>
		public readonly static string PRO_AcbeginbillReceivableDate = "AcbeginbillReceivableDate";
		
		/// <summary>
		/// 备注
		/// </summary>
		public readonly static string PRO_AcbeginbillReceivableDesc = "AcbeginbillReceivableDesc";
		
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
		public readonly static string PRO_AuditingState = "AuditingState";

        public readonly static string PRO_AuditState = "AuditState";

        public readonly static string PRO_AuditEmpId = "AuditEmpId";

		#endregion
	}
}
