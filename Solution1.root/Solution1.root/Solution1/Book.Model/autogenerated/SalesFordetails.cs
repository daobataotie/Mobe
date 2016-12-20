﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：SalesFordetails.autogenerated.cs
// author: peidun
// create date：2009-12-18 11:23:49
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class SalesFordetails
	{
		#region Data

		/// <summary>
		/// 销售预测明细编号
		/// </summary>
		private string _salesFordetailsId;
		
		/// <summary>
		/// 商品编号
		/// </summary>
		private string _productId;
		
		/// <summary>
		/// 销售预测单头编号
		/// </summary>
		private string _salesForHeaderId;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 销售预测数量
		/// </summary>
		private decimal? _salesForSum;
		
		/// <summary>
		/// 明细说明
		/// </summary>
		private string _salesFordesc;
		
		/// <summary>
		/// Column_8
		/// </summary>
		private string _productUnit;
		
		/// <summary>
		/// 产品
		/// </summary>
		private Product _product;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 销售预测明细编号
		/// </summary>
		public string SalesFordetailsId
		{
			get 
			{
				return this._salesFordetailsId;
			}
			set 
			{
				this._salesFordetailsId = value;
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
		/// 销售预测单头编号
		/// </summary>
		public string SalesForHeaderId
		{
			get 
			{
				return this._salesForHeaderId;
			}
			set 
			{
				this._salesForHeaderId = value;
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
		/// 销售预测数量
		/// </summary>
		public decimal? SalesForSum
		{
			get 
			{
				return this._salesForSum;
			}
			set 
			{
				this._salesForSum = value;
			}
		}

		/// <summary>
		/// 明细说明
		/// </summary>
		public string SalesFordesc
		{
			get 
			{
				return this._salesFordesc;
			}
			set 
			{
				this._salesFordesc = value;
			}
		}

		/// <summary>
		/// Column_8
		/// </summary>
		public string ProductUnit
		{
			get 
			{
				return this._productUnit;
			}
			set 
			{
				this._productUnit = value;
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
		/// 销售预测明细编号
		/// </summary>
		public readonly static string PROPERTY_SALESFORDETAILSID = "SalesFordetailsId";
		
		/// <summary>
		/// 商品编号
		/// </summary>
		public readonly static string PROPERTY_PRODUCTID = "ProductId";
		
		/// <summary>
		/// 销售预测单头编号
		/// </summary>
		public readonly static string PROPERTY_SALESFORHEADERID = "SalesForHeaderId";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PROPERTY_INSERTTIME = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PROPERTY_UPDATETIME = "UpdateTime";
		
		/// <summary>
		/// 销售预测数量
		/// </summary>
		public readonly static string PROPERTY_SALESFORSUM = "SalesForSum";
		
		/// <summary>
		/// 明细说明
		/// </summary>
		public readonly static string PROPERTY_SALESFORDESC = "SalesFordesc";
		
		/// <summary>
		/// Column_8
		/// </summary>
		public readonly static string PROPERTY_PRODUCTUNIT = "ProductUnit";
		

		#endregion
	}
}
