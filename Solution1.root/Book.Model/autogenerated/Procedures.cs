﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：Procedures.autogenerated.cs
// author: mayanjun
// create date：2011-3-22 15:11:29
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class Procedures
	{
		#region Data

		/// <summary>
		/// Procedureid
		/// </summary>
		private string _proceduresId;
		
		/// <summary>
		/// 工作中心编号
		/// </summary>
		private string _workHouseId;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// Procedurename
		/// </summary>
		private string _procedurename;
		
		/// <summary>
		/// Proceduresate
		/// </summary>
		private string _proceduresate;
		
		/// <summary>
		/// ProcedureType
		/// </summary>
		private string _procedureType;
		
		/// <summary>
		/// Startdate
		/// </summary>
		private DateTime? _startdate;
		
		/// <summary>
		/// Enddate
		/// </summary>
		private DateTime? _enddate;
		
		/// <summary>
		/// Leadtime
		/// </summary>
		private int? _leadtime;
		
		/// <summary>
		/// Proceduredescription
		/// </summary>
		private string _proceduredescription;
		
		/// <summary>
		/// 
		/// </summary>
		private string _id;
		
		/// <summary>
		/// 
		/// </summary>
		private string _technologydetailsNo;
		
		/// <summary>
		/// 
		/// </summary>
		private string _pronoteMachineId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _processCategoryId;
		
		/// <summary>
		/// 
		/// </summary>
		private bool? _isChecked;
		
		/// <summary>
		/// 
		/// </summary>
		private bool? _isOtherProduceOther;
		
		/// <summary>
		/// 
		/// </summary>
		private string _supplierId;
		
		/// <summary>
		/// 加工种类
		/// </summary>
		private ProcessCategory _processCategory;
		/// <summary>
		/// 供应商
		/// </summary>
		private Supplier _supplier;
		/// <summary>
		/// 工作中心
		/// </summary>
		private WorkHouse _workHouse;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Procedureid
		/// </summary>
		public string ProceduresId
		{
			get 
			{
				return this._proceduresId;
			}
			set 
			{
				this._proceduresId = value;
			}
		}

		/// <summary>
		/// 工作中心编号
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
		/// Procedurename
		/// </summary>
		public string Procedurename
		{
			get 
			{
				return this._procedurename;
			}
			set 
			{
				this._procedurename = value;
			}
		}

		/// <summary>
		/// Proceduresate
		/// </summary>
		public string Proceduresate
		{
			get 
			{
				return this._proceduresate;
			}
			set 
			{
				this._proceduresate = value;
			}
		}

		/// <summary>
		/// ProcedureType
		/// </summary>
		public string ProcedureType
		{
			get 
			{
				return this._procedureType;
			}
			set 
			{
				this._procedureType = value;
			}
		}

		/// <summary>
		/// Startdate
		/// </summary>
		public DateTime? Startdate
		{
			get 
			{
				return this._startdate;
			}
			set 
			{
				this._startdate = value;
			}
		}

		/// <summary>
		/// Enddate
		/// </summary>
		public DateTime? Enddate
		{
			get 
			{
				return this._enddate;
			}
			set 
			{
				this._enddate = value;
			}
		}

		/// <summary>
		/// Leadtime
		/// </summary>
		public int? Leadtime
		{
			get 
			{
				return this._leadtime;
			}
			set 
			{
				this._leadtime = value;
			}
		}

		/// <summary>
		/// Proceduredescription
		/// </summary>
		public string Proceduredescription
		{
			get 
			{
				return this._proceduredescription;
			}
			set 
			{
				this._proceduredescription = value;
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
		/// 
		/// </summary>
		public string TechnologydetailsNo
		{
			get 
			{
				return this._technologydetailsNo;
			}
			set 
			{
				this._technologydetailsNo = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string PronoteMachineId
		{
			get 
			{
				return this._pronoteMachineId;
			}
			set 
			{
				this._pronoteMachineId = value;
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
		public bool? IsChecked
		{
			get 
			{
				return this._isChecked;
			}
			set 
			{
				this._isChecked = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool? IsOtherProduceOther
		{
			get 
			{
				return this._isOtherProduceOther;
			}
			set 
			{
				this._isOtherProduceOther = value;
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
		/// 加工种类
		/// </summary>
		public virtual ProcessCategory ProcessCategory
		{
			get
			{
				return this._processCategory;
			}
			set
			{
				this._processCategory = value;
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
		/// Procedureid
		/// </summary>
		public readonly static string PRO_ProceduresId = "ProceduresId";
		
		/// <summary>
		/// 工作中心编号
		/// </summary>
		public readonly static string PRO_WorkHouseId = "WorkHouseId";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PRO_InsertTime = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PRO_UpdateTime = "UpdateTime";
		
		/// <summary>
		/// Procedurename
		/// </summary>
		public readonly static string PRO_Procedurename = "Procedurename";
		
		/// <summary>
		/// Proceduresate
		/// </summary>
		public readonly static string PRO_Proceduresate = "Proceduresate";
		
		/// <summary>
		/// ProcedureType
		/// </summary>
		public readonly static string PRO_ProcedureType = "ProcedureType";
		
		/// <summary>
		/// Startdate
		/// </summary>
		public readonly static string PRO_Startdate = "Startdate";
		
		/// <summary>
		/// Enddate
		/// </summary>
		public readonly static string PRO_Enddate = "Enddate";
		
		/// <summary>
		/// Leadtime
		/// </summary>
		public readonly static string PRO_Leadtime = "Leadtime";
		
		/// <summary>
		/// Proceduredescription
		/// </summary>
		public readonly static string PRO_Proceduredescription = "Proceduredescription";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_Id = "Id";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_TechnologydetailsNo = "TechnologydetailsNo";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_PronoteMachineId = "PronoteMachineId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProcessCategoryId = "ProcessCategoryId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_IsChecked = "IsChecked";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_IsOtherProduceOther = "IsOtherProduceOther";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_SupplierId = "SupplierId";
		

		#endregion
	}
}
