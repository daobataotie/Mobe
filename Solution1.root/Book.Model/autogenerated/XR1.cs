﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：XR1.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:18
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class XR1
	{
		#region Data

		/// <summary>
		/// 冲销应收款情况1编号
		/// </summary>
		private string _xR1Id;
		
		/// <summary>
		/// 销售出货单编号
		/// </summary>
		private string _invoiceXSId;
		
		/// <summary>
		/// 收款单据编号
		/// </summary>
		private string _invoiceSKId;
		
		/// <summary>
		/// 冲销应收款情况1冲销金额
		/// </summary>
		private decimal? _xR1Money;
		
		/// <summary>
		/// 收款单
		/// </summary>
		private InvoiceSK invoiceSK;
		/// <summary>
		/// 出库单
		/// </summary>
		private InvoiceXS invoiceXS;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 冲销应收款情况1编号
		/// </summary>
		public string XR1Id
		{
			get 
			{
				return this._xR1Id;
			}
			set 
			{
				this._xR1Id = value;
			}
		}

		/// <summary>
		/// 销售出货单编号
		/// </summary>
		public string InvoiceXSId
		{
			get 
			{
				return this._invoiceXSId;
			}
			set 
			{
				this._invoiceXSId = value;
			}
		}

		/// <summary>
		/// 收款单据编号
		/// </summary>
		public string InvoiceSKId
		{
			get 
			{
				return this._invoiceSKId;
			}
			set 
			{
				this._invoiceSKId = value;
			}
		}

		/// <summary>
		/// 冲销应收款情况1冲销金额
		/// </summary>
		public decimal? XR1Money
		{
			get 
			{
				return this._xR1Money;
			}
			set 
			{
				this._xR1Money = value;
			}
		}
	
		/// <summary>
		/// 收款单
		/// </summary>
		public virtual InvoiceSK InvoiceSK
		{
			get
			{
				return this.invoiceSK;
			}
			set
			{
				this.invoiceSK = value;
			}
			
		}
		/// <summary>
		/// 出库单
		/// </summary>
		public virtual InvoiceXS InvoiceXS
		{
			get
			{
				return this.invoiceXS;
			}
			set
			{
				this.invoiceXS = value;
			}
			
		}
		/// <summary>
		/// 冲销应收款情况1编号
		/// </summary>
		public readonly static string PROPERTY_XR1ID = "XR1Id";
		
		/// <summary>
		/// 销售出货单编号
		/// </summary>
		public readonly static string PROPERTY_INVOICEXSID = "InvoiceXSId";
		
		/// <summary>
		/// 收款单据编号
		/// </summary>
		public readonly static string PROPERTY_INVOICESKID = "InvoiceSKId";
		
		/// <summary>
		/// 冲销应收款情况1冲销金额
		/// </summary>
		public readonly static string PROPERTY_XR1MONEY = "XR1Money";
		

		#endregion
	}
}
