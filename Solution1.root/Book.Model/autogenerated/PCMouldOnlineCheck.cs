﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：PCMouldOnlineCheck.autogenerated.cs
// author: mayanjun
// create date：2015/4/13 上午 10:11:44
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class PCMouldOnlineCheck
	{
		#region Data

		/// <summary>
		/// 
		/// </summary>
		private string _pCMouldOnlineCheckId;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _pCMouldOnlineCheckDate;
		
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
		private string _employeeId;
		
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee;

        private int? _auditState;

        private string _auditEmpId;

        private Employee _auditEmp;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		public string PCMouldOnlineCheckId
		{
			get 
			{
				return this._pCMouldOnlineCheckId;
			}
			set 
			{
				this._pCMouldOnlineCheckId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public DateTime? PCMouldOnlineCheckDate
		{
			get 
			{
				return this._pCMouldOnlineCheckDate;
			}
			set 
			{
				this._pCMouldOnlineCheckDate = value;
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
		/// 
		/// </summary>
		public readonly static string PRO_PCMouldOnlineCheckId = "PCMouldOnlineCheckId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_PCMouldOnlineCheckDate = "PCMouldOnlineCheckDate";
		
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
		public readonly static string PRO_EmployeeId = "EmployeeId";

        public readonly static string PRO_AuditState = "AuditState";

        public readonly static string PRO_AuditEmpId = "AuditEmpId";
		

		#endregion
	}
}