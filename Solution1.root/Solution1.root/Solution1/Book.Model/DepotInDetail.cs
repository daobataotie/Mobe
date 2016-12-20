//------------------------------------------------------------------------------
//
// file name：DepotInDetail.cs
// author: mayanjun
// create date：2010-10-25 16:14:52
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 其他入库详细
    /// </summary>
    [Serializable]
    public partial class DepotInDetail
    {
        public string ProductDescription
        {
            get
            {
                return this._product == null ? "" : this._product.ProductDescription;
            }
        }
    }
}
