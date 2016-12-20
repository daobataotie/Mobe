﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProductOnlineCheckDetail.autogenerated.cs
// author: mayanjun
// create date：2013-3-26 10:59:39
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class ProductOnlineCheckDetail
	{
		#region Data

		/// <summary>
		/// 
		/// </summary>
		private string _productOnlineCheckDetailId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _productOnlineCheckId;
		
		/// <summary>
		/// 检查日期
		/// </summary>
		private DateTime? _checkDate;
		
		/// <summary>
		/// 毛边
		/// </summary>
        private string _maoBian;
		
		/// <summary>
		/// 擦伤
		/// </summary>
        private string _caShang;
		
		/// <summary>
		/// 缩水
		/// </summary>
        private string _suoShui;
		
		/// <summary>
		/// 对色
		/// </summary>
        private string _duiSe;
		
		/// <summary>
		/// 折片
		/// </summary>
        private string _zhepian;
		
		/// <summary>
		/// 异常情况
		/// </summary>
		private string _remark;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 
		/// </summary>
		private ProductOnlineCheck _productOnlineCheck;

        private string _businessHoursId;

        public string BusinessHoursId
        {
            get { return _businessHoursId; }
            set { _businessHoursId = value; }
        }

        private BusinessHours businessHours;

        public BusinessHours BusinessHours
        {
            get { return businessHours; }
            set { businessHours = value; }
        }

      
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		public string ProductOnlineCheckDetailId
		{
			get 
			{
				return this._productOnlineCheckDetailId;
			}
			set 
			{
				this._productOnlineCheckDetailId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ProductOnlineCheckId
		{
			get 
			{
				return this._productOnlineCheckId;
			}
			set 
			{
				this._productOnlineCheckId = value;
			}
		}

		/// <summary>
		/// 检查日期
		/// </summary>
		public DateTime? CheckDate
		{
			get 
			{
				return this._checkDate;
			}
			set 
			{
				this._checkDate = value;
			}
		}

		/// <summary>
		/// 毛边
		/// </summary>
        public string MaoBian
		{
			get 
			{
				return this._maoBian;
			}
			set 
			{
				this._maoBian = value;
			}
		}

		/// <summary>
		/// 擦伤
		/// </summary>
        public string CaShang
		{
			get 
			{
				return this._caShang;
			}
			set 
			{
				this._caShang = value;
			}
		}

		/// <summary>
		/// 缩水
		/// </summary>
        public string SuoShui
		{
			get 
			{
				return this._suoShui;
			}
			set 
			{
				this._suoShui = value;
			}
		}

		/// <summary>
		/// 对色
		/// </summary>
        public string DuiSe
		{
			get 
			{
				return this._duiSe;
			}
			set 
			{
				this._duiSe = value;
			}
		}

		/// <summary>
		/// 折片
		/// </summary>
        public string Zhepian
		{
			get 
			{
				return this._zhepian;
			}
			set 
			{
				this._zhepian = value;
			}
		}

		/// <summary>
		/// 异常情况
		/// </summary>
		public string Remark
		{
			get 
			{
				return this._remark;
			}
			set 
			{
				this._remark = value;
			}
		}

		/// <summary>
		/// 
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
		/// 
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
		/// 
		/// </summary>
		public virtual ProductOnlineCheck ProductOnlineCheck
		{
			get
			{
				return this._productOnlineCheck;
			}
			set
			{
				this._productOnlineCheck = value;
			}
			
		}
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProductOnlineCheckDetailId = "ProductOnlineCheckDetailId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProductOnlineCheckId = "ProductOnlineCheckId";
		
		/// <summary>
		/// 检查日期
		/// </summary>
		public readonly static string PRO_CheckDate = "CheckDate";
		
		/// <summary>
		/// 毛边
		/// </summary>
		public readonly static string PRO_MaoBian = "MaoBian";
		
		/// <summary>
		/// 擦伤
		/// </summary>
		public readonly static string PRO_CaShang = "CaShang";
		
		/// <summary>
		/// 缩水
		/// </summary>
		public readonly static string PRO_SuoShui = "SuoShui";
		
		/// <summary>
		/// 对色
		/// </summary>
		public readonly static string PRO_DuiSe = "DuiSe";
		
		/// <summary>
		/// 折片
		/// </summary>
		public readonly static string PRO_Zhepian = "Zhepian";
		
		/// <summary>
		/// 异常情况
		/// </summary>
		public readonly static string PRO_Remark = "Remark";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_InsertTime = "InsertTime";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_UpdateTime = "UpdateTime";

        public readonly static string PRO_BusinessHoursId = "BusinessHoursId";
		

		#endregion
	}
}
