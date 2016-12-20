//------------------------------------------------------------------------------
//
// file name：PCEarProtectCheck.cs
// author: mayanjun
// create date：2013-09-03 15:17:00
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 耳护坠落测试表
    /// </summary>
    [Serializable]
    public partial class PCEarProtectCheck
    {
        private System.Collections.Generic.IList<Model.PCEarProtectCheckDetail> details = new System.Collections.Generic.List<Model.PCEarProtectCheckDetail>();

        public System.Collections.Generic.IList<Model.PCEarProtectCheckDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
    }
}
