//------------------------------------------------------------------------------
//
// file name：ProduceOtherMaterial.cs
// author: peidun
// create date：2010-1-5 15:26:22
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 外包领料
	/// </summary>
	[Serializable]
	public partial class ProduceOtherMaterial
	{
        private System.Collections.Generic.IList<ProduceOtherMaterialDetail> details = new System.Collections.Generic.List<Model.ProduceOtherMaterialDetail>();

        public System.Collections.Generic.IList<ProduceOtherMaterialDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
