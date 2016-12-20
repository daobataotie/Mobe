//------------------------------------------------------------------------------
//
// file name：PCSamplingEar.cs
// author: mayanjun
// create date：2015/10/31 16:25:11
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
    /// <summary>
    /// 品管抽检日报表(耳护类）
    /// </summary>
    [Serializable]
    public partial class PCSamplingEar
    {
        System.Collections.Generic.IList<Model.PCSamplingEarDetail> details = new System.Collections.Generic.List<Model.PCSamplingEarDetail>();

        public System.Collections.Generic.IList<Model.PCSamplingEarDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
    }
}