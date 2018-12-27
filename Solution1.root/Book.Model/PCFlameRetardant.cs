//------------------------------------------------------------------------------
//
// file name：PCFlameRetardant.cs
// author: mayanjun
// create date：2018/12/27 11:27:47
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 阻燃测试
	/// </summary>
	[Serializable]
	public partial class PCFlameRetardant
	{
        System.Collections.Generic.IList<Model.PCFlameRetardantDetail> _details = new System.Collections.Generic.List<Model.PCFlameRetardantDetail>();

        public System.Collections.Generic.IList<Model.PCFlameRetardantDetail> Details
        {
            get { return _details; }
            set { _details = value; }
        }
	}
}