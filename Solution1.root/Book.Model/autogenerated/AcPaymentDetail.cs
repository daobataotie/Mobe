﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcPaymentDetail.autogenerated.cs
// author: mayanjun
// create date：2011-6-23 09:29:26
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class AcPaymentDetail
	{
		#region Data

		/// <summary>
		/// 详细编号
		/// </summary>
		private string _acPaymentDetailId;
		
		/// <summary>
		/// 编号
		/// </summary>
		private string _acPaymentId;
		
		/// <summary>
		/// 单据类别
		/// </summary>
		private string _acInvoiceType;
		
		/// <summary>
		/// 单据编号
		/// </summary>
		private string _acInvoiceId;
		
		/// <summary>
		/// 应付金额
		/// </summary>
		private decimal? _shouldPaymentMoney;
		
		/// <summary>
		/// 未付金额
		/// </summary>
		private decimal? _noPaymentMoney;
		
		/// <summary>
		/// 本次冲销金额
		/// </summary>
		private decimal? _thisChargeMoney;
		
		/// <summary>
		/// 现金折扣
		/// </summary>
		private decimal? _detailCashAgio;
		
		/// <summary>
		/// 可冲销金额
		/// </summary>
		private decimal? _mayChargeMoney;
		
		/// <summary>
		/// 发票编号
		/// </summary>
		private string _billId;
		
		/// <summary>
		/// 本币应付金额
		/// </summary>
		private decimal? _domesticShouldPaymentMoney;
		
		/// <summary>
		/// 本币未付金额
		/// </summary>
		private decimal? _domesticNoPaymentMoney;
		
		/// <summary>
		/// 本币本次冲销金额
		/// </summary>
		private decimal? _domesticThisChargeMoney;
		
		/// <summary>
		/// 本币现金折扣
		/// </summary>
		private decimal? _domesticDetailCashAgio;
		
		/// <summary>
		/// 本币可冲销金额
		/// </summary>
		private decimal? _domesticMayChargeMoney;
		
		/// <summary>
		/// 付款结算
		/// </summary>
		private AcPayment _acPayment;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 详细编号
		/// </summary>
		public string AcPaymentDetailId
		{
			get 
			{
				return this._acPaymentDetailId;
			}
			set 
			{
				this._acPaymentDetailId = value;
			}
		}

		/// <summary>
		/// 编号
		/// </summary>
		public string AcPaymentId
		{
			get 
			{
				return this._acPaymentId;
			}
			set 
			{
				this._acPaymentId = value;
			}
		}

		/// <summary>
		/// 单据类别
		/// </summary>
		public string AcInvoiceType
		{
			get 
			{
				return this._acInvoiceType;
			}
			set 
			{
				this._acInvoiceType = value;
			}
		}

		/// <summary>
		/// 单据编号
		/// </summary>
		public string AcInvoiceId
		{
			get 
			{
				return this._acInvoiceId;
			}
			set 
			{
				this._acInvoiceId = value;
			}
		}

		/// <summary>
		/// 应付金额
		/// </summary>
		public decimal? ShouldPaymentMoney
		{
			get 
			{
				return this._shouldPaymentMoney;
			}
			set 
			{
				this._shouldPaymentMoney = value;
			}
		}

		/// <summary>
		/// 未付金额
		/// </summary>
		public decimal? NoPaymentMoney
		{
			get 
			{
				return this._noPaymentMoney;
			}
			set 
			{
				this._noPaymentMoney = value;
			}
		}

		/// <summary>
		/// 本次冲销金额
		/// </summary>
		public decimal? ThisChargeMoney
		{
			get 
			{
				return this._thisChargeMoney;
			}
			set 
			{
				this._thisChargeMoney = value;
			}
		}

		/// <summary>
		/// 现金折扣
		/// </summary>
		public decimal? DetailCashAgio
		{
			get 
			{
				return this._detailCashAgio;
			}
			set 
			{
				this._detailCashAgio = value;
			}
		}

		/// <summary>
		/// 可冲销金额
		/// </summary>
		public decimal? MayChargeMoney
		{
			get 
			{
				return this._mayChargeMoney;
			}
			set 
			{
				this._mayChargeMoney = value;
			}
		}

		/// <summary>
		/// 发票编号
		/// </summary>
		public string BillId
		{
			get 
			{
				return this._billId;
			}
			set 
			{
				this._billId = value;
			}
		}

		/// <summary>
		/// 本币应付金额
		/// </summary>
		public decimal? DomesticShouldPaymentMoney
		{
			get 
			{
				return this._domesticShouldPaymentMoney;
			}
			set 
			{
				this._domesticShouldPaymentMoney = value;
			}
		}

		/// <summary>
		/// 本币未付金额
		/// </summary>
		public decimal? DomesticNoPaymentMoney
		{
			get 
			{
				return this._domesticNoPaymentMoney;
			}
			set 
			{
				this._domesticNoPaymentMoney = value;
			}
		}

		/// <summary>
		/// 本币本次冲销金额
		/// </summary>
		public decimal? DomesticThisChargeMoney
		{
			get 
			{
				return this._domesticThisChargeMoney;
			}
			set 
			{
				this._domesticThisChargeMoney = value;
			}
		}

		/// <summary>
		/// 本币现金折扣
		/// </summary>
		public decimal? DomesticDetailCashAgio
		{
			get 
			{
				return this._domesticDetailCashAgio;
			}
			set 
			{
				this._domesticDetailCashAgio = value;
			}
		}

		/// <summary>
		/// 本币可冲销金额
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
		/// 付款结算
		/// </summary>
		public virtual AcPayment AcPayment
		{
			get
			{
				return this._acPayment;
			}
			set
			{
				this._acPayment = value;
			}
			
		}
		/// <summary>
		/// 详细编号
		/// </summary>
		public readonly static string PRO_AcPaymentDetailId = "AcPaymentDetailId";
		
		/// <summary>
		/// 编号
		/// </summary>
		public readonly static string PRO_AcPaymentId = "AcPaymentId";
		
		/// <summary>
		/// 单据类别
		/// </summary>
		public readonly static string PRO_AcInvoiceType = "AcInvoiceType";
		
		/// <summary>
		/// 单据编号
		/// </summary>
		public readonly static string PRO_AcInvoiceId = "AcInvoiceId";
		
		/// <summary>
		/// 应付金额
		/// </summary>
		public readonly static string PRO_ShouldPaymentMoney = "ShouldPaymentMoney";
		
		/// <summary>
		/// 未付金额
		/// </summary>
		public readonly static string PRO_NoPaymentMoney = "NoPaymentMoney";
		
		/// <summary>
		/// 本次冲销金额
		/// </summary>
		public readonly static string PRO_ThisChargeMoney = "ThisChargeMoney";
		
		/// <summary>
		/// 现金折扣
		/// </summary>
		public readonly static string PRO_DetailCashAgio = "DetailCashAgio";
		
		/// <summary>
		/// 可冲销金额
		/// </summary>
		public readonly static string PRO_MayChargeMoney = "MayChargeMoney";
		
		/// <summary>
		/// 发票编号
		/// </summary>
		public readonly static string PRO_BillId = "BillId";
		
		/// <summary>
		/// 本币应付金额
		/// </summary>
		public readonly static string PRO_DomesticShouldPaymentMoney = "DomesticShouldPaymentMoney";
		
		/// <summary>
		/// 本币未付金额
		/// </summary>
		public readonly static string PRO_DomesticNoPaymentMoney = "DomesticNoPaymentMoney";
		
		/// <summary>
		/// 本币本次冲销金额
		/// </summary>
		public readonly static string PRO_DomesticThisChargeMoney = "DomesticThisChargeMoney";
		
		/// <summary>
		/// 本币现金折扣
		/// </summary>
		public readonly static string PRO_DomesticDetailCashAgio = "DomesticDetailCashAgio";
		
		/// <summary>
		/// 本币可冲销金额
		/// </summary>
		public readonly static string PRO_DomesticMayChargeMoney = "DomesticMayChargeMoney";
		

		#endregion
	}
}
