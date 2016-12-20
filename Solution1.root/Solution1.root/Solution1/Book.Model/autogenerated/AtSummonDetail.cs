﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AtSummonDetail.autogenerated.cs
// author: mayanjun
// create date：2011-3-3 14:30:30
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class AtSummonDetail
	{
		#region Data

		/// <summary>
		/// 建檔序號
		/// </summary>
		private string _summonDetailId;
		
		/// <summary>
		/// 單據副碼
		/// </summary>
		private int? _billCode;
		
		/// <summary>
		/// 傳票編號
		/// </summary>
		private string _summonId;
		
		/// <summary>
		/// 傳票類別
		/// </summary>
		private string _summonCatetory;
		
		/// <summary>
		/// 借貸
		/// </summary>
		private string _lending;
		
		/// <summary>
		/// 科目編號
		/// </summary>
		private string _subjectId;
		
		/// <summary>
		/// 金額
		/// </summary>
		private decimal? _aMoney;
		
		/// <summary>
		/// 摘要
		/// </summary>
		private string _summary;
		
		/// <summary>
		/// 部門編號
		/// </summary>
		private string _departmentId;
		
		/// <summary>
		/// 專案編號
		/// </summary>
		private string _projectId;
		
		/// <summary>
		/// 沖消傳票
		/// </summary>
		private string _offsettingSummon;
		
		/// <summary>
		/// 借方金額
		/// </summary>
		private decimal? _debitMoney;
		
		/// <summary>
		/// 貸方金額
		/// </summary>
		private decimal? _creditMoney;
		
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
		private string _id;
		
		/// <summary>
		/// 传票主档
		/// </summary>
		private AtSummon _summon;
		/// <summary>
		/// 
		/// </summary>
		private AtAccountSubject _subject;
		/// <summary>
		/// 部门
		/// </summary>
		private Department _department;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 建檔序號
		/// </summary>
		public string SummonDetailId
		{
			get 
			{
				return this._summonDetailId;
			}
			set 
			{
				this._summonDetailId = value;
			}
		}

		/// <summary>
		/// 單據副碼
		/// </summary>
		public int? BillCode
		{
			get 
			{
				return this._billCode;
			}
			set 
			{
				this._billCode = value;
			}
		}

		/// <summary>
		/// 傳票編號
		/// </summary>
		public string SummonId
		{
			get 
			{
				return this._summonId;
			}
			set 
			{
				this._summonId = value;
			}
		}

		/// <summary>
		/// 傳票類別
		/// </summary>
		public string SummonCatetory
		{
			get 
			{
				return this._summonCatetory;
			}
			set 
			{
				this._summonCatetory = value;
			}
		}

		/// <summary>
		/// 借貸
		/// </summary>
		public string Lending
		{
			get 
			{
				return this._lending;
			}
			set 
			{
				this._lending = value;
			}
		}

		/// <summary>
		/// 科目編號
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
		/// 金額
		/// </summary>
		public decimal? AMoney
		{
			get 
			{
				return this._aMoney;
			}
			set 
			{
				this._aMoney = value;
			}
		}

		/// <summary>
		/// 摘要
		/// </summary>
		public string Summary
		{
			get 
			{
				return this._summary;
			}
			set 
			{
				this._summary = value;
			}
		}

		/// <summary>
		/// 部門編號
		/// </summary>
		public string DepartmentId
		{
			get 
			{
				return this._departmentId;
			}
			set 
			{
				this._departmentId = value;
			}
		}

		/// <summary>
		/// 專案編號
		/// </summary>
		public string ProjectId
		{
			get 
			{
				return this._projectId;
			}
			set 
			{
				this._projectId = value;
			}
		}

		/// <summary>
		/// 沖消傳票
		/// </summary>
		public string OffsettingSummon
		{
			get 
			{
				return this._offsettingSummon;
			}
			set 
			{
				this._offsettingSummon = value;
			}
		}

		/// <summary>
		/// 借方金額
		/// </summary>
		public decimal? DebitMoney
		{
			get 
			{
				return this._debitMoney;
			}
			set 
			{
				this._debitMoney = value;
			}
		}

		/// <summary>
		/// 貸方金額
		/// </summary>
		public decimal? CreditMoney
		{
			get 
			{
				return this._creditMoney;
			}
			set 
			{
				this._creditMoney = value;
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
		/// 传票主档
		/// </summary>
		public virtual AtSummon Summon
		{
			get
			{
				return this._summon;
			}
			set
			{
				this._summon = value;
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
		/// 部门
		/// </summary>
		public virtual Department Department
		{
			get
			{
				return this._department;
			}
			set
			{
				this._department = value;
			}
			
		}
		/// <summary>
		/// 建檔序號
		/// </summary>
		public readonly static string PRO_SummonDetailId = "SummonDetailId";
		
		/// <summary>
		/// 單據副碼
		/// </summary>
		public readonly static string PRO_BillCode = "BillCode";
		
		/// <summary>
		/// 傳票編號
		/// </summary>
		public readonly static string PRO_SummonId = "SummonId";
		
		/// <summary>
		/// 傳票類別
		/// </summary>
		public readonly static string PRO_SummonCatetory = "SummonCatetory";
		
		/// <summary>
		/// 借貸
		/// </summary>
		public readonly static string PRO_Lending = "Lending";
		
		/// <summary>
		/// 科目編號
		/// </summary>
		public readonly static string PRO_SubjectId = "SubjectId";
		
		/// <summary>
		/// 金額
		/// </summary>
		public readonly static string PRO_AMoney = "AMoney";
		
		/// <summary>
		/// 摘要
		/// </summary>
		public readonly static string PRO_Summary = "Summary";
		
		/// <summary>
		/// 部門編號
		/// </summary>
		public readonly static string PRO_DepartmentId = "DepartmentId";
		
		/// <summary>
		/// 專案編號
		/// </summary>
		public readonly static string PRO_ProjectId = "ProjectId";
		
		/// <summary>
		/// 沖消傳票
		/// </summary>
		public readonly static string PRO_OffsettingSummon = "OffsettingSummon";
		
		/// <summary>
		/// 借方金額
		/// </summary>
		public readonly static string PRO_DebitMoney = "DebitMoney";
		
		/// <summary>
		/// 貸方金額
		/// </summary>
		public readonly static string PRO_CreditMoney = "CreditMoney";
		
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
		public readonly static string PRO_Id = "Id";
		

		#endregion
	}
}
