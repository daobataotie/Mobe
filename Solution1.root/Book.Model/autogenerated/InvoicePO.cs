﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：InvoicePO.autogenerated.cs
// author: mayanjun
// create date：2010-11-19 11:30:45
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class InvoicePO
	{
		#region Data

		/// <summary>
		/// 部门编号
		/// </summary>
		private string _departmentId;
		
		/// <summary>
		/// 借出单数量
		/// </summary>
		private double? _invoicePOQuantity;
		
		/// <summary>
		/// 
		/// </summary>
		private string _poDepotId;
		
		/// <summary>
		/// 库房
		/// </summary>
		private Depot _poDepot;
		/// <summary>
		/// 部门
		/// </summary>
		private Department _department;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 部门编号
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
		/// 借出单数量
		/// </summary>
		public double? InvoicePOQuantity
		{
			get 
			{
				return this._invoicePOQuantity;
			}
			set 
			{
				this._invoicePOQuantity = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string PoDepotId
		{
			get 
			{
				return this._poDepotId;
			}
			set 
			{
				this._poDepotId = value;
			}
		}
	
		/// <summary>
		/// 库房
		/// </summary>
		public virtual Depot PoDepot
		{
			get
			{
				return this._poDepot;
			}
			set
			{
				this._poDepot = value;
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
		/// 部门编号
		/// </summary>
		public readonly static string PRO_DepartmentId = "DepartmentId";
		
		/// <summary>
		/// 借出单数量
		/// </summary>
		public readonly static string PRO_InvoicePOQuantity = "InvoicePOQuantity";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_PoDepotId = "PoDepotId";
		

		#endregion
	}
}
