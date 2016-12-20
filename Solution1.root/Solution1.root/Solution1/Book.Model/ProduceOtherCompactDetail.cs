﻿//------------------------------------------------------------------------------
//
// file name：ProduceOtherCompactDetail.cs
// author: peidun
// create date：2010-1-4 15:32:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 生产外包合同详细
    /// </summary>
    [Serializable]
    public partial class ProduceOtherCompactDetail
    {
        private string _productSpecification;
        /// <summary>
        /// 规格型号
        /// </summary>
        public string ProductSpecification
        {
            get
            {
                return this._productSpecification;
            }
            set
            {
                this._productSpecification = value;
            }
        }

        private double noInDepotCount = 0;
        /// <summary>
        /// 未入库的委外产品数量
        /// </summary>
        public double NoInDepotCount
        {
            get
            {
                if (this._otherCompactCount == null)
                    this._otherCompactCount = 0;
                if (this._inDepotCount == null)
                    this._inDepotCount = 0;
                return this._otherCompactCount.Value - this._inDepotCount.Value;
            }
        }
      
        public string ProductDesc
        {
            get { return this.Product==null?null:this.Product.ProductDescription; }
        }

    }
}
