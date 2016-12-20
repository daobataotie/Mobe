//------------------------------------------------------------------------------
//
// file name：ProductOnlineCheck.cs
// author: mayanjun
// create date：2013-3-25 17:50:57
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 产品上线检验单
    /// </summary>
    [Serializable]
    public partial class ProductOnlineCheck
    {
        private System.Collections.Generic.IList<Model.ProductOnlineCheckDetail> detail = new System.Collections.Generic.List<Model.ProductOnlineCheckDetail>();

        public System.Collections.Generic.IList<Model.ProductOnlineCheckDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
    }
}
