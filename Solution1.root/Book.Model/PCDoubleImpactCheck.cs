//------------------------------------------------------------------------------
//
// file name：PCDoubleImpactCheck.cs
// author: mayanjun
// create date：2011-11-24 17:38:16
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// ANSI~CSA/H.M冲击测试单
    /// </summary>
    [Serializable]
    public partial class PCDoubleImpactCheck
    {
        private System.Collections.Generic.IList<Model.PCDoubleImpactCheckDetail> _detail;

        public System.Collections.Generic.IList<Model.PCDoubleImpactCheckDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }

    }
}
