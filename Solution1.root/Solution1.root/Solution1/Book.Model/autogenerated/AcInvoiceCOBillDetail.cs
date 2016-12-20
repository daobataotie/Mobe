﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AcInvoiceCOBillDetail.autogenerated.cs
// author: mayanjun
// create date：2011-06-27 15:08:17
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class AcInvoiceCOBillDetail
	{
		#region Data

		/// <summary>
		/// 采购发票详细编号
		/// </summary>
		private string _acInvoiceCOBillDetailId;
		
		/// <summary>
		/// 销售发票主键
		/// </summary>
		private string _acInvoiceCOBillId;
		
		/// <summary>
		/// 单据编号
		/// </summary>
		private string _invoiceId;
		
		/// <summary>
		/// 税额
		/// </summary>
		private decimal? _taxRateMoney;
		
		/// <summary>
		/// 合计
		/// </summary>
		private decimal? _heJiMoney;
		
		/// <summary>
		/// 总额
		/// </summary>
		private decimal? _zongMoney;
		
		/// <summary>
		/// 采购发票
		/// </summary>
		private AcInvoiceCOBill _acInvoiceCOBill;
		/// <summary>
		/// 采购订单
		/// </summary>
		private InvoiceCO _invoice;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 采购发票详细编号
		/// </summary>
		public string AcInvoiceCOBillDetailId
		{
			get 
			{
				return this._acInvoiceCOBillDetailId;
			}
			set 
			{
				this._acInvoiceCOBillDetailId = value;
			}
		}

		/// <summary>
		/// 销售发票主键
		/// </summary>
		public string AcInvoiceCOBillId
		{
			get 
			{
				return this._acInvoiceCOBillId;
			}
			set 
			{
				this._acInvoiceCOBillId = value;
			}
		}

		/// <summary>
		/// 单据编号
		/// </summary>
		public string InvoiceId
		{
			get 
			{
				return this._invoiceId;
			}
			set 
			{
				this._invoiceId = value;
			}
		}

		/// <summary>
		/// 税额
		/// </summary>
		public decimal? TaxRateMoney
		{
			get 
			{
				return this._taxRateMoney;
			}
			set 
			{
				this._taxRateMoney = value;
			}
		}

		/// <summary>
		/// 合计
		/// </summary>
		public decimal? HeJiMoney
		{
			get 
			{
				return this._heJiMoney;
			}
			set 
			{
				this._heJiMoney = value;
			}
		}

		/// <summary>
		/// 总额
		/// </summary>
		public decimal? ZongMoney
		{
			get 
			{
				return this._zongMoney;
			}
			set 
			{
				this._zongMoney = value;
			}
		}
	
		/// <summary>
		/// 采购发票
		/// </summary>
		public virtual AcInvoiceCOBill AcInvoiceCOBill
		{
			get
			{
				return this._acInvoiceCOBill;
			}
			set
			{
				this._acInvoiceCOBill = value;
			}
			
		}
		/// <summary>
		/// 采购订单
		/// </summary>
		public virtual InvoiceCO Invoice
		{
			get
			{
				return this._invoice;
			}
			set
			{
				this._invoice = value;
			}
			
		}
		/// <summary>
		/// 采购发票详细编号
		/// </summary>
		public readonly static string PRO_AcInvoiceCOBillDetailId = "AcInvoiceCOBillDetailId";
		
		/// <summary>
		/// 销售发票主键
		/// </summary>
		public readonly static string PRO_AcInvoiceCOBillId = "AcInvoiceCOBillId";
		
		/// <summary>
		/// 单据编号
		/// </summary>
		public readonly static string PRO_InvoiceId = "InvoiceId";
		
		/// <summary>
		/// 税额
		/// </summary>
		public readonly static string PRO_TaxRateMoney = "TaxRateMoney";
		
		/// <summary>
		/// 合计
		/// </summary>
		public readonly static string PRO_HeJiMoney = "HeJiMoney";
		
		/// <summary>
		/// 总额
		/// </summary>
		public readonly static string PRO_ZongMoney = "ZongMoney";
		

		#endregion
	}
}
