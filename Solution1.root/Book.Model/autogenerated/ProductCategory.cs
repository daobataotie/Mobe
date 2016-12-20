﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：ProductCategory.autogenerated.cs
// author: peidun
// create date：2009-10-13 上午 11:45:16
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class ProductCategory
	{
		#region Data

		/// <summary>
		/// 商品种类编号
		/// </summary>
		private string _productCategoryId;
		
		/// <summary>
		/// 产品类_商品种类编号
		/// </summary>
		private string _productCategoryParentId;
		
		/// <summary>
		/// 插入时间
		/// </summary>
		private DateTime? _insertTime;
		
		/// <summary>
		/// 修改时间
		/// </summary>
		private DateTime? _updateTime;
		
		/// <summary>
		/// 商品种类名称
		/// </summary>
		private string _productCategoryName;
		
		/// <summary>
		/// 编号
		/// </summary>
		private string _id;
		
		/// <summary>
		/// 产品类型
		/// </summary>
		private ProductCategory productCategoryParent;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 商品种类编号
		/// </summary>
		public string ProductCategoryId
		{
			get 
			{
				return this._productCategoryId;
			}
			set 
			{
				this._productCategoryId = value;
			}
		}

		/// <summary>
		/// 产品类_商品种类编号
		/// </summary>
		public string ProductCategoryParentId
		{
			get 
			{
				return this._productCategoryParentId;
			}
			set 
			{
				this._productCategoryParentId = value;
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
		/// 商品种类名称
		/// </summary>
		public string ProductCategoryName
		{
			get 
			{
				return this._productCategoryName;
			}
			set 
			{
				this._productCategoryName = value;
			}
		}

		/// <summary>
		/// 编号
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
		/// 产品类型
		/// </summary>
		public virtual ProductCategory ProductCategoryParent
		{
			get
			{
				return this.productCategoryParent;
			}
			set
			{
				this.productCategoryParent = value;
			}
			
		}
		/// <summary>
		/// 商品种类编号
		/// </summary>
		public readonly static string PROPERTY_PRODUCTCATEGORYID = "ProductCategoryId";
		
		/// <summary>
		/// 产品类_商品种类编号
		/// </summary>
		public readonly static string PROPERTY_PRODUCTCATEGORYPARENTID = "ProductCategoryParentId";
		
		/// <summary>
		/// 插入时间
		/// </summary>
		public readonly static string PROPERTY_INSERTTIME = "InsertTime";
		
		/// <summary>
		/// 修改时间
		/// </summary>
		public readonly static string PROPERTY_UPDATETIME = "UpdateTime";
		
		/// <summary>
		/// 商品种类名称
		/// </summary>
		public readonly static string PROPERTY_PRODUCTCATEGORYNAME = "ProductCategoryName";
		
		/// <summary>
		/// 编号
		/// </summary>
		public readonly static string PROPERTY_ID = "Id";
		

		#endregion
	}
}
