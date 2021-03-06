﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcCollection.autogenerated.cs
// author: mayanjun
// create date：2011-6-23 09:29:23
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class AcCollection
	{
		#region Data

		/// <summary>
		/// 编号
		/// </summary>
		private string _acCollectionId;
		
		/// <summary>
		/// 操作人
		/// </summary>
		private string _employeeId;
		
		/// <summary>
		/// 审核人
		/// </summary>
		private string _employee0Id;
		
		/// <summary>
		/// 科目编号
		/// </summary>
		private string _subjectId;
		
		/// <summary>
		/// 支付方式编号
		/// </summary>
		private string _payMethodId;
		
		/// <summary>
		/// 收款人
		/// </summary>
		private string _employee1Id;
		
		/// <summary>
		/// 客户编号
		/// </summary>
		private string _customerId;
		
		/// <summary>
		/// 币别
		/// </summary>
		private string _atCurrencyCategoryId;
		
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
		private DateTime? _acPaymentDate;
		
		/// <summary>
		/// 银行账号
		/// </summary>
		private string _bankAccount;
		
		/// <summary>
		/// 票据号
		/// </summary>
		private string _billNo;
		
		/// <summary>
		/// 预收余额
		/// </summary>
		private decimal? _advanceCollectionBalance;
		
		/// <summary>
		/// 取用预收款
		/// </summary>
		private decimal? _subscriptionAdvanceCollection;
		
		/// <summary>
		/// 累入预收款
		/// </summary>
		private decimal? _joinAdvanceCollection;
		
		/// <summary>
		/// 实际收款
		/// </summary>
		private decimal? _eealityCollection;
		
		/// <summary>
		/// 本币实际收款
		/// </summary>
		private decimal? _domesticEealityCollection;
		
		/// <summary>
		/// 现金折扣
		/// </summary>
		private decimal? _cashAgio;
		
		/// <summary>
		/// 本币现金折扣
		/// </summary>
		private decimal? _domesticCashAgio;
		
		/// <summary>
		/// 已冲金额
		/// </summary>
		private decimal? _alreadyChargeMoney;
		
		/// <summary>
		/// 本币已冲金额
		/// </summary>
		private decimal? _domesticMayChargeMoney;
		
		/// <summary>
		/// 备注
		/// </summary>
		private string _acDesc;
		
		/// <summary>
		/// 审核状态
		/// </summary>
		private int? _auditingState;
		
		/// <summary>
		/// 单据状态
		/// </summary>
        private int? _acInvoiceState;


        /// <summary>
        /// 审核状态
        /// </summary>
        private int? _auditState;

        /// <summary>
        /// 审核人
        /// </summary>
        private string _auditEmpId;

        /// <summary>
        /// 审核员工
        /// </summary>
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
		/// 员工
		/// </summary>
		private Employee _employee0;
		/// <summary>
		/// 支付方式
		/// </summary>
		private PayMethod _payMethod;
		/// <summary>
		/// 客户
		/// </summary>
		private Customer _customer;
		/// <summary>
		/// 币种类别
		/// </summary>
		private AtCurrencyCategory _atCurrencyCategory;
		/// <summary>
		/// 
		/// </summary>
		private AtAccountSubject _subject;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 编号
		/// </summary>
		public string AcCollectionId
		{
			get 
			{
				return this._acCollectionId;
			}
			set 
			{
				this._acCollectionId = value;
			}
		}

		/// <summary>
		/// 操作人
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
		/// 审核人
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
		/// 科目编号
		/// </summary>
		public string SubjectId
		{
			get 
			{
				return this._subjectId;
			}
			set 
			{
				this._subjectId = value;
			}
		}

		/// <summary>
		/// 支付方式编号
		/// </summary>
		public string PayMethodId
		{
			get 
			{
				return this._payMethodId;
			}
			set 
			{
				this._payMethodId = value;
			}
		}

		/// <summary>
		/// 收款人
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
		/// 客户编号
		/// </summary>
		public string CustomerId
		{
			get 
			{
				return this._customerId;
			}
			set 
			{
				this._customerId = value;
			}
		}

		/// <summary>
		/// 币别
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
		public DateTime? AcPaymentDate
		{
			get 
			{
				return this._acPaymentDate;
			}
			set 
			{
				this._acPaymentDate = value;
			}
		}

		/// <summary>
		/// 银行账号
		/// </summary>
		public string BankAccount
		{
			get 
			{
				return this._bankAccount;
			}
			set 
			{
				this._bankAccount = value;
			}
		}

		/// <summary>
		/// 票据号
		/// </summary>
		public string BillNo
		{
			get 
			{
				return this._billNo;
			}
			set 
			{
				this._billNo = value;
			}
		}

		/// <summary>
		/// 预收余额
		/// </summary>
		public decimal? AdvanceCollectionBalance
		{
			get 
			{
				return this._advanceCollectionBalance;
			}
			set 
			{
				this._advanceCollectionBalance = value;
			}
		}

		/// <summary>
		/// 取用预收款
		/// </summary>
		public decimal? SubscriptionAdvanceCollection
		{
			get 
			{
				return this._subscriptionAdvanceCollection;
			}
			set 
			{
				this._subscriptionAdvanceCollection = value;
			}
		}

		/// <summary>
		/// 累入预收款
		/// </summary>
		public decimal? JoinAdvanceCollection
		{
			get 
			{
				return this._joinAdvanceCollection;
			}
			set 
			{
				this._joinAdvanceCollection = value;
			}
		}

		/// <summary>
		/// 实际收款
		/// </summary>
		public decimal? EealityCollection
		{
			get 
			{
				return this._eealityCollection;
			}
			set 
			{
				this._eealityCollection = value;
			}
		}

		/// <summary>
		/// 本币实际收款
		/// </summary>
		public decimal? DomesticEealityCollection
		{
			get 
			{
				return this._domesticEealityCollection;
			}
			set 
			{
				this._domesticEealityCollection = value;
			}
		}

		/// <summary>
		/// 现金折扣
		/// </summary>
		public decimal? CashAgio
		{
			get 
			{
				return this._cashAgio;
			}
			set 
			{
				this._cashAgio = value;
			}
		}

		/// <summary>
		/// 本币现金折扣
		/// </summary>
		public decimal? DomesticCashAgio
		{
			get 
			{
				return this._domesticCashAgio;
			}
			set 
			{
				this._domesticCashAgio = value;
			}
		}

		/// <summary>
		/// 已冲金额
		/// </summary>
		public decimal? AlreadyChargeMoney
		{
			get 
			{
				return this._alreadyChargeMoney;
			}
			set 
			{
				this._alreadyChargeMoney = value;
			}
		}

		/// <summary>
		/// 本币已冲金额
		/// </summary>
		public decimal? DomesticMayChargeMoney
		{
			get 
			{
				return this._domesticMayChargeMoney;
			}
			set 
			{
				this._domesticMayChargeMoney = value;
			}
		}

		/// <summary>
		/// 备注
		/// </summary>
		public string AcDesc
		{
			get 
			{
				return this._acDesc;
			}
			set 
			{
				this._acDesc = value;
			}
		}

		/// <summary>
		/// 审核状态
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

		/// <summary>
		/// 单据状态
		/// </summary>
        public int? AcInvoiceState
		{
			get 
			{
				return this._acInvoiceState;
			}
			set 
			{
				this._acInvoiceState = value;
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
		/// 支付方式
		/// </summary>
		public virtual PayMethod PayMethod
		{
			get
			{
				return this._payMethod;
			}
			set
			{
				this._payMethod = value;
			}
			
		}
		/// <summary>
		/// 客户
		/// </summary>
		public virtual Customer Customer
		{
			get
			{
				return this._customer;
			}
			set
			{
				this._customer = value;
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
		/// 
		/// </summary>
		public virtual AtAccountSubject Subject
		{
			get
			{
				return this._subject;
			}
			set
			{
				this._subject = value;
			}
			
		}

		/// <summary>
		/// 编号
		/// </summary>
		public readonly static string PRO_AcCollectionId = "AcCollectionId";
		
		/// <summary>
		/// 操作人
		/// </summary>
		public readonly static string PRO_EmployeeId = "EmployeeId";
		
		/// <summary>
		/// 审核人
		/// </summary>
		public readonly static string PRO_Employee0Id = "Employee0Id";
		
		/// <summary>
		/// 科目编号
		/// </summary>
		public readonly static string PRO_SubjectId = "SubjectId";
		
		/// <summary>
		/// 支付方式编号
		/// </summary>
		public readonly static string PRO_PayMethodId = "PayMethodId";
		
		/// <summary>
		/// 收款人
		/// </summary>
		public readonly static string PRO_Employee1Id = "Employee1Id";
		
		/// <summary>
		/// 客户编号
		/// </summary>
		public readonly static string PRO_CustomerId = "CustomerId";
		
		/// <summary>
		/// 币别
		/// </summary>
		public readonly static string PRO_AtCurrencyCategoryId = "AtCurrencyCategoryId";
		
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
		public readonly static string PRO_AcPaymentDate = "AcPaymentDate";
		
		/// <summary>
		/// 银行账号
		/// </summary>
		public readonly static string PRO_BankAccount = "BankAccount";
		
		/// <summary>
		/// 票据号
		/// </summary>
		public readonly static string PRO_BillNo = "BillNo";
		
		/// <summary>
		/// 预收余额
		/// </summary>
		public readonly static string PRO_AdvanceCollectionBalance = "AdvanceCollectionBalance";
		
		/// <summary>
		/// 取用预收款
		/// </summary>
		public readonly static string PRO_SubscriptionAdvanceCollection = "SubscriptionAdvanceCollection";
		
		/// <summary>
		/// 累入预收款
		/// </summary>
		public readonly static string PRO_JoinAdvanceCollection = "JoinAdvanceCollection";
		
		/// <summary>
		/// 实际收款
		/// </summary>
		public readonly static string PRO_EealityCollection = "EealityCollection";
		
		/// <summary>
		/// 本币实际收款
		/// </summary>
		public readonly static string PRO_DomesticEealityCollection = "DomesticEealityCollection";
		
		/// <summary>
		/// 现金折扣
		/// </summary>
		public readonly static string PRO_CashAgio = "CashAgio";
		
		/// <summary>
		/// 本币现金折扣
		/// </summary>
		public readonly static string PRO_DomesticCashAgio = "DomesticCashAgio";
		
		/// <summary>
		/// 已冲金额
		/// </summary>
		public readonly static string PRO_AlreadyChargeMoney = "AlreadyChargeMoney";
		
		/// <summary>
		/// 本币已冲金额
		/// </summary>
		public readonly static string PRO_DomesticMayChargeMoney = "DomesticMayChargeMoney";
		
		/// <summary>
		/// 备注
		/// </summary>
		public readonly static string PRO_AcDesc = "AcDesc";
		
		/// <summary>
		/// 审核状态
		/// </summary>
		public readonly static string PRO_AuditingState = "AuditingState";
		
		/// <summary>
		/// 单据状态
		/// </summary>
		public readonly static string PRO_AcInvoiceState = "AcInvoiceState";

        public readonly static string PRO_AuditState = "AuditState";

        public readonly static string PRO_AuditEmpId = "AuditEmpId";

		#endregion
	}
}
