//------------------------------------------------------------------------------
//
// file name：Technologydetails.cs
// author: peidun
// create date：2009-12-8 16:29:53
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 工艺路线明细
	/// </summary>
	[Serializable]
	public partial class Technologydetails
	{
        private System.Collections.Generic.IList<Model.Procedures> _detail = new System.Collections.Generic.List<Model.Procedures>();
        public System.Collections.Generic.IList<Model.Procedures> detail
        {
            get { return _detail; }
            set { _detail = value; }

        }
	}
}
