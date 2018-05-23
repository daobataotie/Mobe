﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AssemblySiteDifferenceDetai.autogenerated.cs
// author: mayanjun
// create date：2018-05-14 19:16:33
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	public partial class AssemblySiteDifferenceDetai
	{
		#region Data

		/// <summary>
		/// 
		/// </summary>
		private string _assemblySiteDifferenceDetaiId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _assemblySiteDifferenceId;
		
		/// <summary>
		/// 
		/// </summary>
		private string _productId;
		
		/// <summary>
		/// 
		/// </summary>
		private decimal? _actualQuantity;
		
		/// <summary>
		/// 
		/// </summary>
		private decimal? _theoryQuantity;
		
		/// <summary>
		/// 产品
		/// </summary>
		private Product _product;

        private AssemblySiteDifference _assemblySiteDifference;
		 
		#endregion
		
		#region Properties
		
		/// <summary>
		/// 
		/// </summary>
		public string AssemblySiteDifferenceDetaiId
		{
			get 
			{
				return this._assemblySiteDifferenceDetaiId;
			}
			set 
			{
				this._assemblySiteDifferenceDetaiId = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string AssemblySiteDifferenceId
		{
			get 
			{
				return this._assemblySiteDifferenceId;
			}
			set 
			{
				this._assemblySiteDifferenceId = value;
			}
		}

		/// <summary>
		/// 
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
		/// 
		/// </summary>
		public decimal? ActualQuantity
		{
			get 
			{
				return this._actualQuantity;
			}
			set 
			{
				this._actualQuantity = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public decimal? TheoryQuantity
		{
			get 
			{
				return this._theoryQuantity;
			}
			set 
			{
				this._theoryQuantity = value;
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

        public AssemblySiteDifference AssemblySiteDifference
        {
            get { return _assemblySiteDifference; }
            set { _assemblySiteDifference = value; }
        }

		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_AssemblySiteDifferenceDetaiId = "AssemblySiteDifferenceDetaiId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_AssemblySiteDifferenceId = "AssemblySiteDifferenceId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ProductId = "ProductId";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_ActualQuantity = "ActualQuantity";
		
		/// <summary>
		/// 
		/// </summary>
		public readonly static string PRO_TheoryQuantity = "TheoryQuantity";

        public readonly static string PRO_AssemblySiteDifference = "AssemblySiteDifference";

		#endregion
	}
}