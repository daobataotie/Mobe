//------------------------------------------------------------------------------
//
// file name：AcOtherShouldCollectionDetail.cs
// author: mayanjun
// create date：2011-6-10 11:19:27
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 其他应收款详细
    /// </summary>
    [Serializable]
    public partial class AcOtherShouldCollectionDetail
    {
        public System.Collections.Generic.IDictionary<global::Helper.TaxType, global::Helper.TaxCalculateHelper> TaxCalualateDictionary { get; set; }
    }
}
