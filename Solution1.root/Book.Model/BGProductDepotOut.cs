//------------------------------------------------------------------------------
//
// file name：BGProductDepotOut.cs
// author: mayanjun
// create date：2014/3/25 18:18:03
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 手册成品出货单
    /// </summary>
    [Serializable]
    public partial class BGProductDepotOut
    {
        System.Collections.Generic.IList<Model.BGProductDepotOutDetail> detail = new System.Collections.Generic.List<Model.BGProductDepotOutDetail>();

        public System.Collections.Generic.IList<Model.BGProductDepotOutDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
    }
}
