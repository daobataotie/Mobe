//------------------------------------------------------------------------------
//
// file name：DepotOut.cs
// author: mayanjun
// create date：2010-10-15 15:41:13
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Book.Model
{
	/// <summary>
	/// 出库
	/// </summary>
	[Serializable]
	public partial class DepotOut
	{
        private IList<Model.DepotOutDetail> details = new List<Model.DepotOutDetail>();

        public IList<Model.DepotOutDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
