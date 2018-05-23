//------------------------------------------------------------------------------
//
// file name：AssemblySiteDifferenceDetai.cs
// author: mayanjun
// create date：2018-05-14 19:16:31
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public partial class AssemblySiteDifferenceDetai
    {
        public decimal DiffQty
        {
            get
            {
                return (this.ActualQuantity.HasValue ? this.ActualQuantity.Value : 0) - (this.TheoryQuantity.HasValue ? this.TheoryQuantity.Value : 0);
            }
        }
    }
}