//------------------------------------------------------------------------------
//
// file name：ManProcedure.cs
// author: peidun
// create date：2009-12-8 18:37:05
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 工序可加工物料
	/// </summary>
	[Serializable]
	public partial class ManProcedure
	{
        private System.Collections.Generic.IList<Model.Procedures> _detail = new System.Collections.Generic.List<Model.Procedures>();
        public System.Collections.Generic.IList<Model.Procedures> detail
        {
            get { return _detail; }
            set { _detail = value; }
        
        }
	}
}
