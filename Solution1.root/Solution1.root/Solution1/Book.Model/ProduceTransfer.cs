//------------------------------------------------------------------------------
//
// file name：ProduceTransfer.cs
// author: mayanjun
// create date：2011-4-6 10:53:39
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 生产转移单
	/// </summary>
	[Serializable]
	public partial class ProduceTransfer
	{
        private System.Collections.Generic.IList<ProduceTransferDetail> details = new System.Collections.Generic.List<Model.ProduceTransferDetail>();

        public System.Collections.Generic.IList<ProduceTransferDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
