//------------------------------------------------------------------------------
//
// file name:InvoicePT.cs
// author: peidun
// create date:2008/6/6 10:00:36
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Book.Model
{
	/// <summary>
	/// 调拨单
	/// </summary>
	[Serializable]
	public partial class InvoicePT : Invoice
	{
        private IList<Model.InvoicePTDetail> details;

        public IList<Model.InvoicePTDetail> Details
        {
            get 
            {
                return details;
            }
            set 
            {
                details = value;
            }
        }
        public string GetNewId()
        {
            return Guid.NewGuid().ToString();
        }
	}
}
