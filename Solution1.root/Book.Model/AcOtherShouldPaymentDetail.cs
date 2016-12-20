//------------------------------------------------------------------------------
//
// file name：AcOtherShouldPaymentDetail.cs
// author: mayanjun
// create date：2011-6-10 10:11:51
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 其他应付款详细
    /// </summary>
    [Serializable]
    public partial class AcOtherShouldPaymentDetail
    {
        public System.Collections.Generic.IDictionary<global::Helper.TaxType, global::Helper.TaxCalculateHelper> TaxCalualateDictionary { get; set; }
    }
}
