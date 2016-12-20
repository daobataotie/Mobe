﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProduceOtherExitMaterial.autogenerated.cs
// author: mayanjun
// create date：2010-12-14 16:59:28
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class ProduceOtherExitMaterial
	{
		#region Data

		/// <summary>
		/// 编号
		/// </summary>
		private string _produceOtherExitMaterialId;
		
		/// <summary>
		/// 操作人
		/// </summary>
		private string _employee0Id;
		
		/// <summary>
		/// 退料人
		/// </summary>
		private string _employee1Id;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 说明
		/// </summary>
		private string _produceOtherExitMaterialDesc;
		
		/// <summary>
		/// 时间
		/// </summary>
		private DateTime? _produceOtherExitMaterialDate;
		
		/// <summary>
		/// 
		/// </summary>
		private string _workHouseId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _supplierId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _depotId;
		
		/// <summary>
		/// 库房
		/// </summary>
		private Depot _depot;
		/// <summary>
		/// 供应商
		/// </summary>
		private Supplier _supplier;
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee0;
		/// <summary>
		/// 员工
		/// </summary>
		private Employee _employee1;
		/// <summary>
		/// 工作中心
		/// </summary>
		private WorkHouse _workHouse;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 编号
		/// </summary>
		public string ProduceOtherExitMaterialId
		{
			get 
			{
				return this._produceOtherExitMaterialId;
			}
			set 
			{
				this._produceOtherExitMaterialId = value;
			}
		}

		/// <summary>
		/// 操作人
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
		/// 退料人
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
		/// 说明
		/// </summary>
		public string ProduceOtherExitMaterialDesc
		{
			get 
			{
				return this._produceOtherExitMaterialDesc;
			}
			set 
			{
				this._produceOtherExitMaterialDesc = value;
			}
		}

		/// <summary>
		/// 时间
		/// </summary>
		public DateTime? ProduceOtherExitMaterialDate
		{
			get 
			{
				return this._produceOtherExitMaterialDate;
			}
			set 
			{
				this._produceOtherExitMaterialDate = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string WorkHouseId
		{
			get 
			{
				return this._workHouseId;
			}
			set 
			{
				this._workHouseId = value;
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
		/// 工作中心
		/// </summary>
		public virtual WorkHouse WorkHouse
		{
			get
			{
				return this._workHouse;
			}
			set
			{
				this._workHouse = value;
			}
			
		}
		/// <summary>
		/// 编号
		/// </summary>
		public readonly static string PRO_ProduceOtherExitMaterialId = "ProduceOtherExitMaterialId";
		
		/// <summary>
		/// 操作人
		/// </summary>
		public readonly static string PRO_Employee0Id = "Employee0Id";
		
		/// <summary>
		/// 退料人
		/// </summary>
		public readonly static string PRO_Employee1Id = "Employee1Id";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PRO_InsertTime = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PRO_UpdateTime = "UpdateTime";
		
		/// <summary>
		/// 说明
		/// </summary>
		public readonly static string PRO_ProduceOtherExitMaterialDesc = "ProduceOtherExitMaterialDesc";
		
		/// <summary>
		/// 时间
		/// </summary>
		public readonly static string PRO_ProduceOtherExitMaterialDate = "ProduceOtherExitMaterialDate";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_WorkHouseId = "WorkHouseId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_SupplierId = "SupplierId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_DepotId = "DepotId";
		

		#endregion
	}
}
