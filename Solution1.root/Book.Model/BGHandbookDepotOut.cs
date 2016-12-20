//------------------------------------------------------------------------------
//
// file name：BGHandbookDepotOut.cs
// author: mayanjun
// create date：2014/3/5 16:32:46
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 手册转出
    /// </summary>
    [Serializable]
    public partial class BGHandbookDepotOut
    {
        private System.Collections.Generic.IList<Model.BGHandbookDepotOutDetail> detail = new System.Collections.Generic.List<Model.BGHandbookDepotOutDetail>();

        public System.Collections.Generic.IList<Model.BGHandbookDepotOutDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
    }
}
