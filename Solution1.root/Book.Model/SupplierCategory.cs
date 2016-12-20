//------------------------------------------------------------------------------
//
// file name：SupplierCategory.cs
// author: peidun
// create date：2009-08-03 9:37:30
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 供应商分类
    /// </summary>
    [Serializable]
    public partial class SupplierCategory
    {
        public override String ToString()
        {
            return _id + "-" + _supplierCategoryName;
        }

        public bool IsCheck
        {
            get;
            set;
        }
    }
}
