//------------------------------------------------------------------------------
//
// file name：ProduceOtherInDepot.cs
// author: peidun
// create date：2010-1-8 13:43:40
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 外包入库
	/// </summary>
	[Serializable]
	public partial class ProduceOtherInDepot
	{
        private System.Collections.Generic.IList<ProduceOtherInDepotDetail> details;

        public System.Collections.Generic.IList<ProduceOtherInDepotDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
