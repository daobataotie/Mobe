﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：BOMProductProcess.autogenerated.cs
// author: peidun
// create date：2009-11-14 9:57:10
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class BOMProductProcess
	{
		#region Data

		/// <summary>
		/// 
		/// </summary>
		private string _bOMProductProcessId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _bomId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _processCategoryId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _processId;
		
		/// <summary>
		/// 
		/// </summary>
		private bool? _isCheck;
		
		/// <summary>
		/// 加工种类
		/// </summary>
		private ProcessCategory processCategory;
		/// <summary>
		/// 加工
		/// </summary>
		private Processing process;
		/// <summary>
		/// Bom母件信息
		/// </summary>
		private BomParentPartInfo bom;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		public string BOMProductProcessId
		{
			get 
			{
				return this._bOMProductProcessId;
			}
			set 
			{
				this._bOMProductProcessId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string BomId
		{
			get 
			{
				return this._bomId;
			}
			set 
			{
				this._bomId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ProcessCategoryId
		{
			get 
			{
				return this._processCategoryId;
			}
			set 
			{
				this._processCategoryId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ProcessId
		{
			get 
			{
				return this._processId;
			}
			set 
			{
				this._processId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool? IsCheck
		{
			get 
			{
				return this._isCheck;
			}
			set 
			{
				this._isCheck = value;
			}
		}
	
		/// <summary>
		/// 加工种类
		/// </summary>
		public virtual ProcessCategory ProcessCategory
		{
			get
			{
				return this.processCategory;
			}
			set
			{
				this.processCategory = value;
			}
			
		}
		/// <summary>
		/// 加工
		/// </summary>
		public virtual Processing Process
		{
			get
			{
				return this.process;
			}
			set
			{
				this.process = value;
			}
			
		}
		/// <summary>
		/// Bom母件信息
		/// </summary>
		public virtual BomParentPartInfo Bom
		{
			get
			{
				return this.bom;
			}
			set
			{
				this.bom = value;
			}
			
		}
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PROPERTY_BOMPRODUCTPROCESSID = "BOMProductProcessId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PROPERTY_BOMID = "BomId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PROPERTY_PROCESSCATEGORYID = "ProcessCategoryId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PROPERTY_PROCESSID = "ProcessId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PROPERTY_ISCHECK = "IsCheck";
		

		#endregion
	}
}
