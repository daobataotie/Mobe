//------------------------------------------------------------------------------
//
// file name：BGHandbookRange.cs
// author: mayanjun
// create date：2013-4-17 15:13:05
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
    /// <summary>
    /// 手册归类
    /// </summary>
    [Serializable]
    public partial class BGHandbookRange
    {
        private IList<Model.BGHandbookRangeDetail> detailProducts = new List<Model.BGHandbookRangeDetail>();

        /// <summary>
        /// 成品
        /// </summary>
        public IList<Model.BGHandbookRangeDetail> DetailProducts
        {
            get { return detailProducts; }
            set { detailProducts = value; }
        }

        private IList<Model.BGHandbookRangeDetail> detailMaterials = new List<Model.BGHandbookRangeDetail>();

        /// <summary>
        /// 料件
        /// </summary>
        public IList<Model.BGHandbookRangeDetail> DetailMaterials
        {
            get { return detailMaterials; }
            set { detailMaterials = value; }
        }
    }
}
