﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceCTDetail.autogenerated.cs
// author: mayanjun
// create date：2011-06-28 15:05:16
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class InvoiceCTDetail
	{
		#region Data

		/// <summary>
		/// 进货退货单货品编号
		/// </summary>
		private string _invoiceCTDetailId;
		
		/// <summary>
		/// 位置编号
		/// </summary>
		private string _depotPositionId;
		
		/// <summary>
		/// 商品编号
		/// </summary>
		private string _productId;
		
		/// <summary>
		/// 单据编号
		/// </summary>
		private string _invoiceId;
		
		/// <summary>
		/// 进货退货单货品数量
		/// </summary>
		private double? _invoiceCTDetailQuantity;
		
		/// <summary>
		/// 进货退货单货品单价
		/// </summary>
		private decimal? _invoiceCTDetailPrice;
		
		/// <summary>
		/// 进货退货单货品折扣率
		/// </summary>
		private double? _invoiceCTDetailDiscountRate;
		
		/// <summary>
		/// 进货退货单货品赠送否
		/// </summary>
		private bool? _invoiceCTDetailZS;
		
		/// <summary>
		/// 进货退货??货品税率
		/// </summary>
		private double? _invoiceCTDetailTaxRate;
		
		/// <summary>
		/// 进货退货单货品折扣额
		/// </summary>
		private decimal? _invoiceCTDetailDisount;
		
		/// <summary>
		/// 进货退货单货品应付款额
		/// </summary>
		private decimal? _invoiceCTDetailMoney0;
		
		/// <summary>
		/// 进货退货单???品税额
		/// </summary>
		private decimal? _invoiceCTDetailTax;
		
		/// <summary>
		/// 进货退货单货品备注
		/// </summary>
		private string _invoiceCTDetailNote;
		
		/// <summary>
		/// 进货退货单金额
		/// </summary>
		private decimal? _invoiceCTDetailMoney1;
		
		/// <summary>
		/// 单位
		/// </summary>
		private string _invoiceProductUnit;
		
		/// <summary>
		/// 
		/// </summary>
		private int? _inumber;
		
		/// <summary>
		/// 库库货位
		/// </summary>
		private DepotPosition _depotPosition;
		/// <summary>
		/// 库房退货单
		/// </summary>
		private InvoiceCT _invoice;
		/// <summary>
		/// 产品
		/// </summary>
		private Product _product;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 进货退货单货品编号
		/// </summary>
		public string InvoiceCTDetailId
		{
			get 
			{
				return this._invoiceCTDetailId;
			}
			set 
			{
				this._invoiceCTDetailId = value;
			}
		}

		/// <summary>
		/// 位置编号
		/// </summary>
		public string DepotPositionId
		{
			get 
			{
				return this._depotPositionId;
			}
			set 
			{
				this._depotPositionId = value;
			}
		}

		/// <summary>
		/// 商品编号
		/// </summary>
		public string ProductId
		{
			get 
			{
				return this._productId;
			}
			set 
			{
				this._productId = value;
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
		/// 进货退货单货品数量
		/// </summary>
		public double? InvoiceCTDetailQuantity
		{
			get 
			{
				return this._invoiceCTDetailQuantity;
			}
			set 
			{
				this._invoiceCTDetailQuantity = value;
			}
		}

		/// <summary>
		/// 进货退货单货品单价
		/// </summary>
		public decimal? InvoiceCTDetailPrice
		{
			get 
			{
				return this._invoiceCTDetailPrice;
			}
			set 
			{
				this._invoiceCTDetailPrice = value;
			}
		}

		/// <summary>
		/// 进货退货单货品折扣率
		/// </summary>
		public double? InvoiceCTDetailDiscountRate
		{
			get 
			{
				return this._invoiceCTDetailDiscountRate;
			}
			set 
			{
				this._invoiceCTDetailDiscountRate = value;
			}
		}

		/// <summary>
		/// 进货退货单货品赠送否
		/// </summary>
		public bool? InvoiceCTDetailZS
		{
			get 
			{
				return this._invoiceCTDetailZS;
			}
			set 
			{
				this._invoiceCTDetailZS = value;
			}
		}

		/// <summary>
		/// 进货退货??货品税率
		/// </summary>
		public double? InvoiceCTDetailTaxRate
		{
			get 
			{
				return this._invoiceCTDetailTaxRate;
			}
			set 
			{
				this._invoiceCTDetailTaxRate = value;
			}
		}

		/// <summary>
		/// 进货退货单货品折扣额
		/// </summary>
		public decimal? InvoiceCTDetailDisount
		{
			get 
			{
				return this._invoiceCTDetailDisount;
			}
			set 
			{
				this._invoiceCTDetailDisount = value;
			}
		}

		/// <summary>
		/// 进货退货单货品应付款额
		/// </summary>
		public decimal? InvoiceCTDetailMoney0
		{
			get 
			{
				return this._invoiceCTDetailMoney0;
			}
			set 
			{
				this._invoiceCTDetailMoney0 = value;
			}
		}

		/// <summary>
		/// 进货退货单???品税额
		/// </summary>
		public decimal? InvoiceCTDetailTax
		{
			get 
			{
				return this._invoiceCTDetailTax;
			}
			set 
			{
				this._invoiceCTDetailTax = value;
			}
		}

		/// <summary>
		/// 进货退货单货品备注
		/// </summary>
		public string InvoiceCTDetailNote
		{
			get 
			{
				return this._invoiceCTDetailNote;
			}
			set 
			{
				this._invoiceCTDetailNote = value;
			}
		}

		/// <summary>
		/// 进货退货单金额
		/// </summary>
		public decimal? InvoiceCTDetailMoney1
		{
			get 
			{
				return this._invoiceCTDetailMoney1;
			}
			set 
			{
				this._invoiceCTDetailMoney1 = value;
			}
		}

		/// <summary>
		/// 单位
		/// </summary>
		public string InvoiceProductUnit
		{
			get 
			{
				return this._invoiceProductUnit;
			}
			set 
			{
				this._invoiceProductUnit = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public int? Inumber
		{
			get 
			{
				return this._inumber;
			}
			set 
			{
				this._inumber = value;
			}
		}
	
		/// <summary>
		/// 库库货位
		/// </summary>
		public virtual DepotPosition DepotPosition
		{
			get
			{
				return this._depotPosition;
			}
			set
			{
				this._depotPosition = value;
			}
			
		}
		/// <summary>
		/// 库房退货单
		/// </summary>
		public virtual InvoiceCT Invoice
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
		/// 产品
		/// </summary>
		public virtual Product Product
		{
			get
			{
				return this._product;
			}
			set
			{
				this._product = value;
			}
			
		}
		/// <summary>
		/// 进货退货单货品编号
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailId = "InvoiceCTDetailId";
		
		/// <summary>
		/// 位置编号
		/// </summary>
		public readonly static string PRO_DepotPositionId = "DepotPositionId";
		
		/// <summary>
		/// 商品编号
		/// </summary>
		public readonly static string PRO_ProductId = "ProductId";
		
		/// <summary>
		/// 单据编号
		/// </summary>
		public readonly static string PRO_InvoiceId = "InvoiceId";
		
		/// <summary>
		/// 进货退货单货品数量
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailQuantity = "InvoiceCTDetailQuantity";
		
		/// <summary>
		/// 进货退货单货品单价
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailPrice = "InvoiceCTDetailPrice";
		
		/// <summary>
		/// 进货退货单货品折扣率
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailDiscountRate = "InvoiceCTDetailDiscountRate";
		
		/// <summary>
		/// 进货退货单货品赠送否
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailZS = "InvoiceCTDetailZS";
		
		/// <summary>
		/// 进货退货??货品税率
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailTaxRate = "InvoiceCTDetailTaxRate";
		
		/// <summary>
		/// 进货退货单货品折扣额
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailDisount = "InvoiceCTDetailDisount";
		
		/// <summary>
		/// 进货退货单货品应付款额
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailMoney0 = "InvoiceCTDetailMoney0";
		
		/// <summary>
		/// 进货退货单???品税额
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailTax = "InvoiceCTDetailTax";
		
		/// <summary>
		/// 进货退货单货品备注
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailNote = "InvoiceCTDetailNote";
		
		/// <summary>
		/// 进货退货单金额
		/// </summary>
		public readonly static string PRO_InvoiceCTDetailMoney1 = "InvoiceCTDetailMoney1";
		
		/// <summary>
		/// 单位
		/// </summary>
		public readonly static string PRO_InvoiceProductUnit = "InvoiceProductUnit";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_Inumber = "Inumber";
		

		#endregion
	}
}
