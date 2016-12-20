//------------------------------------------------------------------------------
//
// file name：ProduceOtherExitMaterial.cs
// author: peidun
// create date：2010-1-6 10:20:19
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 外包退料
	/// </summary>
	[Serializable]
	public partial class ProduceOtherExitMaterial
	{
        private System.Collections.Generic.IList<ProduceOtherExitDetail> details;

        public System.Collections.Generic.IList<ProduceOtherExitDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
        
	}
}
