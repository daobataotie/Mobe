//------------------------------------------------------------------------------
//
// file name：ProduceOtherMaterialDetail.cs
// author: peidun
// create date：2010-1-5 15:39:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 外包领料详细
	/// </summary>
	[Serializable]
	public partial class ProduceOtherMaterialDetail
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
	}
}
