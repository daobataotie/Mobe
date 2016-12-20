//------------------------------------------------------------------------------
//
// file name：ProduceMaterial.cs
// author: peidun
// create date：2009-12-30 16:33:32
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 加工领料单头
	/// </summary>
	[Serializable]
	public partial class ProduceMaterial
	{
        private System.Collections.Generic.IList<ProduceMaterialdetails> details=new System.Collections.Generic.List<Model.ProduceMaterialdetails>();

        public System.Collections.Generic.IList<ProduceMaterialdetails> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
