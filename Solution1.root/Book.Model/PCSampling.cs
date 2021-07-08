//------------------------------------------------------------------------------
//
// file name：PCSampling.cs
// author: mayanjun
// create date：2015/10/30 17:07:37
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
    /// 品管抽检日报表 → 组装检验日报表
	/// </summary>
	[Serializable]
	public partial class PCSampling
	{
        private System.Collections.Generic.IList<Model.PCSamplingDetail> details = new System.Collections.Generic.List<Model.PCSamplingDetail>();

        public System.Collections.Generic.IList<Model.PCSamplingDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}