//------------------------------------------------------------------------------
//
// file name：PCEarPressCheck.cs
// author: mayanjun
// create date：2013-08-23 16:50:39
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class PCEarPressCheck
	{
        System.Collections.Generic.IList<Model.PCEarPressCheckDetail> _details = new System.Collections.Generic.List<Model.PCEarPressCheckDetail>();

        public System.Collections.Generic.IList<Model.PCEarPressCheckDetail> Details
        {
            get { return _details; }
            set { _details = value; }
        }
	}
}
