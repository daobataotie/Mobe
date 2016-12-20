//------------------------------------------------------------------------------
//
// file name：ANSIPCImpactCheck.cs
// author: mayanjun
// create date：2011-11-23 09:49:55
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// ANSI冲击测试表
    /// </summary>
    [Serializable]
    public partial class ANSIPCImpactCheck
    {
        private System.Collections.Generic.IList<Model.ANSIPCImpactCheckDetail> _Details;

        public System.Collections.Generic.IList<Model.ANSIPCImpactCheckDetail> Details
        {
            get { return _Details; }
            set { _Details = value; }
        }
    }
}
