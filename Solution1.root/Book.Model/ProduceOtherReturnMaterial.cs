//------------------------------------------------------------------------------
//
// file name：ProduceOtherReturnMaterial.cs
// author: mayanjun
// create date：2011-08-31 15:05:12
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 委外订单退料
	/// </summary>
	[Serializable]
	public partial class ProduceOtherReturnMaterial
	{
        private System.Collections.Generic.IList<Model.ProduceOtherReturnDetail> details;

        public System.Collections.Generic.IList<Model.ProduceOtherReturnDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
