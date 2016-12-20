//------------------------------------------------------------------------------
//
// file name：PCImpactCheck.cs
// author: mayanjun
// create date：2011-11-15 13:56:55
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 冲击测试表
    /// </summary>
    [Serializable]
    public partial class PCImpactCheck
    {
        private System.Collections.Generic.IList<Model.PCImpactCheckDetail> details = new System.Collections.Generic.List<Model.PCImpactCheckDetail>();
        public System.Collections.Generic.IList<Model.PCImpactCheckDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
    }
}
