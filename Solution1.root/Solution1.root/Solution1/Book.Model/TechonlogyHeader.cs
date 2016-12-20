//------------------------------------------------------------------------------
//
// file name：TechonlogyHeader.cs
// author: peidun
// create date：2009-12-7 14:57:41
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 工艺路线头
	/// </summary>
	[Serializable]
	public partial class TechonlogyHeader
	{
        private System.Collections.Generic.IList<Model.Technologydetails> _detail = new System.Collections.Generic.List<Model.Technologydetails>();
        public System.Collections.Generic.IList<Model.Technologydetails> detail
        {
            get { return _detail; }
            set { _detail = value; }

        }
        public override string ToString()
        {
            return this._techonlogyHeadername;
        }

	}
}
