//------------------------------------------------------------------------------
//
// file name：AtSummon.cs
// author: mayanjun
// create date：2010-11-24 09:40:44
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 传票主档
	/// </summary>
	[Serializable]
	public partial class AtSummon
	{
        private System.Collections.Generic.IList<AtSummonDetail> details;

        public System.Collections.Generic.IList<AtSummonDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
