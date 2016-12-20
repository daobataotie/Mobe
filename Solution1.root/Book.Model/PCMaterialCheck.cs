//------------------------------------------------------------------------------
//
// file name：PCMaterialCheck.cs
// author: mayanjun
// create date：2015/10/24 17:47:35
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 物料检验单
	/// </summary>
	[Serializable]
	public partial class PCMaterialCheck
	{
        System.Collections.Generic.IList<Model.PCMaterialCheckDetail> details = new System.Collections.Generic.List<Model.PCMaterialCheckDetail>();

        public System.Collections.Generic.IList<Model.PCMaterialCheckDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}