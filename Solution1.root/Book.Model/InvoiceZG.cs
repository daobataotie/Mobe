//------------------------------------------------------------------------------
//
// file name：InvoiceZG.cs
// author: mayanjun
// create date：2012-11-19 14:13:51
//
//------------------------------------------------------------------------------
using System;
namespace Book.Model
{
	/// <summary>
	/// 装柜
	/// </summary>
	[Serializable]
	public partial class InvoiceZG
	{
        /// <summary>
        /// 装柜详细
        /// </summary>
        private System.Collections.Generic.IList<Model.InvoiceZGDetail> details = new System.Collections.Generic.List<Model.InvoiceZGDetail>();

        /// <summary>
        /// 装柜详细
        /// </summary>
        public System.Collections.Generic.IList<Model.InvoiceZGDetail> Details
        {
            get { return details; }
            set { details = value; }
        }
	}
}
