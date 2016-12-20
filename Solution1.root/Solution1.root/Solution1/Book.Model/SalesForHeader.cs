//------------------------------------------------------------------------------
//
// file name：SalesForHeader.cs
// author: peidun
// create date：2009-12-17 15:29:38
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 销售预测单头
	/// </summary>
	[Serializable]
	public partial class SalesForHeader
	{
        private System.Collections.Generic.IList<Model.SalesFordetails> _SalesFordetails;

        public System.Collections.Generic.IList<Model.SalesFordetails> details
        {
            get { return _SalesFordetails; }
            set { _SalesFordetails = value; }
        }
	}
}
