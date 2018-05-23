﻿//------------------------------------------------------------------------------
//
// 说明： 该文件中的内容是由代码生成器自动生成的，请勿手工修改！
//
// file name：AssemblySiteInventoryDetail.autogenerated.cs
// author: mayanjun
// create date：2018-05-14 19:16:33
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    public partial class AssemblySiteInventoryDetail
    {
        #region Data

        /// <summary>
        /// 
        /// </summary>
        private string _assemblySiteInventoryDetailId;

        /// <summary>
        /// 
        /// </summary>
        private string _assemblySiteInventoryId;

        /// <summary>
        /// 
        /// </summary>
        private string _productId;

        /// <summary>
        /// 
        /// </summary>
        private decimal? _quantity;

        /// <summary>
        /// 产品
        /// </summary>
        private Product _product;

        private AssemblySiteInventory _assemblySiteInventory;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public string AssemblySiteInventoryDetailId
        {
            get
            {
                return this._assemblySiteInventoryDetailId;
            }
            set
            {
                this._assemblySiteInventoryDetailId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AssemblySiteInventoryId
        {
            get
            {
                return this._assemblySiteInventoryId;
            }
            set
            {
                this._assemblySiteInventoryId = value;
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
        public decimal? Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
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

        public AssemblySiteInventory AssemblySiteInventory
        {
            get
            {
                return _assemblySiteInventory;
            }
            set
            {
                _assemblySiteInventory = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_AssemblySiteInventoryDetailId = "AssemblySiteInventoryDetailId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_AssemblySiteInventoryId = "AssemblySiteInventoryId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_ProductId = "ProductId";

        /// <summary>
        /// 
        /// </summary>
        public readonly static string PRO_Quantity = "Quantity";

        public readonly static string PRO_AssemblySiteInventory = "AssemblySiteInventory";
        #endregion
    }
}