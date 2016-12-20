﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：process.autogenerated.cs
// author: peidun
// create date：2009-12-11 16:57:38
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class process
	{
		#region Data

		/// <summary>
		/// 过程编号
		/// </summary>
		private string _processId;
		
		/// <summary>
		/// 工作流编号
		/// </summary>
		private string _workflowId;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 过程名称
		/// </summary>
		private string _processname;
		
		/// <summary>
		/// 过程描述
		/// </summary>
		private string _descript;
		
		/// <summary>
		/// 接收方数量
		/// </summary>
		private int? _number;
		
		/// <summary>
		/// 通过与或规则
		/// </summary>
		private string _andrule;
		
		/// <summary>
		/// 下一过程
		/// </summary>
		private string _processnex;
		
		/// <summary>
		/// 上一过程
		/// </summary>
		private string _processpre;
		
		/// <summary>
		/// 
		/// </summary>
		private string _processType;
		
		/// <summary>
		/// 工作流表
		/// </summary>
		private Workflow _workflow;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 过程编号
		/// </summary>
		public string processId
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
		/// 工作流编号
		/// </summary>
		public string WorkflowId
		{
			get 
			{
				return this._workflowId;
			}
			set 
			{
				this._workflowId = value;
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
		/// 过程名称
		/// </summary>
		public string processname
		{
			get 
			{
				return this._processname;
			}
			set 
			{
				this._processname = value;
			}
		}

		/// <summary>
		/// 过程描述
		/// </summary>
		public string descript
		{
			get 
			{
				return this._descript;
			}
			set 
			{
				this._descript = value;
			}
		}

		/// <summary>
		/// 接收方数量
		/// </summary>
		public int? number
		{
			get 
			{
				return this._number;
			}
			set 
			{
				this._number = value;
			}
		}

		/// <summary>
		/// 通过与或规则
		/// </summary>
		public string andrule
		{
			get 
			{
				return this._andrule;
			}
			set 
			{
				this._andrule = value;
			}
		}

		/// <summary>
		/// 下一过程
		/// </summary>
		public string Processnex
		{
			get 
			{
				return this._processnex;
			}
			set 
			{
				this._processnex = value;
			}
		}

		/// <summary>
		/// 上一过程
		/// </summary>
		public string Processpre
		{
			get 
			{
				return this._processpre;
			}
			set 
			{
				this._processpre = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string processType
		{
			get 
			{
				return this._processType;
			}
			set 
			{
				this._processType = value;
			}
		}
	
		/// <summary>
		/// 工作流表
		/// </summary>
		public virtual Workflow Workflow
		{
			get
			{
				return this._workflow;
			}
			set
			{
				this._workflow = value;
			}
			
		}
		/// <summary>
		/// 过程编号
		/// </summary>
		public readonly static string PROPERTY_PROCESSID = "processId";
		
		/// <summary>
		/// 工作流编号
		/// </summary>
		public readonly static string PROPERTY_WORKFLOWID = "WorkflowId";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PROPERTY_INSERTTIME = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PROPERTY_UPDATETIME = "UpdateTime";
		
		/// <summary>
		/// 过程名称
		/// </summary>
		public readonly static string PROPERTY_PROCESSNAME = "processname";
		
		/// <summary>
		/// 过程描述
		/// </summary>
		public readonly static string PROPERTY_DESCRIPT = "descript";
		
		/// <summary>
		/// 接收方数量
		/// </summary>
		public readonly static string PROPERTY_NUMBER = "number";
		
		/// <summary>
		/// 通过与或规则
		/// </summary>
		public readonly static string PROPERTY_ANDRULE = "andrule";
		
		/// <summary>
		/// 下一过程
		/// </summary>
		public readonly static string PROPERTY_PROCESSNEX = "Processnex";
		
		/// <summary>
		/// 上一过程
		/// </summary>
		public readonly static string PROPERTY_PROCESSPRE = "Processpre";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PROPERTY_PROCESSTYPE = "processType";
		

		#endregion
	}
}
