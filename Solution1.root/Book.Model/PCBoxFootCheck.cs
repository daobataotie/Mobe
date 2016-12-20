//------------------------------------------------------------------------------
//
// file name：PCBoxFootCheck.cs
// author: mayanjun
// create date：2013-1-28 15:42:35
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 产品检验单
    /// </summary>
    [Serializable]
    public partial class PCBoxFootCheck
    {
        private System.Collections.Generic.IList<Model.PCBoxFootCheckDetail> _details = new System.Collections.Generic.List<Model.PCBoxFootCheckDetail>();

        public System.Collections.Generic.IList<Model.PCBoxFootCheckDetail> Details
        {
            get { return _details; }
            set { _details = value; }
        }
    }
}
