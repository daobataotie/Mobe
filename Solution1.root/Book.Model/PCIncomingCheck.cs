//------------------------------------------------------------------------------
//
// file name：PCIncomingCheck.cs
// author: mayanjun
// create date：2015/11/8 20:10:10
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class PCIncomingCheck
	{
        System.Collections.Generic.IList<Model.PCIncomingCheckDetail> _detail =new System.Collections.Generic.List<Model.PCIncomingCheckDetail>();

        public System.Collections.Generic.IList<Model.PCIncomingCheckDetail> Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
	}
}