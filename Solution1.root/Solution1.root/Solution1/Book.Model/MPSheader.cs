//------------------------------------------------------------------------------
//
// file name：MPSheader.cs
// author: peidun
// create date：2009-12-18 11:23:48
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 主生产计划头
	/// </summary>
	[Serializable]
	public partial class MPSheader
	{
        private System.Collections.Generic.IList<MPSdetails> details;

        public System.Collections.Generic.IList<MPSdetails> Details
        {
            get { return details; }
            set { details = value; }
        }

	}
}
