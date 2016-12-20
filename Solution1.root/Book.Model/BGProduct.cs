//------------------------------------------------------------------------------
//
// file name：BGProduct.cs
// author: mayanjun
// create date：2013-4-1 11:58:41
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 备案资料
    /// </summary>
    [Serializable]
    public partial class BGProduct
    {
        IList<Model.BGProductDetai> _detailMaterial = new List<Model.BGProductDetai>();

        /// <summary>
        /// 备案资料详细(料件)
        /// </summary>
        public IList<Model.BGProductDetai> DetailMaterial
        {
            get { return _detailMaterial; }
            set { _detailMaterial = value; }
        }

        IList<Model.BGProductDetai> _detailProduct = new List<Model.BGProductDetai>();

        /// <summary>
        /// 备案资料详细(成品)
        /// </summary>
        public IList<Model.BGProductDetai> DetailProduct
        {
            get { return _detailProduct; }
            set { _detailProduct = value; }
        }
    }
}
