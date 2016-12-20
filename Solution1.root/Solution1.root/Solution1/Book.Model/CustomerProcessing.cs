//------------------------------------------------------------------------------
//
// file name：CustomerProcessing.cs
// author: mayanjun
// create date：2010-7-30 19:31:58
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public partial class CustomerProcessing
	{
        private System.Collections.Generic.IList<Model.CustomerProcessingDetail> _detail = new System.Collections.Generic.List<Model.CustomerProcessingDetail>();
        public System.Collections.Generic.IList<Model.CustomerProcessingDetail> detail
        {
            get { return _detail; }
            set { _detail = value; }

        }
        public override string ToString()
        {
            return Id;
        }
	}
}
