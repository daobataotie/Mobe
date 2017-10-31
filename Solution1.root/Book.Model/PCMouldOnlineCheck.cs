//------------------------------------------------------------------------------
//
// file name：PCMouldOnlineCheck.cs
// author: mayanjun
// create date：2015/4/13 上午 10:11:01
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 模具上线检验单
	/// </summary>
	[Serializable]
	public partial class PCMouldOnlineCheck
	{
        System.Collections.Generic.IList<Model.PCMouldOnlineCheckDetail> detail = new System.Collections.Generic.List<Model.PCMouldOnlineCheckDetail>();

        public System.Collections.Generic.IList<Model.PCMouldOnlineCheckDetail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
	}
}