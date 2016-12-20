//------------------------------------------------------------------------------
//
// file name：PCClarityCheck.cs
// author: mayanjun
// create date：2013-08-19 15:44:13
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 清晰度测试
    /// </summary>
    [Serializable]
    public partial class PCClarityCheck
    {
        System.Collections.Generic.IList<Model.PCClarityCheckDetail> _details = new System.Collections.Generic.List<Model.PCClarityCheckDetail>();

        public System.Collections.Generic.IList<Model.PCClarityCheckDetail> Details
        {
            get { return _details; }
            set { _details = value; }
        }
    }
}
