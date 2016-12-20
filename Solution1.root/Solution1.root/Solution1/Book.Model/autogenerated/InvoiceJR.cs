﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoiceJR.autogenerated.cs
// author: mayanjun
// create date：2010-11-17 11:05:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class InvoiceJR
	{
		#region Data

		/// <summary>
		/// 借入单
		/// </summary>
		private double? _invoiceJRQuantity;
		
		/// <summary>
		/// 
		/// </summary>
		private string _depotId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _supplierId;
		
		/// <summary>
		/// 
		/// </summary>
		private bool? _isHcAll;
		
		/// <summary>
		/// 库房
		/// </summary>
		private Depot _depot;
		/// <summary>
		/// 供应商
		/// </summary>
		private Supplier _supplier;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 借入单
		/// </summary>
		public double? InvoiceJRQuantity
		{
			get 
			{
				return this._invoiceJRQuantity;
			}
			set 
			{
				this._invoiceJRQuantity = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string DepotId
		{
			get 
			{
				return this._depotId;
			}
			set 
			{
				this._depotId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string SupplierId
		{
			get 
			{
				return this._supplierId;
			}
			set 
			{
				this._supplierId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool? IsHcAll
		{
			get 
			{
				return this._isHcAll;
			}
			set 
			{
				this._isHcAll = value;
			}
		}
	
		/// <summary>
		/// 库房
		/// </summary>
		public virtual Depot Depot
		{
			get
			{
				return this._depot;
			}
			set
			{
				this._depot = value;
			}
			
		}
		/// <summary>
		/// 供应商
		/// </summary>
		public virtual Supplier Supplier
		{
			get
			{
				return this._supplier;
			}
			set
			{
				this._supplier = value;
			}
			
		}
		/// <summary>
		/// 借入单
		/// </summary>
		public readonly static string PRO_InvoiceJRQuantity = "InvoiceJRQuantity";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_DepotId = "DepotId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_SupplierId = "SupplierId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_IsHcAll = "IsHcAll";
		

		#endregion
	}
}
