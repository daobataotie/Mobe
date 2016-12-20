//------------------------------------------------------------------------------
//
// file name：StockCheck.cs
// author: mayanjun
// create date：2010-7-30 11:43:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 库存盘点
    /// </summary>
    [Serializable]
    public partial class StockCheck
    {
        private System.Collections.Generic.IList<Model.StockCheckDetail> details;

        public System.Collections.Generic.IList<Model.StockCheckDetail> Details
        {
            get { return details; }
            set { details = value; }
        }


        private System.Collections.Generic.IList<Model.StockCheckDetail> productPositionNums = new System.Collections.Generic.List<Model.StockCheckDetail>();

        public System.Collections.Generic.IList<Model.StockCheckDetail> ProductPositionNums
        {
            get { return productPositionNums; }
            set { productPositionNums = value; }
        }

        public string ProductCategoryName { get; set; }
    }
}
